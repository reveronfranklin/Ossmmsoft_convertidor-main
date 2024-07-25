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
using Convertidor.Dtos.Adm;

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
                        row.ConstantItem(50).Border(1).AlignCenter().Element(CellStyle).Text("CANTIDAD").FontSize(7).Bold();
                        row.ConstantItem(50).Border(1).AlignCenter().Element(CellStyle).Text("UNIDAD DE MEDIDA").FontSize(7).Bold();
                        row.RelativeItem(3).Border(1).AlignCenter().Element(CellStyle).Text("DESCRIPCION").FontSize(7).Bold();
                        row.ConstantItem(100).Border(1).AlignCenter().Element(CellStyle).Text("   PRECIO\n"+ "UNITARIO").FontSize(7).Bold();
                        row.ConstantItem(100).Border(1).AlignCenter().Element(CellStyle).Text("   TOTAL\n"+"BOLIVARES").FontSize(7).Bold();


                      });

                });

              
                foreach (var item in ModelCuerpo)
                {
                

                    table.Cell().ColumnSpan(6).Row(row =>
                    {
                        row.ConstantItem(50).BorderVertical(1).AlignCenter().Element(CellStyle).Text(item.Cantidad).FontSize(7);
                        row.ConstantItem(50).BorderVertical(1).AlignCenter().Element(CellStyle).Text(item.DescripcionUdmId).FontSize(7);
                        row.RelativeItem(3).BorderVertical(1).AlignCenter().Element(CellStyle).Text(item.DescripcionArticulo).FontSize(7);
                        row.ConstantItem(100).BorderVertical(1).AlignCenter().Element(CellStyle).Text(item.PrecioUnitario).FontSize(7);
                        row.ConstantItem(100).BorderVertical(1).AlignCenter().Element(CellStyle).Text(item.TotalBolivares).FontSize(7);


                    });

                }

        
                table.Footer(footer =>
                {

                    footer.Cell().ColumnSpan(6).BorderVertical(1).Row(row =>
                    {
                        row.ConstantItem(50).BorderVertical(1).PaddingBottom(600);
                        row.ConstantItem(50).BorderVertical(1).PaddingBottom(600); ;
                        row.RelativeItem().BorderVertical(1).PaddingBottom(600); ;
                        row.ConstantItem(100).BorderVertical(1).PaddingBottom(600); ;
                        row.ConstantItem(100).BorderVertical(1).PaddingBottom(600); ;
                    });

                    footer.Cell().ColumnSpan(4).Column(col =>
                    {
                       col.Item().BorderLeft(1).BorderTop(1).PaddingLeft(5).PaddingBottom(6).AlignLeft().Text("MONTO TOTAL EN LETRA :").FontSize(8).Bold();
                       col.Item().BorderLeft(1).PaddingLeft(5).PaddingBottom(6).AlignLeft().Text($"{ModelEncabezado.MontoLetras.ToUpper()}").FontSize(8);
                    });
                    

                    footer.Cell().Column(col =>
                    {
                        
                        col.Item().BorderTop(1).BorderLeft(1).Width(100).AlignRight().PaddingRight(3).Text("SUBTOTAL").FontSize(8).Bold();
                        col.Item().Width(100).BorderLeft(1).AlignRight().PaddingRight(3).Text("16%    " + "  IVA").FontSize(8).Bold();
                        col.Item().Width(100).BorderLeft(1).AlignRight().PaddingRight(3).Text("TOTAL").FontSize(8).Bold();
                       

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
                        col.Item().BorderVertical(1).BorderTop(1).PaddingLeft(3).Text("MOTIVO  :" ).FontSize(8).Bold();
                        col.Item().BorderVertical(1).PaddingLeft(3).Text(ModelEncabezado.Motivo).FontSize(7);
                    });

                    footer.Cell().ColumnSpan(6).Row(row =>
                    {
                        row.RelativeItem().BorderVertical(1).BorderTop(1).AlignTop().AlignCenter().AlignRight().PaddingRight(15).Text($"Elaborado Por :       ").FontSize(8).Bold();
                        row.RelativeItem().BorderVertical(1).BorderTop(1).AlignTop().AlignCenter().Padding(3).PaddingLeft(8).Text($"Revisado Por :       ").FontSize(8).Bold();
                        row.RelativeItem().BorderVertical(1).BorderTop(1).AlignTop().AlignCenter().Padding(3).PaddingLeft(8).Text($"Confirmado Por :       ").FontSize(8).Bold();

                        
                    });

                    footer.Cell().ColumnSpan(6).Row(row =>
                    {
                        row.RelativeItem().BorderVertical(1).BorderBottom(1).AlignTop().AlignLeft().Padding(3).PaddingLeft(8).PaddingBottom(3).Text($"{ModelEncabezado.Firmante}  \n FIRMA: ________________________________________________________________________").FontSize(8).SemiBold();
                        row.RelativeItem().BorderVertical(1).BorderBottom(1).AlignTop().AlignCenter().Padding(3).PaddingLeft(8).PaddingBottom(3).Text($"         ").FontSize(8).SemiBold();
                        row.RelativeItem().BorderVertical(1).BorderBottom(1).AlignTop().AlignCenter().Padding(3).PaddingLeft(8).PaddingBottom(3).Text($"DIRECCION DE ADMINISTRACION Y FINANZAS").FontSize(8).Bold();
                     ;
  


                    });


                });

             });

   

            
        }

       
    }
}
