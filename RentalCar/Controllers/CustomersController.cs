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
    public class CustomersController : Controller
    {
        private readonly ICustomer _service;
        private readonly RentalCarDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CustomersController(ICustomer service, RentalCarDbContext context, IWebHostEnvironment env)
        {
            _service = service;
            _context = context;
            _env = env;
        }

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var list = await _context.Customers
                    .AsNoTracking()
                    .Where(e => !e.Is_deleted)
                    .Include(e => e.Nationality)
                    .OrderByDescending(e => e.Id)
                    .ToListAsync();

                return View(list);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while loading customers: " + ex.Message;
                return View(new List<Customer>());
            }
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadLookupsAsync();
            return View(new CustomerDTO { LicenseExpiryDate = DateTime.Today });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            CustomerDTO dto,
            IFormFile DrivingLicenseFile,
            List<IFormFile> DocumentFiles,
            List<long> DocumentTypeIds,
            List<string> DocumentExpiries,
            List<string> DocumentDescriptions)
        {
            // Driving license photo.
            if (DrivingLicenseFile != null && DrivingLicenseFile.Length > 0)
            {
                var saved = await SaveDocumentAsync(DrivingLicenseFile);
                if (saved == null)
                {
                    TempData["Error"] = "Invalid driving license file. Allowed: jpg, jpeg, png, webp, pdf.";
                    await LoadLookupsAsync();
                    return View(dto);
                }
                dto.DrivingLicense = saved;
            }

            // Additional documents card (parallel arrays, aligned by row index).
            dto.Documents = await BuildDocumentsAsync(DocumentFiles, DocumentTypeIds, DocumentExpiries, DocumentDescriptions);

            var result = await _service.AddAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Customer added successfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = result.Message ?? "Failed to add customer.";
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
            await LoadCustomerDocumentsAsync(id);
            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            CustomerDTO dto,
            IFormFile DrivingLicenseFile,
            List<IFormFile> DocumentFiles,
            List<long> DocumentTypeIds,
            List<string> DocumentExpiries,
            List<string> DocumentDescriptions,
            List<long> DeleteDocumentIds)
        {
            // Keep the existing license photo unless a new file is uploaded.
            if (DrivingLicenseFile != null && DrivingLicenseFile.Length > 0)
            {
                var saved = await SaveDocumentAsync(DrivingLicenseFile);
                if (saved == null)
                {
                    TempData["Error"] = "Invalid driving license file. Allowed: jpg, jpeg, png, webp, pdf.";
                    await LoadLookupsAsync();
                    await LoadCustomerDocumentsAsync(dto.Id);
                    return View(dto);
                }
                dto.DrivingLicense = saved;
            }

            var result = await _service.UpdateAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                // Remove documents the user flagged for deletion.
                if (DeleteDocumentIds != null)
                {
                    foreach (var docId in DeleteDocumentIds)
                        await _service.DeleteDocumentAsync(docId);
                }

                // Save any newly uploaded documents.
                var newDocs = await BuildDocumentsAsync(DocumentFiles, DocumentTypeIds, DocumentExpiries, DocumentDescriptions);
                if (newDocs.Count > 0)
                    await _service.AddDocumentsAsync(dto.Id, newDocs);

                TempData["Success"] = "Customer updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            if (result.HttpStatusCode == HttpStatusCode.NotFound)
                return NotFound();

            TempData["Error"] = result.Message ?? "Failed to update customer.";
            await LoadLookupsAsync();
            await LoadCustomerDocumentsAsync(dto.Id);
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
                TempData["Success"] = "Customer deleted successfully!";
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
                TempData["Error"] = "Customer not found.";
            else
                TempData["Error"] = result.Message ?? "Failed to delete customer.";

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Helpers
        private async Task LoadLookupsAsync()
        {
            ViewBag.Nationalities = await _context.Nationalities.AsNoTracking()
                .Where(n => !n.Is_deleted).OrderBy(n => n.Name).ToListAsync();

            ViewBag.Genders = await _context.Set<Gender>().AsNoTracking()
                .Where(g => !g.Is_deleted).OrderBy(g => g.Name).ToListAsync();

            ViewBag.DocumentTypes = await _context.DocumentTypes.AsNoTracking()
                .Where(d => !d.Is_deleted).OrderBy(d => d.Name).ToListAsync();

            ViewBag.Users = await _context.EUsers.AsNoTracking()
                .Where(u => !u.Is_deleted).OrderBy(u => u.FullName).ToListAsync();
        }

        private async Task LoadCustomerDocumentsAsync(string customerId)
        {
            var docs = await _service.GetDocumentsAsync(customerId);
            ViewBag.CustomerDocuments = docs.Data ?? new List<CustomerDocumentDTO>();
        }

        // Builds the additional-document DTOs from the parallel form arrays,
        // saving each uploaded file. Rows without a file are skipped.
        private async Task<List<CustomerDocumentDTO>> BuildDocumentsAsync(
            List<IFormFile> files, List<long> typeIds, List<string> expiries, List<string> descriptions)
        {
            var documents = new List<CustomerDocumentDTO>();
            if (files == null) return documents;

            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                if (file == null || file.Length == 0) continue;

                var saved = await SaveDocumentAsync(file);
                if (saved == null) continue;

                DateTime? expires = null;
                if (expiries != null && i < expiries.Count &&
                    DateTime.TryParse(expiries[i], out var parsed))
                {
                    expires = parsed;
                }

                documents.Add(new CustomerDocumentDTO
                {
                    FilePath = saved,
                    DocumentTypeId = (typeIds != null && i < typeIds.Count && typeIds[i] > 0)
                        ? typeIds[i]
                        : (long?)null,
                    Description = (descriptions != null && i < descriptions.Count) ? descriptions[i] : null,
                    ExpiresAt = expires
                });
            }

            return documents;
        }

        private async Task<string> SaveDocumentAsync(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName).ToLower();
            var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp", ".pdf" };
            if (!allowed.Contains(ext))
                return null;

            var fileName = Guid.NewGuid() + ext;
            var uploadPath = Path.Combine(_env.WebRootPath, "Images", "Customers");

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
