using Convertidor.Dtos.Bm;
using Convertidor.Dtos.Presupuesto;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.MinimalApi;
using NPOI.OpenXmlFormats;
using NPOI.SS.Formula.Functions;
using QuestPDF.Elements;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

namespace Convertidor.Services.Rh
{
    public class RhVReciboPagoService : IRhVReciboPagoService
    {

        private readonly IRhVReciboPagoRepository _repository;
        private readonly IConfiguration _configuration;


        public RhVReciboPagoService(IRhVReciboPagoRepository repository,
                                      IRhTipoNominaRepository rhTipoNominaRepository,
                                      IRhPeriodoRepository periodoRepository,
                                      IRhPersonasRepository personasRepository,
                                      ISisUsuarioRepository usuarioRepository,
                                      IConfiguration configuration,
                                      IRhPersonaService personaService)
        {
            _repository = repository;
            _configuration = configuration;


        }

        public FechaDto GetFechaDto(DateTime fecha)
        {
            var FechaDesdeObj = new FechaDto();
            FechaDesdeObj.Year = fecha.Year.ToString();
            string month = "00" + fecha.Month.ToString();
            string day = "00" + fecha.Day.ToString();
            FechaDesdeObj.Month = month.Substring(month.Length - 2);
            FechaDesdeObj.Day = day.Substring(day.Length - 2);

            return FechaDesdeObj;
        }
        public async Task<RhVReciboPagoResponseDto> MapReciboPagoDto(RH_V_RECIBO_PAGO dtos)
        {


            RhVReciboPagoResponseDto itemResult = new RhVReciboPagoResponseDto();
            itemResult.FechaPeriodoNomina = dtos.FECHA_PERIODO_NOMINA;
            itemResult.FechaPeriodoNominaString = dtos.FECHA_PERIODO_NOMINA.ToString("u");
            FechaDto fechaPeriodoNominaObj = GetFechaDto(dtos.FECHA_PERIODO_NOMINA);
            itemResult.FechaPeriodoNominaObj = (FechaDto)fechaPeriodoNominaObj;
            itemResult.FechaNomina = dtos.FECHA_NOMINA;
            itemResult.FechaNominaString = dtos.FECHA_NOMINA.ToString("u");
            FechaDto fechaNominaObj = GetFechaDto(dtos.FECHA_NOMINA);
            itemResult.FechaNominaObj = (FechaDto)fechaNominaObj;
            itemResult.CodigoPeriodo = dtos.CODIGO_PERIODO;
            itemResult.CodigoTipoNomina = dtos.CODIGO_TIPO_NOMINA;
            itemResult.CodigoIcpConcat = dtos.CODIGO_ICP_CONCAT;
            itemResult.Denominacion = dtos.DENOMINACION;
            itemResult.DenominacionCargo = dtos.DENOMINACION_CARGO;
            itemResult.Cedula = dtos.CEDULA;
            itemResult.Nombre = dtos.NOMBRE;
            itemResult.NoCuenta = dtos.NO_CUENTA;
            itemResult.DescripcionTipoNomina = dtos.DESCRIPCION_TIPO_NOMINA;
            itemResult.CodigoConcepto = dtos.CODIGO_CONCEPTO;
            itemResult.CodigoConceptoId = dtos.CODIGO_CONCEPTO_ID;
            itemResult.TipoMovConcepto = dtos.TIPO_MOV_CONCEPTO;
            itemResult.DenominacionConcepto = dtos.DENOMINACION_CONCEPTO;
            itemResult.TipoConcepto = dtos.TIPO_CONCEPTO;
            itemResult.Porcentaje = dtos.PORCENTAJE;
            itemResult.Asignacion = itemResult.Asignacion;
            itemResult.Deduccion = itemResult.Deduccion;
            itemResult.Monto = dtos.MONTO;
            itemResult.SueldoBase = itemResult.Asignacion - itemResult.Deduccion;         
            itemResult.TipoSueldo = dtos.TIPO_SUELDO;
            itemResult.Sueldo = dtos.SUELDO;
            itemResult.CodigoTipoSueldoDesc = dtos.CODIGO_TIPO_SUELDO_DESC;
            itemResult.ComplementoConcepto = dtos.COMPLEMENTO_CONCEPTO;
            itemResult.CodigoPersona = dtos.CODIGO_PERSONA;
            itemResult.Modulo = dtos.MODULO;
            itemResult.CodigoIdentificador = dtos.CODIGO_IDENTIFICADOR;
            
            



            return itemResult;



        }


