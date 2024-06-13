using System.Globalization;
using AutoMapper.Internal;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Adm.ReporteSolicitudCompromiso;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Presupuesto.ReporteSolicitudModificacion;
using NPOI.HSSF.Record;
using NPOI.SS.Formula.Functions;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
namespace Convertidor.Services.Adm.Reports.ReporteSolicitudCompromiso;

  public class DetalleSolicitudComponent : IComponent
  {
    private readonly List<DetalleSolicitudcompromisoDto> ModelDetalle;


    public DetalleSolicitudComponent(List<DetalleSolicitudcompromisoDto> modelDetalle)
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
            string dePara = "";

            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(350);
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.ConstantColumn(500);

            });

            table.Header(header =>
            {
                table.ExtendLastCellsToTableBottom();

                header.Cell().BorderTop(1).BorderLeft(1).BorderBottom(1).AlignCenter().Element(CellStyle).Text("CONCEPTO DE LO PRESUPUESTADO").FontSize(8);

                header.Cell().RowSpan(3).Width(160).Column(col =>
                {

                    col.Item().BorderTop(1).BorderLeft(1).BorderBottom(1).AlignCenter().Element(CellStyle).PaddingBottom(10).Text("IMPUTACION PRESUPUESTARIA").FontSize(8);


                    col.Item().Row(row =>
                    {
                        row.ConstantItem(80).BorderTop(1).BorderLeft(1).AlignCenter().Element(CellStyle).Text("ICP").FontSize(8);
                        row.ConstantItem(80).BorderTop(1).BorderLeft(1).AlignCenter().Element(CellStyle).Text("PUC").FontSize(8);
                    });


                });

                header.Cell().ColumnSpan(2).Column(col =>
                {
                    col.Item().Border(1).AlignCenter().Element(CellStyle).Text("MONTO (Bs)").FontSize(8);
                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text("PRESUPUESTADO").FontSize(8);

                        row.ConstantItem(440).Column(subCol =>
                        {
                            subCol.Item().Border(1).AlignCenter().Element(CellStyle).Text("A LA FECHA").FontSize(8);
                            subCol.Item().Row(subrow =>
                            {

                                subrow.RelativeItem().Border(1).AlignCenter().ScaleToFit().Element(CellStyle).Text("MODIFICADO").FontSize(8);
                                subrow.RelativeItem().Border(1).AlignCenter().ScaleToFit().Element(CellStyle).Text("DESEMBOLSO").FontSize(8);
                                subrow.RelativeItem().Border(1).AlignCenter().ScaleToFit().Element(CellStyle).Text("EJECUTADO").FontSize(8);
                                subrow.RelativeItem().Border(1).AlignCenter().ScaleToFit().Element(CellStyle).Text("DISPONIBLE").FontSize(8);

                            });



                        });
                        row.RelativeItem().Border(1).AlignCenter().Element(CellStyle).Text("SUB-PARTIDA CEDENTE").FontSize(8);


                    });

                });


            });



            ////foreach (var item in ModelDetalle)
            ////{
            ////    dePara = item.DePara;

            ////    table.Cell().Column(column =>
            ////    {
            ////        column.Item().BorderTop(1).BorderLeft(1).BorderBottom(1).AlignCenter().Padding(1).Element(CellStyle).Text(item.DenominacionIcp).FontSize(7);

            ////        column.Item().BorderTop(1).BorderLeft(1).BorderBottom(1).AlignCenter().Padding(1).Element(CellStyle).Text(item.DenominacionPuc).FontSize(7);

            ////    });

            ////    table.Cell().Row(row =>
            ////    {
            ////        row.ConstantItem(80).BorderTop(1).BorderLeft(1).BorderBottom(1).AlignCenter().Element(CellStyle).Text(item.CodigoIcpConcat).FontSize(7);
            ////        row.ConstantItem(80).BorderTop(1).BorderLeft(1).BorderBottom(1).AlignCenter().Element(CellStyle).Text(item.CodigoPucConcat).FontSize(7);
            ////    });
            ////    table.Cell().ColumnSpan(2).Row(row =>
            ////    {
            ////        var presupuesto = item.Presupuestado.ToString("N", formato);
            ////        var modificado = item.MontoModificado.ToString("N", formato);
            ////        var desembolsado = item.TotalDesembolso.ToString("N", formato);
            ////        var ejecuta = item.Ejecutado.ToString("N", formato);
            ////        var disponibilidad = item.Disponible?.ToString("N", formato);
            ////        var montos = item.Monto.ToString("N", formato);

            ////        row.RelativeItem().Border(1).AlignRight().PaddingRight(2).Element(CellStyle).Text($"{presupuesto}").FontSize(7);
            ////        row.RelativeItem().Border(1).AlignRight().PaddingRight(2).Element(CellStyle).Text($"{modificado}").FontSize(7);
            ////        row.RelativeItem().Border(1).AlignRight().PaddingRight(2).Element(CellStyle).Text($"{desembolsado}").FontSize(7);
            ////        row.RelativeItem().Border(1).AlignRight().PaddingRight(2).Element(CellStyle).Text($"{ejecuta}").FontSize(7);
            ////        row.RelativeItem().Border(1).AlignRight().PaddingRight(2).Element(CellStyle).Text($"{disponibilidad}").FontSize(7);
            ////        row.RelativeItem().Border(1).AlignRight().PaddingRight(2).Element(CellStyle).Text($"{montos}").FontSize(7);

            ////    });

            ////}
            ////table.Cell().ColumnSpan(2).RowSpan(2).BorderTop(1).BorderLeft(1).BorderBottom(1).AlignCenter().Element(CellStyle).Text("TOTALES").FontSize(8);

            ////var presupuestado = ModelDetalle.Sum(x => x.Presupuestado);
            ////var montoModificado = ModelDetalle.Sum(x => x.MontoModificado);
            ////var Desembolso = ModelDetalle.Sum(x => x.TotalDesembolso);
            ////var ejecutado = ModelDetalle.Sum(x => x.Ejecutado);
            ////var disponible = ModelDetalle.Sum(x => x.Disponible);
            ////var monto = ModelDetalle.Sum(x => x.Monto);

            ////var totalPresupuestado = presupuestado.ToString("N", formato);
            ////var totalmontoModificado = montoModificado.ToString("N", formato);
            ////var totalDesembolso = Desembolso.ToString("N", formato);
            ////var totalEjecutado = ejecutado.ToString("N", formato);
            ////var totalDisponible = disponible?.ToString("N", formato);
            ////var totalMonto = monto.ToString("N", formato);


            ////table.Cell().ColumnSpan(2).Row(row =>
            ////{
            ////    row.RelativeItem().Border(1).AlignRight().PaddingRight(2).Element(CellStyle).Text($"{totalPresupuestado}").FontSize(7);
            ////    row.RelativeItem().Border(1).AlignRight().PaddingRight(2).Element(CellStyle).Text($"{totalmontoModificado}").FontSize(7);
            ////    row.RelativeItem().Border(1).AlignRight().PaddingRight(2).Element(CellStyle).Text($"{totalDesembolso}").FontSize(7);
            ////    row.RelativeItem().Border(1).AlignRight().PaddingRight(2).Element(CellStyle).Text($"{totalEjecutado}").FontSize(7);
            ////    row.RelativeItem().Border(1).AlignRight().PaddingRight(2).Element(CellStyle).Text($"{totalDisponible}").FontSize(7);
            ////    row.RelativeItem().Border(1).AlignRight().PaddingRight(2).Element(CellStyle).Text($"{totalMonto}").FontSize(7);
            ////});



            ////table.Footer(async footer =>
            ////{

            ////    if (dePara == "D" )
            ////    {
            ////        footer.Cell().ColumnSpan(4).Row(row =>
            ////        {
            ////            row.ConstantItem(50).Border(1).AlignCenter().Element(CellStyle).Text("N°").FontSize(7);
            ////            row.RelativeColumn(2).Border(1).AlignCenter().AlignMiddle().Element(CellStyle).Text("METAS AFECTADAS").FontSize(8);
            ////            row.ConstantItem(100).Border(1).AlignCenter().Element(CellStyle).Text("GRADO DE \n" + " " + "AFECTACION(%)").FontSize(8);
            ////        });

            ////        footer.Cell().ColumnSpan(4).Row(row =>
            ////        {
            ////            row.ConstantItem(50).Border(1).AlignCenter().Element(CellStyle).Text("   ").FontSize(7);
            ////            row.RelativeItem(2).Border(1).AlignCenter().Element(CellStyle).Text("Control y Gestión de la relación de H.C.M, Seguro de Vida y Accidentes Personales, Servicio Funerario," +
            ////                " Servicio de Prevención y Salud Laboral. Seguimiento y Control en la Relación de Aportes de Ley \n" + "  " +
            ////                "" + "(S.S.O. R.P.E, F.A.O.V, F.E.J.P), Caja de Ahorro y Seguimiento y Control en la Relación de Bono de Alimentaciòn y Bono Salud. ").FontSize(8);
            ////            row.ConstantItem(50).Border(1).AlignCenter().Element(CellStyle).Text("%").FontSize(7);

            ////        });
            ////        footer.Cell().ColumnSpan(4).Border(1).AlignLeft().Element(CellStyle).Text("   EFECTO FINANCIERO: SE CUENTA CON RECURSOS DISPONIBLES DEL PRESUPUESTO ORDINARIO").FontSize(8);
                    
                    
            ////    }
            ////    else 
            ////    {
            ////        footer.Cell().ColumnSpan(4).Row(row =>
            ////        {
            ////            row.ConstantItem(50).Border(1).AlignCenter().Element(CellStyle).Text("N°").FontSize(7);
            ////            row.RelativeColumn(2).Border(1).AlignCenter().AlignMiddle().Element(CellStyle).Text("METAS BENEFICIADAS").FontSize(8);
            ////            row.ConstantItem(100).Border(1).AlignCenter().Element(CellStyle).Text("GRADO DE \n" + " " + "AFECTACION(%)").FontSize(8);
            ////        });

            ////        footer.Cell().ColumnSpan(4).Row(row =>
            ////        {
            ////            row.ConstantItem(50).Border(1).AlignCenter().Element(CellStyle).Text("   ").FontSize(7);
            ////            row.RelativeItem(2).Border(1).AlignCenter().Element(CellStyle).Text("Revisión y Control de los bienes muebles del Concejo Municipal (INFORMES, INVENTARIOS, ACTAS, OFICIOS Y ORDENES DE PAGO)").Bold().FontSize(8);
            ////            row.ConstantItem(50).Border(1).AlignCenter().Element(CellStyle).Text("%").FontSize(7);

            ////        });
            ////        footer.Cell().ColumnSpan(4).Border(1).AlignLeft().Element(CellStyle).Text("    EFECTO FINANCIERO: CONTAR CON LOS RECURSOS PRESUPUESTARIOS PARA CUMPLIR CON LOS COMPROMISOS ").FontSize(8);

            ////    }

                
            ////});


        });
       
    }
  }
