using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public class CatDesgloseService : ICatDesgloseService
    {
        private readonly ICatDesgloseRepository _repository;

        public CatDesgloseService(ICatDesgloseRepository repository)
        {
            _repository = repository;
        }

        public async Task<CatDesgloseResponseDto> MapCatDesglose(CAT_DESGLOSE entity)
        {
            CatDesgloseResponseDto dto = new CatDesgloseResponseDto();

            dto.CodigoDesglose = entity.CODIGO_DESGLOSE;
            dto.CodigoDesgloseFk = entity.CODIGO_DESGLOSE_FK;
            dto.CodigoDesglosePk = entity.CODIGO_DESGLOSE_PK;
            dto.CodigoParcela = entity.CODIGO_PARCELA;
            dto.CodigoCatastro = entity.CODIGO_CATASTRO;
            dto.Titulo = entity.TITULO;
            dto.AreaTerrenoTotal = entity.AREA_TERRENO_TOTAL;
            dto.AreaConTrucTotal = entity.AREA_CONTRUC_TOTAL;
            dto.AreaTrrTotalVendi = entity.AREA_TRR_TOTAL_VENDI;
            dto.AreaTerrComun = entity.AREA_TERR_COMUN;
            dto.AreaContrucComun = entity.AREA_CONTRUC_COMUN;
            dto.AreaTerrSinCond = entity.AREA_TERR_SIN_COND;
            dto.Area = entity.AREA;
            dto.EstacionaTerr = entity.ESTACIONA_TERR;
            dto.EstacionaContruc = entity.ESTACIONA_CONTRUC;
            dto.PorcentajCondominio = entity.PORCENTAJ_CONDOMINIO;
            dto.ManualTerreno = entity.MANUAL_TERRENO;
            dto.ManualConstruccion = entity.MANUAL_CONSTRUCCION;
            dto.MaleteroTerreno = entity.MALETERO_TERRENO;
            dto.MaleteroConstruccion = entity.MALETERO_CONSTRUCCION;
            dto.Observacion = entity.OBSERVACION;
            dto.NivelId = entity.NIVEL_ID;
            dto.UnidadId = entity.UNIDAD_ID;
            dto.TipoOperacionId = entity.TIPO_OPERACION_ID;
            dto.TipoTransaccion = entity.TIPO_TRANSACCION;
            dto.Extra1 = entity.EXTRA1;
            dto.Extra2 = entity.EXTRA2;
            dto.Extra3 = entity.EXTRA3;
            dto.Extra4 = entity.EXTRA4;
            dto.Extra5 = entity.EXTRA5;
            


            return dto;

        }

        public async Task<List<CatDesgloseResponseDto>> MapListCatDesglose(List<CAT_DESGLOSE> dtos)
        {
            List<CatDesgloseResponseDto> result = new List<CatDesgloseResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapCatDesglose(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CatDesgloseResponseDto>>> GetAll()
        {

            ResultDto<List<CatDesgloseResponseDto>> result = new ResultDto<List<CatDesgloseResponseDto>>(null);
            try
            {
                var desglose = await _repository.GetAll();
                var cant = desglose.Count();
                if (desglose != null && desglose.Count() > 0)
                {
                    var listDto = await MapListCatDesglose(desglose);

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
