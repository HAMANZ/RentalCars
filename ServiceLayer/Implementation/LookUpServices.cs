using RentalCar.DomainLayer.Models;
using RentalCar.ServiceLayer.Interface;
using RepositoryLayer.RespositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Implementation
{
    public class LookUpservices : ILookUps
    {
        private readonly IRepository<LookUps> _repository;
        private RentalCarDbContext _dbContext;
        public LookUpservices(IRepository<LookUps> rep, RentalCarDbContext dbContext)
        {

            this._repository = rep;
            this._dbContext = dbContext;
        }


        #region Get

        public LookUps Get(long parentId, string code, bool isdiffer)
        {
            try
            {
                LookUps result = new LookUps();

                result = _dbContext.LookUps
                         .Where(e => e.ParentId == parentId && e.IsDeleted == false && e.Code == code)
                         .FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public LookUps Get(string code, long parentId, bool isDiffer)
        {
            try
            {
                LookUps LookUps = new LookUps();
                try
                {
                    LookUps = _dbContext.LookUps
                            .Where(e => e.Code == code && e.ParentId == parentId && e.IsDeleted == false)
                            .FirstOrDefault();
                    
                }
                catch (Exception ex)
                {

                    throw;
                }
                return LookUps;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public LookUps Get(long tableId, long LookUpsId)
        {
            try
            {
                LookUps result = new LookUps();
                
                result = _dbContext.LookUps.Where(e => e.Id == LookUpsId && e.TableId == tableId && e.IsDeleted == false).FirstOrDefault();

                
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        

        /// <summary>
        /// get last main LookUps of table
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public LookUps Get(long tableId, string toDiffer)
        {
            try
            {
                LookUps result = new LookUps();
                result = _dbContext.LookUps.Where(e => e.ParentId == null && e.TableId == 49 && e.IsDeleted == false).OrderByDescending(s => s.Id).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        /// <summary>
        /// get LookUps for a specific table id for pagination
        /// </summary>
        /// <returns></returns>
        public List<LookUps> GetList(long tableId, int offset, int limit)
        {
            List<LookUps> LookUps = new List<LookUps>();
            try
            {
                LookUps = _dbContext.LookUps
                      .Where(e => e.TableId == tableId)
                      .OrderByDescending(o => o.Id)
                      .Skip(offset)
                      .Take(limit)
                      .ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
            return LookUps;

        }
        public List<LookUps> GetList(long parentId, bool NoNeedOnlyToDifferentiate)
        {
            List<LookUps> LookUps = new List<LookUps>();
            try
            {
                LookUps = _dbContext.LookUps
                      .Where(e => e.ParentId == parentId && e.IsDeleted == false)
                      .ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
            return LookUps;

        }
       
        public List<LookUps> GetList(long parentId, bool NoNeedOnlyToDifferentiate, long tableId)
        {
            List<LookUps> LookUps = new List<LookUps>();
            try
            {
                LookUps = _dbContext.LookUps
                       .Where(e => e.ParentId == parentId && e.IsDeleted == false && tableId == tableId)
                       .ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
            return LookUps;

        }

        public List<LookUps> GetList(List<string> codes, long tableid)
        {
            List<LookUps> LookUps = new List<LookUps>();

            try
            {
                LookUps = _dbContext.LookUps.Where(e => codes.Contains(e.Code) && e.TableId == tableid && e.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
            return LookUps;
        }


        /// <summary>
        /// Returns Titles, Descriptions, Images, and Videos
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="columnCode"></param>
        /// <returns></returns>
        /// 

        public List<LookUps> GetMono(long tableId, string columnCode)
        {
            List<LookUps> looks = new List<LookUps>();
            try
            {
                looks = _dbContext.LookUps
                        .Where(e => e.TableId == tableId && e.Code != columnCode && e.ParentId == null && e.IsDeleted == false)
                        .ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
            return looks;
        }
        /// <summary>
        /// Gets list of LookUps from table using code
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="columnCode"></param>
        /// <returns></returns>
        public List<LookUps> GetList(long tableId, string code)
        {
            List<LookUps> looks = new List<LookUps>();
            try
            {
                looks = _dbContext.LookUps
                        .Where(e => e.TableId == tableId && e.Code == code && e.IsDeleted == false)
                        .OrderByDescending(o => o.Id)
                        .ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
            return looks;
        }
        

        /// <summary>
        /// Gets LookUps using code and tableid
        /// </summary>
        /// <param name="code"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public LookUps Get(string code, long tableId)
        {
            LookUps LookUps = new LookUps();
            try
            {
                LookUps = _dbContext.LookUps
                        .Where(e => e.Code == code && e.TableId == tableId && e.IsDeleted == false)
                        .FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }
            return LookUps;
        }


        /// <summary>
        /// get LookUps for a specific table id 
        /// </summary>
        /// <returns></returns>
        public List<LookUps> GetList(long tableId)
        {
            List<LookUps> LookUps = new List<LookUps>();
            try
            {
                LookUps = _dbContext.LookUps
                        .Where(e => e.TableId == tableId && e.IsDeleted == false)
                        .ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
            return LookUps;

        }

        /// <summary>
        /// get LookUps for a specified row id
        /// </summary>
        /// <param name="LookUpsId"></param>
        /// <returns></returns>
        public LookUps Get(long LookUpsId)
        {
            LookUps LookUps = new LookUps();
            try
            {
                LookUps = _dbContext.LookUps
                       .Where(e => e.Id == LookUpsId)
                       .FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }
            return LookUps;
        }
        public List<LookUps> GetChildren(long LookUpsId)
        {
            List<LookUps> LookUps = new List<LookUps>();
            try
            {
                LookUps = _dbContext.LookUps
                        .Where(e => e.ParentId == LookUpsId && e.IsDeleted == false)
                        .ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
            return LookUps;
        }




        #endregion


        #region Add


        public LookUps Add(LookUps LookUps)
        {
            try
            {
                _dbContext.LookUps.Add(LookUps);
                _dbContext.SaveChanges();
                return LookUps;
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        public LookUps Add(string code, int tableId, long userId)
        {
            try
            {
                LookUps add = new LookUps();

                add.Code = code;
                add.TableId = tableId;
                add.UserId = userId;
                add.SysDate = DateTime.Now;
                add.IsDeleted = false;
                add.ParentId = null;
                add.isPublished = true;

                add = Add(add);

                return add;


            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion

        #region Update

        public LookUps UpdateCode(LookUps toUpdate, string newCode)
        {
            try
            {
                _dbContext.LookUps.Attach(toUpdate);
                toUpdate.Code = newCode;
                _dbContext.SaveChanges();
                return toUpdate;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion


        #region Delete



        public void Delete(string code, long tableId)
        {
            try
            {
                LookUps current = new LookUps();
                current = _dbContext.LookUps.Where(e => e.Code == code && e.TableId == tableId && e.IsDeleted == false).FirstOrDefault();
                if (current != null)
                {
                    _dbContext.LookUps.Attach(current);

                    current.IsDeleted = true;

                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }







        public void Delete(long lkId)
        {
            try
            {
                LookUps current = new LookUps();
                current = _dbContext.LookUps.Where(e => e.Id == lkId && e.IsDeleted == false).FirstOrDefault();
                if (current != null)
                {
                    _dbContext.LookUps.Attach(current);

                    current.IsDeleted = true;

                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<LookUps> DeleteContent(bool withParent, long parentId)
        {
            try
            {
                List<LookUps> result = new List<LookUps>();

                if (withParent)
                {
                    result = _dbContext.LookUps.Where(e => (e.Id == parentId || e.ParentId == parentId)).ToList();
                }
                else
                {
                    result = _dbContext.LookUps.Where(e => (e.Id == parentId || e.ParentId == parentId)).ToList();
                }

                foreach (LookUps l in result)
                {
                    _dbContext.LookUps.Attach(l);
                    l.IsDeleted = true;
                }

                _dbContext.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        #endregion







    }
}
