namespace TooliRentClassLibrary.Models.DTO
{
    public class LoginDtoRespond
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
