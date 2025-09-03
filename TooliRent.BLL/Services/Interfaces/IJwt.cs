namespace TooliRent.BLL.Services.Interfaces
{
    public interface IJwt
    {
        string GenerateToken(string userId, IList<string> roles); 
    }
}
