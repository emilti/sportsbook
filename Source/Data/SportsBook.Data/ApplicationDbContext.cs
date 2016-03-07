namespace SportsBook.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Common.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    using SportsBook.Data.Models;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class SportsBookDbContext : IdentityDbContext<AppUser>
    {
        public SportsBookDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<City> Cities { get; set; }

        public virtual IDbSet<Facility> Facilities { get; set; }

        public virtual IDbSet<Event> Events { get; set; }

        public virtual IDbSet<SportCategory> SportCategories { get; set; }

        public virtual IDbSet<FacilityComment> FacilityComments { get; set; }

        public virtual IDbSet<EventComment> EventComments { get; set; }

        public static SportsBookDbContext Create()
        {
            return new SportsBookDbContext();
        }

        // protected override void OnModelCreating(DbModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Facility>().Property(x => x.Longitude).HasPrecision(16, 8);
        //     modelBuilder.Entity<Facility>().Property(x => x.Latitude).HasPrecision(16, 8);
        // }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Facility>().Property(x => x.Longitude).HasPrecision(12, 8);
            modelBuilder.Entity<Facility>().Property(x => x.Latitude).HasPrecision(12, 8);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
