using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IPlateRegion
    {
        Task<DynamicResponse<List<PlateRegionDTO>>> GetAllAsync();
        Task<DynamicResponse<PlateRegionDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(PlateRegionDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(PlateRegionDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
