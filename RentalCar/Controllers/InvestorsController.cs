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
    public class InvestorsController : Controller
    {
        private readonly IInvestor _service;
        private readonly RentalCarDbContext _context;

        public InvestorsController(IInvestor service, RentalCarDbContext context)
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
                var list = await _context.Investors
                    .AsNoTracking()
                    .Where(e => !e.Is_deleted)
                    .Include(e => e.Status)
                    .Include(e => e.Nationality)
                    .OrderByDescending(e => e.Id)
                    .ToListAsync();

                return View(list);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while loading investors: " + ex.Message;
                return View(new List<Investor>());
            }
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadLookupsAsync();
            return View(new InvestorDTO { LicenseExpiryDate = DateTime.Today });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvestorDTO dto)
        {
            var result = await _service.AddAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Investor added successfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = result.Message ?? "Failed to add investor.";
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
        public async Task<IActionResult> Edit(InvestorDTO dto)
        {
            var result = await _service.UpdateAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Investor updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            if (result.HttpStatusCode == HttpStatusCode.NotFound)
                return NotFound();

            TempData["Error"] = result.Message ?? "Failed to update investor.";
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
                TempData["Success"] = "Investor deleted successfully!";
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
                TempData["Error"] = "Investor not found.";
            else
                TempData["Error"] = result.Message ?? "Failed to delete investor.";

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Helpers
        private async Task LoadLookupsAsync()
        {
            ViewBag.Statuses = await _context.Statuses.AsNoTracking()
                .Where(s => !s.Is_deleted).OrderBy(s => s.Name).ToListAsync();

            ViewBag.Nationalities = await _context.Nationalities.AsNoTracking()
                .Where(n => !n.Is_deleted).OrderBy(n => n.Name).ToListAsync();

            ViewBag.Users = await _context.EUsers.AsNoTracking()
                .Where(u => !u.Is_deleted).OrderBy(u => u.FullName).ToListAsync();
        }
        #endregion
    }
}
