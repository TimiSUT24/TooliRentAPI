using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TooliRentClassLibrary.Models.DTO
{
    public class TokenRefreshRequestDto
    {
        public string Email { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
