using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface ICar
    {
        Task<DynamicResponse<List<CarDTO>>> GetAllAsync();
        Task<DynamicResponse<CarDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(CarDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(CarDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
