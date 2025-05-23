﻿using System.Diagnostics;
using System.Globalization;
using System.Text;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Services.Adm;
using Convertidor.Utility;
using Microsoft.Extensions.Caching.Distributed;

namespace Convertidor.Services.Presupuesto
{
	public class PRE_PRESUPUESTOSService: IPRE_PRESUPUESTOSService
    {

        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;
        
        private readonly IPRE_V_DENOMINACION_PUCRepository _preDenominacionPucRepository;
        private readonly IPRE_V_SALDOSRepository _pre_V_SALDOSRepository;
        private readonly IPRE_ASIGNACIONESRepository _PRE_ASIGNACIONESRepository;
        private readonly IPRE_INDICE_CAT_PRGRepository _PRE_INDICE_CAT_PRGRepository;
        private readonly IPRE_PLAN_UNICO_CUENTASRepository _pRE_PLAN_UNICO_CUENTASRepository;
        private readonly IIndiceCategoriaProgramaService _indiceCategoriaProgramaService;
        private readonly IPrePlanUnicoCuentasService _prePlanUnicoCuentasService;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IMapper _mapper;
        private readonly IAdmPucSolicitudService _admPucSolicitudService;
        private readonly IPrePucSolicitudModificacionService _prePucSolicitudModificacionService;
        private readonly IPRE_SALDOSRepository _preSaldosRepository;
        private readonly IDistributedCache _distributedCache;


        public PRE_PRESUPUESTOSService(IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository,
                            
                                        IPRE_V_DENOMINACION_PUCRepository preDenominacionPucRepository,
                                        IPRE_V_SALDOSRepository pre_V_SALDOSRepository,
                                        IPRE_ASIGNACIONESRepository PRE_ASIGNACIONESRepository,
                                        IPRE_INDICE_CAT_PRGRepository PRE_INDICE_CAT_PRGRepository,
                                        IIndiceCategoriaProgramaService indiceCategoriaProgramaService,
                                        IPRE_PLAN_UNICO_CUENTASRepository pRE_PLAN_UNICO_CUENTASRepository,
                                        IPrePlanUnicoCuentasService prePlanUnicoCuentasService,
                                        ISisUsuarioRepository sisUsuarioRepository,
                                        IMapper mapper,
                                        IAdmPucSolicitudService admPucSolicitudService,
                                        IPrePucSolicitudModificacionService prePucSolicitudModificacionService,
                                        IPRE_SALDOSRepository preSaldosRepository,
                                        IDistributedCache distributedCache
                                        )
        {
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
            
            _preDenominacionPucRepository = preDenominacionPucRepository;
            _pre_V_SALDOSRepository = pre_V_SALDOSRepository;
            _PRE_ASIGNACIONESRepository = PRE_ASIGNACIONESRepository;
            _PRE_INDICE_CAT_PRGRepository = PRE_INDICE_CAT_PRGRepository;
            _indiceCategoriaProgramaService = indiceCategoriaProgramaService;
            _pRE_PLAN_UNICO_CUENTASRepository = pRE_PLAN_UNICO_CUENTASRepository;
            _prePlanUnicoCuentasService = prePlanUnicoCuentasService;
            _sisUsuarioRepository = sisUsuarioRepository;
            _mapper = mapper;
            _admPucSolicitudService = admPucSolicitudService;
            _prePucSolicitudModificacionService = prePucSolicitudModificacionService;
            _preSaldosRepository = preSaldosRepository;
            _distributedCache = distributedCache;
        }



        public async Task<List<PRE_PRESUPUESTOS>> GetAllPresupuestoEntity()
        {
            var presupuesto = await _pRE_PRESUPUESTOSRepository.GetAll();
            return presupuesto.ToList();
        }


