using System.Globalization;
using Convertidor.Dtos.Presupuesto.ReporteSolicitudModificacion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
namespace Convertidor.Services.Rh.Report.Example;

  public class DetalleComponent : IComponent
  {
    private readonly List<DetalleReporteSolicitudModificacionDto> ModelDetalle;

    public DetalleComponent(List<DetalleReporteSolicitudModificacionDto> modelDetalle)
    {
        ModelDetalle = modelDetalle;
    }
            
        public void Compose(IContainer container)
        {
            TextDescriptor text = new TextDescriptor();
            
            var headerStyle = TextStyle.Default.SemiBold().FontSize(7);
            NumberFormatInfo formato = new CultureInfo("es-AR").NumberFormat;
            static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
            formato.CurrencyGroupSeparator = ".";
            formato.NumberDecimalSeparator = ",";
            formato.NumberDecimalDigits = 2;
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(260);
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.ConstantColumn(300);
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
            });

            table.Header(header =>
            {
                header.Cell().Border(1).AlignCenter().Element(CellStyle).Text("CONCEPTO DE LO PRESUPUESTADO").FontSize(7);
                header.Cell().ColumnSpan(2).Border(1).AlignCenter().Element(CellStyle).Text("IMPUTACION PRESUPUESTARIA").FontSize(7);
                header.Cell().Border(1).AlignCenter().Element(CellStyle).Text("MONTO (Bs)").FontSize(7);

            });

            foreach (var item in ModelDetalle)
            {
                
                table.Cell().Column(column => 
                {
                    column.Item().Border(1).AlignCenter().Element(CellStyle).Text(item.DenominacionIcp).FontSize(7);

                    column.Item().Border(1).AlignCenter().Element(CellStyle).Text(item.DenominacionPuc).FontSize(7);
                  
                });
                
                

                table.Cell().Border(1).AlignCenter().Element(CellStyle).Text(item.CodigoIcpConcat).FontSize(7);
                table.Cell().Border(1).AlignCenter().Element(CellStyle).Text(item.CodigoPucConcat).FontSize(7);
                table.Cell().Border(1).AlignCenter().Element(CellStyle).Text(item.Monto).FontSize(7);

              

            }


        });

        }
  }
