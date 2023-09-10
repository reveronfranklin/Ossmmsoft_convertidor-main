using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using Convertidor.Data;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Data.Repository.Rh;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;
using Convertidor.Utility;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using static NPOI.HSSF.Util.HSSFColor;

namespace Convertidor.Services.Rh
{
	public class RhHistoricoMovimientoService: IRhHistoricoMovimientoService
    {
	

       


        private readonly IRhHistoricoMovimientoRepository _repository;


        private readonly IRhDescriptivasService _descriptivaServices;
        private readonly IRhProcesoDetalleRepository _rhProcesoDetalleRepository;
        private readonly IRhConceptosRepository _rhConceptosRepository;
        private readonly IRhTipoNominaService _tipoNominaService;

        public RhHistoricoMovimientoService(IRhHistoricoMovimientoRepository repository,
                                            IRhDescriptivasService descriptivaServices,
                                            IRhProcesoDetalleRepository rhProcesoDetalleRepository,
                                            IRhConceptosRepository rhConceptosRepository,
                                            IRhTipoNominaService tipoNominaService)
        {
            _repository = repository;
            _descriptivaServices = descriptivaServices;
            _rhProcesoDetalleRepository = rhProcesoDetalleRepository;
            _rhConceptosRepository = rhConceptosRepository;
            _tipoNominaService = tipoNominaService;

        }


        public async Task<List<ListHistoricoMovimientoDto>> GetByProceso(FilterHistoricoNominaPeriodo filter)
        {
            List<ListHistoricoMovimientoDto> result = new List<ListHistoricoMovimientoDto>();

            if (filter.CodigoProceso > 0)
            {
                var conceptosProceso = await _rhProcesoDetalleRepository.GetByCodigoProceso(filter.CodigoProceso);
                List<ListConceptosDto> resultConcepto = new List<ListConceptosDto>();
                foreach (var item in conceptosProceso)
                {
                    ListConceptosDto itemConcepto = new ListConceptosDto();
                    var concepto = await _rhConceptosRepository.GetByCodigo(item.CODIGO_CONCEPTO);
                    itemConcepto.Codigo = concepto.CODIGO;
                    itemConcepto.CodigoConcepto = concepto.CODIGO_CONCEPTO;
                    itemConcepto.CodigoTipoNomina = concepto.CODIGO_TIPO_NOMINA;
                    itemConcepto.Denominacion = concepto.DENOMINACION;
                    resultConcepto.Add(itemConcepto);
                }

                filter.CodigoConcepto = resultConcepto;
                result = await GetByFechaNominaPersona(filter.Desde, filter.Hasta, filter.CodigoPersona);
                if (filter.CodigoConcepto.Count > 0)
                {
                    List<ListHistoricoMovimientoDto> resultPorConcepto = new List<ListHistoricoMovimientoDto>();
                    foreach (var item in filter.CodigoConcepto)
                    {

                        var concepto = result.Where(x => x.CodigoTipoNomina == item.CodigoTipoNomina && x.Codigo.Trim() == item.Codigo.Trim()).ToList();
                        if (concepto.Count > 0) resultPorConcepto.AddRange(concepto);
                    }
                    result = resultPorConcepto;

                }
            }
            return result;

        }

        public async Task<List<ListHistoricoMovimientoDto>> GetByIndividual(FilterHistoricoNominaPeriodo filter)
        {
            List<ListHistoricoMovimientoDto> result = new List<ListHistoricoMovimientoDto>();

            if (filter.CodigoPersona > 0)
            {
                result = await GetByFechaNominaPersona(filter.Desde, filter.Hasta, filter.CodigoPersona);

                if (filter.CodigoTipoNomina.Count > 0)
                {

                    List<ListHistoricoMovimientoDto> resultTipoNomina = new List<ListHistoricoMovimientoDto>();
                    foreach (var item in filter.CodigoTipoNomina)
                    {

                        var tipoNomina = result.Where(x => x.CodigoTipoNomina == item.CodigoTipoNomina).ToList();
                        if (tipoNomina.Count > 0) resultTipoNomina.AddRange(tipoNomina);
                    }
                    result = resultTipoNomina;


                }

                if (filter.CodigoConcepto.Count > 0)
                {
                    List<ListHistoricoMovimientoDto> resultConcepto = new List<ListHistoricoMovimientoDto>();
                    foreach (var item in filter.CodigoConcepto)
                    {

                        var concepto = result.Where(x => x.CodigoTipoNomina == item.CodigoTipoNomina && x.Codigo.Trim() == item.Codigo.Trim()).ToList();
                        if (concepto.Count > 0) resultConcepto.AddRange(concepto);
                    }
                    result = resultConcepto;

                }

            }
            return result;

        }

