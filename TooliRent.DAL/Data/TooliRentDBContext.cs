using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TooliRentClassLibrary.Models;

namespace TooliRentAPI.Data
{
    public class TooliRentDBContext : IdentityDbContext
    {
        public TooliRentDBContext(DbContextOptions<TooliRentDBContext> options) : base(options)
        {

        }

        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<Tool> Tools { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!; 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Tool)
                .WithMany(t => t.Bookings)
                .HasForeignKey(b => b.ToolId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tool>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Tools)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
