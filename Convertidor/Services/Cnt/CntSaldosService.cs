using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;
using NuGet.Protocol.Core.Types;

namespace Convertidor.Services.Cnt
{
    public class CntSaldosService : ICntSaldosService
    {
        private readonly ICntSaldosRepository _repository;

        public CntSaldosService(ICntSaldosRepository repository)
        {
            _repository = repository;
        }

        public async Task<CntSaldosResponseDto> MapSaldos(CNT_SALDOS dtos)
        {
            CntSaldosResponseDto itemResult = new CntSaldosResponseDto();
            itemResult.CodigoSaldo = dtos.CODIGO_SALDO;
            itemResult.CodigoPeriodo = dtos.CODIGO_PERIODO;
            itemResult.CodigoMayor = dtos.CODIGO_MAYOR;
            itemResult.CodigoAuxiliar = dtos.CODIGO_AUXILIAR;
            itemResult.Debitos = dtos.DEBITOS;
            itemResult.Creditos = dtos.CREDITOS;
            itemResult.Monto = dtos.MONTO;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;


            return itemResult;

        }

        public async Task<List<CntSaldosResponseDto>> MapListSaldos(List<CNT_SALDOS> dtos)
        {
            List<CntSaldosResponseDto> result = new List<CntSaldosResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapSaldos(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntSaldosResponseDto>>> GetAll()
        {

            ResultDto<List<CntSaldosResponseDto>> result = new ResultDto<List<CntSaldosResponseDto>>(null);
            try
            {
                var saldos = await _repository.GetAll();
                var cant = saldos.Count();
                if (saldos != null && saldos.Count() > 0)
                {
                    var listDto = await MapListSaldos(saldos);

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
