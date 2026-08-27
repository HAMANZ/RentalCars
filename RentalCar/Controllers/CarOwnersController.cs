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
    public class CarOwnersController : Controller
    {
        private readonly ICarOwner _service;
        private readonly RentalCarDbContext _context;

        public CarOwnersController(ICarOwner service, RentalCarDbContext context)
        {
            _service = service;
            _context = context;
        }

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var list = await _context.CarOwners
                    .AsNoTracking()
                    .Where(e => !e.Is_deleted)
                    .Include(e => e.Nationality)
                    .Include(e => e.Country)
                    .Include(e => e.City)
                    .OrderByDescending(e => e.Id)
                    .ToListAsync();

                return View(list);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while loading car owners: " + ex.Message;
                return View(new List<CarOwner>());
            }
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadLookupsAsync();
            return View(new CarOwnerDTO { IsActive = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarOwnerDTO dto)
        {
            var result = await _service.AddAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Car owner added successfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = result.Message ?? "Failed to add car owner.";
            await LoadLookupsAsync();
            return View(dto);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var result = await _service.GetAsync(id);
            if (result.HttpStatusCode != HttpStatusCode.OK || result.Data == null)
                return NotFound();

            await LoadLookupsAsync();
            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CarOwnerDTO dto)
        {
            var result = await _service.UpdateAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Car owner updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            if (result.HttpStatusCode == HttpStatusCode.NotFound)
                return NotFound();

            TempData["Error"] = result.Message ?? "Failed to update car owner.";
            await LoadLookupsAsync();
            return View(dto);
        }
        #endregion

        #region Delete (soft)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _service.DeleteAsync(id);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
                TempData["Success"] = "Car owner deleted successfully!";
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
                TempData["Error"] = "Car owner not found.";
            else
                TempData["Error"] = result.Message ?? "Failed to delete car owner.";

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Helpers
        private async Task LoadLookupsAsync()
        {
            ViewBag.Nationalities = await _context.Nationalities.AsNoTracking()
                .Where(n => !n.Is_deleted).OrderBy(n => n.Name).ToListAsync();

            ViewBag.Countries = await _context.Countries.AsNoTracking()
                .Where(c => !c.Is_deleted).OrderBy(c => c.Name).ToListAsync();

            ViewBag.Cities = await _context.Cities.AsNoTracking()
                .Where(c => !c.Is_deleted).OrderBy(c => c.Name).ToListAsync();
        }
        #endregion
    }
}
