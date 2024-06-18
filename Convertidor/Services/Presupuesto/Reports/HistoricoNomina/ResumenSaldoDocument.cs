using Convertidor.Dtos.Presupuesto;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Image = QuestPDF.Infrastructure.Image;

namespace Convertidor.Services.Rh.Report.Example
{
    public class ResumenSaldoDocument : IDocument
    {

        public static Image LogoImage { get; } = Image.FromFile("logo.png");
        private readonly string _title;
        private readonly string _patchLogo;
        public List<PreResumenSaldoGetDto> Model { get; }

        public ResumenSaldoDocument(
            string Title,
            List<PreResumenSaldoGetDto> model,
            string patchLogo)
        {

            Model = model;
            _title = Title;
            _patchLogo = patchLogo;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Margin(50);

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

            var descripcion = "";
            var firstResumen = Model.FirstOrDefault();

            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"REPORTE RESUMEN SALDO PRESUPUESTO")
                        .FontSize(7).SemiBold();

                    column.Item().Text(text =>
                    {
                        text.Span($"{_title}").FontSize(7).SemiBold();
                        ;

                    });

                });
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


                row.ConstantItem(125).Image(_patchLogo);
            });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(20);

               // column.Item().AlignCenter().Text($"RECIBOS").SemiBold();
                var listaTitulos = from s in Model
                    group s by new
                    {
                        Titulo = s.Titulo,


                    }
                    into g
                    select new
                    {
                        Titulo = g.Key.Titulo,


                    };
                foreach (var itemTitulo in listaTitulos)
                {
                    //var persona = Model.Where(x => x.Titulo == itemTitulo.Titulo).FirstOrDefault();
                    column.Item().Row(row => { row.RelativeItem().Component(new TituloComponent(itemTitulo.Titulo)); });

                    var data = Model.Where(x => x.Titulo == itemTitulo.Titulo).ToList();
                    column.Item()
                        .Row(row => { row.RelativeItem().Component(new ResumenSaldoTablaComponent("", data)); });


                }



                //column.Item().Element(ComposeTableRecibo);




            });

        }




    }





}
  