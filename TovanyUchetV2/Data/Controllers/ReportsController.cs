// Controllers/ReportsController.cs
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using TovanyUchetV2.Data.Models;
using static TovanyUchetV2.Components.Pages.SalesReports; // Для SalesReportRow

[Route("api/reports")]
[ApiController]
public class ReportsController : ControllerBase
{
    private readonly IDataService _dataService;

    public ReportsController(IDataService dataService)
    {
        _dataService = dataService;
    }

    [HttpGet("sales/pdf")]
    public async Task<IActionResult> GetSalesReportPdf()
    {
        var sales = await _dataService.GetAllInventoryOperationsAsync();

        var report = sales
            .Where(o => o.OperationType == OperationType.Sale)
            .GroupBy(o => o.Product)
            .Select(g => new SalesReportRow
            {
                ProductName = g.Key.Name,
                TotalQuantity = g.Sum(x => x.Quantity),
                TotalSum = g.Sum(x => x.Quantity * x.Product.Price)
            })
            .ToList();

        var doc = new SalesReportDocument(report);
        var pdfBytes = doc.GeneratePdf();

        return File(pdfBytes, "application/pdf", $"SalesReport_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
    }
}
