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
    public class FuelTypesController : Controller
    {
        private readonly IFuelType _service;

        public FuelTypesController(IFuelType service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _service.GetAllAsync();
            if (result.HttpStatusCode != HttpStatusCode.OK)
                ViewBag.ErrorMessage = result.Message ?? "An error occurred while loading fuel types.";
            return View(result.Data ?? new List<FuelTypeDTO>());
        }

        [HttpGet]
        public IActionResult Create() => View(new FuelTypeDTO());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FuelTypeDTO dto)
        {
            var result = await _service.AddAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Fuel Type added successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = result.Message ?? "Failed to add fuel type.";
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
        public async Task<IActionResult> Edit(FuelTypeDTO dto)
        {
            var result = await _service.UpdateAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Fuel Type updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            if (result.HttpStatusCode == HttpStatusCode.NotFound)
                return NotFound();
            TempData["Error"] = result.Message ?? "Failed to update fuel type.";
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _service.DeleteAsync(id);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
                TempData["Success"] = "Fuel Type deleted successfully!";
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
                TempData["Error"] = "Fuel Type not found.";
            else
                TempData["Error"] = result.Message ?? "Failed to delete fuel type.";
            return RedirectToAction(nameof(Index));
        }
    }
}