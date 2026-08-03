using RentalCar.DomainLayer.Models;
using RentalCar.ServiceLayer.Interface;
using RepositoryLayer.RespositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RentalCar.ServiceLayer.Implementation
{
    public class LookUpMultiLangServices : ILookUpMultiLang
    {
        private readonly IRepository<LookUps> _repository;
        private RentalCarDbContext _dbContext;
        public LookUpMultiLangServices(IRepository<LookUps> rep, RentalCarDbContext dbContext)
        {

            this._repository = rep;
            this._dbContext = dbContext;
        }


        #region Get


        /// <summary>
        /// Gets all lookups even the unpublished
        /// </summary>
        /// <param name="lookUpId"></param>
        /// <param name="langId"></param>
        /// <returns></returns>
        public List<LookUpMultiLang> GetAll(long lookUpId, long langId)
        {
            List<LookUpMultiLang> multis = new List<LookUpMultiLang>();

            try
            {

                multis = _dbContext.LookUpMultiLang
                      .Where(e => e.LookUpId == lookUpId && e.LanguageId == langId)
                      .ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
            return multis;
        }

        public async Task<List<LookUpMultiLang>> GetAllAsync(List<long> lookUpIds, long langId)
        {
            try
            {
                return await _dbContext.LookUpMultiLang
                    .Where(e => lookUpIds.Contains((long)e.LookUpId)
                             && e.LanguageId == langId)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<LookUpMultiLang>> GetAllAsync(long lookUpId, long langId)
        {
            try
            {
                return await _dbContext.LookUpMultiLang
                    .Where(e => e.LookUpId == lookUpId && e.LanguageId == langId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // optional: log exception
                throw;
            }
        }

        public async Task<LookUpMultiLang> GetAsync(long lookUpId, long? langId)
        {
            try
            {
                return await _dbContext.LookUpMultiLang
                    .Where(e => e.LookUpId == lookUpId
                             && e.LanguageId == langId
                             && e.Is_deleted == false)
                    .OrderBy(e => e.Id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<LookUpMultiLang> GetAsync(List<long> lookupIds, long langId, string description)
        {
            try
            {
                return await _dbContext.LookUpMultiLang
                    .Where(e => lookupIds.Contains((long)e.LookUpId)
                             && e.Is_deleted == true
                             && e.LanguageId == langId
                             && e.Description == description)
                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// gets published looks only
        /// </summary>
        /// <param name="lookUpId"></param>
        /// <param name="langId"></param>
        /// <returns></returns>
        public LookUpMultiLang Get(long lookUpId, long? langId)
        {
            try
            {
                return _dbContext.LookUpMultiLang
                         .Where(e => e.LookUpId == lookUpId && e.LanguageId == langId && e.Is_deleted == false)
                         .OrderBy(e => e.Id)
                         .FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public LookUpMultiLang Get(List<long> lookupIds, long langId, string description)
        {
            try
            {
                LookUpMultiLang result = new LookUpMultiLang();

                result = _dbContext.LookUpMultiLang.Where(e => lookupIds.Contains((long)e.LookUpId) && e.Is_deleted == true && e.LanguageId == langId && e.Description == description).FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<LookUpMultiLang> GetAll(List<long> lookUpId, long langId)
        {
            try
            {
                return _dbContext.LookUpMultiLang
                          .Where(e => lookUpId.Contains((long)e.LookUpId) && e.LanguageId == langId)
                          .ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }




        #endregion


        #region Add
        public LookUpMultiLang Add(LookUpMultiLang lookupMulti)
        {
            try
            {
                _dbContext.LookUpMultiLang.Add(lookupMulti);
                _dbContext.SaveChanges();
                return lookupMulti;
            }
            catch (Exception ex)

            {

                throw;
            }
        }

      
        public LookUpMultiLang Add(string description, long lookupId, int langId, long userId)
        {
            try
            {
                LookUpMultiLang add = new LookUpMultiLang();
                add.LookUpId = lookupId;
                add.LanguageId = langId;
                add.SysDate = DateTime.Now;
                add.Description = description;
                add.Is_deleted = false;

                add = Add(add);

                return add;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<LookUpMultiLang> AddAsync(LookUpMultiLang lookupMulti)
        {
            try
            {
                await _dbContext.LookUpMultiLang.AddAsync(lookupMulti);
                await _dbContext.SaveChangesAsync();
                return lookupMulti;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<LookUpMultiLang> AddAsync(string description, long lookupId, int langId, long userId)
        {
            try
            {
                var add = new LookUpMultiLang
                {
                    LookUpId = lookupId,
                    LanguageId = langId,
                    SysDate = DateTime.Now,
                    Description = description,
                    Is_deleted = false
                };

                return await AddAsync(add);
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion


        #region Update


        public void EditDesription(List<long> lookupmultiId, List<string> newDescription)
        {
            try
            {
                LookUpMultiLang ml = new LookUpMultiLang();
               
                for (int i =0; i <= lookupmultiId.Count();i++)
                {
                    ml = _dbContext.LookUpMultiLang.Where(e => e.Id == lookupmultiId[i]).FirstOrDefault();

                    _dbContext.LookUpMultiLang.Attach(ml);
                    ml.Description = newDescription[i];
                }
             
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void EditDesription(long lookupmultiId, string newDescription)
        {
            try
            {
                LookUpMultiLang ml = new LookUpMultiLang();
                ml = _dbContext.LookUpMultiLang.Where(e => e.Id == lookupmultiId).FirstOrDefault();

                _dbContext.LookUpMultiLang.Attach(ml);
                ml.Description = newDescription;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void UpdateDescription(LookUpMultiLang old, string newDescription)
        {
            try
            {
                _dbContext.LookUpMultiLang.Attach(old);

                old.Description = newDescription;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        #endregion


        #region Delete

        public void Delete(long lookupId)
        {
            try
            {
                LookUpMultiLang current = new LookUpMultiLang();
                current = _dbContext.LookUpMultiLang.Where(e => e.LookUpId == lookupId && e.Is_deleted == true).FirstOrDefault();
                if (current != null)
                {
                    _dbContext.LookUpMultiLang.Attach(current);

                    current.Is_deleted = false;
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void DeleteList(List<long> lookupIds)
        {
            try
            {
                List<LookUpMultiLang> toDelete = new List<LookUpMultiLang>();
                toDelete = _dbContext.LookUpMultiLang.Where(e => lookupIds.Contains((long)e.LookUpId) == true).ToList();
                foreach (LookUpMultiLang dl in toDelete)
                {
                    _dbContext.LookUpMultiLang.Attach(dl);
                    dl.Is_deleted = false;

                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        #endregion

       




    }
}
