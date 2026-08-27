using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RentalCar.DomainLayer.Models
{

    public class MenuItem : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string? Name_ar { get; set; }

        public string Title { get; set; } = null!;

        public string? Icon { get; set; }

        public string? Url { get; set; }

        public int? ParentId { get; set; }

        public int SortOrder { get; set; }

        public bool IsActive { get; set; } = true;

        public MenuItem? Parent { get; set; }

        public ICollection<MenuItem> Children { get; set; }
            = new List<MenuItem>();

        public ICollection<UserMenuPermission> UserPermissions { get; set; }
            = new List<UserMenuPermission>();
    }
}