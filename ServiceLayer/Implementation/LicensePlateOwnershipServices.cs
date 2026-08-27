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
    public class LicensePlateOwnershipServices : ILicensePlateOwnership
    {
        private readonly IRepository<LicensePlateOwnership> _repository;
        private readonly RentalCarDbContext _dbContext;

        public LicensePlateOwnershipServices(IRepository<LicensePlateOwnership> rep, RentalCarDbContext dbContext)
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

        private void ApplyForeignKeys(LicensePlateOwnership model, LicensePlateOwnershipDTO dto)
        {
            var entry = _dbContext.Entry(model);
            SetFk(entry, "LicensePlateId", dto.LicensePlateId);
            // PlateOwnerId is now a real long FK, set directly via model.PlateOwnerId.
        }

        #region DTOtoModel / ModeltoDTO
        public LicensePlateOwnership FromDTOtoModel(LicensePlateOwnershipDTO dto)
        {
            return new LicensePlateOwnership
            {
                Id = dto.Id,
                PlateOwnerId = dto.PlateOwnerId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsCurrent = dto.IsCurrent,
                Notes = dto.Notes,
                Is_deleted = dto.Is_deleted,
                Created_by = dto.Created_by,
                Updated_by = dto.Updated_by,
                Created_at = dto.Created_at,
                Updated_at = dto.Updated_at
            };
        }

        public LicensePlateOwnershipDTO FromModeltoDTO(LicensePlateOwnership model)
        {
            return new LicensePlateOwnershipDTO
            {
                Id = model.Id,
                PlateOwnerId = model.PlateOwnerId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                IsCurrent = model.IsCurrent,
                Notes = model.Notes,
                LicensePlateId = model.LicensePlate?.Id,
                Is_deleted = model.Is_deleted,
                Created_by = model.Created_by,
                Updated_by = model.Updated_by,
                Created_at = model.Created_at,
                Updated_at = model.Updated_at
            };
        }
        #endregion

        #region GetAll
        public async Task<DynamicResponse<List<LicensePlateOwnershipDTO>>> GetAllAsync()
        {
            var response = new DynamicResponse<List<LicensePlateOwnershipDTO>>();
            try
            {
                var list = await _dbContext.LicensePlateOwnerships
                    .AsNoTracking()
                    .Include(c => c.LicensePlate)
                    .Include(c => c.PlateOwner)
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
        public async Task<DynamicResponse<LicensePlateOwnershipDTO>> GetAsync(long id)
        {
            var response = new DynamicResponse<LicensePlateOwnershipDTO>();
            try
            {
                var model = await _dbContext.LicensePlateOwnerships
                    .AsNoTracking()
                    .Include(c => c.LicensePlate)
                    .Include(c => c.PlateOwner)
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
        public async Task<DynamicResponse<bool>> AddAsync(LicensePlateOwnershipDTO dto)
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

                await _dbContext.LicensePlateOwnerships.AddAsync(model);
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
        public async Task<DynamicResponse<bool>> UpdateAsync(LicensePlateOwnershipDTO dto)
        {
            var response = new DynamicResponse<bool>();
            try
            {
                // AsTracking because the context's global default is NoTracking.
                var model = await _dbContext.LicensePlateOwnerships
                    .AsTracking()
                    .FirstOrDefaultAsync(e => e.Id == dto.Id && !e.Is_deleted);

                if (model == null)
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                model.PlateOwnerId = dto.PlateOwnerId;
                model.StartDate = dto.StartDate;
                model.EndDate = dto.EndDate;
                model.IsCurrent = dto.IsCurrent;
                model.Notes = dto.Notes;
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
                var model = await _dbContext.LicensePlateOwnerships
                    .AsTracking()
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
