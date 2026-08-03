
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
    public class AnnouncementsServices : IAnnouncement
    {
        private readonly IRepository<Announcements> _repository;
        private RentalCarDbContext _dbContext;
        public AnnouncementsServices(IRepository<Announcements> rep, RentalCarDbContext dbContext)
        {

            this._repository = rep;
            this._dbContext = dbContext;
        }


        #region DTOtoModel/ModeltoDTO 
        public Announcements FromDTOtoModel(AnnouncementDTO dto)
        {
            Announcements Model = new Announcements();
            Model.Id = dto.Id;
            Model.Icon = dto.Icon;
            Model.Image = dto.Image;
            Model.Content = dto.Content;
            Model.LanguageId = dto.LanguageId;
            Model.PublishDate = dto.PublishDate;
            Model.Is_deleted = dto.Is_deleted;
            Model.Created_at = dto.Created_at;
            Model.Updated_at = dto.Updated_at;
            return Model;
        }



        public AnnouncementDTO FromModeltoDTO(Announcements model)
        {
            AnnouncementDTO DTO = new AnnouncementDTO();
            DTO.Id = model.Id;
            DTO.Icon = model.Icon;
            DTO.Image = model.Image;
            DTO.Content = model.Content;
            DTO.LanguageId = model.LanguageId;
            DTO.PublishDate = model.PublishDate;
            DTO.Is_deleted = model.Is_deleted;
            DTO.Created_at = model.Created_at;
            DTO.Updated_at = model.Updated_at;
            return DTO;
        }

        #endregion


        #region Get
        public DynamicResponse<AnnouncementDTO> Get(long Id)
        {
            DynamicResponse<AnnouncementDTO> response = new DynamicResponse<AnnouncementDTO>();

            try
            {
                Announcements Model = _dbContext.Announcements.Where(e => e.Id == Id).FirstOrDefault();
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
        public DynamicResponse<List<AnnouncementDTO>> GetAll()
        {
            DynamicResponse<List<AnnouncementDTO>> response = new DynamicResponse<List<AnnouncementDTO>>();

            try
            {
                List<Announcements> listModel = _dbContext.Announcements.Where(e => e.Is_deleted == false).ToList();
                List<AnnouncementDTO> listDTO = new List<AnnouncementDTO>();
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

        #region Get All By Language Id - added by Hanine
        public async Task<DynamicResponse<List<AnnouncementDTO>>> GetAllByLanguageIdAsync(long languageId)
        {
            var response = new DynamicResponse<List<AnnouncementDTO>>();

            try
            {
                var listModel = await _dbContext.Announcements
                    .Where(e => !e.Is_deleted && (int)e.LanguageId == languageId)
                    .ToListAsync();

                var listDTO = new List<AnnouncementDTO>();

                foreach (var item in listModel)
                {
                    listDTO.Add(FromModeltoDTO(item));
                }

                response.Data = listDTO;
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;
            }

            return response;
        }


        #endregion

        #region Add
        public DynamicResponse<bool> Add(AnnouncementDTO toAdd)
        {
            DynamicResponse<bool> response = new DynamicResponse<bool>();

            try
            {

                if (toAdd != null)
                {
                    Announcements model = FromDTOtoModel(toAdd);
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
        public DynamicResponse<bool> Update(AnnouncementDTO ToUpdate)
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
                Announcements Model = _dbContext.Announcements.Where(e => e.Id == Id).FirstOrDefault();
                if (Model != null)
                {
                    //_repository.Remove(Model);
                    Model.Is_deleted = true;
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


    }
}
