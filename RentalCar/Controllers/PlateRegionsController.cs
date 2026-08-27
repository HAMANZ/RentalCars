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
    public class PlateRegionsController : Controller
    {
        private readonly IPlateRegion _service;

        public PlateRegionsController(IPlateRegion service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _service.GetAllAsync();
            if (result.HttpStatusCode != HttpStatusCode.OK)
                ViewBag.ErrorMessage = result.Message ?? "An error occurred while loading plate regions.";

            return View(result.Data ?? new List<PlateRegionDTO>());
        }

        [HttpGet]
        public IActionResult Create() => View(new PlateRegionDTO());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlateRegionDTO dto)
        {
            var result = await _service.AddAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Plate region added successfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = result.Message ?? "Failed to add plate region.";
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
        public async Task<IActionResult> Edit(PlateRegionDTO dto)
        {
            var result = await _service.UpdateAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Plate region updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            if (result.HttpStatusCode == HttpStatusCode.NotFound)
                return NotFound();

            TempData["Error"] = result.Message ?? "Failed to update plate region.";
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _service.DeleteAsync(id);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
                TempData["Success"] = "Plate region deleted successfully!";
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
                TempData["Error"] = "Plate region not found.";
            else
                TempData["Error"] = result.Message ?? "Failed to delete plate region.";

            return RedirectToAction(nameof(Index));
        }
    }
}
