using System.Globalization;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Presupuesto.ReporteSolicitudModificacion;
using NPOI.SS.Formula.Functions;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
namespace Convertidor.Services.Presupuesto.Reports.ReporteSolicitudModificacionPresupuestaria;

  public class DetalleComponent : IComponent
  {
    private readonly List<DetalleReporteSolicitudModificacionDto> ModelDetalle;

    public DetalleComponent(List<DetalleReporteSolicitudModificacionDto> modelDetalle)
    {
        ModelDetalle = modelDetalle;
    }
            
        public void Compose(IContainer container)
        {
            
            
            var headerStyle = TextStyle.Default.SemiBold().FontSize(7);
            NumberFormatInfo formato = new CultureInfo("es-AR").NumberFormat;
        static IContainer CellStyle(IContainer container) => container;
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
                table.ExtendLastCellsToTableBottom();
                
                header.Cell().Border(1).AlignCenter().Element(CellStyle).Text("CONCEPTO DE LO PRESUPUESTADO").FontSize(7);

                header.Cell().ColumnSpan(2).RowSpan(3).Column(col =>
                {
                    
                    col.Item().Border(1).AlignCenter().PaddingBottom(10).Element(CellStyle).Text("IMPUTACION PRESUPUESTARIA").FontSize(7);
                    

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
                        
                        row.ConstantItem(220).Column(subCol => 
                        {
                            subCol.Item().Border(1).AlignCenter().Element(CellStyle).Text("A LA FECHA").FontSize(7);
                            subCol.Item().Row(subrow => 
                            {
                                
                                subrow.RelativeItem().Border(1).AlignCenter().ScaleToFit().Element(CellStyle).Text("MODIFICADO").FontSize(7);
                                subrow.RelativeItem().Border(1).AlignCenter().ScaleToFit().Element(CellStyle).Text("DESEMBOLSO").FontSize(7);
                                subrow.RelativeItem().Border(1).AlignCenter().ScaleToFit().Element(CellStyle).Text("EJECUTADO").FontSize(7);
                                subrow.RelativeItem().Border(1).AlignCenter().ScaleToFit().Element(CellStyle).Text("DISPONIBLE").FontSize(7);

                            });

                            

                        });
                        row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text("SUB-PARTIDA CEDENTE").FontSize(7);

                       
                    });

                });
               

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
                    row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text(item.Presupuestado).FontSize(7);
                    row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text(item.MontoModificado).FontSize(7);
                    row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text(item.TotalDesembolso).FontSize(7);
                    row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text(item.Ejecutado).FontSize(7);
                    row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text(item.Disponible).FontSize(7);
                    row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text(item.Monto).FontSize(7);

                   

                });
               
            }
            table.Cell().ColumnSpan(3).RowSpan(2).Border(1).AlignCenter().Element(CellStyle).Text("TOTALES").FontSize(7);

                    var presupuestado = ModelDetalle.Sum(x => x.Presupuestado);
                    var montoModificado = ModelDetalle.Sum(x => x.MontoModificado);
                    var totalDesembolso = ModelDetalle.Sum(x => x.TotalDesembolso);
                    var ejecutado = ModelDetalle.Sum(x => x.Ejecutado);
                    var disponible = ModelDetalle.Sum(x => x.Disponible);
                    var monto = ModelDetalle.Sum(x => x.Monto);
            table.Cell().Row(row =>
            {
                row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text(presupuestado).FontSize(7);
                row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text(montoModificado).FontSize(7);
                row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text(totalDesembolso).FontSize(7);
                row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text(ejecutado).FontSize(7);
                row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text(disponible).FontSize(7);
                row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text(monto).FontSize(7);
            });

               

            

            table.Cell().ColumnSpan(4).Row(row => 
            {
                row.ConstantItem(50).Border(1).AlignCenter().Element(CellStyle).Text("N°").FontSize(7);
                row.RelativeColumn(2).Border(1).AlignCenter().Element(CellStyle).Text("METAS BENEFICIADAS").FontSize(7);
                row.ConstantItem(100).Border(1).AlignCenter().Element(CellStyle).Text("GRADO DE \n"+" "+"AFECTACION(%)").FontSize(7);
            });

            table.Cell().ColumnSpan(4).Row(row => 
            {
                row.ConstantItem(50).Border(1).AlignCenter().Element(CellStyle).Text("   ").FontSize(7);
                row.RelativeColumn(2).Border(1).AlignCenter().Element(CellStyle).Text("Control y Gestión de la relación de H.C.M, Seguro de Vida y Accidentes Personales, Servicio Funerario," +
                    " Servicio de Prevención y Salud Laboral. Seguimiento y Control en la Relación de Aportes de Ley \n" +"  " +
                    ""+ "(S.S.O. R.P.E, F.A.O.V, F.E.J.P), Caja de Ahorro y Seguimiento y Control en la Relación de Bono de Alimentaciòn y Bono Salud. ").FontSize(7);
                row.ConstantItem(50).Border(1).AlignCenter().Element(CellStyle).Text("%").FontSize(7);
            });

            table.Cell().ColumnSpan(4).Border(1).AlignLeft().Element(CellStyle).Text("   EFECTO FINANCIERO: SE CUENTA CON RECURSOS DISPONIBLES DEL PRESUPUESTO ORDINARIO").FontSize(7);

            
        });
       
    }
  }
