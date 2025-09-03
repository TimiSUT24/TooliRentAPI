namespace TooliRentClassLibrary.Models.DTO
{
    public class BookingRequestDto
    {
        public int ToolId { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
