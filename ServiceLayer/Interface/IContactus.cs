using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainLayer.DTO_EXT;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IContactus
    {
        DynamicResponse<List<ContactusDTO>> GetAllNotSeen();
        DynamicResponse<List<ContactusDTO>> GetAll();
        DynamicResponse<ContactusDTO> Get(long Id);
        Task<DynamicResponse<bool>> Add(ContactusDTO ToAdd);
        Task<DynamicResponse<bool>> AddMessage(ContactUsDTOExt toAdd);
        DynamicResponse<bool> Delete(long Id);
        DynamicResponse<bool> Update(ContactusDTO ToUpdate);
    }
}

