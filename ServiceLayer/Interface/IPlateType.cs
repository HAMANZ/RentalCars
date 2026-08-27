using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IPlateType
    {
        Task<DynamicResponse<List<PlateTypeDTO>>> GetAllAsync();
        Task<DynamicResponse<PlateTypeDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(PlateTypeDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(PlateTypeDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
