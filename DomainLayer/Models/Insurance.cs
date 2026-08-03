using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.DomainLayer.Models
{

    public class Insurance : BaseEntity
    {

        [Key]
        public long Id { get; set; }

        [ForeignKey("CarId")]

        public Car Car { get; set; }


        public string Company { get; set; }




        [ForeignKey("InsuranceCompanyId")]


        public InsuranceCompany InsuranceCompany { get; set; }
        public DateTime StartDate { get; set; }


        public DateTime EndDate { get; set; }
    }
}