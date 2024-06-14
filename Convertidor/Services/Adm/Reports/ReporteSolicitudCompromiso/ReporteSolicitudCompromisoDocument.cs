using Convertidor.Dtos.Adm.ReporteSolicitudCompromiso;
using MimeKit;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Spire.Xls;
using Image = QuestPDF.Infrastructure.Image;

namespace Convertidor.Services.Adm.Reports.ReporteSolicitudCompromiso
{
    public class ReporteSolicitudCompromisoDocument : IDocument
    {

        public static Image LogoImage { get; } = Image.FromFile("logo.png");
        public ReporteSolicitudCompromisoDto Model { get; }

        private readonly string _patchLogo;


        public ReporteSolicitudCompromisoDocument(

          ReporteSolicitudCompromisoDto model,

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
                    page.Size(PageSizes.A4);
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

            var solicitud = Model.SolicitudCompromiso; ;
            //var firstResumen = Model.FirstOrDefault();

            container.PaddingVertical(1).Row(row =>
            {

                //row.Spacing(2);
                row.ConstantItem(200).BorderLeft(1).BorderTop(1).BorderBottom(1).PaddingVertical(4).PaddingLeft(50).AlignLeft().ScaleToFit().Image(_patchLogo);
                //column.Item().Element(ComposeTableFirma);
                row.ConstantItem(285).BorderTop(1).BorderBottom(1).PaddingTop(20).PaddingRight(20).PaddingVertical(4).AlignCenter().Text("SOLICITUD DE COMPROMISO").SemiBold().FontSize(8);
                
                row.RelativeItem().Column(col =>
                {
                    col.Item().Border(1).AlignCenter().Text("N° Solicitud Compromiso");
                    col.Item().Border(1).AlignCenter().Text(solicitud.NumeroSolicitud).FontSize(7);

                    col.Item().Border(1).AlignCenter().Text("Fecha Compromiso");
                    col.Item().Border(1).AlignCenter().Text(solicitud.FechaSolicitud.ToShortDateString()).FontSize(7);

                });

               

            });
        }

        void ComposeContent(IContainer container)
        {
            container.Column(async column =>
            {



                column.Item().Row(row =>
                {
                    row.RelativeItem().Component(new SolicitudCompromisoComponent(Model.SolicitudCompromiso));

                });



                column.Item().Row(row =>
                {
                    row.RelativeItem().Component(new DetalleSolicitudComponent(Model.DetalleSolicitud));

                });

                //column.Item().Row(row =>
                //{
                //    row.RelativeItem().Component(new DetalleComponent(Model.DetalleDe));

                //});

                //column.Item().Element(ComposeFooter);
            });
            



        }

        //void ComposeFooter(IContainer container)
        //{
        //    container.Table(async tableFooter =>
        //    {
        //        tableFooter.ColumnsDefinition(column =>
        //        {
        //            column.RelativeColumn();
        //            column.RelativeColumn();
        //            column.RelativeColumn();
        //            column.RelativeColumn();
        //        });


        //        tableFooter.Cell().ColumnSpan(2).Border(1).AlignLeft().Text("   Unidad Solicitante").FontSize(7);
        //        tableFooter.Cell().ColumnSpan(2).Border(1).AlignLeft().Text("   Recibido por la Dirección  de Planificacion y Presupuesto").FontSize(8);

        //        tableFooter.Cell().ColumnSpan(2).Column(col => 
        //        {
        //            col.Item().Border(1).AlignLeft().Text("   ").FontSize(7);
        //            col.Item().Border(1).AlignLeft().Text(" ").FontSize(7);
        //        });

        //        tableFooter.Cell().ColumnSpan(2).Column(col =>
        //        {
        //            col.Item().Border(1).AlignLeft().Text("   ").FontSize(7);
        //            col.Item().Border(1).AlignLeft().Text(" ").FontSize(7);
        //        });
        //        tableFooter.Cell().ColumnSpan(4).Row(row =>
        //        {
        //            row.RelativeItem().AlignCenter().ShowOnce().Text("Firma y Sello").FontSize(14).ExtraBold();
                 
        //            row.RelativeItem().AlignCenter().ShowOnce().Text("Firma y Sello").FontSize(14).ExtraBold();
        //        });
        //        tableFooter.Cell().ColumnSpan(2).Column(col => 
        //        {
        //            col.Item().AlignLeft().Text("Nota: Colocar(*)en la imputacion presupeustaria receptora cuando la partida o sub-partida sea una creacion. ").FontSize(8);
        //        });
        //    });
        //}

    }
}
    


            

        




  