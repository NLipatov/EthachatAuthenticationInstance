﻿using AuthAPI.DB.DBContext;
using AuthAPI.Models;
using AuthAPI.Models.Notifications;
using AuthAPI.Services.JWT;
using AuthAPI.Services.JWT.JwtAuthentication;
using AuthAPI.Services.JWT.JwtReading;
using AuthAPI.Services.UserProvider;
using LimpShared.Models.WebPushNotification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebPushController : ControllerBase
    {
        private readonly AuthContext _authContext;
        private readonly IJwtReader _jwtReader;
        private readonly IUserProvider _userProvider;
        private readonly IJwtAuthenticationService _jwtManager;

        public WebPushController(AuthContext authContext, IJwtReader jwtReader, IUserProvider userProvider, IJwtAuthenticationService jwtManager)
        {
            _authContext = authContext;
            _jwtReader = jwtReader;
            _userProvider = userProvider;
            _jwtManager = jwtManager;
        }

        [HttpPatch("notifications/remove")]
        public async Task DeleteSubsciptions(NotificationSubscriptionDTO[] subscriptionDTOs)
        {
            string? accessToken = subscriptionDTOs
                .FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.AccessToken))
                ?.AccessToken;

            if (string.IsNullOrWhiteSpace(accessToken))
                throw new ArgumentException
                    ($"Cannot delete web push subscriptions: " +
                    $"{nameof(NotificationSubscriptionDTO.AccessToken)} is not a well formed JWT access token.");

            bool accessTokenIsValid = _jwtManager.ValidateAccessToken(accessToken);
            if (!accessTokenIsValid)
                throw new ArgumentException("Cannot delete web push subscriptions: given access token is not valid.");

            string username = _jwtReader.GetUsernameFromAccessToken(accessToken);

            User? user = await _authContext.Users
                .Include(x => x.UserWebPushNotificationSubscriptions)
                .FirstOrDefaultAsync(x => x.Username == username);

            if (user is null)
                throw new ArgumentException($"There is no {nameof(User)} with such username — {username}.");

            var targetSubscriptions = user.UserWebPushNotificationSubscriptions.Where(x => subscriptionDTOs.Any(s => s.Id == x.Id));

            _authContext.RemoveRange(targetSubscriptions);

            await _authContext.SaveChangesAsync();
        }

        [HttpGet("notifications/{username}")]
        public async Task<NotificationSubscriptionDTO[]> GetSubscriptions(string username)
        {
            User? user = await _authContext.Users
                .Include(x => x.UserWebPushNotificationSubscriptions)
                .FirstOrDefaultAsync(x => x.Username == username);

            if (user is null)
                throw new ArgumentException($"There is no {nameof(User)} with such username — {username}.");

            return user.UserWebPushNotificationSubscriptions.Select(x => x.ToDTO()).ToArray();
        }

        [HttpPut("notifications/subscribe")]
        public async Task Subscribe(NotificationSubscriptionDTO subscriptionDTO)
        {
            if (string.IsNullOrWhiteSpace(subscriptionDTO.AccessToken))
                throw new ArgumentException
                    ($"Cannot subscribe to web push: " +
                    $"{nameof(subscriptionDTO.AccessToken)} is not a well formed JWT access token.");

            bool accessTokenIsValid = _jwtManager.ValidateAccessToken(subscriptionDTO.AccessToken);
            if (!accessTokenIsValid)
                throw new ArgumentException("Cannot subscribe to web push: given access token is not valid.");

            string username = _jwtReader.GetUsernameFromAccessToken(subscriptionDTO.AccessToken);

            User? user = await _authContext.Users
                .Include(x => x.UserWebPushNotificationSubscriptions)
                .FirstOrDefaultAsync(x => x.Username == username);

            if (user is null)
                throw new ArgumentException
                    ($"Cannot subscribe to web push: " +
                    $"there is no user with such {nameof(Models.User.Username)} found - '{username}'.");

            _authContext.WebPushNotificationSubscriptions.Add(subscriptionDTO.FromDTO(user));

            await _authContext.SaveChangesAsync();
        }
    }
}
