using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public class CntMayoresService : ICntMayoresService
    {
        private readonly ICntMayoresRepository _repository;

        public CntMayoresService(ICntMayoresRepository repository)
        {
            _repository = repository;
        }

        public async Task<CntMayoresResponseDto> MapMayores(CNT_MAYORES dtos)
        {
            CntMayoresResponseDto itemResult = new CntMayoresResponseDto();
            itemResult.CodigoMayor = dtos.CODIGO_MAYOR;
            itemResult.NumeroMayor = dtos.NUMERO_MAYOR;
            itemResult.Denominacion = dtos.DENOMINACION;
            itemResult.Descripcion = dtos.DESCRIPCION;
            itemResult.CodigoBalance = dtos.CODIGO_BALANCE;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
           
            return itemResult;

        }

        public async Task<List<CntMayoresResponseDto>> MapListMayores(List<CNT_MAYORES> dtos)
        {
            List<CntMayoresResponseDto> result = new List<CntMayoresResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapMayores(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntMayoresResponseDto>>> GetAll()
        {

            ResultDto<List<CntMayoresResponseDto>> result = new ResultDto<List<CntMayoresResponseDto>>(null);
            try
            {
                var mayores = await _repository.GetAll();
                var cant = mayores.Count();
                if (mayores != null && mayores.Count() > 0)
                {
                    var listDto = await MapListMayores(mayores);

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
