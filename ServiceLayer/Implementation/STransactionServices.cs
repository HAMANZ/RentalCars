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
    public class STransactionServices : ISTransaction
    {
        private readonly IRepository<STransaction> _repository;
        private readonly RentalCarDbContext _dbContext;

        public STransactionServices(IRepository<STransaction> rep, RentalCarDbContext dbContext)
        {
            _repository = rep;
            _dbContext = dbContext;
        }

        #region DTOtoModel / ModeltoDTO
        public STransaction FromDTOtoModel(STransactionDTO dto)
        {
            return new STransaction
            {
                TransactionId = dto.TransactionId,
                TransactionTypeId = dto.TransactionTypeId,
                Description = dto.Description,
                BranchIdId = dto.BranchIdId,
                DebitAccountId = dto.DebitAccountId,
                CreditAccountId = dto.CreditAccountId,
                Amount = dto.Amount,
                ReferenceId = dto.ReferenceId,
                OccurredAt = dto.OccurredAt,
                Notes = dto.Notes,
                Is_deleted = dto.Is_deleted,
                Created_by = dto.Created_by,
                Updated_by = dto.Updated_by,
                Created_at = dto.Created_at,
                Updated_at = dto.Updated_at
            };
        }

        public STransactionDTO FromModeltoDTO(STransaction model)
        {
            return new STransactionDTO
            {
                TransactionId = model.TransactionId,
                TransactionTypeId = model.TransactionTypeId,
                Description = model.Description,
                BranchIdId = model.BranchIdId,
                DebitAccountId = model.DebitAccountId,
                CreditAccountId = model.CreditAccountId,
                Amount = model.Amount,
                ReferenceId = model.ReferenceId,
                OccurredAt = model.OccurredAt,
                Notes = model.Notes,
                Is_deleted = model.Is_deleted,
                Created_by = model.Created_by,
                Updated_by = model.Updated_by,
                Created_at = model.Created_at,
                Updated_at = model.Updated_at
            };
        }
        #endregion

        #region GetAll
        public async Task<DynamicResponse<List<STransactionDTO>>> GetAllAsync()
        {
            var response = new DynamicResponse<List<STransactionDTO>>();
            try
            {
                var list = await _dbContext.STransactions
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
        public async Task<DynamicResponse<STransactionDTO>> GetAsync(long id)
        {
            var response = new DynamicResponse<STransactionDTO>();
            try
            {
                var model = await _dbContext.STransactions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.TransactionId == id && !e.Is_deleted);

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
        public async Task<DynamicResponse<bool>> AddAsync(STransactionDTO dto)
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
        public async Task<DynamicResponse<bool>> UpdateAsync(STransactionDTO dto)
        {
            var response = new DynamicResponse<bool>();
            try
            {
                var model = await _dbContext.STransactions
                    .FirstOrDefaultAsync(e => e.TransactionId == dto.TransactionId && !e.Is_deleted);

                if (model == null)
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                model.TransactionTypeId = dto.TransactionTypeId;
                model.Description = dto.Description;
                model.BranchIdId = dto.BranchIdId;
                model.DebitAccountId = dto.DebitAccountId;
                model.CreditAccountId = dto.CreditAccountId;
                model.Amount = dto.Amount;
                model.ReferenceId = dto.ReferenceId;
                model.OccurredAt = dto.OccurredAt;
                model.Notes = dto.Notes;
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
                var model = await _dbContext.STransactions
                    .FirstOrDefaultAsync(e => e.TransactionId == id && !e.Is_deleted);

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
