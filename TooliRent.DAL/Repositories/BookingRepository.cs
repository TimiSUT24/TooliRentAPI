using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Booking>> FindAsync(Expression<Func<Booking, bool>> predicate)
        {
            return await _context.Bookings.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings
                .Include(b => b.ToolItems)
                .ThenInclude(ti => ti.Tool) //WIP
                .Include(u => u.User)               
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetUserBookingsAsync(string userId)
        {
            return await _context.Bookings
                .Include(b => b.ToolItems)
                .ThenInclude(ti => ti.Tool)
                .Include(u => u.User)
                .Where(u => u.UserId == userId)
                .ToListAsync();
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
