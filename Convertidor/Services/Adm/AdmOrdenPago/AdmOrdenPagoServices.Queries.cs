using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm.AdmOrdenPago;

public partial class AdmOrdenPagoService 
{



            public async Task<string> GetEstatusTextOrdenPago(int codigoOrdenPago)
            {
                var result = string.Empty;

                await _repository.UpdateEstatusText(codigoOrdenPago);
                var ordenPago = await _repository.GetCodigoOrdenPago(codigoOrdenPago);
                if (ordenPago is not null)
                {
                    result = ordenPago.ESTATUS_TEXT;
                }

                return result;

            }
            
            public string GetDenominacionDescriptiva(List<ADM_DESCRIPTIVAS> descriptivas , int id)
            {
                string result = "";
                
                var descriptivaTipoOrdenPago =
                    descriptivas.Where(x => x.DESCRIPCION_ID == id).FirstOrDefault();
                if (descriptivaTipoOrdenPago != null)
                {
                    result = descriptivaTipoOrdenPago.DESCRIPCION;
                }
    
                return result;
            }
    
            public async Task<ResultDto<List<AdmOrdenPagoResponseDto>>> GetByPresupuesto(AdmOrdenPagoFilterDto filter)
            {
    
                ResultDto<List<AdmOrdenPagoResponseDto>> result = new ResultDto<List<AdmOrdenPagoResponseDto>>(null);
                try
                {
                    var ordenPago = await _repository.GetByPresupuesto(filter);
                 
                    if (ordenPago.Data != null )
                    {
                        var listDto = await MapListOrdenPagoDto(ordenPago.Data);
    
                        result.Data = listDto;
                        result.IsValid = true;
                        result.Message = "";
                        result.CantidadRegistros = ordenPago.CantidadRegistros;
                        result.TotalPage = ordenPago.TotalPage;
                        result.Page = ordenPago.Page;
    
    
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