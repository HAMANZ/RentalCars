

namespace RentalCar.DomainLayer.Models{ 
    public class MessageTemplate : BaseEntity
    {
        
        public long Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Name_ar { get; set; }
        public string Text { get; set; }
    }
}
