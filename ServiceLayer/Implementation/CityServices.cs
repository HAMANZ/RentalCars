
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
    public class CityServices : ICity
    {
        private readonly IRepository<City> _repository;
        private RentalCarDbContext _dbContext;
        public CityServices(IRepository<City> rep, RentalCarDbContext dbContext)
        {

            this._repository = rep;
            this._dbContext = dbContext;
        }


        #region DTOtoModel/ModeltoDTO 
        public City FromDTOtoModel(CityDTO dto)
        {
            City Model = new City();
            Model.Id = dto.Id;
            Model.Name = dto.Name;
            Model.CountryId = dto.CountryId;
            Model.Is_deleted = dto.Is_deleted;
            Model.Created_at = dto.Created_at;
            Model.Updated_at = dto.Updated_at;
            return Model;
        }



        public CityDTO FromModeltoDTO(City model)
        {
            CityDTO DTO = new CityDTO();
            DTO.Id = model.Id;
            DTO.Name = model.Name;
            DTO.CountryId = model.CountryId;
            DTO.Is_deleted = model.Is_deleted;
            DTO.Created_at = model.Created_at;
            DTO.Updated_at = model.Updated_at;
            return DTO;
        }

        #endregion


        #region Get
        public DynamicResponse<CityDTO> Get(long Id)
        {
            DynamicResponse<CityDTO> response = new DynamicResponse<CityDTO>();

            try
            {
                City Model = _dbContext.Cities.Where(e => e.Id == Id).FirstOrDefault();
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


        #region GetAll
        public DynamicResponse<List<CityDTO>> GetAll()
        {
            DynamicResponse<List<CityDTO>> response = new DynamicResponse<List<CityDTO>>();

            try
            {
                List<City> listModel = _dbContext.Cities.Where(e => e.Is_deleted == false).ToList();
                List<CityDTO> listDTO = new List<CityDTO>();
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
        public DynamicResponse<bool> Add(CityDTO toAdd)
        {
            DynamicResponse<bool> response = new DynamicResponse<bool>();

            try
            {

                if (toAdd != null)
                {
                    City model = FromDTOtoModel(toAdd);
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
        public DynamicResponse<bool> Update(CityDTO ToUpdate)
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
                City Model = _dbContext.Cities.Where(e => e.Id == Id).FirstOrDefault();
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
