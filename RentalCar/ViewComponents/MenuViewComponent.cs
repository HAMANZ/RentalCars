using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentalCar.ServiceLayer.Interface;

namespace RentalCar.ViewComponents
{
    // Renders the sidebar navigation tree from the MenuItem table.
    public class MenuViewComponent : ViewComponent
    {
        private readonly IMenuItem _menuService;

        public MenuViewComponent(IMenuItem menuService)
        {
            _menuService = menuService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var tree = await _menuService.GetMenuTreeAsync();
            return View(tree);
        }
    }
}
