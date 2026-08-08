using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IInsuranceType
    {
        Task<DynamicResponse<List<InsuranceTypeDTO>>> GetAllAsync();
        Task<DynamicResponse<InsuranceTypeDTO>> GetAsync(int id);
        Task<DynamicResponse<bool>> AddAsync(InsuranceTypeDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(InsuranceTypeDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(int id);
    }
}
