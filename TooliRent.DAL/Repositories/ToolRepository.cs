using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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
            await Task.Run(() => _context.Tools.Remove(entity));
        }

        public async Task<IEnumerable<Tool>> FindAsync(Expression<Func<Tool, bool>> predicate)
        {
            return await _context.Tools.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Tool>> GetAllAsync()
        {
            return await _context.Tools
                 .Include(t => t.Category)
                 .Include(t => t.ToolItems)   
                 .Where(t => t.ToolItems.Any(ti => ti.Status == ToolStatus.Available)) 
                 .ToListAsync();
        }

        public async Task<Tool?> GetByNameAsync(string name)
        {
             return await _context.Tools
                .Include(t => t.Category)
                .Include(t => t.ToolItems)
                .FirstOrDefaultAsync(t => t.Name == name);
        }

        public async Task<Tool?> GetByIdAsync(int id)
        {
            return await _context.Tools
                .Include(t => t.Category)
                .Include(t => t.ToolItems)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Tool>> GetFilteredToolsAsync(string? categoryName = null,ToolStatus? status = null, bool? onlyAvailable = null)
        {
            IQueryable<Tool> query = _context.Tools
                .Include(t => t.Category)
                .Include(t => t.ToolItems);

            if(categoryName != null)
            {
                query = query.Where(t => t.Category.Name == categoryName);
            }

            if(status.HasValue)
            {
                query = query.Where(t => t.ToolItems.Any(ti => ti.Status == status.Value));
            }

            if (onlyAvailable == true)
            {
                query = query.Where(t => t.ToolItems.Any(ti => ti.Status == ToolStatus.Available));
            }

            return await query.ToListAsync();
        }

        public async Task DeleteToolItem(int toolId)
        {
            var toolItem = await _context.ToolItems
                .FirstOrDefaultAsync(ti => ti.Id == toolId);  
            
            if(toolItem == null)
            {
                throw new KeyNotFoundException($"ToolItem with ToolId '{toolId}' not found.");
            }

            _context.ToolItems.Remove(toolItem);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tool entity)
        {
            await Task.Run(() => _context.Tools.Update(entity));
        }
    }
}
