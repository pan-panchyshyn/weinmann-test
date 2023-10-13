using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Weinmann.Core.Repositories;
using Weinmann.Domain.Models;

namespace Weinmann.DataAccess.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly WeinmannDataContext _context;

        public EmployeeRepository(WeinmannDataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<List<Employee>> ListAsync()
        {
            var result = await _context.Employees
                .Include(e => e.EmployeeBusinessLocations)
                    .ThenInclude(ebc => ebc.BusinessLocation)
                .ToListAsync();

            return result;
        }

        public override async Task<List<Employee>> ListAsync(Expression<Func<Employee, bool>> predicate)
        {
            var result = await _context.Employees
                .Include(e => e.EmployeeBusinessLocations)
                    .ThenInclude(ebc => ebc.BusinessLocation)
                .Where(predicate)
                .ToListAsync();

            return result;
        }

        public override async Task<Employee> GetByIdAsync(int id) 
        {
            var result = await _context.Employees
               .Include(e => e.EmployeeBusinessLocations)
                   .ThenInclude(ebc => ebc.BusinessLocation)
               .FirstOrDefaultAsync(employee => employee.Id == id);

            return result;
        }

        public override async Task<Employee> FindByConditionAsync(Expression<Func<Employee, bool>> predicate)
        {
            var result = await _context.Employees
                .Include(e => e.EmployeeBusinessLocations)
                    .ThenInclude(ebc => ebc.BusinessLocation)
                .FirstOrDefaultAsync(predicate);

            return result;
        }
    }
}
