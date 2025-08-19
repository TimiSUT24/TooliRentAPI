using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TooliRentClassLibrary.DTO
{
    public class BookingRequestDto
    {
        public int ToolId { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
