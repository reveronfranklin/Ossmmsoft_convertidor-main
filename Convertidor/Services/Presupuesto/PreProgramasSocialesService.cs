using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
    public class PreProgramasSocialesService : IPreProgramasSocialesService
    {
        private readonly IPreProgramasSocialesRepository _repository;
        private readonly IPRE_INDICE_CAT_PRGRepository _inDICE_CAT_PRGRepository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public PreProgramasSocialesService(IPreProgramasSocialesRepository repository,
                                               IPRE_INDICE_CAT_PRGRepository inDICE_CAT_PRGRepository,
             
                                               IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository,
                                               ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _inDICE_CAT_PRGRepository = inDICE_CAT_PRGRepository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<ResultDto<List<PreProgramasSocialesResponseDto>>> GetAll()
        {

            ResultDto<List<PreProgramasSocialesResponseDto>> result = new ResultDto<List<PreProgramasSocialesResponseDto>>(null);
            try
            {

                var PreProgramaSocial = await _repository.GetAll();



                if (PreProgramaSocial.Count() > 0)
                {
                    List<PreProgramasSocialesResponseDto> listDto = new List<PreProgramasSocialesResponseDto>();

                    foreach (var item in PreProgramaSocial)
                    {
                        var dto = await MapPreProgramasSociales(item);
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


        public async Task<PreProgramasSocialesResponseDto> MapPreProgramasSociales(PRE_PROGRAMAS_SOCIALES dto)
        {
            PreProgramasSocialesResponseDto itemResult = new PreProgramasSocialesResponseDto();
            itemResult.CodigoPrgSocial = dto.CODIGO_PRG_SOCIAL;
            itemResult.CodigoIcp = dto.CODIGO_ICP;
            itemResult.Denominacion = dto.DENOMINACION;
            itemResult.OrganismoId = dto.ORGANISMO_ID;
            itemResult.AsignacionAnual = dto.ASIGNACION_ANUAL;
            itemResult.Extra1 = dto.EXTRA1;
            itemResult.Extra2 = dto.EXTRA2;
            itemResult.Extra3 = dto.EXTRA3;
       

            return itemResult;

        }

        public async Task<List<PreProgramasSocialesResponseDto>> MapListPreProgramasSociales(List<PRE_PROGRAMAS_SOCIALES> dtos)
        {
            List<PreProgramasSocialesResponseDto> result = new List<PreProgramasSocialesResponseDto>();


            foreach (var item in dtos)
            {

                PreProgramasSocialesResponseDto itemResult = new PreProgramasSocialesResponseDto();

                itemResult = await MapPreProgramasSociales(item);

                result.Add(itemResult);
            }
            return result;



        }

        public async Task<ResultDto<PreProgramasSocialesResponseDto>> Update(PreProgramasSocialesUpdateDto dto)
        {

            ResultDto<PreProgramasSocialesResponseDto> result = new ResultDto<PreProgramasSocialesResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if(dto.CodigoPrgSocial < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Prg Social Org invalido";
                    return result;

                }

                var codigoPrgSocial = await _repository.GetByCodigo(dto.CodigoPrgSocial);
                if(codigoPrgSocial == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo por Gastos Mes invalido";
                    return result;
                }

                if (dto.CodigoIcp < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp invalido";
                    return result;

                }

                var codigoIcp = await _inDICE_CAT_PRGRepository.GetByCodigo(dto.CodigoIcp);
                if(codigoIcp == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp invalido";
                    return result;

                }
                if(dto.Denominacion.Length > 300) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Invalida";
                    return result;

                }
                if(dto.AsignacionAnual < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Asignacion Anual Invalida";
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



                codigoPrgSocial.CODIGO_PRG_SOCIAL = dto.CodigoPrgSocial;
                codigoPrgSocial.CODIGO_ICP = dto.CodigoIcp;
                codigoPrgSocial.DENOMINACION = dto.Denominacion;
                codigoPrgSocial.ASIGNACION_ANUAL = dto.AsignacionAnual;
                codigoPrgSocial.EXTRA1 = dto.Extra1;
                codigoPrgSocial.EXTRA2 = dto.Extra2;
                codigoPrgSocial.EXTRA3 = dto.Extra3;


                codigoPrgSocial.CODIGO_EMPRESA = conectado.Empresa;
                codigoPrgSocial.USUARIO_UPD = conectado.Usuario;
                codigoPrgSocial.FECHA_UPD = DateTime.Now;
                await _repository.Update(codigoPrgSocial);

                var resultDto = await MapPreProgramasSociales(codigoPrgSocial);
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

        public async Task<ResultDto<PreProgramasSocialesResponseDto>> Create(PreProgramasSocialesUpdateDto dto)
        {

            ResultDto<PreProgramasSocialesResponseDto> result = new ResultDto<PreProgramasSocialesResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.CodigoIcp < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp invalido";
                    return result;

                }

                var codigoIcp = await _inDICE_CAT_PRGRepository.GetByCodigo(dto.CodigoIcp);
                if (codigoIcp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp invalido";
                    return result;

                }
                if (dto.Denominacion.Length > 300)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Invalida";
                    return result;

                }
                if (dto.AsignacionAnual < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Asignacion Anual Invalida";
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

                if(dto.CodigoPresupuesto < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;

                }

                var codigoPresupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa , dto.CodigoPresupuesto);
                if(codigoPresupuesto == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;

                }



                PRE_PROGRAMAS_SOCIALES entity = new PRE_PROGRAMAS_SOCIALES();

                entity.CODIGO_PRG_SOCIAL = await _repository.GetNextKey();
                entity.CODIGO_ICP = dto.CodigoIcp;
                entity.DENOMINACION = dto.Denominacion;
                entity.ASIGNACION_ANUAL = dto.AsignacionAnual;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;


                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapPreProgramasSociales(created.Data);
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

        public async Task<ResultDto<PreProgramasSocialesDeleteDto>> Delete(PreProgramasSocialesDeleteDto dto)
        {

            ResultDto<PreProgramasSocialesDeleteDto> result = new ResultDto<PreProgramasSocialesDeleteDto>(null);
            try
            {

                var codigoPrgSocial = await _repository.GetByCodigo(dto.CodigoPrgSocial);
                if (codigoPrgSocial == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Prg Social no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoPrgSocial);

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
