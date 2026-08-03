using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.DomainLayer.Models
{

    public class CarOwner : BaseEntity
    {

        [Key]
        public long Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string FullName { get; set; }

        [MaxLength(100)]
        public string CompanyName { get; set; }

        [MaxLength(50)]
        public string PassportNo { get; set; }

        [MaxLength(50)]
        public string CommercialRegister { get; set; }

        [MaxLength(30)]
        public string Phone1 { get; set; }

        [MaxLength(30)]
        public string Phone2 { get; set; }

        [MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(300)]
        public string Address { get; set; }
   
        public bool IsCompany { get; set; }

        public bool IsActive { get; set; } = true;

        public string Notes { get; set; }

        [ForeignKey("NationalId ")]

        public Nationality Nationality { get; set; }

        [ForeignKey("CountryId ")]

        public Country Country { get; set; }

        [ForeignKey("CityId ")]

        public City City { get; set; }
        public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}