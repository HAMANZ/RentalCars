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
    public class RepairsController : Controller
    {
        private readonly IRepair _service;
        private readonly IRepairType _typeService;

        public RepairsController(IRepair service, IRepairType typeService)
        {
            _service = service;
            _typeService = typeService;
        }

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _service.GetAllAsync();
            if (result.HttpStatusCode != HttpStatusCode.OK)
                ViewBag.ErrorMessage = result.Message ?? "An error occurred while loading repairs.";
            return View(result.Data ?? new List<RepairDTO>());
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadTypesAsync();
            return View(new RepairDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RepairDTO dto)
        {
            var result = await _service.AddAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Repair added successfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = result.Message ?? "Failed to add repair.";
            await LoadTypesAsync();
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

            await LoadTypesAsync();
            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RepairDTO dto)
        {
            var result = await _service.UpdateAsync(dto);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
            {
                TempData["Success"] = "Repair updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            if (result.HttpStatusCode == HttpStatusCode.NotFound)
                return NotFound();

            TempData["Error"] = result.Message ?? "Failed to update repair.";
            await LoadTypesAsync();
            return View(dto);
        }
        #endregion

        private async Task LoadTypesAsync()
        {
            var types = await _typeService.GetAllAsync();
            ViewBag.Types = types.Data ?? new List<RepairTypeDTO>();
        }

        #region Delete (soft)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (result.HttpStatusCode == HttpStatusCode.OK && result.Data)
                TempData["Success"] = "Repair deleted successfully!";
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
                TempData["Error"] = "Repair not found.";
            else
                TempData["Error"] = result.Message ?? "Failed to delete repair.";

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Import from Excel
        private static readonly string[] TemplateHeaders =
            { "Name", "Name_ar", "Type", "Details", "WorkTime", "LaborCost" };

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

            var typesResult = await _typeService.GetAllAsync();
            var typesByName = (typesResult.Data ?? new List<RepairTypeDTO>())
                .Where(t => !string.IsNullOrWhiteSpace(t.Name))
                .GroupBy(t => t.Name.Trim(), StringComparer.OrdinalIgnoreCase)
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

                    var typeName = GetCellString(row, 2);
                    if (string.IsNullOrWhiteSpace(typeName) || !typesByName.TryGetValue(typeName.Trim(), out var typeId))
                    {
                        failed++;
                        errors.Add($"Row {rowIndex + 1}: Type '{typeName}' not found; row skipped.");
                        continue;
                    }

                    var dto = new RepairDTO
                    {
                        Name = name,
                        Name_ar = GetCellString(row, 1),
                        RepairTypeId = typeId,
                        Details = GetCellString(row, 3),
                        WorkTime = GetCellDecimal(row, 4),
                        LaborCost = GetCellDecimal(row, 5)
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

            TempData["Success"] = $"{added} repair(s) imported successfully." +
                (failed > 0 ? $" {failed} row(s) failed." : "");
            if (errors.Count > 0)
                TempData["Error"] = string.Join(" | ", errors.Take(10));

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult DownloadTemplate()
        {
            var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet("Repairs");

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
            exampleRow.CreateCell(0).SetCellValue("Brake Pad Replacement");
            exampleRow.CreateCell(1).SetCellValue("استبدال تيل الفرامل");
            exampleRow.CreateCell(2).SetCellValue("Mechanical");
            exampleRow.CreateCell(3).SetCellValue("Replace front and rear brake pads");
            exampleRow.CreateCell(4).SetCellValue(1.5);
            exampleRow.CreateCell(5).SetCellValue(50.0);

            using var ms = new MemoryStream();
            workbook.Write(ms, true);
            workbook.Close();
            return File(ms.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Repairs_Template.xlsx");
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

        private static decimal GetCellDecimal(IRow row, int index)
        {
            var cell = row.GetCell(index);
            if (cell == null) return 0;
            if (cell.CellType == CellType.Numeric) return (decimal)cell.NumericCellValue;
            return decimal.TryParse(cell.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var val) ? val : 0;
        }
        #endregion
    }
}
