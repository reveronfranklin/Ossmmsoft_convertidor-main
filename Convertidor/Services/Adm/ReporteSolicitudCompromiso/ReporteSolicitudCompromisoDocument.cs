using Convertidor.Dtos.Adm.ReporteSolicitudCompromiso;
using Convertidor.Services.Presupuesto.Reports.ReporteSolicitudModificacionPresupuestaria;
using Microsoft.Data.SqlClient.DataClassification;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

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


                        page.Footer().AlignCenter().Text(text =>
                        {

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
            
        
            container.PaddingVertical(1).Column(column =>
            {
                

                
                
             
                column.Item().Row(row =>
                {
                    var encabezado = new EncabezadoComponent(Model.Encabezado);
                    row.ConstantItem(200).BorderLeft(1).BorderBottom(1).BorderTop(1).PaddingLeft(50).AlignLeft().ScaleToFit().Image(_patchLogo);
                    row.RelativeItem(4).BorderBottom(1).BorderTop(1).AlignCenter().Text("SOLICITUD COMPROMISO").SemiBold().FontSize(14);
                    
                    row.RelativeItem().Column(col =>
                    {
                            col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().Padding(5).Text("N° Solicitud").FontSize(8);
                            col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().Padding(5).Text(encabezado.ModelEncabezado.NumeroSolicitud).FontSize(8);
                            col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().Padding(5).Text("Fecha").FontSize(8);
                            col.Item().Width(120).BorderBottom(1).BorderRight(1).BorderLeft(1).BorderTop(1).AlignCenter().Padding(5).Text(encabezado.ModelEncabezado.FechaSolicitud.ToShortDateString()).FontSize(8);
                    });
                
                    
                });



               

             
                


            });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(5).Column(async column =>
            {

                column.Spacing(5);

                column.Item().Row(row =>
                {
                    row.RelativeItem().Component(new EncabezadoComponent(Model.Encabezado));

                });



                column.Item().Row(row =>
                {
                    row.RelativeItem().Component(new CuerpoComponent(Model.Cuerpo));

                });

                
            });
            
        }


    }


}

