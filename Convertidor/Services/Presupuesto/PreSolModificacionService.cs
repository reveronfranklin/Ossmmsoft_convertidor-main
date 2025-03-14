﻿using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Utility;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NPOI.OpenXmlFormats.Vml.Office;

namespace Convertidor.Services.Presupuesto
{
	public class PreSolModificacionService: IPreSolModificacionService
    {

      
        private readonly IPreSolModificacionRepository _repository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;
        private readonly IPreDescriptivaRepository _repositoryPreDescriptiva;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhPersonasRepository _personasRepository;
        private readonly IPreModificacionRepository _preModificacionRepository;
   
        private readonly IPRE_V_SALDOSRepository _preVSaldosRepository;
        private readonly IPRE_SALDOSRepository _preSaldosRepository;
        private readonly IPreModificacionService _preModificacionService;
        private readonly IPrePucModificacionService _prePucModificacionService;
        private readonly IPrePucModificacionRepository _prePucModificacionRepository;
        private readonly IPRE_INDICE_CAT_PRGRepository _preIndiceCatPrgRepository;
        private readonly IPrePucSolicitudModificacionRepository _prePucSolicitudModificacionRepository;

        public PreSolModificacionService(IPreSolModificacionRepository repository,
                                      IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository,
                                      IPreDescriptivaRepository repositoryPreDescriptiva,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      IRhPersonasRepository personasRepository,
                                      IPreModificacionRepository preModificacionRepository,
                                      IPrePucSolicitudModificacionRepository prePucSolicitudModificacionRepository,
                                      IPRE_V_SALDOSRepository preVSaldosRepository,
                                      IPRE_SALDOSRepository preSaldosRepository,
                                      IPreModificacionService preModificacionService,
                                      IPrePucModificacionService prePucModificacionService,
                                      IPrePucModificacionRepository prePucModificacionRepository,
                                      IPRE_INDICE_CAT_PRGRepository preIndiceCatPrgRepository
                                      
        )
		{
            _repository = repository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
            _repositoryPreDescriptiva = repositoryPreDescriptiva;
            _sisUsuarioRepository = sisUsuarioRepository;
            _personasRepository = personasRepository;
            _preModificacionRepository = preModificacionRepository;
            _prePucSolicitudModificacionRepository = prePucSolicitudModificacionRepository;
            _preVSaldosRepository = preVSaldosRepository;
            _preSaldosRepository = preSaldosRepository;
            _preModificacionService = preModificacionService;
            _prePucModificacionService = prePucModificacionService;
            _prePucModificacionRepository = prePucModificacionRepository;
            _preIndiceCatPrgRepository = preIndiceCatPrgRepository;
        }


