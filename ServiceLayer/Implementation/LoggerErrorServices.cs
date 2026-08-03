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

namespace RentalCar.ServiceLayer.Implementation
{
    public class LoggerErrorServices : ILoggerError
    {
        private readonly IRepository<LoggerError> _repository;

        public LoggerErrorServices(IRepository<LoggerError> repository)
        {
            _repository = repository;
        }

        #region DTO to Model / Model to DTO
        private LoggerError FromDTOtoModel(LoggerErrorDTO dto)
        {
            return new LoggerError
            {
                Id = dto.Id,
                MethodName = dto.MethodName,
                ActionType = dto.ActionType,
                Result = dto.Result,
                Parameters = dto.Parameters,
                Is_deleted = dto.Is_deleted,
                Created_at = dto.Created_at,
                Updated_by = dto.Updated_by,
                Updated_at = dto.Updated_at,
                Created_by = dto.Created_by
            };
        }

        private LoggerErrorDTO FromModeltoDTO(LoggerError model)
        {
            return new LoggerErrorDTO
            {
                Id = model.Id,
                MethodName = model.MethodName,
                ActionType = model.ActionType,
                Result = model.Result,
                Parameters = model.Parameters,
                Is_deleted = model.Is_deleted,
                Created_at = model.Created_at,
                Updated_by = model.Updated_by,
                Updated_at = model.Updated_at,
                Created_by = model.Created_by
            };
        }
        #endregion

        #region Get
        public async Task<DynamicResponse<LoggerErrorDTO>> GetAsync(long id)
        {
            var response = new DynamicResponse<LoggerErrorDTO>();
            try
            {
                var model = (await _repository.GetListByFilterAsync(e => e.Id == id)).FirstOrDefault();
                response.Data = model != null ? FromModeltoDTO(model) : null;
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

        #region GetAll
        public async Task<DynamicResponse<List<LoggerErrorDTO>>> GetAllAsync()
        {
            var response = new DynamicResponse<List<LoggerErrorDTO>>();
            try
            {
                var listModel = await _repository.GetAllListAsync();
                response.Data = listModel.Select(FromModeltoDTO).ToList();
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
        public async Task<DynamicResponse<bool>> AddAsync(string methodName, string actionType, string parameters, string result)
        {
            var response = new DynamicResponse<bool>();

            try
            {
                var dto = new LoggerErrorDTO
                {
                    MethodName = methodName,
                    ActionType = actionType,
                    Parameters = parameters,
                    Result = result,
                    Created_at = DateTime.Now,
                    Is_deleted = false
                };

                var model = FromDTOtoModel(dto);
                await _repository.InsertAsync(model);

                response.Data = true;
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

        #region Update
        public async Task<DynamicResponse<bool>> UpdateAsync(LoggerErrorDTO toUpdate)
        {
            var response = new DynamicResponse<bool>();

            try
            {
                var existing = (await _repository.GetListByFilterAsync(e => e.Id == toUpdate.Id)).FirstOrDefault();
                if (existing != null)
                {
                    await _repository.UpdateAsync(FromDTOtoModel(toUpdate));
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
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;
            }

            return response;
        }
        #endregion

        #region Delete
        public async Task<DynamicResponse<bool>> DeleteAsync(long id)
        {
            var response = new DynamicResponse<bool>();

            try
            {
                var existing = (await _repository.GetListByFilterAsync(e => e.Id == id)).FirstOrDefault();
                if (existing != null)
                {
                    await _repository.RemoveAsync(existing);
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
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;
            }

            return response;
        }
        #endregion
    }
}
