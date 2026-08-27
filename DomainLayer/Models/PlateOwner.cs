using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.DomainLayer.Models
{

    public class PlateOwner : EUser
    {

        public string NationalId { get; set; }

        public string Phone { get; set; }


        public string Address { get; set; }

        public ICollection<LicensePlateOwnership> PlateOwnerships { get; set; }
      }
 }