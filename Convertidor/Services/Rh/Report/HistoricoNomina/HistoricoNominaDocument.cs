using System.Globalization;
using System.Linq;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Convertidor.Services.Rh.Report.Example
{
    public class HistoricoNominaDocument : IDocument
    {
    
        public static Image LogoImage { get; } = Image.FromFile("logo.png");
        private readonly string _patchLogo;
        public List<RhReporteNominaResumenConceptoResponseDto> Model { get; }

        public HistoricoNominaDocument(List<RhReporteNominaResumenConceptoResponseDto> model,string patchLogo)
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

            var firstResumen = Model.FirstOrDefault();
            
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"REPORTE GENERAL DE NÓMINA")
                        .FontSize(7).SemiBold();
                    
                    column.Item().Text(text =>
                    {
                        text.Span($"{firstResumen.TipoNomina}").FontSize(7).SemiBold();;
                       
                    });
                    column.Item().Text(text =>
                    {
                        text.Span($"{firstResumen.FechaNomina:d}").FontSize(7).SemiBold();;
                    });
                });
              
                //row.ConstantItem(175).Image(LogoImage);
               
                row.ConstantItem(125).Image(_patchLogo);
            });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).Column(column => 
            {
                column.Spacing(20);
                
                /*column.Item().Row(row =>
                {
                    row.RelativeItem().Component(new AddressComponent("From", Model.SellerAddress));
                    row.ConstantItem(50);
                    row.RelativeItem().Component(new AddressComponent("For", Model.CustomerAddress));
                });*/
                column.Item().AlignCenter().Text($"RESUMEN CONCEPTO").SemiBold();
                column.Item().Element(ComposeTable);

                /*var totalAsignacion = Model.Sum(x => x.Asignacion);
                var totalDeduccion = Model.Sum(x => x.Deduccion);
                column.Item().PaddingRight(5).AlignRight().Text($"Total: {totalAsignacion:C} {totalDeduccion:C}").SemiBold();
                 */

                /*if (!string.IsNullOrWhiteSpace(Model.Comments))
                    column.Item().PaddingTop(25).Element(ComposeComments);*/
            });
        }

        void ComposeTable(IContainer container)
        {
            var headerStyle = TextStyle.Default.SemiBold().FontSize(7);
            NumberFormatInfo formato = new CultureInfo("es-AR").NumberFormat;

            formato.CurrencyGroupSeparator = ".";
            formato.NumberDecimalSeparator = ",";
            formato.NumberDecimalDigits = 2;
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(25);
                    columns.RelativeColumn(3);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                  // columns.RelativeColumn();
                });
                
                table.Header(header =>
                {
                    header.Cell().Text("Nro").Style(headerStyle);
                    header.Cell().Text("CONCEPTO").Style(headerStyle);
                    header.Cell().AlignRight().Text("ASIGNACIONES").Style(headerStyle);
                    header.Cell().AlignRight().Text("DEDUCCIONES").Style(headerStyle);
                   //header.Cell().AlignRight().Text("").Style(headerStyle);
                    
                    header.Cell().ColumnSpan(4).PaddingTop(5).BorderBottom(1).BorderColor(Colors.Black);
                });
               
                foreach (var item in Model.ToList())
                {
                    var index = Model.ToList().IndexOf(item) + 1;
    
                    table.Cell().Element(CellStyle).Text($"{item.NumeroConcepto}").FontSize(7);
                    table.Cell().Element(CellStyle).Text(item.DenominacionConcepto).FontSize(7);
                    var asignacion = item.Asignacion.ToString("N", formato);
                    var deduccion = item.Deduccion.ToString("N", formato);
                    table.Cell().Element(CellStyle).AlignRight().Text($"{asignacion}").FontSize(7);
                    table.Cell().Element(CellStyle).AlignRight().Text($"{deduccion}").FontSize(7);
                    //table.Cell().Element(CellStyle).Text("");
                   /* table.Cell().Element(CellStyle).AlignRight().Text($"{item.Price:C}");
                    table.Cell().Element(CellStyle).AlignRight().Text($"{item.Quantity}");
                    table.Cell().Element(CellStyle).AlignRight().Text($"{item.Price * item.Quantity:C}");*/
                    
                    //static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    static IContainer CellStyle(IContainer container) => container.PaddingVertical(5);

                }
                table.Cell().Text($"").FontSize(7);
                table.Cell().AlignRight().Text($"Total:").FontSize(10).SemiBold();
                var asig = Model.Sum(x => x.Asignacion);
                var ded = Model.Sum(x => x.Deduccion);
                var totalAsignacion = asig.ToString("N", formato);
                var totalDeduccion = ded.ToString("N", formato);
                table.Cell().AlignRight().Text($"{totalAsignacion}").FontSize(10).SemiBold();
                table.Cell().AlignRight().Text($"{totalDeduccion}").FontSize(10).SemiBold();
            });
        }
        
        void ComposeTable1(IContainer container)
        {
            var headerStyle = TextStyle.Default.SemiBold();
            
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    //columns.ConstantColumn(25);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                   
                });
                
                table.Header(header =>
                {
                    header.Cell().Text("#");
                    header.Cell().Text("CONCEPTO").Style(headerStyle);
                    header.Cell().AlignRight().Text("ASIGNACIONES").Style(headerStyle);
                    header.Cell().AlignRight().Text("DEDUCCIONES").Style(headerStyle);
                   
                    
                    header.Cell().ColumnSpan(5).PaddingTop(5).BorderBottom(1).BorderColor(Colors.Black);
                });
                var list = Model.ToList();
                foreach (var item in list)
                {

                    table.Cell().Element(CellStyle).Text($"{item.NumeroConcepto}");
                    table.Cell().Element(CellStyle).Text(item.DenominacionConcepto);
                    table.Cell().Element(CellStyle).AlignRight().Text($"{item.Asignacion.ToString()}");
                    table.Cell().Element(CellStyle).AlignRight().Text($"{item.Deduccion.ToString()}");
                    
                    static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                }
            });
        }

        void ComposeComments(IContainer container)
        {
            container.ShowEntire().Background(Colors.Grey.Lighten3).Padding(10).Column(column => 
            {
                column.Spacing(5);
                column.Item().Text("Comments").FontSize(14).SemiBold();
                //column.Item().Text(Model.Comments);
            });
        }
    }
    
    /*public class AddressComponent : IComponent
    {
        private string Title { get; }
        private Address Address { get; }

        public AddressComponent(string title, Address address)
        {
            Title = title;
            Address = address;
        }
        
        public void Compose(IContainer container)
        {
            container.ShowEntire().Column(column =>
            {
                column.Spacing(2);

                column.Item().Text(Title).SemiBold();
                column.Item().PaddingBottom(5).LineHorizontal(1); 
                
                column.Item().Text(Address.CompanyName);
                column.Item().Text(Address.Street);
                column.Item().Text($"{Address.City}, {Address.State}");
                column.Item().Text(Address.Email);
                column.Item().Text(Address.Phone);
            });
        }
    }*/
}