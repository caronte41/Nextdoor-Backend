using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NextDoorBackend.SDK.Entities;
using Npgsql;
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
               

            base.OnModelCreating(modelBuilder);
        }
    }
}
