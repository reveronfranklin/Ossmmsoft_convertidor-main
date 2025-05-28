using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm.AdmDocumentosOp;

public partial class AdmDocumentosOpService
{
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
                if (codigoOrdenPago.STATUS=="AP" || codigoOrdenPago.STATUS == "AN")
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Orden de Pago no puede ser modificada esta: {codigoOrdenPago.STATUS}";
                    return result;
                }
                
                
                
                if(dto.MontoDocumento <= 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto documento invalido";
                    return result;
                }
                
                var isValidMonto = await IsValidTotalDocumentosVsTotalCompromisoUpdate(dto.CodigoDocumentoOp,
                    dto.CodigoOrdenPago, dto.MontoDocumento);
                if (isValidMonto==false)
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


                codigoDocumentoOp.FECHA_COMPROBANTE = (DateTime)dto.FechaComprobante;
                codigoDocumentoOp.PERIODO_IMPOSITIVO = dto.PeriodoImpositivo;
                codigoDocumentoOp.TIPO_OPERACION_ID = dto.TipoOperacionId;
                codigoDocumentoOp.TIPO_DOCUMENTO_ID = dto.TipoDocumentoId;
                codigoDocumentoOp.FECHA_DOCUMENTO = dto.FechaDocumento;
                codigoDocumentoOp.NUMERO_DOCUMENTO = dto.NumeroDocumento;
                codigoDocumentoOp.NUMERO_CONTROL_DOCUMENTO = dto.NumeroControlDocumento;
             
                codigoDocumentoOp.NUMERO_DOCUMENTO_AFECTADO = dto.NumeroDocumentoAfectado;
                codigoDocumentoOp.TIPO_TRANSACCION_ID = dto.TipoTransaccionId;
                codigoDocumentoOp.TIPO_IMPUESTO_ID = dto.TipoImpuestoId;
                codigoDocumentoOp.MONTO_DOCUMENTO = dto.MontoDocumento;
                
                //buscar la descriptiva y se toma EXTRA1 EL PORCENTAJE DE RERENCION IVA
                //SI EL PORCENTAJE ES MAYOR A CERO SE REALIZA EL CALCULO DE BASE_IMPONIBLE Y MONTO_IMPUESTO
                var porcentajeRetencion = decimal.Parse(estatusFisco.EXTRA1);
                var porcentajeIva = decimal.Parse(tipoImpuesto.EXTRA1);
                
                
                
                var resultado = CalculoImpuestoRetencion.Calcular(dto.MontoDocumento,  dto.MontoImpuestoExento, porcentajeRetencion, porcentajeIva);
                codigoDocumentoOp.MONTO_DOCUMENTO = dto.MontoDocumento;
                codigoDocumentoOp.MONTO_IMPUESTO_EXENTO = dto.MontoImpuestoExento;
                codigoDocumentoOp.BASE_IMPONIBLE = resultado.BaseImponible;
                codigoDocumentoOp.MONTO_IMPUESTO = resultado.MontoImpuesto;
                codigoDocumentoOp.MONTO_RETENIDO = resultado.MontoRetenido;

                
                codigoDocumentoOp.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                codigoDocumentoOp.NUMERO_EXPEDIENTE = dto.NumeroExpediente;
                codigoDocumentoOp.ESTATUS_FISCO_ID = dto.EstatusFiscoId;

                codigoDocumentoOp.CODIGO_EMPRESA = conectado.Empresa;
                codigoDocumentoOp.USUARIO_UPD = conectado.Usuario;
                codigoDocumentoOp.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoDocumentoOp);
                var documentos = await _repository.GetByCodigoOrdenPago(dto.CodigoOrdenPago);
                var cantidadDocumentos = 0;
                if (documentos != null && documentos.Count() > 0)
                {
                    cantidadDocumentos = documentos.Count();
                }

                await ReconstruirRetenciones(codigoDocumentoOp.CODIGO_ORDEN_PAGO);
           
                var resultDto = await MapDocumentosOpDto(codigoDocumentoOp);
                result.CantidadRegistros = cantidadDocumentos;
                result.Data = resultDto;
                result.IsValid = true;
                result.Message = "";
            }
            catch (Exception ex)
            {
                result.CantidadRegistros = 0;
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }

            return result;
        }


}