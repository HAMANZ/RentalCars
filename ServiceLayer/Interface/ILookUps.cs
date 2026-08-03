using  RentalCar.DomainLayer.CommonObjects.Responses;
using RentalCar.DomainLayer.DTO;
using RentalCar.DomainLayer.Models;
using System.Collections.Generic;

namespace RentalCar.ServiceLayer.Interface
{
    public interface ILookUps
    {
        public LookUps Get(long parentId, string code, bool isdiffer);
        public LookUps Get(string code, long parentId, bool isDiffer);
        public LookUps Get(long tableId, long LookUpsId);
        public LookUps Get(long tableId, string toDiffer);
        public List<LookUps> GetList(long parentId, bool NoNeedOnlyToDifferentiate, long tableId);
        public List<LookUps> GetList(long tableId, int offset, int limit);
        public List<LookUps> GetList(long parentId, bool NoNeedOnlyToDifferentiate);
        public List<LookUps> GetList(List<string> codes, long tableid);
        public List<LookUps> GetMono(long tableId, string columnCode);
        public List<LookUps> GetList(long tableId, string code);
        public LookUps Get(string code, long tableId);
        public LookUps Get(long LookUpsId);
        public List<LookUps> GetList(long tableId);
        public List<LookUps> GetChildren(long LookUpsId);

        public LookUps Add(LookUps LookUps);
        public LookUps Add(string code, int tableId, long userId);

        public LookUps UpdateCode(LookUps toUpdate, string newCode);
        public void Delete(string code, long tableId);
        public void Delete(long lkId);
        public List<LookUps> DeleteContent(bool withParent, long parentId);
    }
}
