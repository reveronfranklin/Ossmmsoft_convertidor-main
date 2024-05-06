using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
    public class PreNivelesPucService : IPreNivelesPucService
    {
        private readonly IPreNivelesPucRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public PreNivelesPucService(IPreNivelesPucRepository repository,
                                    
                                    ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
    
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<ResultDto<List<PreNivelesPucResponseDto>>> GetAll()
        {

            ResultDto<List<PreNivelesPucResponseDto>> result = new ResultDto<List<PreNivelesPucResponseDto>>(null);
            try
            {

                var preNivelesPuc = await _repository.GetAll();



                if (preNivelesPuc.Count() > 0)
                {
                    List<PreNivelesPucResponseDto> listDto = new List<PreNivelesPucResponseDto>();

                    foreach (var item in preNivelesPuc)
                    {
                        var dto = await MapPreNivelesPuc(item);
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

       
        public async Task<PreNivelesPucResponseDto> MapPreNivelesPuc(PRE_NIVELES_PUC dto)
        {
            PreNivelesPucResponseDto itemResult = new PreNivelesPucResponseDto();
            itemResult.CodigoGrupo = dto.CODIGO_GRUPO;
            itemResult.Nivel1 = dto.NIVEL1;
            itemResult.Nivel2 = dto.NIVEL2;
            itemResult.Nivel3 = dto.NIVEL3;
            itemResult.Nivel4 = dto.NIVEL4;
            itemResult.Nivel5 = dto.NIVEL5;
            itemResult.Nivel6 = dto.NIVEL6;
            itemResult.Extra1 = dto.EXTRA1;
            itemResult.Extra2 = dto.EXTRA2;
            itemResult.Extra3 = dto.EXTRA3;
           

            return itemResult;

        }

        public async Task<List<PreNivelesPucResponseDto>> MapListPreNivelesPucDto(List<PRE_NIVELES_PUC> dtos)
        {
            List<PreNivelesPucResponseDto> result = new List<PreNivelesPucResponseDto>();


            foreach (var item in dtos)
            {

                PreNivelesPucResponseDto itemResult = new PreNivelesPucResponseDto();

                itemResult = await MapPreNivelesPuc(item);

                result.Add(itemResult);
            }
            return result;



        }

        public async Task<ResultDto<PreNivelesPucResponseDto>> Update(PreNivelesPucUpdateDto dto)
        {

            ResultDto<PreNivelesPucResponseDto> result = new ResultDto<PreNivelesPucResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if(dto.CodigoGrupo < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Grupo no existe";
                    return result;

                }
                var codigoGrupo = await _repository.GetByCodigo(dto.CodigoGrupo);
                if (codigoGrupo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Grupo no existe";
                    return result;
                }

                if(dto.Nivel1.Length > 20) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "nivel1 Invalido";
                    return result;
                }

                if (dto.Nivel2.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "nivel2 Invalido";
                    return result;
                }

                if (dto.Nivel3.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "nivel3 Invalido";
                    return result;
                }

                if (dto.Nivel4.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "nivel4 Invalido";
                    return result;
                }

                if (dto.Nivel5.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "nivel5 Invalido";
                    return result;
                }

                if (dto.Nivel6.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "nivel6 Invalido";
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

                



                codigoGrupo.CODIGO_GRUPO = dto.CodigoGrupo;
                codigoGrupo.NIVEL1 = dto.Nivel1;
                codigoGrupo.NIVEL2 = dto.Nivel2;
                codigoGrupo.NIVEL3 = dto.Nivel3;
                codigoGrupo.NIVEL4 = dto.Nivel4;
                codigoGrupo.NIVEL5 = dto.Nivel5;
                codigoGrupo.NIVEL6 = dto.Nivel6;
                codigoGrupo.EXTRA1 = dto.Extra1;
                codigoGrupo.EXTRA2 = dto.Extra2;
                codigoGrupo.EXTRA3 = dto.Extra3;




                codigoGrupo.CODIGO_EMPRESA = conectado.Empresa;
                codigoGrupo.USUARIO_UPD = conectado.Usuario;
                codigoGrupo.FECHA_UPD = DateTime.Now;
                await _repository.Update(codigoGrupo);

                var resultDto = await MapPreNivelesPuc(codigoGrupo);
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

        public async Task<ResultDto<PreNivelesPucResponseDto>> Create(PreNivelesPucUpdateDto dto)
        {

            ResultDto<PreNivelesPucResponseDto> result = new ResultDto<PreNivelesPucResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();


                if (dto.Nivel1.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "nivel1 Invalido";
                    return result;
                }

                if (dto.Nivel2.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "nivel2 Invalido";
                    return result;
                }

                if (dto.Nivel3.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "nivel3 Invalido";
                    return result;
                }

                if (dto.Nivel4.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "nivel4 Invalido";
                    return result;
                }

                if (dto.Nivel5.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "nivel5 Invalido";
                    return result;
                }

                if (dto.Nivel6.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "nivel6 Invalido";
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




                PRE_NIVELES_PUC entity = new PRE_NIVELES_PUC();
                entity.CODIGO_GRUPO = await _repository.GetNextKey();
                entity.NIVEL1 = dto.Nivel1;
                entity.NIVEL2 = dto.Nivel2;
                entity.NIVEL3 = dto.Nivel3;
                entity.NIVEL4 = dto.Nivel4;
                entity.NIVEL5 = dto.Nivel5;
                entity.NIVEL6 = dto.Nivel6;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                

                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapPreNivelesPuc(created.Data);
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

        public async Task<ResultDto<PreNivelesPucDeleteDto>> Delete(PreNivelesPucDeleteDto dto)
        {

            ResultDto<PreNivelesPucDeleteDto> result = new ResultDto<PreNivelesPucDeleteDto>(null);
            try
            {

                var codigoGrupo = await _repository.GetByCodigo(dto.CodigoGrupo);
                if (codigoGrupo == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Grupo no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoGrupo);

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
