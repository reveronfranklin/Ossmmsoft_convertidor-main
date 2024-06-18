using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
    public class PreParticipaFinacieraOrgService : IPreParticipaFinacieraOrgService
    {
        private readonly IPreParticipaFinacieraOrgRepository _repository;
        private readonly IPreOrganismosRepository _preOrganismosRepository;
        private readonly IPRE_INDICE_CAT_PRGRepository _inDICE_CAT_PRGRepository;
        private readonly IPRE_PLAN_UNICO_CUENTASRepository _pLAN_UNICO_CUENTASRepository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public PreParticipaFinacieraOrgService(IPreParticipaFinacieraOrgRepository repository,
                                               IPreOrganismosRepository preOrganismosRepository,
                                               IPRE_INDICE_CAT_PRGRepository inDICE_CAT_PRGRepository,
                                               IPRE_PLAN_UNICO_CUENTASRepository pLAN_UNICO_CUENTASRepository,
                                               IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository,
                                               ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _preOrganismosRepository = preOrganismosRepository;
            _inDICE_CAT_PRGRepository = inDICE_CAT_PRGRepository;
            _pLAN_UNICO_CUENTASRepository = pLAN_UNICO_CUENTASRepository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<ResultDto<List<PreParticipaFinacieraOrgResponseDto>>> GetAll()
        {

            ResultDto<List<PreParticipaFinacieraOrgResponseDto>> result = new ResultDto<List<PreParticipaFinacieraOrgResponseDto>>(null);
            try
            {

                var preOrganismos = await _repository.GetAll();



                if (preOrganismos.Count() > 0)
                {
                    List<PreParticipaFinacieraOrgResponseDto> listDto = new List<PreParticipaFinacieraOrgResponseDto>();

                    foreach (var item in preOrganismos)
                    {
                        var dto = await MapPreParticipaFinancieraOrg(item);
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


        public async Task<PreParticipaFinacieraOrgResponseDto> MapPreParticipaFinancieraOrg(PRE_PARTICIPA_FINANCIERA_ORG dto)
        {
            PreParticipaFinacieraOrgResponseDto itemResult = new PreParticipaFinacieraOrgResponseDto();
            itemResult.CodigoParticipaFinancorg = dto.CODIGO_PARTICIPA_FINANC_ORG;
            itemResult.Ano = dto.ANO;
            itemResult.CodigoOrganismo = dto.CODIGO_ORGANISMO;
            itemResult.CodigoIcp = dto.CODIGO_ICP;
            itemResult.CuotaParticipacion = dto.CUOTA_PARTICIPACION;
            itemResult.Observaciones = dto.OBSERVACIONES;
            itemResult.Extra1 = dto.EXTRA1;
            itemResult.Extra2 = dto.EXTRA2;
            itemResult.Extra3 = dto.EXTRA3;
            itemResult.CodigoPuc = dto.CODIGO_PUC;
            itemResult.CodigoPresupuesto = dto.CODIGO_PRESUPUESTO;
           

            return itemResult;

        }

        public async Task<List<PreParticipaFinacieraOrgResponseDto>> MapListPreParticipaFinacieraOrg(List<PRE_PARTICIPA_FINANCIERA_ORG> dtos)
        {
            List<PreParticipaFinacieraOrgResponseDto> result = new List<PreParticipaFinacieraOrgResponseDto>();


            foreach (var item in dtos)
            {

                PreParticipaFinacieraOrgResponseDto itemResult = new PreParticipaFinacieraOrgResponseDto();

                itemResult = await MapPreParticipaFinancieraOrg(item);

                result.Add(itemResult);
            }
            return result;



        }

        public async Task<ResultDto<PreParticipaFinacieraOrgResponseDto>> Update(PreParticipaFinacieraOrgUpdateDto dto)
        {

            ResultDto<PreParticipaFinacieraOrgResponseDto> result = new ResultDto<PreParticipaFinacieraOrgResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if(dto.CodigoParticipaFinancorg < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Participa Financiera Org invalido";
                    return result;

                }

                var codigoParticipaFinancOrg = await _repository.GetByCodigo(dto.CodigoParticipaFinancorg);
                if(codigoParticipaFinancOrg == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Participa Financiera Org invalido";
                    return result;
                }

                if (dto.Ano < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ano invalido";
                    return result;

                }

                if (dto.CodigoOrganismo < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Organismo no existe";
                    return result;

                }

                var codigoOrganismo = await _preOrganismosRepository.GetByCodigo(dto.CodigoOrganismo);
                if (codigoOrganismo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Organismo no existe";
                    return result;

                }

                if(dto.CodigoIcp < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;

                }

                var codigoIcp = await _inDICE_CAT_PRGRepository.GetByCodigo(dto.CodigoIcp);

                if (codigoIcp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;
                }
                

                if (dto.CuotaParticipacion < 0 )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cuota participacion Invalida";
                    return result;
                }

                if (dto.Observaciones.Length > 1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Observaciones Invalidas";
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

                if (dto.CodigoPuc < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Invalido";
                    return result;
                }

                var codigoPuc = await _pLAN_UNICO_CUENTASRepository.GetByCodigo(dto.CodigoPuc);
                if(codigoPuc == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Invalido";
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



                codigoParticipaFinancOrg.CODIGO_PARTICIPA_FINANC_ORG = dto.CodigoParticipaFinancorg;
                codigoParticipaFinancOrg.ANO = dto.Ano;
                codigoParticipaFinancOrg.CODIGO_ORGANISMO = dto.CodigoOrganismo;
                codigoParticipaFinancOrg.CODIGO_ICP = dto.CodigoIcp;
                codigoParticipaFinancOrg.CUOTA_PARTICIPACION = dto.CuotaParticipacion;
                codigoParticipaFinancOrg.OBSERVACIONES = dto.Observaciones;
                codigoParticipaFinancOrg.EXTRA1 = dto.Extra1;
                codigoParticipaFinancOrg.EXTRA2 = dto.Extra2;
                codigoParticipaFinancOrg.EXTRA3 = dto.Extra3;
                codigoParticipaFinancOrg.CODIGO_PUC = dto.CodigoPuc;
                codigoParticipaFinancOrg.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;




                codigoParticipaFinancOrg.CODIGO_EMPRESA = conectado.Empresa;
                codigoParticipaFinancOrg.USUARIO_UPD = conectado.Usuario;
                codigoParticipaFinancOrg.FECHA_UPD = DateTime.Now;
                await _repository.Update(codigoParticipaFinancOrg);

                var resultDto = await MapPreParticipaFinancieraOrg(codigoParticipaFinancOrg);
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

        public async Task<ResultDto<PreParticipaFinacieraOrgResponseDto>> Create(PreParticipaFinacieraOrgUpdateDto dto)
        {

            ResultDto<PreParticipaFinacieraOrgResponseDto> result = new ResultDto<PreParticipaFinacieraOrgResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.Ano < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ano invalido";
                    return result;

                }

                if (dto.CodigoOrganismo < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Organismo no existe";
                    return result;

                }

                var codigoOrganismo = await _preOrganismosRepository.GetByCodigo(dto.CodigoOrganismo);
                if (codigoOrganismo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Organismo no existe";
                    return result;

                }

                if (dto.CodigoIcp < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;

                }

                var codigoIcp = await _inDICE_CAT_PRGRepository.GetByCodigo(dto.CodigoIcp);

                if (codigoIcp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;
                }


                if (dto.CuotaParticipacion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cuota participacion Invalida";
                    return result;
                }

                if (dto.Observaciones.Length > 1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Observaciones Invalidas";
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

                if (dto.CodigoPuc < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Invalido";
                    return result;
                }

                var codigoPuc = await _pLAN_UNICO_CUENTASRepository.GetByCodigo(dto.CodigoPuc);
                if (codigoPuc == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Invalido";
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





                PRE_PARTICIPA_FINANCIERA_ORG entity = new PRE_PARTICIPA_FINANCIERA_ORG();
                entity.CODIGO_PARTICIPA_FINANC_ORG = await _repository.GetNextKey();
                entity.ANO = dto.Ano;
                entity.CODIGO_ORGANISMO = dto.CodigoOrganismo;
                entity.CODIGO_ICP = dto.CodigoIcp;
                entity.CUOTA_PARTICIPACION = dto.CuotaParticipacion;
                entity.OBSERVACIONES = dto.Observaciones;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.CODIGO_PUC = dto.CodigoPuc;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;


                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapPreParticipaFinancieraOrg(created.Data);
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

        public async Task<ResultDto<PreParticipaFinacieraOrgDeleteDto>> Delete(PreParticipaFinacieraOrgDeleteDto dto)
        {

            ResultDto<PreParticipaFinacieraOrgDeleteDto> result = new ResultDto<PreParticipaFinacieraOrgDeleteDto>(null);
            try
            {

                var codigoParticipaFinancOrg = await _repository.GetByCodigo(dto.CodigoParticipaFinancorg);
                if (codigoParticipaFinancOrg == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Participa Financiera Org no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoParticipaFinancorg);

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
