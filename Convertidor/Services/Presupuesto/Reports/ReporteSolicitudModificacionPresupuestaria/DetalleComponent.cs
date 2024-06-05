using System.Globalization;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Presupuesto.ReporteSolicitudModificacion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
namespace Convertidor.Services.Rh.Report.Example;

  public class DetalleComponent : IComponent
  {
    private readonly List<DetalleReporteSolicitudModificacionDto> ModelDetalle;

    public DetalleComponent(List<DetalleReporteSolicitudModificacionDto> modelDetalle)
    {
        ModelDetalle = modelDetalle;
    }
            
        public void Compose(IContainer container)
        {
            TextDescriptor text = new TextDescriptor();
            
            var headerStyle = TextStyle.Default.SemiBold().FontSize(7);
            NumberFormatInfo formato = new CultureInfo("es-AR").NumberFormat;
            static IContainer CellStyle(IContainer container) => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
            formato.CurrencyGroupSeparator = ".";
            formato.NumberDecimalSeparator = ",";
            formato.NumberDecimalDigits = 2;
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(260);
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.ConstantColumn(350);
               
            });

            table.Header(header =>
            {
                header.Cell().Border(1).AlignCenter().Element(CellStyle).Text("CONCEPTO DE LO PRESUPUESTADO").FontSize(7);

                //header.Cell().ColumnSpan(2).RowSpan(2).Border(1).AlignCenter().Element(CellStyle).Text("IMPUTACION PRESUPUESTARIA").FontSize(7);

                header.Cell().ColumnSpan(2).Column(col =>
                {
                    col.Item().Border(1).AlignCenter().Element(CellStyle).Text("IMPUTACION PRESUPUESTARIA").FontSize(7);

                    col.Item().Row(row => 
                    {
                        row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text("ICP").FontSize(7);
                        row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text("PUC").FontSize(7);
                    });
                    

                    


                });

                header.Cell().Column(col => 
                {
                   col.Item().Border(1).AlignCenter().Element(CellStyle).Text("MONTO (Bs)").FontSize(7);
                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text("PRESUPUESTADO").FontSize(7);
                        
                        row.RelativeItem().Column(subCol => 
                        {
                            subCol.Item().Border(1).AlignCenter().Element(CellStyle).Text("A LA FECHA").FontSize(7);
                            subCol.Item().Row(subrow => 
                            {
                                
                                subrow.RelativeItem().Border(1).AlignCenter().ScaleToFit().Element(CellStyle).Text("MOD").FontSize(7);
                                subrow.RelativeItem().Border(1).AlignCenter().ScaleToFit().Element(CellStyle).Text("DESEM").FontSize(7);
                                subrow.RelativeItem().Border(1).AlignCenter().ScaleToFit().Element(CellStyle).Text("EJEC").FontSize(7);
                                subrow.RelativeItem().Border(1).AlignCenter().ScaleToFit().Element(CellStyle).Text("DISP").FontSize(7);

                            });

                            

                        });
                        row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text("SUB-PARTIDA CEDENTE").FontSize(7);

                        //row.RelativeItem().Column(subCol =>
                        //{
                        //    subCol.Item().Row(subColHeader => 
                        //    {
                        //        subColHeader.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text("PRESUPUESTADO").FontSize(7);
                        //        //subColHeader.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text("A LA FECHA").FontSize(7);
                        //        subCol.Item().Column(subColfecha =>
                        //        {
                        //            subColfecha.Item().Border(1).AlignCenter().Element(CellStyle).Text("A LA FECHA").FontSize(7);
                        //            subColfecha.Item().Border(1).AlignCenter().Element(CellStyle).Text("MODIFICACION").FontSize(7);
                        //            subColfecha.Item().Border(1).AlignCenter().Element(CellStyle).Text("DESEMBOLSO").FontSize(7);
                        //            subColfecha.Item().Border(1).AlignCenter().Element(CellStyle).Text("EJECUTADO").FontSize(7);
                        //            subColfecha.Item().Border(1).AlignCenter().Element(CellStyle).Text("DISPONIBILIDAD").FontSize(7);
                        //        });

                        //        subColHeader.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text("SUB-PARTIDA CEDENTE").FontSize(7);
                        //    });
                           

                        //    //subCol.Item().Row(subRow =>
                        //    //{
                        //    //    subRow.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text("MODIFICACION").FontSize(7);
                        //    //    subRow.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text("DESEMBOLSO").FontSize(7);
                        //    //    subRow.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text("EJECUTADO").FontSize(7);
                        //    //    subRow.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text("DISPONIBILIDAD").FontSize(7);
                        //    //});

                        //});
                        //row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text("SUB-PARTIDA CEDENTE").FontSize(7);

                       
                        
                        
                        
                    });

                });
                ////header.Cell().Border(1).AlignCenter().Element(CellStyle).Text("MONTO (Bs)").FontSize(7);

            });

            
            
            foreach (var item in ModelDetalle)
            {
                
                table.Cell().Column(column => 
                {
                    column.Item().Border(1).AlignCenter().Padding(1).Element(CellStyle).Text(item.DenominacionIcp).FontSize(7);

                    column.Item().Border(1).AlignCenter().Padding(1).Element(CellStyle).Text(item.DenominacionPuc).FontSize(7);
                  
                });

                
                table.Cell().Border(1).AlignCenter().Element(CellStyle).Text(item.CodigoIcpConcat).FontSize(7);
                table.Cell().Border(1).AlignCenter().Element(CellStyle).Text(item.CodigoPucConcat).FontSize(7);

                table.Cell().Row(row =>
                {
                    row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text(item.Monto).FontSize(7);
                    row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text(item.MontoModificado).FontSize(7);
                });
               


            }
          

        });

        }
  }
