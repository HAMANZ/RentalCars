using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IBatterySchedule
    {
        Task<DynamicResponse<List<BatteryScheduleDTO>>> GetAllAsync();
        Task<DynamicResponse<BatteryScheduleDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(BatteryScheduleDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(BatteryScheduleDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
