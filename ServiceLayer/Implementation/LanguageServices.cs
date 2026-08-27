
using RepositoryLayer.RespositoryPattern;
using RentalCar.DomainLayer.DTO;
using RentalCar.DomainLayer.Models;
using RentalCar.ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using  RentalCar.DomainLayer.CommonObjects.Responses;
using Language = RentalCar.DomainLayer.Models.Language;
using ServiceLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace RentalCar.ServiceLayer.Implementation
{
    public class LanguageServices : ILanguage
    {
        private readonly IRepository<Language> _repository;
        private RentalCarDbContext _dbContext;
        public LanguageServices(RepositoryLayer.RespositoryPattern.IRepository<Language> rep, RentalCarDbContext dbContext)
        {
           
            this._repository = rep;
            this._dbContext = dbContext;
        }
    
        public Language FromDTOtoModel(LanguageDTO dto)
        {
            Language Model = new Language();
            Model.Id = dto.Id;
            Model.Name = dto.Name;
            Model.Name_ar = dto.Name_ar;
            Model.Name_ex = dto.Name_ex;
            Model.Flag = dto.Flag;
            Model.Is_ltr = dto.Is_ltr;
            return Model;
        }
       
        
        public LanguageDTO FromModeltoDTO(Language model)
        {
            LanguageDTO DTO = new LanguageDTO();
            DTO.Id = model.Id;
            DTO.Name = model.Name;
            DTO.Name_ar = model.Name_ar;
            DTO.Flag = model.Flag;
            DTO.Name_ex = model.Name_ex;
            DTO.LanguageCode = model.LanguageCode;
            DTO.Is_ltr = model.Is_ltr;
            return DTO;
        }

        public List<LanguageDTO> GetLanguages()
        {
            try
            {
              
                List<Language> listModel = _dbContext.Languages.Where(e => e.Is_deleted == false).ToList();
                List<LanguageDTO> listDTO = new List<LanguageDTO>();
                if (listModel.Count != 0)
                {
                    foreach (var item in listModel)
                    {
                        listDTO.Add(FromModeltoDTO(item));
                    }
                }
                return listDTO;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Language Get(string code)
        {
           
            try
            {
                Language Model = _dbContext.Languages.Where(e => e.LanguageCode == code).FirstOrDefault();
              
                
                return Model;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    
        public Language GetById(long Id)
        {
           
            try
            {
                Language Model = _dbContext.Languages.Where(e => e.Id == Id).FirstOrDefault();
              
                
                return Model;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DynamicResponse<List<LanguageDTO>> GetAll()
        {
            DynamicResponse<List<LanguageDTO>> response = new DynamicResponse<List<LanguageDTO>>();

            try
            {
                List<Language> listModel = _dbContext.Languages.Where(e => e.Is_deleted == false).ToList();
                List<LanguageDTO> listDTO = new List<LanguageDTO>();
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



        public DynamicResponse<bool> Add(LanguageDTO toAdd)
        {
            DynamicResponse<bool> response = new DynamicResponse<bool>();

            try
            {

                if (toAdd != null)
                {
                    Language model = FromDTOtoModel(toAdd);
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


        public string GetCurrentLanguage(HttpContext httpContext)
        {
           var LangCookieName = "RentalCarLang";
           var  DefaultLang = "en";

            if (httpContext.Request.Cookies.TryGetValue(LangCookieName, out var lang))
            {
                return lang;
            }

            return DefaultLang;
        }

    }
}
