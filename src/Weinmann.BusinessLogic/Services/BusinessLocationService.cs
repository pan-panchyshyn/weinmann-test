using AutoMapper;
using Weinmann.Core.Repositories;
using Weinmann.Core.Services;
using Weinmann.Domain.DTOs.BusinessLocation;
using Weinmann.Domain.Models;

namespace Weinmann.BusinessLogic.Services
{
    internal class BusinessLocationService : IBusinessLocationService
    {
        private readonly IBusinessLocationRepository _businessLocationRepository;
        private readonly IMapper _mapper;

        public BusinessLocationService(IBusinessLocationRepository businessLocationRepository, IMapper mapper)
        {
            _businessLocationRepository = businessLocationRepository;
            _mapper = mapper;
        }

        public async Task<BusinessLocationDTO> CreateBusinessLocation(UpdateBusinessLocationDTO createBusinessLocationDTO)
        {
            var entityToCreate = new BusinessLocation 
            {
                Address = createBusinessLocationDTO.Address,
                Name = createBusinessLocationDTO.Name,
                PhoneNumber = createBusinessLocationDTO.PhoneNumber
            };

            await _businessLocationRepository.AddAsync(entityToCreate);
            await _businessLocationRepository.SaveChangesAsync();

            var dto = _mapper.Map<BusinessLocationDTO>(entityToCreate);
            
            return dto;
        }

        public async Task<BusinessLocationDTO> GetBusinessLocationById(int businessLocationId)
        {
            var entity = await _businessLocationRepository.GetByIdAsync(businessLocationId);

            if (entity == null) 
                throw new EntityNotFoundException($"No business location found by given Id: {businessLocationId}");

            var dto = _mapper.Map<BusinessLocationDTO>(entity);

            return dto;
        }

        public async Task<List<BusinessLocationDTO>> ListBusinessLocations()
        {
            var entities = await _businessLocationRepository.ListAsync();

            var dtos = _mapper.Map<List<BusinessLocationDTO>>(entities);

            return dtos;
        }

        public async Task<BusinessLocationDTO> UpdateBusinessLocation(int businessLocationId, UpdateBusinessLocationDTO updateBusinessLocationDTO)
        {
            var entityToUpdate = await _businessLocationRepository.GetByIdAsync(businessLocationId);

            if (entityToUpdate == null)
                throw new EntityNotFoundException($"No business location found by given Id: {businessLocationId}");

            entityToUpdate.Name = updateBusinessLocationDTO.Name;
            entityToUpdate.PhoneNumber = updateBusinessLocationDTO.PhoneNumber;
            entityToUpdate.Address = updateBusinessLocationDTO.Address;

            await _businessLocationRepository.UpdateAsync(entityToUpdate);
            await _businessLocationRepository.SaveChangesAsync();

            var dto = _mapper.Map<BusinessLocationDTO>(entityToUpdate);
            return dto;
        }

        public async Task RemoveBusinessLocation(int businessLocationId)
        {
            var entityToRemove = await _businessLocationRepository.GetByIdAsync(businessLocationId);

            if (entityToRemove == null)
                throw new EntityNotFoundException($"No business location found by given Id: {businessLocationId}");

            if (entityToRemove != null)
            {
                await _businessLocationRepository.RemoveAsync(entityToRemove: entityToRemove);
                await _businessLocationRepository.SaveChangesAsync();
            }
        }
    }
}
