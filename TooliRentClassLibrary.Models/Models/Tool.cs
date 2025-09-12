using System.ComponentModel.DataAnnotations.Schema;

namespace TooliRentClassLibrary.Models.Models
{
    public class Tool
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;      
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public ICollection<ToolItem> ToolItems { get; set; } = new List<ToolItem>(); //Navigation property to ToolItems

        [NotMapped]
        public int Quantity => ToolItems.Count;

        [NotMapped]
        public int AvailableUnits => ToolItems.Count(ti => ti.Status == ToolStatus.Available);

    }

}