        public async Task<ResultDto<GetPRE_PRESUPUESTOSDto>> GetByCodigo(FilterPRE_PRESUPUESTOSDto filter)
        {

            ResultDto<GetPRE_PRESUPUESTOSDto> result = new ResultDto<GetPRE_PRESUPUESTOSDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var presupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa, filter.CodigoPresupuesto);
                if (presupuesto != null)
                {
                    var dto = await MapPrePresupuestoToGetPrePresupuestoDto(presupuesto,filter);
                    result.Data = dto;
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


        public async Task<ListPresupuestoDto> MapPresupuesto(PRE_PRESUPUESTOS item)
        {
        
            ListPresupuestoDto dto = new ListPresupuestoDto();
            dto.CodigoPresupuesto = item.CODIGO_PRESUPUESTO;
            dto.Descripcion = item.DENOMINACION;
            dto.Ano = item.ANO;
            var preListFinanciado = await _pre_V_SALDOSRepository.GetListFinanciadoPorPresupuesto(dto.CodigoPresupuesto);
            if (preListFinanciado.Count > 0) {

                dto.PreFinanciadoDto = preListFinanciado;
            }

            dto.presupuestoEnEjecucion = false;
            var presupuestoExiste = await _pre_V_SALDOSRepository.PresupuestoExiste(item.CODIGO_PRESUPUESTO);
            if (presupuestoExiste)
            {
                dto.presupuestoEnEjecucion = presupuestoExiste;
            }

            return dto;
        }

        public async Task<List<ListPresupuestoDto>> MapListPresupuesto(List<PRE_PRESUPUESTOS> presupuestos)
        {
            var watch = Stopwatch.StartNew();
        
            
            List<ListPresupuestoDto> result = new List<ListPresupuestoDto>();
            
            List<ListPresupuestoDto> resultParalelismo = new List<ListPresupuestoDto>();
            
            
            foreach (var item in presupuestos.OrderByDescending(x => x.FECHA_HASTA).ToList())
            {
             
                var dto = await  MapPresupuesto(item);
                result.Add(dto);
            }
            
            
            
            watch.Stop();
            
            Console.WriteLine($"Total de resultados For each: {watch.ElapsedMilliseconds}");
          
        
            
         

            return result;
        }
        public async Task<ResultDto<List<ListPresupuestoDto>>> GetListPresupuesto()
        {

            ResultDto<List<ListPresupuestoDto>> result = new ResultDto<List<ListPresupuestoDto>>(null);
            try
            {
                
                
                    var presupuesto = await _pRE_PRESUPUESTOSRepository.GetAll();
                    if (presupuesto.Count() > 0)
                    {
                       

                        var listDto=await MapListPresupuesto(presupuesto.ToList());


                        result.Data = listDto;

                        result.IsValid = true;
                        result.Message = "";
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


        public async Task<ResultDto<List<GetPRE_PRESUPUESTOSDto>>> GetAll(FilterPRE_PRESUPUESTOSDto filter)
        {

            ResultDto<List<GetPRE_PRESUPUESTOSDto>> result = new ResultDto<List<GetPRE_PRESUPUESTOSDto>>(null);
            try
            {
                
                var presupuesto = await _pRE_PRESUPUESTOSRepository.GetAll();

               /* if (filter.CodigoPresupuesto == 0) {

                    var ultimo = presupuesto.OrderByDescending(x => x.CODIGO_PRESUPUESTO).ToList();

                    if (ultimo.Count > 0) {

                        int codigoPresupuesto = ultimo.FirstOrDefault().CODIGO_PRESUPUESTO;
                        presupuesto = presupuesto.Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto).ToList();

                    }
                }
                else {
                    presupuesto = presupuesto.Where(x => x.CODIGO_PRESUPUESTO == filter.CodigoPresupuesto).ToList();
                }*/


                if (presupuesto.Count() >0)
                    {
                    List< GetPRE_PRESUPUESTOSDto> listDto = new List<GetPRE_PRESUPUESTOSDto>();

                    foreach (var item in presupuesto.OrderByDescending(X=>X.CODIGO_PRESUPUESTO).ToList())
                    {
                        var dto = await MapPrePresupuestoToGetPrePresupuestoDto(item,filter);
                        listDto.Add(dto);
                    }

                   
                    result.Data = listDto.OrderByDescending(x=>x.FechaHasta).ToList();
                    
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


        public async Task<ResultDto<List<GetPRE_PRESUPUESTOSDto>>> GetList(FilterPRE_PRESUPUESTOSDto filter)
        {

            ResultDto<List<GetPRE_PRESUPUESTOSDto>> result = new ResultDto<List<GetPRE_PRESUPUESTOSDto>>(null);
            try
            {




                var presupuesto = await _pRE_PRESUPUESTOSRepository.GetAll();

              


                if (presupuesto.Count() > 0)
                {
                    List<GetPRE_PRESUPUESTOSDto> listDto = new List<GetPRE_PRESUPUESTOSDto>();

                    foreach (var item in presupuesto.OrderByDescending(X => X.CODIGO_PRESUPUESTO).ToList())
                    {
                        var dto = await MapPrePresupuestoToGetPrePresupuestoDtoCrud(item, filter);
                        listDto.Add(dto);
                    }


                    result.Data = listDto.OrderByDescending(x => x.FechaHasta).ToList();

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


        
        public async Task<ResultDto<DeletePrePresupuestoDto>> Delete(DeletePrePresupuestoDto dto)
        {

            ResultDto<DeletePrePresupuestoDto> result = new ResultDto<DeletePrePresupuestoDto>(null);
            try
            {

                var presupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(13, dto.CodigoPresupuesto);
                if (presupuesto == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Presupuesto no existe";
                    return result;
                }

                var presupuestoExiste = await _PRE_ASIGNACIONESRepository.PresupuestoExisteConMonto(dto.CodigoPresupuesto);
                if (presupuestoExiste)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Presupuesto no puede ser eliminado,tiene Movimiento creado";
                    return result;
                }

                
                //TODO ELIMINAR PRE_SALDO
                /*
                 *SE PUEDE BORRAR DE PRE_SALDOS, SIEMPRE QUE NO SE TENGA
                   
                   EJECUTADO EL PRESUPUESTO.
                   LAS TABLAS DONDE INICIA LA EJECUCION DEL PRESUPUESTO SON:
                 *
                 *
                 * ADM_PUC_SOLICITUD.
                   PRE_PUC_SOL_MODIFICACION
                 */
                var presupuestoExisteSolicitud = await _admPucSolicitudService.PresupuestoExiste(dto.CodigoPresupuesto);
                if (presupuestoExisteSolicitud.Data)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Presupuesto no puede ser eliminado,tiene Movimiento PUC_SOLICITUD";
                    return result;
                }

                var presupuestoExisteSolicitudModificacion =
                    await _prePucSolicitudModificacionService.PresupuestoExiste(dto.CodigoPresupuesto);
                if (presupuestoExisteSolicitudModificacion)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Presupuesto no puede ser eliminado,tiene Solicitud de Modificacion";
                    return result;
                }
                
                await _indiceCategoriaProgramaService.DeleteByCodigoPresupuesto(dto.CodigoPresupuesto);
                await _prePlanUnicoCuentasService.DeleteByCodigoPresupuesto(dto.CodigoPresupuesto);
                await _PRE_ASIGNACIONESRepository.DeleteByPresupuesto(dto.CodigoPresupuesto);
                await _preSaldosRepository.DeleteByPresupuesto(dto.CodigoPresupuesto);
                
                var  deleted =await _pRE_PRESUPUESTOSRepository.Delete(13,dto.CodigoPresupuesto);
                
                if (deleted.Length>0)
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
            catch (Exception ex)
            {
                result.Data = dto;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }


        public async Task<PRE_PRESUPUESTOS> GetUltimo()
        {
            try
            {

                var result = await _pRE_PRESUPUESTOSRepository.GetUltimo();
                   
                return (PRE_PRESUPUESTOS)result!;
               
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }

        public async Task<ResultDto<GetPRE_PRESUPUESTOSDto>> Create(CreatePRE_PRESUPUESTOSDto dto)
        {

            ResultDto<GetPRE_PRESUPUESTOSDto> result = new ResultDto<GetPRE_PRESUPUESTOSDto>(null);
            try
            {

                if (dto.Denominacion.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Invalida";
                    return result;
                }
                if (dto.Ano <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Periodo Presupuesto  Invalido";
                    return result;
                }
               var fechaDesde = Convert.ToDateTime(dto.FechaDesde, CultureInfo.InvariantCulture);
                var fechaHasta = Convert.ToDateTime(dto.FechaHasta, CultureInfo.InvariantCulture);
                var existePeriodo = await _pRE_PRESUPUESTOSRepository.ExisteEnPeriodo(dto.CodigoEmpresa, fechaDesde, fechaHasta);
                if (existePeriodo)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Periodo existe en el presupuesto";
                    return result;
                }

                var entity = MapCreatePrePresupuestoDtoToPrePresupuesto(dto);
                entity.CODIGO_PRESUPUESTO = await _pRE_PRESUPUESTOSRepository.GetNextKey();
                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                var created = await _pRE_PRESUPUESTOSRepository.Add(entity);
                
                if (created.IsValid && created.Data!=null)
                {
                    
                    int codigoPresupuestoOrigen = created.Data.CODIGO_PRESUPUESTO - 1;
                    
                    var resultClonadoPUC = await _pRE_PLAN_UNICO_CUENTASRepository.ClonarByCodigoPresupuesto(codigoPresupuestoOrigen, created.Data.CODIGO_PRESUPUESTO,conectado.Usuario,conectado.Empresa);
                    if (resultClonadoPUC.IsValid == false)
                    {
                        DeletePrePresupuestoDto dtoDelete = new DeletePrePresupuestoDto();
                        dtoDelete.CodigoPresupuesto = created.Data.CODIGO_PRESUPUESTO;
                        await Delete(dtoDelete);
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = resultClonadoPUC.Message;
                        return result;
                    }
                    
                    
                    var resultClonado = await _PRE_INDICE_CAT_PRGRepository.ClonarByCodigoPresupuesto(codigoPresupuestoOrigen, created.Data.CODIGO_PRESUPUESTO,conectado.Usuario,conectado.Empresa);
                    if (resultClonado.IsValid == false)
                    {
                        DeletePrePresupuestoDto dtoDelete = new DeletePrePresupuestoDto();
                        dtoDelete.CodigoPresupuesto = created.Data.CODIGO_PRESUPUESTO;
                        await Delete(dtoDelete);
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = resultClonado.Message;
                        return result;
                    }


                }


                FilterPRE_PRESUPUESTOSDto filter = new FilterPRE_PRESUPUESTOSDto();
                filter.FinanciadoId = 0;
                filter.CodigoPresupuesto = entity.CODIGO_PRESUPUESTO;
                var resultDto = await MapPrePresupuestoToGetPrePresupuestoDto(created.Data,filter);
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

        public async Task<ResultDto<GetPRE_PRESUPUESTOSDto>> Update(UpdatePRE_PRESUPUESTOSDto dto)
        {

            ResultDto<GetPRE_PRESUPUESTOSDto> result = new ResultDto<GetPRE_PRESUPUESTOSDto>(null);
            try
            {

                var presupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(dto.CodigoEmpresa, dto.CodigoPresupuesto);
                if (presupuesto==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Presupuesto no existe";
                    return result;
                }
                if (dto.Denominacion.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Invalida";
                    return result;
                }
                if (dto.Ano <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Periodo Presupuesto  Invalido";
                    return result;
                }
                /*if (dto.MontoPresupuesto <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Presupuesto  Invalido";
                    return result;
                }*/

                var conectado = await _sisUsuarioRepository.GetConectado();

                presupuesto.DENOMINACION = dto.Denominacion;
                presupuesto.DESCRIPCION = dto.Descripcion;
                presupuesto.ANO = dto.Ano;
                presupuesto.MONTO_PRESUPUESTO = dto.MontoPresupuesto;
                
                
                DateTime fechaDesde = DateTime.ParseExact(dto.FechaDesde, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                presupuesto.FECHA_DESDE = fechaDesde;
                
                DateTime fechaHasta = DateTime.ParseExact(dto.FechaHasta, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                presupuesto.FECHA_HASTA = fechaHasta;
          
                string format = "yyyy-MM-ddTHH:mm:ss.fffZ";

                DateTime fechaAprobacion = DateTime.ParseExact(dto.FechaAprobacion, format, CultureInfo.InvariantCulture);

                
                presupuesto.FECHA_APROBACION = fechaAprobacion;  
                
                presupuesto.NUMERO_ORDENANZA = dto.NumeroOrdenanza;
                DateTime fechaOrdenanza = DateTime.ParseExact(dto.FechaOrdenanza, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                presupuesto.FECHA_ORDENANZA = fechaOrdenanza;
                
                presupuesto.EXTRA1 = dto.Extra1;
                presupuesto.EXTRA2 = dto.Extra2;
                presupuesto.EXTRA3 = dto.Extra3;
                presupuesto.USUARIO_UPD = conectado.Usuario;
                presupuesto.CODIGO_EMPRESA = conectado.Empresa;
                presupuesto.FECHA_UPD = DateTime.Now;
               
                await _pRE_PRESUPUESTOSRepository.Update(presupuesto);
             

                FilterPRE_PRESUPUESTOSDto filter = new FilterPRE_PRESUPUESTOSDto();
                filter.FinanciadoId = 0;
                filter.CodigoPresupuesto = presupuesto.CODIGO_PRESUPUESTO;
                var resultDto = await MapPrePresupuestoToGetPrePresupuestoDtoCrud(presupuesto,filter);
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

        public async Task<ResultDto<GetPRE_PRESUPUESTOSDto>> AprobarPresupuesto(int codigoPresupuesto,string fechaAprobacion)
        {
            
            
            var conectado = await _sisUsuarioRepository.GetConectado();
            ResultDto<GetPRE_PRESUPUESTOSDto> result = new ResultDto<GetPRE_PRESUPUESTOSDto>(null);
            try
            {

                if (!Convertidor.Utility.DateValidate.IsDate(fechaAprobacion))
                {
                    result.IsValid = false;
                    result.Message = "Fecha de Aprobacion invalida";
                    return result;
                }
                
                var presupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa, codigoPresupuesto);
                if (presupuesto==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Presupuesto no existe";
                    return result;
                }
                if(presupuesto.FECHA_APROBACION!=null) {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = " No existe un Presupuesto Activo.";
                    return result;
                }
                var presupuestoExiste = await _PRE_ASIGNACIONESRepository.PresupuestoExisteConMonto(codigoPresupuesto);
                if (!presupuestoExiste)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No puede aprobar un presupuesto sin asignaciones";
                    return result;
                }

                var preSaldoExiste = await _pre_V_SALDOSRepository.PresupuestoExiste(codigoPresupuesto);
                if (preSaldoExiste)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No puede aprobar un presupuesto ya APROBADO(EXISTE EN SALDO)";
                    return result;
                }
                
                await _pRE_PRESUPUESTOSRepository.AprobarPresupuesto(codigoPresupuesto, conectado.Usuario,
                    conectado.Empresa);
           
                presupuesto.FECHA_APROBACION = Convert.ToDateTime(fechaAprobacion, CultureInfo.InvariantCulture);  
                presupuesto.USUARIO_UPD = conectado.Usuario;
                presupuesto.CODIGO_EMPRESA = conectado.Empresa;
                presupuesto.FECHA_UPD = DateTime.Now;
                await _pRE_PRESUPUESTOSRepository.Update(presupuesto);
                
                FilterPRE_PRESUPUESTOSDto filter = new FilterPRE_PRESUPUESTOSDto();
                filter.FinanciadoId = 0;
                filter.CodigoPresupuesto = presupuesto.CODIGO_PRESUPUESTO;
                var resultDto = await MapPrePresupuestoToGetPrePresupuestoDtoCrud(presupuesto,filter);
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

     

        public async Task<GetPRE_PRESUPUESTOSDto> MapPrePresupuestoToGetPrePresupuestoDto(PRE_PRESUPUESTOS entity, FilterPRE_PRESUPUESTOSDto filterDto)
        {
            GetPRE_PRESUPUESTOSDto dto = new GetPRE_PRESUPUESTOSDto();
            dto.CodigoPresupuesto = entity.CODIGO_PRESUPUESTO;
            dto.Denominacion = entity.DENOMINACION;
            dto.Descripcion = entity.DESCRIPCION ?? "";
            dto.Ano = entity.ANO;
            dto.MontoPresupuesto = entity.MONTO_PRESUPUESTO;
            dto.FechaDesde = entity.FECHA_DESDE;          
            dto.FechaHasta = entity.FECHA_HASTA;  
            dto.FechaDesdeString = Fecha.GetFechaString(entity.FECHA_DESDE);          
            dto.FechaHastaString = Fecha.GetFechaString(entity.FECHA_HASTA);  
            FechaDto FechaDesdeObj = Fecha.GetFechaDto(entity.FECHA_DESDE);
            dto.FechaDesdeObj = (FechaDto)FechaDesdeObj;
            FechaDto FechaHastaObj = Fecha.GetFechaDto(entity.FECHA_HASTA);
            dto.FechaHastaObj = (FechaDto)FechaHastaObj;

            if (entity.FECHA_APROBACION != null)
            {
                dto.FechaAprobacion= (DateTime)entity.FECHA_APROBACION;
         
                dto.FechaAprobacionString=  Fecha.GetFechaString(dto.FechaAprobacion);
            
          
                FechaDto FechaAprobacionObj = Fecha.GetFechaDto((DateTime)entity.FECHA_APROBACION);
                dto.FechaAprobacionObj = (FechaDto)FechaAprobacionObj;
            }
           
            
            dto.NumeroOrdenanza = entity.NUMERO_ORDENANZA;

            dto.FechaOrdenanza = entity.FECHA_ORDENANZA;
            dto.FechaOrdenanzaString = Fecha.GetFechaString(entity.FECHA_ORDENANZA);
            FechaDto FechaOrdenanzaObj = Fecha.GetFechaDto(entity.FECHA_ORDENANZA);
            dto.FechaOrdenanzaObj = (FechaDto)FechaOrdenanzaObj;
            dto.presupuestoEnEjecucion = false;
            var presupuestoExiste = await _pre_V_SALDOSRepository.PresupuestoExiste(entity.CODIGO_PRESUPUESTO);
            if (presupuestoExiste)
            {
                dto.presupuestoEnEjecucion = presupuestoExiste;
            }

            dto.Extra1 = entity.EXTRA1;
            dto.Extra2 = entity.EXTRA2;
            dto.Extra2 = entity.EXTRA3;
            dto.TotalPresupuesto = 0;
            dto.TotalDisponible = 0;
            dto.TotalPresupuestoString = "";
            dto.TotalDisponibleString = "";
            dto.TotalModificacion = 0;
            dto.TotalModificacionString = "";
            dto.TotalVigente = 0;
            dto.TotalVigenteString = "";
            //var preDenominacionPuc = await _preDenominacionPucRepository.GetByCodigoPresupuesto(dto.CodigoPresupuesto);
            List<GetPRE_V_DENOMINACION_PUCDto> listpreDenominacionPuc = new List<GetPRE_V_DENOMINACION_PUCDto>();
            FilterPreDenominacionDto filter = new FilterPreDenominacionDto();
            filter.FinanciadoId = 0;
           
           
            filter.CodigoPresupuesto = entity.CODIGO_PRESUPUESTO;

            int financiadoInt = filterDto.FinanciadoId;
            if (financiadoInt > 0) {
                filter.FinanciadoId = financiadoInt;
            }
            
            filter.FechaDesde = filterDto.FechaDesde;
            filter.FechaHasta = filterDto.FechaHasta;
            filter.CodigoGrupo = "4";
            filter.Nivel = 1;
                var preDenominacionPuc = await _pre_V_SALDOSRepository.GetPreVDenominacionPorPartidaPuc(filter);
            if (preDenominacionPuc.Data!= null && preDenominacionPuc.Data.Count() > 0)
            {
                foreach (var item in preDenominacionPuc.Data)
                {
                    GetPRE_V_DENOMINACION_PUCDto itemPreDenominacionPuc = new GetPRE_V_DENOMINACION_PUCDto();
                    itemPreDenominacionPuc.AnoSaldo = filter.FechaDesde.Year;
                    itemPreDenominacionPuc.MesSaldo = filter.FechaDesde.Month;
                    itemPreDenominacionPuc.CodigoPresupuesto = item.CodigoPresupuesto;
                    itemPreDenominacionPuc.CodigoPartida = item.CodigoPartida;
                    itemPreDenominacionPuc.CodigoGenerica = item.CodigoGenerica;
                    itemPreDenominacionPuc.CodigoEspecifica = item.CodigoEspecifica;
                    itemPreDenominacionPuc.CodigoSubEspecifica = item.CodigoSubEspecifica;
                    itemPreDenominacionPuc.CodigoNivel5 = item.CodigoNivel5;
                    itemPreDenominacionPuc.DenominacionPuc = item.DenominacionPuc;
                    itemPreDenominacionPuc.Presupuestado = item.Presupuestado;
                    itemPreDenominacionPuc.Modificado = item.Modificado;
                    itemPreDenominacionPuc.Comprometido = item.Comprometido;
                    itemPreDenominacionPuc.Causado = item.Causado;
                    itemPreDenominacionPuc.Modificado = item.Modificado;
                    itemPreDenominacionPuc.Pagado = item.Pagado;
                    itemPreDenominacionPuc.Deuda = item.Deuda;
                    itemPreDenominacionPuc.Disponibilidad = item.Disponibilidad;
                    itemPreDenominacionPuc.DisponibilidadFinan = item.DisponibilidadFinan;
                   
                    listpreDenominacionPuc.Add(itemPreDenominacionPuc);
                }

                // if (dto.TotalPresupuesto > 1000) dto.TotalPresupuesto = dto.TotalPresupuesto / 1000;
                //if (dto.TotalDisponible > 1000) dto.TotalDisponible = dto.TotalDisponible / 1000;

                //listpreDenominacionPuc = listpreDenominacionPuc.Where(X => X.Presupuestado > 0).ToList();
                dto.PreDenominacionPuc = listpreDenominacionPuc;

                var preDenominacionPucresumen = ResumenePreDenominacionPuc(listpreDenominacionPuc);


                if (listpreDenominacionPuc.Count > 0) { 
                    dto.PreDenominacionPucResumen = preDenominacionPucresumen;

                    foreach (var item in listpreDenominacionPuc)
                    {
                        dto.TotalPresupuesto = dto.TotalPresupuesto + item.Presupuestado;
                        var partida = Int32.Parse(item.CodigoPartida);
                        if (  partida> 0 && item.CodigoGenerica == "00" && item.CodigoEspecifica == "00" && item.CodigoSubEspecifica == "00" && item.CodigoNivel5 == "00")
                        {
                            dto.TotalDisponible = dto.TotalDisponible + item.Disponibilidad;
                            dto.TotalModificacion = dto.TotalModificacion + item.Modificado;
                        }
                      
                        
                    }

                    dto.TotalVigente = dto.TotalModificacion + dto.TotalPresupuesto;

                    dto.TotalVigenteString = ConvertidorMoneda.ConvertMoneda(dto.TotalVigente);

                    dto.TotalPresupuestoString = ConvertidorMoneda.ConvertMoneda(dto.TotalPresupuesto);

                    dto.TotalDisponibleString = ConvertidorMoneda.ConvertMoneda(dto.TotalDisponible);

                    dto.TotalModificacionString = ConvertidorMoneda.ConvertMoneda(dto.TotalModificacion); 
                }

                var preListFinanciado = await _pre_V_SALDOSRepository.GetListFinanciadoPorPresupuesto(dto.CodigoPresupuesto);
                if (preListFinanciado.Count > 0) {

                    dto.PreFinanciadoDto = preListFinanciado;
                }


            }
            

            return dto;

        }


        public async Task<GetPRE_PRESUPUESTOSDto> MapPrePresupuestoToGetPrePresupuestoDtoCrud(PRE_PRESUPUESTOS entity, FilterPRE_PRESUPUESTOSDto filterDto)
        {
            GetPRE_PRESUPUESTOSDto dto = new GetPRE_PRESUPUESTOSDto();
            dto.CodigoPresupuesto = entity.CODIGO_PRESUPUESTO;
            dto.Denominacion = entity.DENOMINACION;
            dto.Descripcion = entity.DESCRIPCION ?? "";
            dto.Ano = entity.ANO;
            dto.MontoPresupuesto = entity.MONTO_PRESUPUESTO;
            dto.FechaDesde = entity.FECHA_DESDE;
            dto.FechaDesdeString = Fecha.GetFechaString( entity.FECHA_DESDE);
            FechaDto FechaDesdeObj = Fecha.GetFechaDto(entity.FECHA_DESDE);
            dto.FechaDesdeObj = (FechaDto)FechaDesdeObj;
            
            dto.FechaHasta = entity.FECHA_HASTA;
            dto.FechaHastaString = Fecha.GetFechaString( entity.FECHA_HASTA);
            FechaDto FechaHastaObj = Fecha.GetFechaDto(entity.FECHA_HASTA);
            dto.FechaHastaObj = (FechaDto)FechaHastaObj;
            if (entity.FECHA_APROBACION != null)
            {
                dto.FechaAprobacion = (DateTime)entity.FECHA_APROBACION;
                dto.FechaAprobacionString = Fecha.GetFechaString( dto.FechaAprobacion);
                FechaDto FechaAprobacionObj = Fecha.GetFechaDto((DateTime)entity.FECHA_APROBACION);
                dto.FechaAprobacionObj = (FechaDto)FechaAprobacionObj;
            }
       
           
            
            
            dto.NumeroOrdenanza = entity.NUMERO_ORDENANZA;

            dto.FechaOrdenanza = entity.FECHA_ORDENANZA;
            dto.FechaOrdenanzaString = Fecha.GetFechaString( entity.FECHA_ORDENANZA);
            FechaDto FechaOrdenanzaObj = Fecha.GetFechaDto(entity.FECHA_ORDENANZA);
            dto.FechaOrdenanzaObj = (FechaDto)FechaOrdenanzaObj;
            
            dto.Extra1 = entity.EXTRA1;
            dto.Extra2 = entity.EXTRA2;
            dto.Extra2 = entity.EXTRA3;
            dto.TotalPresupuesto = 0;
            dto.TotalDisponible = 0;
            dto.TotalPresupuestoString = "";
            dto.TotalDisponibleString = "";
            dto.TotalModificacion = 0;
            dto.TotalModificacionString = "";
            dto.TotalVigente = 0;
            dto.TotalVigenteString = "";
          


            return dto;

        }





        public List<GetPreDenominacionPucResumenAnoDto> ResumenePreDenominacionPuc(List<GetPRE_V_DENOMINACION_PUCDto> dto)
        {
            List<GetPreDenominacionPucResumenAnoDto> result = new List<GetPreDenominacionPucResumenAnoDto>();
            if (dto.Count > 0)
            {



                foreach (var item in dto)
                {

                        GetPreDenominacionPucResumenAnoDto itemResult = new GetPreDenominacionPucResumenAnoDto();
                        itemResult.AnoSaldo = item.AnoSaldo;
                        itemResult.CodigoPresupuesto = item.CodigoPresupuesto;
                        itemResult.CodigoPartida = item.CodigoPartida;
                        itemResult.CodigoGenerica = item.CodigoGenerica;
                        itemResult.CodigoEspecifica = item.CodigoEspecifica;
                        itemResult.CodigoSubEspecifica = item.CodigoSubEspecifica;
                        itemResult.CodigoNivel5 = item.CodigoNivel5;
                        itemResult.DenominacionPuc = item.DenominacionPuc;

                        itemResult.Presupuestado = item.Presupuestado;
                        itemResult.Modificado = item.Modificado;
                        itemResult.Comprometido = item.Comprometido;
                        itemResult.Causado = item.Causado;
                        itemResult.Pagado =item.Pagado;
                        itemResult.Deuda = item.Deuda;
                        itemResult.Disponibilidad = item.Disponibilidad;
                        itemResult.DisponibilidadFinan = item.DisponibilidadFinan;

                        result.Add(itemResult);

                            

                }

              
             
                

            }



            return result;



        }

        public PRE_PRESUPUESTOS MapCreatePrePresupuestoDtoToPrePresupuesto(CreatePRE_PRESUPUESTOSDto dto)
        {
            PRE_PRESUPUESTOS entity = new PRE_PRESUPUESTOS();

           
            entity.DENOMINACION = dto.Denominacion;
            entity.DESCRIPCION = dto.Descripcion;
            entity.ANO = dto.Ano;
            entity.MONTO_PRESUPUESTO = dto.MontoPresupuesto;
            entity.FECHA_DESDE = Convert.ToDateTime(dto.FechaDesde, CultureInfo.InvariantCulture); 
            entity.FECHA_HASTA = Convert.ToDateTime(dto.FechaHasta, CultureInfo.InvariantCulture);
            DateTime dDate;

            if (DateTime.TryParse(dto.FechaAprobacion, out dDate))
            {
                entity.FECHA_APROBACION = Convert.ToDateTime(dto.FechaAprobacion, CultureInfo.InvariantCulture);
            }
     
           
            entity.NUMERO_ORDENANZA = dto.NumeroOrdenanza;
            if (DateTime.TryParse(dto.FechaOrdenanza, out dDate))
            {
                entity.FECHA_ORDENANZA = Convert.ToDateTime(dto.FechaOrdenanza, CultureInfo.InvariantCulture);
            }
           
            entity.EXTRA1 = dto.Extra1;
            entity.EXTRA2 = dto.Extra2;
            entity.EXTRA3 = dto.Extra3;
            entity.USUARIO_INS = dto.UsuarioIns;
            entity.FECHA_INS = DateTime.Now;
            entity.CODIGO_EMPRESA = dto.CodigoEmpresa;
            return entity;


        }

        public PRE_PRESUPUESTOS MapUpdatePrePresupuestoDtoToPrePresupuesto(UpdatePRE_PRESUPUESTOSDto dto)
        {
            PRE_PRESUPUESTOS entity = new PRE_PRESUPUESTOS();


            entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
            entity.DENOMINACION = dto.Denominacion;
            entity.DESCRIPCION = dto.Descripcion;
            entity.ANO = dto.Ano;
            entity.MONTO_PRESUPUESTO = dto.MontoPresupuesto;
            entity.FECHA_DESDE = Convert.ToDateTime(dto.FechaDesde, CultureInfo.InvariantCulture);  
            entity.FECHA_HASTA = Convert.ToDateTime(dto.FechaHasta, CultureInfo.InvariantCulture); 
            entity.FECHA_APROBACION = Convert.ToDateTime(dto.FechaAprobacion, CultureInfo.InvariantCulture);  
            entity.NUMERO_ORDENANZA = dto.NumeroOrdenanza;
            entity.FECHA_ORDENANZA = Convert.ToDateTime(dto.FechaOrdenanza, CultureInfo.InvariantCulture); 
            entity.EXTRA1 = dto.Extra1;
            entity.EXTRA2 = dto.Extra2;
            entity.EXTRA3 = dto.Extra3;
            entity.USUARIO_UPD = dto.UsuarioUpd;
            entity.CODIGO_EMPRESA = dto.CodigoEmpresa;

            return entity;




        }





    }
}

