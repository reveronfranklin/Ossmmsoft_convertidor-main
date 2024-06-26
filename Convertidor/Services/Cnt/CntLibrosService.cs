using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;
using Convertidor.Utility;

namespace Convertidor.Services.Cnt
{
    public class CntLibrosService : ICntLibrosService
    {
        private readonly ICntLibrosRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public CntLibrosService(ICntLibrosRepository repository,
                                ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<CntLibrosResponseDto> MapCntLibros(CNT_LIBROS dtos)
        {
            CntLibrosResponseDto itemResult = new CntLibrosResponseDto();
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

        public async Task<List<CntLibrosResponseDto>> MapListCntLibros(List<CNT_LIBROS> dtos)
        {
            List<CntLibrosResponseDto> result = new List<CntLibrosResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapCntLibros(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntLibrosResponseDto>>> GetAll()
        {

            ResultDto<List<CntLibrosResponseDto>> result = new ResultDto<List<CntLibrosResponseDto>>(null);
            try
            {
                var libros = await _repository.GetAll();
                var cant = libros.Count();
                if (libros != null && libros.Count() > 0)
                {
                    var listDto = await MapListCntLibros(libros);

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
