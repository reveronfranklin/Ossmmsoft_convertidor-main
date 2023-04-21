using System;
using AutoMapper;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository.Presupuesto;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Services.Presupuesto
{
	public class PRE_V_SALDOSServices: IPRE_V_SALDOSServices
    {
       

        private readonly IPRE_V_SALDOSRepository _repository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;

        private readonly IMapper _mapper;

        public PRE_V_SALDOSServices(IPRE_V_SALDOSRepository repository,
                                    IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository,

                                      IMapper mapper)
        {
            _repository = repository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;

            _mapper = mapper;
        }
        
        public async Task<ResultDto<List<PreVSaldosGetDto>>> GetAllByPresupuestoIpcPuc(FilterPresupuestoIpcPuc filter)
        {
            var presupuesto = await _pRE_PRESUPUESTOSRepository.GetLast();
            //await _repository.RecalcularSaldo(presupuesto.CODIGO_PRESUPUESTO);
            if (filter.CodigoPresupuesto == 0) filter.CodigoPresupuesto = presupuesto.CODIGO_PRESUPUESTO;
            ResultDto<List<PreVSaldosGetDto>> result = new ResultDto<List<PreVSaldosGetDto>>(null);
            try
            {
                var preVSaldos = await _repository.GetAllByPresupuesto(filter.CodigoPresupuesto);
                if (preVSaldos.Count() > 0)
                {
                    if (filter.CodigoIPC>0) {
                        preVSaldos = preVSaldos.Where(x => x.CODIGO_ICP == filter.CodigoIPC).ToList();
                    }
                    if (filter.CodigoPuc > 0)
                    {
                        preVSaldos = preVSaldos.Where(x => x.CODIGO_PUC == filter.CodigoPuc).ToList();
                    }
                    List<PreVSaldosGetDto> resultList = new List<PreVSaldosGetDto>();
                    foreach (var item in preVSaldos)
                    {

                        resultList.Add(MapPRE_V_SADOSTOPreVSaldosGetDto(item));
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

        public async Task<ResultDto<List<PreVSaldosGetDto>>> GetAll(FilterPRE_V_SALDOSDto filter)
        {
            var presupuesto = await _pRE_PRESUPUESTOSRepository.GetLast();
            await _repository.RecalcularSaldo(presupuesto.CODIGO_PRESUPUESTO);

            ResultDto<List<PreVSaldosGetDto>> result = new ResultDto<List<PreVSaldosGetDto>>(null);
            try
            {
                var pRE_V_SALDOs = await _repository.GetAll(filter);
                if (pRE_V_SALDOs.Count() > 0)
                {

                    List<PreVSaldosGetDto> resultList = new List<PreVSaldosGetDto>();
                    foreach (var item in pRE_V_SALDOs)
                    {

                        resultList.Add(MapPRE_V_SADOSTOPreVSaldosGetDto(item));
                    }


                    var groupByDescripcionFinanciado =
                        from item in resultList
                        group item by item.DescripcionFinanciado;
                    var groupByDenominacionIcp =
                       from item in resultList
                       group item by item.DenominacionIcp;
                    var groupByunidadEjecutora =
                                           from item in resultList
                                           group item by item.UnidadEjecutora;

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

        public PreVSaldosGetDto MapPRE_V_SADOSTOPreVSaldosGetDto(PRE_V_SALDOS entity)
        {
            PreVSaldosGetDto dto = new PreVSaldosGetDto();
            dto.CodigoSaldo=entity.CODIGO_SALDO;
            dto.Ano = entity.ANO;
            dto.FinanciadoId=entity.FINANCIADO_ID;
            dto.CodigoFinanciado=entity.CODIGO_FINANCIADO;
            dto.DescripcionFinanciado=entity.DESCRIPCION_FINANCIADO;
            dto.CodigoIcp=entity.CODIGO_ICP;
            dto.CodigoSector=entity.CODIGO_SECTOR;
            dto.CodigoPrograma=entity.CODIGO_PROGRAMA;
            dto.CodigoSubPrograma=entity.CODIGO_SUBPROGRAMA;
            dto.CodigoProyecto=entity.CODIGO_PROYECTO;
            dto.CodigoActividad=entity.CODIGO_ACTIVIDAD;
            dto.CodigoOficina=entity.CODIGO_OFICINA;
            dto.CodigoIcpConcat=entity.CODIGO_ICP_CONCAT;
            dto.DenominacionIcp=entity.DENOMINACION_ICP;
            dto.UnidadEjecutora=entity.UNIDAD_EJECUTORA;
            dto.CodigoPuc=entity.CODIGO_PUC;
            dto.CodigoGrupo=entity.CODIGO_GRUPO;
            dto.CodigoPartida=entity.CODIGO_PARTIDA;
            dto.Codigogenerica=entity.CODIGO_GENERICA;
            dto.CodigoEspecifica=entity.CODIGO_ESPECIFICA;
            dto.CodigoSubEspecifica=entity.CODIGO_SUBESPECIFICA;
            dto.CodigoNivel5=entity.CODIGO_NIVEL5;
            dto.CodigoPucConcat=entity.CODIGO_PUC_CONCAT;
            dto.DenominacionPuc=entity.DENOMINACION_PUC;
            dto.Presupuestado=entity.PRESUPUESTADO;
            dto.Asignacion=entity.ASIGNACION;
            dto.Bloqueado=entity.BLOQUEADO;
            dto.Modificado=entity.MODIFICADO;
            dto.Ajustado=entity.AJUSTADO;
            dto.Vigente=entity.VIGENTE;
            dto.Comprometido=entity.COMPROMETIDO;
            dto.PorComprometido=entity.POR_COMPROMETIDO;
            dto.Disponoble=entity.DISPONIBLE;
            dto.Causado=entity.CAUSADO;
            dto.PorCausado=entity.POR_CAUSADO;
            dto.Pagado=entity.PAGADO;
            dto.PorPagado=entity.POR_PAGADO;
            dto.CodigoEmpresa=entity.CODIGO_EMPRESA;
            dto.CodigoPresupuesto=entity.CODIGO_PRESUPUESTO;
            dto.FechaSolicitud=entity.FECHA_SOLICITUD;


            return dto;


        }

    }
}

