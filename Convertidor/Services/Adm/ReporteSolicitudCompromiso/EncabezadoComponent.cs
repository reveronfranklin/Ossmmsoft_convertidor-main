using Convertidor.Dtos.Adm.ReporteSolicitudCompromiso;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

namespace Convertidor.Services.Adm.ReporteSolicitudCompromiso
{
    public class EncabezadoComponent : IComponent
    {
        public EncabezadoReporteDto ModelEncabezado;

 
        public EncabezadoComponent(EncabezadoReporteDto modelEncabezado)
        {
            ModelEncabezado = modelEncabezado;
        }

        public void Compose(IContainer container)
        {
            var headerStyle = TextStyle.Default.SemiBold().FontSize(7);
            NumberFormatInfo formato = new CultureInfo("es-AR").NumberFormat;

            formato.CurrencyGroupSeparator = ".";
            formato.NumberDecimalSeparator = ",";
            formato.NumberDecimalDigits = 2;

            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.ConstantColumn(55);
                    columns.ConstantColumn(35);

                });

                table.Header(header =>
                {
                    header.Cell().ColumnSpan(4);
                    header.Cell().ColumnSpan(2).Column(col =>
                    {
                         col.Item().Border(1).AlignCenter().PaddingRight(2).Text("N° Solicitud").Style(headerStyle);
                         col.Item().Border(1).AlignCenter().PaddingRight(2).Element(CellStyle).Text(ModelEncabezado.NumeroSolicitud).FontSize(7);
                         col.Item().Border(1).AlignCenter().Text("FECHA").Style(headerStyle);
                         col.Item().Border(1).ScaleToFit().AlignCenter().PaddingLeft(10).Element(CellStyle).Text(ModelEncabezado.FechaSolicitud.ToShortDateString()).FontSize(7);
                    });
                    
                   

                });


                table.Cell().ColumnSpan(6).Column(col =>
                {
                    col.Item().Border(1).AlignLeft().PaddingLeft(4).Text("PARA: ").Style(headerStyle);
                    col.Item().Border(1).AlignLeft().PaddingLeft(4).Text(ModelEncabezado.Denominacion).Style(headerStyle);
                    col.Item().Border(1).AlignLeft().PaddingLeft(4).Text("SOLICITANTE: ").Style(headerStyle);
                    col.Item().Border(1).AlignLeft().PaddingLeft(4).Text(ModelEncabezado.UnidadEjecutora).Style(headerStyle);
                    col.Item().Border(1).AlignLeft().PaddingLeft(4).Text("PROVEEDOR: ").Style(headerStyle);
                    col.Item().Border(1).AlignLeft().PaddingLeft(4).Text(ModelEncabezado.DireccionProveedor).Style(headerStyle);

                });

                


                static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2);



            });
        }


    }
}  

