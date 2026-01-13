using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Presupuesto.ReporteCompromisoPresupuestario;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using NPOI.SS.Formula.Functions;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;
using MathNet.Numerics;

namespace Convertidor.Services.Presupuesto.ReporteOrdenSercicioPresupuestario
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

            int contador = 1;
            var headerStyle = TextStyle.Default.SemiBold().FontSize(11).Fallback();
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

                          row.ConstantItem(75).Border(1).AlignCenter().Element(CellStyle).Text("CANTIDAD").FontSize(11).Bold();
                          row.ConstantItem(90).Border(1).AlignCenter().Element(CellStyle).Text("   UNIDAD\n" + "DE MEDIDA").FontSize(11).Bold();
                          row.ConstantItem(495).Border(1).AlignCenter().Element(CellStyle).PaddingLeft(50).Text("DESCRIPCION").FontSize(11).Bold();
                          row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text("   PRECIO\n" + "UNITARIO").FontSize(11).Bold();
                          
                          row.RelativeItem().Border(1).BorderBottom(1).AlignCenter().Element(CellStyle).Text("   TOTAL\n" + "BOLIVARES").FontSize(11).Bold();



                      });

                });
                int totalLongitudLineas = 0;
                foreach (var item in ModelCuerpo)
                {
                    contador++;
                    if (item.DescripcionArticulo.Length < 75)
                    {
                        totalLongitudLineas = totalLongitudLineas +1;
                    }
                    else
                    {
                        int lineasPorTexto = (int)item.DescripcionArticulo.Trim().Length/75;
                    
                        totalLongitudLineas = totalLongitudLineas+lineasPorTexto;
                    }
                    
                    
                    table.Cell().ColumnSpan(6).Row(row =>
                    {
                        row.ConstantItem(75).BorderVertical(1).AlignCenter().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text(item.Cantidad).FontSize(11);
                        row.ConstantItem(90).BorderVertical(1).AlignCenter().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text(item.DescripcionUdm).FontSize(11);
                        row.ConstantItem(495).BorderVertical(1).AlignLeft().PaddingLeft(3).PaddingTop(3).Element(CellStyle).Text(item.DescripcionArticulo).FontSize(11);
                        var precio = item.PrecioUnitario.ToString("N", formato);
                        row.RelativeItem().BorderVertical(1).AlignRight().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text(precio).FontSize(11);
                        var totalBolivares = item.TotalBolivares.ToString("N", formato);
                        row.RelativeItem().BorderVertical(1).AlignRight().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text(totalBolivares).FontSize(11);
                        
                    });

                }
                
            
                var lineasImpresas = totalLongitudLineas;
                var lineas = 0;
                int canidadMaxima = 32;

                if (canidadMaxima - lineasImpresas <= 0)
                {
                    lineas = canidadMaxima;
                }
                else
                {
                   lineas =  canidadMaxima-lineasImpresas;
                }
                   
                for (int i = 0; i < lineas; i++)
                {
                    table.Cell().ColumnSpan(6).Row(row =>
                    {
                        row.ConstantItem(75).BorderVertical(1).AlignCenter().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text("").FontSize(11);
                        row.ConstantItem(90).BorderVertical(1).AlignCenter().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text("").FontSize(11);
                        row.ConstantItem(495).BorderVertical(1).AlignLeft().PaddingLeft(3).PaddingTop(3).Element(CellStyle).Text("").FontSize(11);
                        row.RelativeItem().BorderVertical(1).AlignRight().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text("").FontSize(11);
                        row.RelativeItem().BorderVertical(1).AlignRight().PaddingRight(3).PaddingTop(3).Element(CellStyle).Text("").FontSize(11);
                        
                    });

                }
                
                
             

            });





     
        }
       
    }
}
