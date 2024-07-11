using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;
using Convertidor.Utility;

namespace Convertidor.Services.Cnt
{
    public class TmpLibrosService : ITmpLibrosService
    {
        private readonly ITmpLibrosRepository _repository;

        public TmpLibrosService(ITmpLibrosRepository repository)
        {
            _repository = repository;
        }

        public async Task<TmpLibrosResponseDto> MapTmptLibros(TMP_LIBROS dtos)
        {
            TmpLibrosResponseDto itemResult = new TmpLibrosResponseDto();
            itemResult.CodigoLibro = dtos.CODIGO_LIBRO;
            itemResult.CodigoCuentaBanco = dtos.CODIGO_CUENTA_BANCO;
            itemResult.FechaLibro = dtos.FECHA_LIBRO;
            itemResult.FechaLibroString = dtos.FECHA_LIBRO.ToString("u");
            FechaDto fechaLibroObj = FechaObj.GetFechaDto(dtos.FECHA_LIBRO);
            itemResult.FechaLibroObj = (FechaDto)fechaLibroObj;
            itemResult.Status = dtos.STATUS;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;


            return itemResult;
        }

        public async Task<List<TmpLibrosResponseDto>> MapListTmpLibros(List<TMP_LIBROS> dtos)
        {
            List<TmpLibrosResponseDto> result = new List<TmpLibrosResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapTmptLibros(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<TmpLibrosResponseDto>>> GetAll()
        {

            ResultDto<List<TmpLibrosResponseDto>> result = new ResultDto<List<TmpLibrosResponseDto>>(null);
            try
            {
                var libros = await _repository.GetAll();
                var cant = libros.Count();
                if (libros != null && libros.Count() > 0)
                {
                    var listDto = await MapListTmpLibros(libros);

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
