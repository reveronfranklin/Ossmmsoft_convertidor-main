using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
    public class PreProyectosService : IPreProyectosService
    {
        private readonly IPreProyectosRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;
        private readonly IPRE_INDICE_CAT_PRGRepository _pRE_INDICE_CAT_PRGRepository;

        public PreProyectosService(IPreProyectosRepository repository,
                                   ISisUsuarioRepository sisUsuarioRepository,
                                   IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository,
                                   IPRE_INDICE_CAT_PRGRepository pRE_INDICE_CAT_PRGRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
            _pRE_INDICE_CAT_PRGRepository = pRE_INDICE_CAT_PRGRepository;
        }


        public async Task<ResultDto<List<PreProyectosResponseDto>>> GetAll()
        {

            ResultDto<List<PreProyectosResponseDto>> result = new ResultDto<List<PreProyectosResponseDto>>(null);
            try
            {

                var preProyectos = await _repository.GetAll();



                if (preProyectos.Count() > 0)
                {
                    List<PreProyectosResponseDto> listDto = new List<PreProyectosResponseDto>();

                    foreach (var item in preProyectos)
                    {
                        var dto = await MapPreProyectos(item);
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

        public async Task<PreProyectosResponseDto> MapPreProyectos(PRE_PROYECTOS dto)
        {
            PreProyectosResponseDto itemResult = new PreProyectosResponseDto();
            itemResult.CodigoProyecto = dto.CODIGO_PROYECTO;
            itemResult.CodigoIcp = dto.CODIGO_ICP;
            itemResult.NumeroProyecto = dto.NUMERO_PROYECTO;
            itemResult.DenominacionProyecto = dto.DENOMINACION_PROYECTO;
            itemResult.Extra1 = dto.EXTRA1;
            itemResult.Extra2 = dto.EXTRA2;
            itemResult.Extra3 = dto.EXTRA3;
            itemResult.CodigoPresupuesto = dto.CODIGO_PRESUPUESTO;

            return itemResult;
        }

        public async Task<List<PreProyectosResponseDto>> MapListPreProyectos(List<PRE_PROYECTOS> dtos)
        {
            List<PreProyectosResponseDto> result = new List<PreProyectosResponseDto>();

            foreach (var item in dtos)
            {
                PreProyectosResponseDto itemResult = new PreProyectosResponseDto();
                itemResult = await MapPreProyectos(item);
                result.Add(itemResult);
            }

            return result;

        }

        public async Task<ResultDto<PreProyectosResponseDto>> Update(PreProyectosUpdateDto dto)
        {

            ResultDto<PreProyectosResponseDto> result = new ResultDto<PreProyectosResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.CodigoProyecto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proyecto no existe";
                    return result;

                }
                var codigoProyecto = await _repository.GetByCodigo(dto.CodigoProyecto);
                if (codigoProyecto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proyecto no existe";
                    return result;
                }

                if (dto.CodigoIcp < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;
                }

                var codigoIcp = await _pRE_INDICE_CAT_PRGRepository.GetByCodigo(dto.CodigoIcp);
                if (codigoIcp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;
                }

                if(dto.NumeroProyecto < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Proyecto Invalido";
                    return result;
                }

                if(dto.DenominacionProyecto.Length > 1000) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Proyecto Invalido";
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

                codigoProyecto.CODIGO_PROYECTO = dto.CodigoProyecto;
                codigoProyecto.CODIGO_ICP = dto.CodigoIcp;
                codigoProyecto.NUMERO_PROYECTO = dto.NumeroProyecto;
                codigoProyecto.DENOMINACION_PROYECTO = dto.DenominacionProyecto;
                codigoProyecto.EXTRA1 = dto.Extra1;
                codigoProyecto.EXTRA2 = dto.Extra2;
                codigoProyecto.EXTRA3 = dto.Extra3;
                codigoProyecto.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;




                codigoProyecto.CODIGO_EMPRESA = conectado.Empresa;
                codigoProyecto.USUARIO_UPD = conectado.Usuario;
                codigoProyecto.FECHA_UPD = DateTime.Now;
                await _repository.Update(codigoProyecto);

                var resultDto = await MapPreProyectos(codigoProyecto);
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

        public async Task<ResultDto<PreProyectosResponseDto>> Create(PreProyectosUpdateDto dto)
        {

            ResultDto<PreProyectosResponseDto> result = new ResultDto<PreProyectosResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();


                if (dto.CodigoIcp < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;
                }

                var codigoIcp = await _pRE_INDICE_CAT_PRGRepository.GetByCodigo(dto.CodigoIcp);
                if (codigoIcp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;
                }

                if (dto.NumeroProyecto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Proyecto Invalido";
                    return result;
                }

                if (dto.DenominacionProyecto.Length > 1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Proyecto Invalido";
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



                PRE_PROYECTOS entity = new PRE_PROYECTOS();
                entity.CODIGO_PROYECTO = await _repository.GetNextKey();
                entity.CODIGO_ICP = dto.CodigoIcp;
                entity.NUMERO_PROYECTO = dto.NumeroProyecto;
                entity.DENOMINACION_PROYECTO = dto.DenominacionProyecto;
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
                    var resultDto = await MapPreProyectos(created.Data);
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

        public async Task<ResultDto<PreProyectosDeleteDto>> Delete(PreProyectosDeleteDto dto)
        {

            ResultDto<PreProyectosDeleteDto> result = new ResultDto<PreProyectosDeleteDto>(null);
            try
            {

                var codigoProyecto = await _repository.GetByCodigo(dto.CodigoProyecto);
                if (codigoProyecto == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Proyecto no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoProyecto);

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