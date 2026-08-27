using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentalCar.Controllers
{
    // Parent menu group: "Fleet Management".
    // Groups the Code + Name lookup child menus:
    //   - Brands       -> Brand
    //   - FuelTypes    -> FuelType
    //   - CarStatuses  -> CarStatus
    [Authorize]
    public class FleetManagementController : Controller
    {
    }
}
