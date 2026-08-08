using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface ISAccountType
    {
        Task<DynamicResponse<List<SAccountTypeDTO>>> GetAllAsync();
        Task<DynamicResponse<SAccountTypeDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(SAccountTypeDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(SAccountTypeDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
