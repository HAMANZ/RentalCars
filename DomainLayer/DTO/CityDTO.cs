using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  RentalCar.DomainLayer.DTO
{
   
    public partial class CityDTO : BaseDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CountryId { get; set; }

    }
}
