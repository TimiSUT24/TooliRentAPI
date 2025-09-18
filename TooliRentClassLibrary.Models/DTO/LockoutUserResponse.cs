using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TooliRentClassLibrary.Models.DTO
{
    public class LockoutUserResponse
    {
        public string Message { get; set; } = "User has been locked out successfully";  
        public string Email { get; set; } = string.Empty;
        public DateTimeOffset LockoutEnd { get; set; }

    }
}
