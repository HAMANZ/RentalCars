using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface ICustomer
    {
        Task<DynamicResponse<List<CustomerDTO>>> GetAllAsync();
        Task<DynamicResponse<CustomerDTO>> GetAsync(string id);
        Task<DynamicResponse<bool>> AddAsync(CustomerDTO dto);
        Task<DynamicResponse<bool>> UpdateAsync(CustomerDTO dto);
        Task<DynamicResponse<bool>> DeleteAsync(string id);

        // Customer documents (additional documents card)
        Task<DynamicResponse<List<CustomerDocumentDTO>>> GetDocumentsAsync(string customerId);
        Task<DynamicResponse<bool>> AddDocumentsAsync(string customerId, List<CustomerDocumentDTO> documents);
        Task<DynamicResponse<bool>> DeleteDocumentAsync(long documentId);
    }
}
