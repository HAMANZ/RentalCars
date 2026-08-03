using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace RentalCar.DomainLayer.Models
{

    [Comment("Logger Action  Table  for adding the Logs for the action")]
    public class LoggerAction : BaseEntity
    {

        [Key]
        public long Id { get; set; }
        [Comment("Method Name Of Action Logged")]
        public string MethodName { get; set; }
        [Comment("Action Type: Add ,Read ,Delete ,Update")]
        public string ActionType { get; set; }
        [Comment("Parameters of the Action Logged")]
        public string Parameters { get; set; }
    }
}