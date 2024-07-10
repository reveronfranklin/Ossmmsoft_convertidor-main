using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public class CntRelacionDocumentosService : ICntRelacionDocumentosService
    {
        private readonly ICntRelacionDocumentosRepository _repository;

        public CntRelacionDocumentosService(ICntRelacionDocumentosRepository repository)
        {
            _repository = repository;
        }

        public async Task<CntRelacionDocumentosResponseDto> MapRelacionDocumentos(CNT_RELACION_DOCUMENTOS dtos)
        {
            CntRelacionDocumentosResponseDto itemResult = new CntRelacionDocumentosResponseDto();
            itemResult.CodigoRelacionDocumento = dtos.CODIGO_RELACION_DOCUMENTO;
            itemResult.TipoDocumentoId = dtos.TIPO_DOCUMENTO_ID;
            itemResult.TipoTransaccionId = dtos.TIPO_TRANSACCION_ID;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;


            return itemResult;

        }

        public async Task<List<CntRelacionDocumentosResponseDto>> MapListRelacionDocumentos(List<CNT_RELACION_DOCUMENTOS> dtos)
        {
            List<CntRelacionDocumentosResponseDto> result = new List<CntRelacionDocumentosResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapRelacionDocumentos(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntRelacionDocumentosResponseDto>>> GetAll()
        {

            ResultDto<List<CntRelacionDocumentosResponseDto>> result = new ResultDto<List<CntRelacionDocumentosResponseDto>>(null);
            try
            {
                var relacionDocumento = await _repository.GetAll();
                var cant = relacionDocumento.Count();
                if (relacionDocumento != null && relacionDocumento.Count() > 0)
                {
                    var listDto = await MapListRelacionDocumentos(relacionDocumento);

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
