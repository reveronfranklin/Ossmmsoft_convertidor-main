using Convertidor.Dtos.Adm.ReporteSolicitudCompromiso;
using Convertidor.Dtos.Presupuesto;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using NPOI.HSSF.Record;
using NPOI.SS.Formula.Functions;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;
using System.Linq;

namespace Convertidor.Services.Adm.ReporteSolicitudCompromiso
{
    public class CuerpoComponent : IComponent
    {
        public List<CuerpoReporteDto> ModelCuerpo;

        public CuerpoComponent(List<CuerpoReporteDto> modelCuerpo)
        {
            ModelCuerpo = modelCuerpo;
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

                      header.Cell().ColumnSpan(6).Row(row =>
                      {
                        row.ConstantItem(50).Border(1).AlignCenter().Element(CellStyle).Text("Cantidad").FontSize(7).SemiBold();
                        row.ConstantItem(50).Border(1).AlignCenter().Element(CellStyle).Text("Unidad de Medida").FontSize(7).SemiBold();
                        row.RelativeItem(3).Border(1).AlignCenter().Element(CellStyle).Text("Descripcion").FontSize(7).SemiBold();
                        row.ConstantItem(100).Border(1).AlignCenter().Element(CellStyle).Text("Precio \nUnitario").FontSize(7).SemiBold();
                        row.ConstantItem(100).Border(1).AlignCenter().Element(CellStyle).Text("Total \nBolivares").FontSize(7).SemiBold();

                      });

                });

                var motivo = "";

                foreach (var item in ModelCuerpo)
                {
                    motivo = item.Motivo;

                    table.Cell().ColumnSpan(6).Row(row =>
                    {
                        row.ConstantItem(50).Border(1).AlignCenter().Element(CellStyle).Text(item.Cantidad).FontSize(7);
                        row.ConstantItem(50).Border(1).AlignCenter().Element(CellStyle).Text(item.DescripcionUdmId).FontSize(7);
                        row.RelativeItem(3).Border(1).AlignCenter().Element(CellStyle).Text(item.DescripcionArticulo).FontSize(7);
                        row.ConstantItem(100).Border(1).AlignCenter().Element(CellStyle).Text(item.PrecioUnitario).FontSize(7);
                        row.ConstantItem(100).Border(1).AlignCenter().Element(CellStyle).Text(item.TotalBolivares).FontSize(7);



                    });

                }

        
                table.Footer(footer =>
                {

                    footer.Cell().ColumnSpan(6).BorderVertical(1).PaddingTop(300);
                    footer.Cell().ColumnSpan(6).BorderVertical(1).BorderBottom(1).PaddingBottom(300);
                    footer.Cell().ColumnSpan(4).BorderLeft(1).BorderBottom(1).BorderTop(1).PaddingLeft(5).AlignLeft().Text("MONTO TOTAL EN LETRA").FontSize(8).SemiBold();
                    

                    footer.Cell().Column(col =>
                    {
                        
                        col.Item().BorderTop(1).Width(100).AlignRight().Text("SUBTOTAL").FontSize(8).SemiBold();
                        col.Item().Width(100).AlignRight().Text("16%    " + "  IVA").FontSize(8).SemiBold();
                        col.Item().Width(100).AlignRight().Text("TOTAL").FontSize(8).SemiBold();
                       

                    });

                    
                     var bolivares = ModelCuerpo.Sum(x => x.TotalBolivares);
                     var montoImpuesto = bolivares * Convert.ToDecimal(0.16);
                     var total = bolivares + montoImpuesto;

                    var totalBolivares = bolivares.ToString("N",formato);
                    var totalImpuesto =  montoImpuesto.ToString("N", formato);
                    var totales = total.ToString("N", formato);

                    footer.Cell().Column(col =>
                    {
                        
                        col.Item().Width(100).Border(1).AlignCenter().Element(CellStyle).Padding(1).Text(bolivares).FontSize(7);
                        col.Item().Width(100).Border(1).AlignCenter().Element(CellStyle).Padding(1).Text(montoImpuesto).FontSize(7);
                        col.Item().Width(100).Border(1).AlignCenter().Element(CellStyle).BorderBottom(1).Padding(1).Text(totales).FontSize(7);


                    });

                    footer.Cell().ColumnSpan(6).Column(col =>
                    {
                        col.Item().BorderVertical(1).BorderTop(1).PaddingLeft(3).Text("Motivo  :" ).FontSize(8).SemiBold();
                        col.Item().BorderVertical(1).PaddingLeft(3).Text(motivo).FontSize(7).SemiBold();
                    });

                    footer.Cell().ColumnSpan(6).Row(row =>
                    {
                        row.RelativeItem().Border(1).AlignTop().Padding(15).PaddingLeft(8).Text($"Elaborado Por :       ").FontSize(8).SemiBold();
                        row.RelativeItem().Border(1).AlignTop().Padding(15).PaddingLeft(8).Text($"Revisado Por :       ").FontSize(8).SemiBold();
                        row.RelativeItem().Border(1).AlignTop().Padding(15).PaddingLeft(8).Text($"Confirmado Por :       ").FontSize(8).SemiBold();
                    });
                });

             });

   

            
        }

       
    }
}
