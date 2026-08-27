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
    public class RepairTypesController : Controller
    {
        private readonly IRepairType _service;
        private readonly IRepairCategory _categoryService;

        public RepairTypesController(IRepairType service, IRepairCategory categoryService)
        {
            _service = service;
            _categoryService = categoryService;
        }

        private async Task LoadCategoriesAsync()
        {
            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = categories.Data ?? new List<RepairCategoryDTO>();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _service.GetAllAsync();
            if (result.HttpStatusCode != HttpStatusCode.OK)
                ViewBag.ErrorMessage = result.Message ?? "An error occurred while loading repair types.";
            return View(result.Data ?? new List<RepairTypeDTO>());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadCategoriesAsync();
            return View(new RepairTypeDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RepairTypeDTO dto)
        {
            var result = await _service.AddAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Repair Type added successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = result.Message ?? "Failed to add repair type.";
            await LoadCategoriesAsync();
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _service.GetAsync(id);
            if (result.HttpStatusCode != HttpStatusCode.OK || result.Data == null)
                return NotFound();
            await LoadCategoriesAsync();
            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RepairTypeDTO dto)
        {
            var result = await _service.UpdateAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Repair Type updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            if (result.HttpStatusCode == HttpStatusCode.NotFound)
                return NotFound();
            TempData["Error"] = result.Message ?? "Failed to update repair type.";
            await LoadCategoriesAsync();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
                TempData["Success"] = "Repair Type deleted successfully!";
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
                TempData["Error"] = "Repair Type not found.";
            else
                TempData["Error"] = result.Message ?? "Failed to delete repair type.";
            return RedirectToAction(nameof(Index));
        }

        #region Import from Excel
        private static readonly string[] TemplateHeaders = { "Name", "Name_ar", "Code", "Category", "IsActive" };

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

            var categoriesResult = await _categoryService.GetAllAsync();
            var categoriesByName = (categoriesResult.Data ?? new List<RepairCategoryDTO>())
                .Where(c => !string.IsNullOrWhiteSpace(c.Name))
                .GroupBy(c => c.Name.Trim(), StringComparer.OrdinalIgnoreCase)
                .ToDictionary(g => g.Key, g => g.First().Id, StringComparer.OrdinalIgnoreCase);

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

                    var categoryName = GetCellString(row, 3);
                    if (string.IsNullOrWhiteSpace(categoryName) || !categoriesByName.TryGetValue(categoryName.Trim(), out var categoryId))
                    {
                        failed++;
                        errors.Add($"Row {rowIndex + 1}: Category '{categoryName}' not found; row skipped.");
                        continue;
                    }

                    var isActiveText = GetCellString(row, 4);
                    var isActive = string.IsNullOrWhiteSpace(isActiveText) ||
                        !(isActiveText.Trim().Equals("false", StringComparison.OrdinalIgnoreCase) ||
                          isActiveText.Trim() == "0");

                    var dto = new RepairTypeDTO
                    {
                        Name = name,
                        Name_ar = GetCellString(row, 1),
                        Code = GetCellString(row, 2),
                        RepairCategoryId = categoryId,
                        IsActive = isActive
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

            TempData["Success"] = $"{added} repair type(s) imported successfully." +
                (failed > 0 ? $" {failed} row(s) failed." : "");
            if (errors.Count > 0)
                TempData["Error"] = string.Join(" | ", errors.Take(10));

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult DownloadTemplate()
        {
            var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet("RepairTypes");

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
            exampleRow.CreateCell(0).SetCellValue("Mechanical");
            exampleRow.CreateCell(1).SetCellValue("ميكانيكي");
            exampleRow.CreateCell(2).SetCellValue("MECH-01");
            exampleRow.CreateCell(3).SetCellValue("Brakes");
            exampleRow.CreateCell(4).SetCellValue("TRUE");

            using var ms = new MemoryStream();
            workbook.Write(ms, true);
            workbook.Close();
            return File(ms.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "RepairTypes_Template.xlsx");
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
