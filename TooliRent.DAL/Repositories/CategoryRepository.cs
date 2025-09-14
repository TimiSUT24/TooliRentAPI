using System.Linq.Expressions;
using TooliRent.DAL.Repositories.Interfaces;
using TooliRentClassLibrary.Models.Models;

namespace TooliRent.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public Task AddAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<Category, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> FindAsync(Expression<Func<Category, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Category?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Category?> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
