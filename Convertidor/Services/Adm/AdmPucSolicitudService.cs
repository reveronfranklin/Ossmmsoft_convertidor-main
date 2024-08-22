using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Cnt;
using Convertidor.Utility;

namespace Convertidor.Services.Adm
{
    public class AdmPucSolicitudService : IAdmPucSolicitudService
    {
        private readonly IAdmPucSolicitudRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
        private readonly IPrePlanUnicoCuentasService _prePlanUnicoCuentasService;
        private readonly IIndiceCategoriaProgramaService _indiceCategoriaProgramaService;
        private readonly IPreDescriptivasService _preDescriptivasService;
        private readonly IAdmDetalleSolicitudRepository _admDetalleSolicitudRepository;
        private readonly IAdmSolicitudesRepository _admSolicitudesRepository;
        private readonly IPRE_V_SALDOSRepository _preVSaldosRepository;

        public AdmPucSolicitudService(IAdmPucSolicitudRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository,
                                     IPrePlanUnicoCuentasService prePlanUnicoCuentasService,
                                     IIndiceCategoriaProgramaService indiceCategoriaProgramaService,
                                     IPreDescriptivasService preDescriptivasService,
                                     IAdmDetalleSolicitudRepository admDetalleSolicitudRepository,
                                     IAdmSolicitudesRepository admSolicitudesRepository,
                                     IPRE_V_SALDOSRepository preVSaldosRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
            _prePlanUnicoCuentasService = prePlanUnicoCuentasService;
            _indiceCategoriaProgramaService = indiceCategoriaProgramaService;
            _preDescriptivasService = preDescriptivasService;
            _admDetalleSolicitudRepository = admDetalleSolicitudRepository;
            _admSolicitudesRepository = admSolicitudesRepository;
            _preVSaldosRepository = preVSaldosRepository;
        }

        public async Task<AdmPucSolicitudResponseDto> MapPucSolicitudDto(ADM_PUC_SOLICITUD dtos)
        {
            
            
            AdmPucSolicitudResponseDto itemResult = new AdmPucSolicitudResponseDto();
            itemResult.CodigoPucSolicitud = dtos.CODIGO_PUC_SOLICITUD;
            itemResult.CodigoDetalleSolicitud = dtos.CODIGO_DETALLE_SOLICITUD;
            itemResult.CodigoSolicitud = dtos.CODIGO_SOLICITUD;
            itemResult.CodigoSaldo = dtos.CODIGO_SALDO;
            itemResult.CodigoIcp = dtos.CODIGO_ICP;
            itemResult.IcpConcat = "";
            var icp = await _indiceCategoriaProgramaService.GetByCodigo(dtos.CODIGO_ICP);
            if (icp != null)
            {
                itemResult.IcpConcat = icp.CodigoIcpConcat;
            }
            itemResult.CodigoPuc = dtos.CODIGO_PUC;
            itemResult.PucConcat = "";
            var puc = await _prePlanUnicoCuentasService.GetById(dtos.CODIGO_PUC);
            if (puc.Data != null)
            {
                itemResult.PucConcat = puc.Data.CodigoPucConcat;
            }
            itemResult.FinanciadoId = dtos.FINANCIADO_ID;
            itemResult.DescripcionFinanciado = "";
            var descriptiva = await _preDescriptivasService.GetByCodigo(dtos.FINANCIADO_ID);
            if (descriptiva.Data != null)
            {
                itemResult.DescripcionFinanciado = descriptiva.Data.Descripcion;
            }

            if (dtos.CODIGO_FINANCIADO == null) dtos.CODIGO_FINANCIADO = 0;
            itemResult.CodigoFinanciado = (int)dtos.CODIGO_FINANCIADO;
            itemResult.Monto = dtos.MONTO;
            itemResult.MontoComprometido = dtos.MONTO_COMPROMETIDO;
            itemResult.MontoAnulado = dtos.MONTO_ANULADO;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            return itemResult;
        }

