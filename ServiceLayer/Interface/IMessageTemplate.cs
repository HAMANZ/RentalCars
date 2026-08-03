
using RentalCar.DomainLayer.DTO;
using System.Collections.Generic;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IMessageTemplate
    {
    
        MessageTemplateDTO Get(long Id);
        List<MessageTemplateDTO> GetAll();
        string Add(MessageTemplateDTO msg);
        string Edit(MessageTemplateDTO msg);
        string Delete(long Id);
    }
}
