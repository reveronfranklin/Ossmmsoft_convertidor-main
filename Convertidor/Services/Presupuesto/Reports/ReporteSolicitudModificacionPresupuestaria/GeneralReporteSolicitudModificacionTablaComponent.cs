using System.Globalization;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Presupuesto.ReporteSolicitudModificacion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
namespace Convertidor.Services.Presupuesto.Reports.ReporteSolicitudModificacionPresupuestaria;

    public class GeneralReporteSolicitudModificacionTablaComponent : IComponent
    {
        //private string Title { get; }
       
        public  GeneralReporteSolicitudModificacionDto  ModelGeneral { get; }
       
        public GeneralReporteSolicitudModificacionTablaComponent( GeneralReporteSolicitudModificacionDto modelGeneral)
        {
        
          ModelGeneral = modelGeneral;
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
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.ConstantColumn(55);
                    columns.ConstantColumn(35);
                    
                });
                
                table.Header(header =>
                {
                    header.Cell().ColumnSpan(4);
                    header.Cell().Border(1).AlignCenter().PaddingRight(2).Text("UNIDAD SOLICITANTE").Style(headerStyle);
                    header.Cell().Border(1).AlignLeft().PaddingLeft(5).Text("NRO").Style(headerStyle);
                    header.Cell().ColumnSpan(2).Border(1).AlignCenter().Text("FECHA").Style(headerStyle);
                   
                });


                    table.Cell().ColumnSpan(4);
                    table.Cell().Border(1).AlignCenter().PaddingRight(2).Element(CellStyle).Text(ModelGeneral.CodigoSolicitante).FontSize(7);
                    table.Cell().Border(1).ScaleToFit().AlignLeft().PaddingLeft(5).Element(CellStyle).Text(ModelGeneral.NumeroSolModificacion).FontSize(7);
                    table.Cell().ColumnSpan(2).Border(1).AlignCenter().Element(CellStyle).Text(ModelGeneral.FechaSolicitud.ToShortDateString()).FontSize(7);
                    table.Cell().ColumnSpan(8).Border(1).PaddingLeft(5).Element(CellStyle).Text(ModelGeneral.Motivo).FontSize(7);
                    table.Cell().ColumnSpan(8).Border(1).AlignCenter().Element(CellStyle).Text("BASE DE CALCULO").FontSize(8);
        
                static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                   


            });
        }
    }
    
    