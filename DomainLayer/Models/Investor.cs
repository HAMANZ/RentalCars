using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.DomainLayer.Models
{
    public class Investor : EUser
    {
     
        public string Phone { get; set; }
        public string DrivingLicense { get; set; }
        public string Address { get; set; }
        public DateTime LicenseExpiryDate { get; set; }


        [ForeignKey("StatusId ")]
        public Status Status { get; set; }
        [ForeignKey("NationalId ")]
        public Nationality Nationality { get; set; }

        public ICollection<RentalContract> Contracts { get; set; }

    }
}
