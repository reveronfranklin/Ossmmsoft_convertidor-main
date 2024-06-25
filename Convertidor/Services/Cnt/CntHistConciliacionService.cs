using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;
using Convertidor.Utility;

namespace Convertidor.Services.Cnt
{
    public class CntHistConciliacionService : ICntHistConciliacionService
    {
        private readonly ICntHistConciliacionRepository _repository;

        public CntHistConciliacionService(ICntHistConciliacionRepository repository)
        {
            _repository = repository;
        }




        public async Task<CntHistConciliacionResponseDto> MapCntHistConciliacion(CNT_HIST_CONCILIACION dtos)
        {
            CntHistConciliacionResponseDto itemResult = new CntHistConciliacionResponseDto();
            itemResult.CodigoHistConciliacion = dtos.CODIGO_HIST_CONCILIACION;
            itemResult.CodigoConciliacion = dtos.CODIGO_CONCILIACION;
            itemResult.CodigoPeriodo = dtos.CODIGO_PERIODO;
            itemResult.CodigoCuentaBanco = dtos.CODIGO_CUENTA_BANCO;
            itemResult.CodigoDetalleLibro = dtos.CODIGO_DETALLE_LIBRO;
            itemResult.CodigoDetalleEdoCta = dtos.CODIGO_DETALLE_EDO_CTA;
            itemResult.Fecha = dtos.FECHA;
            itemResult.FechaString = dtos.FECHA.ToString("u");
            FechaDto fechaObj = FechaObj.GetFechaDto(dtos.FECHA);
            itemResult.FechaObj = (FechaDto)fechaObj;
            itemResult.Numero = dtos.NUMERO;
            itemResult.Monto = dtos.MONTO;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;


            return itemResult;
        }

        public async Task<List<CntHistConciliacionResponseDto>> MapListCntHistoricoConciliacion(List<CNT_HIST_CONCILIACION> dtos)
        {
            List<CntHistConciliacionResponseDto> result = new List<CntHistConciliacionResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapCntHistConciliacion(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntHistConciliacionResponseDto>>> GetAll()
        {

            ResultDto<List<CntHistConciliacionResponseDto>> result = new ResultDto<List<CntHistConciliacionResponseDto>>(null);
            try
            {
                var historicoConciliacion = await _repository.GetAll();
                var cant = historicoConciliacion.Count();
                if (historicoConciliacion != null && historicoConciliacion.Count() > 0)
                {
                    var listDto = await MapListCntHistoricoConciliacion(historicoConciliacion);

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