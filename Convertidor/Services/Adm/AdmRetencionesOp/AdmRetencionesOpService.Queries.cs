using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm;



namespace Convertidor.Services.Adm.AdmRetencionesOp
{
    // Usa 'partial' para indicar que la clase se define en múltiples archivos
    public partial class AdmRetencionesOpService
    {
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
        public async Task<ADM_RETENCIONES_OP> GetByOrdenPagoCodigoRetencionTipoRetencion(int codigoOrdenPago,
            int codigoRetencion, int tipoRetencionId)
        {
            return await _repository.GetByOrdenPagoCodigoRetencionTipoRetencion(codigoOrdenPago, codigoRetencion,
                tipoRetencionId);
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
    }
}