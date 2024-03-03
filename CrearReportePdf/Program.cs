using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Previewer;






Document.Create(documento =>
{
  documento.Page(page =>
  {
      page.Margin(20);

      page.Header().ShowOnce().Row(fila => 
      {
          fila.ConstantItem(140).Border(0).Height(60).Placeholder();
          fila.RelativeItem().Border(0).Column(col =>
          {
              col.Item().AlignCenter().Text("Departamento").Bold().FontSize(14);
              col.Item().AlignCenter().Text("Finanzas").FontSize(9);
              col.Item().AlignCenter().Text("Gerencia").FontSize(9);
          });
          fila.RelativeItem().Border(0).Column(col =>
          {
              col.Item().Border(1).BorderColor(Colors.Cyan.Medium).AlignCenter().Text("Factura° : 0004568").Bold().FontSize(14);
              col.Item().Border(1).Background(Colors.Cyan.Medium).AlignCenter().Text("Orden Entrada : 000123546").FontSize(9);
              col.Item().AlignCenter().Text("Orden Salida : 006987563").FontSize(9);
          });
      });

      page.Content().Column(col1 =>
      {
          col1.Item().Text("Datos Del Cliente").Underline().Bold();

          col1.Item().Text(txt =>
          {
              txt.Span("Nombre : ").SemiBold().FontSize(10);
              txt.Span("Luis Patiño").FontSize(10);
          });

          col1.Item().Text(txt =>
          {
              txt.Span("Rif : ").SemiBold().FontSize(10);
              txt.Span("j-464-455454").FontSize(10);
          });

          col1.Item().LineHorizontal(0.5f);

          col1.Item().Table(async tabla =>
          {
              tabla.ColumnsDefinition(columnas =>
              {
                  columnas.RelativeColumn(4);
                  columnas.RelativeColumn();
                  columnas.RelativeColumn();
                  columnas.RelativeColumn();
              });

              tabla.Header(cabecera =>
              {
                  cabecera.Cell().Background(Colors.LightBlue.Medium)
                  .Padding(2).Text("Producto");
                  cabecera.Cell().Background(Colors.LightBlue.Medium)
                  .Padding(2).Text("Precio Unit");
                  cabecera.Cell().Background(Colors.LightBlue.Medium)
                  .Padding(2).Text("Cantidad");
                  cabecera.Cell().Background(Colors.LightBlue.Medium)
                  .Padding(2).Text("Total");

              });
              
              

              foreach (var item in Enumerable.Range(1, 45))
              {
                  var precio = Placeholders.Random.Next(1, 10);
                  var cantidad = Placeholders.Random.Next(5, 10);
                  var total = cantidad * precio;

                  tabla.Cell().BorderBottom(0.5f).BorderColor("#d9d9d9")
                  .Padding(2).Text(Placeholders.Label()).FontSize(10);

                  tabla.Cell().BorderBottom(0.5f).BorderColor("#d9d9d9")
                  .Padding(2).Text($"Bs .{precio}").FontSize(10);

                  tabla.Cell().BorderBottom(0.5f).BorderColor("#d9d9d9")
                  .Padding(2).Text(cantidad.ToString()).FontSize(10);

                  tabla.Cell().BorderBottom(0.5f).BorderColor("#d9d9d9")
                  .Padding(2).Text($"Bs .{total}").FontSize(10);

                 
              }

          });

          col1.Item().AlignRight().Text($"Total:");

      });
  });
}).ShowInPreviewer();

