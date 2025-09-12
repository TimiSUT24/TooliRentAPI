namespace TooliRentClassLibrary.Models.DTO
{
    public class BookingResponseDto
    {
        public int Id { get; set; } 
        public int ToolId { get; set; } 
        public string ToolName { get; set; } = string.Empty;
        public string ToolDescription { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }     
    }
}
