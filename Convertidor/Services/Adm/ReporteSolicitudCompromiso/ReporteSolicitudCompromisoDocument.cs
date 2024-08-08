using Convertidor.Dtos.Adm.ReporteSolicitudCompromiso;
using Convertidor.Services.Presupuesto.Reports.ReporteSolicitudModificacionPresupuestaria;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Data.SqlClient.DataClassification;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Spire.Xls;
using System.Globalization;

namespace Convertidor.Services.Adm.ReporteSolicitudCompromiso
{
    public class ReporteSolicitudCompromisoDocument : IDocument
    {
        public static Image LogoImage { get; } = Image.FromFile("logo.png");
        public ReporteSolicitudCompromisoDto Model;
        private readonly string _patchLogo;

        public ReporteSolicitudCompromisoDocument(ReporteSolicitudCompromisoDto model,
                                                  string patchLogo)
        {
            Model = model;
            _patchLogo = patchLogo;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
       
        public void Compose(IDocumentContainer container)
        {
            try
            {
                

                container
                    
                    .Page(page =>
                    {
                        QuestPDF.Settings.CheckIfAllTextGlyphsAreAvailable = false;
                        
                        page.Margin(10);
                        page.Size(PageSizes.A3);
                        page.Header().Element(ComposeHeader);
                        page.Content().Element(ComposeContent);
                        page.Footer().Element(ComposeFooter);

                        page.Footer().AlignCenter().Text(text =>
                        {
                            page.Footer().Element(ComposeFooter);
                            text.CurrentPageNumber();
                            text.Span(" / ");
                            text.TotalPages();
                        });


                    });
                
            }
            catch (Exception ex) 
            {
              var message = ex.Message;   
            
            }
        }

        void ComposeHeader(IContainer container)
        {
            
        
            container.Column(column =>
            {
                
                column.Spacing(5);
                column.Item().Row(row =>
                {
                    var encabezado = new EncabezadoComponent(Model.Encabezado);
                    row.ConstantItem(210).BorderLeft(1).BorderBottom(1).BorderTop(1).PaddingLeft(20).AlignLeft().ScaleToFit().Image(_patchLogo);
                    row.RelativeItem(4).BorderBottom(1).BorderTop(1).AlignCenter().PaddingRight(10).Text("SOLICITUD DE COMPROMISO").SemiBold().FontSize(14);
                    row.RelativeItem().Column(col => 
                    {
                        col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().AlignTop().PaddingBottom(5).Text("N° Solicitud").FontSize(8).SemiBold();
                        col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().AlignTop().PaddingBottom(5).Text(encabezado.ModelEncabezado.NumeroSolicitud).FontSize(8);
                        col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().AlignTop().PaddingBottom(5).Text("Fecha").FontSize(8).SemiBold();
                        col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().AlignTop().PaddingBottom(5).Text(encabezado.ModelEncabezado.FechaSolicitud.ToShortDateString()).FontSize(8);
                        col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().AlignTop().PaddingBottom(6).Text("").FontSize(8).SemiBold();

                    });
                });

                column.Item().Column(col =>
                {
                    col.Item().Component(new EncabezadoComponent(Model.Encabezado));
                    
                });



            });
        }

        void ComposeContent(IContainer container)
        {
            container.Table(async table =>
            {
                table.ColumnsDefinition(columns => 
                {
                    columns.ConstantColumn(320);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();

                });

                table.ExtendLastCellsToTableBottom();

               table.Cell().ColumnSpan(6).ExtendVertical().PaddingTop(5).BorderVertical(1).Column(col =>
               {
                   
                   col.Item().Component(new CuerpoComponent(Model.Cuerpo, Model.Encabezado));
                   

               });

            });
            
        }

        void ComposeFooter(IContainer container)
        {

            NumberFormatInfo formato = new CultureInfo("es-AR").NumberFormat;

            formato.CurrencyGroupSeparator = ".";
            formato.NumberDecimalSeparator = ",";
            formato.NumberDecimalDigits = 2;

            container.Table(async table =>
            {
                table.ColumnsDefinition(colums =>
                {
                    colums.ConstantColumn(320);
                    colums.RelativeColumn();
                    colums.RelativeColumn();
                    colums.RelativeColumn();
                    colums.RelativeColumn();
                    colums.RelativeColumn();

                });


                table.Footer(footer =>
                {


                    footer.Cell().ColumnSpan(4).Column(col =>
                    {
                        col.Item().BorderLeft(1).BorderTop(1).PaddingLeft(5).PaddingBottom(6).AlignLeft().Text("MONTO TOTAL EN LETRA :").FontSize(8).Bold();
                        col.Item().BorderLeft(1).PaddingLeft(5).PaddingBottom(6).AlignLeft().Text($"{Model.Encabezado.MontoLetras.ToUpper()}").FontSize(8);
                    });


                    footer.Cell().Column(col =>
                    {

                        col.Item().BorderTop(1).BorderLeft(1).Width(100).AlignRight().PaddingRight(3).Text("SUBTOTAL").FontSize(8).Bold();
                        col.Item().Width(100).BorderLeft(1).AlignRight().PaddingRight(3).Text("16%    " + "  IVA").FontSize(8).Bold();
                        col.Item().Width(100).BorderLeft(1).AlignRight().PaddingRight(3).Text("TOTAL").FontSize(8).Bold();


                    });



                    var bolivares = Model.Cuerpo.Sum(x => x.TotalBolivares);
                    var montoImpuesto = bolivares * (decimal)0.16;
                    var total = bolivares + montoImpuesto;

                    var totalBolivares = bolivares.ToString("N", formato);
                    var totalImpuesto = montoImpuesto.ToString("N", formato);
                    var totales = total.ToString("N", formato);

                    footer.Cell().Column(col =>
                    {

                        col.Item().Width(100).Border(1).AlignRight().Padding(1).PaddingRight(3).Text(totalBolivares).FontSize(7);
                        col.Item().Width(100).Border(1).AlignRight().Padding(1).PaddingRight(3).Text(totalImpuesto).FontSize(7);
                        col.Item().Width(100).Border(1).AlignRight().BorderBottom(1).Padding(1).PaddingRight(3).Text(totales).FontSize(7);


                    });

                    footer.Cell().ColumnSpan(6).Column(col =>
                    {
                        col.Item().BorderVertical(1).BorderTop(1).PaddingLeft(3).Text("MOTIVO  :").FontSize(8).Bold();
                        col.Item().BorderVertical(1).PaddingLeft(3).Text(Model.Encabezado.Motivo).FontSize(7);
                    });

                    footer.Cell().ColumnSpan(6).Row(row =>
                    {
                        row.RelativeItem().BorderVertical(1).BorderTop(1).AlignTop().AlignCenter().AlignRight().PaddingRight(15).Text($"Elaborado Por :       ").FontSize(8).Bold();
                        row.RelativeItem().BorderVertical(1).BorderTop(1).AlignTop().AlignCenter().Padding(3).PaddingLeft(8).Text($"Revisado Por :       ").FontSize(8).Bold();
                        row.RelativeItem().BorderVertical(1).BorderTop(1).AlignTop().AlignCenter().Padding(3).PaddingLeft(8).Text($"Confirmado Por :       ").FontSize(8).Bold();


                    });

                    footer.Cell().ColumnSpan(6).Row(row =>
                    {
                        row.RelativeItem().BorderVertical(1).BorderBottom(1).AlignTop().AlignLeft().Padding(3).PaddingLeft(8).PaddingBottom(3).Text($"{Model.Encabezado.Firmante}  \n FIRMA: ________________________________________________________________________").FontSize(8).SemiBold();
                        row.RelativeItem().BorderVertical(1).BorderBottom(1).AlignTop().AlignCenter().Padding(3).PaddingLeft(8).PaddingBottom(3).Text($"         ").FontSize(8).SemiBold();
                        row.RelativeItem().BorderVertical(1).BorderBottom(1).AlignTop().AlignCenter().Padding(3).PaddingLeft(8).PaddingBottom(3).Text($"DIRECCION DE ADMINISTRACION Y FINANZAS").FontSize(8).Bold();




                    });


                });
            });
        }

    }


}

