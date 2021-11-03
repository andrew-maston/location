using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System;

namespace location.Models
{
    public class SpatialDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserLocation> UserLocations { get; set; }

        public SpatialDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Username).HasMaxLength(50);
            });

            modelBuilder.Entity<UserLocation>(entity =>
            {
                entity.ToTable("UserLocation");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Location).IsRequired();

                entity.Property(e => e.Timestamp).HasColumnType("datetime");
            });

            var geometryFactory = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            var userId = Guid.NewGuid();

            modelBuilder.Entity<User>()
            .HasData(
                new User
                {
                    Id = userId,
                    Username = "Test User"
                }
            );

            modelBuilder.Entity<UserLocation>()
                .HasData(
                    new UserLocation
                    {
                        Id = Guid.NewGuid(),
                        UserId = userId,
                        Location = geometryFactory.CreatePoint(new Coordinate(27.175015, 78.042155)),
                        Timestamp = DateTime.UtcNow
                    }
                );
        }
    }


  
}