using System.Linq.Expressions;
using TooliRent.DAL.Data;
using TooliRent.DAL.Repositories.Interfaces;
using TooliRentClassLibrary.Models.Models;

namespace TooliRent.DAL.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly TooliRentDBContext _context;
        public BookingRepository(TooliRentDBContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Booking entity)
        {
            await _context.Bookings.AddAsync(entity);
        }

        public Task<bool> AnyAsync(Expression<Func<Booking, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Booking entity)
        {
            await Task.Run(() => _context.Bookings.Remove(entity));
        }

        public Task<IEnumerable<Booking>> FindAsync(Expression<Func<Booking, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Booking>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Booking?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Booking?> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(Booking entity)
        {
            throw new NotImplementedException();
        }
    }
}
