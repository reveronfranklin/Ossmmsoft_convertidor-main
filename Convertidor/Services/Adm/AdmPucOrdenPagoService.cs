using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public class AdmPucOrdenPagoService : IAdmPucOrdenPagoService
    {
        private readonly IAdmPucOrdenPagoRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IPRE_PRESUPUESTOSRepository _prePresupuestosRepository;
        private readonly IAdmOrdenPagoRepository _admOrdenPagoRepository;
        private readonly IAdmCompromisoOpRepository _admCompromisoOpRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
        private readonly IPRE_INDICE_CAT_PRGRepository _preIndiceCatPrgRepository;
        private readonly IPRE_PLAN_UNICO_CUENTASRepository _prePlanUnicoCuentasRepository;
        private readonly IPreDescriptivaRepository _preDescriptivaRepository;
        private readonly IPRE_V_SALDOSRepository _preVSaldosRepository;

        public AdmPucOrdenPagoService(IAdmPucOrdenPagoRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IPRE_PRESUPUESTOSRepository prePresupuestosRepository,
                                     IAdmOrdenPagoRepository admOrdenPagoRepository,
                                     IAdmCompromisoOpRepository admCompromisoOpRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository,
                                     IPRE_INDICE_CAT_PRGRepository preIndiceCatPrgRepository,
                                     IPRE_PLAN_UNICO_CUENTASRepository prePlanUnicoCuentasRepository,
                                     IPreDescriptivaRepository preDescriptivaRepository,
                                     IPRE_V_SALDOSRepository preVSaldosRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _prePresupuestosRepository = prePresupuestosRepository;
            _admOrdenPagoRepository = admOrdenPagoRepository;
            _admCompromisoOpRepository = admCompromisoOpRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
            _preIndiceCatPrgRepository = preIndiceCatPrgRepository;
            _prePlanUnicoCuentasRepository = prePlanUnicoCuentasRepository;
            _preDescriptivaRepository = preDescriptivaRepository;
            _preVSaldosRepository = preVSaldosRepository;
        }

      
        public async Task<AdmPucOrdenPagoResponseDto> MapPucOrdenPagoDto(ADM_PUC_ORDEN_PAGO dtos)
        {
            AdmPucOrdenPagoResponseDto itemResult = new AdmPucOrdenPagoResponseDto();
            itemResult.CodigoPucOrdenPago = dtos.CODIGO_PUC_ORDEN_PAGO;
            itemResult.CodigoOrdenPago = dtos.CODIGO_ORDEN_PAGO;
            itemResult.CodigoPucCompromiso = dtos.CODIGO_PUC_COMPROMISO;
            itemResult.CodigoIcp = dtos.CODIGO_ICP;
          
            itemResult.CodigoPuc = dtos.CODIGO_PUC;
          
            itemResult.FinanciadoId = dtos.FINANCIADO_ID;

            itemResult.DescripcionFinanciado = "";
            var financiadoIdObj = await _preDescriptivaRepository.GetByCodigo((int)dtos.FINANCIADO_ID);
            if (financiadoIdObj!= null)
            {
                itemResult.DescripcionFinanciado = financiadoIdObj.DESCRIPCION;
            }
            
           
            itemResult.CodigoFinanciado = dtos.CODIGO_FINANCIADO;
            itemResult.CodigoSaldo= dtos.CODIGO_SALDO;
            itemResult.Monto = dtos.MONTO;
            itemResult.MontoPagado = dtos.MONTO_PAGADO;
            itemResult.MontoAnulado=dtos.MONTO_ANULADO;
            if (dtos.MONTO_COMPROMISO == null) dtos.MONTO_COMPROMISO = 0;
            itemResult.MontoCompromiso = (decimal)dtos.MONTO_COMPROMISO;
            itemResult.CodigoCompromisoOp = dtos.CODIGO_COMPROMISO_OP;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            var saldo = await _preVSaldosRepository.GetByCodigo(dtos.CODIGO_SALDO);
            if (saldo != null)
            {
                itemResult.IcpConcat = saldo.CODIGO_ICP_CONCAT;
                itemResult.PucConcat = saldo.CODIGO_PUC_CONCAT;
                itemResult.DescripcionIcp = saldo.DENOMINACION_ICP;
                itemResult.DescripcionPuc = saldo.DENOMINACION_PUC;
            }
            

            return itemResult;
        }

        public async Task<List<AdmPucOrdenPagoResponseDto>> MapListPucOrdenPagoDto(List<ADM_PUC_ORDEN_PAGO> dtos)
        {
            List<AdmPucOrdenPagoResponseDto> result = new List<AdmPucOrdenPagoResponseDto>();
            {

                 foreach (var item in dtos)
                {

                    var itemResult = await MapPucOrdenPagoDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        /*public async Task<ResultDto<List<AdmPucOrdenPagoResponseDto>>> GetAll()
        {

            ResultDto<List<AdmPucOrdenPagoResponseDto>> result = new ResultDto<List<AdmPucOrdenPagoResponseDto>>(null);
            try
            {
                var pucOrdenPago = await _repository.GetAll();
                var cant = pucOrdenPago.Count();
                if (pucOrdenPago != null && pucOrdenPago.Count() > 0)
                {
                    var listDto = await MapListPucOrdenPagoDto(pucOrdenPago);

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

        }*/
        
        public async Task<ResultDto<List<AdmPucOrdenPagoResponseDto>>> GetByOrdenPago(int codigoOrdenPago)
        {

            ResultDto<List<AdmPucOrdenPagoResponseDto>> result = new ResultDto<List<AdmPucOrdenPagoResponseDto>>(null);
            try
            {
              
                var pucOrdenPago = await _repository.GetByOrdenPago(codigoOrdenPago);
                if (pucOrdenPago != null && pucOrdenPago.Count() > 0)
                {
                    var listDto = await MapListPucOrdenPagoDto(pucOrdenPago);

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

        
        public async Task<ResultDto<AdmPucOrdenPagoResponseDto>> Update(AdmPucOrdenPagoUpdateDto dto)
        {
            ResultDto<AdmPucOrdenPagoResponseDto> result = new ResultDto<AdmPucOrdenPagoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoPucOrdenPago = await _repository.GetCodigoPucOrdenPago(dto.CodigoPucOrdenPago);
                if (codigoPucOrdenPago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc orden pago no existe";
                    return result;
                }
                var codigoOrdenPago = await _admOrdenPagoRepository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
                if (dto.CodigoOrdenPago < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo orden pago invalido";
                    return result;
                }
                if(dto.CodigoPucCompromiso < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc compromiso invalido";
                    return result;
                }
                if (dto.CodigoIcp < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp invalido";
                    return result;
                }
                if (dto.CodigoPuc < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc invalido";
                    return result;
                }
                if (dto.CodigoFinanciado < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo financiado invalido";
                    return result;
                }
                if (dto.CodigoSaldo < 0 )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Saldo invalido";
                    return result;
                }
                var financiadoId = await _admDescriptivaRepository.GetByIdAndTitulo(13, dto.FinanciadoId);
                if (dto.FinanciadoId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Financiado Id invalido";
                    return result;
                }

                if (dto.CodigoFinanciado < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo financiado invalido";
                    return result;
                }
        
                if (dto.CodigoSaldo < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo saldo Invalido";
                    return result;
                }

                if (dto.Monto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Invalido";
                    return result;
                }

                if (dto.MontoPagado < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Pagado Invalido";
                    return result;
                }

                if (dto.MontoAnulado < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto anulado Invalido";
                    return result;
                }
                if (dto.Extra1 is not null && dto.Extra1.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra1 Invalido";
                    return result;
                }
                if (dto.Extra2 is not null && dto.Extra2.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra2 Invalido";
                    return result;
                }

                if (dto.Extra3 is not null && dto.Extra3.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra3 Invalido";
                    return result;
                }

                var codigoCompromisoOp = await _admCompromisoOpRepository.GetCodigoCompromisoOp(dto.CodigoCompromisoOp);
                if(dto.CodigoCompromisoOp < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Compromiso op Invalido";
                    return result;

                }

                var codigopresupuesto = await _prePresupuestosRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (dto.CodigoPresupuesto < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

               
               
                codigoPucOrdenPago.CODIGO_PUC_ORDEN_PAGO=dto.CodigoPucOrdenPago;
                codigoPucOrdenPago.CODIGO_ORDEN_PAGO = dto.CodigoOrdenPago;
                codigoPucOrdenPago.CODIGO_PUC_COMPROMISO = dto.CodigoPucCompromiso;
                codigoPucOrdenPago.CODIGO_ICP = dto.CodigoIcp;
                codigoPucOrdenPago.CODIGO_PUC = dto.CodigoPuc;
                codigoPucOrdenPago.FINANCIADO_ID = dto.FinanciadoId;
                codigoPucOrdenPago.CODIGO_FINANCIADO = dto.CodigoFinanciado;
                codigoPucOrdenPago.CODIGO_SALDO = dto.CodigoSaldo;
                codigoPucOrdenPago.MONTO = dto.Monto;
                codigoPucOrdenPago.MONTO_PAGADO = dto.MontoPagado;
                codigoPucOrdenPago.MONTO_ANULADO = dto.MontoAnulado;
                codigoPucOrdenPago.EXTRA1 = dto.Extra1;
                codigoPucOrdenPago.EXTRA2 = dto.Extra2;
                codigoPucOrdenPago.EXTRA3 = dto.Extra3;
                codigoPucOrdenPago.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                codigoPucOrdenPago.CODIGO_COMPROMISO_OP = dto.CodigoCompromisoOp;




                codigoPucOrdenPago.CODIGO_EMPRESA = conectado.Empresa;
                codigoPucOrdenPago.USUARIO_UPD = conectado.Usuario;
                codigoPucOrdenPago.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoPucOrdenPago);
             
                var resultDto = await MapPucOrdenPagoDto(codigoPucOrdenPago);
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

        public async Task<ResultDto<AdmPucOrdenPagoResponseDto>> Create(AdmPucOrdenPagoUpdateDto dto)
        {
            ResultDto<AdmPucOrdenPagoResponseDto> result = new ResultDto<AdmPucOrdenPagoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoPucOrdenPago = await _repository.GetCodigoPucOrdenPago(dto.CodigoPucOrdenPago);
                if (codigoPucOrdenPago != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo puc orden pago ya existe";
                    return result;
                }
                var codigoOrdenPago = await _admOrdenPagoRepository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
                if (dto.CodigoOrdenPago < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo orden pago invalido";
                    return result;
                }
                if (dto.CodigoPucCompromiso < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc compromiso invalido";
                    return result;
                }
                if (dto.CodigoIcp < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp invalido";
                    return result;
                }
                if (dto.CodigoPuc < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc invalido";
                    return result;
                }
                if (dto.CodigoFinanciado < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo financiado invalido";
                    return result;
                }
                if (dto.CodigoSaldo < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Saldo invalido";
                    return result;
                }
                var financiadoId = await _admDescriptivaRepository.GetByIdAndTitulo(13, dto.FinanciadoId);
                if (dto.FinanciadoId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Financiado Id invalido";
                    return result;
                }

                if (dto.CodigoFinanciado < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo financiado invalido";
                    return result;
                }

                if (dto.CodigoSaldo < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo saldo Invalido";
                    return result;
                }

                if (dto.Monto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Invalido";
                    return result;
                }

                if (dto.MontoPagado < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Pagado Invalido";
                    return result;
                }

                if (dto.MontoAnulado < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto anulado Invalido";
                    return result;
                }
                if (dto.Extra1 is not null && dto.Extra1.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra1 Invalido";
                    return result;
                }
                if (dto.Extra2 is not null && dto.Extra2.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra2 Invalido";
                    return result;
                }

                if (dto.Extra3 is not null && dto.Extra3.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra3 Invalido";
                    return result;
                }

                var codigoCompromisoOp = await _admCompromisoOpRepository.GetCodigoCompromisoOp(dto.CodigoCompromisoOp);
                if (dto.CodigoCompromisoOp < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Compromiso op Invalido";
                    return result;

                }

                var codigopresupuesto = await _prePresupuestosRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (dto.CodigoPresupuesto < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }


                ADM_PUC_ORDEN_PAGO entity = new ADM_PUC_ORDEN_PAGO();
                entity.CODIGO_PUC_ORDEN_PAGO = await _repository.GetNextKey();
                entity.CODIGO_ORDEN_PAGO = dto.CodigoOrdenPago;
                entity.CODIGO_PUC_COMPROMISO = dto.CodigoPucCompromiso;
                entity.CODIGO_ICP = dto.CodigoIcp;
                entity.CODIGO_PUC = dto.CodigoPuc;
                entity.FINANCIADO_ID = dto.FinanciadoId;
                entity.CODIGO_FINANCIADO = dto.CodigoFinanciado;
                entity.CODIGO_SALDO = dto.CodigoSaldo;
                entity.MONTO = dto.Monto;
                entity.MONTO_PAGADO = dto.MontoPagado;
                entity.MONTO_ANULADO = dto.MontoAnulado;
                entity.MONTO_COMPROMISO = dto.MontoCompromiso;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.CODIGO_COMPROMISO_OP=dto.CodigoCompromisoOp;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;


                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapPucOrdenPagoDto(created.Data);
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

        public async Task<ResultDto<AdmPucOrdenPagoDeleteDto>> Delete(AdmPucOrdenPagoDeleteDto dto) 
        {
            ResultDto<AdmPucOrdenPagoDeleteDto> result = new ResultDto<AdmPucOrdenPagoDeleteDto>(null);
            try
            {

                var codigoPucOrdenPago = await _repository.GetCodigoPucOrdenPago(dto.CodigoPucOrdenPago);
                if (codigoPucOrdenPago == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo puc Orden Pago no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoPucOrdenPago);

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
 }

