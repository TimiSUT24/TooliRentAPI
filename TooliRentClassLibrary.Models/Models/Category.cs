namespace TooliRentClassLibrary.Models.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!; 

        public ICollection<Tool> Tools { get; set; } = new List<Tool>(); //Navigation property to Tools 
    }
}
