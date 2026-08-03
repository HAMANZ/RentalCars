using RentalCar.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  RentalCar.DomainLayer.DTO
{
    public class AppLabelDTO : BaseDTO
    {


        public long Id { get; set; }
        public string LabelName { get; set; }
        public string FriendlyName { get; set; }
        public string Value { get; set; }
        public string Desc { get; set; }
        public int LanguagId { get; set; }
    }
}
