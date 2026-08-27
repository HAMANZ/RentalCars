using System;

namespace RentalCar.DomainLayer.DTO
{
    public class DocumentTypeDTO : BaseDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Name_ar { get; set; }
    }
}
