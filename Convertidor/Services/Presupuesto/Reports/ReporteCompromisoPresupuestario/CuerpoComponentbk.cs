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
    public class CuerpoComponentbk : IComponent
    {
        public List<CuerpoReporteDto> ModelCuerpo;
        public  EncabezadoReporteDto ModelEncabezado;

        public CuerpoComponentbk(List<CuerpoReporteDto> modelCuerpo, EncabezadoReporteDto modelEncabezado)
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

                    header.Cell().ColumnSpan(6).Background(Colors.Grey.Lighten2).Border(1).AlignCenter().Text("DETALLES DEL COMPROMISO").FontSize(12).Bold();
                    header.Cell().ColumnSpan(6).Row(row =>
                      {

                          row.ConstantItem(40).Border(1).AlignCenter().Element(CellStyle).Text("CANTIDAD").FontSize(7).Bold();
                          row.ConstantItem(50).Border(1).AlignCenter().Element(CellStyle).Text("   UNIDAD\n" + "DE MEDIDA").FontSize(7).Bold();
                          row.RelativeItem(3).Border(1).AlignCenter().Element(CellStyle).PaddingLeft(50).Text("DESCRIPCION").FontSize(7).Bold();
                          row.ConstantItem(60).Border(1).AlignCenter().Element(CellStyle).Text("   PRECIO\n" + "UNITARIO").FontSize(7).Bold();
                          row.ConstantItem(70).Column(col =>
                          {
                              col.Item().Border(1).AlignCenter().Element(CellStyle).Text("TOTAL").FontSize(7).Bold();
                              col.Item().Border(1).AlignCenter().Element(CellStyle).Text("BOLIVARES").FontSize(7).Bold();
                          });



                      });

                });

                foreach (var item in ModelCuerpo)
                {


                    table.Cell().ColumnSpan(6).Row(row =>
                    {
                        row.ConstantItem(40).BorderVertical(1).AlignCenter().PaddingRight(3).Element(CellStyle).Text(item.Cantidad).FontSize(7);
                        row.ConstantItem(50).BorderVertical(1).AlignCenter().PaddingRight(3).Element(CellStyle).Text(item.DescripcionUdm).FontSize(8);
                        row.RelativeItem(3).BorderVertical(1).AlignLeft().PaddingLeft(10).Element(CellStyle).Text(item.DescripcionArticulo).FontSize(8);
                        var precio = item.PrecioUnitario.ToString("N", formato);
                        row.ConstantItem(60).BorderVertical(1).AlignRight().PaddingRight(3).Element(CellStyle).Text(precio).FontSize(7);
                        var totalBolivares = item.TotalBolivares.ToString("N", formato);
                        row.ConstantItem(70).BorderVertical(1).AlignRight().PaddingRight(3).Element(CellStyle).Text(totalBolivares).FontSize(7);


                    });

                }
                
                
                             int contador = 0;
                             var pucCompromisos = ModelEncabezado.PucCompromisos.ToList();
                
                                 foreach (var itemPuc in pucCompromisos)
                                 {
                                         contador++;
                                         
                                         table.Cell().ColumnSpan(6).BorderLeft(1).Row(row =>
                                         {
                                             row.ConstantItem(40).BorderVertical(1).Text("");
                                             row.ConstantItem(50).BorderVertical(1).Text("");
                                             row.ConstantItem(100).Text("");
                                             row.ConstantItem(100).PaddingLeft(5).Text("SE-PR-SP-PY-AC").FontSize(8).Bold().Underline();
                                             row.ConstantItem(100).Text("GR-PA-GE-ES-SE-EX").FontSize(8).Bold().Underline();
                                             row.ConstantItem(100).AlignCenter().PaddingRight(10).Text("Financiado Por").FontSize(7).Bold().Underline();
                                             row.ConstantItem(100).PaddingLeft(10).Text("Monto").FontSize(8).Bold().Underline();
                                             row.ConstantItem(102).Text("");
                                             row.ConstantItem(60).BorderVertical(1).Text("");
                                             row.ConstantItem(70).BorderVertical(1).Text("");
                                         });

                                         table.Cell().ColumnSpan(6).BorderLeft(1).Column(col =>
                                         {
                                             col.Item().Row(row =>
                                             {
                                                 row.RelativeItem().BorderVertical(1).Text("");
                                                 row.ConstantItem(50).BorderVertical(1).Text("");
                                                 row.ConstantItem(100).Text("");
                                                 row.ConstantItem(100).Column(col =>
                                                 {
                                                     col.Item().PaddingLeft(5).PaddingBottom(2).Text(itemPuc.CodigoIcpConcat).FontSize(9);
                                                     //col.Item().PaddingLeft(5).PaddingBottom(2).Text(icpConcat).FontSize(9);
                                                 });

                                                 row.ConstantItem(100).Column(col =>
                                                 {
                                                     col.Item().PaddingLeft(5).PaddingBottom(2).Text(itemPuc.CodigoPucConcat).FontSize(9);
                                                    
                                                 });

                                                 row.ConstantItem(100).Column(col =>
                                                 {
                                                     col.Item().Width(70).AlignCenter().PaddingLeft(15).PaddingBottom(2).Text(itemPuc.DescripcionFinanciado).FontSize(5);

                                                 });

                                                 row.ConstantItem(100).Column(col =>
                                                 {
                                                     col.Item().PaddingLeft(10).PaddingBottom(2).Text(itemPuc.Monto.ToString("N", formato)).FontSize(8);
                                                    
                                                 });
                                                 row.ConstantItem(102).Text("");
                                                 row.ConstantItem(60).BorderVertical(1).Text("");
                                                 row.ConstantItem(70).BorderVertical(1).Text("");

                                                 col.Item().ExtendVertical().Row(row =>
                                                 {
                                                     row.ConstantItem(40).ExtendVertical().BorderVertical(1);
                                                     row.ConstantItem(50).ExtendVertical().BorderVertical(1);
                                                     row.RelativeItem(3).ExtendVertical().BorderVertical(1);
                                                     row.ConstantItem(60).ExtendVertical().BorderVertical(1);
                                                     row.ConstantItem(70).ExtendVertical().BorderVertical(1);
                                                 });
                                             });

                                         });






                                 

                             }
                             

              


            });





     
        }
       
    }
}
