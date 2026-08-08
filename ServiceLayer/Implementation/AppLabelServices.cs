
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
    public class AppLabelServices : IAppLabel
    {
        private readonly IRepository<AppLabel> _repository;
        private RentalCarDbContext _dbContext;
        public AppLabelServices(IRepository<AppLabel> rep, RentalCarDbContext dbContext)
        {

            this._repository = rep;
            this._dbContext = dbContext;
        }


        #region DTOtoModel/ModeltoDTO 
        public AppLabel FromDTOtoModel(AppLabelDTO dto)
        {
            AppLabel Model = new AppLabel();
            Model.Id = dto.Id;
            Model.LabelName = dto.LabelName;
            Model.FriendlyName = dto.FriendlyName;
            Model.Value = dto.Value;
            Model.Desc = dto.Desc;
            Model.LanguagId = dto.LanguagId;
            Model.Is_deleted = dto.Is_deleted;
            Model.Created_at = dto.Created_at;
            return Model;
        }



        public AppLabelDTO FromModeltoDTO(AppLabel model)
        {
            AppLabelDTO DTO = new AppLabelDTO();
            DTO.Id = model.Id;
            DTO.Id = model.Id;
            DTO.LabelName = model.LabelName;
            DTO.FriendlyName = model.FriendlyName;
            DTO.Value = model.Value;
            DTO.Desc = model.Desc;
            DTO.Is_deleted = model.Is_deleted;
            DTO.Created_at = model.Created_at;
            return DTO;
        }

        #endregion


        #region Get
        public DynamicResponse<AppLabelDTO> Get(long Id)
        {
            DynamicResponse<AppLabelDTO> response = new DynamicResponse<AppLabelDTO>();

            try
            {
                AppLabel Model = _dbContext.AppLabels.Where(e => e.Id == Id).FirstOrDefault();
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
        public DynamicResponse<List<AppLabelDTO>> GetAll(long LangId)
        {
            DynamicResponse<List<AppLabelDTO>> response = new DynamicResponse<List<AppLabelDTO>>();

            try
            {
                List<AppLabel> listModel = _dbContext.AppLabels.Where(e => e.LanguagId == LangId).ToList();
                List<AppLabelDTO> listDTO = new List<AppLabelDTO>();
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
        public async Task<DynamicResponse<List<AppLabelDTO>>> GetAllByLanguageIdAsync(int languageId)
        {
            var response = new DynamicResponse<List<AppLabelDTO>>();

            try
            {
                var listModel = await _dbContext.AppLabels
                    .Where(e => !e.Is_deleted && e.LanguagId == languageId)
                    .ToListAsync();

                var listDTO = new List<AppLabelDTO>();

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
        public DynamicResponse<bool> Add(AppLabelDTO toAdd)
        {
            DynamicResponse<bool> response = new DynamicResponse<bool>();

            try
            {

                if (toAdd != null)
                {
                    AppLabel model = FromDTOtoModel(toAdd);
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
        public DynamicResponse<bool> Update(AppLabelDTO ToUpdate)
        {
            DynamicResponse<bool> response = new DynamicResponse<bool>();

            try
            {
                AppLabel Model = _dbContext.AppLabels.Where(e => e.Id == ToUpdate.Id).FirstOrDefault();
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

        public async Task<DynamicResponse<bool>> UpdateAsync(AppLabelDTO ToUpdate)
        {
            var response = new DynamicResponse<bool>();

            try
            {
                var model = await _dbContext.AppLabels.FindAsync(ToUpdate.Id);
                if (model != null)
                {
                    _repository.Update(FromDTOtoModel(ToUpdate)); 
                    await _dbContext.SaveChangesAsync();         

                    response.Data = true;
                    response.HttpStatusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;
            }

            return response;
        }

        public async Task<DynamicResponse<bool>> UpdateWebsiteLabel(AppLabelDTO en, AppLabelDTO ar)
        {
            var response = new DynamicResponse<bool>();

            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var modelEn = await _dbContext.AppLabels.FindAsync(en.Id);
                var modelAr = await _dbContext.AppLabels.FindAsync(ar.Id);

                if (modelEn == null || modelAr == null)
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                // UPDATE TRACKED ENTITIES (NO new objects!)
                modelEn.LabelName = en.LabelName;
                modelEn.FriendlyName = en.FriendlyName;
                modelEn.Value = en.Value;
                modelEn.Desc = en.Desc;
                modelEn.Updated_at = en.Updated_at;

                modelAr.LabelName = ar.LabelName;
                modelAr.FriendlyName = ar.FriendlyName;
                modelAr.Value = ar.Value;
                modelAr.Desc = ar.Desc;
                modelAr.Updated_at = ar.Updated_at;

                var entries = _dbContext.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged)
                .ToList();

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                response.Data = true;
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                response.Data = false;
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;
            }

            return response;
        }


        #region Delete
        public DynamicResponse<bool> Delete(long Id)
        {
            DynamicResponse<bool> response = new DynamicResponse<bool>();

            try
            {
                AppLabel Model = _dbContext.AppLabels.Where(e => e.Id == Id).FirstOrDefault();
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

        #region Get Value
        public async Task<DynamicResponse<string>> GetValAsync(string LabelName, long LangId)
        {
            DynamicResponse<string> response = new DynamicResponse<string>();

            try
            {
                AppLabel model = await _dbContext.AppLabels
                    .Where(e => e.LabelName == LabelName && e.LanguagId == LangId)
                    .FirstOrDefaultAsync();

                if (model != null)
                    response.Data = model.Value;

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
    }
}
