using AutoMapper;
using Weinmann.Domain.DTOs;
using Weinmann.Domain.Models;

namespace Weinmann.BusinessLogic.Extensions.Mapper
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile() 
        {
            CreateMap<CustomerDTO, Customer>()
                .ForMember(model => model.PasswordHash, option => option.Ignore())
                .ForMember(model => model.PasswordSalt, option => option.Ignore());

            CreateMap<Customer, CustomerDTO>()
                .ForMember(model => model.BusinessLocationName, option => option.MapFrom(dto => dto.BusinessLocation.Name));
        }
    }
}
