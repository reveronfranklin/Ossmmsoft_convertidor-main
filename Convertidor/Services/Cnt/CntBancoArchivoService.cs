using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public class CntBancoArchivoService : ICntBancoArchivoService
    {
        private readonly ICntBancoArchivoRepository _repository;

        public CntBancoArchivoService(ICntBancoArchivoRepository repository)
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
        public async Task<CntBancoArchivoResponseDto> MapCntBancoArchivo(CNT_BANCO_ARCHIVO dtos) 
        {
           CntBancoArchivoResponseDto itemResult = new CntBancoArchivoResponseDto();
            itemResult.CodigoBancoArchivo = dtos.CODIGO_BANCO_ARCHIVO;
            itemResult.CodigoBancoArchivoControl = dtos.CODIGO_BANCO_ARCHIVO_CONTROL;
            itemResult.NumeroBanco = dtos.NUMERO_BANCO;
            itemResult.NumeroCuenta = dtos.NUMERO_CUENTA;
            itemResult.FechaTransaccion = dtos.FECHA_TRANSACCION;
            itemResult.FechaTransaccionString = dtos.FECHA_TRANSACCION.ToString("u");
            FechaDto fechaTransaccionObj = GetFechaDto(dtos.FECHA_TRANSACCION);
            itemResult.FechaTransaccionObj = (FechaDto)fechaTransaccionObj;
            itemResult.NumeroTransaccion = dtos.NUMERO_TRANSACCION;
            itemResult.TipoTransaccionId = dtos.TIPO_TRANSACCION_ID;
            itemResult.TipoTransaccion = dtos.TIPO_TRANSACCION;
            itemResult.DescripcionTransaccion = dtos.DESCRIPCION_TRANSACCION;
            itemResult.MontoTransaccion = dtos.MONTO_TRANSACCION;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.CodigoDetalleEdoCta = dtos.CODIGO_DETALLE_EDO_CTA;
        
            return itemResult;
        }

        public async Task<List<CntBancoArchivoResponseDto>> MapListCntBancoArchivo(List<CNT_BANCO_ARCHIVO> dtos)
        {
            List<CntBancoArchivoResponseDto> result = new List<CntBancoArchivoResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapCntBancoArchivo(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntBancoArchivoResponseDto>>> GetAll()
        {

            ResultDto<List<CntBancoArchivoResponseDto>> result = new ResultDto<List<CntBancoArchivoResponseDto>>(null);
            try
            {
                var bancoArchivo = await _repository.GetAll();
                var cant = bancoArchivo.Count();
                if (bancoArchivo != null && bancoArchivo.Count() > 0)
                {
                    var listDto = await MapListCntBancoArchivo(bancoArchivo);

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
