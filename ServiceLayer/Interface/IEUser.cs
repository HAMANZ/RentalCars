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
        Task<DynamicResponse<EUserDTO>> UpdateAsync(EUserRegisterDTO toUpdate);
        //DynamicResponse<List<IdentityRole>> GetAllRoles();
        Task<DynamicResponse<List<IdentityRole>>> GetAllRoles();
    }
}
