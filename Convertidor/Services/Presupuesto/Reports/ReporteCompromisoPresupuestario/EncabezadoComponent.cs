using Convertidor.Dtos.Presupuesto.ReporteCompromisoPresupuestario;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Drawing.Text;
using System.Globalization;


namespace Convertidor.Services.Presupuesto.ReporteCompromisoPresupuestario
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

            QuestPDF.Settings.CheckIfAllTextGlyphsAreAvailable = false;

            var headerStyle = TextStyle.Default.SemiBold().FontSize(11).Fallback();
            NumberFormatInfo formato = new CultureInfo("es-AR").NumberFormat;

            
            

            formato.CurrencyGroupSeparator = ".";
            formato.NumberDecimalSeparator = ",";
            formato.NumberDecimalDigits = 2;
            
            container
                .Border(1)
                .Table(table =>
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

            
                table.Cell().ColumnSpan(6).Column(col =>
                {
                    col.Item().BorderVertical(1).BorderTop(1).AlignLeft().PaddingLeft(4).Text("SEÑORES: ").Style(headerStyle);
                    col.Item().BorderVertical(1).BorderBottom(1).AlignLeft().PaddingLeft(4).Text(ModelEncabezado.NombreProveedor).Style(headerStyle).FontSize(11);
                    col.Item().BorderVertical(1).AlignLeft().PaddingLeft(4).Text("SOLICITANTE: ").Style(headerStyle);
                    col.Item().BorderVertical(1).BorderBottom(1).AlignLeft().PaddingLeft(4).Text($"{ModelEncabezado.IcpConcat}  {ModelEncabezado.Denominacion} {ModelEncabezado.Rif}").Style(headerStyle).FontSize(11);
                   
                 
                   
                    
                });

                


                static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2);



            });
        }


    }
}  

