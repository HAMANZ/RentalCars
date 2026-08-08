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
    public class CarOwnerServices : ICarOwner
    {
        private readonly IRepository<CarOwner> _repository;
        private readonly RentalCarDbContext _dbContext;

        public CarOwnerServices(IRepository<CarOwner> rep, RentalCarDbContext dbContext)
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

        private void ApplyForeignKeys(CarOwner model, CarOwnerDTO dto)
        {
            var entry = _dbContext.Entry(model);
            SetFk(entry, "UserId", dto.UserId);
            SetFk(entry, "NationalId", dto.NationalityId);
            SetFk(entry, "CountryId", dto.CountryId);
            SetFk(entry, "CityId", dto.CityId);
        }

        #region DTOtoModel / ModeltoDTO
        public CarOwner FromDTOtoModel(CarOwnerDTO dto)
        {
            return new CarOwner
            {
                Id = dto.Id,
                FullName = dto.FullName,
                CompanyName = dto.CompanyName,
                PassportNo = dto.PassportNo,
                CommercialRegister = dto.CommercialRegister,
                Phone1 = dto.Phone1,
                Phone2 = dto.Phone2,
                Email = dto.Email,
                Address = dto.Address,
                IsCompany = dto.IsCompany,
                IsActive = dto.IsActive,
                Notes = dto.Notes,
                Is_deleted = dto.Is_deleted,
                Created_by = dto.Created_by,
                Updated_by = dto.Updated_by,
                Created_at = dto.Created_at,
                Updated_at = dto.Updated_at
            };
        }

        public CarOwnerDTO FromModeltoDTO(CarOwner model)
        {
            return new CarOwnerDTO
            {
                Id = model.Id,
                FullName = model.FullName,
                CompanyName = model.CompanyName,
                PassportNo = model.PassportNo,
                CommercialRegister = model.CommercialRegister,
                Phone1 = model.Phone1,
                Phone2 = model.Phone2,
                Email = model.Email,
                Address = model.Address,
                IsCompany = model.IsCompany,
                IsActive = model.IsActive,
                Notes = model.Notes,
                UserId = model.User?.Id,
                NationalityId = model.Nationality?.Id,
                CountryId = model.Country?.Id,
                CityId = model.City?.Id,
                Is_deleted = model.Is_deleted,
                Created_by = model.Created_by,
                Updated_by = model.Updated_by,
                Created_at = model.Created_at,
                Updated_at = model.Updated_at
            };
        }
        #endregion

        #region GetAll
        public async Task<DynamicResponse<List<CarOwnerDTO>>> GetAllAsync()
        {
            var response = new DynamicResponse<List<CarOwnerDTO>>();
            try
            {
                var list = await _dbContext.CarOwners
                    .AsNoTracking()
                    .Include(c => c.User)
                    .Include(c => c.Nationality)
                    .Include(c => c.Country)
                    .Include(c => c.City)
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
        public async Task<DynamicResponse<CarOwnerDTO>> GetAsync(long id)
        {
            var response = new DynamicResponse<CarOwnerDTO>();
            try
            {
                var model = await _dbContext.CarOwners
                    .AsNoTracking()
                    .Include(c => c.User)
                    .Include(c => c.Nationality)
                    .Include(c => c.Country)
                    .Include(c => c.City)
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
        public async Task<DynamicResponse<bool>> AddAsync(CarOwnerDTO dto)
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

                await _dbContext.CarOwners.AddAsync(model);
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
        public async Task<DynamicResponse<bool>> UpdateAsync(CarOwnerDTO dto)
        {
            var response = new DynamicResponse<bool>();
            try
            {
                var model = await _dbContext.CarOwners
                    .FirstOrDefaultAsync(e => e.Id == dto.Id && !e.Is_deleted);

                if (model == null)
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                model.FullName = dto.FullName;
                model.CompanyName = dto.CompanyName;
                model.PassportNo = dto.PassportNo;
                model.CommercialRegister = dto.CommercialRegister;
                model.Phone1 = dto.Phone1;
                model.Phone2 = dto.Phone2;
                model.Email = dto.Email;
                model.Address = dto.Address;
                model.IsCompany = dto.IsCompany;
                model.IsActive = dto.IsActive;
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
                var model = await _dbContext.CarOwners
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
