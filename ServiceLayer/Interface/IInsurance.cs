using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IInsurance
    {
        Task<DynamicResponse<List<InsuranceDTO>>> GetAllAsync();
        Task<DynamicResponse<InsuranceDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(InsuranceDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(InsuranceDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
