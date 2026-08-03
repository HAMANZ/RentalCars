using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.DomainLayer.Models
{

    public class PlateOwner : BaseEntity
    {

        [Key]
        public long Id { get; set; }

        public string FullName { get; set; }

        public string NationalId { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public ICollection<LicensePlateOwnership> PlateOwnerships { get; set; }
      }
 }