using System.Linq.Expressions;
using TooliRent.DAL.Repositories.Interfaces;
using TooliRentClassLibrary.Models.Models;

namespace TooliRent.DAL.Repositories
{
    public class ToolRepository : IToolRepository
    {
        public Task AddAsync(Tool entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<Tool, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Tool entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tool>> FindAsync(Expression<Func<Tool, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tool>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Tool?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Tool entity)
        {
            throw new NotImplementedException();
        }
    }
}
