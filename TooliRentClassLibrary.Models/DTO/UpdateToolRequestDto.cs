using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRentClassLibrary.Models.Models;

namespace TooliRentClassLibrary.Models.DTO
{
    public class UpdateToolRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public ToolStatus? Status { get; set; }
    }
}
