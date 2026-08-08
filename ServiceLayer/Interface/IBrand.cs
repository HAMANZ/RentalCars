using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IBrand
    {
        Task<DynamicResponse<List<BrandDTO>>> GetAllAsync();
        Task<DynamicResponse<BrandDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(BrandDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(BrandDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
