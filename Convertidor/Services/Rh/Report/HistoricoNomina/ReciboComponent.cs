using System.Globalization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
namespace Convertidor.Services.Rh.Report.HistoricoNomina;

    public class ReciboComponent : IComponent
    {
        private string Title { get; }
       
        public List<RhReporteNominaResponseDto> ModelRecibos { get; }
        public ReciboComponent(string title, List<RhReporteNominaResponseDto> modelRecibos )
        {
            Title = title;
            ModelRecibos  = modelRecibos;
        }
        
        public void Compose(IContainer container)
        {
            var headerStyle = TextStyle.Default.SemiBold().FontSize(7);
            NumberFormatInfo formato = new CultureInfo("es-AR").NumberFormat;

            formato.CurrencyGroupSeparator = ".";
            formato.NumberDecimalSeparator = ",";
            formato.NumberDecimalDigits = 2;
            
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                   
                    columns.ConstantColumn(35);
                    columns.ConstantColumn(25);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });
               
                table.Header(header =>
                {
                    header.Cell().Text("TIPO").Style(headerStyle);
                    header.Cell().Text("Nro").Style(headerStyle);
                    header.Cell().Text("CONCEPTO").Style(headerStyle);
                    header.Cell().Text("COMPLEMENTO").Style(headerStyle);
                    header.Cell().AlignRight().Text("ASIGNACIONES").Style(headerStyle);
                    header.Cell().AlignRight().Text("DEDUCCIONES").Style(headerStyle);
                    //header.Cell().AlignRight().Text("").Style(headerStyle);
                    
                    header.Cell().ColumnSpan(6).PaddingTop(5).BorderBottom(1).BorderColor(Colors.Black);
                });
           
                foreach (var item in ModelRecibos)
                    {
                      

                        //var index = Model.ToList().IndexOf(item) + 1;
                        table.Cell().Element(CellStyle).Text($"{item.TipoMovConcepto}").FontSize(7);
                        table.Cell().Element(CellStyle).Text($"{item.NumeroConcepto}").FontSize(7);
                        table.Cell().Element(CellStyle).Text(item.DenominacionConcepto).FontSize(7);
                        table.Cell().Element(CellStyle).Text(item.ComplementoConcepto).FontSize(7);
                        var asignacion = item.Asignacion.ToString("N", formato);
                        var deduccion = item.Deduccion.ToString("N", formato);
                        table.Cell().Element(CellStyle).AlignRight().Text($"{asignacion}").FontSize(7);
                        table.Cell().Element(CellStyle).AlignRight().Text($"{deduccion}").FontSize(7);
                 
                    
                        static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(2);
                        //static IContainer CellStyle(IContainer container) => container.PaddingVertical(2);

                    }
                   
                    table.Cell().Text($"").FontSize(7);
                    table.Cell().Text($"").FontSize(7);
                    table.Cell().AlignRight().Text($"Total:").FontSize(10).SemiBold();
                    var asig = ModelRecibos.Sum(x => x.Asignacion);
                    var ded = ModelRecibos.Sum(x => x.Deduccion);
                    var totalAsignacion = asig.ToString("N", formato);
                    var totalDeduccion = ded.ToString("N", formato);
                    var neto = asig - ded;
                    table.Cell().AlignRight().Text($"{neto.ToString("N", formato)}").FontSize(10).SemiBold();
                    table.Cell().AlignRight().Text($"{totalAsignacion}").FontSize(10).SemiBold();
                    table.Cell().AlignRight().Text($"{totalDeduccion}").FontSize(10).SemiBold();
               
            });
        }
    }
    
    