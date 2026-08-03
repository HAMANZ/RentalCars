using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RentalCar.DomainLayer.DTO
{

    public class NotificationsDTO : BaseDTO
    {
        [Key]
        public long Id { get; set; }
        [Comment("User Id")]
        public string UserId { get; set; }
        public long MessageId { get; set; }
        public string Email { get; set; }
        public string TokenId { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationSubject { get; set; }
        public string NotificationContent { get; set; }
        public string Date { get; set; }
        public bool Is_Seen { get; set; }
    }
}