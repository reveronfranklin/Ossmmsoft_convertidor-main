using System;
using System.Collections.Generic;
using AutoMapper;
using Convertidor.Data;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Rh
{
	public class RhHistoricoMovimientoService: IRhHistoricoMovimientoService
    {
	

        private readonly DataContext _context;


        private readonly IRhHistoricoMovimientoRepository _repository;

        private readonly IMapper _mapper;

        public RhHistoricoMovimientoService(IRhHistoricoMovimientoRepository repository)
        {
            _repository = repository;

        }

        public async Task<List<ListHistoricoMovimientoDto>> GetByCodigoPersona(int codigoPersona)
        {
            try
            {
                
                List<ListHistoricoMovimientoDto> listHistoricoMovimientoDtos = new List<ListHistoricoMovimientoDto>();

                var historico = await _repository.GetByCodigoPersona(codigoPersona);
                listHistoricoMovimientoDtos=MapListHistoricoMovimiento(historico);
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
                listHistoricoMovimientoDtos = MapListHistoricoMovimiento(historico);
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
                listHistoricoMovimientoDtos = MapListHistoricoMovimiento(historico);
                return (List<ListHistoricoMovimientoDto>)listHistoricoMovimientoDtos;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        private List<ListHistoricoMovimientoDto> MapListHistoricoMovimiento(List<RH_V_HISTORICO_MOVIMIENTOS> dto)
        {
            List<ListHistoricoMovimientoDto> result = new List<ListHistoricoMovimientoDto>();
            foreach (var item in dto)
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
             
                itemResult.CodigoSector =item.CODIGO_SECTOR;
                itemResult.CodigoPrograma = item.CODIGO_PROGRAMA;
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
                result.Add(itemResult);
            }

            return result;

        }

    }
}

