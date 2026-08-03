using DomainLayer.CommonObjects;
using DomainLayer.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RentalCar.DomainLayer.Models;
using RepositoryLayer.RespositoryPattern;
using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using RentalCar;

namespace ServiceLayer.Implementation
{
    public class UsersServices : IEUser
    {
        private readonly IRepository<EUser> _repository;
        private RentalCarDbContext _dbContext;
        private readonly EUserManager _EUserManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersServices(
            IRepository<EUser> repository,
            EUserManager EUserManager,
            RoleManager<IdentityRole> roleManager,
            RentalCarDbContext context
        )
        {
            _repository = repository;
            _EUserManager = EUserManager;
            _roleManager = roleManager;
            _dbContext = context;
        }

        // Convert model to DTO
        public EUserDTO FromModeltoDTOfullData(EUser dto)
        {
            return new EUserDTO
            {
                Id = dto.Id,
                FirstName_ar = dto.FirstName_ar,
                LastName_ar = dto.LastName_ar,
                FirstName_en = dto.FirstName_en,
                LastName_en = dto.LastName_en,
                Profile = dto.Profile,
                Email = dto.Email,
                Created_at = dto.Created_at,
                Updated_at = dto.Updated_at,
                Created_by = dto.Created_by,
                Updated_by = dto.Updated_by
            };
        } 
        
        public async Task<EUserDTO> FromModeltoDTOfullWithRoleData(EUser dto)
        {
            return new EUserDTO
            {
                Id = dto.Id,
                FirstName_ar = dto.FirstName_ar,
                LastName_ar = dto.LastName_ar,
                FirstName_en = dto.FirstName_en,
                LastName_en = dto.LastName_en,
                Profile = dto.Profile,
                Email = dto.Email,
                Role = (await this.GetRoleNameAsync(dto.Id)).Data,
                Created_at = dto.Created_at,
                Updated_at = dto.Updated_at,
                Created_by = dto.Created_by,
                Updated_by = dto.Updated_by
            };
        }

        #region Get All Users
        public async Task<DynamicResponse<List<EUserDTO>>> GetAllAsync()
        {
            var response = new DynamicResponse<List<EUserDTO>>();

            try
            {
                var users = _repository.GetAllList(); // List<EUser>

                var userDtoTasks = users.Select(FromModeltoDTOfullWithRoleData);

                var userDtos = await Task.WhenAll(userDtoTasks);

                response.Data = userDtos.ToList();
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

        #region Add User
        public async Task<DynamicResponse<string>> Add(EUserRegisterDTO toAdd)
        {
            var response = new DynamicResponse<string>();

            try
            {
                if (toAdd == null)
                    return new DynamicResponse<string>
                    {
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "Invalid data"
                    };

                var euser = new EUser
                {
                    FirstName_ar = toAdd.FirstName_ar,
                    FirstName_en = toAdd.FirstName_en,
                    LastName_ar = toAdd.LastName_ar,
                    LastName_en = toAdd.LastName_en,
                    Email = toAdd.Email,
                    UserName = toAdd.Email,
                    PhoneNumber = toAdd.PhoneNumber,
                    Profile = toAdd.ProfileImage,
                    Created_at = DateTime.UtcNow,
                    Created_by = toAdd.Created_by,
                    Is_deleted = false
                };

                var isCreated = await _EUserManager.CreateAsync(euser, toAdd.Password);
                if (!isCreated.Succeeded)
                {
                    response.HttpStatusCode = HttpStatusCode.InternalServerError;
                    response.Errors = isCreated.Errors;
                    return response;
                }

                // Assign default role
                const string defaultRole = "EUser";
                if (!await _roleManager.RoleExistsAsync(defaultRole))
                    await _roleManager.CreateAsync(new IdentityRole(defaultRole));

                await _EUserManager.AddToRoleAsync(euser, defaultRole);

                // Assign additional role if provided
                if (!string.IsNullOrEmpty(toAdd.Role))
                {
                    if (!await _roleManager.RoleExistsAsync(toAdd.Role))
                        await _roleManager.CreateAsync(new IdentityRole(toAdd.Role));

                    await _EUserManager.AddToRoleAsync(euser, toAdd.Role);
                }

                response.HttpStatusCode = HttpStatusCode.OK;
                response.Data = "User added successfully!";
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

        #region Get All Roles
        public async Task<DynamicResponse<List<IdentityRole>>> GetAllRoles()
        {
            var response = new DynamicResponse<List<IdentityRole>>();
            try
            {
                var roles = _roleManager.Roles.ToList();
                response.Data = roles;
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

        #region get role Name
        public async Task<DynamicResponse<string>> GetRoleNameAsync(string userId)
        {
            var response = new DynamicResponse<string>();

            try
            {
                var roleName = await (
                    from ur in _dbContext.UserRoles
                    join r in _dbContext.Roles on ur.RoleId equals r.Id
                    where ur.UserId == userId
                    select r.Name
                ).FirstOrDefaultAsync();

                response.Data = roleName;
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

    }
}
