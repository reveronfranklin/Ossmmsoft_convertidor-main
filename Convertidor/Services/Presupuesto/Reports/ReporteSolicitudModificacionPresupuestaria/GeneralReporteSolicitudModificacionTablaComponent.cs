using System.Globalization;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Presupuesto.ReporteSolicitudModificacion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
namespace Convertidor.Services.Presupuesto.Report.Example;

    public class GeneralReporteSolicitudModificacionTablaComponent : IComponent
    {
        //private string Title { get; }
       
        public  GeneralReporteSolicitudModificacionDto  ModelGeneral { get; }
       
        public GeneralReporteSolicitudModificacionTablaComponent( GeneralReporteSolicitudModificacionDto modelGeneral)
        {
        
          ModelGeneral = modelGeneral;
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
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.ConstantColumn(55);
                    columns.ConstantColumn(35);
                    
                });
                
                table.Header(header =>
                {
                    header.Cell().ColumnSpan(4);
                    header.Cell().Border(1).AlignCenter().PaddingRight(2).Text("UNIDAD SOLICITANTE").Style(headerStyle);
                    header.Cell().Border(1).AlignLeft().PaddingLeft(5).Text("NRO").Style(headerStyle);
                    header.Cell().ColumnSpan(2).Border(1).AlignCenter().Text("FECHA").Style(headerStyle);
                    //header.Cell().AlignRight().Text("FECHA").Style(headerStyle);
                    //header.Cell().AlignRight().Text("ASIGNACION MOD").Style(headerStyle);
                    //header.Cell().AlignRight().Text("COMPROMETIDO").Style(headerStyle);
                    //header.Cell().AlignRight().Text("CAUSADO").Style(headerStyle);
                    //header.Cell().AlignRight().Text("PAGADO").Style(headerStyle);
                    
                    

                });


                    table.Cell().ColumnSpan(4);
                    table.Cell().Border(1).AlignCenter().PaddingRight(2).Element(CellStyle).Text(ModelGeneral.CodigoSolicitante).FontSize(7);
                    table.Cell().Border(1).ScaleToFit().AlignLeft().PaddingLeft(5).Element(CellStyle).Text(ModelGeneral.NumeroSolModificacion).FontSize(7);
                    table.Cell().ColumnSpan(2).Border(1).AlignCenter().Element(CellStyle).Text(ModelGeneral.FechaSolicitud.ToShortDateString()).FontSize(7);
                    table.Cell().ColumnSpan(8).Border(1).PaddingLeft(5).Element(CellStyle).Text(ModelGeneral.Motivo).FontSize(7);
                    table.Cell().ColumnSpan(8).Border(1).AlignCenter().Element(CellStyle).Text("BASE DE CALCULO").FontSize(7);
                //table.Cell().Element(CellStyle).AlignRight().Text($"{modificacion}").FontSize(7);
                //table.Cell().Element(CellStyle).AlignRight().Text($"{asignacionModificada}").FontSize(7);
                //table.Cell().Element(CellStyle).AlignRight().Text($"{comprometido}").FontSize(7);
                //table.Cell().Element(CellStyle).AlignRight().Text($"{causado}").FontSize(7);
                //table.Cell().Element(CellStyle).AlignRight().Text($"{pagado}").FontSize(7);

                static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                    //static IContainer CellStyle(IContainer container) => container.PaddingVertical(2);
                
                

                //table.Cell().Text($"").FontSize(7);
                //table.Cell().AlignRight().Text($"Total:").FontSize(7).SemiBold();
                //var totalPresupuestado = ModelRecibos.Sum(x => x.Presupuestado);
                //var totalModificacion = ModelRecibos.Sum(x => x.Modificacion);
                //var totalAsignacionModificada = ModelRecibos.Sum(x => x.AsignacionModificada);
                //var totalComprometido = ModelRecibos.Sum(x => x.Comprometido);
                //var totalCausado = ModelRecibos.Sum(x => x.Causado);
                //var totalPagado = ModelRecibos.Sum(x => x.Pagado);


                //table.Cell().AlignRight().Text($"{totalPresupuestado.ToString("N", formato)}").FontSize(7).SemiBold();
                //table.Cell().AlignRight().Text($"{totalModificacion.ToString("N", formato)}").FontSize(7).SemiBold();
                //table.Cell().AlignRight().Text($"{totalAsignacionModificada.ToString("N", formato)}").FontSize(7).SemiBold();
                //table.Cell().AlignRight().Text($"{totalComprometido.ToString("N", formato)}").FontSize(7).SemiBold();
                //table.Cell().AlignRight().Text($"{totalCausado.ToString("N", formato)}").FontSize(7).SemiBold();
                //table.Cell().AlignRight().Text($"{totalPagado.ToString("N", formato)}").FontSize(7).SemiBold();


            });
        }
    }
    
    