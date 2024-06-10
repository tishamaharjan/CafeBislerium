using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using static CafeBislerium.Components.Pages.PDFGeneration;

namespace CafeBislerium.Data
{
    internal class PDFService
    {

        public void GenerateReport(string reportFilePath, DateTime day, float TotalSales, List<CoffeeValues> topFiveCoffees, List<AddinValues> topFiveAddIns)
        {
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(3, Unit.Centimetre);//margin for pdf page

                    page.DefaultTextStyle(x => x.FontSize(12));// font size of pdf report

                    page.Header()
                        .Text("Generated Report")
                        .SemiBold().FontSize(22);//font size of heading of report

                    page.Content().Column(col =>
                    {
                        col.Item().Text($"DateTime: {day}");
                        col.Spacing(22);
                        col.Item().Text("Most Liked Coffees");

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(column =>
                            {
                                column.RelativeColumn();
                                column.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Coffee");
                                header.Cell().Text("Total Sold Quantity");
                            });

                            foreach (var item in topFiveCoffees)
                            {
                                table.Cell().Text(item.CoffeeName);
                                table.Cell().Text(item.TotalCoffeeQuantity);
                            }
                        });

                        col.Item().Text("Most Liked Addins");

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(column =>
                            {
                                column.RelativeColumn();
                                column.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("AddIn");
                                header.Cell().Text("Total Sold Quantity");
                            });

                            foreach (var item in topFiveAddIns)
                            {
                                table.Cell().Text(item.AddinName);
                                table.Cell().Text(item.TotalAddinsQuantity);
                            }
                        });

                        col.Spacing(19);
                        col.Item().Text($"Grand Total: Rs. {TotalSales}").Bold().Italic();
                    });

                });
            })
        .GeneratePdf(reportFilePath);
        }
    }
}
