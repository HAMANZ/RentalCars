using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using RentalCar.DomainLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface INotification
    {

        DynamicResponse<List<NotificationsDTO>> GetAll(string UserId);
        DynamicResponse<List<NotificationsDTO>> GetAll(string UserId, int offset, int limit);
        DynamicResponse<List<NotificationsDTO>> GetAllUser(int offset, int limit);
        Task<DynamicResponse<bool>> Add(NotificationsDTO toAdd);
        DynamicResponse<bool> Seen(long Id);
    }
}
