using Convertidor.Dtos.Adm.ReporteSolicitudCompromiso;
using NPOI.SS.Formula.Functions;
using QuestPDF.Fluent;
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
            var headerStyle = TextStyle.Default.SemiBold().FontSize(7);
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
                });

                table.Header(header =>
                {
                    table.ExtendLastCellsToTableBottom();

                    

                    header.Cell().ColumnSpan(4).Row(row =>
                    {
                        row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text("Cantidad").FontSize(7);
                        row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text("Unidad de Medida").FontSize(7);
                        row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text("Descripcion").FontSize(7);
                        row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text("Precio \nUnitario").FontSize(7);
                        
                    });

                    header.Cell().Column(col =>
                    {
                        col.Item().Border(1).AlignCenter().Element(CellStyle).Text("Total \nBolivares").FontSize(7);
                        
                    });

                    foreach (var item in ModelCuerpo)
                    {
                        table.Cell().ColumnSpan(5).Row(row =>
                        {
                            row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text(item.Cantidad).FontSize(7);
                            row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text(item.DescripcionUdmId).FontSize(7);
                            row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text(item.DescripcionArticulo).FontSize(7);
                            row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text(item.PrecioUnitario).FontSize(7);
                            row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text(item.TotalBolivares).FontSize(7);
                        });
                      

                        
                    }

                   
                });
            });
        }
    }
}
