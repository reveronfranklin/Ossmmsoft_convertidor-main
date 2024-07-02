using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public class CntBalancesService : ICntBalancesService
    {
        private readonly ICntBalancesRepository _repository;

        public CntBalancesService(ICntBalancesRepository repository)
        {
            _repository = repository;
        }

        public async Task<CntBalancesResponseDto> MapBalances(CNT_BALANCES dtos)
        {
            CntBalancesResponseDto itemResult = new CntBalancesResponseDto();
            itemResult.CodigoBalance = dtos.CODIGO_BALANCE;
            itemResult.NumeroBalance = dtos.NUMERO_BALANCE;
            itemResult.Denominacion = dtos.DENOMINACION;
            itemResult.Descripcion = dtos.DESCRIPCION;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.CodigoRubro = dtos.CODIGO_RUBRO;

            return itemResult;

        }

        public async Task<List<CntBalancesResponseDto>> MapListBalances(List<CNT_BALANCES> dtos)
        {
            List<CntBalancesResponseDto> result = new List<CntBalancesResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapBalances(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntBalancesResponseDto>>> GetAll()
        {

            ResultDto<List<CntBalancesResponseDto>> result = new ResultDto<List<CntBalancesResponseDto>>(null);
            try
            {
                var balances = await _repository.GetAll();
                var cant = balances.Count();
                if (balances != null && balances.Count() > 0)
                {
                    var listDto = await MapListBalances(balances);

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
