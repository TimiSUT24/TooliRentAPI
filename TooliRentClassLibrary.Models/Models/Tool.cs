namespace TooliRentClassLibrary.Models.Models
{
    public class Tool
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;        
        public ToolStatus Status { get; set; } = ToolStatus.Available;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>(); // Navigation property to Booking

    }

    public enum ToolStatus
    {
        Available,
        Borrowed,
        Maintenance,
        Retired
    }
}
