using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using Microsoft.JSInterop;

namespace Convertidor.Services.Presupuesto;

public class PrePucSolicitudModificacionService: IPrePucSolicitudModificacionService
{
      
        private readonly IPrePucSolicitudModificacionRepository _repository;
        private readonly IPRE_PRESUPUESTOSRepository _presupuestosService;
        private readonly IIndiceCategoriaProgramaService _indiceCategoriaProgramaService;
        private readonly IPrePlanUnicoCuentasService _prePlanUnicoCuentasService;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IPRE_V_SALDOSRepository _preVSaldosRepository;
        private readonly IPreAsignacionesDetalleRepository _preAsignacionesDetalleRepository;
        private readonly IPreDescriptivasService _preDescriptivasService;
        private readonly IPRE_INDICE_CAT_PRGRepository _preIndiceCatPrgRepository;
        private readonly IPreSolModificacionRepository _preSolicitudModificacionRepository;
        private readonly IPRE_SALDOSRepository _preSaldosRepository;
        private readonly IPreSolModificacionService _preSolModificacionService;

        public PrePucSolicitudModificacionService(    IPrePucSolicitudModificacionRepository repository,
                                        IPRE_PRESUPUESTOSRepository presupuestosService,
                                        IIndiceCategoriaProgramaService indiceCategoriaProgramaService,
                                        IPrePlanUnicoCuentasService prePlanUnicoCuentasService,
                                        ISisUsuarioRepository sisUsuarioRepository,
                                        IPRE_V_SALDOSRepository preVSaldosRepository,
                                        IPreAsignacionesDetalleRepository preAsignacionesDetalleRepository,
                                        IPreDescriptivasService preDescriptivasService,
                                        IPRE_INDICE_CAT_PRGRepository preIndiceCatPrgRepository,
                                        IPreSolModificacionRepository preSolicitudModificacionRepository,
                                        IPRE_SALDOSRepository preSaldosRepository,
                                       IPreSolModificacionService preSolModificacionService
                                        
                                        )
        {
            _repository = repository;
            _presupuestosService = presupuestosService;
            _indiceCategoriaProgramaService = indiceCategoriaProgramaService;
            _prePlanUnicoCuentasService = prePlanUnicoCuentasService;
            _sisUsuarioRepository = sisUsuarioRepository;
            _preVSaldosRepository = preVSaldosRepository;
            _preAsignacionesDetalleRepository = preAsignacionesDetalleRepository;
            _preDescriptivasService = preDescriptivasService;
            _preIndiceCatPrgRepository = preIndiceCatPrgRepository;
            _preSolicitudModificacionRepository = preSolicitudModificacionRepository;
            _preSaldosRepository = preSaldosRepository;
            _preSolModificacionService = preSolModificacionService;
        }


        public async Task<bool> PresupuestoExiste(int codPresupuesto)
        {
            return await _repository.PresupuestoExiste(codPresupuesto);
        }

        public async Task<bool> ICPExiste(int codigoICP)
        {
            return await _repository.ICPExiste(codigoICP);
        }

        public async Task<bool> PUCExiste(int codigoPUC)
        {
            return await _repository.PUCExiste(codigoPUC);
        }


