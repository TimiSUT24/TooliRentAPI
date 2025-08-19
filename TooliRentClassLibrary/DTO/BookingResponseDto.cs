using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TooliRentClassLibrary.DTO
{
    public class BookingResponseDto
    {
        public int Id { get; set; } 
        public int ToolId { get; set; } 
        public string ToolName { get; set; } = string.Empty;
        public string ToolDescription { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPickedUp { get; set; } = false;
        public bool IsReturned { get; set; } = false;
    }
}
