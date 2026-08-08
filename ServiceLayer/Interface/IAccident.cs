using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IAccident
    {
        Task<DynamicResponse<List<AccidentDTO>>> GetAllAsync();
        Task<DynamicResponse<AccidentDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(AccidentDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(AccidentDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
