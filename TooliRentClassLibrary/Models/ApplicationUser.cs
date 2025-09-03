using Microsoft.AspNetCore.Identity;

namespace TooliRentClassLibrary.Models.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>(); // Navigation property to Booking
    }
}
