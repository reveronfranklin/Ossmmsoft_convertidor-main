using Convertidor.Dtos.Adm.ReporteSolicitudCompromiso;
using Convertidor.Services.Presupuesto.Reports.ReporteSolicitudModificacionPresupuestaria;
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
            container
                .Page(page =>
                {
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

        void ComposeHeader(IContainer container)
        {

            //var descripcion = "";
            //var firstResumen = Model.FirstOrDefault();

            container.PaddingVertical(1).Column(column =>
            {

                column.Spacing(2);
                column.Item().PaddingLeft(50).Width(100).AlignLeft().ScaleToFit().Image(_patchLogo);
                //column.Item().Element(ComposeTableFirma);
                column.Item().AlignCenter().Text("SOLICITUD COMPROMISO").SemiBold().FontSize(8);
                //column.Item().PaddingLeft(25).AlignLeft().Text("").ExtraBold().FontSize(8);



            });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(2).Column(async column =>
            {



                column.Item().Row(row =>
                {
                    row.RelativeItem().Component(new EncabezadoComponent(Model.Encabezado));

                });



                column.Item().Row(row =>
                {
                    row.RelativeItem().Component(new CuerpoComponent(Model.Cuerpo));

                });

                
            });
            //column.Item().Element(ComposeTableRecibo);



        }
    }
}
