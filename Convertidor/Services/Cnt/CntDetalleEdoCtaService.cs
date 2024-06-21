using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;
namespace Convertidor.Services.Cnt
{
    public class CntDetalleEdoCtaService : ICntDetalleEdoCtaService
    {
        private readonly ICntDetalleEdoCtaRepository _repository;

        public CntDetalleEdoCtaService(ICntDetalleEdoCtaRepository repository)
        {
            _repository = repository;
        }

        public FechaDto GetFechaDto(DateTime fecha)
        {
            var FechaDesdeObj = new FechaDto();
            FechaDesdeObj.Year = fecha.Year.ToString();
            string month = "00" + fecha.Month.ToString();
            string day = "00" + fecha.Day.ToString();
            FechaDesdeObj.Month = month.Substring(month.Length - 2);
            FechaDesdeObj.Day = day.Substring(day.Length - 2);

            return FechaDesdeObj;
        }

        public async Task<CntDetalleEdoCtaResponseDto> MapDetalleEdoCuenta(CNT_DETALLE_EDO_CTA dtos) 
        {
           CntDetalleEdoCtaResponseDto itemResult = new CntDetalleEdoCtaResponseDto();
            itemResult.CodigoDetalleEdoCta = dtos.CODIGO_DETALLE_EDO_CTA;
            itemResult.CodigoEstadoCuenta = dtos.CODIGO_ESTADO_CUENTA;
            itemResult.TipoTransaccionId = dtos.TIPO_TRANSACCION_ID;
            itemResult.NumeroTransaccion = dtos.NUMERO_TRANSACCION;
            itemResult.FechaTransaccion = dtos.FECHA_TRANSACCION;
            itemResult.FechaTransaccionString = dtos.FECHA_TRANSACCION.ToString("u");
            FechaDto fechaTransaccionObj = GetFechaDto(dtos.FECHA_TRANSACCION);
            itemResult.FechaTransaccionObj =(FechaDto)fechaTransaccionObj;
            itemResult.Descripcion = dtos.DESCRIPCION;
            itemResult.Monto = dtos.MONTO;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.Status = dtos.STATUS;

            return itemResult;

        }

        public async Task<List<CntDetalleEdoCtaResponseDto>> MapListDetalleEdoCta(List<CNT_DETALLE_EDO_CTA> dtos)
        {
            List<CntDetalleEdoCtaResponseDto> result = new List<CntDetalleEdoCtaResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapDetalleEdoCuenta(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntDetalleEdoCtaResponseDto>>> GetAll()
        {

            ResultDto<List<CntDetalleEdoCtaResponseDto>> result = new ResultDto<List<CntDetalleEdoCtaResponseDto>>(null);
            try
            {
                var detalleEdoCta = await _repository.GetAll();
                var cant = detalleEdoCta.Count();
                if (detalleEdoCta != null && detalleEdoCta.Count() > 0)
                {
                    var listDto = await MapListDetalleEdoCta(detalleEdoCta);

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
