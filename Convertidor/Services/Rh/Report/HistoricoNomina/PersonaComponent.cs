using System.Globalization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
namespace Convertidor.Services.Rh.Report.HistoricoNomina;

  public class PersonaComponent : IComponent
    {
        private string Title { get; }
        private RhReporteNominaResponseDto Data { get; }

        public PersonaComponent(string title, RhReporteNominaResponseDto data)
        {
            Title = title;
            Data = data;
        }
            
        public void Compose(IContainer container)
        {
            var headerStyle = TextStyle.Default.SemiBold().FontSize(7);
            NumberFormatInfo formato = new CultureInfo("es-AR").NumberFormat;
            static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(2);
            formato.CurrencyGroupSeparator = ".";
            formato.NumberDecimalSeparator = ",";
            formato.NumberDecimalDigits = 2;
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                   
                    
                    
                  
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.ConstantColumn(80);
                });
               
                table.Header(header =>
                {
                   
                    
                    header.Cell().Text("CEDULA").Style(headerStyle);
                    header.Cell().Text("NOMBRE").Style(headerStyle);
                    header.Cell().Text("CARGO").Style(headerStyle);
                    header.Cell().Text("CODIGO").Style(headerStyle);
                    header.Cell().Text("SUELDO").Style(headerStyle);
                    header.Cell().Text("BANCO").Style(headerStyle);
                    header.Cell().Text("CUENTA").Style(headerStyle);
                  
                    
                    header.Cell().ColumnSpan(7).PaddingTop(5).Border(1).BorderColor(Colors.Black);
                });
                table.Cell().Element(CellStyle).Text($"{Data.Cedula}").FontSize(7);
                table.Cell().Element(CellStyle).Text($"{Data.Nombre}").FontSize(7);
                table.Cell().Element(CellStyle).Text($"{Data.DenominacionCargo}").FontSize(7);
                table.Cell().Element(CellStyle).Text($"{Data.CodigoPersona}").FontSize(7);
                var sueldo = Data.Sueldo.ToString("N", formato);
                table.Cell().Element(CellStyle).Text($"{sueldo}").FontSize(7);
                table.Cell().Element(CellStyle).Text($"{Data.Banco}").FontSize(7);
                table.Cell().Element(CellStyle).Text($"{Data.NoCuenta}").FontSize(7);
                
          
            });

        }
    }
