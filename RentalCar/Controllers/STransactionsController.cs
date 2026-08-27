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
    public class STransactionsController : Controller
    {
        private readonly ISTransaction _service;
        private readonly RentalCarDbContext _context;

        public STransactionsController(ISTransaction service, RentalCarDbContext context)
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
                ViewBag.ErrorMessage = result.Message ?? "An error occurred while loading transactions.";

            await LoadLookupsAsync();
            return View(result.Data ?? new List<STransactionDTO>());
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadLookupsAsync();
            return View(new STransactionDTO { OccurredAt = System.DateTime.Today });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(STransactionDTO dto)
        {
            if (dto.DebitAccountId == dto.CreditAccountId)
            {
                TempData["Error"] = "Debit and credit accounts must be different.";
                await LoadLookupsAsync();
                return View(dto);
            }

            var result = await _service.AddAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Transaction added successfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = result.Message ?? "Failed to add transaction.";
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
        public async Task<IActionResult> Edit(STransactionDTO dto)
        {
            if (dto.DebitAccountId == dto.CreditAccountId)
            {
                TempData["Error"] = "Debit and credit accounts must be different.";
                await LoadLookupsAsync();
                return View(dto);
            }

            var result = await _service.UpdateAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Transaction updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            if (result.HttpStatusCode == HttpStatusCode.NotFound)
                return NotFound();

            TempData["Error"] = result.Message ?? "Failed to update transaction.";
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
                TempData["Success"] = "Transaction deleted successfully!";
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
                TempData["Error"] = "Transaction not found.";
            else
                TempData["Error"] = result.Message ?? "Failed to delete transaction.";

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Helpers
        private async Task LoadLookupsAsync()
        {
            ViewBag.TransactionTypes = await _context.STransactionType.AsNoTracking()
                .Where(t => !t.Is_deleted).OrderBy(t => t.Name).ToListAsync();

            ViewBag.Accounts = await _context.SAccounts.AsNoTracking()
                .Where(a => !a.Is_deleted).OrderBy(a => a.Name).ToListAsync();

            ViewBag.Branches = await _context.Branches.AsNoTracking()
                .Where(b => !b.Is_deleted).OrderBy(b => b.Name).ToListAsync();
        }
        #endregion
    }
}
