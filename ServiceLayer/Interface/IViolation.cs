using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IViolation
    {
        Task<DynamicResponse<List<ViolationDTO>>> GetAllAsync();
        Task<DynamicResponse<ViolationDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(ViolationDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(ViolationDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
