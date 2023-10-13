using AutoMapper;
using Weinmann.Domain.DTOs.BusinessLocation;
using Weinmann.Domain.Models;

namespace Weinmann.BusinessLogic.Extensions.Mapper
{
    public class BusinessLocationProfile : Profile
    {
        public BusinessLocationProfile()
        {
            CreateMap<BusinessLocationDTO, BusinessLocation>();
            
            CreateMap<BusinessLocation, BusinessLocationDTO>()
                .ForMember(dto => dto.CustomerIds, option => option.MapFrom(model => model.Customers.Select(customer => customer.Id).ToList()));
        }
    }
}
