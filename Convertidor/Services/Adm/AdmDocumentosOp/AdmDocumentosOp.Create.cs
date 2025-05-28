using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm.AdmDocumentosOp;

public partial class AdmDocumentosOpService
{
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
                if (codigoOrdenPago.STATUS=="AP" || codigoOrdenPago.STATUS == "AN")
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Orden de Pago no puede ser modificada esta: {codigoOrdenPago.STATUS}";
                    return result;
                }
                if(dto.MontoDocumento <=0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto documento invalido";
                    return result;
                }

                var isValidMonto= await IsValidTotalDocumentosVsTotalCompromisoCreate(dto.CodigoOrdenPago, dto.MontoDocumento);
                if (isValidMonto == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "La suma de los documentos supera el compromiso";
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
                entity.FECHA_COMPROBANTE = (DateTime)dto.FechaComprobante;
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
               
                
                //buscar la descriptiva y se toma EXTRA1 EL PORCENTAJE DE RERENCION IVA
                //SI EL PORCENTAJE ES MAYOR A CERO SE REALIZA EL CALCULO DE BASE_IMPONIBLE Y MONTO_IMPUESTO
                var porcentajeRetencion = decimal.Parse(estatusFisco.EXTRA1);
                var porcentajeIva = decimal.Parse(tipoImpuesto.EXTRA1);
                
                
                var resultado = CalculoImpuestoRetencion.Calcular(dto.MontoDocumento,  dto.MontoImpuestoExento, porcentajeRetencion, porcentajeIva);
              
                entity.MONTO_DOCUMENTO = dto.MontoDocumento;
                entity.MONTO_IMPUESTO_EXENTO = dto.MontoImpuestoExento;
                entity.BASE_IMPONIBLE = resultado.BaseImponible;
                entity.MONTO_IMPUESTO = resultado.MontoImpuesto;
                entity.MONTO_RETENIDO = resultado.MontoRetenido;
           
              
                
                entity.NUMERO_DOCUMENTO_AFECTADO = dto.NumeroDocumentoAfectado;
                entity.TIPO_TRANSACCION_ID = dto.TipoTransaccionId;
                entity.TIPO_IMPUESTO_ID = dto.TipoImpuestoId;
                
               
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                entity.NUMERO_EXPEDIENTE = dto.NumeroExpediente;
               
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;
             
                var created = await _repository.Add(entity);
                
                var documentos = await _repository.GetByCodigoOrdenPago(dto.CodigoOrdenPago);
                var cantidadDocumentos = 0;
                if (documentos != null && documentos.Count() > 0)
                {
                    cantidadDocumentos = documentos.Count();
                }

                if (created.IsValid && created.Data != null)
                {
                    
                    await ReconstruirRetenciones(dto.CodigoOrdenPago);
                    var resultDto = await MapDocumentosOpDto(created.Data);
                    result.CantidadRegistros = cantidadDocumentos;
                    result.Data = resultDto;
                    result.IsValid = true;
                    result.Message = "";
                }
                else
                {
                    result.CantidadRegistros = cantidadDocumentos;
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

}