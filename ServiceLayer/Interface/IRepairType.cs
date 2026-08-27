using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IRepairType
    {
        Task<DynamicResponse<List<RepairTypeDTO>>> GetAllAsync();
        Task<DynamicResponse<RepairTypeDTO>> GetAsync(int id);
        Task<DynamicResponse<bool>> AddAsync(RepairTypeDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(RepairTypeDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(int id);
    }
}
