using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentalCar.Controllers
{
    // Parent menu group: "Finance".
    // Groups the Code + Name lookup child menus:
    //   - PaymentMethods -> PaymentMethod
    [Authorize]
    public class FinanceController : Controller
    {
    }
}
