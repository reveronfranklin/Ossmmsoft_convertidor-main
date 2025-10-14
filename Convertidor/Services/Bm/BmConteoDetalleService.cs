using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Dtos.Bm;
using Convertidor.Dtos.Bm.Mobil;
using Convertidor.Utility;

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
        private readonly IBmUbicacionesRepository _bmUbicacionesRepository;

        public BmConteoDetalleService(IBmConteoDetalleRepository repository,
                                IBM_V_BM1Service bm1Service, 
                                ISisUsuarioRepository sisUsuarioRepository,
                                IRhPersonasRepository rhPersonasRepository,
                                IBmConteoRepository bmConteoRepository,
                                IBmDescriptivaRepository bmDescriptivaRepository,
                                IConfiguration configuration,
                                IBmUbicacionesRepository bmUbicacionesRepository)
		{
            _repository = repository;
            _bm1Service = bm1Service;
            _sisUsuarioRepository = sisUsuarioRepository;
            _rhPersonasRepository = rhPersonasRepository;
            _bmConteoRepository = bmConteoRepository;
            _bmDescriptivaRepository = bmDescriptivaRepository;
            _configuration = configuration;
            _bmUbicacionesRepository = bmUbicacionesRepository;
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
                                      FechaMovimientoString=Fecha.GetFechaString(s.FECHA_MOVIMIENTO),
                                      FechaMovimientoObj = Fecha.GetFechaDto(s.FECHA_MOVIMIENTO),
                                      CodigoBien=s.CODIGO_BIEN,
                                      CodigoMovBien=s.CODIGO_MOV_BIEN,
                                      Comentario=s.COMENTARIO,
                                      ReplicarComentario=s.REPLICAR_COMENTARIO
                                    
                                      
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
                                      Comentario=g.Key.Comentario,
                                      ReplicarComentario=g.Key.ReplicarComentario
                                    

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
                                      FechaMovimientoString=Fecha.GetFechaString(s.FECHA_MOVIMIENTO),
                                      FechaMovimientoObj = Fecha.GetFechaDto(s.FECHA_MOVIMIENTO),
                                      CodigoBien=s.CODIGO_BIEN,
                                      CodigoMovBien=s.CODIGO_MOV_BIEN,
                                      Comentario=s.COMENTARIO,
                                      ReplicarComentario=s.REPLICAR_COMENTARIO
                                    
                                      
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
                                      Comentario=g.Key.Comentario,
                                      ReplicarComentario = g.Key.ReplicarComentario,
                                   
                                    

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
                conteo.REPLICAR_COMENTARIO = 0;
                if (dto.ReplicarComentario)
                {
                    conteo.REPLICAR_COMENTARIO = 1;
                    
                }
                
                

                var conectado = await _sisUsuarioRepository.GetConectado();
                conteo.CODIGO_EMPRESA = conectado.Empresa;
                conteo.USUARIO_UPD = conectado.Usuario;
                conteo.FECHA_UPD = DateTime.Now;

                await _repository.Update(conteo);
                if (dto.ReplicarComentario)
                {
                   _repository.ReplicarComentario(conteo.CODIGO_BM_CONTEO,dto.Comentario);
                    
                }
                
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

        public async Task<string> ValidateConteo(List<ConteoCreateDto> dto)
        {
            
            string result = "";
            foreach (var item in dto)
            {
                int CodigoBmConteo = 0;
              
                var keyArray = item.KeyUbicacionResponsable.Split("-");
                CodigoBmConteo=Convert.ToInt32(keyArray[0]);
                var conteo = await _repository.GetByCodigo(CodigoBmConteo);
                if (conteo == null)
                {
                    result=$"No existe un conteo con id:{CodigoBmConteo}";
                    return result;
                }

                var existeConteo = await _repository.ExisteConteo(CodigoBmConteo);
                if (existeConteo == false)
                {
                    result=$"No existe un conteo con id:{CodigoBmConteo} en detalle de Conteo";
                    return result;
                }
                
                var bm1 = await _bm1Service.GetByNroPlaca(item.NroPlaca);
                if (bm1 == null)
                {
                    result=$"Placa no encontrada: {item.NroPlaca}";
                    return result;
                }
                

            }

            return result;
        }
        public async Task<ResultDto<bool>>  RecibeConteo(List<ConteoCreateDto> dto)
        {
            
            var conectado = await _sisUsuarioRepository.GetConectado();
            int CodigoBmConteo = 0;
            var Conteo = 0;
            var CodigoDirBien = 0;
            ResultDto<bool> response = new ResultDto<bool>(false);

            try
            {
            var validarConteo = await ValidateConteo(dto);
            if (validarConteo != "")
            {
                response.IsValid = false;
                response.Message = validarConteo;
                response.Data = false;
                return response;
            }
            
            foreach (var item in dto)
            {
                var keyArray = item.KeyUbicacionResponsable.Split("-");
                CodigoBmConteo=Convert.ToInt32(keyArray[0]);
                Conteo=Convert.ToInt32(keyArray[1]);
                var ubicacion = await _bmUbicacionesRepository.GetByCodigoDirBien(item.CodigoDirBien);
                var conteoDetalle = await _repository.GetByCodigoConteoConteoIcpPlaca(CodigoBmConteo,Conteo,ubicacion.CODIGO_ICP,item.NroPlaca);
                if (conteoDetalle != null)
                {
                    conteoDetalle.CANTIDAD_CONTADA = 1;
                    conteoDetalle.DIFERENCIA =conteoDetalle.CANTIDAD-conteoDetalle.CANTIDAD_CONTADA;
                    conteoDetalle.CODIGO_ICP = ubicacion.CODIGO_ICP;
                    conteoDetalle.UNIDAD_TRABAJO = ubicacion.UNIDAD_EJECUTORA;
                    if (item.UbicacionFisica != null && item.UbicacionFisica != 0)
                    {
                        conteoDetalle.CODIGO_ICP_FISICO = item.UbicacionFisica;
                    }
                    else
                    {
                        conteoDetalle.CODIGO_ICP_FISICO = ubicacion.CODIGO_ICP;
                    }
                    await _repository.Update(conteoDetalle);
                }
                else
                {
                    var nextKey =await _repository.GetNextKey();
                    var bm1 = await _bm1Service.GetByNroPlaca(item.NroPlaca);
                    if (bm1 != null)
                    {
                        BM_CONTEO_DETALLE entity = new BM_CONTEO_DETALLE();
                        entity.CODIGO_BM_CONTEO_DETALLE = nextKey;
                        entity.CODIGO_BM_CONTEO = CodigoBmConteo;
                        entity.CONTEO = Conteo;
                        entity.CODIGO_ICP = ubicacion.CODIGO_ICP;
                        entity.UNIDAD_TRABAJO = ubicacion.UNIDAD_EJECUTORA;
                        entity.CODIGO_GRUPO = bm1.CodigoGrupo;
                        entity.CODIGO_NIVEL1 = bm1.CodigoNivel1;
                        entity.CODIGO_NIVEL2 = bm1.CodigoNivel2;
                        entity.NUMERO_LOTE = bm1.NumeroLote;
                        entity.CANTIDAD = bm1.Cantidad;
                        entity.CANTIDAD_CONTADA =1;
                        entity.DIFERENCIA =entity.CANTIDAD-entity.CANTIDAD_CONTADA;
                        entity.NUMERO_PLACA =item.NroPlaca;
                        entity.VALOR_ACTUAL =bm1.ValorActual;
                        entity.ARTICULO=bm1.Articulo;
                        entity.ESPECIFICACION =bm1.Especificacion;
                        entity.SERVICIO = bm1.Servicio;
                        entity.RESPONSABLE_BIEN = bm1.ResponsableBien;
                        entity.FECHA_MOVIMIENTO = bm1.FechaMovimiento;
                        entity.CODIGO_BIEN = bm1.CodigoBien;
                        entity.CODIGO_MOV_BIEN = bm1.CodigoMovBien;
                        entity.COMENTARIO = "";
                        entity.REPLICAR_COMENTARIO = 0;
                        if (item.UbicacionFisica != null && item.UbicacionFisica != 0)
                        {
                            entity.CODIGO_ICP_FISICO = item.UbicacionFisica;
                        }
                        else
                        {
                            entity.CODIGO_ICP_FISICO = ubicacion.CODIGO_ICP;
                        }
                        
                       
                        entity.CODIGO_EMPRESA = conectado.Empresa;
                        entity.USUARIO_INS = conectado.Usuario;
                        entity.FECHA_INS = DateTime.Now;
                        var created = await _repository.Add(entity);
                    }
                  
                }
            }
            
            response.IsValid = true;
            response.Message = "";
            response.Data = true;
            return response;
            }
            catch (Exception e)
            {
                response.IsValid = false;
                response.Message = e.Message;
                response.Data = false;
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

