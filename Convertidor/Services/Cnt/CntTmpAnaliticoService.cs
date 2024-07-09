using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public class CntTmpAnaliticoService : ICntTmpAnaliticoService
    {
        private readonly ICntTmpAnaliticoRepository _repository;

        public CntTmpAnaliticoService(ICntTmpAnaliticoRepository repository)
        {
            _repository = repository;
        }

        public async Task<CntTmpAnaliticoResponseDto> MapTmpAnalitico(CNT_TMP_ANALITICO dtos)
        {
            CntTmpAnaliticoResponseDto itemResult = new CntTmpAnaliticoResponseDto();
            itemResult.CodigoTmpAnalitico = dtos.CODIGO_TMP_ANALITICO;
            itemResult.CodigoTmpSaldo = dtos.CODIGO_TMP_SALDO;
            itemResult.CodigoDetalleComprobante = dtos.CODIGO_DETALLE_COMPROBANTE;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;


            return itemResult;

        }

        public async Task<List<CntTmpAnaliticoResponseDto>> MapListTmpAnalitico(List<CNT_TMP_ANALITICO> dtos)
        {
            List<CntTmpAnaliticoResponseDto> result = new List<CntTmpAnaliticoResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapTmpAnalitico(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntTmpAnaliticoResponseDto>>> GetAll()
        {

            ResultDto<List<CntTmpAnaliticoResponseDto>> result = new ResultDto<List<CntTmpAnaliticoResponseDto>>(null);
            try
            {
                var tmpAnalitico = await _repository.GetAll();
                var cant = tmpAnalitico.Count();
                if (tmpAnalitico != null && tmpAnalitico.Count() > 0)
                {
                    var listDto = await MapListTmpAnalitico(tmpAnalitico);

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
