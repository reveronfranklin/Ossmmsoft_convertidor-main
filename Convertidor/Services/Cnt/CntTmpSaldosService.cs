using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public class CntTmpSaldosService : ICntTmpSaldosService
    {
        private readonly ICntTmpSaldosRepository _repository;

        public CntTmpSaldosService(ICntTmpSaldosRepository repository)
        {
            _repository = repository;
        }

        public async Task<CntTmpSaldosResponseDto> MapTmpSaldos(CNT_TMP_SALDOS dtos)
        {
            CntTmpSaldosResponseDto itemResult = new CntTmpSaldosResponseDto();
            itemResult.CodigoTmpSaldo = dtos.CODIGO_TMP_SALDO;
            itemResult.CodigoPeriodo = dtos.CODIGO_PERIODO;
            itemResult.CodigoMayor = dtos.CODIGO_MAYOR;
            itemResult.CodigoAuxiliar = dtos.CODIGO_AUXILIAR;
            itemResult.SaldoInicial = dtos.SALDO_INICIAL;
            itemResult.Debitos = dtos.DEBITOS;
            itemResult.Creditos = dtos.CREDITOS;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;


            return itemResult;

        }

        public async Task<List<CntTmpSaldosResponseDto>> MapListTmpSaldos(List<CNT_TMP_SALDOS> dtos)
        {
            List<CntTmpSaldosResponseDto> result = new List<CntTmpSaldosResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapTmpSaldos(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntTmpSaldosResponseDto>>> GetAll()
        {

            ResultDto<List<CntTmpSaldosResponseDto>> result = new ResultDto<List<CntTmpSaldosResponseDto>>(null);
            try
            {
                var tmpSaldos = await _repository.GetAll();
                var cant = tmpSaldos.Count();
                if (tmpSaldos != null && tmpSaldos.Count() > 0)
                {
                    var listDto = await MapListTmpSaldos(tmpSaldos);

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
