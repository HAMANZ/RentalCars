using System;

namespace RentalCar.DomainLayer.DTO
{
    public class RentalPaymentDTO : BaseDTO
    {
        public long Id { get; set; }
        public string TransactionReference { get; set; }
        public string Notes { get; set; }
        public DateTime PaymentDate { get; set; }
        public double Amount { get; set; }

        // Foreign keys
        public long? PaymentMethodId { get; set; }
        public long? RentalContractId { get; set; }
    }
}
