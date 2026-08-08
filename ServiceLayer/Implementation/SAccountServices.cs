using Microsoft.EntityFrameworkCore;
using RepositoryLayer.RespositoryPattern;
using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using RentalCar.DomainLayer.Models;
using RentalCar.ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Implementation
{
    public class SAccountServices : ISAccount
    {
        private readonly IRepository<SAccount> _repository;
        private readonly RentalCarDbContext _dbContext;

        public SAccountServices(IRepository<SAccount> rep, RentalCarDbContext dbContext)
        {
            _repository = rep;
            _dbContext = dbContext;
        }

        #region DTOtoModel / ModeltoDTO
        // NOTE: Balance is intentionally NOT mapped from the DTO — it is only mutated by the transaction engine.
        public SAccount FromDTOtoModel(SAccountDTO dto)
        {
            return new SAccount
            {
                AccountId = dto.AccountId,
                AccountTypeId = dto.AccountTypeId,
                OwnerType = dto.OwnerType,
                OwnerId = dto.OwnerId,
                Code = dto.Code,
                Name = dto.Name,
                Currency = dto.Currency,
                IsActive = dto.IsActive,
                Is_deleted = dto.Is_deleted,
                Created_by = dto.Created_by,
                Updated_by = dto.Updated_by,
                Created_at = dto.Created_at,
                Updated_at = dto.Updated_at
            };
        }

        public SAccountDTO FromModeltoDTO(SAccount model)
        {
            return new SAccountDTO
            {
                AccountId = model.AccountId,
                AccountTypeId = model.AccountTypeId,
                OwnerType = model.OwnerType,
                OwnerId = model.OwnerId,
                Code = model.Code,
                Name = model.Name,
                Balance = model.Balance,
                Currency = model.Currency,
                IsActive = model.IsActive,
                Is_deleted = model.Is_deleted,
                Created_by = model.Created_by,
                Updated_by = model.Updated_by,
                Created_at = model.Created_at,
                Updated_at = model.Updated_at
            };
        }
        #endregion

        #region GetAll
        public async Task<DynamicResponse<List<SAccountDTO>>> GetAllAsync()
        {
            var response = new DynamicResponse<List<SAccountDTO>>();
            try
            {
                var list = await _dbContext.SAccounts
                    .AsNoTracking()
                    .Where(e => !e.Is_deleted)
                    .ToListAsync();

                response.Data = list.Select(FromModeltoDTO).ToList();
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

        #region Get
        public async Task<DynamicResponse<SAccountDTO>> GetAsync(long id)
        {
            var response = new DynamicResponse<SAccountDTO>();
            try
            {
                var model = await _dbContext.SAccounts
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.AccountId == id && !e.Is_deleted);

                if (model == null)
                {
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                response.Data = FromModeltoDTO(model);
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
        public async Task<DynamicResponse<bool>> AddAsync(SAccountDTO dto)
        {
            var response = new DynamicResponse<bool>();
            try
            {
                if (dto == null)
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.BadRequest;
                    return response;
                }

                var model = FromDTOtoModel(dto);
                model.Is_deleted = false;
                model.Created_at = DateTime.UtcNow;
                await _repository.InsertAsync(model);

                response.Data = true;
                response.HttpStatusCode = HttpStatusCode.OK;
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
        #endregion

        #region Update
        // NOTE: Balance is not updated here — that is the transaction engine's responsibility.
        public async Task<DynamicResponse<bool>> UpdateAsync(SAccountDTO dto)
        {
            var response = new DynamicResponse<bool>();
            try
            {
                var model = await _dbContext.SAccounts
                    .FirstOrDefaultAsync(e => e.AccountId == dto.AccountId && !e.Is_deleted);

                if (model == null)
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                model.AccountTypeId = dto.AccountTypeId;
                model.OwnerType = dto.OwnerType;
                model.OwnerId = dto.OwnerId;
                model.Code = dto.Code;
                model.Name = dto.Name;
                model.Currency = dto.Currency;
                model.IsActive = dto.IsActive;
                model.Updated_by = dto.Updated_by;
                model.Updated_at = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();

                response.Data = true;
                response.HttpStatusCode = HttpStatusCode.OK;
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
        #endregion

        #region Delete (soft)
        public async Task<DynamicResponse<bool>> DeleteAsync(long id)
        {
            var response = new DynamicResponse<bool>();
            try
            {
                var model = await _dbContext.SAccounts
                    .FirstOrDefaultAsync(e => e.AccountId == id && !e.Is_deleted);

                if (model == null)
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                model.Is_deleted = true;
                model.Updated_at = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync();

                response.Data = true;
                response.HttpStatusCode = HttpStatusCode.OK;
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
        #endregion
    }
}
