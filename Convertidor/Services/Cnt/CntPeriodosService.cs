using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;
using Convertidor.Utility;

namespace Convertidor.Services.Cnt
{
    public class CntPeriodosService : ICntPeriodosService
    {
        private readonly ICntPeriodosRepository _repository;

        public CntPeriodosService(ICntPeriodosRepository repository)
        {
            _repository = repository;
        }

        public async Task<CntPeriodosResponseDto> MapPeriodos(CNT_PERIODOS dtos)
        {
            CntPeriodosResponseDto itemResult = new CntPeriodosResponseDto();
            itemResult.CodigoPeriodo = dtos.CODIGO_PERIODO;
            itemResult.NombrePeriodo = dtos.NOMBRE_PERIODO;
            itemResult.FechaDesde = dtos.FECHA_DESDE;
            itemResult.FechaDesdeString = dtos.FECHA_DESDE.ToString("u");
            FechaDto fechaDesdeObj = FechaObj.GetFechaDto(dtos.FECHA_DESDE);
            itemResult.FechaDesdeObj = (FechaDto)fechaDesdeObj;
            itemResult.FechaHasta = dtos.FECHA_HASTA;
            itemResult.FechaHastaString = dtos.FECHA_HASTA.ToString("u");
            FechaDto FechaHastaObj = FechaObj.GetFechaDto(dtos.FECHA_HASTA);
            itemResult.FechaHastaObj = (FechaDto)FechaHastaObj;
            itemResult.AnoPeriodo = dtos.ANO_PERIODO;
            itemResult.NumeroPeriodo = dtos.NUMERO_PERIODO;
            itemResult.UsuarioPreCierre = dtos.USUARIO_PRECIERRE;
            itemResult.FechaPreCierre = dtos.FECHA_PRECIERRE;
            itemResult.FechaPreCierreString = dtos.FECHA_PRECIERRE.ToString("u");
            FechaDto fechaPreCierreObj = FechaObj.GetFechaDto(dtos.FECHA_PRECIERRE);
            itemResult.FechaPreCierreObj = (FechaDto)fechaPreCierreObj;
            itemResult.UsuarioCierre = dtos.USUARIO_CIERRE;
            itemResult.FechaCierre = dtos.FECHA_CIERRE;
            itemResult.FechaCierreString = dtos.FECHA_CIERRE.ToString("u");
            FechaDto FechaCierreObj = FechaObj.GetFechaDto(dtos.FECHA_CIERRE);
            itemResult.FechaCierreObj = (FechaDto)FechaCierreObj;
            itemResult.UsuarioPreCierreConc = dtos.USUARIO_PRECIERRE_CONC;
            itemResult.FechaPreCierreConc = dtos.FECHA_PRECIERRE_CONC;
            itemResult.FechaPreCierreConcString = dtos.FECHA_PRECIERRE_CONC.ToString("u");
            FechaDto FechaPreCierreConcObj = FechaObj.GetFechaDto(dtos.FECHA_PRECIERRE_CONC);
            itemResult.FechaPreCierreConcObj = (FechaDto)FechaPreCierreConcObj;
            itemResult.UsuarioCierreConc = dtos.USUARIO_CIERRE_CONC;
            itemResult.FechaCierreConc = dtos.FECHA_CIERRE_CONC;
            itemResult.FechaCierreConcString = dtos.FECHA_CIERRE_CONC.ToString();
            FechaDto FechaCierreConcObj = FechaObj.GetFechaDto(dtos.FECHA_CIERRE_CONC);
            itemResult.FechaCierreConcObj = (FechaDto)FechaCierreConcObj;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
   

            return itemResult;

        }

        public async Task<List<CntPeriodosResponseDto>> MapListPeriodos(List<CNT_PERIODOS> dtos)
        {
            List<CntPeriodosResponseDto> result = new List<CntPeriodosResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapPeriodos(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntPeriodosResponseDto>>> GetAll()
        {

            ResultDto<List<CntPeriodosResponseDto>> result = new ResultDto<List<CntPeriodosResponseDto>>(null);
            try
            {
                var periodos = await _repository.GetAll();
                var cant = periodos.Count();
                if (periodos != null && periodos.Count() > 0)
                {
                    var listDto = await MapListPeriodos(periodos);

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
