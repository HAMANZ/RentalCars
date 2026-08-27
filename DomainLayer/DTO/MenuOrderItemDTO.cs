namespace RentalCar.DomainLayer.DTO
{
    // Lightweight transport used by the drag-and-drop menu ordering screen.
    public class MenuOrderItemDTO
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int SortOrder { get; set; }
    }
}
