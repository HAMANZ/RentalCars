
using RepositoryLayer.RespositoryPattern;
using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using RentalCar.DomainLayer.Models;
using RentalCar.ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace RentalCar.ServiceLayer.Implementation
{
    public class UserServices : IUser
    {
        private static readonly string sercretkey = "llvudfvkwvepwkdnsnwmuulyvtrawppf";
        private readonly IRepository<EUser> _repository;
        private RentalCarDbContext _dbContext;
        private readonly UserManager<EUser> _EUserManager;

        private readonly IConfiguration _configuration;
        private readonly TokenValidationParameters _tokenValidationParams;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserServices(IRepository<EUser> repository, RentalCarDbContext dbContext, IConfiguration IConfiguration, UserManager<EUser> EUserManager, TokenValidationParameters tokenValidationParams, RoleManager<IdentityRole> roleManager)
        {
            this._repository = repository;
            this._dbContext = dbContext;
            this._configuration = IConfiguration;
            this._EUserManager = EUserManager;
            this._tokenValidationParams = tokenValidationParams;
            this._roleManager = roleManager;
        }


        #region DTOtoModel/ModeltoDTO  
        public EUser FromDTOtoModel(EUserDTO dto)
        {
            EUser Model = new EUser();
            Model.Id = dto.Id;
            Model.FirstName_ar = dto.FirstName_ar;
            Model.FirstName_en = dto.FirstName_en;
            Model.LastName_ar = dto.LastName_ar;
            Model.LastName_en = dto.LastName_en;
            Model.Profile = dto.Profile;
            return Model;
        }


        public EUserDTO FromModeltoDTO(EUser model)
        {
            EUserDTO DTO = new EUserDTO();
            DTO.Id = model.Id;
            DTO.FirstName_ar = model.FirstName_ar;
            DTO.FirstName_en = model.FirstName_en;
            DTO.LastName_ar = model.LastName_ar;
            DTO.LastName_en = model.LastName_en;
            DTO.Profile = model.Profile;
            return DTO;
        }

        #endregion


        #region Get Admin
        public async Task<DynamicResponse<EUserDTO>> GetAdminInfo()
        {
            DynamicResponse<EUserDTO> response = new DynamicResponse<EUserDTO>();
            try
            {

                var user = await _EUserManager.FindByNameAsync("admin");

                if (user != null)
                {
                    EUserDTO data = new EUserDTO();
                    data.Id = user.Id;
                    data.Email = user.Email;
                    data.FirstName_ar = user.FirstName_ar;
                    data.FirstName_en = user.FirstName_en;
                    data.LastName_ar = user.LastName_ar;
                    data.LastName_en = user.LastName_en;
                    data.Profile = user.Profile;
                    data.PhoneNumber = user.PhoneNumber;
                    data.GenderId = user.GenderId;

                    response.Data = data;
                    return response;
                }


                response.Message = "df_no_data";
                response.HttpStatusCode = HttpStatusCode.NoContent;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.ServerMessage = ex.InnerException.Message;
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        #endregion
        

        #region Add User
        public async Task<DynamicResponse<string>> Add(RegisterDTO toAdd)
        {
            DynamicResponse<string> response = new DynamicResponse<string>();

            try
            {

                if (toAdd != null)
                {
                    string error = "";
                    //var existingEUsername = await _EUserManager.FindByNameAsync(toAdd.Username);

                    //if (existingEUsername != null)
                    //{
                    //    error = "df_username_exist";
                    //}

                    //check if EUser exist  by email
                    var existingEUser =  _dbContext.EUser.Where(e=>e.Email==toAdd.Email).FirstOrDefault();
                    if (existingEUser != null)
                    {
                        error = "df_email_exist";
                    }

                    if (error.Length != 0)
                    {
                        response.HttpStatusCode = HttpStatusCode.InternalServerError;
                        response.Message = error;
                        return response;
                    }

                    int count = _EUserManager.Users.Count() + 1;
                    var newEUser = new EUser()
                    {
                        EUserId = count,
                        FirstName_ar = toAdd.FirstName_ar,
                        FirstName_en = toAdd.FirstName_en,
                        LastName_ar = toAdd.LastName_ar,
                        LastName_en = toAdd.LastName_en,
                        PhoneNumber = toAdd.PhoneNumber,
                        GenderId = toAdd.GenderId,
                        Email = toAdd.Email,
                        UserName = toAdd.Email,
                        Created_at = DateTime.Now
                    };
                    var isCreated = await _EUserManager.CreateAsync(newEUser, toAdd.Password);
                    if (isCreated.Succeeded)
                    {
                        await _EUserManager.AddToRoleAsync(newEUser, "EUser");
                        if(toAdd.IsAdmin)
                            await _EUserManager.AddToRoleAsync(newEUser, "Adminstrator");
                        var jwtToken = await GenerateJwtToken(newEUser);
                        response.HttpStatusCode = HttpStatusCode.OK;
                        response.Data = jwtToken.Token;
                        return response;
                    }
                    else
                    {

                        response.HttpStatusCode = HttpStatusCode.InternalServerError;
                        return response;
                    }
                }

                response.Data = "Null Data";
                response.HttpStatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;

                return response;
            }
        }

        #endregion


        #region GenerateJwtToken
        public async Task<AuthResult> GenerateJwtToken(EUser EUser)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(sercretkey);

            var claims = await GetAllValidClaims(EUser);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                //  Expires = DateTime.UtcNow.AddSeconds(30), // 5-10 
                Expires = DateTime.Now.AddMinutes(30), // 5-10 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                IsUsed = false,
                IsRevorked = false,
                UserId = EUser.Id,
                AddedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
                Token = Helper.RandomString(35) + Guid.NewGuid()
            };

            await _dbContext.RefreshTokens.AddAsync(refreshToken);
            await _dbContext.SaveChangesAsync();

            return new AuthResult()
            {
                Token = jwtToken,
                Success = true,
                RefreshToken = refreshToken.Token
            };
        }


        #endregion


        #region GetAllValidClaims
        // Get all valid claims for the corresponding EUser
        private async Task<List<Claim>> GetAllValidClaims(EUser EUser)
        {
            var claims = new List<Claim>
                    {
                        new Claim("Id", EUser.Id),
                        new Claim(JwtRegisteredClaimNames.Email, EUser.Email),
                        new Claim(JwtRegisteredClaimNames.Sub, EUser.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

            // Getting the claims that we have assigned to the EUser
            var EUserClaims = await _EUserManager.GetClaimsAsync(EUser);
            claims.AddRange(EUserClaims);

            // Get the EUser role and add it to the claims
            var EUserRoles = await _EUserManager.GetRolesAsync(EUser);

            foreach (var EUserRole in EUserRoles)
            {
                var role = await _roleManager.FindByNameAsync(EUserRole);

                if (role != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, EUserRole));

                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (var roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }

            return claims;
        }

        #endregion

    }
}
