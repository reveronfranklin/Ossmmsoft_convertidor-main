
﻿using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Dtos.Bm;
using Convertidor.Utility;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

﻿
using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Dtos.Bm;
using Convertidor.Utility;



namespace Convertidor.Services.Bm
{
    public class BmConteoService: IBmConteoService
    {

      
        private readonly IBmConteoRepository _repository;
     
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhPersonasRepository _rhPersonasRepository;
        private readonly IBmConteoDetalleService _conteoDetalleService;
        private readonly IBmConteoDetalleRepository _bmConteoDetalleRepository;
        private readonly IBmDescriptivaRepository _bmDescriptivaRepository;
        private readonly IBmConteoHistoricoRepository _bmConteoHistoricoRepository;
        private readonly IBmConteoDetalleHistoricoRepository _bmConteoDetalleHistoricoRepository;
        private readonly IConfiguration _configuration;
        public BmConteoService(IBmConteoRepository repository,
                                ISisUsuarioRepository sisUsuarioRepository,
                                IRhPersonasRepository rhPersonasRepository,
                                IBmConteoDetalleService conteoDetalleService,
                                IBmConteoDetalleRepository bmConteoDetalleRepository,
                                IBmDescriptivaRepository bmDescriptivaRepository,
                                IBmConteoHistoricoRepository bmConteoHistoricoRepository,
                                IBmConteoDetalleHistoricoRepository bmConteoDetalleHistoricoRepository,
                                IConfiguration configuration)
		{
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _rhPersonasRepository = rhPersonasRepository;
            _conteoDetalleService = conteoDetalleService;
            _bmConteoDetalleRepository = bmConteoDetalleRepository;
            _bmDescriptivaRepository = bmDescriptivaRepository;
            _bmConteoHistoricoRepository = bmConteoHistoricoRepository;
            _bmConteoDetalleHistoricoRepository = bmConteoDetalleHistoricoRepository;
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
        public async Task<BmConteoResponseDto> MapBmConteo(BM_CONTEO dtos)
        {


            BmConteoResponseDto itemResult = new BmConteoResponseDto();
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
            var resumen = await _conteoDetalleService.GetResumen(dtos.CODIGO_BM_CONTEO);
            itemResult.ResumenConteo = resumen.Data;
            itemResult.TotalCantidad = itemResult.ResumenConteo.Sum(r => r.Cantidad);
            itemResult.TotalCantidadContado = itemResult.ResumenConteo.Sum(r => r.CantidadContada);
            return itemResult;

        }
        public async Task<List<BmConteoResponseDto>> MapListConteoDto(List<BM_CONTEO> dtos)
        {
            List<BmConteoResponseDto> result = new List<BmConteoResponseDto>();


            foreach (var item in dtos)
            {
                if (item != null)
                {
                    BmConteoResponseDto itemResult = new BmConteoResponseDto();

                    itemResult = await MapBmConteo(item);

                    result.Add(itemResult);

                
                }
               
            }
            return result.OrderByDescending(x=>x.CodigoBmConteo).ToList();



        }

       
        public async Task<ResultDto<List<BmConteoResponseDto>>> GetAll()
        {

            ResultDto<List<BmConteoResponseDto>> result = new ResultDto<List<BmConteoResponseDto>>(null);
            try
            {

                var conteos = await _repository.GetAll();



                if (conteos.Count() > 0)
                {
                    List<BmConteoResponseDto> listDto = new List<BmConteoResponseDto>();
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


        public async Task<ResultDto<BmConteoResponseDto>> Update(BmConteoUpdateDto dto)
        {

            ResultDto<BmConteoResponseDto> result = new ResultDto<BmConteoResponseDto>(null);
            try
            {
                var conteo = await _repository.GetByCodigo(dto.CodigoBmConteo);
                if (conteo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Conteo no Existe";
                    return result;
                }

             

                var persona = await _rhPersonasRepository.GetCodigoPersona(dto.CodigoPersonaResponsable);
                if (persona==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Persona responsable  Invalido";
                    return result;
                }

                if (String.IsNullOrEmpty(dto.Titulo))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Titulo Invalido";
                    return result;
                }
                
                if (!DateValidate.IsDate(dto.Fecha.ToShortDateString()))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha inicial Invalida";
                    result.LinkData = "";
                    return result;
                }
                
              

                conteo.CODIGO_PERSONA_RESPONSABLE = dto.CodigoPersonaResponsable;
                conteo.FECHA = dto.Fecha;
                conteo.COMENTARIO = dto.Comentario;
               

                var conectado = await _sisUsuarioRepository.GetConectado();
                conteo.CODIGO_EMPRESA = conectado.Empresa;
                conteo.USUARIO_UPD = conectado.Usuario;
                conteo.FECHA_UPD = DateTime.Now;

                await _repository.Update(conteo);

                var resultDto = await MapBmConteo(conteo);
                result.Data = resultDto;
                result.IsValid = true;
                result.Message = "";

            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }

        public async Task<ResultDto<BmConteoResponseDto>> Create(BmConteoUpdateDto dto)
        {

            ResultDto<BmConteoResponseDto> result = new ResultDto<BmConteoResponseDto>(null);
            try
            {

                var persona = await _rhPersonasRepository.GetCodigoPersona(dto.CodigoPersonaResponsable);
                if (persona==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Persona responsable  Invalido";
                    return result;
                }

                if (String.IsNullOrEmpty(dto.Titulo))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Titulo Invalido";
                    return result;
                }
                
                if (!DateValidate.IsDate(dto.Fecha.ToShortDateString()))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha inicial Invalida";
                    result.LinkData = "";
                    return result;
                }

                var conteoDescriptiva = await _bmDescriptivaRepository.GetByCodigo(dto.ConteoId);
                if (conteoDescriptiva==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad de Conteos Invalida";
                    return result;
                }



                BM_CONTEO entity = new BM_CONTEO();
                entity.CODIGO_BM_CONTEO = await _repository.GetNextKey();
                entity.TITULO = dto.Titulo;
                entity.CODIGO_PERSONA_RESPONSABLE = dto.CodigoPersonaResponsable;
                entity.CANTIDAD_CONTEOS_ID = dto.ConteoId;
                entity.FECHA = dto.Fecha;

                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data!=null)
                {
                    var cantidadConteos = Int32.Parse(conteoDescriptiva.DESCRIPCION);
                    var bmDetalleCreated= await _conteoDetalleService.CreaDetalleConteoDesdeBm1(created.Data.CODIGO_BM_CONTEO, cantidadConteos,dto.ListIcpSeleccionado,dto.CodigoPersonaResponsable);

                    if (bmDetalleCreated.IsValid)
                    {
                        var resultDto = await MapBmConteo(created.Data);
                        result.Data = resultDto;
                        result.IsValid = true;
                        result.Message = "";
                    }
                    else
                    {
                        var deleted = await _repository.Delete(created.Data);
                        result.Data = null;
                        result.IsValid = false;
                        result.Message =bmDetalleCreated.Message;
                        return result;
                    }
                        
                   
                }
                else
                {
                    
                    result.Data = null;
                    result.IsValid = created.IsValid;
                    result.Message = created.Message;
                }

                return result;
              
               

            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }


        public async Task<BM_CONTEO_HISTORICO> BmConteoToHistrorico(BM_CONTEO conteo)
        {
            var conectado = await _sisUsuarioRepository.GetConectado();
            BM_CONTEO_HISTORICO conteoHistorico = new BM_CONTEO_HISTORICO();
            conteoHistorico.CODIGO_BM_CONTEO = conteo.CODIGO_BM_CONTEO;
            conteoHistorico.TITULO = conteo.TITULO;
            conteoHistorico.COMENTARIO = conteo.COMENTARIO;
            conteoHistorico.CODIGO_PERSONA_RESPONSABLE = conteo.CODIGO_PERSONA_RESPONSABLE;
            conteoHistorico.CANTIDAD_CONTEOS_ID = conteo.CANTIDAD_CONTEOS_ID;
            conteoHistorico.FECHA = conteo.FECHA;
            conteoHistorico.USUARIO_INS = conteo.USUARIO_INS;
            conteoHistorico.FECHA_INS = conteo.FECHA_INS;
            conteoHistorico.USUARIO_UPD = conteo.USUARIO_UPD;
            conteoHistorico.FECHA_UPD = conteo.FECHA_UPD;
            conteoHistorico.CODIGO_EMPRESA = conteo.CODIGO_EMPRESA;
            conteoHistorico.USUARIO_CIERRE = conectado.Usuario;
            conteoHistorico.FECHA_CIERRE =  DateTime.Now;

            var resumen = await _conteoDetalleService.GetResumen(conteo.CODIGO_BM_CONTEO);
            foreach (var item in resumen.Data)
            {
                conteoHistorico.TOTAL_CANTIDAD = conteoHistorico.TOTAL_CANTIDAD  + item.Cantidad;
                conteoHistorico.TOTAL_CANTIDAD_CONTADA = conteoHistorico.TOTAL_CANTIDAD_CONTADA + item.CantidadContada;
            }

            conteoHistorico.TOTAL_DIFERENCIA = conteoHistorico.TOTAL_CANTIDAD - conteoHistorico.TOTAL_CANTIDAD_CONTADA;
            return conteoHistorico;
        }

        public BM_CONTEO_DETALLE_HISTORICO BmConteoDetalleToHistorico(
            BM_CONTEO_DETALLE item)
        {
        
            BM_CONTEO_DETALLE_HISTORICO itemDetalle = new BM_CONTEO_DETALLE_HISTORICO();
            itemDetalle.CODIGO_BM_CONTEO = item.CODIGO_BM_CONTEO;
            itemDetalle.CONTEO = item.CONTEO;
            itemDetalle.CODIGO_ICP = item.CODIGO_ICP;
            itemDetalle.UNIDAD_TRABAJO = item.UNIDAD_TRABAJO;
            itemDetalle.CODIGO_GRUPO = item.CODIGO_GRUPO;
            itemDetalle.CODIGO_NIVEL1 = item.CODIGO_NIVEL1;
            itemDetalle.CODIGO_NIVEL2 = item.CODIGO_NIVEL2;
            itemDetalle.NUMERO_LOTE = item.NUMERO_LOTE;
            itemDetalle.CANTIDAD = item.CANTIDAD;
            itemDetalle.CANTIDAD_CONTADA = item.CANTIDAD_CONTADA;
            itemDetalle.DIFERENCIA = itemDetalle.CANTIDAD -  itemDetalle.CANTIDAD_CONTADA ;
            itemDetalle.NUMERO_PLACA = item.NUMERO_PLACA;
            itemDetalle.VALOR_ACTUAL = item.VALOR_ACTUAL;
            itemDetalle.ARTICULO = item.ARTICULO;
            itemDetalle.ESPECIFICACION = item.ESPECIFICACION;
            itemDetalle.SERVICIO = item.SERVICIO;
            itemDetalle.RESPONSABLE_BIEN = item.RESPONSABLE_BIEN;
            itemDetalle.FECHA_MOVIMIENTO = item.FECHA_MOVIMIENTO;
            itemDetalle.CODIGO_BIEN = item.CODIGO_BIEN;
            itemDetalle.CODIGO_MOV_BIEN = item.CODIGO_MOV_BIEN;
            itemDetalle.COMENTARIO = item.COMENTARIO;
            itemDetalle.CODIGO_BIEN = item.CODIGO_BIEN;
            itemDetalle.USUARIO_INS = item.USUARIO_INS;
            itemDetalle.FECHA_INS = item.FECHA_INS;
            itemDetalle.USUARIO_UPD = item.USUARIO_UPD;
            itemDetalle.FECHA_UPD = item.FECHA_UPD;
            itemDetalle.CODIGO_EMPRESA = item.CODIGO_EMPRESA;
            return itemDetalle;
        }
        public async Task<ResultDto<BmConteoDeleteDto>> Delete(BmConteoDeleteDto dto)
        {

            ResultDto<BmConteoDeleteDto> result = new ResultDto<BmConteoDeleteDto>(null);
            try
            {

                var conteo = await _repository.GetByCodigo(dto.CodigoBmConteo);
                if (conteo == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Conteo no existe";
                    return result;
                }

                var conteoIniciado = await _conteoDetalleService.ConteoIniciado(dto.CodigoBmConteo);
                if (conteoIniciado==true)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "No puede eliminar un conteo Iniciado";
                    return result;
                }

                var deleteDetalle = await _conteoDetalleService.DeleteRangeConteo(dto.CodigoBmConteo);
                if (deleteDetalle)
                {
                    var deleted = await _repository.Delete(conteo);

                    if (deleted.Length > 0)
                    {
                        result.Data = dto;
                        result.IsValid = false;
                        result.Message = deleted;
                    }
                    else
                    {
                        result.Data = dto;
                        result.IsValid = true;
                        result.Message = deleted;

                    }
                }
               


            }
            catch (Exception ex)
            {
                result.Data = dto;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }

        public async Task<ResultDto<bool>> CerrarConteo(BmConteoCerrarDto dto)
        {

            ResultDto<bool> result = new ResultDto<bool>(false);
            try
            {
                if (String.IsNullOrEmpty(dto.Comentario))
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = "No puede cerrar un conteo sin Comentario";
                    return result;
                }

                var conteo = await _repository.GetByCodigo(dto.CodigoBmConteo);
                if (conteo == null)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = "Conteo no existe";
                    return result;
                }

                var conteoIniciado = await _conteoDetalleService.ConteoIniciado(dto.CodigoBmConteo);
                if (conteoIniciado==false)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = "No puede Cerrar un conteo que no ha Iniciado";
                    return result;
                }
                
