using System.Globalization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Image = QuestPDF.Infrastructure.Image;

namespace Convertidor.Services.Rh.Report.Example
{
    public class HistoricoNominaDocument : IDocument
    {
    
        public static Image LogoImage { get; } = Image.FromFile("logo.png");
        private readonly string _patchLogo;
        public List<RhReporteNominaResumenConceptoResponseDto> Model { get; }
        public List<RhReporteFirmaResponseDto> ModelFirma { get; }
        public List<RhReporteNominaResponseDto> ModelRecibos { get; }
        public HistoricoNominaDocument(List<RhReporteNominaResumenConceptoResponseDto> model,
            List<RhReporteFirmaResponseDto> modelFirma,
            List<RhReporteNominaResponseDto> modelRecibos, 
            string patchLogo)
        {
            
            Model = model;
            ModelFirma = modelFirma;
            ModelRecibos = modelRecibos;
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
            var firstResumenBONO = 
                Model.Where(x=>x.Descripcion!=null && x.Descripcion.Length>0).FirstOrDefault();
            if (firstResumenBONO != null)
            {
                descripcion = firstResumenBONO.Descripcion;
            }
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
                 
                });
                row.RelativeItem().Column(column =>
                {
                    
                    column.Item().Text(text =>
                    {
                        text.Span($"").FontSize(7).SemiBold();;
                    });
                    column.Item().Text(text =>
                    {
                        text.Span($"PERIODO: {firstResumen.Periodo}/{firstResumen.Mes} {firstResumen.Año}").FontSize(10).SemiBold();;
                    });
                    column.Item().Text(text =>
                    {
                        text.Span($"{descripcion}").FontSize(7).SemiBold();;
                    });
                });
               
               
                row.ConstantItem(125).Image(_patchLogo);
            });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(20);
                column.Item().AlignCenter().Text($"RESUMEN FIRMA").SemiBold();
                column.Item().Element(ComposeTableFirma);
                column.Item().PageBreak();
                column.Item().AlignCenter().Text($"RESUMEN CONCEPTO").SemiBold();
                column.Item().Element(ComposeTableResumenConcepto);
                column.Item().PageBreak();
                column.Item().AlignCenter().Text($"RECIBOS").SemiBold();
                var listaPersona = from s in ModelRecibos
                group s by new
                {
                    Cedula=s.Cedula,
                  
                            
                } into g
                select new 
                {
                    Cedula=g.Key.Cedula,
                   
                            
                };
                foreach (var itemPersona in listaPersona)
                {
                    var persona = ModelRecibos.Where(x => x.Cedula == itemPersona.Cedula).FirstOrDefault();
                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Component(new PersonaComponent("", persona));
                  
                    });
                    
                    var personaRecibo = ModelRecibos.Where(x => x.Cedula == itemPersona.Cedula).ToList();
                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Component(new ReciboComponent("", personaRecibo));
                  
                    });
                    
                    
                }
               
                
                
                //column.Item().Element(ComposeTableRecibo);
                
                
                
              
            });
         
        }

        void ComposeTableResumenConcepto(IContainer container)
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
                    //var index = Model.ToList().IndexOf(item) + 1;
    
                    table.Cell().Element(CellStyle).Text($"{item.NumeroConcepto}").FontSize(7);
                    table.Cell().Element(CellStyle).Text(item.DenominacionConcepto).FontSize(7);
                    var asignacion = item.Asignacion.ToString("N", formato);
                    var deduccion = item.Deduccion.ToString("N", formato);
                    table.Cell().Element(CellStyle).AlignRight().Text($"{asignacion}").FontSize(7);
                    table.Cell().Element(CellStyle).AlignRight().Text($"{deduccion}").FontSize(7);
                 
                    
                    //static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    static IContainer CellStyle(IContainer container) => container.PaddingVertical(2);

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
        void ComposeTableFirma(IContainer container)
        {
            var headerStyle = TextStyle.Default.SemiBold().FontSize(7);
            NumberFormatInfo formato = new CultureInfo("es-AR").NumberFormat;

            formato.CurrencyGroupSeparator = ".";
            formato.NumberDecimalSeparator = ",";
            formato.NumberDecimalDigits = 2;
            var listaOficina = from s in ModelFirma
                group s by new
                {
                    Oficina=s.Oficina,
                    NombreOficina=s.NombreOficina
                                
                } into g
                select new 
                {
                    Oficina=g.Key.Oficina,
                    NombreOficina=g.Key.NombreOficina
                                
                                
                };
           
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(180);
                    columns.ConstantColumn(50);
                    columns.ConstantColumn(150);
                    columns.ConstantColumn(100);
                });
               
                table.Header(header =>
                {
                    header.Cell().Text("NOMBRE").Style(headerStyle);
                    header.Cell().Text("CEDULA").Style(headerStyle);
                    header.Cell().Text("CARGO").Style(headerStyle);
                    header.Cell().Text("FIRMA").Style(headerStyle);
                    
                    header.Cell().ColumnSpan(4).PaddingTop(5).BorderBottom(1).BorderColor(Colors.Black);
                });
                foreach (var itemOficina in listaOficina.OrderBy(x=>x.Oficina).ToList())
                {

                    var modelFirmaFiltered = ModelFirma.Where(x => x.NombreOficina == itemOficina.NombreOficina).ToList();
                   
                    
                    table.Cell().ColumnSpan(4).PaddingTop(5).Text($"{itemOficina.NombreOficina}");
                   
                
              
                    foreach (var item in modelFirmaFiltered.OrderByDescending(x=>x.Orden).ToList())
                    {
                        //var index = Model.ToList().IndexOf(item) + 1;
                       
                        table.Cell().Element(CellStyle).Text($"{item.AccionResponsable} {item.Nombre} {item.Apellido}").FontSize(7);
                        table.Cell().Element(CellStyle).Text($"{item.Cedula}").FontSize(7);
                        table.Cell().Element(CellStyle).Text(item.DescripcionCargo).FontSize(7);
                        table.Cell().Element(CellStyle).Text("                      ").FontSize(7);
               
             
                
                        static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(10);
                        //static IContainer CellStyle(IContainer container) => container.PaddingVertical(2);

                    }

                }
                
              

              
                
                
              
            });
        }
     
    void ComposeTableRecibo(IContainer container)
        {
            var headerStyle = TextStyle.Default.SemiBold().FontSize(7);
            NumberFormatInfo formato = new CultureInfo("es-AR").NumberFormat;

            formato.CurrencyGroupSeparator = ".";
            formato.NumberDecimalSeparator = ",";
            formato.NumberDecimalDigits = 2;
            
            var listaPersona = from s in ModelRecibos
                group s by new
                {
                    Cedula=s.Cedula,
                    Nombre=s.Nombre,
                    Cargo=s.DenominacionCargo,
                    CodigoPersona=s.CodigoPersona,
                    FechaIngreso=s.FechaIngreso,
                    Banco=s.Banco,
                    NoCuenta=s.NoCuenta
                                
                } into g
                select new 
                {
                    Cedula=g.Key.Cedula,
                    Nombre=g.Key.Nombre,
                    Cargo=g.Key.Cargo,
                    CodigoPersona=g.Key.CodigoPersona,
                    FechaIngreso=g.Key.FechaIngreso,
                    Banco=g.Key.Banco,  
                    NoCuenta=g.Key.NoCuenta, 
                                
                };
            
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                   
                    
                    
                    columns.ConstantColumn(35);
                    columns.ConstantColumn(25);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });
                table.Cell().ColumnSpan(2).Text("2");
                table.Cell().Text("3");
                table.Header(header =>
                {
                   
                    
                    header.Cell().Text("TIPO").Style(headerStyle);
                    header.Cell().Text("Nro").Style(headerStyle);
                    header.Cell().Text("CONCEPTO").Style(headerStyle);
                    header.Cell().Text("COMPLEMENTO").Style(headerStyle);
                    header.Cell().AlignRight().Text("ASIGNACIONES").Style(headerStyle);
                    header.Cell().AlignRight().Text("DEDUCCIONES").Style(headerStyle);
                    //header.Cell().AlignRight().Text("").Style(headerStyle);
                    
                    header.Cell().ColumnSpan(6).PaddingTop(5).BorderBottom(1).BorderColor(Colors.Black);
                });
                foreach (var itemPersona in listaPersona)
                {
                   
                    var modelReciboFiltered = ModelRecibos.Where(x => x.Cedula == itemPersona.Cedula).ToList();
                    table.Cell().ColumnSpan(6).PaddingTop(5).Text($"{itemPersona.Cedula} {itemPersona.Nombre}").FontSize(7);
                    foreach (var item in modelReciboFiltered)
                    {
                      

                        //var index = Model.ToList().IndexOf(item) + 1;
                        table.Cell().Element(CellStyle).Text($"{item.TipoMovConcepto}").FontSize(7);
                        table.Cell().Element(CellStyle).Text($"{item.NumeroConcepto}").FontSize(7);
                        table.Cell().Element(CellStyle).Text(item.DenominacionConcepto).FontSize(7);
                        table.Cell().Element(CellStyle).Text(item.ComplementoConcepto).FontSize(7);
                        var asignacion = item.Asignacion.ToString("N", formato);
                        var deduccion = item.Deduccion.ToString("N", formato);
                        table.Cell().Element(CellStyle).AlignRight().Text($"{asignacion}").FontSize(7);
                        table.Cell().Element(CellStyle).AlignRight().Text($"{deduccion}").FontSize(7);
                 
                    
                        static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(2);
                        //static IContainer CellStyle(IContainer container) => container.PaddingVertical(2);

                    }
               
                    table.Cell().Text($"").FontSize(7);
                    table.Cell().Text($"").FontSize(7);
                    table.Cell().Text($"").FontSize(7);
                    table.Cell().AlignRight().Text($"Total:").FontSize(10).SemiBold();
                    var asig = Model.Sum(x => x.Asignacion);
                    var ded = Model.Sum(x => x.Deduccion);
                    var totalAsignacion = asig.ToString("N", formato);
                    var totalDeduccion = ded.ToString("N", formato);
                    table.Cell().AlignRight().Text($"{totalAsignacion}").FontSize(10).SemiBold();
                    table.Cell().AlignRight().Text($"{totalDeduccion}").FontSize(10).SemiBold();
                    
                    
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
    

    
}
  