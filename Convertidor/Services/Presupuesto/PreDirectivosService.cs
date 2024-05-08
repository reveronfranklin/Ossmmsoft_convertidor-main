using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
    public class PreDirectivosService : IPreDirectivosService
    {
        private readonly IPreDirectivosRepository _repository;
        private readonly IPreDescriptivaRepository _preDescriptivaRepository;
        private readonly IPRE_PRESUPUESTOSRepository _preSUPUESTOSRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public PreDirectivosService(IPreDirectivosRepository repository,
                                     IPreDescriptivaRepository preDescriptivaRepository,
                                     IPRE_PRESUPUESTOSRepository preSUPUESTOSRepository,
                                     ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _preDescriptivaRepository = preDescriptivaRepository;
            _preSUPUESTOSRepository = preSUPUESTOSRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<ResultDto<List<PreDirectivosResponseDto>>> GetAll()
        {
            ResultDto<List<PreDirectivosResponseDto>> result = new ResultDto<List<PreDirectivosResponseDto>>(null);

            try
            {
                var directivos = await _repository.GetAll();

                if (directivos.Count() > 0)
                {
                    List<PreDirectivosResponseDto> listDto = new List<PreDirectivosResponseDto>();

                    foreach (var item in directivos)
                    {
                        var dto = await MapPreDirectivos(item);
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
                    result.Message = " No existen Datos";

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

        public async Task<PreDirectivosResponseDto> MapPreDirectivos(PRE_DIRECTIVOS dto)
        {
            PreDirectivosResponseDto itemResult = new PreDirectivosResponseDto();
            itemResult.CodigoDirectivo = dto.CODIGO_DIRECTIVO;
            itemResult.CodigoIdentificacion = dto.CODIGO_IDENTIFICACION;
            itemResult.TipoDirectivoId = dto.TIPO_DIRECTIVO_ID;
            itemResult.TituloId = dto.TITULO_ID;
            itemResult.Cedula = dto.CEDULA;
            itemResult.Nombre = dto.NOMBRE;
            itemResult.Apellido = dto.APELLIDO;
            itemResult.Cargo = dto.CARGO;
            itemResult.Extra1 = dto.EXTRA1;
            itemResult.Extra2 = dto.EXTRA2;
            itemResult.Extra3 = dto.EXTRA3;
            itemResult.CodigoPresupuesto = dto.CODIGO_PRESUPUESTO;

            return itemResult;

        }

        public async Task<List<PreDirectivosResponseDto>> MapListPreDirectivos(List<PRE_DIRECTIVOS> dtos)
        {
            List<PreDirectivosResponseDto> result = new List<PreDirectivosResponseDto>();

            foreach (var item in dtos)
            {
                PreDirectivosResponseDto itemResult = new PreDirectivosResponseDto();
                itemResult = await MapPreDirectivos(item);
                result.Add(itemResult);
            }

            return result;

        }

        public async Task<ResultDto<PreDirectivosResponseDto>> Update(PreDirectivosUpdateDto dto)
        {

            ResultDto<PreDirectivosResponseDto> result = new ResultDto<PreDirectivosResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.CodigoDirectivo < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Directivo Directivo no existe";
                    return result;

                }
                var codigoDirectivo = await _repository.GetByCodigo(dto.CodigoDirectivo);
                if (codigoDirectivo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Directivo no existe";
                    return result;
                }
                if (dto.CodigoIdentificacion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Año Invalido";
                    return result;
                }

                if (dto.TipoDirectivoId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Directivo Id Invalido";
                    return result;
                }
                var tipoDirectivoId = await _preDescriptivaRepository.GetByIdAndTitulo(10, dto.TipoDirectivoId);
                if (tipoDirectivoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Directivo Id Invalido";
                    return result;
                }

                if (dto.TituloId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Titulo Id invalido";
                    return result;

                }

                var tituloId = await _preDescriptivaRepository.GetByIdAndTitulo(11, dto.TituloId);
                if (tituloId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Titulo Id invalido";
                    return result;

                }

                if(dto.Nombre.Length > 100) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nombre invalido";
                    return result;

                }
                if (dto.Apellido.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Apellido invalido";
                    return result;

                }
                if (dto.Cargo.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cargo invalido";
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

                if (dto.CodigoPresupuesto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                var codigoPresupuesto = await _preSUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (codigoPresupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }


                codigoDirectivo.CODIGO_DIRECTIVO = dto.CodigoDirectivo;
                codigoDirectivo.CODIGO_IDENTIFICACION = dto.CodigoIdentificacion;
                codigoDirectivo.TIPO_DIRECTIVO_ID = dto.TipoDirectivoId;
                codigoDirectivo.TITULO_ID = dto.TituloId;
                codigoDirectivo.CEDULA = dto.Cedula;
                codigoDirectivo.NOMBRE = dto.Nombre;
                codigoDirectivo.APELLIDO = dto.Apellido;
                codigoDirectivo.CARGO = dto.Cargo;
                codigoDirectivo.EXTRA1 = dto.Extra1;
                codigoDirectivo.EXTRA2 = dto.Extra2;
                codigoDirectivo.EXTRA3 = dto.Extra3;
                codigoDirectivo.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;



                codigoDirectivo.CODIGO_EMPRESA = conectado.Empresa;
                codigoDirectivo.USUARIO_UPD = conectado.Usuario;
                codigoDirectivo.FECHA_UPD = DateTime.Now;
                await _repository.Update(codigoDirectivo);

                var resultDto = await MapPreDirectivos(codigoDirectivo);
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

        public async Task<ResultDto<PreDirectivosResponseDto>> Create(PreDirectivosUpdateDto dto)
        {

            ResultDto<PreDirectivosResponseDto> result = new ResultDto<PreDirectivosResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

             
                if (dto.CodigoIdentificacion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Año Invalido";
                    return result;
                }

                if (dto.TipoDirectivoId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Directivo Id Invalido";
                    return result;
                }

                var tipoDirectivoId = await _preDescriptivaRepository.GetByIdAndTitulo(10, dto.TipoDirectivoId);
                if (tipoDirectivoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Directivo Id Invalido";
                    return result;
                }

                if (dto.TituloId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Titulo Id invalido";
                    return result;

                }

                var tituloId = await _preDescriptivaRepository.GetByIdAndTitulo(11, dto.TituloId);
                if (tituloId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Titulo Id invalido";
                    return result;

                }

                if (dto.Nombre.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nombre invalido";
                    return result;

                }
                if (dto.Apellido.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Apellido invalido";
                    return result;

                }
                if (dto.Cargo.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cargo invalido";
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

                if (dto.CodigoPresupuesto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                var codigoPresupuesto = await _preSUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (codigoPresupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                PRE_DIRECTIVOS entity = new PRE_DIRECTIVOS();

                entity.CODIGO_DIRECTIVO = await _repository.GetNextKey();
                entity.CODIGO_IDENTIFICACION = dto.CodigoIdentificacion;
                entity.TIPO_DIRECTIVO_ID = dto.TipoDirectivoId;
                entity.TITULO_ID = dto.TituloId;
                entity.CEDULA = dto.Cedula;
                entity.NOMBRE = dto.Nombre;
                entity.APELLIDO = dto.Apellido;
                entity.CARGO = dto.Cargo;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;



                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapPreDirectivos(created.Data);
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

        public async Task<ResultDto<PreDirectivosDeleteDto>> Delete(PreDirectivosDeleteDto dto)
        {

            ResultDto<PreDirectivosDeleteDto> result = new ResultDto<PreDirectivosDeleteDto>(null);
            try
            {

                var codigoDirectivo = await _repository.GetByCodigo(dto.CodigoDirectivo);
                if (codigoDirectivo == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Directivo no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoDirectivo);

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





