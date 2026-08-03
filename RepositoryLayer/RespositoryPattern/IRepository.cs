using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.RespositoryPattern
{
    public interface IRepository<T>
    {

        IEnumerable<T> GetAll();
        List<T> GetAllList();
        List<T> GetAllList(int limit);
        List<T> GetAllListPagination(int offset, int limit);
        T GetById(long id);
        T GetByFilter(Expression<Func<T, bool>> filter);
        List<T> GetListByFilter(Expression<Func<T, bool>> filter);
        int GetCount(Expression<Func<T, bool>> filter);
        int Activate(int Id);
        int Deactivate(int Id);
        void Insert(T entity);
        void Remove(T entity);
        Task<T> Update(T entity);
        Task<T> UpdateAsync(T entity);
        Task InsertAsync(T entity);
        Task<List<T>> GetAllListAsync();
        Task<List<T>> GetListByFilterAsync(Expression<Func<T, bool>> filter);
        Task RemoveAsync(T entity);

    }
}
