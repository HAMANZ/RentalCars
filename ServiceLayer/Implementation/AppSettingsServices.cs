
using RepositoryLayer.RespositoryPattern;
using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using RentalCar.DomainLayer.Models;
using RentalCar.ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using RentalCar.DomainLayer.Model;

namespace RentalCar.ServiceLayer.Implementation
{
    public class AppSettingsServices : IAppSettings
    {
        private readonly IRepository<AppSettings> _repository;
        private RentalCarDbContext _dbContext;
        public AppSettingsServices(IRepository<AppSettings> rep, RentalCarDbContext dbContext)
        {

            this._repository = rep;
            this._dbContext = dbContext;
        }


        #region DTOtoModel/ModeltoDTO 
        public AppSettings FromDTOtoModel(AppSettingsDTO dto)
        {
            AppSettings Model = new AppSettings();
            Model.Id = dto.Id;
            Model.Logo=dto.Logo;
            Model.ApplicationName=dto.ApplicationName ;
            Model.ApplicationUrl = dto.ApplicationUrl;
            Model.ApplicationApiUrl=dto.ApplicationApiUrl;
            Model.ShortDescription=dto.ShortDescription;
            Model.Description=dto.Description;
            Model.ContactWebsite=dto.ContactWebsite;
            Model.ContactEmail=dto.ContactEmail;
            Model.PrivacyPolicy=dto.PrivacyPolicy;
            Model.TermsConditions=dto.TermsConditions;
            Model.LicenseDetail=dto.LicenseDetail;
            Model.RefundPolicy=dto.RefundPolicy;
            Model.Phone=dto.Phone;
            Model.Phone2=dto.Phone2;
            Model.Mobile=dto.Mobile;
            Model.Mobile2= dto.Mobile2;
            Model.Email=dto.Email;
            Model.Password=dto.Password;
            Model.Facebook=dto.Facebook;
            Model.Twitter=dto.Twitter;
            Model.LinkedIn=dto.LinkedIn;
            Model.Youtube=dto.Youtube;
            Model.Instagram=dto.Instagram;
            Model.Snapchat=dto.Snapchat;
            Model.Tiktok=dto.Tiktok;
            Model.Whatsapp=dto.Whatsapp;
            return Model;
        }



        public AppSettingsDTO FromModeltoDTO(AppSettings model)
        {
            AppSettingsDTO DTO = new AppSettingsDTO();
            DTO.Id = model.Id;
            DTO.Logo = model.Logo;
            DTO.ApplicationName = model.ApplicationName;
            DTO.Description = model.Description;
            DTO.ContactWebsite = model.ContactWebsite;
            DTO.ApplicationUrl = model.ApplicationUrl;
            DTO.ApplicationApiUrl = model.ApplicationApiUrl;
            DTO.ShortDescription = model.ShortDescription;
            DTO.Description = model.Description;
            DTO.ContactEmail = model.ContactEmail;
            DTO.PrivacyPolicy = model.PrivacyPolicy;
            DTO.TermsConditions = model.TermsConditions;
            DTO.LicenseDetail = model.LicenseDetail;
            DTO.RefundPolicy = model.RefundPolicy;
            DTO.Phone = model.Phone;
            DTO.Phone2 = model.Phone2;
            DTO.Mobile = model.Mobile;
            DTO.Mobile2 = model.Mobile2;
            DTO.Email = model.Email;
            DTO.Password = model.Password;
            DTO.Facebook = model.Facebook;
            DTO.Twitter = model.Twitter;
            DTO.LinkedIn = model.LinkedIn;
            DTO.Youtube = model.Youtube;
            DTO.Instagram = model.Instagram;
            DTO.Snapchat = model.Snapchat;
            DTO.Tiktok = model.Tiktok;
            DTO.Whatsapp = model.Whatsapp;
            return DTO;
        }

        #endregion


        #region Get
        public DynamicResponse<AppSettingsDTO> Get()
        {
            DynamicResponse<AppSettingsDTO> response = new DynamicResponse<AppSettingsDTO>();

            try
            {
                AppSettings Model = _dbContext.AppSettings.Where(e => e.Is_deleted == false).FirstOrDefault();
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
        public DynamicResponse<List<AppSettingsDTO>> GetAll(long LangId)
        {
            DynamicResponse<List<AppSettingsDTO>> response = new DynamicResponse<List<AppSettingsDTO>>();

            try
            {
                List<AppSettings> listModel = _dbContext.AppSettings.Where(e => e.Is_deleted == false).ToList();
                List<AppSettingsDTO> listDTO = new List<AppSettingsDTO>();
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
        public DynamicResponse<bool> Add(AppSettingsDTO toAdd)
        {
            DynamicResponse<bool> response = new DynamicResponse<bool>();

            try
            {

                if (toAdd != null)
                {
                    AppSettings model = FromDTOtoModel(toAdd);
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
        public DynamicResponse<bool> Update(AppSettingsDTO ToUpdate)
        {
            DynamicResponse<bool> response = new DynamicResponse<bool>();

            try
            {
                AppSettings Model = _dbContext.AppSettings.Where(e => e.Id == ToUpdate.Id).FirstOrDefault();
                if (Model != null)
                {
                    _repository.Update(FromDTOtoModel(ToUpdate));
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
                AppSettings Model = _dbContext.AppSettings.Where(e => e.Id == Id).FirstOrDefault();
                if (Model != null)
                {
                    _repository.Remove(Model);
                    response.Data = true;
                    response.HttpStatusCode = HttpStatusCode.OK;
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
