using System;
using System.Collections.Generic;
using System.Globalization;
using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Data.Repository.Rh;
using Convertidor.Dtos.Bm;
using Convertidor.Services.Catastro;
using Convertidor.Utility;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using NuGet.Packaging;

namespace Convertidor.Services.Bm
{
    public class BmConteoDetalleService: IBmConteoDetalleService
    {

      
        private readonly IBmConteoDetalleRepository _repository;
        private readonly IBM_V_BM1Service _bm1Service;

        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhPersonasRepository _rhPersonasRepository;
        private readonly IBmConteoRepository _bmConteoRepository;
        private readonly IBmDescriptivaRepository _bmDescriptivaRepository;
        private readonly IConfiguration _configuration;
        public BmConteoDetalleService(IBmConteoDetalleRepository repository,
                                IBM_V_BM1Service bm1Service, 
                                ISisUsuarioRepository sisUsuarioRepository,
                                IRhPersonasRepository rhPersonasRepository,
                                IBmConteoRepository bmConteoRepository,
                                IBmDescriptivaRepository bmDescriptivaRepository,
                                IConfiguration configuration)
		{
            _repository = repository;
            _bm1Service = bm1Service;
            _sisUsuarioRepository = sisUsuarioRepository;
            _rhPersonasRepository = rhPersonasRepository;
            _bmConteoRepository = bmConteoRepository;
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

    public async Task<List<BM_CONTEO_DETALLE>> GetByCodigoConteo(int codigoConteo)
    {
        return await _repository.GetAllByConteo(codigoConteo);
    }
    
     public async Task<ResultDto<List<BmConteoDetalleResumenResponseDto>>> GetResumen(int codigoConteo)
    {
       
       
        ResultDto<List<BmConteoDetalleResumenResponseDto>> response = new ResultDto<List<BmConteoDetalleResumenResponseDto>>(null);
        try
        {
            
            
            var result = await _repository.GetAllByConteo(codigoConteo);
      
                var lista = from s in result
                              group s by new
                              {
                                  
                                  CodigoBmConteo = s.CODIGO_BM_CONTEO,
                                  Conteo = s.CONTEO,
                                  Cantidad=s.CANTIDAD,
                                  CantidadContada=s.CANTIDAD_CONTADA
                                
                                  
                              } into g
                              select new BmConteoDetalleResumenResponseDto()
                              {
                                 
                                  CodigoBmConteo = g.Key.CodigoBmConteo,
                                  Conteo = g.Key.Conteo,
                                  Cantidad = g.Sum(u => u.CANTIDAD),
                                  CantidadContada = g.Sum(u => u.CANTIDAD_CONTADA)
                                  
                                

                              };
                
            
           
              
                
                
            response.Data = lista.ToList();
            response.IsValid = true;
            response.Message = "";
            response.LinkData= $"";
            return response;
        }
        catch (Exception ex)
        {
            response.Data = null;
            response.IsValid = true;
            response.Message = ex.InnerException.Message;
            return response;
        }
       
    }

     public async Task<ResultDto<List<BmConteoDetalleResponseDto>>> GetAll()
        {
           
           
            ResultDto<List<BmConteoDetalleResponseDto>> response = new ResultDto<List<BmConteoDetalleResponseDto>>(null);
            try
            {
                
                
                var result = await _repository.GetAll();
          
                    var lista = from s in result
                                  group s by new
                                  {
                                      CodigoBmConteoDetalle = s.CODIGO_BM_CONTEO_DETALLE,
                                      CodigoBmConteo = s.CODIGO_BM_CONTEO,
                                      Conteo = s.CONTEO,
                                      CodigoIcp = s.CODIGO_ICP,
                                      UnidadTrabajo = s.UNIDAD_TRABAJO,
                                      CodigoGrupo = s.CODIGO_GRUPO,
                                      CodigoNivel1 = s.CODIGO_NIVEL1,
                                      CodigoNivel2 = s.CODIGO_NIVEL2,
                                      NumeroLote = s.NUMERO_LOTE,
                                      Cantidad = s.CANTIDAD,
                                      CantidadContada = s.CANTIDAD_CONTADA,
                                      Diferencia = s.DIFERENCIA,
                                      NumeroPlaca=s.NUMERO_PLACA,
                                      ValorActual=s.VALOR_ACTUAL,
                                      Articulo=s.ARTICULO,
                                      Especificacion=s.ESPECIFICACION,
                                      Servicio=s.SERVICIO,
                                      ResponsableBien=s.RESPONSABLE_BIEN,
                                      FechaMovimiento=s.FECHA_MOVIMIENTO,
                                      FechaMovimientoString=s.FECHA_MOVIMIENTO.ToString("u"),
                                      FechaMovimientoObj = GetFechaDto(s.FECHA_MOVIMIENTO),
                                      CodigoBien=s.CODIGO_BIEN,
                                      CodigoMovBien=s.CODIGO_MOV_BIEN,
                                      Comentario=s.COMENTARIO
                                    
                                      
                                  } into g
                                  select new BmConteoDetalleResponseDto()
                                  {
                                      CodigoBmConteoDetalle = g.Key.CodigoBmConteoDetalle,
                                      CodigoBmConteo = g.Key.CodigoBmConteo,
                                      Conteo = g.Key.Conteo,
                                      CodigoIcp = g.Key.CodigoIcp,
                                      UnidadTrabajo = g.Key.UnidadTrabajo,
                                      CodigoGrupo = g.Key.CodigoGrupo,
                                      CodigoNivel1 = g.Key.CodigoNivel1,
                                      CodigoNivel2 = g.Key.CodigoNivel2,
                                      NumeroLote = g.Key.NumeroLote,
                                      Cantidad = g.Key.Cantidad,
                                      CantidadContada = g.Key.CantidadContada,
                                      Diferencia = g.Key.Diferencia,
                                      NumeroPlaca=g.Key.NumeroPlaca,
                                      ValorActual=g.Key.ValorActual,
                                      Articulo=g.Key.Articulo,
                                      Especificacion=g.Key.Especificacion,
                                      Servicio=g.Key.Servicio,
                                      ResponsableBien=g.Key.ResponsableBien,
                                      FechaMovimiento=g.Key.FechaMovimiento,
                                      FechaMovimientoString=g.Key.FechaMovimientoString,
                                      FechaMovimientoObj = g.Key.FechaMovimientoObj,
                                      CodigoBien=g.Key.CodigoBien,
                                      CodigoMovBien=g.Key.CodigoMovBien,
                                      Comentario=g.Key.Comentario
                                   
                                    

                                  };
                    
                
               
                  
                    
                    
                response.Data = lista.ToList();
                response.IsValid = true;
                response.Message = "";
                response.LinkData= $"";
                return response;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.IsValid = true;
                response.Message = ex.InnerException.Message;
                return response;
            }
           
        }
   
       public async Task<ResultDto<List<BmConteoDetalleResponseDto>>> GetAllByConteo(BmConteoFilterDto filter)
        {
           
           
            ResultDto<List<BmConteoDetalleResponseDto>> response = new ResultDto<List<BmConteoDetalleResponseDto>>(null);
            try
            {
                
                
                var result = await _repository.GetAllByConteo(filter.CodigoBmConteo);
          
                    var lista = from s in result
                                  group s by new
                                  {
                                      CodigoBmConteoDetalle = s.CODIGO_BM_CONTEO_DETALLE,
                                      CodigoBmConteo = s.CODIGO_BM_CONTEO,
                                      Conteo = s.CONTEO,
                                      CodigoIcp = s.CODIGO_ICP,
                                      UnidadTrabajo = s.UNIDAD_TRABAJO,
                                      CodigoGrupo = s.CODIGO_GRUPO,
                                      CodigoNivel1 = s.CODIGO_NIVEL1,
                                      CodigoNivel2 = s.CODIGO_NIVEL2,
                                      NumeroLote = s.NUMERO_LOTE,
                                      Cantidad = s.CANTIDAD,
                                      CantidadContada = s.CANTIDAD_CONTADA,
                                      Diferencia = s.DIFERENCIA,
                                      NumeroPlaca=s.NUMERO_PLACA,
                                      ValorActual=s.VALOR_ACTUAL,
                                      Articulo=s.ARTICULO,
                                      Especificacion=s.ESPECIFICACION,
                                      Servicio=s.SERVICIO,
                                      ResponsableBien=s.RESPONSABLE_BIEN,
                                      FechaMovimiento=s.FECHA_MOVIMIENTO,
                                      FechaMovimientoString=s.FECHA_MOVIMIENTO.ToString("u"),
                                      FechaMovimientoObj = GetFechaDto(s.FECHA_MOVIMIENTO),
                                      CodigoBien=s.CODIGO_BIEN,
                                      CodigoMovBien=s.CODIGO_MOV_BIEN,
                                      Comentario=s.COMENTARIO
                                    
                                      
                                  } into g
                                  select new BmConteoDetalleResponseDto()
                                  {
                                      CodigoBmConteoDetalle = g.Key.CodigoBmConteoDetalle,
                                      CodigoBmConteo = g.Key.CodigoBmConteo,
                                      Conteo = g.Key.Conteo,
                                      CodigoIcp = g.Key.CodigoIcp,
                                      UnidadTrabajo = g.Key.UnidadTrabajo,
                                      CodigoGrupo = g.Key.CodigoGrupo,
                                      CodigoNivel1 = g.Key.CodigoNivel1,
                                      CodigoNivel2 = g.Key.CodigoNivel2,
                                      NumeroLote = g.Key.NumeroLote,
                                      Cantidad = g.Key.Cantidad,
                                      CantidadContada = g.Key.CantidadContada,
                                      Diferencia = g.Key.Diferencia,
                                      NumeroPlaca=g.Key.NumeroPlaca,
                                      ValorActual=g.Key.ValorActual,
                                      Articulo=g.Key.Articulo,
                                      Especificacion=g.Key.Especificacion,
                                      Servicio=g.Key.Servicio,
                                      ResponsableBien=g.Key.ResponsableBien,
                                      FechaMovimiento=g.Key.FechaMovimiento,
                                      FechaMovimientoString=g.Key.FechaMovimientoString,
                                      FechaMovimientoObj = g.Key.FechaMovimientoObj,
                                      CodigoBien=g.Key.CodigoBien,
                                      CodigoMovBien=g.Key.CodigoMovBien,
                                      Comentario=g.Key.Comentario
                                   
                                    

                                  };
                    
                
               
                  
                    
                    
                response.Data = lista.ToList();
                response.IsValid = true;
                response.Message = "";
                response.LinkData= $"";
                return response;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.IsValid = true;
                response.Message = ex.InnerException.Message;
                return response;
            }
           
        }


   
     public async Task<ResultDto<List<BmConteoDetalleResponseDto>>> ComparaConteo(BmConteoFilterDto filter)
        {
           
            var detalleConteo = await GetAllByConteo(filter);
            ResultDto<List<BmConteoDetalleResponseDto>> response = new ResultDto<List<BmConteoDetalleResponseDto>>(null);
            List<BmConteoDetalleResponseDto> lista = new List<BmConteoDetalleResponseDto>();
            List<BmConteoDetalleResponseDto> listaResponse = new List<BmConteoDetalleResponseDto>();
            try
            {
                var conteo = await _bmConteoRepository.GetByCodigo(filter.CodigoBmConteo);
                if (conteo != null)
                {
                    
                    var conteoDescriptiva = await _bmDescriptivaRepository.GetByCodigo(conteo.CANTIDAD_CONTEOS_ID);
                    var cantidadConteos = Int32.Parse(conteoDescriptiva.DESCRIPCION);
                    
                   

                    var primerConteo = detalleConteo.Data.Where(x => x.Conteo == 1).ToList();
                    foreach (var item in primerConteo)
                    {
                        if (cantidadConteos == 1)
                        {
                            item.CantidadContadaOtroConteo = item.CantidadContada;
                            
                        }
                        else
                        {
                            var otroConteo = detalleConteo.Data
                                .Where(x => x.Conteo == 2 && x.CodigoBien == item.CodigoBien).FirstOrDefault();
                            if (otroConteo != null)
                            {
                                item.CantidadContadaOtroConteo = otroConteo.CantidadContada;
                            }
                        }
                        lista.Add(item);
                        
                    }
                }
                
                var diferencias= lista.Where(x=> x.CantidadContada != x.CantidadContadaOtroConteo).ToList();
                foreach (var itemDiferencias in diferencias)
                {
                    var listDif = detalleConteo.Data.Where(x => x.NumeroPlaca == itemDiferencias.NumeroPlaca).ToList();
                    var find = listaResponse.Where(x => x.NumeroPlaca == itemDiferencias.NumeroPlaca).FirstOrDefault();
                    if (find == null)
                    {
                        listaResponse.AddRange(listDif);
                    }
                   
                }
                response.Data = listaResponse;
                response.IsValid = true;
                response.Message = "";
                response.LinkData= $"";
                return response;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.IsValid = true;
                response.Message = ex.InnerException.Message;
                return response;
            }
           
        }
   
       
       
       
     public async Task<ResultDto<List<BmConteoDetalleResponseDto>>> Update(BmConteoDetalleUpdateDto dto)
        {

            ResultDto<List<BmConteoDetalleResponseDto>> result = new ResultDto<List<BmConteoDetalleResponseDto>>(null);
            try
            {
                var conteo = await _repository.GetByCodigo(dto.CodigoBmConteoDetalle);
                if (conteo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Conteo no Existe";
                    return result;
                }

                conteo.CANTIDAD_CONTADA = dto.CantidadContada;
                conteo.DIFERENCIA = conteo.CANTIDAD - conteo.CANTIDAD_CONTADA;
                conteo.COMENTARIO = dto.Comentario;

                var conectado = await _sisUsuarioRepository.GetConectado();
                conteo.CODIGO_EMPRESA = conectado.Empresa;
                conteo.USUARIO_UPD = conectado.Usuario;
                conteo.FECHA_UPD = DateTime.Now;

                await _repository.Update(conteo);
                BmConteoFilterDto filterConteo = new BmConteoFilterDto();
                filterConteo.CodigoBmConteo = conteo.CODIGO_BM_CONTEO;
                result = await GetAllByConteo(filterConteo);
                return result;

            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }



        }

     
     public async Task<ResultDto<List<BmConteoDetalleResponseDto>>> CreaDetalleConteoDesdeBm1SP(int codigoConteo,int cantidadConteo,List<ICPGetDto> listIcpSeleccionado,int codigoResponsable )
        {
           
           
            
            
            ResultDto<List<BmConteoDetalleResponseDto>> response = new ResultDto<List<BmConteoDetalleResponseDto>>(null);
            try
            {

                string unidadTrabajoParameter = "";
                var conectado = await _sisUsuarioRepository.GetConectado();
             
                listIcpSeleccionado = listIcpSeleccionado.Where(x => x.CodigoIcp > 0).ToList();
                List<Bm1GetDto> searchList = new List<Bm1GetDto>();
                if (listIcpSeleccionado.Count > 0 )
                {
                    foreach (var item in listIcpSeleccionado)
                    {
                        if (unidadTrabajoParameter.Length == 0)
                        {
                            unidadTrabajoParameter = $"{item.CodigoIcp}";
                        }
                        else
                        {
                            unidadTrabajoParameter = $"{unidadTrabajoParameter},{item.CodigoIcp}";
                        }
                       
                       
                    }
                }
                
                if (unidadTrabajoParameter.Length == 0) unidadTrabajoParameter = "TODOS";
                var bmConteoDetalleCreated =_repository.CrearDesdeBm1(unidadTrabajoParameter, conectado.Empresa,codigoResponsable, codigoConteo,
                    cantidadConteo);
                if (bmConteoDetalleCreated.Data == true)
                {
                    BmConteoFilterDto filterConteo = new BmConteoFilterDto();
                    filterConteo.CodigoBmConteo = codigoConteo;
                    response = await GetAllByConteo(filterConteo);
                }
                else
                {
                    response.IsValid = false;
                    response.Message = bmConteoDetalleCreated.Message;
                    response.Data = null;
                }
                
              
                
                return response;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.IsValid = true;
                response.Message = ex.InnerException.Message;
                return response;
            }
           
        }
   
     public async Task<ResultDto<List<BmConteoDetalleResponseDto>>> CreaDetalleConteoDesdeBm1(int codigoConteo,int cantidadConteo,List<ICPGetDto> listIcpSeleccionado,int codigoResponsable )
        {
           
           
            
            
            ResultDto<List<BmConteoDetalleResponseDto>> response = new ResultDto<List<BmConteoDetalleResponseDto>>(null);
            try
            {

                string unidadTrabajoParameter = "";
                var conectado = await _sisUsuarioRepository.GetConectado();
               
                listIcpSeleccionado = listIcpSeleccionado.Where(x => x.CodigoIcp > 0).ToList();
                List<Bm1GetDto> searchList = new List<Bm1GetDto>();
                if (listIcpSeleccionado.Count > 0 )
                {
                    foreach (var item in listIcpSeleccionado)
                    {

                        var itemFilter = await _bm1Service.GetAllByIcp(item.CodigoIcp,DateTime.Now.AddYears(-20),DateTime.Now);
                        if (itemFilter.Data.Count > 0)
                        {
                            unidadTrabajoParameter = $"{unidadTrabajoParameter},{item.CodigoIcp}";
                            searchList.AddRange(itemFilter.Data);
                            
                        }
                    }
                }
                else
                {
                    var bm1= await _bm1Service.GetAll(DateTime.Now.AddYears(-20),DateTime.Now);
                    searchList = bm1.Data;
                }

                //if (unidadTrabajoParameter.Length == 0) unidadTrabajoParameter = "TODOS";
                /*_repository.CrearDesdeBm1(unidadTrabajoParameter, conectado.Empresa,codigoResponsable, codigoConteo,
                    cantidadConteo);*/
                var nextKey =await _repository.GetNextKey();
                  
                List<BM_CONTEO_DETALLE> entities = new List<BM_CONTEO_DETALLE>();
                for (int i = 0; i < cantidadConteo; i++)
                {
                    //List<BM_CONTEO_DETALLE> entites = new List<BM_CONTEO_DETALLE>();
                 
                    foreach (var item in searchList)
                    {
                        BM_CONTEO_DETALLE entity = new BM_CONTEO_DETALLE();
                        entity.CODIGO_BM_CONTEO_DETALLE = nextKey;
                        entity.CODIGO_BM_CONTEO = codigoConteo;
                        entity.CONTEO = i+1;
                        entity.CODIGO_ICP = item.CodigoIcp;
                        entity.UNIDAD_TRABAJO = item.UnidadTrabajo;
                        entity.CODIGO_GRUPO = item.CodigoGrupo;
                        entity.CODIGO_NIVEL1 = item.CodigoNivel1;
                        entity.CODIGO_NIVEL2 = item.CodigoNivel2;
                        entity.NUMERO_LOTE = item.NumeroLote;
                        entity.CANTIDAD = item.Cantidad;
                        entity.CANTIDAD_CONTADA = 0;
                        entity.DIFERENCIA =item.Cantidad;
                        entity.NUMERO_PLACA =item.NumeroPlaca;
                        entity.VALOR_ACTUAL =item.ValorActual;
                        entity.ARTICULO=item.Articulo;
                        entity.ESPECIFICACION =item.Especificacion;
                        entity.SERVICIO = item.Servicio;
                        entity.RESPONSABLE_BIEN = item.ResponsableBien;
                        entity.FECHA_MOVIMIENTO = item.FechaMovimiento;
                        entity.CODIGO_BIEN = item.CodigoBien;
                        entity.CODIGO_MOV_BIEN = item.CodigoMovBien;
                        entity.COMENTARIO = "";
                        
                       
                        entity.CODIGO_EMPRESA = conectado.Empresa;
                        entity.USUARIO_INS = conectado.Usuario;
                        entity.FECHA_INS = DateTime.Now;
                        entities.Add(entity);
                        
                        nextKey = nextKey + 1;

                    }
                  

               
                }
                var created = await _repository.AddRange(entities);
                if (created.IsValid == true)
                {
                    BmConteoFilterDto filterConteo = new BmConteoFilterDto();
                    filterConteo.CodigoBmConteo = codigoConteo;
                    response = await GetAllByConteo(filterConteo);
                }
                else
                {
                    response.IsValid = false;
                    response.Message = created.Message;
                    response.Data = null;
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.IsValid = true;
                response.Message = ex.InnerException.Message;
                return response;
            }
           
        }
   

        public async Task<bool> ConteoIniciado(int codigoConteo)
        {

            var conteoIniciado = await _repository.ConteoIniciado(codigoConteo);
          
            return conteoIniciado;
          
        }


        public async Task<bool> DeleteRangeConteo(int codigoConteo)
        {
            var deleted = await _repository.DeleteRangeConteo(codigoConteo);
            return deleted;
        }

        public async Task<bool> ConteoIniciadoConDiferenciaSinComentario(int codigoConteo)
        {
            return await _repository.ConteoIniciadoConDiferenciaSinComentario(codigoConteo);
        }


    }

}

