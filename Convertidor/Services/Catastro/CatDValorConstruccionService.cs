using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Dtos.Catastro;
using Convertidor.Utility;

namespace Convertidor.Services.Catastro
{
    public class CatDValorConstruccionService : ICatDValorConstruccionService
    {
        private readonly ICatDValorConstruccionRepository _repository;

        public CatDValorConstruccionService(ICatDValorConstruccionRepository repository)
        {
            _repository = repository;
        }

        public async Task<CatDValorContruccionResponseDto> MapDValorConstruccion(CAT_D_VALOR_CONSTRUCCION entity)
        {
            CatDValorContruccionResponseDto dto = new CatDValorContruccionResponseDto();

            dto.CodigoParcela = entity.CODIGO_PARCELA;
            dto.CodigoDValorConstruccion = entity.CODIGO_D_VALOR_CONSTRUCCION;
            dto.CodigoValorConstruccion = entity.CODIGO_VALOR_CONSTRUCCION;
            dto.CodigoInmueble = entity.CODIGO_INMUEBLE;
            dto.CodigoCatastro = entity.CODIGO_CATASTRO;
            dto.EstructuraNivel1Id = entity.ESTRUCTURA_NIVEL1_ID;
            dto.EstructuraNivel2Id = entity.ESTRUCTURA_NIVEL2_ID;
            dto.EstructuraNivel3Id = entity.ESTRUCTURA_NIVEL3_ID;
            dto.Estructuranivel4Id = entity.ESTRUCTURA_NIVEL4_ID;
            dto.EstructuraDescriptiva = entity.ESTRUCTURA_DESCRIPTIVA;
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
            dto.ValorComplementario = entity.VALOR_COMPLEMENTARIO;



            return dto;

        }

        public async Task<List<CatDValorContruccionResponseDto>> MapListDValorConstruccion(List<CAT_D_VALOR_CONSTRUCCION> dtos)
        {
            List<CatDValorContruccionResponseDto> result = new List<CatDValorContruccionResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapDValorConstruccion(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CatDValorContruccionResponseDto>>> GetAll()
        {

            ResultDto<List<CatDValorContruccionResponseDto>> result = new ResultDto<List<CatDValorContruccionResponseDto>>(null);
            try
            {
                var dValorConstruccion = await _repository.GetAll();
                var cant = dValorConstruccion.Count();
                if (dValorConstruccion != null && dValorConstruccion.Count() > 0)
                {
                    var listDto = await MapListDValorConstruccion(dValorConstruccion);

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
