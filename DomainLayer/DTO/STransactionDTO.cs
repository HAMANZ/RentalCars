using System;

namespace RentalCar.DomainLayer.DTO
{
    public class STransactionDTO : BaseDTO
    {
        public long TransactionId { get; set; }
        public int TransactionTypeId { get; set; }
        public long DebitAccountId { get; set; }
        public long CreditAccountId { get; set; }
        public double Amount { get; set; }
        public string ReferenceType { get; set; }
        public int? ReferenceId { get; set; }
        public DateTime OccurredAt { get; set; }
        public string Notes { get; set; }
    }
}
