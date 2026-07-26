using Convertidor.Dtos.Adm;
using Microsoft.Extensions.Logging;

namespace Convertidor.Services.Adm.AdmOrdenPago;

public partial class AdmOrdenPagoService 
{
        public async Task<ResultDto<AdmOrdenPagoResponseDto>> Aprobar(AdmOrdenPagoAprobarAnular dto)
        {
            ResultDto<AdmOrdenPagoResponseDto> result = new ResultDto<AdmOrdenPagoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoOrdenPago = await _repository.GetCodigoOrdenPago(dto.CodigoOrdenPago);
                if (codigoOrdenPago == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Orden Pago no existe";
                    return result;
                }

                var validaAprobarOrdenPago =await ValidaAprobarOrdenPago(codigoOrdenPago);
                
                if (validaAprobarOrdenPago!="")
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message =validaAprobarOrdenPago;
                    return result;
                }

                var comprobante = await _admRetencionesOpService.AsignarComprobanteIvaOrdenPago(dto.CodigoOrdenPago);
                if (!comprobante.IsValid || comprobante.Data == null || comprobante.Data.Estado == EstadoAsignacionComprobante.Error)
                {
                    _logger.LogWarning("Aprobar: orden {CodigoOrdenPago} no pudo aprobarse por fallo en la asignacion de comprobante IVA: {Mensaje}", dto.CodigoOrdenPago, comprobante.Message);
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = comprobante.Message;
                    return result;
                }

                if (comprobante.Data.NumeroComprobante.HasValue)
                {
                    codigoOrdenPago.NUMERO_COMPROBANTE = comprobante.Data.NumeroComprobante;
                }

                codigoOrdenPago.STATUS = "AP";


                codigoOrdenPago.CODIGO_EMPRESA = conectado.Empresa;
                codigoOrdenPago.USUARIO_UPD = conectado.Usuario;
                codigoOrdenPago.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoOrdenPago);

                await _preSaldosRepository.RecalcularSaldo(codigoOrdenPago.CODIGO_PRESUPUESTO);
                var descriptivas = await _admDescriptivaRepository.GetAll();
                var proveedores = await _admProveedoresRepository.GetByCodigo(codigoOrdenPago.CODIGO_PROVEEDOR);
                var resultDto = await MapOrdenPagoDto(codigoOrdenPago,descriptivas,proveedores);
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