        public async Task<List<ListHistoricoMovimientoDto>> GetByMasivo(FilterHistoricoNominaPeriodo filter)
        {
            List<ListHistoricoMovimientoDto> result = new List<ListHistoricoMovimientoDto>();

            result = await GetByFechaNomina(filter.Desde, filter.Hasta);

            if (filter.CodigoTipoNomina.Count == 0)
            {
                var tiposNomina = await _tipoNominaService.GetAll();
                if (tiposNomina.Count > 0)
                {
                    var firstTipo = tiposNomina.FirstOrDefault();
                    if (firstTipo != null)
                    {
                        filter.CodigoTipoNomina.Add(firstTipo);
                    }

                }
            }


            if (filter.CodigoTipoNomina.Count > 0)
            {

                List<ListHistoricoMovimientoDto> resultTipoNomina = new List<ListHistoricoMovimientoDto>();
                foreach (var item in filter.CodigoTipoNomina)
                {

                    var tipoNomina = result.Where(x => x.CodigoTipoNomina == item.CodigoTipoNomina).ToList();
                    if (tipoNomina.Count > 0) resultTipoNomina.AddRange(tipoNomina);
                }
                result = resultTipoNomina;


            }

            if (filter.CodigoConcepto.Count > 0)
            {
                List<ListHistoricoMovimientoDto> resultConcepto = new List<ListHistoricoMovimientoDto>();
                foreach (var item in filter.CodigoConcepto)
                {
                    var concepto = result.Where(x => x.CodigoTipoNomina == item.CodigoTipoNomina && x.Codigo.Trim() == item.Codigo.Trim()).ToList();
                    if (concepto.Count > 0) resultConcepto.AddRange(concepto);
                }
                result = resultConcepto;

            }
            return result;

        }


