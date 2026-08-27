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
    public class WorkOrdersController : Controller
    {
        private readonly IWorkOrder _service;
        private readonly IWorkOrderDetail _detailService;
        private readonly RentalCarDbContext _context;

        public WorkOrdersController(IWorkOrder service, IWorkOrderDetail detailService, RentalCarDbContext context)
        {
            _service = service;
            _detailService = detailService;
            _context = context;
        }

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _service.GetAllAsync();
            if (result.HttpStatusCode != HttpStatusCode.OK)
                ViewBag.ErrorMessage = result.Message ?? "An error occurred while loading work orders.";

            var cars = await _context.Cars.AsNoTracking().ToDictionaryAsync(c => c.Id, c => c.Model);
            ViewBag.CarModels = cars;

            return View(result.Data ?? new List<WorkOrderDTO>());
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadLookupsAsync();
            return View(new WorkOrderDTO { Date = System.DateTime.Today });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            WorkOrderDTO dto,
            List<int> DetailRepairIds,
            List<int> DetailSparePartIds,
            List<int> DetailQuantities,
            List<decimal> DetailTotals)
        {
            if (dto.CarId == null)
            {
                TempData["Error"] = "Please select a car.";
                await LoadLookupsAsync();
                return View(dto);
            }

            var result = await _service.AddAsync(dto);
            if (result.HttpStatusCode != HttpStatusCode.OK)
            {
                TempData["Error"] = result.Message ?? "Failed to add work order.";
                await LoadLookupsAsync();
                return View(dto);
            }

            var workOrderId = result.Data;

            // Add the detail rows collected in the "Work Order Details" card
            // (parallel arrays, aligned by row index).
            int added = 0;
            if (DetailRepairIds != null)
            {
                for (int i = 0; i < DetailRepairIds.Count; i++)
                {
                    var detailDto = new WorkOrderDetailDTO
                    {
                        WorkOrderId = workOrderId,
                        RepairId = DetailRepairIds[i] > 0 ? DetailRepairIds[i] : (int?)null,
                        SparePartId = (DetailSparePartIds != null && i < DetailSparePartIds.Count && DetailSparePartIds[i] > 0)
                            ? DetailSparePartIds[i]
                            : (int?)null,
                        Quantity = (DetailQuantities != null && i < DetailQuantities.Count) ? DetailQuantities[i] : 0,
                        Total = (DetailTotals != null && i < DetailTotals.Count) ? DetailTotals[i] : 0
                    };

                    if (detailDto.RepairId == null && detailDto.SparePartId == null) continue;

                    var detailResult = await _detailService.AddAsync(detailDto);
                    if (detailResult.HttpStatusCode == HttpStatusCode.OK && detailResult.Data)
                        added++;
                }
            }

            TempData["Success"] = $"Work order added successfully with {added} detail row(s).";
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete (soft)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
                TempData["Success"] = "Work order deleted successfully!";
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
                TempData["Error"] = "Work order not found.";
            else
                TempData["Error"] = result.Message ?? "Failed to delete work order.";

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Helpers
        private async Task LoadLookupsAsync()
        {
            ViewBag.Cars = await _context.Cars.AsNoTracking()
                .Where(c => !c.Is_deleted).OrderBy(c => c.Model).ToListAsync();

            ViewBag.Statuses = await _context.Statuses.AsNoTracking()
                .Where(s => !s.Is_deleted).OrderBy(s => s.Name).ToListAsync();

            ViewBag.Repairs = await _context.Repairs.AsNoTracking()
                .Where(r => !r.Is_deleted).OrderBy(r => r.Name).ToListAsync();

            ViewBag.SpareParts = await _context.SpareParts.AsNoTracking()
                .Where(p => !p.Is_deleted).OrderBy(p => p.Name).ToListAsync();
        }
        #endregion
    }
}
