using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Dtos.Bm;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;


namespace Convertidor.Services.Bm
{
    public class BmConteoHistoricoService: IBmConteoHistoricoService
    {

      
        private readonly IBmConteoHistoricoRepository _repository;
     
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhPersonasRepository _rhPersonasRepository;
        private readonly IBmConteoDetalleHistoricoService _conteoDetalleHistoricoService;
        private readonly IBmDescriptivaRepository _bmDescriptivaRepository;
        private readonly IConfiguration _configuration;
        public BmConteoHistoricoService(IBmConteoHistoricoRepository repository,
                                ISisUsuarioRepository sisUsuarioRepository,
                                IRhPersonasRepository rhPersonasRepository,
                                IBmConteoDetalleHistoricoService conteoDetalleHistoricoService,
                                IBmDescriptivaRepository bmDescriptivaRepository,
                                IConfiguration configuration)
		{
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _rhPersonasRepository = rhPersonasRepository;
            _conteoDetalleHistoricoService = conteoDetalleHistoricoService;
            _bmDescriptivaRepository = bmDescriptivaRepository;
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
        public async Task<BmConteoHistoricoResponseDto> MapBmConteo(BM_CONTEO_HISTORICO dtos)
        {


            BmConteoHistoricoResponseDto itemResult = new BmConteoHistoricoResponseDto();
            itemResult.CodigoBmConteo = dtos.CODIGO_BM_CONTEO;
            itemResult.Titulo = dtos.TITULO;
            itemResult.Comentario = dtos.COMENTARIO;
            itemResult.CodigoPersonaResponsable = dtos.CODIGO_PERSONA_RESPONSABLE;
            itemResult.NombrePersonaResponsable = "";
            var persona = await _rhPersonasRepository.GetCodigoPersona(dtos.CODIGO_PERSONA_RESPONSABLE);
            if (persona!=null)
            {
                itemResult.NombrePersonaResponsable = $"{persona.NOMBRE} {persona.APELLIDO}";
            }

          
            itemResult.ConteoId = dtos.CANTIDAD_CONTEOS_ID;
            itemResult.Fecha = dtos.FECHA;
            itemResult.FechaString = dtos.FECHA.ToString("u");
            FechaDto fechaObj = GetFechaDto(dtos.FECHA);
            itemResult.FechaObj = (FechaDto)fechaObj;
            itemResult.TotalCantidad = dtos.TOTAL_CANTIDAD;
            itemResult.TotalCantidadContada = dtos.TOTAL_CANTIDAD_CONTADA;
            itemResult.TotalDiferencia = dtos.TOTAL_DIFERENCIA;
            return itemResult;

        }
        public async Task<List<BmConteoHistoricoResponseDto>> MapListConteoDto(List<BM_CONTEO_HISTORICO> dtos)
        {
            List<BmConteoHistoricoResponseDto> result = new List<BmConteoHistoricoResponseDto>();


            foreach (var item in dtos)
            {

                BmConteoHistoricoResponseDto itemResult = new BmConteoHistoricoResponseDto();

                itemResult = await MapBmConteo(item);

                result.Add(itemResult);
            }
            return result.OrderByDescending(x=>x.CodigoBmConteo).ToList();



        }
        
        public async Task<ResultDto<List<BmConteoHistoricoResponseDto>>> GetAll()
        {

            ResultDto<List<BmConteoHistoricoResponseDto>> result = new ResultDto<List<BmConteoHistoricoResponseDto>>(null);
            try
            {

                var conteos = await _repository.GetAll();



                if (conteos.Count() > 0)
                {
                    List<BmConteoHistoricoResponseDto> listDto = new List<BmConteoHistoricoResponseDto>();
                    listDto = await MapListConteoDto(conteos);
                

                    result.Data = listDto;

                    result.IsValid = true;
                    result.Message = "";
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
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
        
        public async Task<ResultDto<BmConteoHistoricoResponseDto>> GetByCodigoConteo(int codigoConteo)
        {

            ResultDto<BmConteoHistoricoResponseDto> result = new ResultDto<BmConteoHistoricoResponseDto>(null);
            try
            {

                var conteo = await _repository.GetByCodigo(codigoConteo);

                

                if (conteo!=null)
                {
                   
                    var listDto = await MapBmConteo(conteo);
                

                    result.Data = listDto;

                    result.IsValid = true;
                    result.Message = "";
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
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

        public async Task CreateReportConteoHistorico(int codigoConteo)
        {

            var connteo =await  GetByCodigoConteo(codigoConteo);

            var detalle = await _conteoDetalleHistoricoService.GetAllByConteo(codigoConteo);

            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var destino = @settings.BmFiles;

            var fileName = $"{destino}{codigoConteo.ToString()}.pdf";
            try
            {
                Document.Create(documento =>
                {
                    documento.Page(page =>
                    {
                        page.Margin(20);

                        page.Header().Row(fila =>
                        {
                            fila.ConstantItem(140).Border(0).Height(60).Placeholder();
                            fila.RelativeItem().Border(0).Column(col =>
                            {
                                col.Item().AlignCenter().Text("Fecha Conteo").Bold().FontSize(14);
                                col.Item().AlignCenter().Text($"{connteo.Data.Fecha.ToString()}").FontSize(9);
                                col.Item().AlignCenter().Text("Gerencia").FontSize(9);
                            });
                            fila.RelativeItem().Border(0).Column(col =>
                            {
                                col.Item().Border(1).BorderColor(Colors.Cyan.Medium).AlignCenter().Text($"Conteo : {connteo.Data.CodigoBmConteo}").Bold().FontSize(14);
                                col.Item().Border(1).Background(Colors.Cyan.Medium).AlignCenter().Text("Orden Entrada : 000123546").FontSize(9);
                                col.Item().AlignCenter().Text("Orden Salida : 006987563").FontSize(9);
                            });
                        });

                        page.Content().Column(col1 =>
                        {
                            col1.Item().Text("Datos Del Cliente").Underline().Bold();

                            col1.Item().Text(txt =>
                            {
                                txt.Span("Nombre : ").SemiBold().FontSize(10);
                                txt.Span("Luis Patiño").FontSize(10);
                            });

                            col1.Item().Text(txt =>
                            {
                                txt.Span("Rif : ").SemiBold().FontSize(10);
                                txt.Span("j-464-455454").FontSize(10);
                            });

                            col1.Item().LineHorizontal(0.5f);

                            col1.Item().Table(async tabla =>
                            {
                                tabla.ColumnsDefinition(columnas =>
                                {
                                    columnas.RelativeColumn(4);
                                    columnas.RelativeColumn();
                                    columnas.RelativeColumn();
                                    columnas.RelativeColumn();
                                });

                                tabla.Header(cabecera =>
                                {
                                    cabecera.Cell().Background(Colors.LightBlue.Medium)
                                    .Padding(2).Text("Placa");
                                    cabecera.Cell().Background(Colors.LightBlue.Medium)
                                    .Padding(2).Text("Precio Unit");
                                    cabecera.Cell().Background(Colors.LightBlue.Medium)
                                    .Padding(2).Text("Cantidad");
                                    cabecera.Cell().Background(Colors.LightBlue.Medium)
                                    .Padding(2).Text("Total");

                                });



                                foreach (var item in detalle)
                                {
                                    var precio = Placeholders.Random.Next(1, 10);
                                    var cantidad = Placeholders.Random.Next(5, 10);
                                    var total = cantidad * precio;

                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#d9d9d9")
                                    .Padding(2).Text(item.NUMERO_PLACA).FontSize(10);

                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#d9d9d9")
                                    .Padding(2).Text($"Bs .{precio}").FontSize(10);

                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#d9d9d9")
                                    .Padding(2).Text(cantidad.ToString()).FontSize(10);

                                    tabla.Cell().BorderBottom(0.5f).BorderColor("#d9d9d9")
                                    .Padding(2).Text($"Bs .{total}").FontSize(10);


                                }

                            });

                            col1.Item().AlignRight().Text($"Total:");

                        });
                    });
                }).GeneratePdf(fileName);

            }
            catch (Exception ex)
            {

                var message = ex.Message;
            } 
            

        }
    }
}

