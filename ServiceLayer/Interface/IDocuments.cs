using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IDocuments
    {
        Task<DynamicResponse<List<DocumentsDTO>>> GetAllAsync();
        Task<DynamicResponse<DocumentsDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(DocumentsDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(DocumentsDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
