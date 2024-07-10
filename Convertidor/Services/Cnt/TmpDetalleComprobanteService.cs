using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public class TmpDetalleComprobanteService : ITmpDetalleComprobanteService
    {
        private readonly ITmpDetalleComprobanteRepository _repository;

        public TmpDetalleComprobanteService(ITmpDetalleComprobanteRepository repository)
        {
            _repository = repository;
        }

        public async Task<TmpDetalleComprobanteResponseDto> MapTmpDetalleComprobante(TMP_DETALLE_COMPROBANTE dtos)
        {
            TmpDetalleComprobanteResponseDto itemResult = new TmpDetalleComprobanteResponseDto();
            itemResult.CodigoDetalleComprobante = dtos.CODIGO_DETALLE_COMPROBANTE;
            itemResult.CodigoComprobante = dtos.CODIGO_COMPROBANTE;
            itemResult.CodigoMayor = dtos.CODIGO_MAYOR;
            itemResult.CodigoAuxiliar = dtos.CODIGO_AUXILIAR;
            itemResult.Referencia1 = dtos.REFERENCIA1;
            itemResult.Referencia2 = dtos.REFERENCIA2;
            itemResult.Referencia3 = dtos.REFERENCIA3;
            itemResult.Descripcion = dtos.DESCRIPCION;
            itemResult.Monto = dtos.MONTO;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;


            return itemResult;

        }

        public async Task<List<TmpDetalleComprobanteResponseDto>> MapListTmpDetalleComprobante(List<TMP_DETALLE_COMPROBANTE> dtos)
        {
            List<TmpDetalleComprobanteResponseDto> result = new List<TmpDetalleComprobanteResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapTmpDetalleComprobante(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<TmpDetalleComprobanteResponseDto>>> GetAll()
        {

            ResultDto<List<TmpDetalleComprobanteResponseDto>> result = new ResultDto<List<TmpDetalleComprobanteResponseDto>>(null);
            try
            {
                var tmpDetalleComprobante = await _repository.GetAll();
                var cant = tmpDetalleComprobante.Count();
                if (tmpDetalleComprobante != null && tmpDetalleComprobante.Count() > 0)
                {
                    var listDto = await MapListTmpDetalleComprobante(tmpDetalleComprobante);

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
