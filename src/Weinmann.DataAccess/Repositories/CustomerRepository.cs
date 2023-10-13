using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Weinmann.Core.Repositories;
using Weinmann.Domain.Models;

namespace Weinmann.DataAccess.Repositories;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    private readonly WeinmannDataContext _context;

    public CustomerRepository(WeinmannDataContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<List<Customer>> ListAsync()
    {
        var result = await _context.Customers
            .Include(customer => customer.BusinessLocation)
            .ToListAsync();

        return result;
    }

    public override async Task<List<Customer>> ListAsync(Expression<Func<Customer, bool>> predicate)
    {
        var result = await _context.Customers
            .Include(customer => customer.BusinessLocation)
            .Where(predicate)
            .ToListAsync();

        return result;
    }

    public override async Task<Customer> GetByIdAsync(int id)
    {
        var result = await _context.Customers
           .Include(customer => customer.BusinessLocation)
           .FirstOrDefaultAsync(employee => employee.Id == id);

        return result;
    }

    public override async Task<Customer> FindByConditionAsync(Expression<Func<Customer, bool>> predicate)
    {
        var result = await _context.Customers
            .Include(customer => customer.BusinessLocation)
            .FirstOrDefaultAsync(predicate);

        return result;
    }
}
