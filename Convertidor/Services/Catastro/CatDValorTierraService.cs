using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Dtos.Catastro;
using Convertidor.Utility;

namespace Convertidor.Services.Catastro
{
    public class CatDValorTierraService : ICatDValorTierraService
    {
        private readonly ICatDValorTierraRepository _repository;

        public CatDValorTierraService(ICatDValorTierraRepository repository)
        {
            _repository = repository;
        }

        public async Task<CatDValorTierraResponseDto> MapDValorTierra(CAT_D_VALOR_TIERRA entity)
        {
            CatDValorTierraResponseDto dto = new CatDValorTierraResponseDto();

            dto.CodigoValorTierra = entity.CODIGO_VALOR_TIERRA;
            dto.CodigoDValorTierraUrbFk = entity.CODIGO_D_VALOR_TIERRA_URB_FK;
            dto.PaisId = entity.PAIS_ID;
            dto.EstadoId = entity.ESTADO_ID;
            dto.MunicipioId = entity.MUNICIPIO_ID;
            dto.ParroquiaId = entity.PARROQUIA_ID;
            dto.SectorId = entity.SECTOR_ID;
            dto.FechaIniVigValor = entity.FECHA_INI_VIG_VALOR;
            dto.FechaIniVigValorString = entity.FECHA_INI_VIG_VALOR.ToString("u");
            FechaDto FechaIniVigValorObj = FechaObj.GetFechaDto(entity.FECHA_INI_VIG_VALOR);
            dto.FechaIniVigValorObj = (FechaDto)FechaIniVigValorObj;
            dto.FechaFinVigValor = entity.FECHA_FIN_VIG_VALOR;
            dto.FechaFinVigValorString = entity.FECHA_FIN_VIG_VALOR.ToString("u");
            FechaDto FechaFinVigValorObj = FechaObj.GetFechaDto(entity.FECHA_FIN_VIG_VALOR);
            dto.FechaFinVigValorObj = (FechaDto)FechaFinVigValorObj;
            dto.Extra1 = entity.EXTRA1;
            dto.Extra2 = entity.EXTRA2;
            dto.Extra3 = entity.EXTRA3;
            dto.CodigoZonificacionId = entity.CODIGO_ZONIFICACION_ID;
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

        public async Task<List<CatDValorTierraResponseDto>> MapListDValorTierra(List<CAT_D_VALOR_TIERRA> dtos)
        {
            List<CatDValorTierraResponseDto> result = new List<CatDValorTierraResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapDValorTierra(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CatDValorTierraResponseDto>>> GetAll()
        {

            ResultDto<List<CatDValorTierraResponseDto>> result = new ResultDto<List<CatDValorTierraResponseDto>>(null);
            try
            {
                var dValorTierra = await _repository.GetAll();
                var cant = dValorTierra.Count();
                if (dValorTierra != null && dValorTierra.Count() > 0)
                {
                    var listDto = await MapListDValorTierra(dValorTierra);

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
