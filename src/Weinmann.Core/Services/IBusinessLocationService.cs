using Weinmann.Domain.DTOs.BusinessLocation;

namespace Weinmann.Core.Services
{
    public interface IBusinessLocationService
    {
        public Task<BusinessLocationDTO> CreateBusinessLocation(UpdateBusinessLocationDTO createBusinessLocationDTO);
        public Task<List<BusinessLocationDTO>> ListBusinessLocations();
        public Task<BusinessLocationDTO> GetBusinessLocationById(int businessLocationId);
        public Task<BusinessLocationDTO> UpdateBusinessLocation(int businessLocationId, UpdateBusinessLocationDTO updateBusinessLocationDTO);
        public Task RemoveBusinessLocation(int businessLocationId);
    }
}
