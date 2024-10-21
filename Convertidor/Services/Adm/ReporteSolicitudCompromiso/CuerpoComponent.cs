using Convertidor.Dtos.Adm.ReporteSolicitudCompromiso;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

namespace Convertidor.Services.Adm.ReporteSolicitudCompromiso
{
    public class CuerpoComponent : IComponent
    {
        public List<CuerpoReporteDto> ModelCuerpo;
        public  EncabezadoReporteDto ModelEncabezado;

        public CuerpoComponent(List<CuerpoReporteDto> modelCuerpo, EncabezadoReporteDto modelEncabezado)
        {
            ModelCuerpo = modelCuerpo;
            ModelEncabezado = modelEncabezado;
        }

        public void Compose(IContainer container)
        {
            QuestPDF.Settings.CheckIfAllTextGlyphsAreAvailable = false;


            var headerStyle = TextStyle.Default.SemiBold().FontSize(11).Fallback();
            NumberFormatInfo formato = new CultureInfo("es-AR").NumberFormat;
            static IContainer CellStyle(IContainer container) => container;
            formato.CurrencyGroupSeparator = ".";
            formato.NumberDecimalSeparator = ",";
            formato.NumberDecimalDigits = 2;


            container.Table(table =>
            {
               // table.ExtendLastCellsToTableBottom();


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
                      header.Cell().ColumnSpan(6).Background(Colors.Grey.Lighten2).Border(1).AlignCenter().Text("DETALLES DE SOLICITUD").FontSize(12).Bold();
                      header.Cell().ColumnSpan(6).Row(row =>
                      {

                          row.ConstantItem(75).Border(1).AlignCenter().Element(CellStyle).Text("CANTIDAD").FontSize(11).Bold();
                          row.ConstantItem(75).Border(1).AlignCenter().Element(CellStyle).Text("   UNIDAD\n" + "DE MEDIDA").FontSize(11).Bold();
                          row.ConstantItem(400).Border(1).AlignCenter().Element(CellStyle).PaddingLeft(50).Text("DESCRIPCION").FontSize(11).Bold();
                          row.ConstantItem(120).Border(1).AlignCenter().Element(CellStyle).Text("   PRECIO\n" + "UNITARIO").FontSize(11).Bold();
                          
                          row.ConstantItem(150).Border(0).BorderBottom(1).AlignCenter().Element(CellStyle).Text("   TOTAL\n" + "BOLIVARES").FontSize(11).Bold();
                          /*row.ConstantItem(125).Border(1).Column(col =>
                          {
                              col.Item().BorderRight(0).AlignCenter().Element(CellStyle).Text("TOTAL").FontSize(11).Bold();
                              col.Item().BorderRight(0).AlignCenter().Element(CellStyle).Text("BOLIVARES").FontSize(11).Bold();
                          });*/

                      });

                });

                int contador = 0;



                foreach (var item in ModelCuerpo)
                {
                    contador++;

              
                    
                    table.Cell().ColumnSpan(6).Row(row =>
                    {

                    row.ConstantItem(75).BorderVertical(1).AlignCenter().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text(item.Cantidad).FontSize(11);
                    row.ConstantItem(75).BorderVertical(1).AlignCenter().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text(item.DescripcionUdmId).FontSize(11);
                    row.ConstantItem(400).BorderVertical(1).AlignLeft().PaddingLeft(3).PaddingTop(3).Element(CellStyle).Text(item.DescripcionArticulo).FontSize(11);
                    var precio = item.PrecioUnitario.ToString("N", formato);
                    row.ConstantItem(120).BorderVertical(1).AlignRight().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text(precio).FontSize(11);
                    var totalBolivares = item.TotalBolivares.ToString("N", formato);
                    row.ConstantItem(120).BorderVertical(0).AlignRight().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text(totalBolivares).FontSize(11);

                     /*if (contador == ModelCuerpo.LongCount())
                     {

                            table.Cell().ColumnSpan(6).ExtendVertical().Column(col =>
                            {

                                col.Item().ExtendVertical().Row(row =>
                                {
                                    row.ConstantItem(75).ExtendVertical().BorderVertical(1);
                                    row.ConstantItem(75).ExtendVertical().BorderVertical(1);
                                    row.ConstantItem(465).ExtendVertical().BorderVertical(1);
                                    row.ConstantItem(100).ExtendVertical().BorderVertical(1);
                                    row.ConstantItem(100).ExtendVertical().BorderVertical(1);
                                });

                            });

                        }*/
                     
                    });

                    if (contador == 17)
                    {
                        table.Cell().ColumnSpan(6).Row(row =>
                        {
                            row.ConstantItem(75).BorderVertical(1).AlignCenter().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text("").FontSize(11);
                            row.ConstantItem(75).BorderVertical(1).AlignCenter().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text("").FontSize(11);
                            row.ConstantItem(400).BorderVertical(1).AlignLeft().PaddingLeft(3).PaddingTop(3).Element(CellStyle).Text($"").FontSize(11);
                            row.ConstantItem(120).BorderVertical(1).AlignRight().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text("").FontSize(11);
                            row.ConstantItem(120).BorderVertical(0).AlignRight().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text("").FontSize(11);
                            
                     
                        });
                        table.Cell().ColumnSpan(6).Row(row =>
                        {
                            row.ConstantItem(75).BorderVertical(1).AlignCenter().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text("").FontSize(11);
                            row.ConstantItem(75).BorderVertical(1).AlignCenter().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text("").FontSize(11);
                            row.ConstantItem(400).BorderVertical(1).AlignLeft().PaddingLeft(3).PaddingTop(3).Element(CellStyle).Text($"").FontSize(11);
                            row.ConstantItem(120).BorderVertical(1).AlignRight().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text("").FontSize(11);
                            row.ConstantItem(120).BorderVertical(0).AlignRight().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text("").FontSize(11);
                            
                     
                        });
                        table.Cell().ColumnSpan(6).Row(row =>
                        {
                            row.ConstantItem(75).BorderVertical(1).AlignCenter().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text("").FontSize(11);
                            row.ConstantItem(75).BorderVertical(1).AlignCenter().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text("").FontSize(11);
                            row.ConstantItem(400).BorderVertical(1).AlignLeft().PaddingLeft(3).PaddingTop(3).Element(CellStyle).Text($"").FontSize(11);
                            row.ConstantItem(120).BorderVertical(1).AlignRight().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text("").FontSize(11);
                            row.ConstantItem(120).BorderVertical(0).AlignRight().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text("").FontSize(11);
                            
                     
                        });
                        table.Cell().ColumnSpan(6).Row(row =>
                        {
                            row.ConstantItem(75).BorderVertical(1).AlignCenter().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text("").FontSize(11);
                            row.ConstantItem(75).BorderVertical(1).AlignCenter().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text("").FontSize(11);
                            row.ConstantItem(400).BorderVertical(1).AlignLeft().PaddingLeft(3).PaddingTop(3).Element(CellStyle).Text($"").FontSize(11);
                            row.ConstantItem(120).BorderVertical(1).AlignRight().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text("").FontSize(11);
                            row.ConstantItem(120).BorderVertical(0).AlignRight().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text("").FontSize(11);
                            
                     
                        });
                        table.Cell().ColumnSpan(6).Row(row =>
                        {
                            row.ConstantItem(75).BorderVertical(1).AlignCenter().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text("").FontSize(11);
                            row.ConstantItem(75).BorderVertical(1).AlignCenter().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text("").FontSize(11);
                            row.ConstantItem(400).BorderVertical(1).AlignLeft().PaddingLeft(3).PaddingTop(3).Element(CellStyle).Text($"").FontSize(11);
                            row.ConstantItem(120).BorderVertical(1).AlignRight().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text("").FontSize(11);
                            row.ConstantItem(120).BorderVertical(0).AlignRight().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text("").FontSize(11);
                            
                     
                        });
                    }

                   
                }






            });


        }
            
        

       
    }
}
