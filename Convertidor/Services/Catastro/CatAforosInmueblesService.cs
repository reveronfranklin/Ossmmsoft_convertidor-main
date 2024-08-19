using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Data.Repository.Catastro;
using Convertidor.Dtos.Catastro;
using Convertidor.Dtos.Cnt;
using Convertidor.Utility;

namespace Convertidor.Services.Catastro
{
    public class CatAforosInmueblesService : ICatAforosInmueblesService
    {
        private readonly ICatAforosInmueblesRepository _repository;

        public CatAforosInmueblesService(ICatAforosInmueblesRepository repository)
        {
            _repository = repository;
        }

        public async Task<CatAforosInmueblesResponseDto> MapCatAforosInmuebles(CAT_AFOROS_INMUEBLES entity)
        {
            CatAforosInmueblesResponseDto dto = new CatAforosInmueblesResponseDto();
            dto.CodigoAforoInmueble = entity.CODIGO_AFORO_INMUEBLE;
            dto.Tributo = entity.TRIBUTO;
            dto.CodigoInmueble = entity.CODIGO_INMUEBLE;
            dto.Monto = entity.MONTO;
            dto.MontoMinimo = entity.MONTO_MINIMO;
            dto.CodigoFormaLiquidacion = entity.CODIGO_FORMA_LIQUIDACION;
            dto.CodigoFormaLiqMinimo = entity.CODIGO_FORMA_LIQ_MINIMO;
            dto.FechaLiquidacion = entity.FECHA_LIQUIDACION;
            dto.FechaLiquidacionString = entity.FECHA_LIQUIDACION.ToString("u");
            FechaDto fechaLiquidacionObj = FechaObj.GetFechaDto(entity.FECHA_LIQUIDACION);
            dto.FechaLiquidacionObj = (FechaDto)fechaLiquidacionObj;
            dto.FechaPeriodoIni = entity.FECHA_PERIODO_INI;
            dto.FechaPeriodoIniString = entity.FECHA_PERIODO_INI.ToString("u");
            FechaDto fechaPeriodoIniObj = FechaObj.GetFechaDto(entity.FECHA_PERIODO_INI);
            dto.FechaPeriodoIniObj = (FechaDto)fechaPeriodoIniObj;
            dto.FechaPeriodoFin = entity.FECHA_PERIODO_FIN;
            dto.FechaPeriodoFinString = entity.FECHA_PERIODO_FIN.ToString("u");
            FechaDto fechaPeriodoFinObj = FechaObj.GetFechaDto(entity.FECHA_PERIODO_FIN);
            dto.FechaPeriodoFinObj = (FechaDto)fechaPeriodoFinObj;
            dto.AplicadoId = entity.APLICADO_ID;
            dto.CodigoAplicado = entity.CODIGO_APLICADO;
            dto.Estatus = entity.ESTATUS;
            dto.Extra1 = entity.EXTRA1;
            dto.Extra2 = entity.EXTRA2;
            dto.Extra3 = entity.EXTRA3;
            dto.FechaInicioExonera = entity.FECHA_INICIO_EXONERA;
            dto.FechaInicioExoneraString = entity.FECHA_INICIO_EXONERA.ToString("u");
            FechaDto fechaInicioExonera = FechaObj.GetFechaDto(entity.FECHA_INICIO_EXONERA);
            dto.FechaInicioExoneraObj = (FechaDto)fechaInicioExonera;
            dto.FechaFinExonera = entity.FECHA_FIN_EXONERA;
            dto.FechaFinExoneraString = entity.FECHA_FIN_EXONERA.ToString("u");
            FechaDto fechaFinExonera = FechaObj.GetFechaDto(entity.FECHA_FIN_EXONERA);
            dto.FechaFinExoneraObj = (FechaDto)fechaFinExonera;
            dto.Observacion = entity.OBSERVACION;
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
            dto.Codigoficha = entity.CODIGO_FICHA;
            dto.CodigoAvaluoConstruccion = entity.CODIGO_AVALUO_CONSTRUCCION;
            dto.CodigoAvaluoTerreno = entity.CODIGO_AVALUO_TERRENO;

            return dto;

        }

        public async Task<List<CatAforosInmueblesResponseDto>> MapListCatAforosInmuebles(List<CAT_AFOROS_INMUEBLES> dtos)
        {
            List<CatAforosInmueblesResponseDto> result = new List<CatAforosInmueblesResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapCatAforosInmuebles(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CatAforosInmueblesResponseDto>>> GetAll()
        {

            ResultDto<List<CatAforosInmueblesResponseDto>> result = new ResultDto<List<CatAforosInmueblesResponseDto>>(null);
            try
            {
                var aforosInmuebles = await _repository.GetAll();
                var cant = aforosInmuebles.Count();
                if (aforosInmuebles != null && aforosInmuebles.Count() > 0)
                {
                    var listDto = await MapListCatAforosInmuebles(aforosInmuebles);

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
