using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IFuelType
    {
        Task<DynamicResponse<List<FuelTypeDTO>>> GetAllAsync();
        Task<DynamicResponse<FuelTypeDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(FuelTypeDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(FuelTypeDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
