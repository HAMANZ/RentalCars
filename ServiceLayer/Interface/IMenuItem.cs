using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using RentalCar.DomainLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Interface
{
    public interface IMenuItem
    {
        // Active, non-deleted menu items as a parent/child tree (used by the sidebar).
        Task<List<MenuItem>> GetMenuTreeAsync();

        // All non-deleted menu items as a tree, including inactive ones (used by the ordering screen).
        Task<List<MenuItem>> GetManagementTreeAsync();

        // Persists the new parent + sort order for the supplied items.
        Task<DynamicResponse<bool>> UpdateOrderAsync(List<MenuOrderItemDTO> items);

        // Returns a single menu item's editable properties.
        Task<DynamicResponse<MenuItemDTO>> GetByIdAsync(int id);

        // Persists edits to a menu item's name, title, icon, url and active state.
        Task<DynamicResponse<bool>> UpdateDetailsAsync(MenuItemDTO dto);

        // Creates a new menu item (top level, or under ParentId when supplied).
        Task<DynamicResponse<bool>> CreateAsync(MenuItemDTO dto);

        // Soft-deletes a menu item and all of its descendants.
        Task<DynamicResponse<bool>> DeleteAsync(int id);
    }
}
