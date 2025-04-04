﻿using Convertidor.Dtos.Adm.ReporteSolicitudCompromiso;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Drawing.Text;
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

            QuestPDF.Settings.CheckIfAllTextGlyphsAreAvailable = false;

            var headerStyle = TextStyle.Default.SemiBold().FontSize(11).Fallback();
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


                table.Cell().ColumnSpan(6).Column(col =>
                {
                    col.Item().BorderTop(1).BorderVertical(1).AlignLeft().PaddingLeft(4).Text("PARA: ").Style(headerStyle);
                    col.Item().BorderBottom(1).BorderVertical(1).AlignLeft().PaddingLeft(4).Text(ModelEncabezado.Para).Style(headerStyle);
                    col.Item().BorderTop(1).BorderVertical(1).AlignLeft().PaddingLeft(4).Text("SOLICITANTE: ").Style(headerStyle);
                    col.Item().BorderBottom(1).BorderVertical(1).AlignLeft().PaddingLeft(4).Text(ModelEncabezado.UnidadEjecutora).Style(headerStyle);
                    col.Item().BorderTop(1).BorderVertical(1).AlignLeft().PaddingLeft(4).Text("PROVEEDOR: ").Style(headerStyle);
                    if(ModelEncabezado.Vialidad == string.Empty && ModelEncabezado.Vivienda == string.Empty) 
                    {
                        ModelEncabezado.Vialidad =" ";
                        ModelEncabezado.Vivienda =" ";
                        
                    }
                    col.Item().BorderBottom(1).BorderVertical(1).AlignLeft().PaddingLeft(4).Text($"{ModelEncabezado.NombreProveedor}    {ModelEncabezado.DireccionProveedor.TrimStart()}     {ModelEncabezado.TelefonoProveedor.Trim()} {ModelEncabezado.Rif.Trim()}").Style(headerStyle);
                    
                });

                


                static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2);



            });
        }


    }
}  

