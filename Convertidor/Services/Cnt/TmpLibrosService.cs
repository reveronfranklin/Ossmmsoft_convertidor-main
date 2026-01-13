using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;
using Convertidor.Utility;

namespace Convertidor.Services.Cnt
{
    public class TmpLibrosService : ITmpLibrosService
    {
        private readonly ITmpLibrosRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public TmpLibrosService(ITmpLibrosRepository repository,
                                 ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
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

        public async Task<ResultDto<TmpLibrosResponseDto>> Create(TmpLibrosUpdateDto dto)
        {
            ResultDto<TmpLibrosResponseDto> result = new ResultDto<TmpLibrosResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();


                if (dto.CodigoCuentaBanco <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo cuenta Banco Invalido";
                    return result;
                }



                if (dto.FechaLibro == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "fecha Libro invalido";
                    return result;
                }

                if (dto.Status.Length > 1)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
                    return result;
                }


                if (dto.Extra1 is not null && dto.Extra1.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra1 Invalido";
                    return result;
                }
                if (dto.Extra2 is not null && dto.Extra2.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra2 Invalido";
                    return result;
                }

                if (dto.Extra3 is not null && dto.Extra3.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra3 Invalido";
                    return result;
                }






                TMP_LIBROS entity = new TMP_LIBROS();
                entity.CODIGO_LIBRO = await _repository.GetNextKey();
                entity.CODIGO_CUENTA_BANCO = dto.CodigoCuentaBanco;
                entity.FECHA_LIBRO = dto.FechaLibro;
                entity.STATUS = dto.Status;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;





                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapTmptLibros(created.Data);
                    result.Data = resultDto;
                    result.IsValid = true;
                    result.Message = "";
                }
                else
                {

                    result.Data = null;
                    result.IsValid = created.IsValid;
                    result.Message = created.Message;
                }

                return result;


            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }


        public async Task<ResultDto<TmpLibrosResponseDto>> Update(TmpLibrosUpdateDto dto)
        {
            ResultDto<TmpLibrosResponseDto> result = new ResultDto<TmpLibrosResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoLibro = await _repository.GetByCodigo(dto.CodigoLibro);
                if (codigoLibro == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Libro Invalido";
                    return result;

                }


                if (dto.CodigoCuentaBanco <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo cuenta Banco Invalido";
                    return result;
                }



                if (dto.FechaLibro == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "fecha Libro invalido";
                    return result;
                }

                if (dto.Status.Length > 1)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
                    return result;
                }


                if (dto.Extra1 is not null && dto.Extra1.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra1 Invalido";
                    return result;
                }
                if (dto.Extra2 is not null && dto.Extra2.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra2 Invalido";
                    return result;
                }

                if (dto.Extra3 is not null && dto.Extra3.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra3 Invalido";
                    return result;
                }





                codigoLibro.CODIGO_LIBRO = dto.CodigoLibro;
                codigoLibro.CODIGO_CUENTA_BANCO = dto.CodigoCuentaBanco;
                codigoLibro.FECHA_LIBRO = dto.FechaLibro;
                codigoLibro.STATUS = dto.Status;
                codigoLibro.EXTRA1 = dto.Extra1;
                codigoLibro.EXTRA2 = dto.Extra2;
                codigoLibro.EXTRA3 = dto.Extra3;




                codigoLibro.CODIGO_EMPRESA = conectado.Empresa;
                codigoLibro.USUARIO_UPD = conectado.Usuario;
                codigoLibro.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoLibro);

                var resultDto = await MapTmptLibros(codigoLibro);
                result.Data = resultDto;
                result.IsValid = true;
                result.Message = "";
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<ResultDto<TmpLibrosDeleteDto>> Delete(TmpLibrosDeleteDto dto)
        {
            ResultDto<TmpLibrosDeleteDto> result = new ResultDto<TmpLibrosDeleteDto>(null);
            try
            {

                var codigoLibro = await _repository.GetByCodigo(dto.CodigoLibro);
                if (codigoLibro == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Libro no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoLibro);

                if (deleted.Length > 0)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = deleted;
                }
                else
                {
                    result.Data = dto;
                    result.IsValid = true;
                    result.Message = deleted;

                }


            }
            catch (Exception ex)
            {
                result.Data = dto;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }

    }
}
