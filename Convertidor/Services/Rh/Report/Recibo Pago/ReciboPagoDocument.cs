using System.Globalization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Image = QuestPDF.Infrastructure.Image;

namespace Convertidor.Services.Rh.Report.Example
{
    public class ReciboPagoDocument : IDocument
    {

        public static Image LogoImage { get; } = Image.FromFile("logo.png");


        private readonly string _patchLogo;
        private readonly string _descripcionTipoNomina;
        private readonly bool _ImprimirMarcaAgua;

        public List<RhReporteNominaResponseDto> ModelRecibos { get; }
        public ReciboPagoDocument(
            List<RhReporteNominaResponseDto> modelRecibos,
            string patchLogo, string descripcionTipoNomina, bool ImprimirMarcaAgua)
        {


            ModelRecibos = modelRecibos;

            _patchLogo = patchLogo;
            _descripcionTipoNomina = descripcionTipoNomina;
            _ImprimirMarcaAgua = ImprimirMarcaAgua;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    
                    const float horizontalMargin = 0.5f;
                    const float verticalMargin = 1f;
                    if (_ImprimirMarcaAgua == true)
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(15);
                        page.MarginVertical(verticalMargin, Unit.Inch);
                        page.MarginHorizontal(horizontalMargin, Unit.Inch);
                        page.Header().Element(ComposeHeader);
                        page.Content().Element(ComposeContent);
                        page.Background()


                        .PaddingVertical(verticalMargin, Unit.Inch)

                        .RotateRight()

                        .Decoration(decoration =>

                        {

                            decoration.Before().RotateRight().RotateRight().Element(DrawSide);

                            decoration.Content().Extend();

                            decoration.After().Element(DrawSide);


                            void DrawSide(IContainer container)
                            {
                                container
                                    .Height(horizontalMargin, Unit.Inch)
                                    .AlignMiddle()
                                    .Row(row =>
                                    {
                                        row.AutoItem().PaddingRight(16).Text("PROVISIONAL").FontSize(16).FontColor(Colors.Red.Medium);
                                        row.RelativeItem().PaddingTop(12).ExtendHorizontal().LineHorizontal(2).LineColor(Colors.Red.Medium);
                                    });
                            }
                        });

                        

                        void ComposeHeader(IContainer container)
                        {

                            var descripcion = "";



                            container.Row(row =>
                            {
                                row.RelativeItem().Column(column =>
                                {
                                    //column.Item().Text($"RECIBOS")
                                    //    .FontSize(7).SemiBold();

                                    column.Item().Text(text =>
                                    {
                                        //text.Span($"{firstResumen.TipoNomina}").FontSize(7).SemiBold();;

                                    });

                                });
                                row.RelativeItem().Column(column =>
                                {

                                    column.Item().Text(text =>
                                    {
                                        text.Span($"").FontSize(7).SemiBold(); ;
                                    });
                                    //column.Item().Text(text =>
                                    //{
                                    //    text.Span($"RECIBOS DE PAGO").FontSize(10).SemiBold();
                                    //});
                                    //column.Item().Text(text =>
                                    //{
                                    //    text.Span($"{descripcion}").FontSize(7).SemiBold();;
                                    //});
                                });


                                row.ConstantItem(125).Image(_patchLogo);
                            });
                        }

                        void ComposeContent(IContainer container)
                        {
                            container.PaddingVertical(40).Column(async column =>
                            {

                                var listaDepartamento = from s in ModelRecibos
                                                   group s by new
                                                   {
                                                       CodigoIcpConcat = s.CodigoIcpConcat,


                                                   } into g
                                                   select new
                                                   {
                                                       CodigoIcpConcat = g.Key.CodigoIcpConcat,


                                                   };
                                int contador = 0;
                                foreach (var itemDepartamento in listaDepartamento)
                                {


                                    if (contador % 2 != 0)
                                    {

                                        column.Item().PageBreak();
                                        column.Item().AlignCenter().ShowOnce().Text($"DEPARTAMENTO : {itemDepartamento.CodigoIcpConcat}").SemiBold();

                                        column.Item().PageBreak();

                                    }
                                    else 
                                    {
                                        column.Item().AlignCenter().ShowOnce().Text($"DEPARTAMENTO : {itemDepartamento.CodigoIcpConcat}").SemiBold();
                                        column.Item().PageBreak();
                                    }

                                    var listaPersona = from s in ModelRecibos.Where(x=> x.CodigoIcpConcat == itemDepartamento.CodigoIcpConcat)
                                                       group s by new
                                                       {
                                                           Cedula = s.Cedula,


                                                       } into g
                                                       select new
                                                       {
                                                           Cedula = g.Key.Cedula,


                                                       };
                                    



                                    foreach (var itemPersona in listaPersona)
                                    {
                                        contador++;
                                      
                                            var persona = ModelRecibos.Where(x => x.Cedula == itemPersona.Cedula).FirstOrDefault();
                                          
                                            column.Item().Row(row =>
                                            {
                                                row.RelativeItem().Component(new PersonasComponent("", persona, _descripcionTipoNomina));

                                            });

                                            var personaRecibo = ModelRecibos.Where(x => x.Cedula == itemPersona.Cedula).ToList();
                                            column.Item().Row(row =>
                                            {
                                                row.RelativeItem().Component(new ReciboPagoComponent("", personaRecibo, contador));

                                            });

                                            column.Item().Row(row =>
                                            {
                                                row.RelativeItem().Component(new PieReciboComponent(contador));

                                            });
                                        

                                    }
                                }


                               



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
                                    header.Cell().Text("CONCEPTO").Style(headerStyle);
                                    header.Cell().Text("COMPLEMENTO").Style(headerStyle);
                                    header.Cell().Text("%").Style(headerStyle);
                                    header.Cell().AlignRight().Text("ASIGNACIONES").Style(headerStyle);
                                    header.Cell().AlignRight().Text("DEDUCCIONES").Style(headerStyle);
                                    //header.Cell().AlignRight().Text("").Style(headerStyle);

                                    //header.Cell().ColumnSpan(4).PaddingTop(5).BorderBottom(1).BorderColor(Colors.Black);
                                });

                                foreach (var item in ModelRecibos.ToList())
                                {
                                    //var index = Model.ToList().IndexOf(item) + 1;

                                    table.Cell().Element(CellStyle).Text($"{item.DenominacionConcepto}").FontSize(7);
                                    table.Cell().Element(CellStyle).Text(item.NumeroConcepto).FontSize(7);
                                    table.Cell().Element(CellStyle).Text(item.Porcentaje).FontSize(7);
                                    var asignacion = item.Asignacion.ToString("N", formato);
                                    var deduccion = item.Deduccion.ToString("N", formato);
                                    table.Cell().Element(CellStyle).AlignRight().Text($"{asignacion}").FontSize(7);
                                    table.Cell().Element(CellStyle).AlignRight().Text($"{deduccion}").FontSize(7);


                                    //static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                                    static IContainer CellStyle(IContainer container) => container.Border(1);

                                }
                                table.Cell().Text($"").FontSize(7);
                                table.Cell().AlignRight().Text($"Total:").FontSize(10).SemiBold();
                                var asig = ModelRecibos.Sum(x => x.Asignacion);
                                var ded = ModelRecibos.Sum(x => x.Deduccion);
                                var totalAsignacion = asig.ToString("N", formato);
                                var totalDeduccion = ded.ToString("N", formato);
                                table.Cell().AlignRight().Text($"{totalAsignacion}").FontSize(10).SemiBold();
                                table.Cell().AlignRight().Text($"{totalDeduccion}").FontSize(10).SemiBold();
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
                                                   Cedula = s.Cedula,
                                                   Nombre = s.Nombre,
                                                   Cargo = s.DenominacionCargo,
                                                   CodigoPersona = s.CodigoPersona,
                                                   FechaIngreso = s.FechaIngreso,
                                                   Banco = s.Banco,
                                                   NoCuenta = s.NoCuenta

                                               } into g
                                               select new
                                               {
                                                   Cedula = g.Key.Cedula,
                                                   Nombre = g.Key.Nombre,
                                                   Cargo = g.Key.Cargo,
                                                   CodigoPersona = g.Key.CodigoPersona,
                                                   FechaIngreso = g.Key.FechaIngreso,
                                                   Banco = g.Key.Banco,
                                                   NoCuenta = g.Key.NoCuenta,

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
                                //table.Cell().ColumnSpan(2).Text("2");
                                //table.Cell().Text("3");
                                table.Header(header =>
                                {


                                    header.Cell().Text("TIPO").Style(headerStyle);
                                    header.Cell().Text("Nro").Style(headerStyle);
                                    header.Cell().Text("CONCEPTO").Style(headerStyle);
                                    header.Cell().Text("COMPLEMENTO").Style(headerStyle);
                                    header.Cell().AlignRight().Text("ASIGNACIONES").Style(headerStyle);
                                    header.Cell().AlignRight().Text("DEDUCCIONES").Style(headerStyle);
                                    //header.Cell().AlignRight().Text("").Style(headerStyle);

                                    //header.Cell().ColumnSpan(6).PaddingTop(5).BorderBottom(1).BorderColor(Colors.Black);
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
                                    var asig = ModelRecibos.Sum(x => x.Asignacion);
                                    var ded = ModelRecibos.Sum(x => x.Deduccion);
                                    var totalAsignacion = asig.ToString("N", formato);
                                    var totalDeduccion = ded.ToString("N", formato);
                                    table.Cell().AlignRight().Text($"{totalAsignacion}").FontSize(10).SemiBold();
                                    table.Cell().AlignRight().Text($"{totalDeduccion}").FontSize(10).SemiBold();


                                }




                            });
                        }

                    }
                    else 
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(15);
                      
