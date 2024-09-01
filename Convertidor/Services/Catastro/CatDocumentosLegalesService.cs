using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Dtos.Catastro;
using Convertidor.Utility;

namespace Convertidor.Services.Catastro
{
    public class CatDocumentosLegalesService : ICatDocumentosLegalesService
    {
        private readonly ICatDocumentosLegalesRepository _repository;

        public CatDocumentosLegalesService(ICatDocumentosLegalesRepository repository)
        {
            _repository = repository;
        }

        public async Task<CatDocumentosLegalesResponseDto> MapDocumentosLegales(CAT_DOCUMENTOS_LEGALES entity)
        {
            CatDocumentosLegalesResponseDto dto = new CatDocumentosLegalesResponseDto();

            dto.CodigoDocumentosLegales = entity.CODIGO_DOCUMENTOS_LEGALES;
            dto.CodigoFicha = entity.CODIGO_FICHA;
            dto.DocumentoNumero = entity.DOCUMENTO_NUMERO;
            dto.FolioNumero = entity.FOLIO_NUMERO;
            dto.TomoNumero = entity.TOMO_NUMERO;
            dto.ProfNumero = entity.PROF_NUMERO;
            dto.FechaRegistro = entity.FECHA_REGISTRO;
            dto.FechaRegistroString = entity.FECHA_REGISTRO.ToString("u");
            FechaDto fechaRegistroObj = FechaObj.GetFechaDto(entity.FECHA_REGISTRO);
            dto.FechaRegistroObj = (FechaDto)fechaRegistroObj;
            dto.AreaTerreno = entity.AREA_TERRENO;
            dto.AreaConstruccion = entity.AREA_CONSTRUCCION;
            dto.Protocolo = entity.PROTOCOLO;
            dto.MontoRegistro = entity.MONTO_REGISTRO;
            dto.ServTerreno = entity.SERV_TERRENO;
            dto.PrecioTerreno = entity.PRECIO_TERRENO;
            dto.NumeroCivico = entity.NUMERO_CIVICO;
            dto.FechaPrimeraVisita = entity.FECHA_PRIMERA_VISITA;
            dto.FechaPrimeraVisitaString = entity.FECHA_PRIMERA_VISITA.ToString("u");
            FechaDto fechaPrimeraVisita = FechaObj.GetFechaDto(entity.FECHA_PRIMERA_VISITA);
            dto.FechaPrimeraVisitaObj = (FechaDto) fechaPrimeraVisita;
            dto.FechaLevantamiento = entity.FECHA_LEVANTAMIENTO;
            dto.FechaLevantamientoString = entity.FECHA_LEVANTAMIENTO.ToString("u");
            FechaDto fechaLevantamiento = FechaObj.GetFechaDto(entity.FECHA_LEVANTAMIENTO);
            dto.FechaLevantamientoObj = (FechaDto)fechaLevantamiento;
            dto.ControlArchivo = entity.CONTROL_ARCHIVO;
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

        public async Task<List<CatDocumentosLegalesResponseDto>> MapListDirecciones(List<CAT_DOCUMENTOS_LEGALES> dtos)
        {
            List<CatDocumentosLegalesResponseDto> result = new List<CatDocumentosLegalesResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapDocumentosLegales(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CatDocumentosLegalesResponseDto>>> GetAll()
        {

            ResultDto<List<CatDocumentosLegalesResponseDto>> result = new ResultDto<List<CatDocumentosLegalesResponseDto>>(null);
            try
            {
                var documentosLegales = await _repository.GetAll();
                var cant = documentosLegales.Count();
                if (documentosLegales != null && documentosLegales.Count() > 0)
                {
                    var listDto = await MapListDirecciones(documentosLegales);

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
