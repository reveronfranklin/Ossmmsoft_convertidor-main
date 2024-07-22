using Convertidor.Dtos.Adm.ReporteSolicitudCompromiso;
using Convertidor.Dtos.Presupuesto;
using NPOI.HSSF.Record;
using NPOI.SS.Formula.Functions;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

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
                

                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(100);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.ExtendLastCellsToTableBottom();

                table.Header(header =>
                {
                    

                    

                    header.Cell().ColumnSpan(6).Row(row =>
                    {
                        row.ConstantItem(50).Border(1).AlignCenter().Element(CellStyle).Text("Cantidad").FontSize(7).SemiBold();
                        row.ConstantItem(50).Border(1).AlignCenter().Element(CellStyle).Text("Unidad de Medida").FontSize(7).SemiBold();
                        row.RelativeItem(3).Border(1).AlignCenter().Element(CellStyle).Text("Descripcion").FontSize(7).SemiBold();
                        row.ConstantItem(100).Border(1).AlignCenter().Element(CellStyle).Text("Precio \nUnitario").FontSize(7).SemiBold();
                        row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text("Total \nBolivares").FontSize(7).SemiBold();

                    });


                    foreach (var item in ModelCuerpo)
                    {
                        table.Cell().ColumnSpan(6).Row(row =>
                        {
                            row.ConstantItem(50).Border(1).AlignCenter().Element(CellStyle).Text(item.Cantidad).FontSize(7);
                            row.ConstantItem(50).Border(1).AlignCenter().Element(CellStyle).Text(item.DescripcionUdmId).FontSize(7);
                            row.RelativeItem(3).Border(1).AlignCenter().Element(CellStyle).Text(item.DescripcionArticulo).FontSize(7);
                            row.ConstantItem(100).Border(1).AlignCenter().Element(CellStyle).Text(item.PrecioUnitario).FontSize(7);
                            row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text(item.TotalBolivares).FontSize(7);
                            
                            
                          
                        });

                        
                    }

                    

                    table.Footer(footer =>
                    {
                        

                       footer.Cell().ColumnSpan(6).RowSpan(10).BorderRight(1).BorderLeft(1).BorderTop(1).BorderBottom(1);
                       footer.Cell().ColumnSpan(4).BorderLeft(1).BorderBottom(1).AlignLeft().Text("MONTO TOTAL EN LETRA").FontSize(8).SemiBold();
                       
                        footer.Cell().Column(col =>
                        {
                            
                            col.Item().BorderRight(1).Width(120).AlignRight().Text("SUBTOTAL").FontSize(8).SemiBold();
                            col.Item().BorderRight(1).Width(120).AlignRight().Text("16%    "+"  IVA").FontSize(8).SemiBold();
                            col.Item().BorderRight(1).Width(120).AlignRight().Text("TOTAL").FontSize(8).SemiBold();
                        });

                        
                        footer.Cell().ColumnSpan(5).Border(1).AlignCenter().Element(CellStyle).Text("");
                        footer.Cell().Column(col =>
                        {
                            col.Item().Border(1).AlignCenter().Element(CellStyle).Text(ModelCuerpo.Sum(x => x.TotalBolivares * x.Cantidad)).FontSize(7);
                            col.Item().Border(1).AlignCenter().Element(CellStyle).Text(ModelCuerpo.Sum(x => x.TotalMontoImpuesto)).FontSize(7);
                            col.Item().Border(1).AlignCenter().Element(CellStyle).Text(ModelCuerpo.Sum(x => x.Total)).FontSize(7);

                        });
                       
                    });

                });
                    
            });
        }
    }
}
