using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Weinmann.Core.Repositories;
using Weinmann.Domain.Models;

namespace Weinmann.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly WeinmannDataContext _context;
        private readonly DbSet<T> _entities;

        public Repository(WeinmannDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entities = context.Set<T>();
        }

        public async Task AddAsync(T newEntity)
        {
            await _entities.AddAsync(newEntity);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result = await _entities.FirstOrDefaultAsync(s => s.Id == id);
            return result;
        }

        public async Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            var result = await _context.Set<T>().FirstOrDefaultAsync(predicate);
            return result;
        }

        public async Task<List<T>> ListAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }

        public async Task RemoveAsync(T entityToRemove)
        {
            _entities.Remove(entityToRemove);
        }

        public async Task UpdateAsync(T entityToUpdate)
        {
            _entities.Update(entityToUpdate);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }
    }
}
