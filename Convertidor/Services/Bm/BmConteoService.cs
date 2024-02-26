using System;
using System.Collections.Generic;
using System.Globalization;
using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Data.Repository.Rh;
using Convertidor.Dtos.Bm;
using Convertidor.Utility;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using NuGet.Packaging;

namespace Convertidor.Services.Bm
{
    public class BmConteoService: IBmConteoService
    {

      
        private readonly IBmConteoRepository _repository;
     
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhPersonasRepository _rhPersonasRepository;
        private readonly IBmConteoDetalleService _conteoDetalleService;
        private readonly IBmDescriptivaRepository _bmDescriptivaRepository;
        private readonly IBmConteoHistoricoRepository _bmConteoHistoricoRepository;
        private readonly IBmConteoDetalleHistoricoRepository _bmConteoDetalleHistoricoRepository;
        private readonly IConfiguration _configuration;
        public BmConteoService(IBmConteoRepository repository,
                                ISisUsuarioRepository sisUsuarioRepository,
                                IRhPersonasRepository rhPersonasRepository,
                                IBmConteoDetalleService conteoDetalleService,
                                IBmDescriptivaRepository bmDescriptivaRepository,
                                IBmConteoHistoricoRepository bmConteoHistoricoRepository,
                                IBmConteoDetalleHistoricoRepository bmConteoDetalleHistoricoRepository,
                                IConfiguration configuration)
		{
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _rhPersonasRepository = rhPersonasRepository;
            _conteoDetalleService = conteoDetalleService;
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
                conteoHistorico.TOTAL_CANTIDAD_CONTADA = conteoHistorico.TOTAL_CANTIDAD_CONTADA  + item.CantidadContada;
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
            itemDetalle.DIFERENCIA = item.DIFERENCIA;
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

    }
}

