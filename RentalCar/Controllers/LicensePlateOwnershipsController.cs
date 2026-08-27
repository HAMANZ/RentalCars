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
    public class LicensePlateOwnershipsController : Controller
    {
        private readonly ILicensePlateOwnership _service;
        private readonly RentalCarDbContext _context;

        public LicensePlateOwnershipsController(ILicensePlateOwnership service, RentalCarDbContext context)
        {
            _service = service;
            _context = context;
        }

        #region Index (with related plate & owner for display)
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var list = await _context.LicensePlateOwnerships
                    .AsNoTracking()
                    .Where(e => !e.Is_deleted)
                    .Include(e => e.LicensePlate)
                    .Include(e => e.PlateOwner)
                    .OrderByDescending(e => e.Id)
                    .ToListAsync();

                return View(list);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while loading ownerships: " + ex.Message;
                return View(new List<LicensePlateOwnership>());
            }
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadLookupsAsync();
            return View(new LicensePlateOwnershipDTO { StartDate = DateTime.Today, IsCurrent = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LicensePlateOwnershipDTO dto)
        {
            var result = await _service.AddAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Ownership added successfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = result.Message ?? "Failed to add ownership.";
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
        public async Task<IActionResult> Edit(LicensePlateOwnershipDTO dto)
        {
            var result = await _service.UpdateAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Ownership updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            if (result.HttpStatusCode == HttpStatusCode.NotFound)
                return NotFound();

            TempData["Error"] = result.Message ?? "Failed to update ownership.";
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
                TempData["Success"] = "Ownership deleted successfully!";
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
                TempData["Error"] = "Ownership not found.";
            else
                TempData["Error"] = result.Message ?? "Failed to delete ownership.";

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Helpers
        private async Task LoadLookupsAsync()
        {
            ViewBag.LicensePlates = await _context.LicensePlates.AsNoTracking()
                .Where(p => !p.Is_deleted).OrderBy(p => p.PlateNumber).ToListAsync();

            ViewBag.PlateOwners = await _context.PlateOwners.AsNoTracking()
                .Where(o => !o.Is_deleted).OrderBy(o => o.FullName).ToListAsync();
        }
        #endregion
    }
}
