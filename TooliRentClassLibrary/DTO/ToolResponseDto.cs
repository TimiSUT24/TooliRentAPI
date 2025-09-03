namespace TooliRentClassLibrary.Models.DTO
{
    public class ToolResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;
        public string Category { get; set; } = string.Empty;
    }
}
