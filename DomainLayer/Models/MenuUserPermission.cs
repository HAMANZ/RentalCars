using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RentalCar.DomainLayer.Models
{

  
    public class UserMenuPermission : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        public string UserId { get; set; } = null!;

        public int MenuItemId { get; set; }

        public bool CanView { get; set; }

        public bool CanCreate { get; set; }

        public bool CanEdit { get; set; }

        public bool CanDelete { get; set; }

        public EUser User { get; set; } = null!;

        public MenuItem MenuItem { get; set; } = null!;
    }
}