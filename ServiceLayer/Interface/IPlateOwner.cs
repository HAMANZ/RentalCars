using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IPlateOwner
    {
        Task<DynamicResponse<List<PlateOwnerDTO>>> GetAllAsync();
        Task<DynamicResponse<PlateOwnerDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(PlateOwnerDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(PlateOwnerDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
