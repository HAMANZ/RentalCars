using DomainLayer.CommonObjects;
using  RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using RentalCar.DomainLayer.LookUpObjects;
using RentalCar.DomainLayer.Models;
using System.Collections.Generic;
using static RentalCar.ServiceLayer.Implementation.LookUpLogicservices;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface ILookUpLogic
    {
        public void DeleteContent(long parentId);
        //public long AddContent<T>(T obj, AdminTables table,int LangId);
        //public long AddContentLang<T>(T obj, AdminTables table,long parentId,int LangId);
        public  Task<long> AddContent<T>(T obj, AdminTables table,int LangId);
        public Task<long >AddContentLang<T>(T obj, AdminTables table,long parentId,int LangId);
        public List<LookUps> GetRowsOfTableByCode(long tableId, string code);
      //  public List<T> ReturnListOf<T>(AdminTables table, long langId, int offset, int limit, List<LookUpAttributes> attributes, LookUpAttributes main, List<string> fields, bool isAll, out int total, long? lookUpId);
        public Task<(List<T>,int)> ReturnListOf<T>(AdminTables table, long langId, int offset, int limit, List<LookUpAttributes> attributes, LookUpAttributes main, List<string> fields, bool isAll,  int total, long? lookUpId);


        public List<LookUpAttributes> GetAttributes(AdminTables table, string langCode);
      //  public GeneralContents<T> GetContent<T>(AdminTables table, string langCode, int offset, int limit, List<string> fields, bool isAll, long? LookUpId);
        public Task<GeneralContents<T>> GetContent<T>(AdminTables table, string langCode, int offset, int limit, List<string> fields, bool isAll, long? LookUpId);
       // public bool UpdatePosition(string positionCode, long tableId, bool isAdd, long? recordId, string pos);
        public Task<bool> UpdatePosition(string positionCode, long tableId, bool isAdd, long? recordId, string pos);
    }
}