        public async Task<List<AdmPucSolicitudResponseDto>> MapListPucSolicitudDto(List<ADM_PUC_SOLICITUD> dtos)
        {
            List<AdmPucSolicitudResponseDto> result = new List<AdmPucSolicitudResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapPucSolicitudDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public async Task<ResultDto<List<AdmPucSolicitudResponseDto>>> GetByDetalleSolicitud(AdmPucSolicitudFilterDto filter)
        {

            ResultDto<List<AdmPucSolicitudResponseDto>> result = new ResultDto<List<AdmPucSolicitudResponseDto>>(null);
            try
            {
                var pucSolicitud = await _repository.GetByDetalleSolicitud(filter.CodigoDetalleSolicitud);
                
                if ( pucSolicitud !=null && pucSolicitud.Count() > 0)
                {
                    var listDto = await MapListPucSolicitudDto(pucSolicitud);
                    var total = await GetTotalMonto(filter.CodigoDetalleSolicitud);
                    result.Total1 = total;
                    result.Total2 = total;
                    result.Data = listDto;
                    result.IsValid = true;
                    result.Message = "";


                    return result;
                }
                else
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No data";

                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }

        }

        
        public async Task<ResultDto<List<AdmPucSolicitudResponseDto>>> GetAll()
        {

            ResultDto<List<AdmPucSolicitudResponseDto>> result = new ResultDto<List<AdmPucSolicitudResponseDto>>(null);
            try
            {
                var pucSolicitud = await _repository.GetAll();
                var cant = pucSolicitud.Count();
                if (pucSolicitud != null && pucSolicitud.Count() > 0)
                {
                    var listDto = await MapListPucSolicitudDto(pucSolicitud);

                    result.Data = listDto;
                    result.IsValid = true;
                    result.Message = "";


                    return result;
                }
                else
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No data";

                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }

        }
       
        public async Task<ResultDto<bool>> PresupuestoExiste(int codigoPresupuesto)
        {

            ResultDto<bool> result = new ResultDto<bool>(false);
            try
            {
                var existe = await _repository.ExistePresupuesto(codigoPresupuesto);
   
                    result.Data = existe;
                    result.IsValid = true;
                    result.Message = "";


                    return result;
              
            }
            catch (Exception ex)
            {
                result.Data = false;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }

        }

        public async Task<bool> SuperaElMontoDetalle(int codigoDetalleSolicitud,decimal nuevoMonto)
        {

            bool result = false;
            try
            {
                var codigoDetallesolicitud = await _admDetalleSolicitudRepository.GetCodigoDetalleSolicitud(codigoDetalleSolicitud);
                var pucSolicitud = await _repository.GetByDetalleSolicitud(codigoDetalleSolicitud);
                if (pucSolicitud != null && pucSolicitud.Count > 0)
                {
                    var total = nuevoMonto;
                    foreach (var item in pucSolicitud)
                    {
                        total = total + item.MONTO;
                    }

                    if (total > codigoDetallesolicitud.TOTAL_MAS_IMPUESTO)
                    {
                        result = true;
                    }
                   
                    
                }
                else
                {
                    result = false;
                }
              


                return result;
              
            }
            catch (Exception ex)
            {
                return result;
            }

        }
        
        public async Task<decimal> GetTotalMonto(int codigoDetalleSolicitud)
        {

            decimal result = 0;
            try
            {
                var codigoDetallesolicitud = await _admDetalleSolicitudRepository.GetCodigoDetalleSolicitud(codigoDetalleSolicitud);
                var pucSolicitud = await _repository.GetByDetalleSolicitud(codigoDetalleSolicitud);
                if (pucSolicitud != null && pucSolicitud.Count > 0)
                {
                  
                    foreach (var item in pucSolicitud)
                    {
                        result = result + item.MONTO;
                    }
                   
                    
                }
                else
                {
                    result = 0;
                }
              


                return result;
              
            }
            catch (Exception ex)
            {
                return result;
            }

        }
        
        public async Task<ResultDto<AdmPucSolicitudResponseDto>> Update(AdmPucSolicitudUpdateDto dto)
        {
            ResultDto<AdmPucSolicitudResponseDto> result = new ResultDto<AdmPucSolicitudResponseDto>(null);
            try
            {
                var codigoPucsolicitud = await _repository.GetCodigoPucSolicitud(dto.CodigoPucSolicitud);
                if (codigoPucsolicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Solicitud no existe";
                    return result;
                }
                var solicitud = await _admSolicitudesRepository.GetByCodigoSolicitud(dto.CodigoSolicitud);
                if (solicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No existe Esta Solicitud";
                    return result;
                }

                var status = Estatus.GetStatusObj(solicitud.STATUS);
                if (status.Modificable == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Solicitud no puede ser modificada, se encuentra en status: {status.Descripcion}";
                    return result;
                }
                
                if (dto.CodigoDetalleSolicitud<0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle solicitud invalido";
                    return result;
                }

                if (dto.CodigoSolicitud <0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitud invalido";
                    return result;

                }

                if (dto.CodigoSaldo <0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Saldo Invalido";
                    return result;
                }
                if (dto.CodigoIcp < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;
                }
                if (dto.CodigoPuc < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Invalido";
                    return result;
                }
                
                var financiadoId = await _admDescriptivaRepository.GetByIdAndTitulo(13,dto.FinanciadoId);

                if (financiadoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "financiado Id no existe";
                    return result;

                }

                if (dto.CodigoFinanciado <0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Invalido";
                    return result;
                }

                if (dto.MontoComprometido <0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto comprometido Invalido";
                    return result;
                }
                if (dto.MontoAnulado<0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Anulado invalido";
                    return result;
                }

          
                if (dto.CodigoPresupuesto < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }
                if (dto.Monto <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto no puede ser cero ni negativo";
                    return result;
                }

                codigoPucsolicitud.CODIGO_PUC_SOLICITUD = dto.CodigoPucSolicitud;
                codigoPucsolicitud.CODIGO_DETALLE_SOLICITUD = dto.CodigoDetalleSolicitud;
                codigoPucsolicitud.CODIGO_SOLICITUD = dto.CodigoSolicitud;
                codigoPucsolicitud.CODIGO_SALDO = dto.CodigoSaldo;
                codigoPucsolicitud.CODIGO_ICP = dto.CodigoIcp;
                codigoPucsolicitud.CODIGO_PUC = dto.CodigoPuc;
                codigoPucsolicitud.FINANCIADO_ID = dto.FinanciadoId;
                codigoPucsolicitud.CODIGO_FINANCIADO = dto.CodigoFinanciado;
                codigoPucsolicitud.MONTO = dto.Monto;
                codigoPucsolicitud.MONTO_COMPROMETIDO = dto.MontoComprometido;
                codigoPucsolicitud.MONTO_ANULADO = dto.MontoAnulado;
                codigoPucsolicitud.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;

                var conectado = await _sisUsuarioRepository.GetConectado();
                codigoPucsolicitud.CODIGO_EMPRESA = conectado.Empresa;
                codigoPucsolicitud.USUARIO_UPD = conectado.Usuario;
                codigoPucsolicitud.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoPucsolicitud);
                //ACTUALIZAR PRE_V_SALDO
                _preVSaldosRepository.RecalculaSaldosPreIcpPucFi(dto.CodigoPresupuesto,dto.CodigoIcp,dto.CodigoPuc,dto.CodigoFinanciado);
                var resultDto = await MapPucSolicitudDto(codigoPucsolicitud);
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

        public async Task<ResultDto<AdmPucSolicitudResponseDto>> Create(AdmPucSolicitudUpdateDto dto)
        {
            ResultDto<AdmPucSolicitudResponseDto> result = new ResultDto<AdmPucSolicitudResponseDto>(null);
            try
            {
                var solicitud = await _admSolicitudesRepository.GetByCodigoSolicitud(dto.CodigoSolicitud);
                if (solicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No existe Esta Solicitud";
                    return result;
                }

                var status = Estatus.GetStatusObj(solicitud.STATUS);
                if (status.Modificable == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Solicitud no puede ser modificada, se encuentra en status: {status.Descripcion}";
                    return result;
                }
                
                var codigoPucsolicitud = await _repository.GetCodigoPucSolicitud(dto.CodigoPucSolicitud);
                if (codigoPucsolicitud != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Solicitud ya existe";
                    return result;
                }
                
                var codigoDetallesolicitud = await _admDetalleSolicitudRepository.GetCodigoDetalleSolicitud(dto.CodigoDetalleSolicitud);
                if (codigoDetallesolicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = TraduccionErrores.AdmDetalleSolicitudNoexiste; 
                    return result;
                }
                
                

                var superaMontoDetalle = await SuperaElMontoDetalle(dto.CodigoDetalleSolicitud, dto.Monto);
                if(superaMontoDetalle==true)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Supera el monto del detalle de la solicitud"; 
                    return result;
                }

                var pucByIcpPuFinanciado = await _repository.GetByIcpPucFInanciado(dto);
                if (pucByIcpPuFinanciado.Data != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ya existe Registro para Este Icp,Puc,Financiado"; 
                    return result;
                }

                
                if (dto.CodigoDetalleSolicitud < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle solicitud invalido";
                    return result;
                }

                if (dto.CodigoSolicitud < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitud invalido";
                    return result;

                }

                if (dto.CodigoSaldo < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Saldo Invalido";
                    return result;
                }
                if (dto.CodigoIcp < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;
                }
                if (dto.CodigoPuc < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Invalido";
                    return result;
                }

                var financiadoId = await _admDescriptivaRepository.GetByIdAndTitulo(13, dto.FinanciadoId);

                if (financiadoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "financiado Id no existe";
                    return result;

                }

                if (dto.CodigoFinanciado < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Invalido";
                    return result;
                }

                if (dto.MontoComprometido < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto comprometido Invalido";
                    return result;
                }
                if (dto.MontoAnulado < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Anulado invalido";
                    return result;
                }

            
                if (dto.CodigoPresupuesto < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }
                
                if (dto.Monto <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto no puede ser cero ni negativo";
                    return result;
                }


            ADM_PUC_SOLICITUD entity = new ADM_PUC_SOLICITUD();
            entity.CODIGO_PUC_SOLICITUD = await _repository.GetNextKey();
            entity.CODIGO_DETALLE_SOLICITUD = dto.CodigoDetalleSolicitud;
            entity.CODIGO_SOLICITUD = dto.CodigoSolicitud;
            entity.CODIGO_SALDO = dto.CodigoSaldo;
            entity.CODIGO_ICP = dto.CodigoIcp;
            entity.CODIGO_PUC = dto.CodigoPuc;
            entity.FINANCIADO_ID = dto.FinanciadoId;
            entity.CODIGO_FINANCIADO = dto.CodigoFinanciado;
            entity.MONTO=dto.Monto;
            entity.MONTO_COMPROMETIDO = dto.MontoComprometido;
            entity.MONTO_ANULADO = dto.MontoAnulado;
       
            entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;


            var conectado = await _sisUsuarioRepository.GetConectado();
            entity.CODIGO_EMPRESA = conectado.Empresa;
            entity.USUARIO_INS = conectado.Usuario;
            entity.FECHA_INS = DateTime.Now;

            var created = await _repository.Add(entity);
            if (created.IsValid && created.Data != null)
            {
                
                //ACTUALIZAR PRE_V_SALDO
                _preVSaldosRepository.RecalculaSaldosPreIcpPucFi(dto.CodigoPresupuesto,dto.CodigoIcp,dto.CodigoPuc,dto.CodigoFinanciado);
                var resultDto = await MapPucSolicitudDto(created.Data);
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

        public async Task<ResultDto<AdmPucSolicitudDeleteDto>> Delete(AdmPucSolicitudDeleteDto dto) 
        {
            ResultDto<AdmPucSolicitudDeleteDto> result = new ResultDto<AdmPucSolicitudDeleteDto>(null);
            try
            {

                var codigoPucSolicitud = await _repository.GetCodigoPucSolicitud(dto.CodigoPucSolicitud);
                if (codigoPucSolicitud == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Solicitud no existe";
                    return result;
                }
              

                var deleted = await _repository.Delete(dto.CodigoPucSolicitud);
                //ACTUALIZAR PRE_V_SALDO
                _preVSaldosRepository.RecalculaSaldosPreIcpPucFi(codigoPucSolicitud.CODIGO_PRESUPUESTO,codigoPucSolicitud.CODIGO_ICP,codigoPucSolicitud.CODIGO_PUC,(int)codigoPucSolicitud.CODIGO_FINANCIADO);
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

        public async Task<ResultDto<AdmPucSolicitudResponseDto>> GetByCodigo(int codigoPucSolicitud)
        {
            ResultDto<AdmPucSolicitudResponseDto> result = new ResultDto<AdmPucSolicitudResponseDto>(null);
            try
            {

                var pucSolicitud = await _repository.GetCodigoPucSolicitud(codigoPucSolicitud);
                if (pucSolicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc solicitud no existe";
                    return result;
                }

                var resultDto = await MapPucSolicitudDto(pucSolicitud);
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
    }
 }

