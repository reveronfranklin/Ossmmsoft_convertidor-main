using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Presupuesto.ReporteCompromisoPresupuestario;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using NPOI.SS.Formula.Functions;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

namespace Convertidor.Services.Presupuesto.ReporteCompromisoPresupuestario
{
    public class PucComponent : IComponent
    {
        public List<CuerpoReporteDto> ModelCuerpo;
        public  EncabezadoReporteDto ModelEncabezado;

        public PucComponent(List<CuerpoReporteDto> modelCuerpo, EncabezadoReporteDto modelEncabezado)
        {
            ModelCuerpo = modelCuerpo;
            ModelEncabezado = modelEncabezado;
        }

        public void Compose(IContainer container)
        {
            QuestPDF.Settings.CheckIfAllTextGlyphsAreAvailable = false;


            var headerStyle = TextStyle.Default.SemiBold().FontSize(7).Fallback();
            NumberFormatInfo formato = new CultureInfo("es-AR").NumberFormat;
            static IContainer CellStyle(IContainer container) => container;
            formato.CurrencyGroupSeparator = ".";
            formato.NumberDecimalSeparator = ",";
            formato.NumberDecimalDigits = 2;


            container.Table(table =>
            {

                
                table.ExtendLastCellsToTableBottom();

                table.ColumnsDefinition(columns =>
                {

                    columns.ConstantColumn(320);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
        
                   
                });



                table.Header(header =>
                {
                    table.ExtendLastCellsToTableBottom();

                    header.Cell().ColumnSpan(6).Background(Colors.Grey.Lighten2).Border(1).AlignCenter().Text("PUC DEL COMPROMISO").FontSize(12).Bold();
                    header.Cell().ColumnSpan(6).Row(row =>
                    {

                        row.ConstantItem(80).Border(1).AlignCenter().Element(CellStyle).Text("SE-PR-SP-PY-AC").FontSize(7).Bold();
                        row.ConstantItem(80).Border(1).AlignCenter().Element(CellStyle).Text("GR-PA-GE-ES-SE-EX").FontSize(7).Bold();
                        row.RelativeItem(3).Border(1).AlignCenter().Element(CellStyle).PaddingLeft(40).Text("FINANCIADO").FontSize(7).Bold();
                        row.ConstantItem(60).Border(1).AlignCenter().Element(CellStyle).Text("MONTO").FontSize(7).Bold();
                        /*row.ConstantItem(70).Column(col =>
                        {
                            col.Item().Border(1).AlignCenter().Element(CellStyle).Text("").FontSize(7).Bold();
                            col.Item().Border(1).AlignCenter().Element(CellStyle).Text("").FontSize(7).Bold();
                        });*/



                    });;

                });

                foreach (var item in ModelEncabezado.PucCompromisos)
                {
                    
                    table.Cell().ColumnSpan(6).Row(row =>
                    {
                        row.ConstantItem(80).BorderVertical(1).AlignCenter().PaddingRight(3).Element(CellStyle).Text(item.CodigoIcpConcat).FontSize(7);
                        row.ConstantItem(80).BorderVertical(1).AlignCenter().PaddingRight(3).Element(CellStyle).Text(item.CodigoPucConcat).FontSize(8);
                        row.RelativeItem(3).BorderVertical(1).AlignLeft().PaddingLeft(10).Element(CellStyle).Text(item.DescripcionFinanciado).FontSize(8);
                        var precio = item.Monto.ToString("N", formato);
                        row.ConstantItem(60).BorderVertical(1).AlignRight().PaddingRight(3).Element(CellStyle).Text(precio).FontSize(7);
                        /*var totalBolivares = item.TotalBolivares.ToString("N", formato);
                        row.ConstantItem(70).BorderVertical(1).AlignRight().PaddingRight(3).Element(CellStyle).Text(totalBolivares).FontSize(7);
                        */

                    });
                 

                }

                foreach (var i in Enumerable.Range(0, 15))
                {
                           table.Cell().ColumnSpan(6).BorderLeft(1).Column(col =>
                                         {
                                             col.Item().Row(row =>
                                             {

                                                 col.Item().ExtendVertical().Row(row =>
                                                 {
                                                     row.ConstantItem(80).ExtendVertical().BorderVertical(1);
                                                     row.ConstantItem(80).ExtendVertical().BorderVertical(1);
                                                     row.RelativeItem(3).ExtendVertical().BorderVertical(1);
                                                     row.ConstantItem(60).ExtendVertical().BorderVertical(1);
                                                 });
                                             });

                                         });

                }
                    

            });





     
        }
       
    }
}
