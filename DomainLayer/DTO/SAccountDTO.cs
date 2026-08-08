using System;

namespace RentalCar.DomainLayer.DTO
{
    public class SAccountDTO : BaseDTO
    {
        public long AccountId { get; set; }
        public int AccountTypeId { get; set; }
        public string OwnerType { get; set; }
        public int? OwnerId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        // Read-only: Balance is only mutated by the transaction engine.
        public double Balance { get; set; }

        public string Currency { get; set; }
        public bool IsActive { get; set; }
    }
}
