using JustInTimeCompany.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace JustInTimeCompany.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>
            options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Flight>()
                .HasOne(f => f.From)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Flight>()
                .HasOne(f => f.To)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Flight>()
                .HasOne(f => f.Pilot)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Flight>()
                .HasOne(f => f.Helicopter)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            UserSeed.SeedUsers(builder);
            UserSeed.SeedRoles(builder);
            UserSeed.SeedUserRoles(builder);
            AirportSeed.Seed(builder);
            HelicopterSeed.Seed(builder);
            FlightSeed.Seed(builder);
            ReservationSeed.Seed(builder);
            UserMessageSeed.Seed(builder);
        }

        public DbSet<Airport> Airports { get; set; }
        public DbSet<Helicopter> Helicopters { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightLog>? FlightLog { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }
    }
}