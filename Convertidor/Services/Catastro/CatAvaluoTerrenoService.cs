using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Dtos.Catastro;
using Convertidor.Utility;

namespace Convertidor.Services.Catastro
{
    public class CatAvaluoTerrenoService : ICatAvaluoTerrenoService
    {
        private readonly ICatAvaluoTerrenoRepository _repository;

        public CatAvaluoTerrenoService(ICatAvaluoTerrenoRepository repository)
        {
            _repository = repository;
        }

        public async Task<CatAvaluoTerrenoResponseDto> MapCatAvaluoTerreno(CAT_AVALUO_TERRENO entity)
        {
            CatAvaluoTerrenoResponseDto dto = new CatAvaluoTerrenoResponseDto();

            dto.CodigoAvaluoTerreno = entity.CODIGO_AVALUO_TERRENO;
            dto.CodigoFicha = entity.CODIGO_FICHA;
            dto.AnoAvaluo = entity.ANO_AVALUO;
            dto.AnoAvaluoString = entity.ANO_AVALUO.ToString("u");
            FechaDto anoAvaluoObj = FechaObj.GetFechaDto(entity.ANO_AVALUO);
            dto.AnoAvaluoObj = (FechaDto)anoAvaluoObj;
            dto.UnidadMedidaId = entity.UNIDAD_MEDIDA_ID;
            dto.AreaM2 = entity.AREA_M2;
            dto.ValorUnitario = entity.VALOR_UNITARIO;
            dto.ValorAjustado = entity.VALOR_AJUSTADO;
            dto.FactorAjuste = entity.FACTOR_AJUSTE;
            dto.FactorFrente = entity.FACTOR_FRENTE;
            dto.FactorForma = entity.FACTOR_FORMA;
            dto.FactorEsquina = entity.FACTOR_ESQUINA;
            dto.FactorProf = entity.FACTOR_PROF;
            dto.FactorArea = entity.FACTOR_AREA;
            dto.ValorModificado = entity.VALOR_MODIFICADO;
            dto.AreaTotal = entity.AREA_TOTAL;
            dto.MontoAvaluo = entity.MONTO_AVALUO;
            dto.Observaciones = entity.OBSERVACIONES;
            dto.IncrementoEsquina = entity.INCREMENTO_ESQUINA;
            dto.Extra1 = entity.EXTRA1;
            dto.Extra2 = entity.EXTRA2;
            dto.Extra3 = entity.EXTRA3;
            dto.CodigoParcela = entity.CODIGO_PARCELA;
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
            dto.CodigoZonificacion = entity.CODIGO_ZONIFICACION;
            dto.FrenteParcela = entity.FRENTE_PARCELA;
            dto.CodigoVialidadPrincipal = entity.CODIGO_VIALIDAD_PRINCIPAL;
            dto.CodigoVialidadAdyacente1 = entity.CODIGO_VIALIDAD_ADYACENTE1;
            dto.CodigoVialidadAdyacente2 = entity.CODIGO_VIALIDAD_ADYACENTE1;
            dto.Vialidad1 = entity.VIALIDAD1;
            dto.Vialidad2 = entity.VIALIDAD2;
            dto.Vialidad3 = entity.VIALIDAD3;
            dto.Vialidad4 = entity.VIALIDAD4;
            dto.UbicacionTerreno = entity.UBICACION_TERRENO;
            dto.CodigoVialidad1 = entity.CODIGO_VIALIDAD1;
            dto.CodigoVialidad2 = entity.CODIGO_VIALIDAD2;
            dto.CodigoVialidad3 = entity.CODIGO_VIALIDAD3;
            dto.CodigoVialidad4 = entity.CODIGO_VIALIDAD4;
            dto.FactorProFundidad = entity.FACTOR_PROFUNDIDAD;
            dto.MontoTotalAvaluo = entity.MONTO_AVALUO;


            return dto;

        }

        public async Task<List<CatAvaluoTerrenoResponseDto>> MapListCatAvaluoTerreno(List<CAT_AVALUO_TERRENO> dtos)
        {
            List<CatAvaluoTerrenoResponseDto> result = new List<CatAvaluoTerrenoResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapCatAvaluoTerreno(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CatAvaluoTerrenoResponseDto>>> GetAll()
        {

            ResultDto<List<CatAvaluoTerrenoResponseDto>> result = new ResultDto<List<CatAvaluoTerrenoResponseDto>>(null);
            try
            {
                var avaluoTerreno = await _repository.GetAll();
                var cant = avaluoTerreno.Count();
                if (avaluoTerreno != null && avaluoTerreno.Count() > 0)
                {
                    var listDto = await MapListCatAvaluoTerreno(avaluoTerreno);

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
