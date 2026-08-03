using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.DomainLayer.DTO
{
    public class BaseDTO
    {
        public bool Is_deleted { get; set; }
        public long Created_by { get; set; }
        public long Updated_by { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
