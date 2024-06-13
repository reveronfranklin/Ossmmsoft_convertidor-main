using Convertidor.Dtos.Adm.ReporteSolicitudCompromiso;
using MimeKit;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;
namespace Convertidor.Services.Adm.Reports.ReporteSolicitudCompromiso;

public class SolicitudCompromisoComponent : IComponent
    {
        //private string Title { get; }
       
        public SolicitudcompromisoDto ModelSolicitud { get; }
       
        public SolicitudCompromisoComponent( SolicitudcompromisoDto modelSolicitud)
        {
        
          ModelSolicitud = modelSolicitud;
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
                    columns.ConstantColumn(55);
                    columns.ConstantColumn(35);
                    
                });
                
                table.Header(header =>
                {
                    header.Cell().ColumnSpan(3);
                    header.Cell().Border(1).AlignCenter().PaddingRight(2).Text("N° Solicitud Compromiso").Style(headerStyle);
                   
                 
                    
                });

                table.Cell().Border(1).AlignCenter().Text("Fecha Compromiso").Style(headerStyle);

                table.Cell().ColumnSpan(3);
                table.Cell().Border(1).AlignCenter().PaddingRight(2).Element(CellStyle).Text(ModelSolicitud.NumeroSolicitud).FontSize(7);
                //table.Cell().Border(1).ScaleToFit().AlignLeft().PaddingLeft(5).Element(CellStyle).Text(ModelGeneral.NumeroSolicitud).FontSize(7);
                //table.Cell().ColumnSpan(2).Border(1).AlignCenter().Element(CellStyle).Text(ModelGeneral.FechaSolicitud.ToShortDateString()).FontSize(7);
                //table.Cell().ColumnSpan(8).Border(1).PaddingLeft(5).Element(CellStyle).Text(ModelGeneral.Motivo).FontSize(7);
                //table.Cell().ColumnSpan(8).Border(1).AlignCenter().Element(CellStyle).Text("BASE DE CALCULO").FontSize(8);

                static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                   


            });
        }
    }
    
    