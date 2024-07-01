using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;
using Convertidor.Utility;

namespace Convertidor.Services.Cnt
{
    public class CntTmpConciliacionService : ICntTmpConciliacionService
    {
        private readonly ICntTmpConciliacionRepository _repository;
        private readonly ICntDetalleLibroService _cntDetalleLibroService;
        private readonly ICntDetalleEdoCtaService _cntDetalleEdoCtaService;

        public CntTmpConciliacionService(ICntTmpConciliacionRepository repository,
                                          ICntDetalleLibroService cntDetalleLibroService,
                                          ICntDetalleEdoCtaService cntDetalleEdoCtaService,
                                          ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _cntDetalleLibroService = cntDetalleLibroService;
            _cntDetalleEdoCtaService = cntDetalleEdoCtaService;
        }

        public async Task<CntTmpConciliacionResponseDto> MapCntTmpConciliacion(CNT_TMP_CONCILIACION dtos)
        {
            CntTmpConciliacionResponseDto itemResult = new CntTmpConciliacionResponseDto();
            itemResult.CodigoTmpConciliacion = dtos.CODIGO_TMP_CONCILIACION;
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

        public async Task<List<CntTmpConciliacionResponseDto>> MapListCntTmpConciliacion(List<CNT_TMP_CONCILIACION> dtos)
        {
            List<CntTmpConciliacionResponseDto> result = new List<CntTmpConciliacionResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapCntTmpConciliacion(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntTmpConciliacionResponseDto>>> GetAll()
        {

            ResultDto<List<CntTmpConciliacionResponseDto>> result = new ResultDto<List<CntTmpConciliacionResponseDto>>(null);
            try
            {
                var tmpConciliacion = await _repository.GetAll();
                var cant = tmpConciliacion.Count();
                if (tmpConciliacion != null && tmpConciliacion.Count() > 0)
                {
                    var listDto = await MapListCntTmpConciliacion(tmpConciliacion);

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
