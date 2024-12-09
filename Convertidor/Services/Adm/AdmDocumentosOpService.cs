using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Utility;

namespace Convertidor.Services.Adm
{
    public class AdmDocumentosOpService : IAdmDocumentosOpService
    {
        private readonly IAdmDocumentosOpRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IPRE_PRESUPUESTOSRepository _prePresupuestosRepository;
        private readonly IAdmOrdenPagoRepository _admOrdenPagoRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;

        public AdmDocumentosOpService(IAdmDocumentosOpRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IPRE_PRESUPUESTOSRepository prePresupuestosRepository,
                                     IAdmOrdenPagoRepository admOrdenPagoRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _prePresupuestosRepository = prePresupuestosRepository;
            _admOrdenPagoRepository = admOrdenPagoRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
        }

      
        public async Task<AdmDocumentosOpResponseDto> MapDocumentosOpDto(ADM_DOCUMENTOS_OP dtos)
        {
            AdmDocumentosOpResponseDto itemResult = new AdmDocumentosOpResponseDto();
            itemResult.CodigoDocumentoOp = dtos.CODIGO_DOCUMENTO_OP;
            itemResult.CodigoOrdenPago = dtos.CODIGO_ORDEN_PAGO;
            itemResult.FechaComprobante = dtos.FECHA_COMPROBANTE;
            itemResult.FechaComprobanteString = Fecha.GetFechaString(dtos.FECHA_COMPROBANTE);
            FechaDto fechaComprobanteObj = Fecha.GetFechaDto( dtos.FECHA_COMPROBANTE);
            itemResult.FechaComprobanteObj = (FechaDto) fechaComprobanteObj;    
            itemResult.PeriodoImpositivo = dtos.PERIODO_IMPOSITIVO;
            
            //Lista de valores ===> Titulo Id 31
            itemResult.DescripcionTipoOperacion = "";
            itemResult.TipoOperacionId = dtos.TIPO_OPERACION_ID;
            itemResult.DescripcionTipoOperacion =  await _admDescriptivaRepository.GetDescripcion(itemResult.TipoOperacionId);
            
            //Lista de valores ===> Titulo Id 32
            itemResult.DescripcionTipoDocumento = "";
            itemResult.TipoDocumentoId = dtos.TIPO_DOCUMENTO_ID;
            itemResult.DescripcionTipoDocumento =  await _admDescriptivaRepository.GetDescripcion(itemResult.TipoDocumentoId);

            //Lista de valores ===> Titulo Id 34
            itemResult.DescripcionTipoTransaccion = "";
            itemResult.TipoTransaccionId = dtos.TIPO_TRANSACCION_ID;
            itemResult.DescripcionTipoTransaccion =  await _admDescriptivaRepository.GetDescripcion(itemResult.TipoTransaccionId);

            //Lista de valores ===> Titulo Id 18
            itemResult.DescripcionTipoImpuesto = "";
            itemResult.TipoImpuestoId = dtos.TIPO_IMPUESTO_ID;
            itemResult.DescripcionTipoImpuesto =  await _admDescriptivaRepository.GetDescripcion(itemResult.TipoImpuestoId);

            //Lista de valores ===> Titulo Id 33
            itemResult.DescripcionEstatusFisco = "";
            itemResult.EstatusFiscoId = dtos.ESTATUS_FISCO_ID;
            itemResult.DescripcionEstatusFisco =  await _admDescriptivaRepository.GetDescripcion((int)itemResult.EstatusFiscoId);

            
            itemResult.FechaDocumento = dtos.FECHA_DOCUMENTO;
            itemResult.FechaDocumentoString =Fecha.GetFechaString(dtos.FECHA_DOCUMENTO);
            FechaDto fechaDocumentoObj = Fecha.GetFechaDto(dtos.FECHA_DOCUMENTO);
            itemResult.FechaDocumentoObj = (FechaDto)fechaDocumentoObj;
            itemResult.NumeroDocumento = dtos.NUMERO_DOCUMENTO;
            itemResult.NumeroControlDocumento = dtos.NUMERO_CONTROL_DOCUMENTO;
            itemResult.MontoDocumento = dtos.MONTO_DOCUMENTO;
            itemResult.BaseImponible = dtos.BASE_IMPONIBLE;
            itemResult.MontoImpuesto = dtos.MONTO_IMPUESTO;
            itemResult.NumeroDocumentoAfectado = dtos.NUMERO_DOCUMENTO_AFECTADO;
         
            itemResult.MontoImpuestoExento = dtos.MONTO_IMPUESTO_EXENTO;
            itemResult.MontoRetenido = dtos.MONTO_RETENIDO;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            itemResult.NumeroExpediente = dtos.NUMERO_EXPEDIENTE;
           
            

            return itemResult;
        }

