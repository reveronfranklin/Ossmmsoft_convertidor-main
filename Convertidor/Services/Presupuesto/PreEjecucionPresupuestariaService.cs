using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository.Rh;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
    public class PreEjecucionPresupuestariaService : IPreEjecucionPresupuestariaService
    {
        private readonly IPreEjecucionPresupuestariaRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;

        public PreEjecucionPresupuestariaService(IPreEjecucionPresupuestariaRepository repository,
                                                  ISisUsuarioRepository sisUsuarioRepository,
                                                  IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
        }

        public async Task<ResultDto<List<PreEjecucionPresupuestariaResponseDto>>> GetAll()
        {
            ResultDto<List<PreEjecucionPresupuestariaResponseDto>> result = new ResultDto<List<PreEjecucionPresupuestariaResponseDto>>(null);

            try
            {
                var ejecucionPresupuestaria = await _repository.GetAll();

                if (ejecucionPresupuestaria.Count() > 0)
                {
                    List<PreEjecucionPresupuestariaResponseDto> listDto = new List<PreEjecucionPresupuestariaResponseDto>();

                    foreach (var item in ejecucionPresupuestaria)
                    {
                        var dto = await MapPreEjecucionPresupuestaria(item);
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

        public async Task<PreEjecucionPresupuestariaResponseDto> MapPreEjecucionPresupuestaria(PRE_EJECUCION_PRESUPUESTARIA dto)
        {
            PreEjecucionPresupuestariaResponseDto itemResult = new PreEjecucionPresupuestariaResponseDto();
            itemResult.CodigoGrupo = dto.CODIGO_GRUPO;
            itemResult.CodigoNivel1 = dto.CODIGO_NIVEL1;
            itemResult.CodigoNivel2 = dto.CODIGO_NIVEL2;
            itemResult.CodigoNivel3 = dto.CODIGO_NIVEL3;
            itemResult.CodigoNivel4 = dto.CODIGO_NIVEL4;
            itemResult.IReal = dto.I_REAL;
            itemResult.IProyectado = dto.I_PROYECTADO;
            itemResult.IiReal = dto.II_REAL;
            itemResult.IiProyectado = dto.II_PROYECTADO;
            itemResult.IiiReal = dto.III_REAL;
            itemResult.IiiProyectado = dto.III_PROYECTADO;
            itemResult.IvReal = dto.IV_REAL;
            itemResult.IvProyectado = dto.IV_PROYECTADO;
            itemResult.Extra1 = dto.EXTRA1;
            itemResult.Extra2 = dto.EXTRA2;
            itemResult.Extra3 = dto.EXTRA3;
            itemResult.CodigoPresupuesto = dto.CODIGO_PRESUPUESTO;

            return itemResult;

        }

        public async Task<List<PreEjecucionPresupuestariaResponseDto>> MapListPreEjecucionPresupuestaria(List<PRE_EJECUCION_PRESUPUESTARIA> dtos)
        {
            List<PreEjecucionPresupuestariaResponseDto> result = new List<PreEjecucionPresupuestariaResponseDto>();

            foreach (var item in dtos)
            {
                PreEjecucionPresupuestariaResponseDto itemResult = new PreEjecucionPresupuestariaResponseDto();
                itemResult = await MapPreEjecucionPresupuestaria(item);
                result.Add(itemResult);
            }

            return result;

        }

        public async Task<ResultDto<PreEjecucionPresupuestariaResponseDto>> Update(PreEjecucionPresupuestariaUpdateDto dto)
        {

            ResultDto<PreEjecucionPresupuestariaResponseDto> result = new ResultDto<PreEjecucionPresupuestariaResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if(dto.CodigoEjePresupuestaria <= 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo ejecucion Presupuestaria no existe";
                    return result;


                }

                var codigoEjePresupuestaria = await _repository.GetByCodigo(dto.CodigoEjePresupuestaria);
                if (codigoEjePresupuestaria == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo ejecucion Presupuestaria no existe";
                    return result;
                }

                if (dto.CodigoGrupo.Length > 1)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Grupo Invalido";
                    return result;

                }
               
                if (dto.CodigoNivel1.Length > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Nivel1 Invalido";
                    return result;
                }

                if (dto.CodigoNivel2.Length > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Nivel2 Invalido";
                    return result;
                }
                if (dto.CodigoNivel3.Length > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Nivel3 Invalido";
                    return result;
                }

                if (dto.CodigoNivel4.Length > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Nivel4 Invalido";
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

                var codigoPresupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (codigoPresupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }


                codigoEjePresupuestaria.CODIGO_GRUPO = dto.CodigoGrupo;
                codigoEjePresupuestaria.CODIGO_NIVEL1 = dto.CodigoNivel1;
                codigoEjePresupuestaria.CODIGO_NIVEL2 = dto.CodigoNivel2;
                codigoEjePresupuestaria.CODIGO_NIVEL3 = dto.CodigoNivel3;
                codigoEjePresupuestaria.CODIGO_NIVEL4 = dto.CodigoNivel4;
                codigoEjePresupuestaria.I_REAL = dto.IReal;
                codigoEjePresupuestaria.I_PROYECTADO = dto.IProyectado;
                codigoEjePresupuestaria.II_REAL = dto.IiReal;
                codigoEjePresupuestaria.II_PROYECTADO = dto.IiProyectado;
                codigoEjePresupuestaria.III_REAL = dto.IiiReal;
                codigoEjePresupuestaria.III_PROYECTADO = dto.IiiProyectado;
                codigoEjePresupuestaria.IV_REAL = dto.IvReal;
                codigoEjePresupuestaria.IV_PROYECTADO = dto.IvProyectado;
                codigoEjePresupuestaria.EXTRA1 = dto.Extra1;
                codigoEjePresupuestaria.EXTRA2 = dto.Extra2;
                codigoEjePresupuestaria.EXTRA3 = dto.Extra3;
                codigoEjePresupuestaria.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;



                codigoEjePresupuestaria.CODIGO_EMPRESA = conectado.Empresa;
                codigoEjePresupuestaria.USUARIO_UPD = conectado.Usuario;
                codigoEjePresupuestaria.FECHA_UPD = DateTime.Now;
                await _repository.Update(codigoEjePresupuestaria);

                var resultDto = await MapPreEjecucionPresupuestaria(codigoEjePresupuestaria);
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

        public async Task<ResultDto<PreEjecucionPresupuestariaResponseDto>> Create(PreEjecucionPresupuestariaUpdateDto dto)
        {

            ResultDto<PreEjecucionPresupuestariaResponseDto> result = new ResultDto<PreEjecucionPresupuestariaResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.CodigoGrupo.Length > 1)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Grupo Invalido";
                    return result;

                }

                if (dto.CodigoNivel1.Length > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Nivel1 Invalido";
                    return result;
                }

                if (dto.CodigoNivel2.Length > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Nivel2 Invalido";
                    return result;
                }
                if (dto.CodigoNivel3.Length > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Nivel3 Invalido";
                    return result;
                }

                if (dto.CodigoNivel4.Length > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Nivel4 Invalido";
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

                var codigoPresupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (codigoPresupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }


                PRE_EJECUCION_PRESUPUESTARIA entity = new PRE_EJECUCION_PRESUPUESTARIA();

                entity.CODIGO_EJE_PRESUPUESTARIA = await _repository.GetNextKey();
                entity.CODIGO_GRUPO = dto.CodigoGrupo;
                entity.CODIGO_NIVEL1 = dto.CodigoNivel1;
                entity.CODIGO_NIVEL2 = dto.CodigoNivel2;
                entity.CODIGO_NIVEL3 = dto.CodigoNivel3;
                entity.CODIGO_NIVEL4 = dto.CodigoNivel4;
                entity.I_REAL = dto.IReal;
                entity.I_PROYECTADO = dto.IProyectado;
                entity.II_REAL = dto.IiReal;
                entity.II_PROYECTADO = dto.IiProyectado;
                entity.III_REAL = dto.IiiReal;
                entity.III_PROYECTADO = dto.IiiProyectado;
                entity.IV_REAL = dto.IvReal;
                entity.IV_PROYECTADO = dto.IvProyectado;
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
                    var resultDto = await MapPreEjecucionPresupuestaria(created.Data);
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

        public async Task<ResultDto<PreEjecucionPresupuestariaDeleteDto>> Delete(PreEjecucionPresupuestariaDeleteDto dto)
        {

            ResultDto<PreEjecucionPresupuestariaDeleteDto> result = new ResultDto<PreEjecucionPresupuestariaDeleteDto>(null);
            try
            {

                var codigoGrupo = await _repository.GetByCodigo(dto.CodigoEjePresupuestaria);
                if (codigoGrupo == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Grupo no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoEjePresupuestaria);

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
