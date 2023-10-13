using AutoMapper;
using Weinmann.Core.Repositories;
using Weinmann.Core.Services;
using Weinmann.Domain.DTOs.Employee;
using Weinmann.Domain.Models;

namespace Weinmann.BusinessLogic.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRepository<EmployeeBusinessLocation> _employeeBusinessLocationRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IRepository<EmployeeBusinessLocation> employeeBusinessLocationRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _employeeBusinessLocationRepository = employeeBusinessLocationRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeDTO> CreateEmployee(UpdateEmployeeDTO createEmployeeDTO)
        {
            var entityToCreate = new Employee
            {
                Email = createEmployeeDTO.Email,
                FirstName = createEmployeeDTO.FirstName,
                LastName = createEmployeeDTO.LastName,
                PhoneNumber = createEmployeeDTO.PhoneNumber
            };

            await _employeeRepository.AddAsync(entityToCreate);
            await _employeeRepository.SaveChangesAsync();

            foreach (var businessLocationId in createEmployeeDTO.BusinessLocationIds)
            {
                await _employeeBusinessLocationRepository.AddAsync(new EmployeeBusinessLocation
                {
                    BusinessLocationId = businessLocationId,
                    EmployeeId = entityToCreate.Id
                });
            }

            await _employeeBusinessLocationRepository.SaveChangesAsync();

            var dto = _mapper.Map<EmployeeDTO>(entityToCreate);

            return dto;
        }

        public async Task<EmployeeDTO> GetEmployeeById(int EmployeeId)
        {
            var entity = _employeeRepository.GetByIdAsync(EmployeeId);

            var dto = _mapper.Map<EmployeeDTO>(entity);

            return dto;
        }

        public async Task<List<EmployeeDTO>> ListEmployees()
        {
            var entities = _employeeRepository.ListAsync();
            
            var dtos = _mapper.Map<List<EmployeeDTO>>(entities);

            return dtos;
        }

        public async Task RemoveEmployee(int EmployeeId)
        {
            var entityToremove = await _employeeRepository.GetByIdAsync(EmployeeId);

            await _employeeRepository.RemoveAsync(entityToremove);
            await _employeeRepository.SaveChangesAsync();
        }

        public async Task<EmployeeDTO> UpdateEmployee(int employeeId, UpdateEmployeeDTO updateEmployeeDTO)
        {
            var entityToUpdate = await _employeeRepository.GetByIdAsync(employeeId);

            entityToUpdate.Email = updateEmployeeDTO.Email;
            entityToUpdate.FirstName = updateEmployeeDTO.FirstName;
            entityToUpdate.LastName = updateEmployeeDTO.LastName;
            entityToUpdate.PhoneNumber = updateEmployeeDTO.PhoneNumber;

            await _employeeRepository.UpdateAsync(entityToUpdate);
            await _employeeRepository.SaveChangesAsync();

            if (updateEmployeeDTO.BusinessLocationIds != null)
            {
                if (!updateEmployeeDTO.BusinessLocationIds.Any())
                {
                    var entitiesToRemove = await _employeeBusinessLocationRepository.ListAsync(data => data.EmployeeId == employeeId);
                    await _employeeBusinessLocationRepository.RemoveRangeAsync(entitiesToRemove);
                }
                else
                {
                    var currentEntities = await _employeeBusinessLocationRepository.ListAsync(data => data.EmployeeId == employeeId);
                    var currentBusinessLocationIds = currentEntities.Select(e => e.BusinessLocationId).ToList();

                    var idsToAdd = updateEmployeeDTO.BusinessLocationIds.Except(currentBusinessLocationIds).ToList();
                    var idsToRemove = currentBusinessLocationIds.Except(idsToAdd).ToList();

                    foreach (var item in idsToAdd)
                    {
                        await _employeeBusinessLocationRepository.AddAsync(new EmployeeBusinessLocation 
                        {
                            BusinessLocationId = item,
                            EmployeeId = employeeId,
                        });
                    }

                    if (idsToRemove.Any())
                    {
                        var entitiesToRemove = await _employeeBusinessLocationRepository.ListAsync(data => idsToRemove.Contains(data.BusinessLocationId));
                        await _employeeBusinessLocationRepository.RemoveRangeAsync(entitiesToRemove);
                    }
                }
                await _employeeBusinessLocationRepository.SaveChangesAsync();
            }

            var dto = _mapper.Map<EmployeeDTO>(entityToUpdate);
            return dto;
        }
    }
}
