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


            var headerStyle = TextStyle.Default.SemiBold().FontSize(10).Fallback();
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
                      header.Cell().ColumnSpan(6).Background(Colors.Grey.Lighten2).Border(1).AlignCenter().Text("DETALLES DE SOLICITUD").FontSize(12).Bold();
                      header.Cell().ColumnSpan(6).Row(row =>
                      {

                          row.ConstantItem(75).Border(1).AlignCenter().Element(CellStyle).Text("CANTIDAD").FontSize(10).Bold();
                          row.ConstantItem(75).Border(1).AlignCenter().Element(CellStyle).Text("   UNIDAD\n" + "DE MEDIDA").FontSize(10).Bold();
                          row.RelativeItem(3).Border(1).AlignCenter().Element(CellStyle).PaddingLeft(50).Text("DESCRIPCION").FontSize(10).Bold();
                          row.ConstantItem(100).Border(1).AlignCenter().Element(CellStyle).Text("   PRECIO\n" + "UNITARIO").FontSize(10).Bold();
                          row.ConstantItem(100).Column(col =>
                          {
                              col.Item().Border(1).AlignCenter().Element(CellStyle).Text("TOTAL").FontSize(10).Bold();
                              col.Item().Border(1).AlignCenter().Element(CellStyle).Text("BOLIVARES").FontSize(10).Bold();
                          });

                      });

                });

                int contador = 0;



                foreach (var item in ModelCuerpo)
                {
                    contador++;

                    table.Cell().ColumnSpan(6).Row(row =>
                    {

                    row.ConstantItem(75).BorderVertical(1).AlignCenter().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text(item.Cantidad).FontSize(10);
                    row.ConstantItem(75).BorderVertical(1).AlignCenter().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text(item.DescripcionUdmId).FontSize(10);
                    row.RelativeItem(3).BorderVertical(1).AlignLeft().PaddingLeft(10).PaddingTop(3).Element(CellStyle).Text(item.DescripcionArticulo).FontSize(10);
                    var precio = item.PrecioUnitario.ToString("N", formato);
                    row.ConstantItem(100).BorderVertical(1).AlignRight().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text(precio).FontSize(10);
                    var totalBolivares = item.TotalBolivares.ToString("N", formato);
                    row.ConstantItem(100).BorderVertical(1).AlignRight().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text(totalBolivares).FontSize(10);

                     if (contador == ModelCuerpo.LongCount())
                     {

                        table.Cell().ColumnSpan(6).ExtendVertical().Column(col =>
                        {

                            col.Item().ExtendVertical().Row(row =>
                            {
                                row.ConstantItem(75).ExtendVertical().BorderVertical(1);
                                row.ConstantItem(75).ExtendVertical().BorderVertical(1);
                                row.RelativeItem(3).ExtendVertical().BorderVertical(1);
                                row.ConstantItem(100).ExtendVertical().BorderVertical(1);
                                row.ConstantItem(100).ExtendVertical().BorderVertical(1);
                            });

                        });

                    }
                     
                    });

                   

                   
                }






            });


        }
            
        

       
    }
}
