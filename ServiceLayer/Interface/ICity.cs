using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface ICity
    {
        //DynamicResponse<List<LanguageDTO>> GetAll(long LangId);
        DynamicResponse<List<CityDTO>> GetAll();
        DynamicResponse<CityDTO> Get(long Id);
        DynamicResponse<bool> Add(CityDTO ToAdd);
        DynamicResponse<bool> Delete(long Id);
        DynamicResponse<bool> Update(CityDTO ToUpdate);
    }
}
