using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TooliRentClassLibrary.Models.Models
{
    public class ToolItem
    {
        public int Id { get; set; }
        public int ToolId { get; set; }
        public Tool Tool { get; set; } = null!;
        public ToolStatus Status { get; set; } = ToolStatus.Available;

    }
    public enum ToolStatus
    {
        Available,
        Borrowed,
        Maintenance,
        Retired
    }
}
