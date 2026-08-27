using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RentalCar.DomainLayer.Models
{

    [Comment("Status Table is for predefined data used in the app")]
    public class Status : BaseEntity
    {

        [Key]
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Name_ar { get; set; }
        public bool Is_WorkOrderStatus { get; set; }
        public bool Is_InsuranceStatus { get; set; }

    }
}