        public async Task<RH_V_HISTORICO_MOVIMIENTOS> GetPrimerMovimientoByCodigoPersona(int codigoPersona)
        {
            try
            {

                var result = await _repository.GetPrimerMovimientoByCodigoPersona(codigoPersona);
                return (RH_V_HISTORICO_MOVIMIENTOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }
        public async Task<List<ListHistoricoMovimientoDto>> GetByCodigoPersona(int codigoPersona)
        {
            try
            {
                
                List<ListHistoricoMovimientoDto> listHistoricoMovimientoDtos = new List<ListHistoricoMovimientoDto>();

                var historico = await _repository.GetByCodigoPersona(codigoPersona);
                listHistoricoMovimientoDtos=await MapListHistoricoMovimiento(historico);
                return (List<ListHistoricoMovimientoDto>)listHistoricoMovimientoDtos;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<ListHistoricoMovimientoDto>> GetByTipoNominaPeriodo(int tipoNomina,int codigoPeriodo)
        {
            try
            {

                List<ListHistoricoMovimientoDto> listHistoricoMovimientoDtos = new List<ListHistoricoMovimientoDto>();

                var historico = await _repository.GetByTipoNominaPeriodo(tipoNomina, codigoPeriodo);
                listHistoricoMovimientoDtos = await MapListHistoricoMovimiento(historico);
                return (List<ListHistoricoMovimientoDto>)listHistoricoMovimientoDtos;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<ListHistoricoMovimientoDto>> GetByFechaNomina(DateTime desde, DateTime hasta)
        {
            try
            {

                List<ListHistoricoMovimientoDto> listHistoricoMovimientoDtos = new List<ListHistoricoMovimientoDto>();

                var historico = await _repository.GetByFechaNomina(desde, hasta);
                listHistoricoMovimientoDtos =await  MapListHistoricoMovimiento(historico);
                return (List<ListHistoricoMovimientoDto>)listHistoricoMovimientoDtos;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<ListHistoricoMovimientoDto>> GetByFechaNominaPersona(DateTime desde, DateTime hasta,int idPersona)
        {
            try
            {

                List<ListHistoricoMovimientoDto> listHistoricoMovimientoDtos = new List<ListHistoricoMovimientoDto>();

                var historico = await _repository.GetByFechaNominaPersona(desde, hasta,idPersona);
                listHistoricoMovimientoDtos = await MapListHistoricoMovimiento(historico);
                return (List<ListHistoricoMovimientoDto>)listHistoricoMovimientoDtos;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        private RH_DESCRIPTIVAS GetDescriptiva(List<RH_DESCRIPTIVAS> list,string? codigo) {
            var descriptivas = list.Where(d => d.CODIGO == codigo).First();

            return descriptivas;
        }

        private async Task<List<ListHistoricoMovimientoDto>> MapListHistoricoMovimiento(List<RH_V_HISTORICO_MOVIMIENTOS> dto)
        {
            
            List<ListHistoricoMovimientoDto> result = new List<ListHistoricoMovimientoDto>();
            var descriptivas =await _descriptivaServices.GetAll();
            try
            {
                var resultNew = dto

                 .Select(e => new ListHistoricoMovimientoDto
                 {
                     CodigoHistoricoNomina = e.CODIGO_HISTORICO_NOMINA,
                     CodigoPersona = e.CODIGO_PERSONA,
                     Cedula = e.CEDULA,
                     Foto = e.FOTO,
                     Nombre = e.NOMBRE,
                     Apellido = e.APELLIDO,
                     Full_Name = $"{e.NOMBRE} {e.APELLIDO}",
                     Nacionalidad = e.NACIONALIDAD,
                     DescripcionNacionalidad = e.DESCRIPCION_NACIONALIDAD,
                     Sexo = e.SEXO,
                     Status = e.STATUS,
                     DescripcionStatus = e.DESCRIPCION_STATUS,
                     CodigoRelacionCargo = e.CODIGO_RELACION_CARGO,
                     CodigoCargo = e.CODIGO_CARGO,
                     CargoCodigo = e.CARGO_CODIGO,
                     CodigoIcp = e.CODIGO_ICP,
                     Sueldo = e.SUELDO,
                     DescripcionCargo = e.DESCRIPCION_CARGO,
                     CodigoTipoNomina = e.CODIGO_TIPO_NOMINA,
                     TipoNomina = e.TIPO_NOMINA,
                     TipoCuentaId = e.TIPO_CUENTA_ID,
                     DescripcionTipoCuenta = e.DESCRIPCION_TIPO_CUENTA,
                     BancoId = e.BANCO_ID,
                     DescripcionBanco = e.DESCRIPCION_BANCO,
                     NoCuenta = e.NO_CUENTA,
                     FechaNominaMov = e.FECHA_NOMINA_MOV.ToShortDateString(),
                     FechaNomina=e.FECHA_NOMINA_MOV,
                     Complemento = e.COMPLEMENTO,
                     Tipo = e.TIPO,
                     Monto = e.MONTO,
                     StatusMov = e.ESTATUS_MOV,
                     Codigo = e.CODIGO, 
                     Denominacion = e.DENOMINACION,
                     CodigoPeriodo = (int)e.CODIGO_PERIODO,
                     Avatar = "",
                     UnidadEjecutora = e.UNIDAD_EJECUTORA,
                     EstadoCivil = e.ESTADO_CIVIL

                 });
                    result = resultNew.ToList();
             
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
           



         
         

        }

    }
}

