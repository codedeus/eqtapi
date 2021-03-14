using equipment_lease_api.Entities.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.Entities
{
    public class AppDataContext : IdentityDbContext<AppUser>
    {
        public AppDataContext() : base()
        {
        }

        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Subsidiary> Subsidiaries { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<AssetItem> AssetItems { get; set; }
        public virtual DbSet<AssetLease> AssetLeases { get; set; }
        public virtual DbSet<AssetLeaseEntry> AssetLeaseEntries { get; set; }
        public virtual DbSet<AssetGroup> AssetGroups { get; set; }
        public virtual DbSet<AssetLeaseEntryUpdate> AssetLeaseEntryUpdates { get; set; }
        public virtual DbSet<LeaseInvoice> LeaseInvoices { get; set; }
        public virtual DbSet<AssetType> AssetTypes { get; set; }
        public virtual DbSet<AssetBrand> AssetBrands { get; set; }
        public virtual DbSet<ProjectSite> ProjectSites { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<AssetCapacity> AssetCapacities { get; set; }
        public virtual DbSet<AssetDimension> AssetDimensions { get; set; }
        public virtual DbSet<AssetModel> AssetModels { get; set; }
        public virtual DbSet<EngineModel> EngineModels { get; set; }
        public virtual DbSet<EngineType> EngineTypes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlite("Data Source=EquipmentLeaseStore.db");
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("AssetManagerConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
            modelBuilder.ApplyConfiguration(new SubsidiaryConfiguration());
            modelBuilder.ApplyConfiguration(new AssetItemConfiguration());
            modelBuilder.ApplyConfiguration(new AssetLeaseConfiguration());
            modelBuilder.ApplyConfiguration(new AssetLeaseEntryConfiguration());
            modelBuilder.ApplyConfiguration(new AssetGroupConfiguration());
            modelBuilder.ApplyConfiguration(new AssetLeaseEntryUpdateConfiguration());
            modelBuilder.ApplyConfiguration(new LeaseInvoiceConfiguration());
            modelBuilder.ApplyConfiguration(new AssetTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AssetBrandConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectSiteConfiguration());
            modelBuilder.ApplyConfiguration(new AssetConfiguration());
            modelBuilder.ApplyConfiguration(new AssetCapacityConfiguration());
            modelBuilder.ApplyConfiguration(new AssetModelConfiguration());
            modelBuilder.ApplyConfiguration(new AssetDimensionConfiguration());
            modelBuilder.ApplyConfiguration(new EngineTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EngineModelConfiguration());
            //modelBuilder.Entity<Invoice>().HasOne(a => a.Customer)
            //              .WithMany(au => au.Invoices)
            //              .HasForeignKey(a => a.CustomerId)
            //              .IsRequired(false);
        }
    }
}
