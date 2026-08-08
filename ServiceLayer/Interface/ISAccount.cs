using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface ISAccount
    {
        Task<DynamicResponse<List<SAccountDTO>>> GetAllAsync();
        Task<DynamicResponse<SAccountDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(SAccountDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(SAccountDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
