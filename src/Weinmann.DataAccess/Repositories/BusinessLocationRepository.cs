using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Weinmann.Core.Repositories;
using Weinmann.Domain.Models;

namespace Weinmann.DataAccess.Repositories
{
    public class BusinessLocationRepository : Repository<BusinessLocation>, IBusinessLocationRepository
    {
        private readonly WeinmannDataContext _context;
        public BusinessLocationRepository(WeinmannDataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<List<BusinessLocation>> ListAsync()
        {
            var result = await _context.BusinessLocations
                .Include(bLocation => bLocation.Customers)
                .ToListAsync();

            return result;
        }

        public override async Task<List<BusinessLocation>> ListAsync(Expression<Func<BusinessLocation, bool>> predicate)
        {
            var result = await _context.BusinessLocations
                .Include(bLocation => bLocation.Customers)
                .Where(predicate)
                .ToListAsync();

            return result;
        }

        public override async Task<BusinessLocation> GetByIdAsync(int id)
        {
            var result = await _context.BusinessLocations
                .Include(bLocation => bLocation.Customers)
                .FirstOrDefaultAsync(bLocation => bLocation.Id == id);

            return result;
        }

        public override async Task<BusinessLocation> FindByConditionAsync(Expression<Func<BusinessLocation, bool>> predicate)
        {
            var result = await _context.BusinessLocations
                .Include(bLocation => bLocation.Customers)
                .FirstOrDefaultAsync(predicate);

            return result;
        }
    }
}
