using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;


namespace GardenPlannerData
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();

        }

        public DbSet<MyPlants> MyPlants { get; set; }
        public DbSet<PlantCare> PlantCare { get; set; }
        public DbSet<PlantDetails> PlantDetails { get; set; }
        public DbSet<Plants> Plants { get; set; }
        public DbSet<PlantSeasons> PlantSeasons { get; set; }
        public DbSet<PlantTypes> PlantTypes { get; set; }
        public DbSet<PlantZones> PlantZones { get; set; }
        public DbSet<RootStructure> RootStructure { get; set; }
        public DbSet<SunExposures> SunExposures { get; set; }
        public DbSet<WaterNeeds> WaterNeeds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                 .Conventions
                 .Remove<PluralizingTableNameConvention>();

            modelBuilder
                .Configurations
                 .Add(new IdentityUserLoginConfiguration())
                .Add(new IdentityUserRoleConfiguration());
        }

        public class IdentityUserLoginConfiguration : EntityTypeConfiguration<IdentityUserLogin>
        {
            public IdentityUserLoginConfiguration()
            {
                HasKey(iul => iul.UserId);
            }
        }


        public class IdentityUserRoleConfiguration : EntityTypeConfiguration<IdentityUserRole>
        {

            public IdentityUserRoleConfiguration()
            {
                HasKey(iur => iur.UserId);
            }

        }
        public DbSet<MyPlants> MyPlants { get; set; }
        public DbSet<PlantCare> PlantCare { get; set; }
        public DbSet<PlantDetails> PlantDetails { get; set; }
        public DbSet<Plants> Plants { get; set; }
        public DbSet<PlantSeasons> PlantSeasons { get; set; }
        public DbSet<PlantTypes> PlantTypes { get; set; }
        public DbSet<PlantZones> PlantZones { get; set; }
        public DbSet<RootStructure> RootStructure { get; set; }
        public DbSet<SunExposures> SunExposures { get; set; }
        public DbSet<WaterNeeds> WaterNeeds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                 .Conventions
                 .Remove<PluralizingTableNameConvention>();

            modelBuilder
                .Configurations
                 .Add(new IdentityUserLoginConfiguration())
                .Add(new IdentityUserRoleConfiguration());
        }

        public class IdentityUserLoginConfiguration : EntityTypeConfiguration<IdentityUserLogin>
        {
            public IdentityUserLoginConfiguration()
            {
                HasKey(iul => iul.UserId);
            }
        }


        public class IdentityUserRoleConfiguration : EntityTypeConfiguration<IdentityUserRole>
        {

            public IdentityUserRoleConfiguration()
            {
                HasKey(iur => iur.UserId);
            }

        }
    }
}