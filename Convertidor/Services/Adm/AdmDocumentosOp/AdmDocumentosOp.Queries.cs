using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm.AdmDocumentosOp;

public partial class AdmDocumentosOpService
{
    
    
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
                    // Calcular el total del BaseImponible
                    decimal totalBaseImponible = listDto.Sum(t => t.BaseImponible);

                    // Calcular el total del Impuesto
                    decimal totalMontoImpuesto = listDto.Sum(t => t.MontoImpuesto);
                    
                    // Calcular el total del Impuesto exento
                    decimal totalMontoImpuestoExento = listDto.Sum(t => t.MontoImpuestoExento);

                    // Calcular el total del Impuesto exento
                    decimal totalMontoRetenido = listDto.Sum(t => t.MontoRetenido);

                    result.Total1 = totalBaseImponible;
                    result.Total2 = totalMontoImpuesto;
                    result.Total3 = totalMontoImpuestoExento;
                    result.Total4 = totalMontoRetenido;
                    result.Page = 1;
                    result.TotalPage = 1;
                    result.CantidadRegistros=listDto.Count();
                    result.Data = listDto;
                    result.IsValid = true;
                    result.Message = "";


                    return result;
                }
                else
                {
                    result.CantidadRegistros = 0;
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
                    
                    // Calcular el total del BaseImponible
                    decimal totalBaseImponible = listDto.Sum(t => t.BaseImponible);

                    // Calcular el total del Impuesto
                    decimal totalMontoImpuesto = listDto.Sum(t => t.MontoImpuesto);
                    
                    // Calcular el total del Impuesto exento
                    decimal totalMontoImpuestoExento = listDto.Sum(t => t.MontoImpuestoExento);

                    // Calcular el total del Monto exento
                    decimal totalMontoRetenido = listDto.Sum(t => t.MontoRetenido);

                    result.Total1 = totalBaseImponible;
                    result.Total2 = totalMontoImpuesto;
                    result.Total3 = totalMontoImpuestoExento;
                    result.Total4 = totalMontoRetenido;
                    result.Page = 1;
                    result.TotalPage = 1;
                    result.CantidadRegistros = listDto.Count();
                    result.Data = listDto;
                    result.IsValid = true;
                    result.Message = "";


                    return result;
                }
                else
                {
                    result.Total1 = 0;
                    result.Total2 = 0;
                    result.Total3 = 0;
                    result.Total4 = 0;
                    result.Page = 1;
                    result.TotalPage = 1;
                    result.CantidadRegistros = 0;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No data";

                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Total1 = 0;
                result.Total2 = 0;
                result.Total3 = 0;
                result.Total4 = 0;
                result.Page = 1;
                result.TotalPage = 1;
                result.CantidadRegistros = 0;
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }

        }

}