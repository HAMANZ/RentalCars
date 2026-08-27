
using RepositoryLayer.RespositoryPattern;
using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using RentalCar.DomainLayer.Models;
using RentalCar.ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RentalCar.ServiceLayer.Implementation
{
    public class CountryServices : ICountry
    {
        private readonly IRepository<Country> _repository;
        private RentalCarDbContext _dbContext;
        public CountryServices(IRepository<Country> rep, RentalCarDbContext dbContext)
        {

            this._repository = rep;
            this._dbContext = dbContext;
        }


        #region DTOtoModel/ModeltoDTO 
        public Country FromDTOtoModel(DefinedDTO dto)
        {
            Country Model = new Country();
            Model.Id = dto.Id;
            Model.Name = dto.Name;
            Model.Name_ar = dto.Name_ar;
            Model.Is_deleted = dto.Is_deleted;
            Model.Created_at = dto.Created_at;
            Model.Updated_at = dto.Updated_at;
            return Model;
        }



        public DefinedDTO FromModeltoDTO(Country model)
        {
            DefinedDTO DTO = new DefinedDTO();
            DTO.Id = model.Id;
            DTO.Name = model.Name;
            DTO.Name_ar = model.Name_ar;
            DTO.Is_deleted = model.Is_deleted;
            DTO.Created_at = model.Created_at;
            DTO.Updated_at = model.Updated_at;
            return DTO;
        }

        #endregion


        #region Get
        public DynamicResponse<DefinedDTO> Get(long Id)
        {
            DynamicResponse<DefinedDTO> response = new DynamicResponse<DefinedDTO>();

            try
            {
                Country Model = _dbContext.Countries.Where(e => e.Id == Id).FirstOrDefault();
                response.Data = FromModeltoDTO(Model);
                response.HttpStatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;

                return response;
            }
        }

        #endregion


        //#region GetAll
        //public DynamicResponse<List<DefinedDTO>> GetAll(long LangId)
        //{
        //    DynamicResponse<List<DefinedDTO>> response = new DynamicResponse<List<DefinedDTO>>();

        //    try
        //    {
        //        List<Country> listModel = _dbContext.Countries.Where(e => e.LanguagId == LangId).ToList();
        //        List<DefinedDTO> listDTO = new List<DefinedDTO>();
        //        if (listModel.Count != 0)
        //        {
        //            foreach (var item in listModel)
        //            {
        //                listDTO.Add(FromModeltoDTO(item));
        //            }
        //        }
        //        response.Data = listDTO;
        //        response.HttpStatusCode = HttpStatusCode.OK;

        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.HttpStatusCode = HttpStatusCode.InternalServerError;
        //        response.Message = "Please try again later";
        //        response.ServerMessage = ex.Message;

        //        return response;
        //    }
        //}

        //#endregion



        #region GetAll
        public DynamicResponse<List<DefinedDTO>> GetAll()
        {
            DynamicResponse<List<DefinedDTO>> response = new DynamicResponse<List<DefinedDTO>>();

            try
            {
                List<Country> listModel = _dbContext.Countries.Where(e => e.Is_deleted == false).ToList();
                List<DefinedDTO> listDTO = new List<DefinedDTO>();
                if (listModel.Count != 0)
                {
                    foreach (var item in listModel)
                    {
                        listDTO.Add(FromModeltoDTO(item));
                    }
                }
                response.Data = listDTO;
                response.HttpStatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;

                return response;
            }
        }

        #endregion


        #region Add
        public DynamicResponse<bool> Add(DefinedDTO toAdd)
        {
            DynamicResponse<bool> response = new DynamicResponse<bool>();

            try
            {

                if (toAdd != null)
                {
                    Country model = FromDTOtoModel(toAdd);
                    _repository.Insert(model);
                }

                response.Data = true;
                response.HttpStatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;

                return response;
            }
        }

        #endregion


        #region Update
        public DynamicResponse<bool> Update(DefinedDTO ToUpdate)
        {
            DynamicResponse<bool> response = new DynamicResponse<bool>();

            try
            {

                if (ToUpdate.Id != 0)
                {
                    ToUpdate.Updated_at = DateTime.Now;
                    _dbContext.Update(FromDTOtoModel(ToUpdate));
                    _dbContext.SaveChanges();
                    response.Data = true;
                    response.HttpStatusCode = HttpStatusCode.OK;
                    return response;
                }

                response.Data = false;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                return response;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;

                return response;
            }
        }

        #endregion


        #region Delete
        public DynamicResponse<bool> Delete(long Id)
        {
            DynamicResponse<bool> response = new DynamicResponse<bool>();

            try
            {
                Country Model = _dbContext.Countries.Where(e => e.Id == Id).FirstOrDefault();
                if (Model != null)
                {
                    _repository.Remove(Model);
                    response.Data = true;
                    response.HttpStatusCode = HttpStatusCode.OK;
                    return response;
                }

                response.Data = false;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                return response;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;

                return response;
            }
        }

        #endregion


    }
}
