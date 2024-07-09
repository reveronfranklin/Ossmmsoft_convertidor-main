using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public class CntTmpHistAnaliticoService : ICntTmpHistAnaliticoService
    {
        private readonly ICntTmpHistAnaliticoRepository _repository;

        public CntTmpHistAnaliticoService(ICntTmpHistAnaliticoRepository repository)
        {
            _repository = repository;
        }

        public async Task<CntTmpHistAnaliticoResponseDto> MapTmpHistAnalitico(CNT_TMP_HIST_ANALITICO dtos)
        {
            CntTmpHistAnaliticoResponseDto itemResult = new CntTmpHistAnaliticoResponseDto();
            itemResult.CodigoHistAnalitico = dtos.CODIGO_HIST_ANALITICO;
            itemResult.CodigoSaldo = dtos.CODIGO_SALDO;
            itemResult.CodigoDetalleComprobante = dtos.CODIGO_DETALLE_COMPROBANTE;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;


            return itemResult;

        }

        public async Task<List<CntTmpHistAnaliticoResponseDto>> MapListTmpHistAnalitico(List<CNT_TMP_HIST_ANALITICO> dtos)
        {
            List<CntTmpHistAnaliticoResponseDto> result = new List<CntTmpHistAnaliticoResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapTmpHistAnalitico(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntTmpHistAnaliticoResponseDto>>> GetAll()
        {

            ResultDto<List<CntTmpHistAnaliticoResponseDto>> result = new ResultDto<List<CntTmpHistAnaliticoResponseDto>>(null);
            try
            {
                var tmpHistAnalitico = await _repository.GetAll();
                var cant = tmpHistAnalitico.Count();
                if (tmpHistAnalitico != null && tmpHistAnalitico.Count() > 0)
                {
                    var listDto = await MapListTmpHistAnalitico(tmpHistAnalitico);

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
