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
    public class InspectionServices : IInspection
    {
        private readonly IRepository<Inspection> _repository;
        private readonly RentalCarDbContext _dbContext;

        public InspectionServices(IRepository<Inspection> rep, RentalCarDbContext dbContext)
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

        private void ApplyForeignKeys(Inspection model, InspectionDTO dto)
        {
            var entry = _dbContext.Entry(model);
            SetFk(entry, "CarId", dto.CarId);
        }

        #region DTOtoModel / ModeltoDTO
        public Inspection FromDTOtoModel(InspectionDTO dto)
        {
            return new Inspection
            {
                Id = dto.Id,
                Date = dto.Date,
                ExpiryDate = dto.ExpiryDate,
                Is_deleted = dto.Is_deleted,
                Created_by = dto.Created_by,
                Updated_by = dto.Updated_by,
                Created_at = dto.Created_at,
                Updated_at = dto.Updated_at
            };
        }

        public InspectionDTO FromModeltoDTO(Inspection model)
        {
            return new InspectionDTO
            {
                Id = model.Id,
                Date = model.Date,
                ExpiryDate = model.ExpiryDate,
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
        public async Task<DynamicResponse<List<InspectionDTO>>> GetAllAsync()
        {
            var response = new DynamicResponse<List<InspectionDTO>>();
            try
            {
                var list = await _dbContext.Inspections
                    .AsNoTracking()
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
        public async Task<DynamicResponse<InspectionDTO>> GetAsync(long id)
        {
            var response = new DynamicResponse<InspectionDTO>();
            try
            {
                var model = await _dbContext.Inspections
                    .AsNoTracking()
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
        public async Task<DynamicResponse<bool>> AddAsync(InspectionDTO dto)
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

                await _dbContext.Inspections.AddAsync(model);
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
        public async Task<DynamicResponse<bool>> UpdateAsync(InspectionDTO dto)
        {
            var response = new DynamicResponse<bool>();
            try
            {
                var model = await _dbContext.Inspections
                    .AsTracking()
                    .FirstOrDefaultAsync(e => e.Id == dto.Id && !e.Is_deleted);

                if (model == null)
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                model.Date = dto.Date;
                model.ExpiryDate = dto.ExpiryDate;
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
                var model = await _dbContext.Inspections
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