        public async Task<ResultDto<List<PrePucSolModificacionResponseDto>>>  GetAllByCodigoSolicitud(PrePucSolModificacionFilterDto filter)
        {

            ResultDto<List<PrePucSolModificacionResponseDto>> result = new ResultDto<List<PrePucSolModificacionResponseDto>>(null);
            try
            {

                var pucSolicitud = await _repository.GetAllByCodigoSolicitud(filter.CodigoSolModificacion);
                pucSolicitud = pucSolicitud.OrderBy(x => x.DE_PARA).ToList();


                if (pucSolicitud.Count() > 0)
                {
                    List<PrePucSolModificacionResponseDto> listDto = new List<PrePucSolModificacionResponseDto>();

                    foreach (var item in pucSolicitud)
                    {
                        var dto = await MapPrePucSoliModificacion(item);
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
        
        public async Task<PrePucSolModificacionResponseDto> MapPrePucSoliModificacion(PRE_PUC_SOL_MODIFICACION dto)
        {
            PrePucSolModificacionResponseDto itemResult = new PrePucSolModificacionResponseDto();
            itemResult.CodigoPucSolModificacion = dto.CODIGO_PUC_SOL_MODIFICACION;
            itemResult.CodigoSolModificacion = dto.CODIGO_SOL_MODIFICACION;
            itemResult.CodigoSaldo = dto.CODIGO_SALDO;
            itemResult.FinanciadoId = dto.FINANCIADO_ID;
            itemResult.DescripcionFinanciado = "";
            int intFinanciadoId = Int32.Parse(itemResult.FinanciadoId);
            var financiadoIdObj = await _preDescriptivasService.GetByCodigo(intFinanciadoId);
            if (financiadoIdObj.Data != null)
            {
                itemResult.DescripcionFinanciado =financiadoIdObj.Data.Descripcion;
            }
            
            itemResult.CodigoFinanciado = dto.CODIGO_FINANCIADO;
            itemResult.CodigoIcp = dto.CODIGO_ICP;
       
            itemResult.DenominacionIcp = "";
            var icp = await _indiceCategoriaProgramaService.GetByCodigo(dto.CODIGO_ICP);
            if (icp != null)
            {
                itemResult.DenominacionIcp = icp.Denominacion;
                itemResult.CodigoIcpConcat = icp.CodigoIcpConcat;
            }
            itemResult.CodigoPuc = dto.CODIGO_PUC;
            itemResult.DenominacionPuc = "";
            var puc = await _prePlanUnicoCuentasService.GetById(dto.CODIGO_PUC);
            if (puc.Data != null)
            {
                itemResult.DenominacionPuc = puc.Data.Denominacion;
                itemResult.CodigoPucConcat = puc.Data.CodigoPucConcat;
            }
            itemResult.Monto = dto.MONTO;
            itemResult.DePara = dto.DE_PARA;
            if (dto.DE_PARA == "D")
            {
                itemResult.Descontar = dto.MONTO;
                itemResult.Aportar =0;
            }
            if (dto.DE_PARA == "P")
            {
                itemResult.Descontar = 0;
                itemResult.Aportar =dto.MONTO;
            }
            itemResult.MontoAnulado = dto.MONTO_ANULADO;
            itemResult.Monto = dto.MONTO;
            itemResult.MontoModificado = dto.MONTO_MODIFICADO;
            itemResult.Status = "";
            var solicitud = await _preSolicitudModificacionRepository.GetByCodigo(dto.CODIGO_SOL_MODIFICACION);
            if (solicitud != null)
            {
                itemResult.Status = solicitud.STATUS;
            }
        

            return itemResult;

        }
        
     public async Task<ResultDto<PrePucSolModificacionResponseDto>> Create(PrePucSolModificacionUpdateDto dto)
        {

            ResultDto<PrePucSolModificacionResponseDto> result = new ResultDto<PrePucSolModificacionResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

               
                if (dto.CodigoSolModificacion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Sol Modificacion Invalido";
                    return result;
                }
                var codigoModificacion = await _preSolicitudModificacionRepository.GetByCodigo(dto.CodigoSolModificacion);
                if (codigoModificacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Sol Modificacion Invalido";
                    return result;
                }
                
                var puedeMdificar =
                    await _preSolModificacionService.SolicitudPuedeModificarseoEliminarse(dto.CodigoSolModificacion);
                if (puedeMdificar == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No puede Modificar De una Solicitud de Modificacion Aprobada";
                    return result;
                }

                if (dto.DePara=="D" && dto.CodigoSaldo <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Saldo Invalido,Seleccione de la lista de Saldos disponibles";
                    return result;

                }

                if (dto.CodigoSaldo > 0)
                {
                    var codigoSaldo = await _preSaldosRepository.GetByCodigo(dto.CodigoSaldo);
                    if (codigoSaldo ==null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Codigo saldo Invalido";
                        return result;
                    }
                    else
                    {
                        dto.FinanciadoId = codigoSaldo.FINANCIADO_ID;
                        dto.CodigoIcp = codigoSaldo.CODIGO_ICP;
                        dto.CodigoPuc = codigoSaldo.CODIGO_PUC;
                        dto.CodigoFinanciado = codigoSaldo.CODIGO_FINANCIADO;

                    }
                }
               
                if (dto.FinanciadoId <=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Financiado Id";
                    return result;
                }

                var financiado = await _preDescriptivasService.GetByCodigo(dto.FinanciadoId);
                if (financiado== null )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Financiado Invalido";
                    return result;
                }

                if (dto.CodigoPresupuesto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;

                }

                var presupuesto = await _presupuestosService.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (presupuesto==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }
                
                if (dto.CodigoIcp <=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo icp Invalido";
                    return result;

                }
                var codigoIcp = await _preIndiceCatPrgRepository.GetByCodigo(dto.CodigoIcp);
                if (codigoIcp==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;
                }
                if (dto.CodigoPresupuesto !=codigoIcp.CODIGO_PRESUPUESTO)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido,no corresponde al presupuesto seleccionado";
                    return result;

                }
                

                if (dto.CodigoPuc <=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Invalido";
                    return result;

                }
                var codigoPuc = await _prePlanUnicoCuentasService.GetById(dto.CodigoPuc);
                if (codigoPuc.Data==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Invalido";
                    return result;
                }
                
                if (dto.CodigoPresupuesto !=codigoPuc.Data.CodigoPresupuesto)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Invalido,no corresponde al presupuesto seleccionado";
                    return result;

                }

                if (dto.Monto <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Invalido";
                    return result;

                }

            

                var presaldo = await _preSaldosRepository
                    .GetAllByIcpPucFinanciado(dto.CodigoPresupuesto, dto.CodigoIcp, dto.CodigoPuc, dto.FinanciadoId);
                if (presaldo == null)
                {
                    PRE_SALDOS entitySaldo = new PRE_SALDOS();
                    entitySaldo.CODIGO_SALDO = await _preSaldosRepository.GetNextKey();
                    entitySaldo.ANO = presupuesto.ANO;
                    entitySaldo.CODIGO_ICP = dto.CodigoIcp;
                    entitySaldo.CODIGO_PUC = dto.CodigoPuc;
                    entitySaldo.FINANCIADO_ID =dto.FinanciadoId;
                    entitySaldo.CODIGO_FINANCIADO = dto.CodigoSolModificacion;
                    entitySaldo.CODIGO_PRESUPUESTO =dto.CodigoPresupuesto;
                    entitySaldo.ASIGNACION = 0;
                    entitySaldo.BLOQUEADO = dto.Monto;
                    entitySaldo.MODIFICADO =  dto.Monto;
                    entitySaldo.COMPROMETIDO = 0;
                    entitySaldo.CAUSADO = 0;
                    entitySaldo.PAGADO = 0;
                    entitySaldo.AJUSTADO = 0;
                    entitySaldo.PRESUPUESTADO =0;
                    entitySaldo.USUARIO_INS = conectado.Usuario;
                    entitySaldo.FECHA_INS = DateTime.Now;
                    entitySaldo.CODIGO_EMPRESA = conectado.Empresa;
                    var preSaldoCreated = await _preSaldosRepository.Add(entitySaldo);
                    if (preSaldoCreated.IsValid == true)
                    {
                        dto.CodigoSaldo = preSaldoCreated.Data.CODIGO_SALDO;
                        dto.CodigoFinanciado = dto.CodigoSolModificacion;
                    }
                }
                else
                {
                    dto.CodigoSaldo = presaldo.CODIGO_SALDO;
                    dto.CodigoFinanciado = presaldo.CODIGO_FINANCIADO;
                }

                if (dto.CodigoFinanciado == 0)
                {
                    dto.CodigoFinanciado = dto.CodigoSolModificacion;
                }

                var pucSolModificacion = await _repository.GetByCodigoSolModificacionCodigoSaldo(dto.CodigoSolModificacion,dto.CodigoSaldo);
                if (pucSolModificacion != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Ya existe registro para: {codigoIcp.DESCRIPCION} {codigoPuc.Data.Denominacion} {financiado.Data.Descripcion}";
                    return result;
                }
                
                PRE_PUC_SOL_MODIFICACION entity = new PRE_PUC_SOL_MODIFICACION();
                entity.CODIGO_PUC_SOL_MODIFICACION = await _repository.GetNextKey();
                entity.CODIGO_SOL_MODIFICACION = dto.CodigoSolModificacion;
                entity.CODIGO_SALDO = dto.CodigoSaldo;
              
                entity.CODIGO_FINANCIADO =  (int)dto.CodigoFinanciado;
              
               
               
                entity.FINANCIADO_ID = dto.FinanciadoId.ToString();
                entity.CODIGO_ICP = dto.CodigoIcp;
                entity.CODIGO_PUC = dto.CodigoPuc;
                entity.MONTO = dto.Monto;
                entity.MONTO_MODIFICADO = 0;
                entity.MONTO_ANULADO = 0;
                entity.DE_PARA = dto.DePara;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;
                
                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapPrePucSoliModificacion(created.Data);
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
    
     public async Task<ResultDto<PrePucSolModificacionResponseDto>> UpdateMontoModificado(int codigoPucSolModificacion,decimal montoModificado)
        {

            ResultDto<PrePucSolModificacionResponseDto> result = new ResultDto<PrePucSolModificacionResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoPucModificacion = await _repository.GetByCodigo(codigoPucSolModificacion);
                if (codigoPucModificacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Modificacion no existe";
                    return result;
                }
                
             
          
                codigoPucModificacion.MONTO_MODIFICADO = montoModificado;


                codigoPucModificacion.CODIGO_EMPRESA = conectado.Empresa;
                codigoPucModificacion.USUARIO_UPD = conectado.Usuario;
                codigoPucModificacion.FECHA_UPD = DateTime.Now;
                await _repository.Update(codigoPucModificacion);

                var resultDto = await MapPrePucSoliModificacion(codigoPucModificacion);
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
     
    
        public async Task<ResultDto<PrePucSolModificacionDeleteDto>> Delete(PrePucSolModificacionDeleteDto dto)
        {

            ResultDto<PrePucSolModificacionDeleteDto> result = new ResultDto<PrePucSolModificacionDeleteDto>(null);
            try
            {

                var codigoPucModificacion = await _repository.GetByCodigo(dto.CodigoPucSolModificacion);
                if (codigoPucModificacion == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Modificacion no existe";
                    return result;
                }

                var puedeEliminar =
                    await _preSolModificacionService.SolicitudPuedeModificarseoEliminarse(codigoPucModificacion.CODIGO_SOL_MODIFICACION);
                if (puedeEliminar == false)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "No puede eliminar De una Solicitud de Modificacion Aprobada";
                    return result;
                }
                var deleted = await _repository.Delete(dto.CodigoPucSolModificacion);

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
        
}