﻿using AuthAPI.Models;
using AuthAPI.Models.Fido2;
using AuthAPI.Models.Notifications;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.DB.DBContext
{
    public class AuthContext : DbContext
    {
        public AuthContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IConfiguration _configuration;

        public DbSet<User> Users { get; set; }
        public DbSet<UserClaim> UsersClaim { get; set; }
        public DbSet<FidoUser> FidoUsers { get; set; }
        public DbSet<FidoCredential> StoredCredentials { get; set; }
        public DbSet<UserNotificationSubscription> NotificationSubscriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration["ConnectionStrings:PostgreSQL_DEV"]);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
