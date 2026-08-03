using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentalCar.DomainLayer.DTO;

namespace DomainLayer.DTO_EXT
{
    public class ContactUsDTOExt : BaseDTO
    {

        public long Id { get; set; }
        public string FullName { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public bool Is_Seen { get; set; }
    }

}
