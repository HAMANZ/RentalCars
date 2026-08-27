using RentalCar.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  RentalCar.DomainLayer.DTO
{
    public  class DefinedDTO : BaseDTO
    {
       
     
        public long Id { get; set; }
        public string Name { get; set; }
        public string Name_ar { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
