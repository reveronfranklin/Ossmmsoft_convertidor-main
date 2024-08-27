using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public class CatControlParcelasService : ICatControlParcelasService
    {
        private readonly ICatControlParcelasRepository _repository;

        public CatControlParcelasService(ICatControlParcelasRepository repository)
        {
            _repository = repository;
        }

        public async Task<CatControlParcelasResponseDto> MapControlParcelas(CAT_CONTROL_PARCELAS entity)
        {
            CatControlParcelasResponseDto dto = new CatControlParcelasResponseDto();

            dto.CodigoControlParcela = entity.CODIGO_CONTROL_PARCELA;
            dto.CodigoCatastro = entity.CODIGO_CATASTRO;
            dto.CodigoViejoCat = entity.CODIGO_VIEJO_CAT;
            dto.CodigoContribuyente = entity.CODIGO_CONTRIBUYENTE;
            dto.CodigoUbicacionNac = entity.CODIGO_UBICACION_NAC;
            dto.PaisId = entity.PAIS_ID;
            dto.EntidadId = entity.ENTIDAD_ID;
            dto.MunicipioId = entity.MUNICIPIO_ID;
            dto.ParroquiaId = entity.PARROQUIA_ID;
            dto.AmbitoId = entity.AMBITO_ID;
            dto.SectorId = entity.SECTOR_ID;
            dto.SubsectorId = entity.SUB_SECTOR_ID;
            dto.ManzanaId = entity.MANZANA_ID;
            dto.ParcelaId = entity.PARCELA_ID;
            dto.SubsectorId = entity.SUB_SECTOR_ID;
            dto.Observacion = entity.OBSERVACION;
            dto.NumeroControl = entity.NUMERO_CONTROL;
            dto.AreaParcela = entity.AREA_PARCELA;
            dto.FrenteParcela = entity.FRENTE_PARCELA;
            dto.FrenteTipo = entity.FRENTE_TIPO;
            dto.AreaTipo = entity.AREA_TIPO;
            dto.TipoTransaccion = entity.TIPO_TRANSACCION;
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


            return dto;

        }

        public async Task<List<CatControlParcelasResponseDto>> MapListControlParcelas(List<CAT_CONTROL_PARCELAS> dtos)
        {
            List<CatControlParcelasResponseDto> result = new List<CatControlParcelasResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapControlParcelas(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CatControlParcelasResponseDto>>> GetAll()
        {

            ResultDto<List<CatControlParcelasResponseDto>> result = new ResultDto<List<CatControlParcelasResponseDto>>(null);
            try
            {
                var controlParcelas = await _repository.GetAll();
                var cant = controlParcelas.Count();
                if (controlParcelas != null && controlParcelas.Count() > 0)
                {
                    var listDto = await MapListControlParcelas(controlParcelas);

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
