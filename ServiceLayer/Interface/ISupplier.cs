using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface ISupplier
    {
        Task<DynamicResponse<List<SupplierDTO>>> GetAllAsync();
        Task<DynamicResponse<SupplierDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(SupplierDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(SupplierDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
