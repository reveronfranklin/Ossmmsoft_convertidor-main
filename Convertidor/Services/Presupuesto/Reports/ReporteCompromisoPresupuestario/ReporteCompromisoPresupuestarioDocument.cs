using Convertidor.Dtos.Presupuesto.ReporteCompromisoPresupuestario;
using NPOI.SS.Formula.Functions;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Spire.Xls;
using System.Globalization;

namespace Convertidor.Services.Presupuesto.ReporteCompromisoPresupuestario
{
    public class ReporteCompromisoPresupuestarioDocument : IDocument
    {
        public static Image LogoImage { get; } = Image.FromFile("logo.png");
        public ReporteCompromisoPresupuestarioDto Model;
        private readonly string _patchLogo;

        public ReporteCompromisoPresupuestarioDocument(ReporteCompromisoPresupuestarioDto model,
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
                    row.RelativeItem(4).BorderBottom(1).BorderTop(1).AlignCenter().AlignMiddle().PaddingRight(18).Text("COMPROMISO PRESUPUESTARIO").SemiBold().FontSize(14);
                    
                    row.RelativeItem().Column(col =>
                    {
   
                        col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().AlignTop().PaddingBottom(5).Text(encabezado.ModelEncabezado.NumeroCompromiso).FontSize(12).Bold().FontColor(Colors.Red.Medium);
                        col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().AlignTop().PaddingBottom(5).Text("N° COMPROMISO").FontSize(8).SemiBold();
                        col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().AlignTop().PaddingBottom(5).Text(encabezado.ModelEncabezado.FechaCompromiso.ToShortDateString()).FontSize(8);
                        col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().AlignTop().PaddingBottom(5).Text("Fecha").FontSize(8).SemiBold();
                        col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().AlignTop().PaddingBottom(6).Text(encabezado.ModelEncabezado.NumeroSolicitud).FontSize(8).SemiBold();
                        col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().AlignTop().PaddingBottom(5).Text("N° SOLICITUD").FontSize(8).SemiBold();
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


                var bolivares = Model.Cuerpo.Sum(x => x.TotalBolivares);

                var montoImpuesto = bolivares * (decimal)0.16;
                var total = bolivares + montoImpuesto;

                var totalBolivares = bolivares.ToString("N", formato);
                var totalImpuesto = montoImpuesto.ToString("N", formato);
                var totales = total.ToString("N", formato);
                table.Footer(footer =>
                {
                    
                    footer.Cell().ColumnSpan(6).BorderVertical(1).BorderTop(1).Row(row =>
                    {
                        row.RelativeItem(5).PaddingLeft(10).PaddingRight(5).AlignLeft().Text($"MONTO TOTAL EN LETRA :\n{Model.Encabezado.MontoEnLetras.ToUpper()}").FontSize(8).Bold();
                        row.ConstantItem(70).AlignRight().AlignBottom().PaddingRight(2).Text("TOTAL").FontSize(8).Bold();

                        
                        row.ConstantItem(70).BorderLeft(1).AlignRight().Column(col =>
                        {
                            col.Item().BorderBottom(1).ExtendHorizontal().PaddingVertical(20);
                            col.Item().ExtendHorizontal().AlignRight().PaddingBottom(-1).PaddingRight(3).Text(totalBolivares).FontSize(7);
                        });

                    });

                    footer.Cell().ColumnSpan(6).Column(col =>
                    {
                        col.Item().BorderVertical(1).BorderTop(1).PaddingLeft(3).Text("MOTIVO  :").FontSize(8).Bold();
                        col.Item().BorderVertical(1).PaddingLeft(3).PaddingBottom(3).Text(Model.Encabezado.Motivo).FontSize(7);
                    });

                    footer.Cell().ColumnSpan(2).Column(col =>
                    {
                        col.Item().BorderVertical(1).BorderTop(1).AlignTop().AlignCenter().AlignRight().PaddingRight(15).PaddingVertical(3).Text($"ANALISTA").FontSize(8).Bold();
                        col.Item().BorderVertical(1).Text($"{Model.Encabezado.Firmante}").FontSize(7);
                        col.Item().BorderVertical(1).BorderBottom(1).PaddingLeft(4).PaddingVertical(4).Text($"FIRMA : ________________________________________     ").FontSize(8).Bold();

                    });

                    footer.Cell().ColumnSpan(4).BorderVertical(1).BorderBottom(1).BorderTop(1).AlignBottom().AlignCenter().Padding(3).PaddingLeft(8).PaddingBottom(5).Text($"DIRECCION DE PLANIFICACION Y PRESUPUESTO").FontSize(8).Bold();

                });
            });
        }

    }


}

