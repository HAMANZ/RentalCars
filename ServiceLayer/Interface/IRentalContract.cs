using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IRentalContract
    {
        Task<DynamicResponse<List<RentalContractDTO>>> GetAllAsync();
        Task<DynamicResponse<RentalContractDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(RentalContractDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(RentalContractDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
