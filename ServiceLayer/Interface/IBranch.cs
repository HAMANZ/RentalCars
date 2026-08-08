using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IBranch
    {
        Task<DynamicResponse<List<BranchDTO>>> GetAllAsync();
        Task<DynamicResponse<BranchDTO>> GetAsync(int id);
        Task<DynamicResponse<bool>> AddAsync(BranchDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(BranchDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(int id);
    }
}
