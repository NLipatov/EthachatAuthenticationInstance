﻿using AuthAPI.Services.Cryptography;
using AuthAPI.Services.JWT;
using AuthAPI.Services.JWT.JwtReading;
using AuthAPI.Services.UserCredentialsValidation;
using AuthAPI.Services.UserProvider;

namespace AuthAPI.Extensions
{
    public static class AuthApiExtensions
    {
        public static void UseCryptographyHelper(this IServiceCollection services)
        {
            services.AddTransient<ICryptographyHelper, CryptographyHelper>();
        }

        public static void UseUserProvider(this IServiceCollection services)
        {
            services.AddScoped<IUserProvider, UserProvider>();
        }

        public static void UseUserCredentialsValidator(this IServiceCollection services)
        {
            services.AddTransient<IUserCredentialsValidator, UserCredentialsValidator>();
        }
    }
}
