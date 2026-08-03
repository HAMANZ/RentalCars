
using Microsoft.AspNetCore.Mvc;

namespace RentalCar.Controllers
{

	public class ErrorController : Controller
	{

		public ErrorController()
		{
		}

		[HttpGet]
		public IActionResult Error()
		{

			return View();
		}


		[HttpGet]
		public IActionResult Error_400()
		{

			return View();
		}

		[HttpGet]
		public IActionResult Error_404()
		{

			return View();
		}

		[HttpGet]
		public IActionResult Error_403()
		{

			return View();
		}

		[HttpGet]
		public IActionResult Error_500()
		{

			return View();
		}



		[HttpGet]
		public IActionResult Error_503()
		{

			return View();
		}





	}
}