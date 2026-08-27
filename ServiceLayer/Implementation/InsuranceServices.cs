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
    public class InsuranceServices : IInsurance
    {
        private readonly IRepository<Insurance> _repository;
        private readonly RentalCarDbContext _dbContext;

        public InsuranceServices(IRepository<Insurance> rep, RentalCarDbContext dbContext)
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

        private void ApplyForeignKeys(Insurance model, InsuranceDTO dto)
        {
            var entry = _dbContext.Entry(model);
            SetFk(entry, "InsuranceCompanyId", dto.InsuranceCompanyId);
            SetFk(entry, "CarId", dto.CarId);
            SetFk(entry, "InsuranceTypeId", dto.InsuranceTypeId);
            SetFk(entry, "StatusId", dto.StatusId);
        }

        #region DTOtoModel / ModeltoDTO
        public Insurance FromDTOtoModel(InsuranceDTO dto)
        {
            return new Insurance
            {
                Id = dto.Id,
                PolicyNumber = dto.PolicyNumber,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Premium = dto.Premium,
                CoverageAmount = dto.CoverageAmount,
                Deductible = dto.Deductible,
                CoverageDetails = dto.CoverageDetails,
                Notes = dto.Notes,
                RenewalReminderSent = dto.RenewalReminderSent,
                Is_deleted = dto.Is_deleted,
                Created_by = dto.Created_by,
                Updated_by = dto.Updated_by,
                Created_at = dto.Created_at,
                Updated_at = dto.Updated_at
            };
        }

        public InsuranceDTO FromModeltoDTO(Insurance model)
        {
            return new InsuranceDTO
            {
                Id = model.Id,
                PolicyNumber = model.PolicyNumber,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Premium = model.Premium,
                CoverageAmount = model.CoverageAmount,
                Deductible = model.Deductible,
                CoverageDetails = model.CoverageDetails,
                Notes = model.Notes,
                RenewalReminderSent = model.RenewalReminderSent,
                InsuranceCompanyId = model.InsuranceCompany?.Id,
                CarId = model.Car?.Id,
                InsuranceTypeId = model.InsuranceType?.Id,
                StatusId = model.Status?.Id,
                Is_deleted = model.Is_deleted,
                Created_by = model.Created_by,
                Updated_by = model.Updated_by,
                Created_at = model.Created_at,
                Updated_at = model.Updated_at
            };
        }
        #endregion

        #region GetAll
        public async Task<DynamicResponse<List<InsuranceDTO>>> GetAllAsync()
        {
            var response = new DynamicResponse<List<InsuranceDTO>>();
            try
            {
                var list = await _dbContext.Insurances
                    .AsNoTracking()
                    .Include(c => c.InsuranceCompany)
                    .Include(c => c.Car)
                    .Include(c => c.InsuranceType)
                    .Include(c => c.Status)
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
        public async Task<DynamicResponse<InsuranceDTO>> GetAsync(long id)
        {
            var response = new DynamicResponse<InsuranceDTO>();
            try
            {
                var model = await _dbContext.Insurances
                    .AsNoTracking()
                    .Include(c => c.InsuranceCompany)
                    .Include(c => c.Car)
                    .Include(c => c.InsuranceType)
                    .Include(c => c.Status)
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
        public async Task<DynamicResponse<bool>> AddAsync(InsuranceDTO dto)
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

                await _dbContext.Insurances.AddAsync(model);
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
        public async Task<DynamicResponse<bool>> UpdateAsync(InsuranceDTO dto)
        {
            var response = new DynamicResponse<bool>();
            try
            {
                var model = await _dbContext.Insurances
                    .AsTracking()
                    .FirstOrDefaultAsync(e => e.Id == dto.Id && !e.Is_deleted);

                if (model == null)
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                model.PolicyNumber = dto.PolicyNumber;
                model.StartDate = dto.StartDate;
                model.EndDate = dto.EndDate;
                model.Premium = dto.Premium;
                model.CoverageAmount = dto.CoverageAmount;
                model.Deductible = dto.Deductible;
                model.CoverageDetails = dto.CoverageDetails;
                model.Notes = dto.Notes;
                model.RenewalReminderSent = dto.RenewalReminderSent;
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
                var model = await _dbContext.Insurances
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
