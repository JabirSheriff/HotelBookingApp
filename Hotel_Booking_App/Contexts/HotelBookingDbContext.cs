using Hotel_Booking_App.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Booking_App.Contexts
{
    public class HotelBookingDbContext : DbContext
    {
        public HotelBookingDbContext(DbContextOptions<HotelBookingDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; } // Fixed casing
        public DbSet<HotelOwner> HotelOwners { get; set; } // Fixed casing

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasDefaultValue("Unassigned");

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .HasColumnType("varbinary(max)");

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordSalt)
                .HasColumnType("varbinary(max)");

            // Hash the admin password and seed the admin user
            var adminPassword = "Admin@123";
            var (passwordHash, passwordSalt) = CreatePasswordHash(adminPassword);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FullName = "Admin",
                    Email = "admin@gmail.com",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Role = "Admin",
                    PhoneNumber = "7397388965",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );
        }

        // Helper method to hash passwords
        private (byte[] PasswordHash, byte[] PasswordSalt) CreatePasswordHash(string password)
        {
            using var hmac = new HMACSHA512();
            return (hmac.ComputeHash(Encoding.UTF8.GetBytes(password)), hmac.Key);
        }
    }
}
