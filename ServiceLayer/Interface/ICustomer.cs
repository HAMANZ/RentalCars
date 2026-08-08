using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface ICustomer
    {
        Task<DynamicResponse<List<CustomerDTO>>> GetAllAsync();
        Task<DynamicResponse<CustomerDTO>> GetAsync(int id);
        Task<DynamicResponse<bool>> AddAsync(CustomerDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(CustomerDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(int id);
    }
}
