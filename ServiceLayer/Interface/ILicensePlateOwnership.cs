using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface ILicensePlateOwnership
    {
        Task<DynamicResponse<List<LicensePlateOwnershipDTO>>> GetAllAsync();
        Task<DynamicResponse<LicensePlateOwnershipDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(LicensePlateOwnershipDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(LicensePlateOwnershipDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
