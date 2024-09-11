using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NextDoorBackend.SDK.Entities;
using Npgsql;
using YourNamespace.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NextDoorBackend.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("WebApiDatabase");

            options.UseNpgsql(connectionString, o =>
                    o.UseNetTopologySuite());
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("WebApiDatabase",
        //        npgsqlOptions => npgsqlOptions.UseNetTopologySuite());
        //}

        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<NeighborhoodEntity> Neighborhoods { get; set; }
        public DbSet<AccountsEntity> Accounts { get; set; }
        public DbSet<ProfilesEntity> Profiles { get; set; }
        public DbSet<IndividualProfilesEntity> IndividualProfiles { get; set; }
        public DbSet<BusinessProfilesEntity> BusinessProfiles { get; set; }
        public DbSet<BusinessCategoriesEntity> BusinessCategories { get; set; }
        public DbSet<GendersEntity> Genders { get; set; }
        public DbSet<FriendshipConnectionsEntity> FriendshipConnections { get; set; }
        public DbSet<PostsEntity> Posts { get; set; }
        public DbSet<PostLikesEntity> PostLikes { get; set; }
        public DbSet<PostCommentsEntity> PostComments { get; set; }
        public DbSet<FavoritesEntitys> Favorites { get; set; }
        public DbSet<EventsEntity> Events { get; set; }
        public DbSet<EventsParticipantsEntity> EventParticipants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the one-to-many relationship
            modelBuilder.Entity<GendersEntity>()
             .HasKey(p => p.Id);
            modelBuilder.Entity<BusinessCategoriesEntity>()
             .HasKey(p => p.Id);
            modelBuilder.Entity<ProfilesEntity>()
             .HasKey(p => p.Id);
            modelBuilder.Entity<BusinessProfilesEntity>()
             .HasKey(p => p.Id);
            modelBuilder.Entity<IndividualProfilesEntity>()
             .HasKey(p => p.Id);
            modelBuilder.Entity<AccountsEntity>()
             .HasKey(p => p.Id);
            modelBuilder.Entity<NeighborhoodEntity>()
             .HasKey(p => p.Id);
            modelBuilder.Entity<PostsEntity>()
             .HasKey(p => p.Id);
            modelBuilder.Entity<FriendshipConnectionsEntity>()
             .HasKey(p => p.Id);
            modelBuilder.Entity<PostsEntity>()
             .HasKey(p => p.Id);
            modelBuilder.Entity<PostLikesEntity>()
             .HasKey(p => p.Id);
            modelBuilder.Entity<PostCommentsEntity>()
             .HasKey(p => p.Id);
            modelBuilder.Entity<FavoritesEntitys>()
             .HasKey(p => p.Id);
            modelBuilder.Entity<EventsEntity>()
             .HasKey(p => p.Id);
            modelBuilder.Entity<EventsParticipantsEntity>()
             .HasKey(p => p.Id);


            modelBuilder.Entity<ProfilesEntity>()
                .HasOne(p => p.Account)
                .WithMany(a => a.Profiles)
                .HasForeignKey(p => p.AccountId);

            modelBuilder.Entity<ProfilesEntity>()
         .HasOne(p => p.IndividualProfile)
         .WithOne(ip => ip.Profile)
         .HasForeignKey<IndividualProfilesEntity>(ip => ip.Id);

            modelBuilder.Entity<ProfilesEntity>()
        .HasOne(p => p.BusinessProfile)
        .WithOne(ip => ip.Profile)
        .HasForeignKey<BusinessProfilesEntity>(ip => ip.Id);

            modelBuilder.Entity<IndividualProfilesEntity>()
        .HasOne(ip => ip.Gender)
        .WithMany()
        .HasForeignKey(ip => ip.GenderId);

            modelBuilder.Entity<BusinessProfilesEntity>()
                .HasMany(bp => bp.Categories)
                .WithMany();
            modelBuilder.Entity<PostsEntity>()
     .HasOne(p => p.Profile)
     .WithMany() // Assuming there is no navigation property in ProfilesEntity pointing back to PostsEntity
     .HasForeignKey(p => p.ProfileId)
     .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PostsEntity>()
      .HasOne(p => p.Neighborhood)
      .WithMany() // Assuming there is no navigation property in NeighborhoodsEntity pointing back to PostsEntity
      .HasForeignKey(p => p.NeighborhoodId)
      .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<FriendshipConnectionsEntity>()
       .HasOne(fc => fc.RequestorAccount)
       .WithMany() // Assuming there is no navigation property in AccountsEntity pointing back to FriendshipConnectionsEntity
       .HasForeignKey(fc => fc.RequestorAccountId)
       .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<FriendshipConnectionsEntity>()
     .HasOne(fc => fc.ReceiverAccount)
     .WithMany() // Assuming there is no navigation property in AccountsEntity pointing back to FriendshipConnectionsEntity
     .HasForeignKey(fc => fc.ReceiverAccountId)
     .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PostLikesEntity>()
           .HasOne(pl => pl.Profile)
           .WithMany() // Assuming a Profile can have many likes
           .HasForeignKey(pl => pl.ProfileId)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PostLikesEntity>()
                .HasOne(pl => pl.Post)
                .WithMany(p => p.Likes)// Assuming a Post can have many likes
                .HasForeignKey(pl => pl.PostId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PostCommentsEntity>()
          .HasOne(pc => pc.Profile)
          .WithMany()
          .HasForeignKey(pc => pc.ProfileId)
          .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PostCommentsEntity>()
                .HasOne(pc => pc.Post)
                .WithMany(p => p.Comments) // Use the new Comments property in PostsEntity
                .HasForeignKey(pc => pc.PostId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<FavoritesEntitys>()
           .HasOne(f => f.Profile)
           .WithMany(p => p.Favorites)
           .HasForeignKey(f => f.ProfileId)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FavoritesEntitys>()
                .HasOne(f => f.BusinessProfile)
                .WithMany(b => b.Favorites)
                .HasForeignKey(f => f.BusinessProfileId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<EventsEntity>()
     .HasOne(e => e.Profile)
     .WithMany(p => p.Events)
     .HasForeignKey(e => e.ProfileId)
     .OnDelete(DeleteBehavior.Cascade);

            // Configuring the relationship between Events and Neighborhoods
            modelBuilder.Entity<EventsEntity>()
                .HasOne(e => e.Neighborhood)
                .WithMany()
                .HasForeignKey(e => e.NeighborhoodId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuring the relationship between Events and EventParticipants
            modelBuilder.Entity<EventsEntity>()
                .HasMany(e => e.EventsParticipants)
                .WithOne(ep => ep.Event)
                .HasForeignKey(ep => ep.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EventsParticipantsEntity>()
    .HasOne(ep => ep.Profile)
    .WithMany()
    .HasForeignKey(ep => ep.ProfileId)
    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EventsParticipantsEntity>()
                .HasOne(ep => ep.Event)
                .WithMany(e => e.EventsParticipants)
                .HasForeignKey(ep => ep.EventId)
                .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);
        }
    }
}
