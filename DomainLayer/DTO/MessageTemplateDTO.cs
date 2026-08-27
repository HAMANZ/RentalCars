

namespace RentalCar.DomainLayer.DTO{ 
    public class MessageTemplateDTO : BaseDTO
    {
        public long Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Name_ar { get; set; }
        public string Text { get; set; }
    }
}
