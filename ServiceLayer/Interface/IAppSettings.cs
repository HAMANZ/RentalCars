using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using RentalCar.DomainLayer.Model;
using System.Collections.Generic;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IAppSettings
    {
        DynamicResponse<List<AppSettingsDTO>> GetAll(long LangId);
        DynamicResponse<AppSettingsDTO> Get();
        DynamicResponse<bool> Add(AppSettingsDTO ToAdd);
        DynamicResponse<bool> Delete(long Id);
        DynamicResponse<bool> Update(AppSettingsDTO ToUpdate);
    }
}
