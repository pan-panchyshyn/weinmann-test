using AutoMapper;
using Weinmann.Core.Repositories;
using Weinmann.Core.Services;
using Weinmann.Domain.DTOs.Customer;

namespace Weinmann.BusinessLogic.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<CustomerDTO> GetCustomerById(int customerId)
    {
        var entity = _customerRepository.GetByIdAsync(customerId);

        if (entity == null)
            throw new EntityNotFoundException($"No customer found by given Id: {customerId}");

        var dto = _mapper.Map<CustomerDTO>(entity);

        return dto;
    }

    public async Task<List<CustomerDTO>> ListCustomers()
    {
        var entities = await _customerRepository.ListAsync();

        var dtos = _mapper.Map<List<CustomerDTO>>(entities);

        return dtos;
    }

    public async Task<CustomerDTO> UpdateCustomer(int customerId, UpdateCustomerDTO updateCustomerDTO)
    {
        var entityToUpdate = await _customerRepository.GetByIdAsync(customerId);

        if (entityToUpdate == null)
            throw new EntityNotFoundException($"No customer found by given Id: {customerId}");

        entityToUpdate.UserName = updateCustomerDTO.UserName;
        entityToUpdate.BusinessLocationId = updateCustomerDTO.BusinessLocationId;

        await _customerRepository.UpdateAsync(entityToUpdate);
        await _customerRepository.SaveChangesAsync();

        var dto = _mapper.Map<CustomerDTO>(entityToUpdate);
        return dto;
    }

    public async Task RemoveCustomer(int customerId)
    {
        var entityToRemove = await _customerRepository.GetByIdAsync(customerId);

        if (entityToRemove == null)
            throw new EntityNotFoundException($"No customer found by given Id: {customerId}");

        if (entityToRemove != null)
        {
            await _customerRepository.RemoveAsync(entityToRemove: entityToRemove);
            await _customerRepository.SaveChangesAsync();
        }
    }
}
