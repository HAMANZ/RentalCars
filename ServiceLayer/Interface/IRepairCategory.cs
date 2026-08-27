using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IRepairCategory
    {
        Task<DynamicResponse<List<RepairCategoryDTO>>> GetAllAsync();
        Task<DynamicResponse<RepairCategoryDTO>> GetAsync(int id);
        Task<DynamicResponse<bool>> AddAsync(RepairCategoryDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(RepairCategoryDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(int id);
    }
}
