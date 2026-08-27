namespace RentalCar.DomainLayer.DTO
{
    // Editable properties of a menu item (name, display title, icon, redirect URL, active state).
    public class MenuItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name_ar { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }

        // Optional parent for placing a newly created item under a group (null = top level).
        public int? ParentId { get; set; }
    }
}
