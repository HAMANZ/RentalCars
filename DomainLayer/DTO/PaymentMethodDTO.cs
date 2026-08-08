using System;

namespace RentalCar.DomainLayer.DTO
{
    public class PaymentMethodDTO : BaseDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
