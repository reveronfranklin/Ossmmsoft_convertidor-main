using System.Globalization;
using System.Linq;
using Convertidor.Dtos.Presupuesto;
using iText.Layout.Element;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
namespace Convertidor.Services.Rh.Report.Example;

    public class ResumenSaldoTablaComponent : IComponent
    {
        private string Title { get; }
       
        public  List<PreResumenSaldoGetDto>  ModelRecibos { get; }
       
        public ResumenSaldoTablaComponent(string title, List<PreResumenSaldoGetDto> modelRecibos )
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
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });
               
                table.Header(header =>
                {
                    header.Cell().Text("ICP").Style(headerStyle);
                    header.Cell().Text("PARTIDA").Style(headerStyle);
                    header.Cell().AlignRight().Text("PRESUPUESTADO").Style(headerStyle);
                    header.Cell().AlignRight().Text("MODIFICACION").Style(headerStyle);
                    header.Cell().AlignRight().Text("ASIGNACION MOD").Style(headerStyle);
                    header.Cell().AlignRight().Text("COMPROMETIDO").Style(headerStyle);
                    header.Cell().AlignRight().Text("CAUSADO").Style(headerStyle);
                    header.Cell().AlignRight().Text("PAGADO").Style(headerStyle);
                    
                    
                    header.Cell().ColumnSpan(8).PaddingTop(5).BorderBottom(1).BorderColor(Colors.Black);
                });
           
                foreach (var item in ModelRecibos)
                    {
                     
                        table.Cell().Element(CellStyle).Text($"{item.CodigoIcpConcat}").FontSize(7);
                        table.Cell().Element(CellStyle).Text(item.Partida).FontSize(7);
                        var presupuestado = item.Presupuestado.ToString("N", formato);
                        var modificacion = item.Modificacion.ToString("N", formato);
                        var asignacionModificada = item.AsignacionModificada.ToString("N", formato);
                        var comprometido = item.Comprometido.ToString("N", formato);
                        var causado = item.Causado.ToString("N", formato);
                        var pagado = item.Pagado.ToString("N", formato);
                        table.Cell().Element(CellStyle).AlignRight().Text($"{presupuestado}").FontSize(7);
                        table.Cell().Element(CellStyle).AlignRight().Text($"{modificacion}").FontSize(7);
                        table.Cell().Element(CellStyle).AlignRight().Text($"{asignacionModificada}").FontSize(7);
                        table.Cell().Element(CellStyle).AlignRight().Text($"{comprometido}").FontSize(7);
                        table.Cell().Element(CellStyle).AlignRight().Text($"{causado}").FontSize(7);
                        table.Cell().Element(CellStyle).AlignRight().Text($"{pagado}").FontSize(7);
                        
                        static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(2);
                        //static IContainer CellStyle(IContainer container) => container.PaddingVertical(2);

                    }
                   
                    table.Cell().Text($"").FontSize(7);
                    table.Cell().AlignRight().Text($"Total:").FontSize(10).SemiBold();
                    var totalPresupuestado = ModelRecibos.Sum(x => x.Presupuestado);
                    var totalModificacion = ModelRecibos.Sum(x => x.Modificacion);
                    var totalAsignacionModificada = ModelRecibos.Sum(x => x.AsignacionModificada);
                    var totalComprometido = ModelRecibos.Sum(x => x.Comprometido);
                    var totalCausado = ModelRecibos.Sum(x => x.Causado);
                    var totalPagado = ModelRecibos.Sum(x => x.Pagado);
                    
          
                    table.Cell().AlignRight().Text($"{totalPresupuestado.ToString("N", formato)}").FontSize(10).SemiBold();
                    table.Cell().AlignRight().Text($"{totalModificacion.ToString("N", formato)}").FontSize(10).SemiBold();
                    table.Cell().AlignRight().Text($"{totalAsignacionModificada.ToString("N", formato)}").FontSize(10).SemiBold();
                    table.Cell().AlignRight().Text($"{totalComprometido.ToString("N", formato)}").FontSize(10).SemiBold();
                    table.Cell().AlignRight().Text($"{totalCausado.ToString("N", formato)}").FontSize(10).SemiBold();
                    table.Cell().AlignRight().Text($"{totalPagado.ToString("N", formato)}").FontSize(10).SemiBold();

               
            });
        }
    }
    
    