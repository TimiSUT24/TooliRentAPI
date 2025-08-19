using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TooliRentClassLibrary.Models
{
    public class Tool
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = new Category();

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>(); // Navigation property to Booking

    }
}
