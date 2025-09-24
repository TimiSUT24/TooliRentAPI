using System;
using System.Collections.Generic;
namespace TooliRentClassLibrary.Models.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!; // Foreign key to ApplicationUser
        public ApplicationUser User { get; set; } = null!; // Navigation property to ApplicationUser
        public ICollection<ToolItem> ToolItems { get; set; } = new List<ToolItem>(); // Navigation property to ToolItems
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        public bool IsLate { get; set; } = false;
        public decimal? Latefee { get; set; } // Should not allow null

    }

    public enum BookingStatus
    {
        Pending,
        Active,
        Completed,
        Cancelled
    }
}
