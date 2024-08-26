using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Dtos.Catastro;
using Convertidor.Utility;

namespace Convertidor.Services.Catastro
{
    public class CatCalcXTriangulacionService : ICatCalcXTriangulacionService
    {
        private readonly ICatCalcXTriangulacionRepository _repository;

        public CatCalcXTriangulacionService(ICatCalcXTriangulacionRepository repository)
        {
            _repository = repository;
        }

        public async Task<CatCalcXTriangulacionResponseDto> MapCalcXTriangulacion(CAT_CALC_X_TRIANGULACION entity)
        {
            CatCalcXTriangulacionResponseDto dto = new CatCalcXTriangulacionResponseDto();

            dto.CodigoTriangulacion = entity.CODIGO_TRIANGULACION;
            dto.CodigoFicha = entity.CODIGO_FICHA;
            dto.CodigoAvaluoConstruccion = entity.CODIGO_AVALUO_CONSTRUCCION;
            dto.CatetoA = entity.CATETO_A;
            dto.CatetoB = entity.CATETO_B;
            dto.CatetoC = entity.CATETO_C;
            dto.AreaParcial = entity.AREA_PARCIAL;
            dto.AreaComplementaria = entity.AREA_COMPLEMENTARIA;
            dto.Observaciones = entity.OBSERVACIONES;
            dto.Extra1 = entity.EXTRA1;
            dto.Extra2 = entity.EXTRA2;
            dto.Extra3 = entity.EXTRA3;
            dto.Extra4 = entity.EXTRA4;
            dto.Extra5 = entity.EXTRA5;
            dto.Extra6 = entity.EXTRA6;
            dto.Extra7 = entity.EXTRA7;
            dto.Extra8 = entity.EXTRA8;
            dto.Extra9 = entity.EXTRA9;
            dto.Extra10 = entity.EXTRA10;
            dto.Extra11 = entity.EXTRA11;
            dto.Extra12 = entity.EXTRA12;
            dto.Extra13 = entity.EXTRA13;
            dto.Extra14 = entity.EXTRA14;
            dto.Extra15 = entity.EXTRA15;


            return dto;

        }

        public async Task<List<CatCalcXTriangulacionResponseDto>> MapListCalcXTriangulacion(List<CAT_CALC_X_TRIANGULACION> dtos)
        {
            List<CatCalcXTriangulacionResponseDto> result = new List<CatCalcXTriangulacionResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapCalcXTriangulacion(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CatCalcXTriangulacionResponseDto>>> GetAll()
        {

            ResultDto<List<CatCalcXTriangulacionResponseDto>> result = new ResultDto<List<CatCalcXTriangulacionResponseDto>>(null);
            try
            {
                var calcTriangulacion = await _repository.GetAll();
                var cant = calcTriangulacion.Count();
                if (calcTriangulacion != null && calcTriangulacion.Count() > 0)
                {
                    var listDto = await MapListCalcXTriangulacion(calcTriangulacion);

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
