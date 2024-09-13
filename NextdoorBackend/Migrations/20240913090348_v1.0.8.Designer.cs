﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using NextDoorBackend.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NextDoorBackend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240913090348_v1.0.8")]
    partial class v108
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "postgis");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BusinessCategoriesEntityBusinessProfilesEntity", b =>
                {
                    b.Property<Guid>("BusinessProfilesEntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoriesId")
                        .HasColumnType("uuid");

                    b.HasKey("BusinessProfilesEntityId", "CategoriesId");

                    b.HasIndex("CategoriesId");

                    b.ToTable("BusinessCategoriesEntityBusinessProfilesEntity");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.AccountsEntity", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.BusinessCategoriesEntity", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CategoryDescription")
                        .HasColumnType("text");

                    b.Property<string>("CategoryName")
                        .HasColumnType("text");

                    b.Property<int?>("CategorySubType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("BusinessCategories");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.BusinessProfilesEntity", b =>
                {
                    b.Property<Guid?>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("About")
                        .HasColumnType("text");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("BusinessHours")
                        .HasColumnType("text");

                    b.Property<string>("BusinessName")
                        .HasColumnType("text");

                    b.Property<string>("BusinessStatus")
                        .HasColumnType("text");

                    b.Property<Guid[]>("CategoryId")
                        .HasColumnType("uuid[]");

                    b.Property<string>("CoverPhoto")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<double?>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<string>("LogoPhoto")
                        .HasColumnType("text");

                    b.Property<double?>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<int?>("NeighborhoodId")
                        .HasColumnType("integer");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<string>("Website")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("NeighborhoodId");

                    b.ToTable("BusinessProfiles");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.EmployeeEntity", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.EventsEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("EventDay")
                        .HasColumnType("timestamp with time zone");

                    b.Property<TimeSpan>("EventHour")
                        .HasColumnType("interval");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NeighborhoodId")
                        .HasColumnType("integer");

                    b.Property<string>("OrganizatorName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NeighborhoodId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.EventsParticipantsEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AttendedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("ProfileId");

                    b.ToTable("EventParticipants");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.FavoritesEntitys", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BusinessProfileId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("FavoritedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ProfileId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("BusinessProfileId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.GendersEntity", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("GenderName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Genders");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.IndividualProfilesEntity", b =>
                {
                    b.Property<Guid?>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("Bio")
                        .HasColumnType("text");

                    b.Property<string>("CoverPhoto")
                        .HasColumnType("text");

                    b.Property<Guid?>("GenderId")
                        .HasColumnType("uuid");

                    b.Property<int?>("NeighborhoodId")
                        .HasColumnType("integer");

                    b.Property<string>("ProfilePhoto")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GenderId");

                    b.HasIndex("NeighborhoodId");

                    b.ToTable("IndividualProfiles");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.NeighborhoodEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Polygon>("Bbox")
                        .IsRequired()
                        .HasColumnType("geometry");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Polygon>("Geometry")
                        .IsRequired()
                        .HasColumnType("geometry");

                    b.Property<float>("Importance")
                        .HasColumnType("real");

                    b.Property<long>("OSMId")
                        .HasColumnType("bigint");

                    b.Property<string>("OSMType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("PlaceId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Neighborhoods");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.NotificationsEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool?>("IsRead")
                        .HasColumnType("boolean");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ReadAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("RelatedEntityId")
                        .HasColumnType("uuid");

                    b.Property<int?>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.PostCommentsEntity", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CommentedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("PostId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ProfileId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("ProfileId");

                    b.ToTable("PostComments");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.PostLikesEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LikedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("ProfileId");

                    b.ToTable("PostLikes");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.PostsEntity", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("NeighborhoodId")
                        .HasColumnType("integer");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("text");

                    b.Property<Guid?>("ProfileId")
                        .HasColumnType("uuid");

                    b.Property<string>("Summary")
                        .HasColumnType("text");

                    b.Property<string>("VideoUrl")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("NeighborhoodId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.ProfilesEntity", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uuid");

                    b.Property<string>("ProfileName")
                        .HasColumnType("text");

                    b.Property<string>("ProfileType")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("YourNamespace.Entities.FriendshipConnectionsEntity", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("BlockedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool?>("IsMutual")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("ReceiverAccountId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("RequestedAt")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("RequestorAccountId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("RespondedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverAccountId");

                    b.HasIndex("RequestorAccountId");

                    b.ToTable("FriendshipConnections");
                });

            modelBuilder.Entity("BusinessCategoriesEntityBusinessProfilesEntity", b =>
                {
                    b.HasOne("NextDoorBackend.SDK.Entities.BusinessProfilesEntity", null)
                        .WithMany()
                        .HasForeignKey("BusinessProfilesEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NextDoorBackend.SDK.Entities.BusinessCategoriesEntity", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.BusinessProfilesEntity", b =>
                {
                    b.HasOne("NextDoorBackend.SDK.Entities.ProfilesEntity", "Profile")
                        .WithOne("BusinessProfile")
                        .HasForeignKey("NextDoorBackend.SDK.Entities.BusinessProfilesEntity", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NextDoorBackend.SDK.Entities.NeighborhoodEntity", "Neighborhood")
                        .WithMany()
                        .HasForeignKey("NeighborhoodId");

                    b.Navigation("Neighborhood");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.EventsEntity", b =>
                {
                    b.HasOne("NextDoorBackend.SDK.Entities.NeighborhoodEntity", "Neighborhood")
                        .WithMany()
                        .HasForeignKey("NeighborhoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NextDoorBackend.SDK.Entities.ProfilesEntity", "Profile")
                        .WithMany("Events")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Neighborhood");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.EventsParticipantsEntity", b =>
                {
                    b.HasOne("NextDoorBackend.SDK.Entities.EventsEntity", "Event")
                        .WithMany("EventsParticipants")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NextDoorBackend.SDK.Entities.ProfilesEntity", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.FavoritesEntitys", b =>
                {
                    b.HasOne("NextDoorBackend.SDK.Entities.BusinessProfilesEntity", "BusinessProfile")
                        .WithMany("Favorites")
                        .HasForeignKey("BusinessProfileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NextDoorBackend.SDK.Entities.ProfilesEntity", "Profile")
                        .WithMany("Favorites")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("BusinessProfile");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.IndividualProfilesEntity", b =>
                {
                    b.HasOne("NextDoorBackend.SDK.Entities.GendersEntity", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId");

                    b.HasOne("NextDoorBackend.SDK.Entities.ProfilesEntity", "Profile")
                        .WithOne("IndividualProfile")
                        .HasForeignKey("NextDoorBackend.SDK.Entities.IndividualProfilesEntity", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NextDoorBackend.SDK.Entities.NeighborhoodEntity", "Neighborhood")
                        .WithMany()
                        .HasForeignKey("NeighborhoodId");

                    b.Navigation("Gender");

                    b.Navigation("Neighborhood");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.NotificationsEntity", b =>
                {
                    b.HasOne("NextDoorBackend.SDK.Entities.AccountsEntity", "Account")
                        .WithMany("Notifications")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.PostCommentsEntity", b =>
                {
                    b.HasOne("NextDoorBackend.SDK.Entities.PostsEntity", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NextDoorBackend.SDK.Entities.ProfilesEntity", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Post");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.PostLikesEntity", b =>
                {
                    b.HasOne("NextDoorBackend.SDK.Entities.PostsEntity", "Post")
                        .WithMany("Likes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NextDoorBackend.SDK.Entities.ProfilesEntity", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.PostsEntity", b =>
                {
                    b.HasOne("NextDoorBackend.SDK.Entities.NeighborhoodEntity", "Neighborhood")
                        .WithMany()
                        .HasForeignKey("NeighborhoodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NextDoorBackend.SDK.Entities.ProfilesEntity", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Neighborhood");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.ProfilesEntity", b =>
                {
                    b.HasOne("NextDoorBackend.SDK.Entities.AccountsEntity", "Account")
                        .WithMany("Profiles")
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("YourNamespace.Entities.FriendshipConnectionsEntity", b =>
                {
                    b.HasOne("NextDoorBackend.SDK.Entities.AccountsEntity", "ReceiverAccount")
                        .WithMany()
                        .HasForeignKey("ReceiverAccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NextDoorBackend.SDK.Entities.AccountsEntity", "RequestorAccount")
                        .WithMany()
                        .HasForeignKey("RequestorAccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ReceiverAccount");

                    b.Navigation("RequestorAccount");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.AccountsEntity", b =>
                {
                    b.Navigation("Notifications");

                    b.Navigation("Profiles");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.BusinessProfilesEntity", b =>
                {
                    b.Navigation("Favorites");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.EventsEntity", b =>
                {
                    b.Navigation("EventsParticipants");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.PostsEntity", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Likes");
                });

            modelBuilder.Entity("NextDoorBackend.SDK.Entities.ProfilesEntity", b =>
                {
                    b.Navigation("BusinessProfile")
                        .IsRequired();

                    b.Navigation("Events");

                    b.Navigation("Favorites");

                    b.Navigation("IndividualProfile")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
