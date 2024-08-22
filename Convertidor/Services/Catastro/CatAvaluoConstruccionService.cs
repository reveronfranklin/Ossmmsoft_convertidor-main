using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Data.Repository.Catastro;
using Convertidor.Dtos.Catastro;
using Convertidor.Utility;

namespace Convertidor.Services.Catastro
{
    public class CatAvaluoConstruccionService : ICatAvaluoConstruccionService
    {
        private readonly ICatAvaluoConstruccionRepository _repository;

        public CatAvaluoConstruccionService(ICatAvaluoConstruccionRepository repository)
        {
            _repository = repository;
        }

        public async Task<CatAvaluoConstruccionResponseDto> MapCatAvaluoConstruccion(CAT_AVALUO_CONSTRUCCION entity)
        {
            CatAvaluoConstruccionResponseDto dto = new CatAvaluoConstruccionResponseDto();

            dto.CodigoAvaluoConstruccion = entity.CODIGO_AVALUO_CONSTRUCCION;
            dto.CodigoFicha = entity.CODIGO_FICHA;
            dto.AnoAvaluo = entity.ANO_AVALUO;
            dto.AnoAvaluoString = entity.ANO_AVALUO.ToString("u");
            FechaDto anoAvaluoObj = FechaObj.GetFechaDto(entity.ANO_AVALUO);
            dto.AnoAvaluoObj = (FechaDto)anoAvaluoObj;
            dto.PlantaId = entity.PLANTA_ID;
            dto.UnidadMedidaId = entity.UNIDAD_MEDIDA_ID;
            dto.ValorMedida = entity.VALOR_MEDIDA;
            dto.FactorDePreciacion = entity.FACTOR_DEPRECIACION;
            dto.ValorModificado = entity.VALOR_MODIFICADO;
            dto.AreaTotal = entity.AREA_TOTAL;
            dto.MontoAvaluo = entity.MONTO_AVALUO;
            dto.Observaciones = entity.OBSERVACIONES;
            dto.ValorReposicion = entity.VALOR_REPOSICION;
            dto.AreaConstruccion = entity.AREA_CONSTRUCCION;
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
            dto.Tipologia = entity.TIPOLOGIA;
            dto.FrenteParcela = entity.FRENTE_PARCELA;
            dto.MontoComplemento = entity.MONTO_COMPLEMENTO;
            dto.MontoComplementoUsuario = entity.MONTO_COMPLEMENTO_USUARIO;
            dto.MontoTotalAvaluo = entity.MONTO_AVALUO;


            return dto;

        }

        public async Task<List<CatAvaluoConstruccionResponseDto>> MapListCatAvaluoConstruccion(List<CAT_AVALUO_CONSTRUCCION> dtos)
        {
            List<CatAvaluoConstruccionResponseDto> result = new List<CatAvaluoConstruccionResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapCatAvaluoConstruccion(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CatAvaluoConstruccionResponseDto>>> GetAll()
        {

            ResultDto<List<CatAvaluoConstruccionResponseDto>> result = new ResultDto<List<CatAvaluoConstruccionResponseDto>>(null);
            try
            {
                var avaluoContruccion = await _repository.GetAll();
                var cant = avaluoContruccion.Count();
                if (avaluoContruccion != null && avaluoContruccion.Count() > 0)
                {
                    var listDto = await MapListCatAvaluoConstruccion(avaluoContruccion);

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
