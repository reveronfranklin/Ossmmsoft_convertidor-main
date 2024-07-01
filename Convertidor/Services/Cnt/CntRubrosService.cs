using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public class CntRubrosService : ICntRubrosService
    {
        private readonly ICntRubrosRepository _repository;

        public CntRubrosService(ICntRubrosRepository repository)
        {
            _repository = repository;
        }

        public async Task<CntRubrosResponseDto> MapRubros(CNT_RUBROS dtos)
        {
            CntRubrosResponseDto itemResult = new CntRubrosResponseDto();
            itemResult.CodigoRubro = dtos.CODIGO_RUBRO;
            itemResult.Denominacion = dtos.DENOMINACION;
            itemResult.Descripcion = dtos.DESCRIPCION;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.Extra4 = dtos.EXTRA4;
            itemResult.Extra5 = dtos.EXTRA5;
            itemResult.Extra6 = dtos.EXTRA6;
            itemResult.Extra7 = dtos.EXTRA7;
            itemResult.Extra8 = dtos.EXTRA8;
            itemResult.Extra9 = dtos.EXTRA9;
            itemResult.Extra10 = dtos.EXTRA10;
            itemResult.Extra11 = dtos.EXTRA11;
            itemResult.Extra12 = dtos.EXTRA12;
            itemResult.Extra13 = dtos.EXTRA13;
            itemResult.Extra14 = dtos.EXTRA14;
            itemResult.Extra15 = dtos.EXTRA15;

            return itemResult;

        }

        public async Task<List<CntRubrosResponseDto>> MapListRubros(List<CNT_RUBROS> dtos)
        {
            List<CntRubrosResponseDto> result = new List<CntRubrosResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapRubros(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntRubrosResponseDto>>> GetAll()
        {

            ResultDto<List<CntRubrosResponseDto>> result = new ResultDto<List<CntRubrosResponseDto>>(null);
            try
            {
                var rubros = await _repository.GetAll();
                var cant = rubros.Count();
                if (rubros != null && rubros.Count() > 0)
                {
                    var listDto = await MapListRubros(rubros);

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
