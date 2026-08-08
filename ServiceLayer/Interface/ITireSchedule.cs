using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface ITireSchedule
    {
        Task<DynamicResponse<List<TireScheduleDTO>>> GetAllAsync();
        Task<DynamicResponse<TireScheduleDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(TireScheduleDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(TireScheduleDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
