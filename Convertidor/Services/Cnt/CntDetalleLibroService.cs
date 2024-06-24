using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Data.Repository.Cnt;
using Convertidor.Dtos.Cnt;
using Convertidor.Dtos.Presupuesto;
namespace Convertidor.Services.Cnt
{
    
    //
    public class CntDetalleLibroService : ICntDetalleLibroService
    {
        private readonly ICntDetalleLibroRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ICntDescriptivaRepository _cntDescriptivaRepository;

        public CntDetalleLibroService(ICntDetalleLibroRepository repository,
                                        ISisUsuarioRepository sisUsuarioRepository,
                                        ICntDescriptivaRepository cntDescriptivaRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _cntDescriptivaRepository = cntDescriptivaRepository;
        }

       

        public async Task<CntDetalleLibroResponseDto> MapDetalleLibro(CNT_DETALLE_LIBRO dtos)
        {
            CntDetalleLibroResponseDto itemResult = new CntDetalleLibroResponseDto();
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

        public async Task<List<CntDetalleLibroResponseDto>> MapListDetalleLibro(List<CNT_DETALLE_LIBRO> dtos)
        {
            List<CntDetalleLibroResponseDto> result = new List<CntDetalleLibroResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapDetalleLibro(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntDetalleLibroResponseDto>>> GetAll()
        {

            ResultDto<List<CntDetalleLibroResponseDto>> result = new ResultDto<List<CntDetalleLibroResponseDto>>(null);
            try
            {
                var detalleLibro = await _repository.GetAll();
                var cant = detalleLibro.Count();
                if (detalleLibro != null && detalleLibro.Count() > 0)
                {
                    var listDto = await MapListDetalleLibro(detalleLibro);

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

        public async Task<ResultDto<List<CntDetalleLibroResponseDto>>> GetAllByCodigoLibro(int codigoLibro)
        {

            ResultDto<List<CntDetalleLibroResponseDto>> result = new ResultDto<List<CntDetalleLibroResponseDto>>(null);
            try
            {

                var detalleLibro = await _repository.GetByCodigoLibro(codigoLibro);



                if (detalleLibro.Count() > 0)
                {
                    List<CntDetalleLibroResponseDto> listDto = new List<CntDetalleLibroResponseDto>();

                    foreach (var item in detalleLibro)
                    {
                        var dto = await MapDetalleLibro(item);
                        listDto.Add(dto);
                    }


                    result.Data = listDto;

                    result.IsValid = true;
                    result.Message = "";
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = "No existen Datos";

                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }
    }
}