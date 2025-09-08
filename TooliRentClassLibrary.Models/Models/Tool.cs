namespace TooliRentClassLibrary.Models.Models
{
    public class Tool
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;
        public bool IsAvailable => Quantity > 0; 

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>(); // Navigation property to Booking

    }
}
