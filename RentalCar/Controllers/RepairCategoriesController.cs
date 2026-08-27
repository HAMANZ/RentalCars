using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using RentalCar.DomainLayer.DTO;
using RentalCar.ServiceLayer.Interface;

namespace RentalCar.Controllers
{
    [Authorize]
    public class RepairCategoriesController : Controller
    {
        private readonly IRepairCategory _service;

        public RepairCategoriesController(IRepairCategory service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _service.GetAllAsync();
            if (result.HttpStatusCode != HttpStatusCode.OK)
                ViewBag.ErrorMessage = result.Message ?? "An error occurred while loading repair categories.";
            return View(result.Data ?? new List<RepairCategoryDTO>());
        }

        [HttpGet]
        public IActionResult Create() => View(new RepairCategoryDTO());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RepairCategoryDTO dto)
        {
            var result = await _service.AddAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Repair Category added successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = result.Message ?? "Failed to add repair category.";
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _service.GetAsync(id);
            if (result.HttpStatusCode != HttpStatusCode.OK || result.Data == null)
                return NotFound();
            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RepairCategoryDTO dto)
        {
            var result = await _service.UpdateAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Repair Category updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            if (result.HttpStatusCode == HttpStatusCode.NotFound)
                return NotFound();
            TempData["Error"] = result.Message ?? "Failed to update repair category.";
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
                TempData["Success"] = "Repair Category deleted successfully!";
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
                TempData["Error"] = "Repair Category not found.";
            else
                TempData["Error"] = result.Message ?? "Failed to delete repair category.";
            return RedirectToAction(nameof(Index));
        }

        #region Import from Excel
        private static readonly string[] TemplateHeaders = { "Name", "Name_ar" };

        [HttpGet]
        public IActionResult Import() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile File)
        {
            if (File == null || File.Length == 0)
            {
                TempData["Error"] = "Please choose an Excel file (.xlsx) to import.";
                return RedirectToAction(nameof(Import));
            }

            if (Path.GetExtension(File.FileName).ToLowerInvariant() != ".xlsx")
            {
                TempData["Error"] = "Only .xlsx files are supported.";
                return RedirectToAction(nameof(Import));
            }

            int added = 0, failed = 0;
            var errors = new List<string>();

            using (var stream = new MemoryStream())
            {
                await File.CopyToAsync(stream);
                stream.Position = 0;

                IWorkbook workbook;
                try
                {
                    workbook = new XSSFWorkbook(stream);
                }
                catch (Exception)
                {
                    TempData["Error"] = "Could not read the Excel file. Make sure it matches the downloaded template.";
                    return RedirectToAction(nameof(Import));
                }

                var sheet = workbook.GetSheetAt(0);

                // Row 0 is the header; data starts at row 1.
                for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
                {
                    var row = sheet.GetRow(rowIndex);
                    if (row == null || IsRowEmpty(row)) continue;

                    var name = GetCellString(row, 0);
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        failed++;
                        errors.Add($"Row {rowIndex + 1}: Name is required.");
                        continue;
                    }

                    var dto = new RepairCategoryDTO
                    {
                        Name = name,
                        Name_ar = GetCellString(row, 1)
                    };

                    var result = await _service.AddAsync(dto);
                    if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
                        added++;
                    else
                    {
                        failed++;
                        errors.Add($"Row {rowIndex + 1}: {result.Message ?? "Failed to add."}");
                    }
                }
            }

            TempData["Success"] = $"{added} repair category(ies) imported successfully." +
                (failed > 0 ? $" {failed} row(s) failed." : "");
            if (errors.Count > 0)
                TempData["Error"] = string.Join(" | ", errors.Take(10));

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult DownloadTemplate()
        {
            var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet("RepairCategories");

            var headerFont = workbook.CreateFont();
            headerFont.IsBold = true;
            var headerStyle = workbook.CreateCellStyle();
            headerStyle.SetFont(headerFont);

            var headerRow = sheet.CreateRow(0);
            for (int i = 0; i < TemplateHeaders.Length; i++)
            {
                var cell = headerRow.CreateCell(i);
                cell.SetCellValue(TemplateHeaders[i]);
                cell.CellStyle = headerStyle;
                sheet.SetColumnWidth(i, 20 * 256);
            }

            // Example row to guide the user; safe to delete before importing.
            var exampleRow = sheet.CreateRow(1);
            exampleRow.CreateCell(0).SetCellValue("Brakes");
            exampleRow.CreateCell(1).SetCellValue("الفرامل");

            using var ms = new MemoryStream();
            workbook.Write(ms, true);
            workbook.Close();
            return File(ms.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "RepairCategories_Template.xlsx");
        }

        private static bool IsRowEmpty(IRow row)
        {
            for (int c = 0; c < row.LastCellNum; c++)
            {
                var cell = row.GetCell(c);
                if (cell != null && cell.CellType != CellType.Blank && !string.IsNullOrWhiteSpace(cell.ToString()))
                    return false;
            }
            return true;
        }

        private static string GetCellString(IRow row, int index)
        {
            var cell = row.GetCell(index);
            if (cell == null) return null;
            return cell.CellType == CellType.Numeric
                ? cell.NumericCellValue.ToString(CultureInfo.InvariantCulture)
                : cell.ToString()?.Trim();
        }
        #endregion
    }
}
