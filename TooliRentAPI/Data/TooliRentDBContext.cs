using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TooliRentAPI.Data
{
    public class TooliRentDBContext : IdentityDbContext
    {
        public TooliRentDBContext(DbContextOptions<TooliRentDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            

        }
    }
}
