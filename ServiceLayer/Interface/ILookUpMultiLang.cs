using  RentalCar.DomainLayer.CommonObjects.Responses;
using RentalCar.DomainLayer.DTO;
using RentalCar.DomainLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface ILookUpMultiLang
    {
    //    public LookUpMultiLang Addold(LookUpMultiLang lookupMulti);
    //    public LookUpMultiLang Getold(long lookUpId, long? langId);
    //    public List<LookUpMultiLang> GetAllold(long lookUpId, long langId);
    //    public LookUpMultiLang Getold(List<long> lookupIds, long langId, string description);
    //    public List<LookUpMultiLang> GetAllold(List<long> lookUpId, long langId);
    //    public LookUpMultiLang Addold(string description, long lookupId, int langId, long userId);



         public  Task<LookUpMultiLang> AddAsync(LookUpMultiLang lookupMulti);
         public  Task<LookUpMultiLang> GetAsync(long lookUpId, long? langId);
         public  Task<List<LookUpMultiLang>> GetAllAsync(long lookUpId, long langId);
         public  Task<LookUpMultiLang> GetAsync(List<long> lookupIds, long langId, string description);
         public  Task<List<LookUpMultiLang>> GetAllAsync(List<long> lookUpId, long langId);
         public  Task<LookUpMultiLang> AddAsync(string description, long lookupId, int langId, long userId);



        public void EditDesription(long lookupmultiId, string newDescription);
        public void UpdateDescription(LookUpMultiLang old, string newDescription);
        public void DeleteList(List<long> lookupIds);
    }
}
