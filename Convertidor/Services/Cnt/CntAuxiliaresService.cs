using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;
using Convertidor.Utility;

namespace Convertidor.Services.Cnt
{
    public class CntAuxiliaresService : ICntAuxiliaresService
    {
        private readonly ICntAuxiliaresRepository _repository;

        public CntAuxiliaresService(ICntAuxiliaresRepository repository)
        {
            _repository = repository;
        }

        public async Task<CntAuxiliaresResponseDto> MapAuxiliares(CNT_AUXILIARES dtos)
        {
            CntAuxiliaresResponseDto itemResult = new CntAuxiliaresResponseDto();
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
            itemResult.FechaFinVigencia = dtos.FECHA_FIN_VIGENCIA;
            itemResult.FechaFinVigencistring = dtos.FECHA_FIN_VIGENCIA.ToString("u");
            FechaDto fechaFinVigenciaObj =FechaObj.GetFechaDto(dtos.FECHA_FIN_VIGENCIA);
            itemResult.FechaFinVigenciaObj = (FechaDto)fechaFinVigenciaObj;
            itemResult.CodigoProveedor = dtos.CODIGO_PROVEEDOR;

            return itemResult;

        }

        public async Task<List<CntAuxiliaresResponseDto>> MapListAuxiliares(List<CNT_AUXILIARES> dtos)
        {
            List<CntAuxiliaresResponseDto> result = new List<CntAuxiliaresResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapAuxiliares(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntAuxiliaresResponseDto>>> GetAll()
        {

            ResultDto<List<CntAuxiliaresResponseDto>> result = new ResultDto<List<CntAuxiliaresResponseDto>>(null);
            try
            {
                var auxiliares = await _repository.GetAll();
                var cant = auxiliares.Count();
                if (auxiliares != null && auxiliares.Count() > 0)
                {
                    var listDto = await MapListAuxiliares(auxiliares);

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
