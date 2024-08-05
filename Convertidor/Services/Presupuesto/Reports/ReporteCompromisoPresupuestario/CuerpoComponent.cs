using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Presupuesto.ReporteCompromisoPresupuestario;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

namespace Convertidor.Services.Presupuesto.ReporteCompromisoPresupuestario
{
    public class CuerpoComponent : IComponent
    {
        public List<CuerpoReporteDto> ModelCuerpo;
        public  EncabezadoReporteDto ModelEncabezado;

        public CuerpoComponent(List<CuerpoReporteDto> modelCuerpo, EncabezadoReporteDto modelEncabezado)
        {
            ModelCuerpo = modelCuerpo;
            ModelEncabezado = modelEncabezado;
        }

        public void Compose(IContainer container)
        {
            QuestPDF.Settings.CheckIfAllTextGlyphsAreAvailable = false;


            var headerStyle = TextStyle.Default.SemiBold().FontSize(7).Fallback();
            NumberFormatInfo formato = new CultureInfo("es-AR").NumberFormat;
            static IContainer CellStyle(IContainer container) => container;
            formato.CurrencyGroupSeparator = ".";
            formato.NumberDecimalSeparator = ",";
            formato.NumberDecimalDigits = 2;

          
            container.Table(table =>
            {
               
                

                table.ExtendLastCellsToTableBottom();

                table.ColumnsDefinition(columns =>
                {
                    
                    columns.ConstantColumn(320);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                

                table.Header(header =>
                {
                    table.ExtendLastCellsToTableBottom();

                    header.Cell().ColumnSpan(6).Background(Colors.Grey.Lighten2).Border(1).AlignCenter().Text("DETALLES DEL COMPROMISO").FontSize(12).Bold();
                    header.Cell().ColumnSpan(6).Row(row =>
                      {

                        row.ConstantItem(40).Border(1).AlignCenter().Element(CellStyle).Text("CANTIDAD").FontSize(7).Bold();
                        row.ConstantItem(50).Border(1).AlignCenter().Element(CellStyle).Text("   UNIDAD\n"+ "DE MEDIDA").FontSize(7).Bold();
                        row.RelativeItem(3).Border(1).AlignCenter().Element(CellStyle).PaddingLeft(50).Text("DESCRIPCION").FontSize(7).Bold();
                        row.ConstantItem(60).Border(1).AlignCenter().Element(CellStyle).Text("   PRECIO\n"+ "UNITARIO").FontSize(7).Bold();
                          row.ConstantItem(70).Column(col =>
                          {
                              col.Item().Border(1).AlignCenter().Element(CellStyle).Text("TOTAL").FontSize(7).Bold();
                              col.Item().Border(1).AlignCenter().Element(CellStyle).Text("BOLIVARES").FontSize(7).Bold();
                          });
                        


                    });

                });

              
                foreach (var item in ModelCuerpo)
                {

                    
                    table.Cell().ColumnSpan(6).Row(row =>
                    {
                        row.ConstantItem(40).BorderVertical(1).AlignCenter().PaddingRight(3).PaddingTop(5).Element(CellStyle).Text(item.Cantidad).FontSize(7);
                        row.ConstantItem(50).BorderVertical(1).AlignCenter().PaddingRight(3).PaddingTop(5).Element(CellStyle).Text(item.DescripcionUdm).FontSize(8);
                        row.RelativeItem(3).BorderVertical(1).AlignLeft().PaddingLeft(10).PaddingTop(5).Element(CellStyle).Text(item.DescripcionArticulo).FontSize(8);
                        var precio = item.PrecioUnitario.ToString("N", formato);
                        row.ConstantItem(60).BorderVertical(1).AlignRight().PaddingRight(3).PaddingTop(5).Element(CellStyle).Text(precio).FontSize(7);
                        var totalBolivares = item.TotalBolivares.ToString("N", formato);
                        row.ConstantItem(70).BorderVertical(1).AlignRight().PaddingRight(3).PaddingTop(5).Element(CellStyle).Text(totalBolivares).FontSize(7);


                    });

                    

                }

                table.Cell();

                table.Cell().Column(col =>
                {
                    
                    col.Item().Text("SE-PR-SP-PY-AC").FontSize(7).Bold().Underline();
                    col.Item().Text(ModelEncabezado.CodigoIcpConcat).FontSize(7);
                    col.Item().Text(ModelEncabezado.CodigoIcpConcat).FontSize(7);

                });

                table.Cell().Column(async col =>
                {
                   
                    var pucConcatOrdinario = ModelCuerpo.ElementAt(7);
                    var pucConcatTraspaso = ModelCuerpo.ElementAt(0);
                    col.Item().Text("GR-PA-GE-ES-SE-EX").FontSize(7).Bold().Underline();
                    
                    col.Item().Text(pucConcatTraspaso.CodigoPucConcat).FontSize(7);
                    col.Item().Text(pucConcatOrdinario.CodigoPucConcat).FontSize(7);
                });

                table.Cell().Column(col =>
                {
                    var financiado = ModelCuerpo.ElementAt(0);
                    col.Item().Text("Financiado Por").FontSize(7).Bold().Underline();
                    col.Item().AlignCenter().Text(financiado.DescripcionFinanciado).FontSize(7);
                    //if(financiado.DescripcionFinanciado.Contains("TRASPASO PRESUPUESTARIO")) 
                    //{
                    //    col.Item().Text(financiado.DescripcionFinanciado= "TRASPASO\nPRESUPUESTARIO").FontSize(7);
                    //}
                    

                });

                table.Cell().Column(col =>
                {
                    var montoIva = ModelCuerpo.Sum(x => x.MontoImpuesto);
                    var monto = montoIva.ToString("N", formato);
                    col.Item().Text("Monto").FontSize(7).Bold().Underline();
                    var montoTraspaso = ModelCuerpo.Sum(x => x.Monto);
                    var montoPresupuestario = montoTraspaso.ToString("N", formato);
                    col.Item().Text(montoPresupuestario).FontSize(7);
                    col.Item().Text(monto).FontSize(7);
                   

                });
                table.Footer(footer =>
                {


                    footer.Cell().ColumnSpan(6).ExtendHorizontal().BorderVertical(1).BorderBottom(1).BorderTop(1).PaddingRight(5).Row(row =>
                    {
                        row.RelativeItem().PaddingLeft(10).PaddingRight(5).AlignLeft().Text($"MONTO TOTAL EN LETRA :\n{ModelEncabezado.MontoEnLetras.ToUpper()}").FontSize(8).Bold();
                        
                        row.RelativeItem().PaddingBottom(4);
                        row.RelativeItem().PaddingBottom(4); 
                        row.RelativeItem().PaddingBottom(4); 
                        row.RelativeItem().PaddingBottom(4); 
                        row.RelativeItem().Width(70).AlignRight().PaddingRight(5).PaddingTop(3).Text("TOTAL").FontSize(8).Bold();
                        var bolivares = ModelCuerpo.Sum(x => x.TotalBolivares);
                        var montoImpuesto = bolivares * (decimal)0.16;
                        var total = bolivares + montoImpuesto;

                        var totalBolivares = bolivares.ToString("N", formato);
                        var totalImpuesto = montoImpuesto.ToString("N", formato);
                        var totales = total.ToString("N", formato);
                        row.RelativeItem().Width(70).BorderVertical(1).AlignRight().Element(CellStyle).BorderBottom(1).Padding(1).PaddingRight(3).Text(totalBolivares).FontSize(7);
                     
                    });


         
                    

                    footer.Cell().ColumnSpan(6).Column(col =>
                    {
                        col.Item().BorderVertical(1).BorderTop(1).PaddingLeft(3).Text("MOTIVO  :" ).FontSize(8).Bold();
                        col.Item().BorderVertical(1).PaddingLeft(3).Text(ModelEncabezado.Motivo).FontSize(7);
                    });

                    footer.Cell().Row(row =>
                    {
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().BorderVertical(1).BorderTop(1).AlignTop().AlignCenter().AlignRight().PaddingRight(15).PaddingBottom(5).Text($"ANALISTA").FontSize(8).Bold();
                            col.Item().BorderVertical(1).Text($"{ModelEncabezado.Firmante}").FontSize(7);
                            col.Item().BorderVertical(1).BorderBottom(1).PaddingLeft(4).PaddingBottom(4).Text($"FIRMA : ________________________________________     ").FontSize(8).Bold();
                        });
                       
                        

                    });

                    footer.Cell().ColumnSpan(5).BorderVertical(1).BorderBottom(1).BorderTop(1).AlignBottom().AlignCenter().Padding(3).PaddingLeft(8).PaddingBottom(5).Text($"DIRECCION DE PLANIFICACION Y PRESUPUESTO").FontSize(8).Bold();

                    
                });

                table.Cell().ColumnSpan(4).Column(col =>
                {
                    col.Item().PageBreak();
                });

               
                    table.ExtendLastCellsToTableBottom();

                    table.Cell().ColumnSpan(6).Border(1).Text("");
                    
                    table.Cell().ColumnSpan(6).Row(row =>
                    {

                        row.ConstantItem(40).Border(1).AlignCenter().PaddingVertical(20).Element(CellStyle).Text("").FontSize(7).Bold();
                        row.ConstantItem(50).Border(1).AlignCenter().PaddingVertical(20).Element(CellStyle).Text(" ").FontSize(7).Bold();
                        row.RelativeItem(3).Border(1).AlignCenter().PaddingVertical(20).Element(CellStyle).PaddingLeft(50).Text("").FontSize(7).Bold();
                        row.ConstantItem(60).Border(1).AlignCenter().PaddingVertical(20).Element(CellStyle).Text(" ").FontSize(7).Bold();
                        row.ConstantItem(70).BorderVertical(1).PaddingVertical(20).Column(col =>
                        {
                            
                            
                        });



                    });

            });
            

   

            
        }

       
    }
}
