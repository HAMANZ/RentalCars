using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IInsuranceCompany
    {
        Task<DynamicResponse<List<InsuranceCompanyDTO>>> GetAllAsync();
        Task<DynamicResponse<InsuranceCompanyDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(InsuranceCompanyDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(InsuranceCompanyDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