        public async Task<List<AdmDocumentosOpResponseDto>> MapListDocumentosOpDto(List<ADM_DOCUMENTOS_OP> dtos)
        {
            List<AdmDocumentosOpResponseDto> result = new List<AdmDocumentosOpResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapDocumentosOpDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public async Task<ResultDto<List<AdmDocumentosOpResponseDto>>> GetAll()
        {

            ResultDto<List<AdmDocumentosOpResponseDto>> result = new ResultDto<List<AdmDocumentosOpResponseDto>>(null);
            try
            {
                var documentosOp = await _repository.GetAll();
                var cant = documentosOp.Count();
                if (documentosOp != null && documentosOp.Count() > 0)
                {
                    var listDto = await MapListDocumentosOpDto(documentosOp);

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

        public async Task<ResultDto<List<AdmDocumentosOpResponseDto>>> GetByCodigoOrdenPago(AdmDocumentosFilterDto dto)
        {

            ResultDto<List<AdmDocumentosOpResponseDto>> result = new ResultDto<List<AdmDocumentosOpResponseDto>>(null);
            try
            {
                var documentosOp = await _repository.GetByCodigoOrdenPago(dto.CodigoOrdenPago);
                var cant = documentosOp.Count();
                if (documentosOp != null && documentosOp.Count() > 0)
                {
                    var listDto = await MapListDocumentosOpDto(documentosOp);

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

        
        public async Task<ResultDto<AdmDocumentosOpResponseDto>> Update(AdmDocumentosOpUpdateDto dto)
        {
            ResultDto<AdmDocumentosOpResponseDto> result = new ResultDto<AdmDocumentosOpResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoDocumentoOp = await _repository.GetCodigoDocumentoOp(dto.CodigoDocumentoOp);
                if (codigoDocumentoOp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo documento op no existe";
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
                if (dto.FechaComprobante ==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha comprobante invalido";
                    return result;
                }
                if(dto.PeriodoImpositivo is not null && dto.PeriodoImpositivo.Length < 6) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Periodo impositivo invalido";
                    return result;
                }
                var tipoOperacion = await _admDescriptivaRepository.GetByCodigo( dto.TipoOperacionId);
                if(tipoOperacion==null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo operacion Id invalido";
                    return result;
                }
                var tipoDocumento = await _admDescriptivaRepository.GetByCodigo(dto.TipoDocumentoId);
                if (tipoDocumento==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo documento Id invalido";
                    return result;
                }
                if (dto.FechaDocumento==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha documento invalida";
                    return result;
                }
                if (dto.NumeroDocumento is not null && dto.NumeroDocumento.Length>20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero documento invalido";
                    return result;
                }

                if (dto.NumeroControlDocumento is not null && dto.NumeroControlDocumento.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero control documento invalido";
                    return result;
                }

                if(dto.MontoDocumento < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto documento invalido";
                    return result;
                }

                if(dto.BaseImponible < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Base imponible invalida";
                    return result;
                }

                if (dto.MontoImpuesto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto impuesto invalido";
                    return result;
                }

                var tipoTransaccion = await _admDescriptivaRepository.GetByCodigo( dto.TipoTransaccionId);
                if (tipoTransaccion==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo transaccion Id invalido";
                    return result;
                }

                var tipoImpuesto = await _admDescriptivaRepository.GetByCodigo( dto.TipoImpuestoId);
                if (tipoImpuesto==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo impuesto Id invalido";
                    return result;
                }

                if (dto.MontoImpuestoExento < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto impuesto exento invalido";
                    return result;
                }

                if(dto.MontoRetenido < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto retenido invalido";
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

                var estatusFisco = await _admDescriptivaRepository.GetByCodigo(dto.EstatusFiscoId);
                 if(estatusFisco==null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estatus fisco Id Invalido";
                    return result;
                }


                codigoDocumentoOp.FECHA_COMPROBANTE = dto.FechaComprobante;
                codigoDocumentoOp.PERIODO_IMPOSITIVO = dto.PeriodoImpositivo;
                codigoDocumentoOp.TIPO_OPERACION_ID = dto.TipoOperacionId;
                codigoDocumentoOp.TIPO_DOCUMENTO_ID = dto.TipoDocumentoId;
                codigoDocumentoOp.FECHA_DOCUMENTO = dto.FechaDocumento;
                codigoDocumentoOp.NUMERO_DOCUMENTO = dto.NumeroDocumento;
                codigoDocumentoOp.NUMERO_CONTROL_DOCUMENTO = dto.NumeroControlDocumento;
                codigoDocumentoOp.MONTO_DOCUMENTO = dto.MontoDocumento;
                codigoDocumentoOp.BASE_IMPONIBLE = dto.BaseImponible;
                codigoDocumentoOp.MONTO_IMPUESTO = dto.MontoImpuesto;
                codigoDocumentoOp.NUMERO_DOCUMENTO_AFECTADO = dto.NumeroDocumentoAfectado;
                codigoDocumentoOp.TIPO_TRANSACCION_ID = dto.TipoTransaccionId;
                codigoDocumentoOp.TIPO_IMPUESTO_ID = dto.TipoImpuestoId;
                codigoDocumentoOp.MONTO_IMPUESTO_EXENTO = dto.MontoImpuestoExento;
                codigoDocumentoOp.MONTO_RETENIDO = dto.MontoRetenido;
                codigoDocumentoOp.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                codigoDocumentoOp.NUMERO_EXPEDIENTE = dto.NumeroExpediente;
                codigoDocumentoOp.ESTATUS_FISCO_ID = dto.EstatusFiscoId;




                codigoDocumentoOp.CODIGO_EMPRESA = conectado.Empresa;
                codigoDocumentoOp.USUARIO_UPD = conectado.Usuario;
                codigoDocumentoOp.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoDocumentoOp);

                var resultDto = await MapDocumentosOpDto(codigoDocumentoOp);
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

        public async Task<ResultDto<AdmDocumentosOpResponseDto>> Create(AdmDocumentosOpUpdateDto dto)
        {
            ResultDto<AdmDocumentosOpResponseDto> result = new ResultDto<AdmDocumentosOpResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoDocumentoOp = await _repository.GetCodigoDocumentoOp(dto.CodigoDocumentoOp);
                if (codigoDocumentoOp != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo documento ya existe";
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
                if (dto.FechaComprobante ==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha comprobante invalido";
                    return result;
                }
                if(dto.PeriodoImpositivo is not null && dto.PeriodoImpositivo.Length < 6) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Periodo impositivo invalido";
                    return result;
                }
                var tipoOperacion = await _admDescriptivaRepository.GetByCodigo( dto.TipoOperacionId);
                if(tipoOperacion==null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo operacion Id invalido";
                    return result;
                }
                var tipoDocumento = await _admDescriptivaRepository.GetByCodigo(dto.TipoDocumentoId);
                if (tipoDocumento==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo documento Id invalido";
                    return result;
                }
                if (dto.FechaDocumento==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha documento invalida";
                    return result;
                }
                if (dto.NumeroDocumento is not null && dto.NumeroDocumento.Length>20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero documento invalido";
                    return result;
                }

                if (dto.NumeroControlDocumento is not null && dto.NumeroControlDocumento.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero control documento invalido";
                    return result;
                }

                if(dto.MontoDocumento >0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto documento invalido";
                    return result;
                }

             


                var tipoTransaccion = await _admDescriptivaRepository.GetByCodigo( dto.TipoTransaccionId);
                if (tipoTransaccion==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo transaccion Id invalido";
                    return result;
                }

                var tipoImpuesto = await _admDescriptivaRepository.GetByCodigo( dto.TipoImpuestoId);
                if (tipoImpuesto==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo impuesto Id invalido";
                    return result;
                }

                if (dto.MontoImpuestoExento < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto impuesto exento invalido";
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

                var estatusFisco = await _admDescriptivaRepository.GetByCodigo(dto.EstatusFiscoId);
                 if(estatusFisco==null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estatus fisco Id Invalido";
                    return result;
                }


                ADM_DOCUMENTOS_OP entity = new ADM_DOCUMENTOS_OP();
                entity.CODIGO_DOCUMENTO_OP = await _repository.GetNextKey();
                entity.CODIGO_ORDEN_PAGO = dto.CodigoOrdenPago;
                entity.FECHA_COMPROBANTE = dto.FechaComprobante;
                entity.PERIODO_IMPOSITIVO = dto.PeriodoImpositivo;
                entity.TIPO_OPERACION_ID = dto.TipoOperacionId;
                entity.TIPO_DOCUMENTO_ID = dto.TipoDocumentoId;
                entity.FECHA_DOCUMENTO = dto.FechaDocumento;
                entity.NUMERO_DOCUMENTO = dto.NumeroDocumento;
                entity.NUMERO_CONTROL_DOCUMENTO = dto.NumeroControlDocumento;
            
                //TODO Calcular Impuesto dependiendo del  proveedor
                //CALCULA EL MONTO DE LA BASE IMPONIBLE Y DEMAS PARA CUANDO ES CARGADO EL TOTAL DEL DOCUMENTO--
                /*

                    -----------------------------------------------------------------------------------------------
                   -----------------------------------------------------------------------------------------------
                   --CALCULA EL MONTO DE LA BASE IMPONIBLE Y DEMAS PARA CUANDO ES CARGADO EL TOTAL DEL DOCUMENTO--
                   -----------------------------------------------------------------------------------------------
                   -----------------------------------------------------------------------------------------------
                   --/* 
                   IF NVL(:ADM_DOCUMENTOS_OP.ESTATUS_FISCO_VALOR,0) = 0 Then
                    :ADM_DOCUMENTOS_OP.MONTO_IMPUESTO := 0;
                    :ADM_DOCUMENTOS_OP.MONTO_RETENIDO := 0;
                    :ADM_DOCUMENTOS_OP.BASE_IMPONIBLE := 0;
                    :ADM_DOCUMENTOS_OP.MONTO_IMPUESTO_EXENTO := :ADM_DOCUMENTOS_OP.MONTO_DOCUMENTO;
                   ELSE                                         
                   	:ADM_DOCUMENTOS_OP.BASE_IMPONIBLE := ROUND((NVL(:ADM_DOCUMENTOS_OP.MONTO_DOCUMENTO,0)-NVL(:ADM_DOCUMENTOS_OP.MONTO_IMPUESTO_EXENTO,0))/((:ADM_IMPUESTOS_OP.POR_IMPUESTO/100)+1),2);
                   	:ADM_DOCUMENTOS_OP.MONTO_IMPUESTO := ROUND(NVL(:ADM_DOCUMENTOS_OP.BASE_IMPONIBLE,0)*(:ADM_IMPUESTOS_OP.POR_IMPUESTO/100),2);
                   	:ADM_DOCUMENTOS_OP.MONTO_RETENIDO := ROUND(((NVL(:ADM_DOCUMENTOS_OP.MONTO_IMPUESTO,0)*((:ADM_DOCUMENTOS_OP.ESTATUS_FISCO_VALOR/100)+1))-(NVL(:ADM_DOCUMENTOS_OP.MONTO_IMPUESTO,0))),2);
                   END IF;
                   
                   
                   IF :ADM_DOCUMENTOS_OP.TOTAL_MONTO_DOCUMENTO > NVL(:ADM_COMPROMISO_OP.MONTO_TOTAL,NVL(:ADM_DOCUMENTOS_OP.TOTAL_MONTO_DOCUMENTO,0)) THEN
                   	F_ALERT.OK('El Monto total de las Facturas No pueden ser Mayor al Monto del Compromiso',null,'CAUTION');
                     RAISE FORM_TRIGGER_FAILURE;
                   END IF;
                   

                 */
                entity.ESTATUS_FISCO_ID = dto.EstatusFiscoId;
                entity.MONTO_DOCUMENTO = dto.MontoDocumento;
                
                //buscar la descriptiva y se toma EXTRA1 EL PORCENTAJE DE RERENCION IVA
                //SI EL PORCENTAJE ES MAYOR A CERO SE REALIZA EL CALCULO DE BASE_IMPONIBLE Y MONTO_IMPUESTO
                var porcentajeRetencion = decimal.Parse(estatusFisco.EXTRA1);
                var porcentajeIva = decimal.Parse(tipoImpuesto.EXTRA1);
                if (porcentajeRetencion == 0)
                {
                    entity.MONTO_IMPUESTO = 0;
                    entity.MONTO_RETENIDO = 0;
                    entity.BASE_IMPONIBLE = 0;
                    entity.MONTO_IMPUESTO_EXENTO = dto.MontoDocumento;
                }
                else
                {
                    
                    entity.BASE_IMPONIBLE =  (entity.MONTO_DOCUMENTO-entity.MONTO_IMPUESTO_EXENTO)/((porcentajeIva/100)+1);
                    entity.BASE_IMPONIBLE= Math.Ceiling((decimal)entity.BASE_IMPONIBLE * 100) / 100; 
                   
                    entity.MONTO_IMPUESTO = (entity.BASE_IMPONIBLE)*(porcentajeIva/100);
                    entity.MONTO_IMPUESTO= Math.Ceiling((decimal)entity.MONTO_IMPUESTO * 100) / 100; 
                  
                    entity.MONTO_RETENIDO = (entity.MONTO_IMPUESTO)*((porcentajeIva/100)+1)-(entity.MONTO_IMPUESTO);
                    entity.MONTO_RETENIDO= Math.Ceiling((decimal)entity.MONTO_RETENIDO * 100) / 100; 
                    entity.MONTO_IMPUESTO_EXENTO = 0;

                }
              
           
              
                
                entity.NUMERO_DOCUMENTO_AFECTADO = dto.NumeroDocumentoAfectado;
                entity.TIPO_TRANSACCION_ID = dto.TipoTransaccionId;
                entity.TIPO_IMPUESTO_ID = dto.TipoImpuestoId;
                
               
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                entity.NUMERO_EXPEDIENTE = dto.NumeroExpediente;
               
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapDocumentosOpDto(created.Data);
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

        public async Task<ResultDto<AdmDocumentosOpDeleteDto>> Delete(AdmDocumentosOpDeleteDto dto) 
        {
            ResultDto<AdmDocumentosOpDeleteDto> result = new ResultDto<AdmDocumentosOpDeleteDto>(null);
            try
            {

                var codigoDocumentoOp = await _repository.GetCodigoDocumentoOp(dto.CodigoDocumentoOp);
                if (codigoDocumentoOp == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo documento op no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoDocumentoOp);

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

