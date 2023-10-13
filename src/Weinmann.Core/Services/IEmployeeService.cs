using Weinmann.Domain.DTOs.Employee;

namespace Weinmann.Core.Services
{
    public interface IEmployeeService
    {
        public Task<EmployeeDTO> CreateEmployee(UpdateEmployeeDTO createEmployeeDTO);
        public Task<List<EmployeeDTO>> ListEmployees();
        public Task<EmployeeDTO> GetEmployeeById(int EmployeeId);
        public Task<EmployeeDTO> UpdateEmployee(int EmployeeId, UpdateEmployeeDTO updateEmployeeDTO);
        public Task RemoveEmployee(int EmployeeId);
    }
}
