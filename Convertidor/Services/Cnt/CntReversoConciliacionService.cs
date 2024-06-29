using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;
using Convertidor.Utility;

namespace Convertidor.Services.Cnt
{
    public class CntReversoConciliacionService : ICntReversoConciliacionService
    {
        private readonly ICntReversoConciliacionRepository _repository;

        public CntReversoConciliacionService(ICntReversoConciliacionRepository repository)
        {
            _repository = repository;
        }

        public async Task<CntReversoConciliacionResponseDto> MapCntReversoConciliacion(CNT_REVERSO_CONCILIACION dtos)
        {
            CntReversoConciliacionResponseDto itemResult = new CntReversoConciliacionResponseDto();
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

        public async Task<List<CntReversoConciliacionResponseDto>> MapListCntReversoConciliacion(List<CNT_REVERSO_CONCILIACION> dtos)
        {
            List<CntReversoConciliacionResponseDto> result = new List<CntReversoConciliacionResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapCntReversoConciliacion(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntReversoConciliacionResponseDto>>> GetAllByCodigoConciliacion(int codigoConciliacion)
        {

            ResultDto<List<CntReversoConciliacionResponseDto>> result = new ResultDto<List<CntReversoConciliacionResponseDto>>(null);
            try
            {

                var conciliacion = await _repository.GetByCodigoConciliacion(codigoConciliacion);



                if (conciliacion.Count() > 0)
                {
                    List<CntReversoConciliacionResponseDto> listDto = new List<CntReversoConciliacionResponseDto>();

                    foreach (var item in conciliacion)
                    {
                        var dto = await MapCntReversoConciliacion(item);
                        listDto.Add(dto);
                    }


                    result.Data = listDto;

                    result.IsValid = true;
                    result.Message = "";
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = "No existen Datos";

                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }
    }
}
