using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TooliRentClassLibrary.DTO
{
    public class LoginDtoRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
