using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRentClassLibrary.Models.DTO.Interfaces;

namespace TooliRentClassLibrary.Models.DTO
{
    public class ToolResponseBorrowedDto : IToolResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int BorrowedUnits { get; set; } 
    }
}
