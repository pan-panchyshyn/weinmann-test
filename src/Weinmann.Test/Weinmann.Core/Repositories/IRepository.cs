using System.Linq.Expressions;
using Weinmann.Domain.Models;

namespace Weinmann.Core.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        public Task AddAsync(T newEntity);
        public Task<List<T>> ListAsync();
        public Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate);
        public Task<T> GetByIdAsync(int id);
        public Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate);
        public Task UpdateAsync(T entityToUpdate);
        public Task RemoveAsync(T entityToRemove);

        public Task RemoveRangeAsync(List<T> entitiesToRemove);
        public Task<bool> SaveChangesAsync();
    }
}
