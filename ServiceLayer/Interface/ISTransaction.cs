using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface ISTransaction
    {
        Task<DynamicResponse<List<STransactionDTO>>> GetAllAsync();
        Task<DynamicResponse<STransactionDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(STransactionDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(STransactionDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
