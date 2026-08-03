using  RentalCar.DomainLayer.CommonObjects.Responses;
using RentalCar.DomainLayer.DTO;
using RentalCar.DomainLayer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace ServiceLayer.Interface
{
    public interface ILanguage
    {

        public  Language Get(string code);
        public Language GetById(long Id);
        public DynamicResponse<List<LanguageDTO>> GetAll();
        public DynamicResponse<bool> Add(LanguageDTO toAdd);
        public List<LanguageDTO> GetLanguages();
        public string GetCurrentLanguage(HttpContext httpContext);
    }
}
