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
    public class STransactionTypesController : Controller
    {
        private readonly ISTransactionType _service;

        public STransactionTypesController(ISTransactionType service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _service.GetAllAsync();
            if (result.HttpStatusCode != HttpStatusCode.OK)
                ViewBag.ErrorMessage = result.Message ?? "An error occurred while loading transaction types.";
            return View(result.Data ?? new List<STransactionTypeDTO>());
        }

        [HttpGet]
        public IActionResult Create() => View(new STransactionTypeDTO());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(STransactionTypeDTO dto)
        {
            var result = await _service.AddAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Transaction Type added successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = result.Message ?? "Failed to add transaction type.";
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
        public async Task<IActionResult> Edit(STransactionTypeDTO dto)
        {
            var result = await _service.UpdateAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Transaction Type updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            if (result.HttpStatusCode == HttpStatusCode.NotFound)
                return NotFound();
            TempData["Error"] = result.Message ?? "Failed to update transaction type.";
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _service.DeleteAsync(id);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
                TempData["Success"] = "Transaction Type deleted successfully!";
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
                TempData["Error"] = "Transaction Type not found.";
            else
                TempData["Error"] = result.Message ?? "Failed to delete transaction type.";
            return RedirectToAction(nameof(Index));
        }
    }
}