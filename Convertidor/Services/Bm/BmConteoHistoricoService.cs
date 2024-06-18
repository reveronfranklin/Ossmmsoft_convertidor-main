using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Dtos.Bm;
using Convertidor.Utility;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


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
            itemResult.FechaString = Fecha.GetFechaString(dtos.FECHA);
            FechaDto fechaObj = Fecha.GetFechaDto(dtos.FECHA);
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
                
                await CreateReportConteoHistorico(item.CODIGO_BM_CONTEO);
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
            static IContainer BlockResumenIcp(IContainer container)
            {
                return container
                    .Border(1)
                    .Background(Colors.Grey.Lighten3)
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


            var connteo =await  GetByCodigoConteo(codigoConteo);

            var detalle = await _conteoDetalleHistoricoService.GetAllByConteo(codigoConteo);

            var resumenIcp = GetResumenICP(detalle);
            var resumenConteo = GetResumenConteo(detalle);

            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var destino = @settings.BmFiles;
            var destinoReport = @settings.ExcelFiles;
          
            var fileName = $"{destinoReport}/{codigoConteo.ToString()}.pdf";
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

                                col.Item().AlignCenter().Text($"{connteo.Data.Titulo}").FontSize(12);

                            });
                            fila.RelativeItem().Border(0).Column(col =>
                            {
                                col.Item().Border(1).BorderColor(Colors.Cyan.Medium).AlignCenter()
                                .Text($"Conteo : {connteo.Data.CodigoBmConteo}").Bold().FontSize(14);

                                col.Item().Border(1).Background(Colors.LightBlue.Medium).AlignCenter()
                                .Text($"{connteo.Data.Fecha.ToShortDateString()}").FontSize(9);

                                col.Spacing(4);

                                
                            });

                        });

                      

                        page.Content().Column(async col1 =>
                        {

                            col1.Spacing(4);

                            col1.Item().Element(Block).Text($"Comentario :{"  "}  {connteo.Data.Comentario}");
                            col1.Item().LineHorizontal(0.5f);

                            

                            col1.Item().Table(async tabla =>
                            {


                                foreach (var itemResumenIcp in resumenIcp)
                                {

                                    tabla.Cell().RowSpan(5).ColumnSpan(5).Element(BlockResumenIcp).Text(itemResumenIcp.UnidadTrabajo);

                                    foreach (var item in detalle.Where(x=> x.CODIGO_ICP== itemResumenIcp.CodigoIcp).ToList())
                                    {
                                        

                                        tabla.ColumnsDefinition(async columnas =>
                                        {
                                            columnas.RelativeColumn(1);
                                            columnas.RelativeColumn(4);
                                            columnas.RelativeColumn(2);
                                            columnas.RelativeColumn(2);
                                            columnas.RelativeColumn(2);


                                            tabla.Cell().BorderBottom(0.5f).BorderColor("#d9d9d9").AlignCenter()
                                            .Padding(2).Text($"{item.CONTEO}").FontSize(8);

                                            tabla.Cell().BorderBottom(0.5f).BorderColor("#d9d9d9")
                                            .Padding(2).Text($"{item.NUMERO_PLACA + "        "} {item.ARTICULO}").FontSize(8);

                                            tabla.Cell().BorderBottom(0.5f).BorderColor("#d9d9d9").AlignRight()
                                            .Padding(2).Text(item.CANTIDAD).FontSize(8);

                                            tabla.Cell().BorderBottom(0.5f).BorderColor("#d9d9d9").AlignRight()
                                            .Padding(2).Text(item.CANTIDAD_CONTADA).FontSize(8);

                                            tabla.Cell().BorderBottom(0.5f).BorderColor("#d9d9d9").AlignRight()
                                            .Padding(2).Text(item.DIFERENCIA).FontSize(8);



                                        });

                                      

                                        if (item.COMENTARIO != null && item.COMENTARIO.Length > 0)
                                        {
                                            tabla.Cell().ColumnSpan(5).Element(Block).Text(item.COMENTARIO);
                                        }
                                    }

                                }



                                tabla.Header(cabecera =>
                                {
                                    cabecera.Cell().ScaleToFit().Background(Colors.LightBlue.Medium).AlignMiddle().AlignCenter()
                                    .Padding(2).Text("Conteo");

                                    cabecera.Cell().Background(Colors.LightBlue.Medium).AlignMiddle().AlignCenter()
                                    .Padding(2).Text("Numero Placa");

                                    cabecera.Cell().Background(Colors.LightBlue.Medium).AlignMiddle().AlignCenter()
                                    .Padding(2).Text("Cantidad");

                                    cabecera.Cell().Background(Colors.LightBlue.Medium).AlignMiddle().AlignCenter()
                                    .Padding(2).Text("Contado");

                                    cabecera.Cell().Background(Colors.LightBlue.Medium).AlignMiddle().AlignCenter()
                                    .Padding(2).Text("Diferencia");

                                });

                                
                            });
                            col1.Item().Row(pie =>
                            {
                                pie.ConstantItem(350).PaddingRight(2).AlignRight().Element(BlockCabeceraTotal).Text("Total Cantidad");
                                pie.RelativeColumn().PaddingRight(2).Element(BlockCabeceraTotal).Text("Total Contada");
                                pie.RelativeColumn().PaddingRight(2).Element(BlockCabeceraTotal).Text("Total Diferencia");

                            });

                            col1.Item().Row(pie =>
                            {
                                pie.ConstantItem(350).PaddingRight(2).AlignRight().Element(BlockTotales).Text(connteo.Data.TotalCantidad);
                                pie.RelativeColumn().PaddingRight(2).Element(BlockTotales).AlignRight().Text(connteo.Data.TotalCantidadContada);
                                pie.RelativeColumn().PaddingRight(2).Element(BlockTotales).AlignRight().Text(connteo.Data.TotalDiferencia);

                            });
                        });
                    });
                }).GeneratePdf(fileName);

            }
            catch (Exception ex)
            {

                var message = ex.Message;
            } 
            

        }

        public List<ICPGetDto> GetResumenICP(List<BM_CONTEO_DETALLE_HISTORICO> dto)
        {


            var lista = from s in dto
                        group s by new
                        {
                            CodigoIcp = s.CODIGO_ICP,
                            UnidadTrabajo = s.UNIDAD_TRABAJO,



                        } into g
                        select new ICPGetDto()
                        {

                            CodigoIcp = g.Key.CodigoIcp,
                            UnidadTrabajo = g.Key.UnidadTrabajo,


                        };
            return lista.ToList();

        }
        public List<ResumenConteoGetDto> GetResumenConteo(List<BM_CONTEO_DETALLE_HISTORICO> dto)
        {


            var lista = from s in dto
                        group s by new
                        {
                            Conteo = s.CONTEO,
                            



                        } into g
                        select new ResumenConteoGetDto()
                        {

                            Conteo = g.Key.Conteo,
                          

                        };
            return lista.ToList();

        }


    }
}

