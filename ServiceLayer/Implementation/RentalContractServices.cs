using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    public class RentalContractServices : IRentalContract
    {
        private readonly IRepository<RentalContract> _repository;
        private readonly RentalCarDbContext _dbContext;

        public RentalContractServices(IRepository<RentalContract> rep, RentalCarDbContext dbContext)
        {
            _repository = rep;
            _dbContext = dbContext;
        }

        // Sets a (possibly shadow) FK property; tolerant of trailing-space FK names in the model.
        private static void SetFk(EntityEntry entry, string name, object value)
        {
            if (value == null) return;
            var prop = entry.Metadata.FindProperty(name) ?? entry.Metadata.FindProperty(name.Trim());
            if (prop != null)
                entry.Property(prop.Name).CurrentValue = value;
        }

        private void ApplyForeignKeys(RentalContract model, RentalContractDTO dto)
        {
            var entry = _dbContext.Entry(model);
            SetFk(entry, "StatusId", dto.StatusId);
            SetFk(entry, "CustomerId", dto.CustomerId);
            SetFk(entry, "CarId", dto.CarId);
        }

        #region DTOtoModel / ModeltoDTO
        public RentalContract FromDTOtoModel(RentalContractDTO dto)
        {
            return new RentalContract
            {
                Id = dto.Id,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                ActualReturnDate = dto.ActualReturnDate,
                OdometerStart = dto.OdometerStart,
                OdometerEnd = dto.OdometerEnd,
                DailyRate = dto.DailyRate,
                Discount = dto.Discount,
                TotalAmount = dto.TotalAmount,
                PaidAmount = dto.PaidAmount,
                Is_deleted = dto.Is_deleted,
                Created_by = dto.Created_by,
                Updated_by = dto.Updated_by,
                Created_at = dto.Created_at,
                Updated_at = dto.Updated_at
            };
        }

        public RentalContractDTO FromModeltoDTO(RentalContract model)
        {
            return new RentalContractDTO
            {
                Id = model.Id,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                ActualReturnDate = model.ActualReturnDate,
                OdometerStart = model.OdometerStart,
                OdometerEnd = model.OdometerEnd,
                DailyRate = model.DailyRate,
                Discount = model.Discount,
                TotalAmount = model.TotalAmount,
                PaidAmount = model.PaidAmount,
                StatusId = model.Status?.Id,
                CustomerId = model.Customer?.Id,
                CarId = model.Car?.Id,
                Is_deleted = model.Is_deleted,
                Created_by = model.Created_by,
                Updated_by = model.Updated_by,
                Created_at = model.Created_at,
                Updated_at = model.Updated_at
            };
        }
        #endregion

        #region GetAll
        public async Task<DynamicResponse<List<RentalContractDTO>>> GetAllAsync()
        {
            var response = new DynamicResponse<List<RentalContractDTO>>();
            try
            {
                var list = await _dbContext.RentalContracts
                    .AsNoTracking()
                    .Include(c => c.Status)
                    .Include(c => c.Customer)
                    .Include(c => c.Car)
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
        public async Task<DynamicResponse<RentalContractDTO>> GetAsync(long id)
        {
            var response = new DynamicResponse<RentalContractDTO>();
            try
            {
                var model = await _dbContext.RentalContracts
                    .AsNoTracking()
                    .Include(c => c.Status)
                    .Include(c => c.Customer)
                    .Include(c => c.Car)
                    .FirstOrDefaultAsync(e => e.Id == id && !e.Is_deleted);

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
        public async Task<DynamicResponse<bool>> AddAsync(RentalContractDTO dto)
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

                await _dbContext.RentalContracts.AddAsync(model);
                ApplyForeignKeys(model, dto);
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

        #region Update
        public async Task<DynamicResponse<bool>> UpdateAsync(RentalContractDTO dto)
        {
            var response = new DynamicResponse<bool>();
            try
            {
                var model = await _dbContext.RentalContracts
                    .FirstOrDefaultAsync(e => e.Id == dto.Id && !e.Is_deleted);

                if (model == null)
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                model.StartDate = dto.StartDate;
                model.EndDate = dto.EndDate;
                model.ActualReturnDate = dto.ActualReturnDate;
                model.OdometerStart = dto.OdometerStart;
                model.OdometerEnd = dto.OdometerEnd;
                model.DailyRate = dto.DailyRate;
                model.Discount = dto.Discount;
                model.TotalAmount = dto.TotalAmount;
                model.PaidAmount = dto.PaidAmount;
                model.Updated_by = dto.Updated_by;
                model.Updated_at = DateTime.UtcNow;

                ApplyForeignKeys(model, dto);
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
                var model = await _dbContext.RentalContracts
                    .FirstOrDefaultAsync(e => e.Id == id && !e.Is_deleted);

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
