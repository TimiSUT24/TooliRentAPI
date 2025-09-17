using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TooliRent.DAL.Data;
using TooliRent.DAL.Repositories.Interfaces;
using TooliRentClassLibrary.Models.Models;

namespace TooliRent.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly TooliRentDBContext _context;
        public CategoryRepository(TooliRentDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Category entity)
        {
            await _context.Categories.AddAsync(entity);
        }

        public Task<bool> AnyAsync(Expression<Func<Category, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Category entity)
        {
            await Task.Run(() => _context.Categories.Remove(entity));
        }

        public Task<IEnumerable<Category>> FindAsync(Expression<Func<Category, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category entity)
        {
            await Task.Run(() => _context.Categories.Update(entity));
        }
    }
}
