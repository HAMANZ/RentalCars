
using DomainLayer.CommonObjects;
using EsnadTakaful.ServiceLayer.Implementation;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentalCar.DomainLayer.LookUpObjects;
using static EsnadTakaful.ServiceLayer.Implementation.AdminServices;

namespace EsnadTakaful.ServiceLayer.Interface
{
    public interface IAdmin
    {

        public bool UpdatePosition(string positionCode, long tableId, bool isAdd, long? recordId, string pos);
        public Task<long> AddContent<T>(T obj, AdminTables table, int LangId);
        public Task<long> AddContentLang<T>(T obj, AdminTables table, long parentId, int LangId);
       public void DeleteContent(long contenId);
        public List<string> GetAttributes(AdminTables table);
        public  Task<GeneralContents<T>> GetContent<T>(AdminTables table,string LangCode,int limit);
        public Task<GeneralContents<T>> GetContentOfItem<T>(AdminTables table,string LangCode, int limit, long mainId);
    }
}
