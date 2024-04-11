using System.Globalization;
using System.Linq;
using iText.Layout.Element;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
namespace Convertidor.Services.Rh.Report.Example;

  public class TituloComponent : IComponent
    {
        private string Title { get; }
      

        public TituloComponent(string title)
        {
            Title = title;
         
        }
            
        public void Compose(IContainer container)
        {
            var headerStyle = TextStyle.Default.SemiBold().FontSize(7);
            NumberFormatInfo formato = new CultureInfo("es-AR").NumberFormat;
            static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(2);
            formato.CurrencyGroupSeparator = ".";
            formato.NumberDecimalSeparator = ",";
            formato.NumberDecimalDigits = 2;
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {

                    columns.RelativeColumn();
                   
                });
               
              
                table.Cell().Element(CellStyle).Text($"{Title}").FontSize(7);
               
                
          
            });

        }
    }
