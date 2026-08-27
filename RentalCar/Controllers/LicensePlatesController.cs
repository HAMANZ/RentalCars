using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalCar.DomainLayer.DTO;
using RentalCar.DomainLayer.Models;
using RentalCar.ServiceLayer.Interface;

namespace RentalCar.Controllers
{
    [Authorize]
    public class LicensePlatesController : Controller
    {
        private readonly ILicensePlate _service;
        private readonly RentalCarDbContext _context;

        public LicensePlatesController(ILicensePlate service, RentalCarDbContext context)
        {
            _service = service;
            _context = context;
        }

        #region Index (with type & region for display)
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var list = await _context.LicensePlates
                    .AsNoTracking()
                    .Where(e => !e.Is_deleted)
                    .Include(e => e.PlateType)
                    .Include(e => e.PlateRegion)
                    .OrderByDescending(e => e.Id)
                    .ToListAsync();

                return View(list);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while loading license plates: " + ex.Message;
                return View(new List<LicensePlate>());
            }
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadLookupsAsync();
            return View(new LicensePlateDTO { IsActive = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LicensePlateDTO dto)
        {
            var result = await _service.AddAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "License plate added successfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = result.Message ?? "Failed to add license plate.";
            await LoadLookupsAsync();
            return View(dto);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var result = await _service.GetAsync(id);
            if (result.HttpStatusCode != HttpStatusCode.OK || result.Data == null)
                return NotFound();

            await LoadLookupsAsync();
            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LicensePlateDTO dto)
        {
            var result = await _service.UpdateAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "License plate updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            if (result.HttpStatusCode == HttpStatusCode.NotFound)
                return NotFound();

            TempData["Error"] = result.Message ?? "Failed to update license plate.";
            await LoadLookupsAsync();
            return View(dto);
        }
        #endregion

        #region Delete (soft)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _service.DeleteAsync(id);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
                TempData["Success"] = "License plate deleted successfully!";
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
                TempData["Error"] = "License plate not found.";
            else
                TempData["Error"] = result.Message ?? "Failed to delete license plate.";

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Helpers
        private async Task LoadLookupsAsync()
        {
            ViewBag.PlateTypes = await _context.PlateTypes.AsNoTracking()
                .Where(t => !t.Is_deleted).OrderBy(t => t.Name).ToListAsync();

            ViewBag.PlateRegions = await _context.PlateRegion.AsNoTracking()
                .Where(r => !r.Is_deleted).OrderBy(r => r.Name).ToListAsync();
        }
        #endregion
    }
}
