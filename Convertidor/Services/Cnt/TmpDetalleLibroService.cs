using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public class TmpDetalleLibroService : ITmpDetalleLibroService
    {
        private readonly ITmpDetalleLibroRepository _repository;

        public TmpDetalleLibroService(ITmpDetalleLibroRepository repository)
        {
            _repository = repository;
        }

        public async Task<TmpDetalleLibroResponseDto> MapTmpDetalleLibro(TMP_DETALLE_LIBRO dtos)
        {
            TmpDetalleLibroResponseDto itemResult = new TmpDetalleLibroResponseDto();
            itemResult.CodigoDetalleLibro = dtos.CODIGO_DETALLE_LIBRO;
            itemResult.CodigoLibro = dtos.CODIGO_LIBRO;
            itemResult.TipoDocumentoId = dtos.TIPO_DOCUMENTO_ID;
            itemResult.CodigoCheque = dtos.CODIGO_CHEQUE;
            itemResult.CodigoIdentificador = dtos.CODIGO_IDENTIFICADOR;
            itemResult.OrigenId = dtos.ORIGEN_ID;
            itemResult.NumeroDocumento = dtos.NUMERO_DOCUMENTO;
            itemResult.Descripcion = dtos.DESCRIPCION;
            itemResult.Monto = dtos.MONTO;
            itemResult.Status = dtos.STATUS;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.Status = dtos.STATUS;

            return itemResult;

        }

        public async Task<List<TmpDetalleLibroResponseDto>> MapListTmpDetalleLibro(List<TMP_DETALLE_LIBRO> dtos)
        {
            List<TmpDetalleLibroResponseDto> result = new List<TmpDetalleLibroResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapTmpDetalleLibro(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<TmpDetalleLibroResponseDto>>> GetAll()
        {

            ResultDto<List<TmpDetalleLibroResponseDto>> result = new ResultDto<List<TmpDetalleLibroResponseDto>>(null);
            try
            {
                var tmpDetalleLibro = await _repository.GetAll();
                var cant = tmpDetalleLibro.Count();
                if (tmpDetalleLibro != null && tmpDetalleLibro.Count() > 0)
                {
                    var listDto = await MapListTmpDetalleLibro(tmpDetalleLibro);

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
