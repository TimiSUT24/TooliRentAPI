using TooliRentClassLibrary.Models.Models;

namespace TooliRent.DAL.Repositories.Interfaces
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task<IEnumerable<Booking>> GetUserBookingsAsync(string userId);
    }
}
