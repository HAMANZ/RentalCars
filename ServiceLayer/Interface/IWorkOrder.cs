using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IWorkOrder
    {
        Task<DynamicResponse<List<WorkOrderDTO>>> GetAllAsync();
        Task<DynamicResponse<WorkOrderDTO>> GetAsync(int id);
        Task<DynamicResponse<bool>> AddAsync(WorkOrderDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(WorkOrderDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(int id);
    }
}
