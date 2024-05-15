using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
    public class PreEscalaService : IPreEscalaService
    {
        private readonly IPreEscalaRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;

        public PreEscalaService(IPreEscalaRepository repository,
                                 ISisUsuarioRepository sisUsuarioRepository,
                                 IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
        }

        public async Task<ResultDto<List<PreEscalaResponseDto>>> GetAll()
        {
            ResultDto<List<PreEscalaResponseDto>> result = new ResultDto<List<PreEscalaResponseDto>>(null);

            try
            {
                var escala = await _repository.GetAll();
                if (escala.Count() > 0)
                {
                    List<PreEscalaResponseDto> listDto = new List<PreEscalaResponseDto>();
                    foreach (var item in escala)
                    {
                        var dto = await MapPreEscala(item);
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

        public async Task<ResultDto<PreEscalaResponseDto>> GetByCodigo(int codigoEscala)
        {
            ResultDto<PreEscalaResponseDto> result = new ResultDto<PreEscalaResponseDto>(null);
            try
            {
                var escala = await _repository.GetByCodigo(codigoEscala);
                if (escala != null)
                {
                    var dto = await MapPreEscala(escala);
                    result.Data = dto;
                    result.IsValid = true;
                    result.Message = "";
                    result.LinkData = "";
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = " No existen Datos";
                    result.LinkData = "";

                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                result.LinkData = "";
            }



            return result;
        }

        public async Task<PreEscalaResponseDto> MapPreEscala(PRE_ESCALA dto)
        {
            PreEscalaResponseDto itemResult = new PreEscalaResponseDto();
            itemResult.CodigoEscala = dto.CODIGO_ESCALA;
            itemResult.Ano = dto.ANO;
            itemResult.Escenario = dto.ESCENARIO;
            itemResult.NumeroEscala = dto.NUMERO_ESCALA;
            itemResult.CodigoGrupo = dto.CODIGO_GRUPO;
            itemResult.ValorIni = dto.VALOR_INI;
            itemResult.ValorFin = dto.VALOR_FIN;
            itemResult.CodigoPresupuesto = dto.CODIGO_PRESUPUESTO;
            itemResult.Extra1 = dto.EXTRA1;
            itemResult.Extra2 = dto.EXTRA2;
            itemResult.Extra3 = dto.EXTRA3;
            

            return itemResult;

        }

        public async Task<List<PreEscalaResponseDto>> MapListPreEscala(List<PRE_ESCALA> dtos)
        {
            List<PreEscalaResponseDto> result = new List<PreEscalaResponseDto>();

            foreach (var item in dtos)
            {
                PreEscalaResponseDto itemResult = new PreEscalaResponseDto();
                itemResult = await MapPreEscala(item);
                result.Add(itemResult);
            }

            return result;

        }

        public async Task<ResultDto<PreEscalaResponseDto>> Update(PreEscalaUpdateDto dto)
        {

            ResultDto<PreEscalaResponseDto> result = new ResultDto<PreEscalaResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();



                if (dto.CodigoEscala <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Escala no existe";
                    return result;

                }

                var codigoEscala = await _repository.GetByCodigo(dto.CodigoEscala);
                if (codigoEscala == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Escala no existe";
                    return result;

                }


                if(dto.Escenario < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Escenario Invalido";
                    return result;

                }

                var escenario = dto.Escenario.ToString();
                if (escenario.Length > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Escenario Invalido";
                    return result;

                }

                if (dto.NumeroEscala.Length > 5)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Escala Invalido";
                    return result;

                }

                if (String.IsNullOrEmpty(dto.CodigoGrupo))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Grupo Invalido";
                    return result;
                }

                if (dto.CodigoGrupo.Length > 10)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Grupo Invalido";
                    return result;

                }
               

                if (dto.ValorIni < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Valor Ini Invalida";
                    return result;
                }

                if (dto.ValorFin < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "valor fin Invalido";
                    return result;
                }

                if (dto.CodigoPresupuesto <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                var codigoPresupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (codigoPresupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
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

               

                dto.Ano = codigoPresupuesto.ANO;



                codigoEscala.CODIGO_ESCALA = dto.CodigoEscala;
                codigoEscala.ANO = dto.Ano;
                codigoEscala.ESCENARIO = dto.Escenario;
                codigoEscala.NUMERO_ESCALA = dto.NumeroEscala;
                codigoEscala.CODIGO_GRUPO = dto.CodigoGrupo;
                codigoEscala.VALOR_INI = dto.ValorIni;
                codigoEscala.VALOR_FIN = dto.ValorFin;
                codigoEscala.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                codigoEscala.EXTRA1 = dto.Extra1;
                codigoEscala.EXTRA2 = dto.Extra2;
                codigoEscala.EXTRA3 = dto.Extra3;
                

                codigoEscala.CODIGO_EMPRESA = conectado.Empresa;
                codigoEscala.USUARIO_UPD = conectado.Usuario;
                codigoEscala.FECHA_UPD = DateTime.Now;
                await _repository.Update(codigoEscala);

                var resultDto = await MapPreEscala(codigoEscala);
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

        public async Task<ResultDto<PreEscalaResponseDto>> Create(PreEscalaUpdateDto dto)
        {

            ResultDto<PreEscalaResponseDto> result = new ResultDto<PreEscalaResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();


                if (dto.Escenario < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Escenario Invalido";
                    return result;

                }

                var escenario = dto.Escenario.ToString();
                if (escenario.Length > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Escenario Invalido";
                    return result;

                }

                if (dto.NumeroEscala.Length > 5) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Escala Invalido";
                    return result;

                }

                if (String.IsNullOrEmpty(dto.CodigoGrupo))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Grupo Invalido";
                    return result;
                }

                if (dto.CodigoGrupo.Length > 10)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Grupo Invalido";
                    return result;

                }


                if (dto.ValorIni < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Valor Ini Invalida";
                    return result;
                }

                if (dto.ValorFin < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "valor fin Invalido";
                    return result;
                }

                if (dto.CodigoPresupuesto <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                var codigoPresupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (codigoPresupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
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



                dto.Ano = codigoPresupuesto.ANO;




                PRE_ESCALA entity = new PRE_ESCALA();
                entity.CODIGO_ESCALA = await _repository.GetNextKey();
                entity.ANO = dto.Ano;
                entity.ESCENARIO = dto.Escenario;
                entity.NUMERO_ESCALA = dto.NumeroEscala;
                entity.CODIGO_GRUPO = dto.CodigoGrupo;
                entity.VALOR_INI = dto.ValorIni;
                entity.VALOR_FIN = dto.ValorFin;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
              



                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapPreEscala(created.Data);
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

        public async Task<ResultDto<PreEscalaDeleteDto>> Delete(PreEscalaDeleteDto dto)
        {

            ResultDto<PreEscalaDeleteDto> result = new ResultDto<PreEscalaDeleteDto>(null);
            try
            {

                var codigoEscala = await _repository.GetByCodigo(dto.CodigoEscala);
                if (codigoEscala == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Escala no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoEscala);

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
