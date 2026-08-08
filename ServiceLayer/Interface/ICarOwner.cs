using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface ICarOwner
    {
        Task<DynamicResponse<List<CarOwnerDTO>>> GetAllAsync();
        Task<DynamicResponse<CarOwnerDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(CarOwnerDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(CarOwnerDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
