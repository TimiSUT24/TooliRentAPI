using System.Linq.Expressions;
using TooliRent.DAL.Repositories.Interfaces;
using TooliRentClassLibrary.Models.Models;

namespace TooliRent.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task AddAsync(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<ApplicationUser, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationUser>> FindAsync(Expression<Func<ApplicationUser, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
