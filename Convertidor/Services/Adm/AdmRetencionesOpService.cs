using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public class AdmRetencionesOpService : IAdmRetencionesOpService
    {
        private readonly IAdmRetencionesOpRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IPRE_PRESUPUESTOSRepository _prePresupuestosRepository;
        private readonly IAdmOrdenPagoRepository _admOrdenPagoRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;
        private readonly IAdmRetencionesRepository _admRetencionesRepository;
        private readonly ISisSerieDocumentosRepository _serieDocumentosRepository;
        private readonly ISisDescriptivaRepository _sisDescriptivaRepository;
        private readonly IAdmDocumentosOpRepository _admDocumentosOpRepository;
        private readonly IOssConfigRepository _ossConfigRepository;
        private readonly IAdmBeneficariosOpService _admBeneficariosOpService;
        private readonly IAdmCompromisoOpRepository _admCompromisoOpRepository;

        public AdmRetencionesOpService(IAdmRetencionesOpRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IPRE_PRESUPUESTOSRepository prePresupuestosRepository,
                                     IAdmOrdenPagoRepository admOrdenPagoRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository,
                                     IAdmRetencionesRepository admRetencionesRepository,
                                     ISisSerieDocumentosRepository serieDocumentosRepository,
                                     ISisDescriptivaRepository sisDescriptivaRepository,
                                     IAdmDocumentosOpRepository admDocumentosOpRepository,
                                     IOssConfigRepository ossConfigRepository,
                                     IAdmBeneficariosOpService admBeneficariosOpService,
                                     IAdmCompromisoOpRepository admCompromisoOpRepository
                                     )
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _prePresupuestosRepository = prePresupuestosRepository;
            _admOrdenPagoRepository = admOrdenPagoRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
            _admRetencionesRepository = admRetencionesRepository;
            _serieDocumentosRepository = serieDocumentosRepository;
            _sisDescriptivaRepository = sisDescriptivaRepository;
            _admDocumentosOpRepository = admDocumentosOpRepository;
            _ossConfigRepository = ossConfigRepository;
            _admBeneficariosOpService = admBeneficariosOpService;
            _admCompromisoOpRepository = admCompromisoOpRepository;
        }

      
        public async Task<AdmRetencionesOpResponseDto> MapRetencionesOpDto(ADM_RETENCIONES_OP dtos)
        {
            AdmRetencionesOpResponseDto itemResult = new AdmRetencionesOpResponseDto();
            itemResult.CodigoRetencionOp = dtos.CODIGO_RETENCION_OP;
            itemResult.CodigoOrdenPago = dtos.CODIGO_ORDEN_PAGO;
            itemResult.TipoRetencionId = dtos.TIPO_RETENCION_ID;
            itemResult.DescripcionTipoRetencion = "";
            var descriptivaTipoRetencion = await _admDescriptivaRepository.GetByCodigo(dtos.TIPO_RETENCION_ID);
            if (descriptivaTipoRetencion != null)
            {
                itemResult.DescripcionTipoRetencion = descriptivaTipoRetencion.DESCRIPCION;
            }
            itemResult.CodigoRetencion = dtos.CODIGO_RETENCION;
            var admRetenciones = await _admRetencionesRepository.GetCodigoRetencion((int)dtos.CODIGO_RETENCION);
            itemResult.ConceptoPago = "";
            if (admRetenciones != null)
            {
                itemResult.ConceptoPago = admRetenciones.CONCEPTO_PAGO;
            }

            if (dtos.POR_RETENCION == null) dtos.POR_RETENCION = 0;
            itemResult.PorRetencion = dtos.POR_RETENCION;
            
            if(dtos.MONTO_RETENCION==null) dtos.MONTO_RETENCION=0;
            itemResult.MontoRetencion = dtos.MONTO_RETENCION;
            
            if (dtos.BASE_IMPONIBLE == null) dtos.BASE_IMPONIBLE = 0;
            itemResult.BaseImponible = dtos.BASE_IMPONIBLE;

            itemResult.MontoRetenido = dtos.MONTO_RETENCION;
            
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            itemResult.NumeroComprobante = dtos.NUMERO_COMPROBANTE;
            return itemResult;
            
        }

        public async Task<List<AdmRetencionesOpResponseDto>> MapListRetencionesOpDto(List<ADM_RETENCIONES_OP> dtos)
        {
            List<AdmRetencionesOpResponseDto> result = new List<AdmRetencionesOpResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapRetencionesOpDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public async Task<ResultDto<List<AdmRetencionesOpResponseDto>>> GetByOrdenPago(AdmRetencionesFilterDto filter)
        {

            ResultDto<List<AdmRetencionesOpResponseDto>> result = new ResultDto<List<AdmRetencionesOpResponseDto>>(null);
            try
            {
                var retencionesOp = await _repository.GetByOrdenPago(filter.CodigoOrdenPago);
                var cant = retencionesOp.Count();
                if (retencionesOp != null && retencionesOp.Count() > 0)
                {
                    var listDto = await MapListRetencionesOpDto(retencionesOp);
                    
   
                    
                    // Calcular el total del BaseImponible
                    var totalBaseImponible = listDto.Sum(t => t.BaseImponible);

                    // Calcular el total del Impuesto
                    var totalMontoRetencion = listDto.Sum(t => t.MontoRetencion);
                    
              
                    // Calcular el total del Impuesto exento
                    var totalMontoRetenido = listDto.Sum(t => t.MontoRetenido);

                    if (totalBaseImponible != null) result.Total1 = (decimal)totalBaseImponible;
                    if (totalMontoRetencion != null) result.Total2 = (decimal)totalMontoRetencion;
                    if (totalMontoRetenido != null) result.Total3 = (decimal)totalMontoRetenido;
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

        
        public async Task<ResultDto<List<AdmRetencionesOpResponseDto>>> GetAll()
        {

            ResultDto<List<AdmRetencionesOpResponseDto>> result = new ResultDto<List<AdmRetencionesOpResponseDto>>(null);
            try
            {
                var retencionesOp = await _repository.GetAll();
                var cant = retencionesOp.Count();
                if (retencionesOp != null && retencionesOp.Count() > 0)
                {
                    var listDto = await MapListRetencionesOpDto(retencionesOp);

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

        public async Task ReplicarMotoRetenidoDocumento(int codigoDocumentoOp,decimal montoRetencion)
        {
            await _admDocumentosOpRepository.UpdateMontoRetenido(codigoDocumentoOp,montoRetencion);
            
          
        }
        
        public async Task<ResultDto<AdmRetencionesOpResponseDto>> Update(AdmRetencionesOpUpdateDto dto)
        {
            ResultDto<AdmRetencionesOpResponseDto> result = new ResultDto<AdmRetencionesOpResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoRetencionOp = await _repository.GetCodigoRetencionOp(dto.CodigoRetencionOp);
                if (codigoRetencionOp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo retencion op no existe";
                    return result;
                }
                var codigoOrdenPago = await _admOrdenPagoRepository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
                if (codigoOrdenPago==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo orden pago invalido";
                    return result;
                }
                var tipoRetencionId = await _admDescriptivaRepository.GetByCodigo( dto.TipoRetencionId);
                if (tipoRetencionId==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "tipo retencion Id invalido";
                    return result;
                }

                var retenciones = await _admRetencionesRepository.GetAll();
                var retencionesPorTipo =
                    retenciones.Where(x => x.TIPO_RETENCION_ID == dto.TipoRetencionId).FirstOrDefault();
                if (retencionesPorTipo != null)
                {
                    var admRetencion = await _admRetencionesRepository.GetCodigoRetencion(dto.CodigoRetencion);
                    if (admRetencion ==null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Codigo retencion invalido";
                        return result;
                    }
                }
                if (dto.PorRetencion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Por retencion invalido";
                    return result;
                }
                if (dto.MontoRetencion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto retencion invalido";
                    return result;
                }
               
                if (dto.NumeroComprobante != null && dto.NumeroComprobante.Length>20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "La longitud Maxima del Comprobante es de 20 digitos";
                    return result;
                }
            


                var presupuesto = await _prePresupuestosRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (presupuesto==null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                if (dto.BaseImponible < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Base imponible Invalida";
                    return result;
                }
               
               
                codigoRetencionOp.CODIGO_RETENCION_OP=dto.CodigoRetencionOp;
                codigoRetencionOp.CODIGO_ORDEN_PAGO = dto.CodigoOrdenPago;
                codigoRetencionOp.TIPO_RETENCION_ID = dto.TipoRetencionId;
                codigoRetencionOp.CODIGO_RETENCION = dto.CodigoRetencion;
                codigoRetencionOp.POR_RETENCION = dto.PorRetencion;
                codigoRetencionOp.MONTO_RETENCION = dto.MontoRetencion;
            
                codigoRetencionOp.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                codigoRetencionOp.BASE_IMPONIBLE = dto.BaseImponible;
             
                codigoRetencionOp.CODIGO_EMPRESA = conectado.Empresa;
                codigoRetencionOp.USUARIO_UPD = conectado.Usuario;
                codigoRetencionOp.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoRetencionOp);
                
                await ReplicaRetencionesEnAdmBeneficiariosOp(dto.CodigoOrdenPago);
                await ReplicaRetencionesDocumentosEnAdmBeneficiariosOp(dto.CodigoOrdenPago);

                var resultDto = await MapRetencionesOpDto(codigoRetencionOp);
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

       
        
        public async Task<ResultDto<AdmRetencionesOpResponseDto>> Create(AdmRetencionesOpUpdateDto dto)
        {
            ResultDto<AdmRetencionesOpResponseDto> result = new ResultDto<AdmRetencionesOpResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoRetencionOp = await _repository.GetCodigoRetencionOp(dto.CodigoRetencionOp);
                if (codigoRetencionOp != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo retencion orden pago ya existe";
                    return result;
                }
                var codigoOrdenPago = await _admOrdenPagoRepository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
                if (codigoOrdenPago==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo orden pago invalido";
                    return result;
                }
                var tipoRetencion = await _admDescriptivaRepository.GetByCodigo( dto.TipoRetencionId);
                if (tipoRetencion==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "tipo retencion Id invalido";
                    return result;
                }

                var retenciones = await _admRetencionesRepository.GetAll();
                var retencionesPorTipo =
                    retenciones.Where(x => x.TIPO_RETENCION_ID == dto.TipoRetencionId).FirstOrDefault();
                if (retencionesPorTipo != null)
                {
                    var admRetencion = await _admRetencionesRepository.GetCodigoRetencion(dto.CodigoRetencion);
                    if (admRetencion ==null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Codigo retencion invalido";
                        return result;
                    }
                }
               
                if (dto.PorRetencion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Por retencion invalido";
                    return result;
                }
                if (dto.MontoRetencion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto retencion invalido";
                    return result;
                }

             


                var presupuesto = await _prePresupuestosRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (presupuesto==null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }
           
                if (dto.BaseImponible < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Base imponible Invalida";
                    return result;
                }


                ADM_RETENCIONES_OP entity = new ADM_RETENCIONES_OP();
                entity.CODIGO_RETENCION_OP = await _repository.GetNextKey();
                entity.CODIGO_ORDEN_PAGO = dto.CodigoOrdenPago;
                entity.TIPO_RETENCION_ID = dto.TipoRetencionId;
                entity.CODIGO_RETENCION = dto.CodigoRetencion;
                entity.POR_RETENCION = dto.PorRetencion;
                entity.MONTO_RETENCION = dto.MontoRetencion;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                entity.BASE_IMPONIBLE = dto.BaseImponible;
                if (tipoRetencion.CODIGO == "ISLR")
                {
                    string paddedNumber = dto.CodigoOrdenPago.ToString().PadLeft(8, '0');
                   
                    var mes = DateTime.Now.Month.ToString().PadLeft(2, '0');
                    var serieLetras = $"{DateTime.Now.Year}{mes} ";
                    entity.NUMERO_COMPROBANTE =$"{serieLetras.Trim()}{paddedNumber.Trim()}";
                }
                else
                {
                    entity.NUMERO_COMPROBANTE = "";
                    var sisDescriptiva = await _sisDescriptivaRepository.GetByExtra1(tipoRetencion.CODIGO);
                    if (sisDescriptiva != null)
                    {
                        var numeroSolicitud = await _serieDocumentosRepository.GenerateNextSerie((int)entity.CODIGO_PRESUPUESTO , sisDescriptiva.DESCRIPCION_ID,sisDescriptiva.CODIGO_DESCRIPCION);
                        if (!numeroSolicitud.IsValid)
                        {
                            result.Data = null;
                            result.IsValid = numeroSolicitud.IsValid;
                            result.Message = numeroSolicitud.Message;
                            return result;
                        }
                        entity.NUMERO_COMPROBANTE = numeroSolicitud.Data;
                    }
                   
                }
              
                
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {

                    await ReplicaRetencionesEnAdmBeneficiariosOp(dto.CodigoOrdenPago);
                    await ReplicaRetencionesDocumentosEnAdmBeneficiariosOp(dto.CodigoOrdenPago);
                    
                    var resultDto = await MapRetencionesOpDto(created.Data);
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

        public async Task ReplicaRetencionesDocumentosEnAdmBeneficiariosOp(int codigoOrdenPago)
        {

            decimal totalRetenciones = 0;
            decimal totalMontoDocumentos = 0;
            
            var ordenPago = await _admOrdenPagoRepository.GetCodigoOrdenPago(codigoOrdenPago);
            var retencionesOp = await _repository.GetByOrdenPago(codigoOrdenPago);
            if (retencionesOp.Count > 0)
            {
                // Calcular el total del Impuesto
                totalRetenciones = (decimal)retencionesOp.Sum(t => t.MONTO_RETENCION);
                
            }
            
            var documentosOp = await _admDocumentosOpRepository.GetByCodigoOrdenPago(codigoOrdenPago);
            if (documentosOp != null && documentosOp.Count() > 0)
            {
                totalMontoDocumentos = documentosOp.Sum(t => t.MONTO_DOCUMENTO);
            }
         
            var compromisos = await _admCompromisoOpRepository.GetCodigoOrdenPago(codigoOrdenPago,ordenPago.CODIGO_PRESUPUESTO);
            if (compromisos.Count > 0)
            {
                var compromiso = compromisos.FirstOrDefault();
                var beneficiarioCompromiso =
                    await _admBeneficariosOpService.GetByOrdenPagoProveedor(codigoOrdenPago, compromiso.CODIGO_PROVEEDOR);
                if (beneficiarioCompromiso != null)
                {
                    AdmBeneficiariosOpUpdateMontoDto dto  = new AdmBeneficiariosOpUpdateMontoDto();
                    dto.Monto=totalMontoDocumentos-totalRetenciones;
                    dto.CodigoBeneficiarioOp = beneficiarioCompromiso.CODIGO_BENEFICIARIO_OP;
                    await  _admBeneficariosOpService.UpdateMonto(dto);
                }
                
                
            }
            
            
           
        }
        
        public async Task ReplicaRetencionesEnAdmBeneficiariosOp(int codigoOrdenPago)
        {

            var ordenPago = await _admOrdenPagoRepository.GetCodigoOrdenPago(codigoOrdenPago);
          
            var retencionesOp = await _repository.GetByOrdenPago(codigoOrdenPago);
            if (retencionesOp.Count > 0)
            {
                // Calcular el total del Impuesto
                var totalMontoRetencion=retencionesOp.Sum(t => t.MONTO_RETENCION);
                
                var prooveedorFiscoConfig = await _ossConfigRepository.GetByClave("PROVEEDOR_FISCO");
                if (prooveedorFiscoConfig != null)
                {
                    var proveedor = int.Parse(prooveedorFiscoConfig.VALOR);
                    
                    var admBeneficiario = await _admBeneficariosOpService.GetByOrdenPagoProveedor(codigoOrdenPago,proveedor);
                    if (admBeneficiario != null)
                    {
                     
                        
                        AdmBeneficiariosOpUpdateDto dto = new AdmBeneficiariosOpUpdateDto();
                        dto.CodigoBeneficiarioOp = admBeneficiario.CODIGO_BENEFICIARIO_OP;
                        dto.CodigoPresupuesto = ordenPago.CODIGO_PRESUPUESTO;
                        dto.CodigoOrdenPago=codigoOrdenPago;
                        dto.CodigoProveedor = admBeneficiario.CODIGO_PROVEEDOR;
                        dto.Monto= (decimal)totalMontoRetencion;
                        dto.MontoAnulado = admBeneficiario.MONTO_ANULADO;
                        dto.MontoPagado = admBeneficiario.MONTO_PAGADO;
                        await _admBeneficariosOpService.Update(dto);
                        
                        
                    }
                    else
                    {
                        AdmBeneficiariosOpUpdateDto dto = new AdmBeneficiariosOpUpdateDto();
                        dto.CodigoBeneficiarioOp = 0;
                        dto.CodigoPresupuesto = ordenPago.CODIGO_PRESUPUESTO;
                        dto.CodigoOrdenPago=codigoOrdenPago;
                        dto.CodigoProveedor = proveedor;
                        dto.Monto= (decimal)totalMontoRetencion;
                        dto.MontoAnulado = 0;
                        dto.MontoPagado = 0;
                        await _admBeneficariosOpService.Create(dto);

                    }
                    
                }
                
                
            }
            
          

            
        }
        
        public async Task<ResultDto<AdmRetencionesOpDeleteDto>> Delete(AdmRetencionesOpDeleteDto dto) 
        {
            ResultDto<AdmRetencionesOpDeleteDto> result = new ResultDto<AdmRetencionesOpDeleteDto>(null);
            try
            {

                var codigoRetencionOp = await _repository.GetCodigoRetencionOp(dto.CodigoRetencionOp);
                if (codigoRetencionOp == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo retencion op no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoRetencionOp);

                await ReplicaRetencionesEnAdmBeneficiariosOp(codigoRetencionOp.CODIGO_ORDEN_PAGO);
                await ReplicaRetencionesDocumentosEnAdmBeneficiariosOp(codigoRetencionOp.CODIGO_ORDEN_PAGO);
                
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

