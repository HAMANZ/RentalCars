using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IAppLabel
    {
        DynamicResponse<List<AppLabelDTO>> GetAll(long LangId);
        Task<DynamicResponse<List<AppLabelDTO>>> GetAllByLanguageIdAsync(int languageId);
        DynamicResponse<AppLabelDTO> Get(long Id);
        DynamicResponse<bool> Add(AppLabelDTO ToAdd);
        DynamicResponse<bool> Delete(long Id);
        DynamicResponse<bool> Update(AppLabelDTO ToUpdate);
        Task<DynamicResponse<bool>> UpdateAsync(AppLabelDTO ToUpdate);
        Task<DynamicResponse<bool>> UpdateWebsiteLabel (AppLabelDTO en, AppLabelDTO ar);
        Task<DynamicResponse<string>> GetValAsync(string LabelName, long LangId);
    }
}
