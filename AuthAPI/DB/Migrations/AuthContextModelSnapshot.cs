﻿// <auto-generated />
using System;
using AuthAPI.DB.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AuthAPI.Migrations
{
    [DbContext(typeof(AuthContext))]
    partial class AuthContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AuthAPI.DB.Models.Fido.FidoCredential", b =>
                {
                    b.Property<Guid>("RecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AaGuid")
                        .HasColumnType("uuid");

                    b.Property<string>("CredType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("DescriptorId")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<int[]>("DescriptorTransports")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.Property<int>("DescriptorType")
                        .HasColumnType("integer");

                    b.Property<byte[]>("PublicKey")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<DateTime>("RegDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("SignatureCounter")
                        .HasColumnType("bigint");

                    b.Property<byte[]>("UserHandle")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("UserId")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.HasKey("RecordId");

                    b.ToTable("StoredCredentials");
                });

            modelBuilder.Entity("AuthAPI.DB.Models.Fido.FidoUser", b =>
                {
                    b.Property<Guid>("RecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "displayName");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<byte[]>("UserId")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.HasKey("RecordId");

                    b.ToTable("FidoUsers");
                });

            modelBuilder.Entity("AuthAPI.DB.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime>("RefreshTokenCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("RefreshTokenExpires")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AuthAPI.DB.Models.UserAccessRefreshEventLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("IssueReason")
                        .HasColumnType("integer");

                    b.Property<string>("UserAgent")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserAgentId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserAccessRefreshEventLogs");
                });

            modelBuilder.Entity("AuthAPI.DB.Models.UserClaim", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("FidoUserRecordId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FidoUserRecordId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersClaim");
                });

            modelBuilder.Entity("AuthAPI.DB.Models.WebPushNotifications.UserWebPushNotificationSubscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Auth")
                        .HasColumnType("text");

                    b.Property<Guid?>("FidoUserRecordId")
                        .HasColumnType("uuid");

                    b.Property<string>("FirebaseRegistrationToken")
                        .HasColumnType("text");

                    b.Property<string>("P256dh")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.Property<Guid?>("UserAgentId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("FidoUserRecordId");

                    b.HasIndex("UserId");

                    b.ToTable("WebPushNotificationSubscriptions");
                });

            modelBuilder.Entity("AuthAPI.DB.Models.UserAccessRefreshEventLog", b =>
                {
                    b.HasOne("AuthAPI.DB.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AuthAPI.DB.Models.UserClaim", b =>
                {
                    b.HasOne("AuthAPI.DB.Models.Fido.FidoUser", null)
                        .WithMany("Claims")
                        .HasForeignKey("FidoUserRecordId");

                    b.HasOne("AuthAPI.DB.Models.User", null)
                        .WithMany("Claims")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("AuthAPI.DB.Models.WebPushNotifications.UserWebPushNotificationSubscription", b =>
                {
                    b.HasOne("AuthAPI.DB.Models.Fido.FidoUser", "FidoUser")
                        .WithMany("UserWebPushNotificationSubscriptions")
                        .HasForeignKey("FidoUserRecordId");

                    b.HasOne("AuthAPI.DB.Models.User", "User")
                        .WithMany("UserWebPushNotificationSubscriptions")
                        .HasForeignKey("UserId");

                    b.Navigation("FidoUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AuthAPI.DB.Models.Fido.FidoUser", b =>
                {
                    b.Navigation("Claims");

                    b.Navigation("UserWebPushNotificationSubscriptions");
                });

            modelBuilder.Entity("AuthAPI.DB.Models.User", b =>
                {
                    b.Navigation("Claims");

                    b.Navigation("UserWebPushNotificationSubscriptions");
                });
#pragma warning restore 612, 618
        }
    }
}
