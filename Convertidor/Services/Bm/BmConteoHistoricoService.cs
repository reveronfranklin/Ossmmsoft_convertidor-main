using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Dtos.Bm;
using Convertidor.Dtos.Presupuesto;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NuGet.Protocol.Plugins;
using QuestPDF.Elements;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using static SkiaSharp.HarfBuzz.SKShaper;


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
                            fila.ConstantItem(140).Border(0).Height(60).Image(filePath:destino +"LogoIzquierda.jpeg")
                            .FitWidth().FitHeight();
                            fila.Spacing(4);
                            fila.RelativeItem().Border(0).Column(col =>
                            {
                                col.Item().AlignCenter().Text("Historico Conteo").Bold().FontSize(14);

                                col.Item().AlignCenter().Text($"{connteo.Data.Titulo}").FontSize(9);
                                
                            });
                            fila.RelativeItem().Border(0).Column(col =>
                            {
                                col.Item().Border(1).BorderColor(Colors.Cyan.Medium).AlignCenter()
                                .Text($"Conteo : {connteo.Data.CodigoBmConteo}").Bold().FontSize(14);

                                col.Item().Border(1).Background(Colors.Cyan.Medium).AlignCenter()
                                .Text($"{connteo.Data.Fecha.ToShortDateString()}").FontSize(9);

                                col.Spacing(4);
                            });
                            
                        });

                        

                        page.Content().Column(async col1 =>
                        {
                            
                            col1.Spacing(4);

                            col1.Item().LineHorizontal(0.5f);
                            

                            col1.Item().Table(async tabla =>
                            {

                                foreach (var item in detalle)
                                {
                                    
                                    tabla.ColumnsDefinition(async columnas =>
                                    {
                                        columnas.RelativeColumn(5);
                                        columnas.RelativeColumn();
                                        columnas.RelativeColumn();
                                        columnas.RelativeColumn();
                                        
                                      
                                        tabla.Cell().BorderBottom(0.5f).BorderColor("#d9d9d9")
                                        .Padding(2).Text($"{item.NUMERO_PLACA + "        "} {item.ARTICULO}").FontSize(8);

                                        tabla.Cell().BorderBottom(0.5f).BorderColor("#d9d9d9")
                                        .Padding(2).Text(item.CANTIDAD).FontSize(8);

                                        tabla.Cell().BorderBottom(0.5f).BorderColor("#d9d9d9")
                                        .Padding(2).Text(item.CANTIDAD_CONTADA).FontSize(8);

                                        tabla.Cell().BorderBottom(0.5f).BorderColor("#d9d9d9")
                                        .Padding(2).Text(item.DIFERENCIA).FontSize(8);



                                    });

                                    static IContainer Block(IContainer container)
                                    {
                                        return container
                                            .Border(1)
                                            .Background(Colors.Grey.Lighten3)
                                            .ShowOnce()
                                            .MinWidth(50)
                                            .MinHeight(50)
                                            .AlignCenter()
                                            .AlignMiddle();
                                    }

                                    if (item.COMENTARIO != null && item.COMENTARIO.Length > 0)
                                    {
                                        tabla.Cell().RowSpan(4).ColumnSpan(4).Element(Block).Text(item.COMENTARIO);
                                    }
                                }


                                tabla.Header(cabecera =>
                                {
                                    cabecera.Cell().Background(Colors.LightBlue.Medium)
                                    .Padding(2).Text("Numero Placa");

                                    cabecera.Cell().Background(Colors.LightBlue.Medium)
                                    .Padding(2).Text("Cantidad");

                                    cabecera.Cell().Background(Colors.LightBlue.Medium)
                                    .Padding(2).Text("Contado");

                                    cabecera.Cell().Background(Colors.LightBlue.Medium)
                                    .Padding(2).Text("Diferencia");

                                });

                                
                            });

                            col1.Item().AlignRight().BorderColor(Colors.Cyan.Medium).Border(1)
                            .Text($"Total : {connteo.Data.TotalCantidadContada}");

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

