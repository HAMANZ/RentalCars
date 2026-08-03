using RentalCar.DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  RentalCar.DomainLayer.DTO
{

    public partial class LanguageDataDTO : BaseDTO
    {
       
       
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

    }
}
