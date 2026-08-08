using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IInsuranceDocument
    {
        Task<DynamicResponse<List<InsuranceDocumentDTO>>> GetAllAsync();
        Task<DynamicResponse<InsuranceDocumentDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(InsuranceDocumentDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(InsuranceDocumentDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
