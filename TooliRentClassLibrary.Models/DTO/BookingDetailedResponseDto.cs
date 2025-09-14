using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRentClassLibrary.Models.Models;

namespace TooliRentClassLibrary.Models.DTO
{
    public class BookingDetailedResponseDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int ToolId { get; set; }
        public string ToolName { get; set; } = string.Empty;
        public string ToolDescription { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = null!;
    }
}