        public async Task<List<RhVReciboPagoResponseDto>> MapListReciboPagoDto(List<RH_V_RECIBO_PAGO> dtos)
        {
            List<RhVReciboPagoResponseDto> result = new List<RhVReciboPagoResponseDto>();

            foreach (var item in dtos)
            {

                var itemResult = await MapReciboPagoDto(item);


                result.Add(itemResult);


            }
            return result.OrderByDescending(x => x.CodigoPersona).ToList();


        }

        public async Task<List<RhVReciboPagoResponseDto>> GetAll()
        {
            try
            {
                var reciboPago = await _repository.GetAll();

                var result = await MapListReciboPagoDto(reciboPago);


                return (List<RhVReciboPagoResponseDto>)result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

    
        public  List<ResumenRhVReciboPagoResponseDto> GetResumenRecibo(List<RH_V_RECIBO_PAGO> dto)
        {
            var lista = from s in dto
                        group s by new
                        {

                            Asignacion = s.ASIGNACION,
                            Deduccion = s.DEDUCCION,
                            TotalNeto = s.ASIGNACION - s.DEDUCCION,



                        } into g
                        select new ResumenRhVReciboPagoResponseDto()
                        {


                            Asignacion = g.Key.Asignacion,
                            Deduccion = g.Key.Deduccion,


                        };
            return lista.ToList();
        }

        public async Task<ResultDto<List<RhVReciboPagoResponseDto>>> GetByFilter(FilterReciboPagoDto dto)
        {

            ResultDto<List<RhVReciboPagoResponseDto>> result = new ResultDto<List<RhVReciboPagoResponseDto>>(null);

            try
            {

                var reciboPago = await _repository.GeTByFilter(dto.CodigoPeriodo, dto.CodigoTipoNomina, dto.CodigoPersona);



                if (reciboPago != null)
                {

                    var listDto = await MapListReciboPagoDto(reciboPago);


                    result.Data = listDto;

                    result.IsValid = true;
                    result.Message = "";
                }
                else
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = " No existen Datos";

                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }

        public async Task CreateReportReciboPago(int codigoPeriodo, int codigoTipoNomina, int codigoPersona)
        {
            static IContainer Block(IContainer container)
            {
                return container
                    .Border(1)
                    .Background(Colors.Grey.Lighten3)
                    .ShowOnce()
                    .MinWidth(50)
                    .MinHeight(20)
                    .AlignCenter()
                    .AlignMiddle();

            }
            static IContainer BlockResumenIcp(IContainer container)
            {
                return container
                    .Border(1)
                    .ShowOnce()
                    .MinWidth(50)
                    .MinHeight(25)
                    .AlignCenter()
                    .AlignMiddle();
            }

            static IContainer BlockCabeceraTotal(IContainer container)
            {
                return container
                    .Border(1)
                    .Background(Colors.Grey.Lighten3)
                    .PaddingRight(2)
                    .ShowOnce()
                    .MinWidth(50)
                    .MinHeight(20)
                    .AlignCenter()
                    .AlignMiddle();
            }

            static IContainer BlockTotales(IContainer container)
            {
                return container
                    .Border(1)
                    .Background(Colors.Grey.Lighten3)
                    .PaddingRight(2)
                    .PaddingLeft(26)
                    .ShowOnce()
                    .MinWidth(50)
                    .MinHeight(20)
                    .AlignRight()
                    .AlignMiddle();

            }






            var reciboPago = await _repository.GeTByFilter(codigoPeriodo, codigoTipoNomina, codigoPersona);

            var resumen = reciboPago.FirstOrDefault();

            var tipoNomina = await _repository.GetByCodigoTipoNomina(codigoTipoNomina,codigoPersona);
           
            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var destino = @settings.ExcelFiles;

            var fileName = $"{destino}Recibo-{codigoTipoNomina.ToString()}.pdf";
            try
            {

                ResultDto<RhVReciboPagoResponseDto> recibo = new ResultDto<RhVReciboPagoResponseDto>(null);
                if (reciboPago == null)
                {
                    recibo.Data = null;
                    recibo.IsValid = true;
                    recibo.Message = " No existen Datos";
                }
                else
                {
                    Document.Create(documento =>
                    {
                        documento.Page(page =>
                        {

                            page.Margin(20);
                            page.Size(PageSizes.A4.Landscape());

                            page.Header().Row(fila =>
                            {

                                fila.ConstantItem(140).Border(0).Height(60).Image(filePath: settings.BmFiles + "LogoIzquierda.jpeg")
                                .FitWidth().FitHeight();
                                fila.Spacing(4);


                            });



                            page.Content().Column(async col1 =>
                            {
                                var resumenRecibo = GetResumenRecibo(reciboPago);
                                col1.Spacing(4);


                                //col1.Item().Element(Block).Text($"Comentario :{"  "}  {connteo.Data.Comentario}");
                                col1.Item().LineHorizontal(0.5f);


                                col1.Item().Table(async tabla =>
                                {





                                    tabla.ColumnsDefinition(columns =>
                                    {

                                        columns.RelativeColumn(1);
                                        columns.RelativeColumn(1);
                                        columns.RelativeColumn(1);
                                        columns.RelativeColumn(1);
                                        columns.RelativeColumn(1);
                                        columns.RelativeColumn(1);
                                        columns.RelativeColumn(1);




                                        tabla.Cell().Border(1).AlignCenter().Text(tipoNomina.CODIGO_ICP_CONCAT);
                                        tabla.Cell().ColumnSpan(2).Border(1).AlignCenter().Text(tipoNomina.NOMBRE);

                                        tabla.Cell().Border(1).AlignCenter().Text(tipoNomina.CODIGO_TIPO_NOMINA);
                                        tabla.Cell().Border(1).AlignCenter().Text(tipoNomina.CEDULA);

                                        tabla.Cell().Border(1).AlignCenter().Text(tipoNomina.SUELDO);
                                        tabla.Cell().Border(1).AlignCenter().Text(tipoNomina.FECHA_NOMINA.ToShortDateString());




                                        tabla.Header(cabecera =>
                                        {


                                            cabecera.Cell().Border(1).AlignCenter().AlignMiddle().Text("DEPARTAMENTO").Bold();
                                            cabecera.Cell().ColumnSpan(2).Border(1).AlignCenter().AlignMiddle().Text("NOMBRE").Bold();

                                            cabecera.Cell().Border(1).AlignCenter().AlignMiddle().Text("TIPO NOMINA").Bold();
                                            cabecera.Cell().Border(1).AlignCenter().AlignMiddle().Text("CEDULA").Bold();

                                            cabecera.Cell().Border(1).AlignCenter().AlignMiddle().Text("SUELDO").Bold();
                                            cabecera.Cell().Border(1).AlignCenter().AlignMiddle().Text("FECHA").Bold();





                                            tabla.Cell().ColumnSpan(2).Border(1).AlignCenter().Text("CONCEPTO").Bold();
                                            tabla.Cell().Border(1).AlignCenter().Text("COMPLEMENTO").Bold();
                                            tabla.Cell().Border(1).AlignCenter().ShrinkHorizontal().Text("%").Bold();
                                            tabla.Cell().Border(1).AlignCenter().Text("ACUMULADO").Bold();
                                            tabla.Cell().Border(1).AlignCenter().Text("ASIGNACIONES").Bold();
                                            tabla.Cell().Border(1).AlignCenter().Text("DEDUCCIONES").Bold();


                                            foreach (var itemResumen in resumenRecibo)
                                            {

                                                foreach (var item in reciboPago)
                                                {
                                                    tabla.Cell().ColumnSpan(2).BorderLeft(1).BorderRight(1).AlignCenter().ScaleToFit().Text(item.DENOMINACION_CONCEPTO);
                                                    tabla.Cell().BorderLeft(1).BorderRight(1).AlignCenter().Padding(2).Text(item.COMPLEMENTO_CONCEPTO);

                                                    tabla.Cell().BorderLeft(1).BorderRight(1).AlignCenter().Text(item.PORCENTAJE);
                                                    tabla.Cell().BorderLeft(1).BorderRight(1).AlignCenter().Text(item.MONTO);

                                                    tabla.Cell().BorderLeft(1).BorderRight(1).AlignCenter().Text(item.ASIGNACION);
                                                    tabla.Cell().BorderLeft(1).BorderRight(1).AlignCenter().Text(item.DEDUCCION);


                                                }

                                                tabla.Cell().Row(fila =>
                                                {
                                                    fila.ConstantColumn(80).Border(1).AlignRight().Padding(2).Text(resumen.ASIGNACION + resumen.ASIGNACION);
                                                    fila.RelativeColumn().Border(1).AlignRight().Padding(2).Text(resumen.DEDUCCION + resumen.DEDUCCION);
                                                });


                                            }





                                            col1.Item().Row(pie =>
                                            {
                                                pie.RelativeItem().Border(1).Text($"N° \n \n \n " + "    " + "   " + "  " + "1");
                                                pie.ConstantItem(550).Border(1).AlignCenter().Text($"Liquidacion de Sueldos y Salarios. Acepto que despues de hechas las" +
                                                    "Deducciones de mi Sueldo o Salario, he recibido conforme el saldo abajo indicado, en pago de los servicios" +
                                                    "que he prestado hasta la fecha que se indica.");
                                                pie.RelativeItem().Border(1).AlignCenter().AlignMiddle().Text($"_______________________" + " " +
                                                                                                 $"Firma del Beneficiario")
                                                                                             ;

                                            });


                                        });

                                    });


                                });


                                //     }
                                //     //tabla.Cell().ShowOnce().Border(1).Text("Temperature");
                                //     //tabla.Cell().ShowOnce().Border(1).Text("17°C");

                                //     //tabla.Cell().ShowOnce().Border(1).Text("Temperature");
                                //     //tabla.Cell().ShowOnce().Border(1).Text("32°C");

                                //     //tabla.Cell().ShowOnce().Border(1).Text("Remarks");
                                //     //tabla.Cell().ColumnSpan(3).ShowOnce().Border(1).Text("");
                                // });




                                //    //col1.Item().Row(pie =>
                                //    //{
                                //    //    pie.ConstantItem(350).AlignRight().PaddingRight(2).Element(BlockTotales).AlignRight().Text(item./*Asignacion*/);
                                //    //    pie.RelativeColumn().PaddingRight(2).Element(BlockTotales).AlignRight().Text(.Deduccion);
                                //    //});

                                //    //col1.Item().Row(pie =>
                                //    //{
                                //    //    pie.ConstantItem(350).PaddingRight(2).AlignRight().Element(BlockCabeceraTotal).Text("Total Neto");
                                //    //    pie.RelativeItem().PaddingRight(2).Element(BlockCabeceraTotal).Text(itemResumen.TotalNeto);

                                //    //});











                            });


                        });


                    }).GeneratePdf(fileName);
                }
            }
            catch (Exception ex)
            {

                var message = ex.Message;
            }

        }
    }
}    

      



            
        

    

