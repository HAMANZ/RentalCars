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
                FullName = dto.FullName,
                FullName_ar = dto.FullName_ar,
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
                FullName_ar = dto.FullName_ar,
                FullName = dto.FullName,
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
                    FullName = toAdd.FullName,
                    FullName_ar = toAdd.FullName_ar,
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

        #region Update User
        public async Task<DynamicResponse<EUserDTO>> UpdateAsync(EUserRegisterDTO toUpdate)
        {
            var response = new DynamicResponse<EUserDTO>();

            try
            {
                if (toUpdate == null || string.IsNullOrEmpty(toUpdate.Id))
                {
                    response.HttpStatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Invalid data";
                    return response;
                }

                var euser = await _EUserManager.FindByIdAsync(toUpdate.Id);
                if (euser == null)
                {
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    response.Message = "User not found";
                    return response;
                }

                euser.FullName = toUpdate.FullName;
                euser.FullName_ar = toUpdate.FullName_ar;
                euser.Email = toUpdate.Email;
                euser.UserName = toUpdate.Email;

                if (!string.IsNullOrEmpty(toUpdate.PhoneNumber))
                    euser.PhoneNumber = toUpdate.PhoneNumber;

                if (!string.IsNullOrEmpty(toUpdate.ProfileImage))
                    euser.Profile = toUpdate.ProfileImage;

                euser.Updated_at = DateTime.UtcNow;
                euser.Updated_by = toUpdate.Created_by;

                var updateResult = await _EUserManager.UpdateAsync(euser);
                if (!updateResult.Succeeded)
                {
                    response.HttpStatusCode = HttpStatusCode.InternalServerError;
                    response.Errors = updateResult.Errors;
                    return response;
                }

                // Update role if provided
                if (!string.IsNullOrEmpty(toUpdate.Role))
                {
                    if (!await _roleManager.RoleExistsAsync(toUpdate.Role))
                        await _roleManager.CreateAsync(new IdentityRole(toUpdate.Role));

                    var currentRoles = await _EUserManager.GetRolesAsync(euser);
                    var rolesToRemove = currentRoles
                        .Where(r => r != "EUser" && r != toUpdate.Role)
                        .ToList();

                    if (rolesToRemove.Any())
                        await _EUserManager.RemoveFromRolesAsync(euser, rolesToRemove);

                    if (!currentRoles.Contains(toUpdate.Role))
                        await _EUserManager.AddToRoleAsync(euser, toUpdate.Role);
                }

                response.Data = await FromModeltoDTOfullWithRoleData(euser);
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
                var roles = await (
                    from ur in _dbContext.UserRoles
                    join r in _dbContext.Roles on ur.RoleId equals r.Id
                    where ur.UserId == userId
                    select r.Name
                ).ToListAsync();

                // Prefer the user's real role over the default "EUser" role so the
                // displayed/selected role is consistent for every user.
                response.Data = roles.FirstOrDefault(r => r != "EUser") ?? roles.FirstOrDefault();
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
