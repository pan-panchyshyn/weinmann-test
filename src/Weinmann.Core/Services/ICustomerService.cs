using Weinmann.Domain.DTOs.Customer;

namespace Weinmann.Core.Services;

public interface ICustomerService
{
    public Task<List<CustomerDTO>> ListCustomers();
    public Task<CustomerDTO> GetCustomerById(int customerId);
    public Task<CustomerDTO> UpdateCustomer(int customerId, UpdateCustomerDTO updateCustomerDTO);
    public Task RemoveCustomer(int customerId);
}
