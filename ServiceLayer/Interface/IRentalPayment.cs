using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IRentalPayment
    {
        Task<DynamicResponse<List<RentalPaymentDTO>>> GetAllAsync();
        Task<DynamicResponse<RentalPaymentDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(RentalPaymentDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(RentalPaymentDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
