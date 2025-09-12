using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TooliRent.DAL.Data;
using TooliRent.DAL.Repositories.Interfaces;
using TooliRentClassLibrary.Models.Models;

namespace TooliRent.DAL.Repositories
{
    public class ToolRepository : IToolRepository
    {
        private readonly TooliRentDBContext _context;
        public ToolRepository(TooliRentDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Tool entity)
        {
            await _context.Tools.AddAsync(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<Tool, bool>> predicate)
        {
            return await _context.Tools.AnyAsync(predicate);
        }

        public async Task DeleteAsync(Tool entity)
        {
            _context.Tools.Remove(entity);          
        }

        public async Task<IEnumerable<Tool>> FindAsync(Expression<Func<Tool, bool>> predicate)
        {
            return await _context.Tools.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Tool>> GetAllAsync()
        {
           throw new NotImplementedException();
        }

        public async Task<Tool?> GetByIdAsync(int id)
        {
             return await _context.Tools.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tool entity)
        {
            _context.Tools.Update(entity);
        }
    }
}
