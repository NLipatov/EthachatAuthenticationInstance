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
                .HasAnnotation("ProductVersion", "7.0.5")
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
                        .HasColumnType("bytea");

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

                    b.HasIndex("DescriptorId");

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

            modelBuilder.Entity("AuthAPI.DB.Models.RefreshTokenHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserAgent")
                        .HasColumnType("text");

                    b.Property<Guid>("UserAgentId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokenHistories");
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

            modelBuilder.Entity("AuthAPI.DB.Models.UserClaim", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
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

                    b.Property<string>("P256dh")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.Property<Guid?>("UserAgentId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("WebPushNotificationSubscriptions");
                });

            modelBuilder.Entity("Fido2NetLib.Objects.PublicKeyCredentialDescriptor", b =>
                {
                    b.Property<byte[]>("Id")
                        .HasColumnType("bytea")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<int[]>("Transports")
                        .HasColumnType("integer[]")
                        .HasAnnotation("Relational:JsonPropertyName", "transports");

                    b.Property<int?>("Type")
                        .HasColumnType("integer")
                        .HasAnnotation("Relational:JsonPropertyName", "type");

                    b.HasKey("Id");

                    b.ToTable("PublicKeyCredentialDescriptor");
                });

            modelBuilder.Entity("AuthAPI.DB.Models.Fido.FidoCredential", b =>
                {
                    b.HasOne("Fido2NetLib.Objects.PublicKeyCredentialDescriptor", "Descriptor")
                        .WithMany()
                        .HasForeignKey("DescriptorId");

                    b.Navigation("Descriptor");
                });

            modelBuilder.Entity("AuthAPI.DB.Models.RefreshTokenHistory", b =>
                {
                    b.HasOne("AuthAPI.DB.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AuthAPI.DB.Models.UserClaim", b =>
                {
                    b.HasOne("AuthAPI.DB.Models.User", null)
                        .WithMany("Claims")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("AuthAPI.DB.Models.WebPushNotifications.UserWebPushNotificationSubscription", b =>
                {
                    b.HasOne("AuthAPI.DB.Models.User", "User")
                        .WithMany("UserWebPushNotificationSubscriptions")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
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
