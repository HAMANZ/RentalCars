using DomainLayer.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;

namespace ServiceLayer.Interface
{
    public interface IEUser
    {
        //DynamicResponse<List<EUserDTO>> GetAll();
        Task<DynamicResponse<List<EUserDTO>>> GetAllAsync();

        Task<DynamicResponse<string>> Add(EUserRegisterDTO toAdd);
        //DynamicResponse<List<IdentityRole>> GetAllRoles();
        Task<DynamicResponse<List<IdentityRole>>> GetAllRoles();
    }
}
