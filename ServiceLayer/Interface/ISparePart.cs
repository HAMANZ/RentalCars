using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface ISparePart
    {
        Task<DynamicResponse<List<SparePartDTO>>> GetAllAsync();
        Task<DynamicResponse<SparePartDTO>> GetAsync(int id);
        Task<DynamicResponse<bool>> AddAsync(SparePartDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(SparePartDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(int id);
    }
}
