using Convertidor.Data.Entities.ADM;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Presupuesto.ReporteSolicitudModificacion;
using Convertidor.Services.Adm.Reports.ReporteSolicitudCompromiso;
using Convertidor.Services.Presupuesto.Reports.ReporteSolicitudModificacionPresupuestaria;
using iText.Kernel.Geom;
using MathNet.Numerics.Distributions;
using Org.BouncyCastle.Asn1.X509;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Spire.Xls;
using System.ComponentModel.DataAnnotations;
using Image = QuestPDF.Infrastructure.Image;

namespace Convertidor.Services.Adm.Reports.ReporteSolicitudModificacionPresupuestaria
{
    public class ReporteSolicitudCompromisoDocument : IDocument
    {

        public static Image LogoImage { get; } = Image.FromFile("logo.png");
        public ADM_DETALLE_SOLICITUD Model { get; }

        private readonly string _patchLogo;


        public ReporteSolicitudCompromisoDocument(

          ADM_DETALLE_SOLICITUD model,

            string patchLogo)
        {

            Model = model;


            _patchLogo = patchLogo;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Margin(10);
                    page.Size(PageSizes.A3.Landscape());
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

        void ComposeHeader(IContainer container)
        {

            //var descripcion = "";
            //var firstResumen = Model.FirstOrDefault();

            container.PaddingVertical(1).Column(column =>
            {

                column.Spacing(2);
                column.Item().PaddingLeft(50).Width(100).AlignLeft().ScaleToFit().Image(_patchLogo);
                //column.Item().Element(ComposeTableFirma);
                column.Item().AlignCenter().Text("SOLICITUD DE MODIFICACIONES PRESUPUESTARIAS").SemiBold().FontSize(8);
                column.Item().PaddingLeft(25).AlignLeft().Text("DIRECCION  DE PLANIFICACIÓN Y \n " + "      " + "       " + "       " + "PRESUPUESTO").ExtraBold().FontSize(8);

               

            });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(2).Column(async column =>
            {



                //column.Item().Row(row =>
                //{
                //    row.RelativeItem().Component(new SolicitudCompromisoComponent (Model.CODIGO_SOLICITUD));

                //});

                

                //column.Item().Row(row =>
                //{
                //    row.RelativeItem().Component(new DetalleComponent(Model));

                //});

                //column.Item().Row(row =>
                //{
                //    row.RelativeItem().Component(new DetalleComponent(Model.DetalleDe));

                //});

                //column.Item().Element(ComposeFooter);
            });
            



        }

        void ComposeFooter(IContainer container)
        {
            container.Table(async tableFooter =>
            {
                tableFooter.ColumnsDefinition(column =>
                {
                    column.RelativeColumn();
                    column.RelativeColumn();
                    column.RelativeColumn();
                    column.RelativeColumn();
                });


                tableFooter.Cell().ColumnSpan(2).Border(1).AlignLeft().Text("   Unidad Solicitante").FontSize(7);
                tableFooter.Cell().ColumnSpan(2).Border(1).AlignLeft().Text("   Recibido por la Dirección  de Planificacion y Presupuesto").FontSize(8);

                tableFooter.Cell().ColumnSpan(2).Column(col => 
                {
                    col.Item().Border(1).AlignLeft().Text("   ").FontSize(7);
                    col.Item().Border(1).AlignLeft().Text(" ").FontSize(7);
                });

                tableFooter.Cell().ColumnSpan(2).Column(col =>
                {
                    col.Item().Border(1).AlignLeft().Text("   ").FontSize(7);
                    col.Item().Border(1).AlignLeft().Text(" ").FontSize(7);
                });
                tableFooter.Cell().ColumnSpan(4).Row(row =>
                {
                    row.RelativeItem().AlignCenter().ShowOnce().Text("Firma y Sello").FontSize(14).ExtraBold();
                 
                    row.RelativeItem().AlignCenter().ShowOnce().Text("Firma y Sello").FontSize(14).ExtraBold();
                });
                tableFooter.Cell().ColumnSpan(2).Column(col => 
                {
                    col.Item().AlignLeft().Text("Nota: Colocar(*)en la imputacion presupeustaria receptora cuando la partida o sub-partida sea una creacion. ").FontSize(8);
                });
            });
        }

    }
}
    


            

        




  