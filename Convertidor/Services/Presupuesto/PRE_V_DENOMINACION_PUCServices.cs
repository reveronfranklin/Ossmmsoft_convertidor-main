using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public class PRE_V_DENOMINACION_PUCServices : IPRE_V_DENOMINACION_PUCServices
    {
       

        private readonly IPRE_V_SALDOSRepository _repository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository ;
        private readonly IPRE_V_DENOMINACION_PUCRepository _pRE_V_DENOMINACION_PUCRepository;
        private readonly IMapper _mapper;

        public PRE_V_DENOMINACION_PUCServices(IPRE_V_SALDOSRepository repository,
                                     IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository,
                                    IPRE_V_DENOMINACION_PUCRepository pRE_V_DENOMINACION_PUCRepository,
                                    IMapper mapper)
        {
            _repository = repository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
            _pRE_V_DENOMINACION_PUCRepository = pRE_V_DENOMINACION_PUCRepository;

            _mapper = mapper;
        }


        public async Task<ResultDto<List<GetPRE_V_DENOMINACION_PUCDto>>> GetResumenPresupuestoDenominacionPuc(FilterPreVDenominacionPuc filter)
        {
            var presupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(13,filter.CodigoPresupuesto);
            await _repository.RecalcularSaldo(presupuesto.CODIGO_PRESUPUESTO);

            ResultDto<List<GetPRE_V_DENOMINACION_PUCDto>> result = new ResultDto<List<GetPRE_V_DENOMINACION_PUCDto>>(null);
            var denominacionPuc = await _pRE_V_DENOMINACION_PUCRepository.GetByCodigoPresupuesto(filter.CodigoPresupuesto);
            if (denominacionPuc.Count() > 0)
            {
                List<GetPRE_V_DENOMINACION_PUCDto> resultList = new List<GetPRE_V_DENOMINACION_PUCDto>();
                foreach (var item in denominacionPuc)
                {

                    resultList.Add(MapPRE_V_DENOMINACIONGetPRE_V_DENOMINACION_PUCDto(item));
                }
                result.Data = resultList;

                result.IsValid = true;
                result.Message = "";
            }
            else
            {
                result.Data = null;
                result.IsValid = true;
                result.Message = " No existen Datos";

            }
            return result;

        }

            

        public async Task<ResultDto<List<GetPRE_V_DENOMINACION_PUCDto>>> GetAll(FilterPRE_V_DENOMINACION_PUC filter)
        {
            var presupuesto = await _pRE_PRESUPUESTOSRepository.GetLast();
            await _repository.RecalcularSaldo(presupuesto.CODIGO_PRESUPUESTO);

            ResultDto<List<GetPRE_V_DENOMINACION_PUCDto>> result = new ResultDto<List<GetPRE_V_DENOMINACION_PUCDto>>(null);
            try
            {
                var pRE_V_SALDOs = await _pRE_V_DENOMINACION_PUCRepository.GetAll(filter);
                if (pRE_V_SALDOs.Count() > 0)
                {

                    List<GetPRE_V_DENOMINACION_PUCDto> resultList = new List<GetPRE_V_DENOMINACION_PUCDto>();
                    foreach (var item in pRE_V_SALDOs)
                    {

                        resultList.Add(MapPRE_V_DENOMINACIONGetPRE_V_DENOMINACION_PUCDto(item));
                    }


                   

                    result.Data = resultList;

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


        public List<GetPreDenominacionPucResumenAnoDto> ResumenePreDenominacionPuc(List<GetPRE_V_DENOMINACION_PUCDto> dto)
        {
            List<GetPreDenominacionPucResumenAnoDto> result   = new  List<GetPreDenominacionPucResumenAnoDto>();
            if (dto.Count>0)
            {
                foreach (var item in dto)
                {
                    
                    var getPreDenominacionPucResumenAnoDto = result
                                .Where(x => x.AnoSaldo == item.AnoSaldo &&
                                            x.CodigoPresupuesto == item.CodigoPresupuesto &&
                                            x.CodigoPartida == item.CodigoPartida &&
                                            x.CodigoGenerica==item.CodigoGenerica &&
                                            x.CodigoEspecifica==item.CodigoEspecifica &&
                                            x.CodigoNivel5==item.CodigoNivel5).FirstOrDefault();
                    if (getPreDenominacionPucResumenAnoDto == null)
                    {
                        GetPreDenominacionPucResumenAnoDto itemResult = new GetPreDenominacionPucResumenAnoDto();
                        itemResult.AnoSaldo = item.AnoSaldo;
                        itemResult.CodigoPresupuesto = item.CodigoPresupuesto;
                        itemResult.CodigoPartida = item.CodigoPartida;
                        itemResult.CodigoGenerica = item.CodigoGenerica;
                        itemResult.CodigoEspecifica = item.CodigoEspecifica;
                        itemResult.CodigoSubEspecifica = item.CodigoSubEspecifica;
                        itemResult.CodigoNivel5 = item.CodigoNivel5;
                        itemResult.DenominacionPuc = item.DenominacionPuc;
                        itemResult.Presupuestado = item.Presupuestado;
                        itemResult.Modificado = item.Modificado;
                        itemResult.Comprometido = item.Comprometido;
                        itemResult.Causado = item.Causado;
                        itemResult.Pagado = item.Pagado;
                        itemResult.Deuda = item.Deuda;
                        itemResult.Disponibilidad = item.Disponibilidad;
                        itemResult.DisponibilidadFinan = item.DisponibilidadFinan;

                        result.Add(itemResult);

                    }
                    else
                    {
                        getPreDenominacionPucResumenAnoDto.Presupuestado +=  item.Presupuestado;
                        getPreDenominacionPucResumenAnoDto.Modificado += item.Modificado;
                        getPreDenominacionPucResumenAnoDto.Comprometido += item.Comprometido;
                        getPreDenominacionPucResumenAnoDto.Causado += item.Causado;
                        getPreDenominacionPucResumenAnoDto.Pagado += item.Pagado;
                        getPreDenominacionPucResumenAnoDto.Deuda += item.Deuda;
                        getPreDenominacionPucResumenAnoDto.Disponibilidad += item.Disponibilidad;
                        getPreDenominacionPucResumenAnoDto.DisponibilidadFinan += item.DisponibilidadFinan;
                    }

                }

            }



            return result;



    }

        public GetPRE_V_DENOMINACION_PUCDto MapPRE_V_DENOMINACIONGetPRE_V_DENOMINACION_PUCDto(PRE_V_DENOMINACION_PUC entity)
        {
            GetPRE_V_DENOMINACION_PUCDto dto = new GetPRE_V_DENOMINACION_PUCDto();
            dto.AnoSaldo = entity.ANO_SALDO;
            dto.MesSaldo = entity.MES_SALDO;
            dto.CodigoPresupuesto = entity.CODIGO_PRESUPUESTO;
            dto.CodigoPartida = entity.CODIGO_PARTIDA;
            dto.CodigoGenerica = entity.CODIGO_GENERICA;
            dto.CodigoEspecifica = entity.CODIGO_ESPECIFICA;
            dto.CodigoSubEspecifica = entity.CODIGO_SUBESPECIFICA;
            dto.CodigoNivel5 = entity.CODIGO_NIVEL5;
            dto.DenominacionPuc = entity.DENOMINACION_PUC;
            dto.Presupuestado = entity.PRESUPUESTADO;
            dto.Modificado = entity.MODIFICADO;
            dto.Comprometido = entity.COMPROMETIDO;
            dto.Causado = entity.CAUSADO;
            dto.Modificado = entity.MODIFICADO;
            dto.Pagado = entity.PAGADO;
            dto.Deuda = entity.DEUDA;
            dto.Disponibilidad = entity.DISPONIBILIDAD;
            dto.DisponibilidadFinan = entity.DISPONIBILIDAD_FINAN;


            return dto;


        }

    }
}

