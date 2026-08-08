using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IInvestor
    {
        Task<DynamicResponse<List<InvestorDTO>>> GetAllAsync();
        Task<DynamicResponse<InvestorDTO>> GetAsync(int id);
        Task<DynamicResponse<bool>> AddAsync(InvestorDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(InvestorDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(int id);
    }
}
