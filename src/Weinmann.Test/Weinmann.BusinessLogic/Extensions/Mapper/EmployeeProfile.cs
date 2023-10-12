using AutoMapper;
using Weinmann.Domain.DTOs.Employee;
using Weinmann.Domain.Models;

namespace Weinmann.BusinessLogic.Extensions.Mapper
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dto => dto.BusinessLocationIds, option => option
                    .MapFrom(model => model.EmployeeBusinessLocations
                        .Select(ebc => ebc.BusinessLocationId)))
                .ForMember(dto => dto.CustomerIds, option => option
                    .MapFrom(model => model.EmployeeBusinessLocations
                        .SelectMany(ebc => ebc.BusinessLocation.Customers.Select(customer => customer.Id))));

            CreateMap<EmployeeDTO, Employee>();
        }
    }
}
