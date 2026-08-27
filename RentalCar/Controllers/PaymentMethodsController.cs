using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.DomainLayer.DTO;
using RentalCar.ServiceLayer.Interface;

namespace RentalCar.Controllers
{
    [Authorize]
    public class PaymentMethodsController : Controller
    {
        private readonly IPaymentMethod _service;

        public PaymentMethodsController(IPaymentMethod service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _service.GetAllAsync();
            if (result.HttpStatusCode != HttpStatusCode.OK)
                ViewBag.ErrorMessage = result.Message ?? "An error occurred while loading payment methods.";
            return View(result.Data ?? new List<PaymentMethodDTO>());
        }

        [HttpGet]
        public IActionResult Create() => View(new PaymentMethodDTO());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentMethodDTO dto)
        {
            var result = await _service.AddAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Payment Method added successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = result.Message ?? "Failed to add payment method.";
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var result = await _service.GetAsync(id);
            if (result.HttpStatusCode != HttpStatusCode.OK || result.Data == null)
                return NotFound();
            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PaymentMethodDTO dto)
        {
            var result = await _service.UpdateAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Payment Method updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            if (result.HttpStatusCode == HttpStatusCode.NotFound)
                return NotFound();
            TempData["Error"] = result.Message ?? "Failed to update payment method.";
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _service.DeleteAsync(id);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
                TempData["Success"] = "Payment Method deleted successfully!";
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
                TempData["Error"] = "Payment Method not found.";
            else
                TempData["Error"] = result.Message ?? "Failed to delete payment method.";
            return RedirectToAction(nameof(Index));
        }
    }
}