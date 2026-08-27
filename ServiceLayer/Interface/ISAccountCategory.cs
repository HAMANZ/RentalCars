using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface ISAccountCategory
    {
        Task<DynamicResponse<List<SAccountCategoryDTO>>> GetAllAsync();
        Task<DynamicResponse<SAccountCategoryDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(SAccountCategoryDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(SAccountCategoryDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
