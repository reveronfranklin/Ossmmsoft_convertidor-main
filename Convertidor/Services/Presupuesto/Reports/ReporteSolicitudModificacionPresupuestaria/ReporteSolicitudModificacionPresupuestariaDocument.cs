using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Presupuesto.ReporteSolicitudModificacion;
using Convertidor.Services.Presupuesto.Report.Example;
using Convertidor.Services.Presupuesto.Reports.ReporteSolicitudModificacionPresupuestaria;
using iText.Kernel.Geom;
using MathNet.Numerics.Distributions;
using Org.BouncyCastle.Asn1.X509;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.ComponentModel.DataAnnotations;
using Image = QuestPDF.Infrastructure.Image;

namespace Convertidor.Services.Rh.Report.Example
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
                    page.Margin(20);
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

            container.PaddingVertical(10).Column(column =>
            {

                column.Spacing(20);
                column.Item().PaddingLeft(50).Width(100).AlignLeft().ScaleToFit().Image(_patchLogo);
                //column.Item().Element(ComposeTableFirma);
                column.Item().AlignCenter().Text("SOLICITUD DE MODIFICACIONES PRESUPUESTARIAS").SemiBold().FontSize(8);
                column.Item().PaddingLeft(25).AlignLeft().Text("DIRECCION  DE PLANIFICACIÓN Y \n " + "      " + "       " + "       " + "PRESUPUESTO").ExtraBold().FontSize(8);

                //column.Item().Element(ComposeTableResumenConcepto);




                /*row.RelativeItem().Column(column =>
                {

                    column.Item().Text(text =>
                    {
                        text.Span($"").FontSize(7).SemiBold();
                        ;
                    });
                    column.Item().Text(text =>
                    {
                        text.Span($"PRESUPUESTO: {firstResumen.CodigoPresupuesto}").FontSize(10).SemiBold();
                        ;
                    });
                    column.Item().Text(text =>
                    {
                        text.Span($"{descripcion}").FontSize(7).SemiBold();
                        ;
                    });
                });*/



            });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(5).Column(async column =>
            {



                column.Item().Row(row =>
                {
                    row.RelativeItem().Component(new GeneralReporteSolicitudModificacionTablaComponent(Model.General));

                });

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
    


            

        




  