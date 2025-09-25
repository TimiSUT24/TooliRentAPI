using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TooliRentClassLibrary.Models.DTO
{
    public class UpdateCategoryRequestDto
    {
        public string CategoryName { get; set; } = string.Empty;
        public string NewCategoryName { get; set; } = string.Empty;
    }
}
