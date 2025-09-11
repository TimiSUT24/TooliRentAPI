using Microsoft.AspNetCore.Identity;

namespace TooliRentClassLibrary.Models.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>(); // Navigation property to Booking
    }
}
