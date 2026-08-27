using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCar.DomainLayer.Models
{

    [Comment("AccountType Table is for predefined data used in the app")]
    public class SAccountType : BaseEntity
    {

        [Key]
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Name_ar { get; set; }

        public long AccountCategoryId { get; set; }

        [ForeignKey(nameof(AccountCategoryId))]
        public SAccountCategory AccountCategory { get; set; } = null!;

        public ICollection<SAccount> Accounts { get; set; }
            = new List<SAccount>();

        public bool IsActive { get; set; } = true;
    }
}