        public async Task<ResultDto<List<PreSolModificacionResponseDto>>> GetAll()
        {

            ResultDto<List<PreSolModificacionResponseDto>> result = new ResultDto<List<PreSolModificacionResponseDto>>(null);
            try
            {

                var solModificacion = await _repository.GetAll();

                var descriptivaTipoSolicitud = await _repositoryPreDescriptiva.GetByTitulo(8);

                if (solModificacion.Count() > 0)
                {
                    List<PreSolModificacionResponseDto> listDto = new List<PreSolModificacionResponseDto>();

                    foreach (var item in solModificacion)
                    {
                        var dto = await MapPreSolModificacion(item,descriptivaTipoSolicitud);
                        listDto.Add(dto);
                    }


                    result.Data = listDto;

                    result.IsValid = true;
                    result.Message = "";
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = "No existen Datos";

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

        public async Task<ResultDto<List<PreSolModificacionResponseDto>>>  GetByPresupuesto(FilterPresupuestoDto filter)
        {
            ResultDto<List<PreSolModificacionResponseDto>> result = new ResultDto<List<PreSolModificacionResponseDto>>(null);
            try
            {

                var solModificacion = await _repository.GetByPresupuesto(filter.CodigoPresupuesto);

                var descriptivaTipoSolicitud = await _repositoryPreDescriptiva.GetByTitulo(8);

                if (solModificacion.Count() > 0)
                {
                    List<PreSolModificacionResponseDto> listDto = new List<PreSolModificacionResponseDto>();

                    foreach (var item in solModificacion)
                    {
                        var dto = await MapPreSolModificacion(item,descriptivaTipoSolicitud);
                        listDto.Add(dto);
                    }


                    result.Data = listDto.OrderByDescending(x=>x.CodigoSolModificacion).ToList();

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
        
        public List<StatusSolicitudModificacion> GetListStatus()
        {
            List<StatusSolicitudModificacion> result = new List<StatusSolicitudModificacion>();
            StatusSolicitudModificacion anulada = new StatusSolicitudModificacion();
            anulada.Codigo = "AN";
            anulada.Descripcion = "ANULADA";
            StatusSolicitudModificacion pendiente = new StatusSolicitudModificacion();
            pendiente.Codigo = "PE";
            pendiente.Descripcion = "PENDIENTE";
            
            StatusSolicitudModificacion aprobada = new StatusSolicitudModificacion();
            aprobada.Codigo = "AP";
            aprobada.Descripcion = "APROBADA";
            result.Add(anulada);
            result.Add(pendiente);
            result.Add(aprobada);
          
            return result;
        }
        public string GetStatus(string status)
        {
            string result = "";
            var tipoNominaObj = GetListStatus().Where(x => x.Codigo == status).First();
            result = tipoNominaObj.Descripcion;
          
            return result;
        }
        
      
        
        public async Task<bool> SolicitudPuedeModificarseoEliminarse(int codigoSolicitudModificacion)
        {
            bool result = false;
            var preSolModificacion = await _repository.GetByCodigo(codigoSolicitudModificacion);
            if (preSolModificacion != null)
            {
                var status = Estatus.GetStatusObj(preSolModificacion.STATUS);
                result = status.Modificable;
            }
            
            return result;
        }
        
        
        public async Task<string> GetStatusProceso(PRE_SOL_MODIFICACION dto)
        {
           
          
            var preModificacionAprobada = await _preModificacionRepository.GetByCodigoSolicitud(dto.CODIGO_SOL_MODIFICACION);
            string result = "";
            if (dto.STATUS == "AN" || ( preModificacionAprobada!=null && preModificacionAprobada.STATUS=="AN"))
            {
                var persona = await _personasRepository.GetCodigoPersona(dto.USUARIO_UPD);
                var nombrePersona = "";
                if (persona != null)
                {
                    nombrePersona = $"{persona.NOMBRE} {persona.APELLIDO}";
                }

                var fechaAnulado = Fecha.GetFechaString(dto.FECHA_UPD);
                if (preModificacionAprobada != null)
                {
                    fechaAnulado = Fecha.GetFechaString(preModificacionAprobada.FECHA_INS);
                }
                result=$"ANULADO MODIFICACION POR: {nombrePersona} { fechaAnulado}";
                return result;
            }

        
            if (preModificacionAprobada != null && preModificacionAprobada.STATUS=="AP")
            {
                result = $"APROBADO MODIFICACION: #{preModificacionAprobada.CODIGO_MODIFICACION} {Fecha.GetFechaString(preModificacionAprobada.FECHA_INS)}";
                return result;
            }
            
            var preModificacion = await _preModificacionRepository.GetByCodigoSolicitud(dto.CODIGO_SOL_MODIFICACION);
            if (preModificacion == null)
            {
                result = "PENDIENTE MODIFICACION";
                return result;
            }
            


            return result;
        }

        public async Task<bool> TipoDeModificacionDebeCuadrar(int tipoModificacionId)
        {
            var result = false;
            var tipoModificacion = await _repositoryPreDescriptiva.GetByCodigo(tipoModificacionId);
            if (tipoModificacion != null)
            {
                string[] accionList = tipoModificacion.EXTRA3.Split(",");
                if (accionList.Length > 1)
                {
                    result = true;
                }
                
            }


            return result;
        }
        
        public string GetDenominacionIcp(List<PRE_INDICE_CAT_PRG> listIcp,int codigoIcp)
        {
            var result = "";
            var icp = listIcp.Where(x => x.CODIGO_ICP == codigoIcp).FirstOrDefault();
            if (icp != null)
            {
                result = icp.DENOMINACION;
            }
            return result;
        }
        public string GetDenominacionDescriptiva(List<PRE_DESCRIPTIVAS> list,int id)
        {
            var result = "";
            var descriptiva = list.Where(x => x.DESCRIPCION_ID == id).FirstOrDefault();
            if (descriptiva != null)
            {
                result = descriptiva.DESCRIPCION;
            }
            return result;
        }
        
        public async Task<PreSolModificacionResponseDto> MapPreSolModificacion(PRE_SOL_MODIFICACION dto,List<PRE_DESCRIPTIVAS> listDescriptiva)
        {
            PreSolModificacionResponseDto itemResult = new PreSolModificacionResponseDto();
            itemResult.CodigoSolModificacion = dto.CODIGO_SOL_MODIFICACION;
            
            try
            { 
                itemResult.TipoModificacionId = dto.TIPO_MODIFICACION_ID;
            itemResult.DescripcionTipoModificacion = "";
            var tipoModificacionId = listDescriptiva.Where(x=>x.DESCRIPCION_ID== dto.TIPO_MODIFICACION_ID).FirstOrDefault();
            if (tipoModificacionId != null)
            {
                itemResult.DescripcionTipoModificacion = tipoModificacionId.DESCRIPCION;
                string[] accionList = tipoModificacionId.EXTRA3.Split(",");
                itemResult.Aportar = false;
                itemResult.Descontar = false;
                itemResult.OrigenPreSaldo = false;
                foreach (var item in accionList)
                {
                    if (item == "APORTAR")
                    {
                        itemResult.Aportar = true;
                    }
                    if (item == "DESCONTAR")
                    {
                        itemResult.Descontar = true;
                    }
                    if (item == "ORIGEN_PRESALDO")
                    {
                        itemResult.OrigenPreSaldo = true;
                    }
                    
                    
                }
            }

            itemResult.TotalDescontar = 0;
            itemResult.TotalAportar = 0;
            var pucSolModificacion =
                await _prePucSolicitudModificacionRepository.GetAllByCodigoSolicitud(itemResult.CodigoSolModificacion);
            decimal totalDescontar = 0;
            decimal totalAportar = 0;
            if (pucSolModificacion != null && pucSolModificacion.Count > 0)
            {
                totalDescontar = pucSolModificacion.Where(x => x.DE_PARA == "D").Sum(x => x.MONTO);
                totalAportar = pucSolModificacion.Where(x => x.DE_PARA == "P").Sum(x => x.MONTO);
                itemResult.TotalDescontar = totalDescontar;
                itemResult.TotalAportar = totalAportar;
            }
            
            itemResult.FechaSolicitud = dto.FECHA_SOLICITUD;
            itemResult.FechaSolicitudString =Fecha.GetFechaString(dto.FECHA_SOLICITUD);
            FechaDto FechaSolicitudObj = Fecha.GetFechaDto(dto.FECHA_SOLICITUD);
            itemResult.FechaSolicitudObj = (FechaDto)FechaSolicitudObj;
            itemResult.Ano = dto.ANO;
            itemResult.NumeroSolModificacion = dto.NUMERO_SOL_MODIFICACION;
            itemResult.CodigoOficio = dto.CODIGO_OFICIO;
            itemResult.CodigoSolicitante = dto.CODIGO_SOLICITANTE;
            itemResult.Motivo = dto.MOTIVO;
            itemResult.Status = dto.STATUS;
            itemResult.DescripcionEstatus =GetStatus(dto.STATUS);
            itemResult.NumeroCorrelativo = dto.NUMERO_CORRELATIVO;
            itemResult.CodigoPresupuesto = dto.CODIGO_PRESUPUESTO;
            itemResult.StatusProceso = await GetStatusProceso(dto);
            return itemResult;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var a = dto.CODIGO_SOL_MODIFICACION;
                throw;
            }
          

        }


        public async Task<List<PreSolModificacionResponseDto>> MapListPreSolModificacionDto(List<PRE_SOL_MODIFICACION> dtos)
        {
            List<PreSolModificacionResponseDto> result = new List<PreSolModificacionResponseDto>();
            var descriptivaTipoSolicitud = await _repositoryPreDescriptiva.GetByTitulo(8);


            foreach (var item in dtos)
            {

                PreSolModificacionResponseDto itemResult = new PreSolModificacionResponseDto();

                itemResult = await MapPreSolModificacion(item,descriptivaTipoSolicitud);

                result.Add(itemResult);
            }
            return result;



        }
        
        
        public async Task<ResultDto<PreSolModificacionResponseDto>> UpdateStatus(int codigoSolModificacion,string status)
        {

            ResultDto<PreSolModificacionResponseDto> result = new ResultDto<PreSolModificacionResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var solModificacion = await _repository.GetByCodigo(codigoSolModificacion);
                if (solModificacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Sol ModificaCion no existe";
                    return result;
                }
                var findStatus=GetStatus(status);
                if (findStatus == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status no existe";
                    return result;
                }
                


                solModificacion.STATUS = status;


                solModificacion.CODIGO_EMPRESA = conectado.Empresa;
                solModificacion.USUARIO_UPD = conectado.Usuario;
                solModificacion.FECHA_UPD = DateTime.Now;
                await _repository.Update(solModificacion);
                var descriptivaTipoSolicitud = await _repositoryPreDescriptiva.GetByTitulo(8);

                var resultDto =await  MapPreSolModificacion(solModificacion,descriptivaTipoSolicitud);
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
        

        public async Task<ResultDto<PreSolModificacionResponseDto>> Update(PreSolModificacionUpdateDto dto)
        {

            ResultDto<PreSolModificacionResponseDto> result = new ResultDto<PreSolModificacionResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoSolModificacion = await _repository.GetByCodigo(dto.CodigoSolModificacion);
                if (codigoSolModificacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Sol ModificaCion no existe";
                    return result;
                }

               var  puedeModificarse=await SolicitudPuedeModificarseoEliminarse(dto.CodigoSolModificacion);
               if (!puedeModificarse)
               {
                   result.Data = null;
                   result.IsValid = false;
                   result.Message = "Solicitud no puede se alterada, ya existe en historico de modificacion";
                   return result;
               }
                var tipoModificacionId = await _repositoryPreDescriptiva.GetByIdAndTitulo(8, dto.TipoModificacionId);
                if (tipoModificacionId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Modificaion Id Invalido";
                    return result;
                }
                

                if (dto.CodigoSolicitante <=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitante Invalido";
                    return result;
                }

                var solicitante = await _preIndiceCatPrgRepository.GetByCodigo(dto.CodigoSolicitante);
                if (solicitante == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitante Invalido";
                    return result;
                }

                if (dto.Motivo.Length > 1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;
                }
                
             

                if (dto.CodigoPresupuesto <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                var codigoPresupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);

                if (codigoPresupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }



                codigoSolModificacion.CODIGO_SOL_MODIFICACION = dto.CodigoSolModificacion;
                codigoSolModificacion.TIPO_MODIFICACION_ID = dto.TipoModificacionId;
                codigoSolModificacion.FECHA_SOLICITUD = dto.FechaSolicitud;
                codigoSolModificacion.ANO =codigoPresupuesto.ANO;
                codigoSolModificacion.NUMERO_SOL_MODIFICACION = dto.NumeroSolModificacion;
                codigoSolModificacion.CODIGO_OFICIO = $"FORMA 01-{codigoPresupuesto.ANO.ToString()}";
                codigoSolModificacion.CODIGO_SOLICITANTE =dto.CodigoSolicitante;
                codigoSolModificacion.MOTIVO =dto.Motivo;
                codigoSolModificacion.NUMERO_CORRELATIVO = dto.NumeroCorrelativo;
                codigoSolModificacion.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
              


                codigoSolModificacion.CODIGO_EMPRESA = conectado.Empresa;
                codigoSolModificacion.USUARIO_UPD = conectado.Usuario;
                codigoSolModificacion.FECHA_UPD = DateTime.Now;
                await _repository.Update(codigoSolModificacion);
                var descriptivaTipoSolicitud = await _repositoryPreDescriptiva.GetByTitulo(8);

                var resultDto =await  MapPreSolModificacion(codigoSolModificacion,descriptivaTipoSolicitud);
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

        public async Task<ResultDto<PreSolModificacionResponseDto>> Create(PreSolModificacionUpdateDto dto)
        {

            ResultDto<PreSolModificacionResponseDto> result = new ResultDto<PreSolModificacionResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoSolModificacion = await _repository.GetByCodigo(dto.CodigoSolModificacion);
                if (codigoSolModificacion != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Sol ModificaCion no existe";
                    return result;
                }

                var tipoModificacionId = await _repositoryPreDescriptiva.GetByIdAndTitulo(8, dto.TipoModificacionId);
                if (tipoModificacionId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Modificaion Id Invalido";
                    return result;
                }
                


                if (dto.NumeroSolModificacion.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero compromiso Invalido";
                    return result;
                }
              


                if (dto.CodigoSolicitante <=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitante Invalido";
                    return result;
                }

                var solicitante = await _preIndiceCatPrgRepository.GetByCodigo(dto.CodigoSolicitante);
                if (solicitante == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitante Invalido";
                    return result;
                }
                
                
                if (dto.Motivo.Length > 1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;
                }

           


                if(dto.CodigoPresupuesto <= 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                var codigoPresupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);

                if (codigoPresupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }



                PRE_SOL_MODIFICACION entity = new PRE_SOL_MODIFICACION();
                entity.CODIGO_SOL_MODIFICACION = await _repository.GetNextKey();
                entity.TIPO_MODIFICACION_ID = dto.TipoModificacionId;
                entity.FECHA_SOLICITUD = dto.FechaSolicitud;
                entity.ANO = codigoPresupuesto.ANO;
                entity.NUMERO_SOL_MODIFICACION = dto.NumeroSolModificacion;
                entity.CODIGO_OFICIO = $"FORMA 01-{codigoPresupuesto.ANO.ToString()}";
                entity.CODIGO_SOLICITANTE = dto.CodigoSolicitante;
                entity.MOTIVO = dto.Motivo;
                entity.STATUS = "PE";
                entity.NUMERO_CORRELATIVO = dto.NumeroCorrelativo;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;

                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var descriptivaTipoSolicitud = await _repositoryPreDescriptiva.GetByTitulo(8);

                    var resultDto = await MapPreSolModificacion(created.Data,descriptivaTipoSolicitud);
                    result.Data = resultDto;
                    result.IsValid = true;
                    result.Message = "";


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
        
        public async Task<ResultDto<PreSolModificacionDeleteDto>> Delete(PreSolModificacionDeleteDto dto)
        {

            ResultDto<PreSolModificacionDeleteDto> result = new ResultDto<PreSolModificacionDeleteDto>(null);
            try
            {

                var codigoSolModificacion= await _repository.GetByCodigo(dto.CodigoSolModificacion);
                if (codigoSolModificacion == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Sol Modificacion no existe";
                    return result;
                }
                var  puedeModificarse=await SolicitudPuedeModificarseoEliminarse(dto.CodigoSolModificacion);
                if (!puedeModificarse)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Solicitud no puede se Eliminada, ya existe en historico de modificacion";
                    return result;
                }
                
                var deletedPuc = await _prePucSolicitudModificacionRepository.DeleteByCodigoSolicitud(dto.CodigoSolModificacion);
                if (deletedPuc == false)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Error al borrar detalle de PUC";
                }
                var deleted = await _repository.Delete(dto.CodigoSolModificacion);
                
                //RECALCULAMOS PRE_SALDO
                await _preVSaldosRepository.RecalcularSaldo(codigoSolModificacion.CODIGO_PRESUPUESTO);
                
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
            catch (Exception ex)
            {
                result.Data = dto;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }

        public async Task<ResultDto<bool>> UpdateMontoModificado(int codigoPucSolModificacion,decimal montoModificado)
        {

            ResultDto<bool> result = new ResultDto<bool>(false);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoPucModificacion = await _prePucSolicitudModificacionRepository.GetByCodigo(codigoPucSolModificacion);
                if (codigoPucModificacion == null)
                {
                    result.Data = false;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Modificacion no existe";
                    return result;
                }
                
             
          
                codigoPucModificacion.MONTO_MODIFICADO = montoModificado;


                codigoPucModificacion.CODIGO_EMPRESA = conectado.Empresa;
                codigoPucModificacion.USUARIO_UPD = conectado.Usuario;
                codigoPucModificacion.FECHA_UPD = DateTime.Now;
                await _prePucSolicitudModificacionRepository.Update(codigoPucModificacion);

                
                result.Data = true;
                result.IsValid = true;
                result.Message = "";

            }
            catch (Exception ex)
            {
                result.Data = false;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }

        private async Task<bool> RollbackSolicitudModificacion(int codigoSolicitudModificacion,int codigoPresupuesto)
        {
            var result = false;
            var prePucSolicitud =
                await _prePucSolicitudModificacionRepository.GetAllByCodigoSolicitud(codigoSolicitudModificacion);
            foreach (var item in prePucSolicitud)
            {
                await UpdateMontoModificado(item.CODIGO_PUC_SOL_MODIFICACION, 0);
            }
        
            
            
            
            var modificacion = await _preModificacionRepository.GetByCodigoSolicitud(codigoSolicitudModificacion);
            if (modificacion != null)
            {
                var pucModificacion =
                    await _prePucModificacionRepository.GetByCodigoModificacion(modificacion.CODIGO_MODIFICACION);
                if (pucModificacion != null && pucModificacion.Count > 0)
                {
                   var deleted= await _prePucModificacionRepository.DeleteRange(modificacion.CODIGO_MODIFICACION);
                  
                }
                await _preModificacionRepository.Delete(modificacion.CODIGO_MODIFICACION);
                result = true;
            }
            else
            {
                result = true;
            }
            
            //RECALCULAMOS PRE_SALDO
            await _preVSaldosRepository.RecalcularSaldo(codigoPresupuesto);

            return result;
        }

        public async Task<bool> PodemosAnularSolicitudDeModificacion(int codigoSolicitud)
        {
            var result = true;
            var pucModificacion =
                await _prePucSolicitudModificacionRepository.GetAllByCodigoSolicitud(codigoSolicitud);
         
            if (pucModificacion != null && pucModificacion.Count > 0)
            {
                foreach (var item in pucModificacion)
                {
                    
                    /*
                     * AND a.MONTO_ANULADO = 0
                       and a.MONTO_MODIFICADO <> 0
                       AND b.COMPROMETIDO <> 0
                     */
                    
                    
                    var preSaldo = await _preSaldosRepository.GetByCodigo(item.CODIGO_SALDO);
                    if (preSaldo != null)
                    {
                        if (preSaldo.COMPROMETIDO != 0 && item.MONTO_MODIFICADO != 0 && item.MONTO_ANULADO==0)
                        {
                            return false;
                        }
                    }
                 
                    
               
                }
            }
            else
            {
                result = true;
               
            }

            return result;
        }
        
        public async Task<ResultDto<PreSolModificacionResponseDto>> Anular(PreSolModificacionDeleteDto dto)
        {
            ResultDto<PreSolModificacionResponseDto> result = new ResultDto<PreSolModificacionResponseDto>(null);
            try
            {
                var solModificacion = await _repository.GetByCodigo(dto.CodigoSolModificacion);
                if (solModificacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Sol ModificaCion no existe";
                    return result;
                }

                if (solModificacion.STATUS != "AP")
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "La solicitud debe estar APROBADA para poder ANULAR";
                    return result;
                }

                var modificacion = await _preModificacionService.GetByCodigoSolicitud(dto.CodigoSolModificacion);
                if (modificacion==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No existe Modificacion Presupuestaria para  ANULAR";
                    return result;
                }
                
                //TODO: comentado momentaneamente 
                
                /*var puedeAnular = await PodemosAnularSolicitudDeModificacion(dto.CodigoSolModificacion);
                if (puedeAnular == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Solicitud no puede ser ANULADA (Tiene un saldo comprometido o Solicitud con Monto Modificado)";
                    return result;
                }*/


                //Recorremos la tabla PRE_PUC_MODIFICACION

                var pucModificacion =
                    await _prePucModificacionService.GetAllByCodigoModificacion(modificacion.CodigoModificacion);
               
                
                foreach (var item in pucModificacion.Data)
                {
                    //Actualizamoos PRE_SALDO
                    var preSaldo = await _preSaldosRepository.GetByCodigo(item.CodigoSaldo);
                    if (preSaldo != null)
                    {
                        if (item.DePara == "D")
                        {
                            preSaldo.BLOQUEADO = preSaldo.BLOQUEADO + item.Monto;
                            preSaldo.MODIFICADO = preSaldo.MODIFICADO + item.Monto;
                        }
                        else
                        {
                            preSaldo.MODIFICADO = preSaldo.MODIFICADO - item.Monto;
                        }

                        await _preSaldosRepository.Update(preSaldo);
                    }
                    
                    //ACTUALIZAMOS EL MONTO MODIFICADO EN PRE PUC SOLICITUD MODIFICACION(PRE_PUC_SOL_MODIFICACION)

                    var prePucSolModificacion =
                        await _prePucSolicitudModificacionRepository.GetByCodigo(item.CodigoPucSolModificacion);
                    if (prePucSolModificacion != null)
                    {
                        await UpdateMontoModificado(item.CodigoPucSolModificacion, 0);
                    }
                    //ACTUALIZAMOS EL MONTO ANULADO EN PRE PUC SMODIFICACION(PRE_PUC_MODIFICACION)
                    await _prePucModificacionService.UpdateMontoAnulado(item.CodigoPucModificacion,item.Monto);

                }
                
                //PRE_MODIFICACION SE LES ASIGNA EL SIGUIENTE VALOR:PRE_MODIFICACION.STATUS := "AN"
                
                await _preModificacionRepository.UpdateStatus(modificacion.CodigoModificacion,"AN");
                
                //SE CAMBIA EL ESTATUS DE PRE_SOL_MODIFICACION.STATUS A PENDIENTE “PE” =>> PRE_SOL_MODIFICACION.STATUS="PE"
                var modified = await UpdateStatus(dto.CodigoSolModificacion, "PE");
                //RECALCULAMOS PRE_SALDO
                await _preVSaldosRepository.RecalcularSaldo(solModificacion.CODIGO_PRESUPUESTO);

                var resultDto = modified.Data;
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
        public async Task<ResultDto<PreSolModificacionResponseDto>> Aprobar(PreSolModificacionDeleteDto dto)
        {

           
            
            ResultDto<PreSolModificacionResponseDto> result = new ResultDto<PreSolModificacionResponseDto>(null);
            
            var solModificacion = await _repository.GetByCodigo(dto.CodigoSolModificacion);
            if (solModificacion == null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "Codigo Sol ModificaCion no existe";
                return result;
            }
            try
            {
                

               

                var preModificacion = await _preModificacionRepository.GetByCodigoSolicitud(dto.CodigoSolModificacion);
                if (preModificacion != null)
                {
                    if (preModificacion.STATUS != "AN")
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Solicitud no puede se alterada, ya existe en historico de modificacion como APROBADA";
                        return result;
                    }
                }
             

               PrePucSolModificacionFilterDto filter = new PrePucSolModificacionFilterDto();
               filter.CodigoSolModificacion = dto.CodigoSolModificacion;
               filter.DePara = "";
               
               var prePucSolicitud =
                   await _prePucSolicitudModificacionRepository.GetAllByCodigoSolicitud(dto.CodigoSolModificacion);

               if (prePucSolicitud != null && prePucSolicitud.Count == 0)
               {
                   result.Data = null;
                   result.IsValid = false;
                   result.Message = "No puede Aprobar solicitud sin detalle de PUC";
                   return result;
               }

               
               var tipoDeModificacionDebeCuadrar =
                   await TipoDeModificacionDebeCuadrar(solModificacion.TIPO_MODIFICACION_ID);
               if (tipoDeModificacionDebeCuadrar)
               {
                   decimal totalAportar = prePucSolicitud.Where(x=>x.DE_PARA=="P").Sum(val => val.MONTO);
                   decimal totalDescontar = prePucSolicitud.Where(x=>x.DE_PARA=="D").Sum(val => val.MONTO);
                   if (totalAportar!=totalDescontar)
                   {
                       result.Data = null;
                       result.IsValid = false;
                       result.Message = $"No puede Aprobar solicitud con dif en Aportar y Descontar: {totalAportar} dif {totalDescontar}";
                       return result;
                   }
               }
              
               //TRANSFERIMOS PRE_SOL_MODIFICACION A PRE_MODIFICACION
               PreModificacionUpdateDto preModificacionCreateDto = new PreModificacionUpdateDto();
               preModificacionCreateDto.CodigoModificacion = 0;
               preModificacionCreateDto.CodigoSolModificacion = solModificacion.CODIGO_SOL_MODIFICACION;
               preModificacionCreateDto.FechaModificacion = solModificacion.FECHA_SOLICITUD;
               preModificacionCreateDto.Ano = solModificacion.ANO;
               preModificacionCreateDto.NumeroModificacion = "";
               preModificacionCreateDto.NoResAct = "";
               preModificacionCreateDto.CodigoOficio = solModificacion.CODIGO_OFICIO;
               preModificacionCreateDto.CodigoSolicitante = solModificacion.CODIGO_SOLICITANTE;
               preModificacionCreateDto.Motivo = solModificacion.MOTIVO;
               preModificacionCreateDto.Status = "AP";
               preModificacionCreateDto.Extra1 = "";
               preModificacionCreateDto.Extra2 = "";
               preModificacionCreateDto.Extra3 = "";
               preModificacionCreateDto.CodigoPresupuesto = solModificacion.CODIGO_PRESUPUESTO;
               preModificacionCreateDto.TipoModificacionId = solModificacion.TIPO_MODIFICACION_ID;
               var preModificacionCreated = await _preModificacionService.Create(preModificacionCreateDto);
               if (preModificacionCreated.IsValid == false)
               {
                   result.Data = null;
                   result.IsValid = false;
                   result.Message = preModificacionCreated.Message;
                   return result;
               }
               
               foreach (var item in prePucSolicitud.Where(x=>x.MONTO_ANULADO==0).ToList())
               {

                   //ACTUALIZAMOS EL MONTO MODIFICADO EN PRE PUC SOLICITUD MODIFICACION(PRE_PUC_SOL_MODIFICACION)
           
                   var montoModificado = await UpdateMontoModificado(item.CODIGO_PUC_SOL_MODIFICACION,
                       item.MONTO);
                   if (montoModificado.IsValid == false)
                   {
                       await RollbackSolicitudModificacion(dto.CodigoSolModificacion,solModificacion.CODIGO_PRESUPUESTO);
                       result.Data = null;
                       result.IsValid = false;
                       result.Message = $"{montoModificado.Message} ";
                       return result;
                   }
                    
                   //TRANSFERIMOS PRE_PUC_SOL_MODIFICACION A PRE_PUC_MODIFICACION
                   PrePucModificacionUpdateDto prePucModificacionUpdateDto = new PrePucModificacionUpdateDto();
                   prePucModificacionUpdateDto.CodigoPucModificacion = 0;
                   prePucModificacionUpdateDto.CodigoModificacion = preModificacionCreated.Data.CodigoModificacion;
                   prePucModificacionUpdateDto.CodigoSaldo = item.CODIGO_SALDO;
                   prePucModificacionUpdateDto.FinanciadoId = item.FINANCIADO_ID;
                   prePucModificacionUpdateDto.CodigoIcp = item.CODIGO_ICP;
                   prePucModificacionUpdateDto.CodigoPuc = item.CODIGO_PUC;
                   prePucModificacionUpdateDto.Monto = item.MONTO;
                   prePucModificacionUpdateDto.MontoModificado = item.MONTO_MODIFICADO;
                   prePucModificacionUpdateDto.MontoAnulado = 0;
                   prePucModificacionUpdateDto.DePara = item.DE_PARA;
                   prePucModificacionUpdateDto.CodigoPucSolModificacion = item.CODIGO_PUC_SOL_MODIFICACION;
                   prePucModificacionUpdateDto.CodigoPresupuesto = solModificacion.CODIGO_PRESUPUESTO;
                    if(item.CODIGO_FINANCIADO == null) 
                    {
                        item.CODIGO_FINANCIADO = 0;
                    }
                   prePucModificacionUpdateDto.CodigoFinanciado = (int)item.CODIGO_FINANCIADO;
                   
                   var prePucModificacionCreated= await _prePucModificacionService.Create(prePucModificacionUpdateDto);
                   if (prePucModificacionCreated.IsValid == false)
                   {
                       await RollbackSolicitudModificacion(dto.CodigoSolModificacion,solModificacion.CODIGO_PRESUPUESTO);
                       result.Data = null;
                       result.IsValid = false;
                       result.Message = $"{prePucModificacionCreated.Message} ";
                       return result;
                   }

                   
                   //ACTUALIZAMOS PRE SALDOS LOS CAMPOS DE BLOQUEADO Y MODIFICADO
                   var preSaldo = await _preSaldosRepository.GetByCodigo(item.CODIGO_SALDO);
                   if (preSaldo != null)
                   {
                       if (item.DE_PARA == "D")
                       {
                           preSaldo.BLOQUEADO = preSaldo.BLOQUEADO - item.MONTO;
                           preSaldo.MODIFICADO = preSaldo.MODIFICADO - item.MONTO;
                       }
                       else
                       {
                           preSaldo.MODIFICADO = preSaldo.MODIFICADO + item.MONTO;
                       }

                       await _preSaldosRepository.Update(preSaldo);
                   }
                   
                   
               }
             
               
               //COLOCAMOS EL ESTATUS DE LA SOLICITUD EN APROBADO "AP"..
               solModificacion.STATUS = "AP";
               var modified = await UpdateStatus(dto.CodigoSolModificacion, "AP");
               
               //RECALCULAMOS PRE_SALDO
               await _preVSaldosRepository.RecalcularSaldo(solModificacion.CODIGO_PRESUPUESTO);

               var resultDto = modified.Data;
                result.Data = resultDto;
                result.IsValid = true;
                result.Message = "";

            }
            catch (Exception ex)
            {
                await RollbackSolicitudModificacion(dto.CodigoSolModificacion,solModificacion.CODIGO_PRESUPUESTO);
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }

        public async Task<ResultDto<PreSolModificacionResponseDto>> GetByCodigoSolicitud(int codigoSolicitud)
        {
            ResultDto<PreSolModificacionResponseDto> result = new ResultDto<PreSolModificacionResponseDto>(null);
            try
            {

                var solModificacion = await _repository.GetByCodigo(codigoSolicitud);

                var descriptivas = await _repositoryPreDescriptiva.GetByTitulo(8);

                if (solModificacion != null)
                {
                    
                    var dto = await MapPreSolModificacion(solModificacion,descriptivas);

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

    }
}

