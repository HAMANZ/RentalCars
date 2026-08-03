using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RentalCar.DomainLayer.Models
{

    [Comment("Notification Table is for storing all notifications for each User")]
    public class Notifications : BaseEntity
    {
        [Key]
        public long Id { get; set; }
        [Comment("User Id")]
        public string UserId { get; set; }
        public bool Is_Seen { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationSubject { get; set; }
        public string NotificationContent { get; set; }
    }
}