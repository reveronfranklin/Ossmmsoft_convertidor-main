using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;
using Convertidor.Utility;

namespace Convertidor.Services.Cnt
{
    public class TmpAuxiliaresService : ITmpAuxiliaresService
    {
        private readonly ITmpAuxiliaresRepository _repository;

        public TmpAuxiliaresService(ITmpAuxiliaresRepository repository)
        {
            _repository = repository;
        }

        public async Task<TmpAuxiliaresResponseDto> MapTmpAuxiliares(TMP_AUXILIARES dtos)
        {
            TmpAuxiliaresResponseDto itemResult = new TmpAuxiliaresResponseDto();
            itemResult.CodigoAuxiliar = dtos.CODIGO_AUXILIAR;
            itemResult.CodigoMayor = dtos.CODIGO_MAYOR;
            itemResult.Segmento1 = dtos.SEGMENTO1;
            itemResult.Segmento2 = dtos.SEGMENTO2;
            itemResult.Segmento3 = dtos.SEGMENTO3;
            itemResult.Segmento4 = dtos.SEGMENTO4;
            itemResult.Segmento5 = dtos.SEGMENTO5;
            itemResult.Segmento6 = dtos.SEGMENTO6;
            itemResult.Segmento7 = dtos.SEGMENTO7;
            itemResult.Segmento8 = dtos.SEGMENTO8;
            itemResult.Segmento10 = dtos.SEGMENTO10;
            itemResult.Denominacion = dtos.DENOMINACION;
            itemResult.Descripcion = dtos.DESCRIPCION;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
           

            return itemResult;

        }

        public async Task<List<TmpAuxiliaresResponseDto>> MapListTmpAuxiliares(List<TMP_AUXILIARES> dtos)
        {
            List<TmpAuxiliaresResponseDto> result = new List<TmpAuxiliaresResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapTmpAuxiliares(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<TmpAuxiliaresResponseDto>>> GetAll()
        {

            ResultDto<List<TmpAuxiliaresResponseDto>> result = new ResultDto<List<TmpAuxiliaresResponseDto>>(null);
            try
            {
                var tmpAuxiliares = await _repository.GetAll();
                var cant = tmpAuxiliares.Count();
                if (tmpAuxiliares != null && tmpAuxiliares.Count() > 0)
                {
                    var listDto = await MapListTmpAuxiliares(tmpAuxiliares);

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
