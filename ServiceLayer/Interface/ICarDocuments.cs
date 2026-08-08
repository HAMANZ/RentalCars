using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface ICarDocuments
    {
        Task<DynamicResponse<List<CarDocumentsDTO>>> GetAllAsync();
        Task<DynamicResponse<CarDocumentsDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(CarDocumentsDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(CarDocumentsDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
