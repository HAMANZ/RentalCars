using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentalCar.Controllers
{
    // Parent menu group: "Documents".
    // Groups the Code + Name lookup child menus:
    //   - DocumentTypes -> DocumentType
    [Authorize]
    public class DocumentsController : Controller
    {
    }
}
