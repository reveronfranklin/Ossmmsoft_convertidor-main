using System;
using AutoMapper;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Services.Presupuesto
{
	public class PRE_V_SALDOSServices: IPRE_V_SALDOSServices
    {
       

        private readonly IPRE_V_SALDOSRepository _repository;

        private readonly IMapper _mapper;

        public PRE_V_SALDOSServices(IPRE_V_SALDOSRepository repository,

                                      IMapper mapper)
        {
            _repository = repository;

            _mapper = mapper;
        }

        public async Task<List<PreVSaldosGetDto>> GetAll(FilterPRE_V_SALDOSDto filter)
        {
            try
            {
                List<PreVSaldosGetDto> result = new List<PreVSaldosGetDto>();
                var pRE_V_SALDOs = await _repository.GetAll(filter);
                foreach (var item in pRE_V_SALDOs)
                {

                    result.Add(MapPRE_V_SADOSTOPreVSaldosGetDto(item));
                }


                return (List<PreVSaldosGetDto>)result;
            }
            catch (Exception ex)
            {

                return null;
            }

        }


        public PreVSaldosGetDto MapPRE_V_SADOSTOPreVSaldosGetDto(PRE_V_SALDOS entity)
        {
            PreVSaldosGetDto dto = new PreVSaldosGetDto();
            dto.CodigoSaldo=entity.CODIGO_SALDO;
            dto.Ano = entity.ANO;
            dto.FinanciadoId=entity.FINANCIADO_ID;
            dto.CodigoFinanciado=entity.CODIGO_FINANCIADO;
            dto.DescripcionFinanciado=entity.DESCRIPCION_FINANCIADO;
            dto.CodigiIcp=entity.CODIGO_ICP;
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

