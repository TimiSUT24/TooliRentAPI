using TooliRentClassLibrary.Models.Models;

namespace TooliRentClassLibrary.Models.DTO
{
    public class BookingRequestDto
    {
        public string ToolName { get; set; } = string.Empty;       
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
