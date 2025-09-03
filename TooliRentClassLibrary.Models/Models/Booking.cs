using System;
using System.Collections.Generic;
namespace TooliRentClassLibrary.Models.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!; // Foreign key to ApplicationUser
        public ApplicationUser User { get; set; } = null!; // Navigation property to ApplicationUser

        public int ToolId { get; set; } // Foreign key to Tool
        public Tool Tool { get; set; } = null!; // Navigation property to Tool

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsPickedUp { get; set; } = false;
        public bool IsReturned { get; set; } = false;
        
    }
}