                var  conteoIniciadoConDiferenciaSinComentario=await _conteoDetalleService.ConteoIniciadoConDiferenciaSinComentario(dto.CodigoBmConteo);
                if (conteoIniciadoConDiferenciaSinComentario==true)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = "No puede Cerrar un conteo con diferencia sin colocar comentarios";
                    return result;
                }

                BmConteoFilterDto filter = new BmConteoFilterDto();
                filter.CodigoBmConteo = dto.CodigoBmConteo;
                var  cuadreConteo=await _conteoDetalleService.ComparaConteo(filter);
                if (cuadreConteo.Data.Count>0)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = "No puede Cerrar un conteo con diferencia entre Conteos";
                    return result;
                }
           
                var conectado = await _sisUsuarioRepository.GetConectado();
                if (conteo.COMENTARIO != dto.Comentario)
                {
                    conteo.COMENTARIO = dto.Comentario;
             
                    conteo.CODIGO_EMPRESA = conectado.Empresa;
                    conteo.USUARIO_UPD = conectado.Usuario;
                    conteo.FECHA_UPD = DateTime.Now;

                    await _repository.Update(conteo);
                }
                //TODO COPIAR REGISTROS DE CONTEO Y DETALLE A EL HISTORICO 
                var conteoHistorico = await BmConteoToHistrorico(conteo);
                var resulUpdateConteoHistorico=await _bmConteoHistoricoRepository.Add(conteoHistorico);

                var conteoDetalle = await _conteoDetalleService.GetByCodigoConteo(dto.CodigoBmConteo);
                if (conteoDetalle.Count > 0)
                {
                    List<BM_CONTEO_DETALLE_HISTORICO> listHistorico = new List<BM_CONTEO_DETALLE_HISTORICO>();
                    foreach (var item in conteoDetalle)
                    {
                        var itemDetalle = BmConteoDetalleToHistorico(item);
                        listHistorico.Add(itemDetalle);
                    }
                    var addHistorico=await _bmConteoDetalleHistoricoRepository.AddRange(listHistorico);
                    //BORRAR CONTEO DEL TEMPORAL
                    if (addHistorico.IsValid)
                    {
                        var deleteDetalle = await _conteoDetalleService.DeleteRangeConteo(dto.CodigoBmConteo);
                        if (deleteDetalle)
                        {
                            var deleted = await _repository.Delete(conteo);
                        }
                    }
                   
                }
                
                result.Data = true;
                result.IsValid = true;
                result.Message = "Conteo Cerrado Satisfactoriamente";
                return result;
               


            }
            catch (Exception ex)
            {
                result.Data = false;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }

        public List<ICPGetDto> GetResumenICP(List<BM_CONTEO_DETALLE> dto)
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
        public List<ResumenConteoGetDto> GetResumenConteo(List<BM_CONTEO_DETALLE> dto)
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

        public async Task<ResultDto<BmConteoResponseDto>> GetByCodigoConteo(int conteiID)
        {

            ResultDto<BmConteoResponseDto> result = new ResultDto<BmConteoResponseDto>(null);
            try
            {

                var conteo = await _repository.GetByCodigo(conteiID);



                if (conteo != null)
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

        public async Task CreateReportConteo(int conteoId)
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


            BmConteoFilterDto filter = new BmConteoFilterDto();
            

            var connteo = await GetByCodigoConteo(conteoId);
            
            var detalle = await _bmConteoDetalleRepository.GetAllByConteo(conteoId);

            var resumenIcp = GetResumenICP(detalle);
            var resumenConteo = GetResumenConteo(detalle);

            
            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var destino = @settings.BmFiles;

            var fileName = $"{destino}Conteo-{conteoId.ToString()}.pdf";
            try
            {
                if (connteo == null)
                {
                    var ex = new IOException();
                    connteo.Data = null;
                    connteo.IsValid = false;
                    connteo.Message = ex.Message;
                }

                else
                {
                    Document.Create(documento =>
                    {
                        documento.Page(page =>
                        {
                            page.Margin(20);

                            page.Header().Row(fila =>
                            {

                                fila.ConstantItem(140).Border(0).Height(60).Image(filePath: destino + "LogoIzquierda.jpeg")
                                .FitWidth().FitHeight();
                                fila.Spacing(4);
                                fila.RelativeItem().Border(0).Column(col =>
                                {
                                    col.Item().AlignCenter().Text("Conteo en Proceso").Bold().FontSize(14);

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

                                        foreach (var item in detalle.Where(x => x.CODIGO_ICP == itemResumenIcp.CodigoIcp).ToList())
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
                                    pie.RelativeItem().PaddingRight(2).Element(BlockCabeceraTotal).Text("Total Contada");
                                    pie.RelativeItem().PaddingRight(2).Element(BlockCabeceraTotal).Text("Total Diferencia");

                                });

                                col1.Item().Row(pie =>
                                {
                                    pie.ConstantItem(350).AlignRight().PaddingRight(2).Element(BlockTotales).AlignRight().Text(connteo.Data.TotalCantidad);
                                    pie.RelativeColumn().PaddingRight(2).Element(BlockTotales).AlignRight().Text(connteo.Data.TotalCantidadContado);
                                    pie.RelativeColumn().PaddingRight(2).Element(BlockTotales).AlignRight().Text(connteo.Data.TotalDiferencia);

                                });
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

