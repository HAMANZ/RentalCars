using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using RentalCar.DomainLayer.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IUser
    {

         Task<DynamicResponse<string>> Add(RegisterDTO toAdd);
         Task<AuthResult> GenerateJwtToken(EUser EUser);

        Task<DynamicResponse<EUserDTO>> GetAdminInfo();
    }
}
