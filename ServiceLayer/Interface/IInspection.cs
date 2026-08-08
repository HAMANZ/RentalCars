using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IInspection
    {
        Task<DynamicResponse<List<InspectionDTO>>> GetAllAsync();
        Task<DynamicResponse<InspectionDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(InspectionDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(InspectionDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
