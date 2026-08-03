using RentalCar.DomainLayer.Models;
using RentalCar.ServiceLayer.Interface;
using RepositoryLayer.RespositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;


namespace RentalCar.ServiceLayer.Implementation
{
   public class LookUpMediaServices : ILookUpMedia
    {
            private readonly IRepository<LookUps> _repository;
            private RentalCarDbContext _dbContext;
            public LookUpMediaServices(IRepository<LookUps> rep, RentalCarDbContext dbContext)
            {

                this._repository = rep;
                this._dbContext = dbContext;
            }
            #region Get

        public Media Get(long lookUpId)
        {
            try
            {
                return _dbContext.Media.Where(e => e.LookUpId == lookUpId).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
      
        public List<Media> GetList()
        {
            List<Media> response = new List<Media>();
            try
            {
                response = _dbContext.Media.Where(e => e.Is_deleted == false).ToList();
                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<Media> Get(long lookUpId, bool isVideo)
        {
            List<Media> Media = new List<Media>();
            try
            {
                Media = _dbContext.Media
                         .Where(e => e.LookUpId == lookUpId && e.Is_deleted == false && e.IsVideo == isVideo && e.Is_deleted == false)
                         .ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
            return Media;
        }


   
        public List<Media> Get(List<long> lookUpIds)
        {
            List<Media> Media = new List<Media>();
            try
            {
                Media = _dbContext.Media
                       .Where(e => lookUpIds.Contains((long)e.LookUpId) && e.Is_deleted == false)
                       .ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
            return Media;
        }


        #endregion


        #region Add
     
        public void Add(Media newMedia)
        {
            try
            {

                _dbContext.Media.Add(newMedia);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        #endregion

        #region Update

        #endregion

        #region Delete

    
        public void Delete(long lookUpId)
        {
            try
            {
                List<Media> toDelete = new List<Media>();
                toDelete = _dbContext.Media.Where(e => e.LookUpId == lookUpId).ToList();

                foreach (Media m in toDelete)
                {
                    _dbContext.Media.Attach(m);
                    m.Is_deleted = true;
                }
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

       
        public void Delete(List<long> lookUpId)
        {
            try
            {
                List<Media> toDelete = new List<Media>();
                toDelete = _dbContext.Media.Where(e => lookUpId.Contains((long)e.LookUpId)).ToList();

                foreach (Media m in toDelete)
                {
                    _dbContext.Media.Attach(m);
                    m.Is_deleted = true;
                }
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion


    }
}
