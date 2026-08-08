using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IDocumentType
    {
        Task<DynamicResponse<List<DocumentTypeDTO>>> GetAllAsync();
        Task<DynamicResponse<DocumentTypeDTO>> GetAsync(long id);
        Task<DynamicResponse<bool>> AddAsync(DocumentTypeDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(DocumentTypeDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(long id);
    }
}
