using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalCar.DomainLayer.DTO;
using RentalCar.DomainLayer.Models;
using RentalCar.ServiceLayer.Interface;

namespace RentalCar.Controllers
{
    [Authorize]
    public class CarsController : Controller
    {
        private readonly RentalCarDbContext _context;
        private readonly ICar _carService;
        private readonly IWebHostEnvironment _env;

        public CarsController(RentalCarDbContext context, ICar carService, IWebHostEnvironment env)
        {
            _context = context;
            _carService = carService;
            _env = env;
        }

        #region Index (Cars list / DataTable)
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var cars = await _context.Cars
                    .AsNoTracking()
                    .Where(c => !c.Is_deleted)
                    .Include(c => c.Brand)
                    .Include(c => c.LicensePlate)
                    .Include(c => c.CarStatus)
                    .Include(c => c.Branch)
                    .Include(c => c.FuelType)
                    .OrderByDescending(c => c.Id)
                    .ToListAsync();

                return View(cars);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while loading the cars: " + ex.Message;
                return View(new List<Car>());
            }
        }
        #endregion

        #region Details (Car details + related cards)
        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            var car = await _context.Cars
                .AsNoTracking()
                // Car information
                .Include(c => c.Brand)
                .Include(c => c.FuelType)
                .Include(c => c.LicensePlate)
                .Include(c => c.CarOwner)
                .Include(c => c.Branch)
                .Include(c => c.CarStatus)
                .Include(c => c.Investor)
                // Maintenance schedules
                .Include(c => c.OilSchedules)
                .Include(c => c.TireSchedules)
                .Include(c => c.BatterySchedules)
                // Rental contracts
                .Include(c => c.Contracts).ThenInclude(rc => rc.Customer)
                .Include(c => c.Contracts).ThenInclude(rc => rc.Status)
                // Work orders
                .Include(c => c.WorkOrders).ThenInclude(w => w.Status)
                // Insurance
                .Include(c => c.Insurances).ThenInclude(ins => ins.InsuranceCompany)
                .Include(c => c.Insurances).ThenInclude(ins => ins.InsuranceType)
                .Include(c => c.Insurances).ThenInclude(ins => ins.Status)
                // Documents
                .Include(c => c.Documents).ThenInclude(d => d.DocumentType)
                .FirstOrDefaultAsync(c => c.Id == id && !c.Is_deleted);

            if (car == null)
                return NotFound();

            return View(car);
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadLookupsAsync();
            return View(new CarDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarDTO dto, IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var saved = await SaveCarImageAsync(ImageFile);
                if (saved == null)
                {
                    TempData["Error"] = "Invalid image type. Allowed: jpg, jpeg, png, webp.";
                    await LoadLookupsAsync();
                    return View(dto);
                }
                dto.Image = saved;
            }

            var result = await _carService.AddAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Car added successfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = result.Message ?? "Failed to add car.";
            await LoadLookupsAsync();
            return View(dto);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var result = await _carService.GetAsync(id);
            if (result.HttpStatusCode != HttpStatusCode.OK || result.Data == null)
                return NotFound();

            await LoadLookupsAsync();
            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CarDTO dto, IFormFile ImageFile)
        {
            // Keep the existing image unless a new file is uploaded.
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var saved = await SaveCarImageAsync(ImageFile);
                if (saved == null)
                {
                    TempData["Error"] = "Invalid image type. Allowed: jpg, jpeg, png, webp.";
                    await LoadLookupsAsync();
                    return View(dto);
                }
                dto.Image = saved;
            }

            var result = await _carService.UpdateAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Car updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            if (result.HttpStatusCode == HttpStatusCode.NotFound)
                return NotFound();

            TempData["Error"] = result.Message ?? "Failed to update car.";
            await LoadLookupsAsync();
            return View(dto);
        }
        #endregion

        #region Delete (soft)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _carService.DeleteAsync(id);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
                TempData["Success"] = "Car deleted successfully!";
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
                TempData["Error"] = "Car not found.";
            else
                TempData["Error"] = result.Message ?? "Failed to delete car.";

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Helpers
        private async Task LoadLookupsAsync()
        {
            ViewBag.Brands = await _context.Brands.AsNoTracking()
                .Where(b => !b.Is_deleted).OrderBy(b => b.Name).ToListAsync();

            ViewBag.FuelTypes = await _context.FuelTypes.AsNoTracking()
                .Where(f => !f.Is_deleted).OrderBy(f => f.Name).ToListAsync();

            ViewBag.LicensePlates = await _context.LicensePlates.AsNoTracking()
                .Where(l => !l.Is_deleted).OrderBy(l => l.PlateNumber).ToListAsync();

            ViewBag.CarOwners = await _context.CarOwners.AsNoTracking()
                .Where(o => !o.Is_deleted).OrderBy(o => o.FullName).ToListAsync();

            ViewBag.Branches = await _context.Branches.AsNoTracking()
                .Where(br => !br.Is_deleted).OrderBy(br => br.Name).ToListAsync();

            ViewBag.CarStatuses = await _context.CarStatus.AsNoTracking()
                .Where(s => !s.Is_deleted).OrderBy(s => s.Name).ToListAsync();

            ViewBag.Investors = await _context.Investors.AsNoTracking()
                .Where(i => !i.Is_deleted)
                .ToListAsync();
        }

        private async Task<string> SaveCarImageAsync(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName).ToLower();
            var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            if (!allowed.Contains(ext))
                return null;

            var fileName = Guid.NewGuid() + ext;
            var uploadPath = Path.Combine(_env.WebRootPath, "Images", "Cars");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fullPath = Path.Combine(uploadPath, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
        #endregion
    }
}
