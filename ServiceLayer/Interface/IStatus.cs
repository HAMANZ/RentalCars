using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IStatus
    {
        Task<DynamicResponse<List<StatusDTO>>> GetAllAsync();
        Task<DynamicResponse<StatusDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(StatusDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(StatusDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
