using Convertidor.Data.Entities;
using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;
using Convertidor.Utility;

namespace Convertidor.Services.Cnt
{
    public class CntComprobantesService : ICntComprobantesService
    {
        private readonly ICntComprobantesRepository _repository;

        public CntComprobantesService(ICntComprobantesRepository repository)
        {
            _repository = repository;
        }

        public async Task<CntComprobantesResponseDto> MapComprobantes(CNT_COMPROBANTES dtos)
        {
            CntComprobantesResponseDto itemResult = new CntComprobantesResponseDto();
            itemResult.CodigoComprobante = dtos.CODIGO_COMPROBANTE;
            itemResult.CodigoPeriodo = dtos.CODIGO_PERIODO;
            itemResult.TipoComprobanteId = dtos.TIPO_COMPROBANTE_ID;
            itemResult.NumeroComprobante = dtos.NUMERO_COMPROBANTE;
            itemResult.FechaComprobante = dtos.FECHA_COMPROBANTE;
            itemResult.FechaComprobanteString = dtos.FECHA_COMPROBANTE.ToString("u");
            FechaDto fechaComprobanteObj = FechaObj.GetFechaDto(dtos.FECHA_COMPROBANTE);
            itemResult.FechaComprobanteObj = (FechaDto)fechaComprobanteObj;
            itemResult.OrigenId = dtos.ORIGEN_ID;
            itemResult.Observacion = dtos.OBSERVACION;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;

            return itemResult;
        }

        public async Task<List<CntComprobantesResponseDto>> MapListComprobantes(List<CNT_COMPROBANTES> dtos)
        {
            List<CntComprobantesResponseDto> result = new List<CntComprobantesResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapComprobantes(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntComprobantesResponseDto>>> GetAll()
        {

            ResultDto<List<CntComprobantesResponseDto>> result = new ResultDto<List<CntComprobantesResponseDto>>(null);
            try
            {
                var comprobantes = await _repository.GetAll();
                var cant = comprobantes.Count();
                if (comprobantes != null && comprobantes.Count() > 0)
                {
                    var listDto = await MapListComprobantes(comprobantes);

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
