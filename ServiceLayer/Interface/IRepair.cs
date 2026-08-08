using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IRepair
    {
        Task<DynamicResponse<List<RepairDTO>>> GetAllAsync();
        Task<DynamicResponse<RepairDTO>> GetAsync(int id);
        Task<DynamicResponse<bool>> AddAsync(RepairDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(RepairDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(int id);
    }
}
