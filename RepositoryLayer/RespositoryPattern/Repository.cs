using Microsoft.EntityFrameworkCore;
using  RentalCar.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.RespositoryPattern
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly RentalCarDbContext _context;
        private readonly DbSet<T> _entities;

        public Repository(RentalCarDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }


        public async Task InsertAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _entities.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _entities.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public IEnumerable<T> GetAll()
        {

            return _entities.AsEnumerable();
        }
        
        public List<T> GetAllList()
        {

            return _entities.ToList();
        }
           
        public List<T> GetAllList(int limit)
        {

            return _entities.Take(limit).ToList();
        }
        
           
        public List<T> GetAllListPagination(int offset, int limit)
        {

            return _entities.Take(limit).Skip(offset).Take(limit).ToList();
        }
        
       
        public T GetById(long id)
        {
            return _entities.Find(id);
        }
        public T GetByFilter(Expression<Func<T, bool>> filter)
        {

            return _entities.SingleOrDefault(filter);
        }

        public List<T> GetListByFilter(Expression<Func<T, bool>> filter)
        {
            return _entities.Where(filter).ToList();
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Add(entity);
            SaveChanges();
        }

        //public async Task<T> Update(T entity)
        //{
        //    bool saveFailed;

        //    do
        //    {
        //        saveFailed = false;
        //                try{
        //                if (entity == null)
        //                {
        //                    throw new ArgumentNullException("entity");
        //                }

        //                _entities.Update(entity);
        //            SaveChangesAsync();
        //                return entity;
        //            }
        //    catch(DbUpdateException ex)
        //    {
        //        saveFailed = true;
        //            var entry = ex.Entries.Single();
        //            var databaseValues = entry.GetDatabaseValues();

        //            entry.OriginalValues.SetValues(entry.GetDatabaseValues());
        //        }
        //    } while (saveFailed);
        //     return entity;
        //}

        public async Task<T> Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _entities.Update(entity);

            bool saveFailed;
            do
            {
                saveFailed = false;
                try
                {
                    await SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    saveFailed = true;
                    var entry = ex.Entries.Single();
                    var databaseValues = entry.GetDatabaseValues();
                    entry.OriginalValues.SetValues(databaseValues);
                }
            } while (saveFailed);

            return entity;
        }

        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        private void SaveChanges()
        {
            _context.SaveChanges();
        }

        public int GetCount(Expression<Func<T, bool>> filter)
        {
            return _entities.Count(filter);
        }
        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Remove(entity);
            SaveChanges();
        }

        public int Activate(int Id)
        {
            throw new NotImplementedException();
        }

        public int Deactivate(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAllListAsync()
        {
            return await _entities.AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetListByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _entities.AsNoTracking().Where(filter).ToListAsync();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task RemoveAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}