using System.Globalization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
namespace Convertidor.Services.Rh.Report.Example;

  public class PersonasComponent : IComponent
    {
    private readonly string _descripcionTipoNomina;

    private string Title { get; }
        private RhReporteNominaResponseDto Data { get; }

        public PersonasComponent(string title, RhReporteNominaResponseDto data,string descripcionTipoNomina)
        {
            Title = title;
            Data = data;
        _descripcionTipoNomina = descripcionTipoNomina;
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
                    columns.ConstantColumn(180);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.ConstantColumn(63);
                    columns.ConstantColumn(63);

                });
               
                table.Header(header =>
                {
                   
                    
                    header.Cell().Border(1).AlignCenter().Text("DEPARTAMENTO").Style(headerStyle);
                    header.Cell().Border(1).AlignCenter().Text("NOMBRE").Style(headerStyle);
                    header.Cell().Border(1).AlignCenter().Text("TIPO NOMINA").Style(headerStyle);
                    header.Cell().Border(1).AlignCenter().Text("CEDULA").Style(headerStyle);
                    header.Cell().Border(1).AlignCenter().Text("SUELDO").Style(headerStyle);
                    header.Cell().Border(1).AlignCenter().Text("FECHA").Style(headerStyle);
                   
                    //header.Cell().ColumnSpan(6).PaddingTop(5).Border(1).BorderColor(Colors.Black);
                });
                table.Cell().Border(1).AlignCenter().Element(CellStyle).Text($"{Data.CodigoIcpConcat}").FontSize(7);
                table.Cell().Border(1).AlignCenter().Element(CellStyle).Text($"{Data.Nombre}").FontSize(7);
                table.Cell().Border(1).AlignCenter().Element(CellStyle).Text($"{_descripcionTipoNomina}").FontSize(7);
                table.Cell().Border(1).AlignCenter().Element(CellStyle).Text($"{Data.Cedula}").FontSize(7);

                
                var sueldo = Data.Sueldo.ToString("N", formato);
                table.Cell().Border(1).AlignCenter().Element(CellStyle).Text($"{sueldo}").FontSize(7);
                table.Cell().Border(1).AlignCenter().Element(CellStyle).Text($"{Data.FechaNomina.ToShortDateString()}").FontSize(7);
                
               
            });

        }
    }
