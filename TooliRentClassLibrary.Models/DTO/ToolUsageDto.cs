using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TooliRentClassLibrary.Models.DTO
{
    public class ToolUsageDto
    {
        public int ToolId { get; set; }
        public string ToolName { get; set; } = string.Empty;
        public int TimesBooked { get; set; }
    }
}
