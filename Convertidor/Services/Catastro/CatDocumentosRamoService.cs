using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Dtos.Catastro;
using Convertidor.Utility;

namespace Convertidor.Services.Catastro
{
    public class CatDocumentosRamoService : ICatDocumentosRamoService
    {
        private readonly ICatDocumentosRamoRepository _repository;

        public CatDocumentosRamoService(ICatDocumentosRamoRepository repository)
        {
           _repository = repository;
        }

        public async Task<CatDocumentosRamoResponseDto> MapDocumentosRamo(CAT_DOCUMENTOS_RAMO entity)
        {
            CatDocumentosRamoResponseDto dto = new CatDocumentosRamoResponseDto();

            dto.CodigoDocuRamo = entity.CODIGO_DOCU_RAMO;
            dto.CodigoContribuyente = entity.CODIGO_CONTRIBUYENTE;
            dto.CodigoContribuyenteFk = entity.CODIGO_CONTRIBUYENTE_FK;
            dto.Tributo = entity.TRIBUTO;
            dto.CodigoIdentificador = entity.CODIGO_IDENTIFICADOR;
            dto.CodigoEstado = entity.CODIGO_ESTADO;
            dto.CodigoMunicipio = entity.CODIGO_MUNICIPIO;
            dto.TipoDocumentoId = entity.TIPO_DOCUMENTO_ID;
            dto.NumeroDocumento = entity.NUMERO_DOCUMENTO;
            dto.FechaDocumento = entity.FECHA_DOCUMENTO;
            dto.FechaDocumentoString = entity.FECHA_DOCUMENTO.ToString("u");
            FechaDto fechaDocumentoObj = FechaObj.GetFechaDto(entity.FECHA_DOCUMENTO);
            dto.FechaDocumentoObj = (FechaDto)fechaDocumentoObj;
            dto.Observacion = entity.OBSERVACION;
            dto.Descripcion = entity.DESCRIPCION;
            dto.Extra1 = entity.EXTRA1;
            dto.Extra2 = entity.EXTRA2;
            dto.Extra3 = entity.EXTRA3;
            dto.Origen = entity.ORIGEN;
            dto.TipoTransaccion = entity.TIPO_TRANSACCION;
            dto.TipoDocumentoId = entity.TIPO_DOCUMENTO_ID;
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
            dto.CodigoFicha = entity.CODIGO_FICHA;



            return dto;

        }

        public async Task<List<CatDocumentosRamoResponseDto>> MapListDirecciones(List<CAT_DOCUMENTOS_RAMO> dtos)
        {
            List<CatDocumentosRamoResponseDto> result = new List<CatDocumentosRamoResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapDocumentosRamo(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CatDocumentosRamoResponseDto>>> GetAll()
        {

            ResultDto<List<CatDocumentosRamoResponseDto>> result = new ResultDto<List<CatDocumentosRamoResponseDto>>(null);
            try
            {
                var documentosRamo = await _repository.GetAll();
                var cant = documentosRamo.Count();
                if (documentosRamo != null && documentosRamo.Count() > 0)
                {
                    var listDto = await MapListDirecciones(documentosRamo);

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
