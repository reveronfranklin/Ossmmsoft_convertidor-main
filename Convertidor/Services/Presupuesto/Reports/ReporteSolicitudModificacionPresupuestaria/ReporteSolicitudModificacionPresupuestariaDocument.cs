using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Presupuesto.ReporteSolicitudModificacion;
using Convertidor.Services.Presupuesto.Reports.ReporteSolicitudModificacionPresupuestaria;
using iText.Kernel.Geom;
using MathNet.Numerics.Distributions;
using Org.BouncyCastle.Asn1.X509;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.ComponentModel.DataAnnotations;
using Image = QuestPDF.Infrastructure.Image;

namespace Convertidor.Services.Presupuesto.Reports.ReporteSolicitudModificacionPresupuestaria
{
    public class ReporteSolicitudModificacionPresupuestariaDocument : IDocument
    {

        public static Image LogoImage { get; } = Image.FromFile("logo.png");
        public ReporteSolicitudModificacionPresupuestariaDto Model { get; }

        private readonly string _patchLogo;


        public ReporteSolicitudModificacionPresupuestariaDocument(

           ReporteSolicitudModificacionPresupuestariaDto model,

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
                    page.Size(PageSizes.A4.Landscape());
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



                column.Item().Row(row =>
                {
                    row.RelativeItem().Component(new GeneralReporteSolicitudModificacionTablaComponent(Model.General));

                });

                if(Model.DetallePara == )

                column.Item().Row(row =>
                {
                    row.RelativeItem().Component(new DetalleComponent(Model.DetallePara));

                });

                column.Item().Row(row =>
                {
                    row.RelativeItem().Component(new DetalleComponent(Model.DetalleDe));

                });

            });
            //column.Item().Element(ComposeTableRecibo);



        }
    }
}
    


            

        




  