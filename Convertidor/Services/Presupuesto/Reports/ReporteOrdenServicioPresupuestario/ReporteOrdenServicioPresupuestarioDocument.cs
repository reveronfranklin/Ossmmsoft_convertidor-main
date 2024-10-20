using Convertidor.Dtos.Presupuesto.ReporteCompromisoPresupuestario;
using NPOI.SS.Formula.Functions;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Spire.Xls;
using System.Globalization;

namespace Convertidor.Services.Presupuesto.ReporteOrdenSercicioPresupuestario
{
    public class ReporteOrdenServicioPresupuestarioDocument : IDocument
    {
        public static Image LogoImage { get; } = Image.FromFile("logo.png");
        public ReporteCompromisoPresupuestarioDto Model;
        private readonly string _patchLogo;

        public ReporteOrdenServicioPresupuestarioDocument(ReporteCompromisoPresupuestarioDto model,
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
                        //page.Header().Element(ComposeHeader);

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
                
                

                column.Item().Row(row =>
                {
                    var encabezado = new EncabezadoComponent(Model.Encabezado);
                    row.ConstantItem(210).BorderLeft(1).BorderBottom(1).BorderTop(1).PaddingLeft(20).AlignLeft().AlignMiddle().ScaleToFit().Image(_patchLogo);
                    row.RelativeItem(4).BorderBottom(1).BorderTop(1).AlignCenter().AlignMiddle().PaddingRight(18).Text(Model.Encabezado.Titulo).SemiBold().FontSize(14);
                    
                    row.RelativeItem().Column(col =>
                    {
   
                        col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().AlignTop().PaddingBottom(5).Text(encabezado.ModelEncabezado.NumeroCompromiso).FontSize(12).Bold().FontColor(Colors.Red.Medium);
                        col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().AlignTop().PaddingBottom(5).Text("N° COMPROMISO").FontSize(11).SemiBold();
                        col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().AlignTop().PaddingBottom(5).Text(encabezado.ModelEncabezado.FechaCompromiso.ToShortDateString()).FontSize(11);
                        col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().AlignTop().PaddingBottom(5).Text("Fecha").FontSize(11).SemiBold();
                        col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().AlignTop().PaddingBottom(6).Text(encabezado.ModelEncabezado.NumeroSolicitud).FontSize(11).SemiBold();
                        col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().AlignTop().PaddingBottom(5).Text("N° SOLICITUD").FontSize(11).SemiBold();
                    });
                
                    
                });

                column.Spacing(10);
                column.Item().Component(new EncabezadoComponent(Model.Encabezado));
            });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingTop(5).Column(async column =>
            {

                column.Item().Row(row =>
                {

                    row.RelativeItem().Component(new CuerpoComponent(Model.Cuerpo,Model.Encabezado));
                    
                });
                /*column.Item().Row(row =>
                {

                    row.RelativeItem().Component(new PucComponent(Model.Cuerpo,Model.Encabezado));
                    
                });*/
              

                
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


                var bolivares =Model.Encabezado.Tolal;

                var montoImpuesto = Model.Encabezado.Impuesto;
                var total =Model.Encabezado.TolalMasImpuesto;

                var totalBolivares = bolivares.ToString("N", formato);
                var totalImpuesto = montoImpuesto.ToString("N", formato);
                var totales = total.ToString("N", formato);
                table.Footer(footer =>
                {
                    
                    footer.Cell().ColumnSpan(4).BorderLeft(1).Column(col =>
                    {
                        col.Item().BorderLeft(1).BorderTop(1).PaddingLeft(5).AlignLeft().Text("MONTO TOTAL EN LETRA :").FontSize(11).Bold();
                        col.Item().BorderLeft(1).PaddingLeft(5).AlignLeft().PaddingBottom(10).Text($"{Model.Encabezado.MontoEnLetras.ToUpper()}").FontSize(11);
                    });


                    var bolivares = Model.Encabezado.Base;
                    var montoImpuesto = Model.Encabezado.Impuesto;
                    var total = Model.Encabezado.TolalMasImpuesto;
                    var porcImpuesto = Model.Encabezado.PorcentajeImpuesto;
                    var totalBolivares = bolivares.ToString("N", formato);
                    var totalImpuesto = montoImpuesto.ToString("N", formato);
                    var totales = total.ToString("N", formato);
                    var porcImpuestoString = $"{porcImpuesto.ToString("N", formato)}%  IVA";
                    if (porcImpuesto == 0)
                    {
                        porcImpuestoString = "";
                    }
                    footer.Cell().Column(col =>
                    {

                        col.Item().BorderTop(1).BorderLeft(1).Width(100).AlignRight().PaddingRight(3).Text("SUBTOTAL").FontSize(11).Bold();
                        col.Item().Width(100).BorderLeft(1).AlignRight().PaddingRight(3).Text($"{porcImpuestoString}").FontSize(11).Bold();
                        col.Item().Width(100).BorderLeft(1).AlignRight().AlignMiddle().PaddingRight(3).PaddingBottom(10).Text("TOTAL").FontSize(11).Bold();

                    });

                    footer.Cell().Column(col =>
                    {

                        col.Item().Width(100).Border(1).AlignRight().Padding(1).PaddingRight(3).Text(totalBolivares).FontSize(11);
                        col.Item().Width(100).Border(1).AlignRight().Padding(1).PaddingRight(3).Text(totalImpuesto).FontSize(11);
                        col.Item().Width(100).Border(1).AlignRight().AlignMiddle().BorderBottom(1).Padding(1).PaddingBottom(10).PaddingRight(3).Text(totales).FontSize(11);


                    });
                    
                    footer.Cell().ColumnSpan(6).Column(col =>
                    {
                        col.Item().BorderVertical(1).BorderTop(1).PaddingLeft(3).Text("MOTIVO  :").FontSize(11).Bold();
                        col.Item().BorderVertical(1).PaddingLeft(3).PaddingBottom(3).Text(Model.Encabezado.Motivo).FontSize(11);
                    });

                    footer.Cell().ColumnSpan(2).Column(col =>
                    {
                        col.Item().BorderVertical(1).BorderTop(1).AlignTop().AlignCenter().AlignRight().PaddingRight(15).PaddingVertical(3).Text($"Elaborado por:").FontSize(11).Bold();
                        col.Item().BorderVertical(1).Text($"{Model.Encabezado.Firmante}").FontSize(11);
                        col.Item().BorderVertical(1).BorderBottom(1).PaddingLeft(4).PaddingVertical(4).Text($"FIRMA : ________________________________________     ").FontSize(11).Bold();

                    });
                    footer.Cell().ColumnSpan(2).Column(col =>
                    {
                        col.Item().BorderVertical(1).BorderTop(1).AlignTop().AlignCenter().PaddingRight(15).PaddingVertical(3).Text($"Revisado por:").FontSize(11).Bold();
                        col.Item().BorderVertical(1).Text($"").FontSize(11);
                        col.Item().BorderVertical(1).Text($"").FontSize(11);
                        col.Item().BorderVertical(1).BorderBottom(1).PaddingLeft(4).AlignCenter().PaddingVertical(4).Text($"Director(a) Administración").FontSize(11).Bold();

                    });
                    footer.Cell().ColumnSpan(2).Column(col =>
                    {
                        col.Item().BorderVertical(1).BorderTop(1).AlignTop().AlignCenter().PaddingRight(15).PaddingVertical(3).Text($"Aprobado por:").FontSize(11).Bold();
                        col.Item().BorderVertical(1).Text($"").FontSize(11);
                        col.Item().BorderVertical(1).Text($"").FontSize(11);
                        col.Item().BorderVertical(1).BorderBottom(1).PaddingLeft(4).AlignCenter().PaddingVertical(4).Text($"Presidente(a)").FontSize(11).Bold();

                    });

                });
            });
        }

    }


}

