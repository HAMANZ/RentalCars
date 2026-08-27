using Microsoft.AspNetCore.Identity;
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
    public class PlateOwnerServices : IPlateOwner
    {
        private readonly IRepository<PlateOwner> _repository;
        private readonly RentalCarDbContext _dbContext;
        private readonly UserManager<EUser> _userManager;

        public PlateOwnerServices(IRepository<PlateOwner> rep, RentalCarDbContext dbContext, UserManager<EUser> userManager)
        {
            _repository = rep;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        // A policy-compliant random password for admin-created party accounts.
        private static string GeneratePassword() => $"Aa1!{Guid.NewGuid():N}";

        // Party entities inherit EUser (Identity user) — populate the required Identity
        // fields when saving directly (i.e. not through UserManager).
        private static void EnsureIdentityFields(EUser user)
        {
            if (string.IsNullOrWhiteSpace(user.Id))
                user.Id = Guid.NewGuid().ToString();
            if (string.IsNullOrWhiteSpace(user.UserName))
                user.UserName = !string.IsNullOrWhiteSpace(user.Email) ? user.Email : $"user_{Guid.NewGuid():N}";
            user.NormalizedUserName = user.UserName.ToUpperInvariant();
            if (!string.IsNullOrWhiteSpace(user.Email))
                user.NormalizedEmail = user.Email.ToUpperInvariant();
            if (string.IsNullOrWhiteSpace(user.SecurityStamp))
                user.SecurityStamp = Guid.NewGuid().ToString();
        }

        #region DTOtoModel / ModeltoDTO
        public PlateOwner FromDTOtoModel(PlateOwnerDTO dto)
        {
            return new PlateOwner
            {
                Id = dto.Id,
                FullName = dto.FullName,
                NationalId = dto.NationalId,
                Phone = dto.Phone,
                Email = dto.Email,
                Address = dto.Address,
                Is_deleted = dto.Is_deleted,
                Created_by = dto.Created_by,
                Updated_by = dto.Updated_by,
                Created_at = dto.Created_at,
                Updated_at = dto.Updated_at
            };
        }

        public PlateOwnerDTO FromModeltoDTO(PlateOwner model)
        {
            return new PlateOwnerDTO
            {
                Id = model.Id,
                FullName = model.FullName,
                NationalId = model.NationalId,
                Phone = model.Phone,
                Email = model.Email,
                Address = model.Address,
                Is_deleted = model.Is_deleted,
                Created_by = model.Created_by,
                Updated_by = model.Updated_by,
                Created_at = model.Created_at,
                Updated_at = model.Updated_at
            };
        }
        #endregion

        #region GetAll
        public async Task<DynamicResponse<List<PlateOwnerDTO>>> GetAllAsync()
        {
            var response = new DynamicResponse<List<PlateOwnerDTO>>();
            try
            {
                var list = await _dbContext.PlateOwners
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
        public async Task<DynamicResponse<PlateOwnerDTO>> GetAsync(string id)
        {
            var response = new DynamicResponse<PlateOwnerDTO>();
            try
            {
                var model = await _dbContext.PlateOwners
                    .AsNoTracking()
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
        public async Task<DynamicResponse<bool>> AddAsync(PlateOwnerDTO dto)
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
                if (string.IsNullOrWhiteSpace(model.UserName))
                    model.UserName = !string.IsNullOrWhiteSpace(model.Email) ? model.Email : $"user_{Guid.NewGuid():N}";

                // Create as an Identity user (hashed password + normalized fields).
                var createResult = await _userManager.CreateAsync(model, GeneratePassword());
                if (!createResult.Succeeded)
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.BadRequest;
                    response.Message = string.Join("; ", createResult.Errors.Select(e => e.Description));
                    return response;
                }

                await _userManager.AddToRoleAsync(model, "PlateOwner");

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
        public async Task<DynamicResponse<bool>> UpdateAsync(PlateOwnerDTO dto)
        {
            var response = new DynamicResponse<bool>();
            try
            {
                var model = await _dbContext.PlateOwners
                    .AsTracking()
                    .FirstOrDefaultAsync(e => e.Id == dto.Id && !e.Is_deleted);

                if (model == null)
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                model.FullName = dto.FullName;
                model.NationalId = dto.NationalId;
                model.Phone = dto.Phone;
                model.Email = dto.Email;
                model.Address = dto.Address;
                model.Updated_by = dto.Updated_by;
                model.Updated_at = DateTime.UtcNow;
                EnsureIdentityFields(model);

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
        public async Task<DynamicResponse<bool>> DeleteAsync(string id)
        {
            var response = new DynamicResponse<bool>();
            try
            {
                var model = await _dbContext.PlateOwners
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
