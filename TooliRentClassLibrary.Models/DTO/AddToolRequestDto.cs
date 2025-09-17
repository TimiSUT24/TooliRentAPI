using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRentClassLibrary.Models.Models;

namespace TooliRentClassLibrary.Models.DTO
{
    public class AddToolRequestDto
    {   
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; } 
        public int Quantity { get; set; }
        public ToolStatus Status { get; set; }
    }
}
