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
    public class BranchesController : Controller
    {
        private readonly IBranch _service;
        private readonly RentalCarDbContext _context;

        public BranchesController(IBranch service, RentalCarDbContext context)
        {
            _service = service;
            _context = context;
        }

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _service.GetAllAsync();
            if (result.HttpStatusCode != HttpStatusCode.OK)
                ViewBag.ErrorMessage = result.Message ?? "An error occurred while loading branches.";
            return View(result.Data ?? new List<BranchDTO>());
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadLookupsAsync();
            return View(new BranchDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BranchDTO dto)
        {
            var result = await _service.AddAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Branch added successfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = result.Message ?? "Failed to add branch.";
            await LoadLookupsAsync();
            return View(dto);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _service.GetAsync(id);
            if (result.HttpStatusCode != HttpStatusCode.OK || result.Data == null)
                return NotFound();

            await LoadLookupsAsync();
            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BranchDTO dto)
        {
            var result = await _service.UpdateAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Branch updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            if (result.HttpStatusCode == HttpStatusCode.NotFound)
                return NotFound();

            TempData["Error"] = result.Message ?? "Failed to update branch.";
            await LoadLookupsAsync();
            return View(dto);
        }
        #endregion

        #region Delete (soft)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
                TempData["Success"] = "Branch deleted successfully!";
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
                TempData["Error"] = "Branch not found.";
            else
                TempData["Error"] = result.Message ?? "Failed to delete branch.";

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Helpers
        private async Task LoadLookupsAsync()
        {
            ViewBag.Cities = await _context.Cities.AsNoTracking()
                .Where(c => !c.Is_deleted).OrderBy(c => c.Name).ToListAsync();
        }
        #endregion
    }
}
