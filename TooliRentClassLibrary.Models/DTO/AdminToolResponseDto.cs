using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TooliRentClassLibrary.Models.DTO
{
    public class AdminToolResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Category { get; set; } = null!;
        public int AvailableUnits { get; set; }
        public int TotalQuantity { get; set; }

    }
}
