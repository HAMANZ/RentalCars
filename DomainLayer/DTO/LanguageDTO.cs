using RentalCar.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  RentalCar.DomainLayer.DTO
{
    public partial class LanguageDTO : BaseDTO
    {

        public int Id { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public string Name_ex { get; set; }
        public string Flag { get; set; }
        public bool Is_ltr { get; set; }
    }
}
