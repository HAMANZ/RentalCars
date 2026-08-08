using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IPaymentMethod
    {
        Task<DynamicResponse<List<PaymentMethodDTO>>> GetAllAsync();
        Task<DynamicResponse<PaymentMethodDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(PaymentMethodDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(PaymentMethodDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
