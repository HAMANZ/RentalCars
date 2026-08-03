using RentalCar.DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace RentalCar.DomainLayer.DTO
{
    public class LoggerErrorDTO : BaseEntity
    {

        [Key]
        public long Id { get; set; }
        public string MethodName { get; set; }
        public string ActionType { get; set; }
        public string Parameters { get; set; }
        public string Result { get; set; }
    }
}