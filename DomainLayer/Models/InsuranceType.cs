using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.DomainLayer.Models
{

    public class InsuranceType : BaseEntity
    {

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Name_ar { get; set; }
    }
}