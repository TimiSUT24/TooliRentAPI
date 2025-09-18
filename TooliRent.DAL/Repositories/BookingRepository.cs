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

        public async Task<IEnumerable<(Tool Tool, int count)>> GetToolUsage()
        {
            var bookings = await _context.Bookings
                .Where(b => b.Status == BookingStatus.Completed)
                .Include(b => b.ToolItems)
                .ThenInclude(ti => ti.Tool)               
                .ToListAsync();

            return bookings
                .SelectMany(b => b.ToolItems)
                .GroupBy(ti => ti.Tool)
                .Select(g => (Tool: g.Key, g.Count()))
                .OrderByDescending(tc => tc.Item2)                
                .ToList();
        }    

        public async Task<IEnumerable<Booking>> GetUserBookingsAsync(string userId, BookingStatus? status)
        {
            var query = _context.Bookings
                .Include(b => b.ToolItems)
                .ThenInclude(ti => ti.Tool)
                .Include(u => u.User)
                .Where(u => u.UserId == userId);

            if (status.HasValue)
            {
                query = query.Where(b => b.Status == status.Value);
            }

            return await query.ToListAsync();
                
        }


        public async Task<Booking?> GetByIdAsync(int id, string userId)
        {
            return await _context.Bookings
                .Include(b => b.ToolItems)
                .ThenInclude(ti => ti.Tool) 
                .Include(u => u.User)
                .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
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

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.ToolItems)
                .ThenInclude(ti => ti.Tool)
                .Include(u => u.User)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