                        page.Header().Element(ComposeHeader);
                        page.Content().Element(ComposeContent);
                    

                       



                        void ComposeHeader(IContainer container)
                        {

                            var descripcion = "";



                            container.Row(row =>
                            {
                                row.RelativeItem().Column(column =>
                                {
                                    //column.Item().Text($"RECIBOS")
                                    //    .FontSize(7).SemiBold();

                                    column.Item().Text(text =>
                                    {
                                        //text.Span($"{firstResumen.TipoNomina}").FontSize(7).SemiBold();;

                                    });

                                });
                                row.RelativeItem().Column(column =>
                                {

                                    column.Item().Text(text =>
                                    {
                                        text.Span($"").FontSize(7).SemiBold(); ;
                                    });
                                    //column.Item().Text(text =>
                                    //{
                                    //    text.Span($"RECIBOS DE PAGO").FontSize(10).SemiBold();
                                    //});
                                    //column.Item().Text(text =>
                                    //{
                                    //    text.Span($"{descripcion}").FontSize(7).SemiBold();;
                                    //});
                                });


                                row.ConstantItem(125).Image(_patchLogo);
                            });
                        }

                        void ComposeContent(IContainer container)
                        {
                            container.PaddingVertical(40).Column(async column =>
                            {

                                var listaDepartamento = from s in ModelRecibos
                                                        group s by new
                                                        {
                                                            CodigoIcpConcat = s.CodigoIcpConcat,


                                                        } into g
                                                        select new
                                                        {
                                                            CodigoIcpConcat = g.Key.CodigoIcpConcat,


                                                        };
                                int contador = 0;
                                foreach (var itemDepartamento in listaDepartamento)
                                {


                                    if (contador % 2 != 0)
                                    {

                                        column.Item().PageBreak();
                                        column.Item().AlignCenter().ShowOnce().Text($"DEPARTAMENTO : {itemDepartamento.CodigoIcpConcat}").SemiBold();

                                        column.Item().PageBreak();

                                    }
                                    else
                                    {
                                        column.Item().AlignCenter().ShowOnce().Text($"DEPARTAMENTO : {itemDepartamento.CodigoIcpConcat}").SemiBold();
                                        column.Item().PageBreak();
                                    }

                                    var listaPersona = from s in ModelRecibos.Where(x => x.CodigoIcpConcat == itemDepartamento.CodigoIcpConcat)
                                                       group s by new
                                                       {
                                                           Cedula = s.Cedula,


                                                       } into g
                                                       select new
                                                       {
                                                           Cedula = g.Key.Cedula,


                                                       };




                                    foreach (var itemPersona in listaPersona)
                                    {
                                        contador++;

                                        var persona = ModelRecibos.Where(x => x.Cedula == itemPersona.Cedula).FirstOrDefault();

                                        column.Item().Row(row =>
                                        {
                                            row.RelativeItem().Component(new PersonasComponent("", persona, _descripcionTipoNomina));

                                        });

                                        var personaRecibo = ModelRecibos.Where(x => x.Cedula == itemPersona.Cedula).ToList();
                                        column.Item().Row(row =>
                                        {
                                            row.RelativeItem().Component(new ReciboPagoComponent("", personaRecibo, contador));

                                        });

                                        column.Item().Row(row =>
                                        {
                                            row.RelativeItem().Component(new PieReciboComponent(contador));

                                        });


                                    }
                                }






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
                                    header.Cell().Text("CONCEPTO").Style(headerStyle);
                                    header.Cell().Text("COMPLEMENTO").Style(headerStyle);
                                    header.Cell().Text("%").Style(headerStyle);
                                    header.Cell().AlignRight().Text("ASIGNACIONES").Style(headerStyle);
                                    header.Cell().AlignRight().Text("DEDUCCIONES").Style(headerStyle);
                                    //header.Cell().AlignRight().Text("").Style(headerStyle);

                                    //header.Cell().ColumnSpan(4).PaddingTop(5).BorderBottom(1).BorderColor(Colors.Black);
                                });

                                foreach (var item in ModelRecibos.ToList())
                                {
                                    //var index = Model.ToList().IndexOf(item) + 1;

                                    table.Cell().Element(CellStyle).Text($"{item.DenominacionConcepto}").FontSize(7);
                                    table.Cell().Element(CellStyle).Text(item.NumeroConcepto).FontSize(7);
                                    table.Cell().Element(CellStyle).Text(item.Porcentaje).FontSize(7);
                                    var asignacion = item.Asignacion.ToString("N", formato);
                                    var deduccion = item.Deduccion.ToString("N", formato);
                                    table.Cell().Element(CellStyle).AlignRight().Text($"{asignacion}").FontSize(7);
                                    table.Cell().Element(CellStyle).AlignRight().Text($"{deduccion}").FontSize(7);


                                    //static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                                    static IContainer CellStyle(IContainer container) => container.Border(1);

                                }
                                table.Cell().Text($"").FontSize(7);
                                table.Cell().AlignRight().Text($"Total:").FontSize(10).SemiBold();
                                var asig = ModelRecibos.Sum(x => x.Asignacion);
                                var ded = ModelRecibos.Sum(x => x.Deduccion);
                                var totalAsignacion = asig.ToString("N", formato);
                                var totalDeduccion = ded.ToString("N", formato);
                                table.Cell().AlignRight().Text($"{totalAsignacion}").FontSize(10).SemiBold();
                                table.Cell().AlignRight().Text($"{totalDeduccion}").FontSize(10).SemiBold();
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
                                                   Cedula = s.Cedula,
                                                   Nombre = s.Nombre,
                                                   Cargo = s.DenominacionCargo,
                                                   CodigoPersona = s.CodigoPersona,
                                                   FechaIngreso = s.FechaIngreso,
                                                   Banco = s.Banco,
                                                   NoCuenta = s.NoCuenta

                                               } into g
                                               select new
                                               {
                                                   Cedula = g.Key.Cedula,
                                                   Nombre = g.Key.Nombre,
                                                   Cargo = g.Key.Cargo,
                                                   CodigoPersona = g.Key.CodigoPersona,
                                                   FechaIngreso = g.Key.FechaIngreso,
                                                   Banco = g.Key.Banco,
                                                   NoCuenta = g.Key.NoCuenta,

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
                                //table.Cell().ColumnSpan(2).Text("2");
                                //table.Cell().Text("3");
                                table.Header(header =>
                                {


                                    header.Cell().Text("TIPO").Style(headerStyle);
                                    header.Cell().Text("Nro").Style(headerStyle);
                                    header.Cell().Text("CONCEPTO").Style(headerStyle);
                                    header.Cell().Text("COMPLEMENTO").Style(headerStyle);
                                    header.Cell().AlignRight().Text("ASIGNACIONES").Style(headerStyle);
                                    header.Cell().AlignRight().Text("DEDUCCIONES").Style(headerStyle);
                                    //header.Cell().AlignRight().Text("").Style(headerStyle);

                                    //header.Cell().ColumnSpan(6).PaddingTop(5).BorderBottom(1).BorderColor(Colors.Black);
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
                                    var asig = ModelRecibos.Sum(x => x.Asignacion);
                                    var ded = ModelRecibos.Sum(x => x.Deduccion);
                                    var totalAsignacion = asig.ToString("N", formato);
                                    var totalDeduccion = ded.ToString("N", formato);
                                    table.Cell().AlignRight().Text($"{totalAsignacion}").FontSize(10).SemiBold();
                                    table.Cell().AlignRight().Text($"{totalDeduccion}").FontSize(10).SemiBold();


                                }




                            });
                        }
                    }

                    
                });
        }
    }
}
  