using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface ICountry
    {
        //DynamicResponse<List<LanguageDTO>> GetAll(long LangId);
        DynamicResponse<List<DefinedDTO>> GetAll();
        DynamicResponse<DefinedDTO> Get(long Id);
        DynamicResponse<bool> Add(DefinedDTO ToAdd);
        DynamicResponse<bool> Delete(long Id);
        DynamicResponse<bool> Update(DefinedDTO ToUpdate);
    }
}
