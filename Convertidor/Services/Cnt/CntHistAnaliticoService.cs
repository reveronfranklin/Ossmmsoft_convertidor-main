using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public class CntHistAnaliticoService : ICntHistAnaliticoService
    {
        private readonly ICntHistAnaliticoRepository _repository;

        public CntHistAnaliticoService(ICntHistAnaliticoRepository repository)
        {
            _repository = repository;
        }

        public async Task<CntHistAnaliticoResponseDto> MapHistAnalitico(CNT_HIST_ANALITICO dtos)
        {
            CntHistAnaliticoResponseDto itemResult = new CntHistAnaliticoResponseDto();
            itemResult.CodigoHistAnalitico = dtos.CODIGO_HIST_ANALITICO;
            itemResult.CodigoSaldo = dtos.CODIGO_SALDO;
            itemResult.CodigoDetalleComprobante = dtos.CODIGO_DETALLE_COMPROBANTE;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;


            return itemResult;

        }

        public async Task<List<CntHistAnaliticoResponseDto>> MapListHistAnalitico(List<CNT_HIST_ANALITICO> dtos)
        {
            List<CntHistAnaliticoResponseDto> result = new List<CntHistAnaliticoResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapHistAnalitico(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntHistAnaliticoResponseDto>>> GetAll()
        {

            ResultDto<List<CntHistAnaliticoResponseDto>> result = new ResultDto<List<CntHistAnaliticoResponseDto>>(null);
            try
            {
                var histAnalitico = await _repository.GetAll();
                var cant = histAnalitico.Count();
                if (histAnalitico != null && histAnalitico.Count() > 0)
                {
                    var listDto = await MapListHistAnalitico(histAnalitico);

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
