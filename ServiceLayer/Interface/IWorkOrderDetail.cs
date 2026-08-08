using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IWorkOrderDetail
    {
        Task<DynamicResponse<List<WorkOrderDetailDTO>>> GetAllAsync();
        Task<DynamicResponse<WorkOrderDetailDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(WorkOrderDetailDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(WorkOrderDetailDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
