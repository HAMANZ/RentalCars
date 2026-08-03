using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace RentalCar.DomainLayer.Models
{
    [Comment("Logger Error   Table  for adding the Logs for the each error occured in app")]
    public class LoggerError : BaseEntity
    {

        [Key]
        public long Id { get; set; }
        [Comment("Method Name Of Action Logged")]
        public string MethodName { get; set; }
        [Comment("Action Type: Add ,Read ,Delete ,Update")]
        public string ActionType { get; set; }
        [Comment("Parameters of the Action Logged")]
        public string Parameters { get; set; }
        [Comment("Exception Message of the Error")]
        public string Result { get; set; }
    }
}