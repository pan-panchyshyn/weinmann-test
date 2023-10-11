using System.Linq.Expressions;
using Weinmann.Domain.Models;

namespace Weinmann.Core.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        public Task<List<T>> List();
        public Task<List<T>> List(Expression<Func<T, bool>> predicate);
        public Task<T> GetById(int id);
        public Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate);
        public Task Add(T newEntity);
        public Task<T> Update(T entityToUpdate);
        public Task<T> Remove(T entityToRemove);
        public Task<bool> SaveChangesAsync();
    }
}
