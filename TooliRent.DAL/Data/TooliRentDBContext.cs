using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TooliRentClassLibrary.Models.Models;

namespace TooliRent.DAL.Data
{
    public class TooliRentDBContext : IdentityDbContext<ApplicationUser>
    {
        public TooliRentDBContext(DbContextOptions<TooliRentDBContext> options) : base(options)
        {

        }

        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<Tool> Tools { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;   
        
        public DbSet<ToolItem> ToolItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Booking>()
                .HasMany(b => b.ToolItems)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "BookingToolItem",
                    j => j.HasOne<ToolItem>().WithMany().HasForeignKey("ToolItemId").OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<Booking>().WithMany().HasForeignKey("BookingId").OnDelete(DeleteBehavior.Cascade)
                   );

            modelBuilder.Entity<Tool>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Tools)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
