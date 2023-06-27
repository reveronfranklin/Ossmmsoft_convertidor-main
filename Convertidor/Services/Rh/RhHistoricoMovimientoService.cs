using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using Convertidor.Data;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Dtos.Rh;
using NPOI.SS.Formula.Functions;
using static NPOI.HSSF.Util.HSSFColor;

namespace Convertidor.Services.Rh
{
	public class RhHistoricoMovimientoService: IRhHistoricoMovimientoService
    {
	

       


        private readonly IRhHistoricoMovimientoRepository _repository;


        private readonly IRhDescriptivasService _descriptivaServices;

        public RhHistoricoMovimientoService(IRhHistoricoMovimientoRepository repository, IRhDescriptivasService descriptivaServices)
        {
            _repository = repository;
            _descriptivaServices = descriptivaServices;

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
                     Complemento = e.COMPLEMENTO,
                     Tipo = e.TIPO,
                     Monto = e.MONTO,
                     StatusMov = e.ESTATUS_MOV,
                     Codigo = e.CODIGO,
                     Denominacion = e.DENOMINACION,
                     CodigoPeriodo = (int)e.CODIGO_PERIODO,
                     Avatar = "",
                     UnidadEjecutora = e.UNIDAD_EJECUTORA,
                     EstadoCivil = "" //descriptivas.Where(d => d.CODIGO == (e.ESTADO_CIVIL ?? "0")).First().DESCRIPCION

                 });
                result = resultNew.ToList();

                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
           



           /* foreach (var item in dto)
            {
                try
                {
                    ListHistoricoMovimientoDto itemResult = new ListHistoricoMovimientoDto();
                    itemResult.CodigoHistoricoNomina = item.CODIGO_HISTORICO_NOMINA;
                    itemResult.CodigoPersona = item.CODIGO_PERSONA;
                    itemResult.Cedula = item.CEDULA;
                    itemResult.Foto = item.FOTO;
                    itemResult.Nombre = item.NOMBRE;
                    itemResult.Apellido = item.APELLIDO;
                    itemResult.Full_Name = $"{item.NOMBRE} {item.APELLIDO}";
                    itemResult.Nacionalidad = item.NACIONALIDAD;
                    itemResult.DescripcionNacionalidad = item.DESCRIPCION_NACIONALIDAD;
                    itemResult.Sexo = item.SEXO;
                    itemResult.Status = item.STATUS;
                    itemResult.DescripcionStatus = item.DESCRIPCION_STATUS;

                    itemResult.CodigoRelacionCargo = item.CODIGO_RELACION_CARGO;
                    itemResult.CodigoCargo = item.CODIGO_CARGO;
                    itemResult.CargoCodigo = item.CARGO_CODIGO;
                    itemResult.CodigoIcp = item.CODIGO_ICP;

                    itemResult.Sueldo = item.SUELDO;
                    itemResult.DescripcionCargo = item.DESCRIPCION_CARGO;
                    itemResult.CodigoTipoNomina = item.CODIGO_TIPO_NOMINA;
                    itemResult.TipoNomina = item.TIPO_NOMINA;

                    itemResult.TipoCuentaId = item.TIPO_CUENTA_ID;
                    itemResult.DescripcionTipoCuenta = item.DESCRIPCION_TIPO_CUENTA;
                    itemResult.BancoId = item.BANCO_ID;
                    itemResult.DescripcionBanco = item.DESCRIPCION_BANCO;
                    itemResult.NoCuenta = item.NO_CUENTA;


                    itemResult.FechaNominaMov = item.FECHA_NOMINA_MOV.ToShortDateString();
                    itemResult.Complemento = item.COMPLEMENTO;
                    itemResult.Tipo = item.TIPO;
                    itemResult.Monto = item.MONTO;
                    itemResult.StatusMov = item.ESTATUS_MOV;
                    itemResult.Codigo = item.CODIGO;
                    itemResult.Denominacion = item.DENOMINACION;
                    itemResult.CodigoPeriodo = (int)item.CODIGO_PERIODO;
                    itemResult.Avatar = "";
                    itemResult.UnidadEjecutora = item.UNIDAD_EJECUTORA;
                    if (item.ESTADO_CIVIL == null)
                    {
                        item.ESTADO_CIVIL = "0";
                    }
                    int idStadoCivil = Int32.Parse(item.ESTADO_CIVIL);
                    itemResult.EstadoCivil = await _descriptivaServices.GetDescripcionByCodigoDescriptiva(idStadoCivil);

                    result.Add(itemResult);

                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
               
            }*/

         

        }

    }
}

