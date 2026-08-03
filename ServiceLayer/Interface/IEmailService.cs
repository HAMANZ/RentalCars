using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.CommonObjects.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IEmailService
    {
        DynamicResponse<bool> SendEmail(MailRequest mailRequest);

    }
}
