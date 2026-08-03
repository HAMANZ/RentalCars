using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IAnnouncement
    {
        //DynamicResponse<List<LanguageDTO>> GetAll(long LangId);
        DynamicResponse<List<AnnouncementDTO>> GetAll();
        Task<DynamicResponse<List<AnnouncementDTO>>> GetAllByLanguageIdAsync(long languageId);
        DynamicResponse<AnnouncementDTO> Get(long Id);
        DynamicResponse<bool> Add(AnnouncementDTO ToAdd);
        DynamicResponse<bool> Delete(long Id);
        DynamicResponse<bool> Update(AnnouncementDTO ToUpdate);
    }
}
