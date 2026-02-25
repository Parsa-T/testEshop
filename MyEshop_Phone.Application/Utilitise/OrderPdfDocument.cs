using MyEshop_Phone.Application.DTO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public class OrderPdfDocument : IDocument
{
    private readonly AdminOrderPdfDto _model;

    public OrderPdfDocument(AdminOrderPdfDto model)
    {
        _model = model ?? new AdminOrderPdfDto();
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(30);

            page.DefaultTextStyle(x =>
                x.FontFamily("Vazir")
                 .FontSize(12)
                 .DirectionFromRightToLeft());

            page.Content().Column(col =>
            {
                col.Spacing(10);

                // عنوان
                col.Item().AlignCenter().Text("فاکتور فروش")
                    .FontSize(20)
                    .Bold();

                col.Item().LineHorizontal(1);

                // اطلاعات خریدار (ایمن شده)
                col.Item().Text($"نام و نام خانوادگی: {_model.FullName ?? ""}");
                col.Item().Text($"استان : {_model.StateName ?? ""}");
                col.Item().Text($"شهرستان : {_model.CityName ?? ""}");
                col.Item().Text($"آدرس: {_model.Address ?? ""}");
                col.Item().Text($"کد پستی: {_model.PostalCode ?? ""}");

                col.Item().LineHorizontal(1);

                // جدول محصولات
                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(3);
                        columns.RelativeColumn(2);
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(2);
                    });

                    // Header
                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text("محصول").Bold();
                        header.Cell().Element(CellStyle).Text("رنگ").Bold();
                        header.Cell().Element(CellStyle).Text("تعداد").Bold();
                        header.Cell().Element(CellStyle).Text("قیمت").Bold();
                    });

                    foreach (var item in _model.Items ?? new List<AdminOrderPdfItemDto>())
                    {
                        var safeColor = GetSafeColor(item.ColorName);

                        table.Cell().Element(CellStyle)
                            .Text(item.ProductTitle ?? "");

                        table.Cell().Element(CellStyle).Row(row =>
                        {
                            row.ConstantItem(20)
                               .Height(20)
                               .Background(safeColor);

                            row.ConstantItem(5);

                            row.RelativeItem()
                               .Text(item.ColorName ?? "");
                        });

                        table.Cell().Element(CellStyle)
                            .Text(item.Count.ToString());

                        table.Cell().Element(CellStyle)
                            .Text(item.Price.ToString("N0"));
                    }
                });

                col.Item().LineHorizontal(1);

                col.Item().AlignLeft()
                    .Text($"مبلغ کل: {_model.TotalPrice:N0} تومان")
                    .FontSize(14)
                    .Bold();
            });
        });
    }

    static IContainer CellStyle(IContainer container)
    {
        return container
            .Border(1)
            .Padding(5)
            .AlignMiddle();
    }
    private string GetSafeColor(string? color)
    {
        if (string.IsNullOrWhiteSpace(color))
            return "#FFFFFF";

        color = color.Trim();

        // اگر با # شروع نشه، معتبر نیست
        if (!color.StartsWith("#"))
            return "#FFFFFF";

        // پشتیبانی از #RGB و #RRGGBB و حتی #RGBA
        if (color.Length == 4 || color.Length == 7 || color.Length == 9)
            return color;

        return "#FFFFFF";
    }
}