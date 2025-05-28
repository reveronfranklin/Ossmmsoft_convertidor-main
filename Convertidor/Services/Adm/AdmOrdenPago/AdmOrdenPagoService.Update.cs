using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm.AdmOrdenPago;

public partial class AdmOrdenPagoService 
{
            public async Task<ResultDto<AdmOrdenPagoResponseDto>> Update(AdmOrdenPagoUpdateDto dto)
        {
            ResultDto<AdmOrdenPagoResponseDto> result = new ResultDto<AdmOrdenPagoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoOrdenPago = await _repository.GetCodigoOrdenPago((int)dto.CodigoOrdenPago);
                if (codigoOrdenPago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Orden Pago no existe";
                    return result;
                }

                var modificable= await OrdenDePagoEsModificable((int)dto.CodigoOrdenPago);
                if (!modificable)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Orden de pago no es Modificable(Status o Tiene Pagos)";
                    return result;
                }
                var compromiso = await _preCompromisosService.GetByCompromiso(dto.CodigoCompromiso);
                if (compromiso == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Compromiso Invalido";
                    return result;
                }

                if (compromiso.FechaCompromiso.Date > dto.FechaOrdenPago.Date)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"La fecha de la Orden de Pago({dto.FechaOrdenPago.Date}) no puede ser menor a la fecha del compromiso({compromiso.FechaCompromiso.Date})";
                    return result;
                }

                if (dto.FechaOrdenPago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Orden Pago Invalida";
                    return result;
                }
                var tipoOrdenPago = await _admDescriptivaRepository.GetByCodigo(dto.TipoOrdenPagoId);
                if (tipoOrdenPago==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Orden Pago Invalido";
                    return result;
                }

             

                if (dto.CantidadPago < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cantidad Pago Invalida";
                    return result;
                }

            
                var frecuenciaPagoId = await _admDescriptivaRepository.GetByIdAndTitulo(15, dto.FrecuenciaPagoId);
                if (frecuenciaPagoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Frecuencia Pago Invalido";
                    return result;
                }

                var tipoPagoId = await _admDescriptivaRepository.GetByIdAndTitulo(16, dto.TipoPagoId);
                if (tipoPagoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Pago Id Invalido";
                    return result;
                }

            
                

                if (dto.Motivo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;
                }
              
                if (dto.CodigoPresupuesto < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
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
              

          

                if (dto.NumeroComprobante < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero comprobante Invalido";
                    return result;
                }
                if (dto.FechaComprobante == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha comprobante Invalida";
                    return result;
                }

                if (dto.NumeroComprobante2 < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero comprobante2 Invalido";
                    return result;
                }
                
                codigoOrdenPago.ANO = presupuesto.ANO;
                codigoOrdenPago.CODIGO_PROVEEDOR = compromiso.CodigoProveedor;
                codigoOrdenPago.FECHA_ORDEN_PAGO = dto.FechaOrdenPago;
                codigoOrdenPago.TIPO_ORDEN_PAGO_ID = dto.TipoOrdenPagoId;
                codigoOrdenPago.CANTIDAD_PAGO = dto.CantidadPago;
                codigoOrdenPago.FRECUENCIA_PAGO_ID = dto.FrecuenciaPagoId;
                codigoOrdenPago.TIPO_PAGO_ID = dto.TipoPagoId;
                codigoOrdenPago.MOTIVO = dto.Motivo;
                codigoOrdenPago.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                codigoOrdenPago.NUMERO_COMPROBANTE = dto.NumeroComprobante;
                if (dto.FechaComprobante != null)
                {
                    codigoOrdenPago.FECHA_COMPROBANTE = (DateTime)dto.FechaComprobante;
                }

             
                codigoOrdenPago.NUMERO_COMPROBANTE2=dto.NumeroComprobante2;
                codigoOrdenPago.NUMERO_COMPROBANTE3 = dto.NumeroComprobante3;
                codigoOrdenPago.NUMERO_COMPROBANTE4 = dto.NumeroComprobante4;

                codigoOrdenPago.CON_FACTURA = 0;
                if (dto.ConFactura)
                {
                    codigoOrdenPago.CON_FACTURA = 1;
                }
                
                codigoOrdenPago.CODIGO_EMPRESA = conectado.Empresa;
                codigoOrdenPago.USUARIO_UPD = conectado.Usuario;
                codigoOrdenPago.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoOrdenPago);
                var descriptivas = await _admDescriptivaRepository.GetAll();
                var proveedores = await _admProveedoresRepository.GetByCodigo(codigoOrdenPago.CODIGO_PROVEEDOR);
                var resultDto = await MapOrdenPagoDto(codigoOrdenPago,descriptivas,proveedores);
                
                await _prePresupuestosRepository.RecalcularSaldo(codigoOrdenPago.CODIGO_PRESUPUESTO);
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