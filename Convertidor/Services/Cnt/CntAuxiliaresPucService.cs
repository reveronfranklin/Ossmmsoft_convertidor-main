using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;
using Convertidor.Utility;

namespace Convertidor.Services.Cnt
{
    public class CntAuxiliaresPucService : ICntAuxiliaresPucService
    {
        private readonly ICntAuxiliaresPucRepository _repository;

        public CntAuxiliaresPucService(ICntAuxiliaresPucRepository repository)
        {
            _repository = repository;
        }

        public async Task<CntAuxiliaresPucResponseDto> MapAuxiliaresPuc(CNT_AUXILIARES_PUC dtos)
        {
            CntAuxiliaresPucResponseDto itemResult = new CntAuxiliaresPucResponseDto();
            itemResult.CodigoAuxiliarPuc = dtos.CODIGO_AUXILIAR_PUC;
            itemResult.CodigoAuxiliar = dtos.CODIGO_AUXILIAR;
            itemResult.CodigoPuc = dtos.CODIGO_PUC;
            itemResult.TipoDocumentoId = dtos.TIPO_DOCUMENTO_ID;
           

            return itemResult;

        }

        public async Task<List<CntAuxiliaresPucResponseDto>> MapListAuxiliaresPuc(List<CNT_AUXILIARES_PUC> dtos)
        {
            List<CntAuxiliaresPucResponseDto> result = new List<CntAuxiliaresPucResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapAuxiliaresPuc(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntAuxiliaresPucResponseDto>>> GetAll()
        {

            ResultDto<List<CntAuxiliaresPucResponseDto>> result = new ResultDto<List<CntAuxiliaresPucResponseDto>>(null);
            try
            {
                var auxiliaresPuc = await _repository.GetAll();
                var cant = auxiliaresPuc.Count();
                if (auxiliaresPuc != null && auxiliaresPuc.Count() > 0)
                {
                    var listDto = await MapListAuxiliaresPuc(auxiliaresPuc);

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
