using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface ICarStatus
    {
        Task<DynamicResponse<List<CarStatusDTO>>> GetAllAsync();
        Task<DynamicResponse<CarStatusDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(CarStatusDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(CarStatusDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
