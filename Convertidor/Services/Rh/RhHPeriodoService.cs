using System;
using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;
using Convertidor.Services.Rh;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class RhHPeriodoService: IRhHPeriodoService
    {
		
        private readonly DataContext _context;



   
        private readonly IRhHPeriodoRepository _repository;
        private readonly IRhTipoNominaRepository _rhTipoNominaRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhPeriodoRepository _rhPeriodoRepository;
        private readonly IMapper _mapper;

        public RhHPeriodoService(IRhHPeriodoRepository repository,
                                 IRhTipoNominaRepository rhTipoNominaRepository,
                                 ISisUsuarioRepository sisUsuarioRepository,
                                 IRhPeriodoRepository rhPeriodoService)
        {
            _repository = repository;
            _rhTipoNominaRepository = rhTipoNominaRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _rhPeriodoRepository = rhPeriodoService;
        }
       
        public async Task<List<RH_H_PERIODOS>> GetAll(PeriodoFilterDto filter)
        {
            try
            {
                var result = await _repository.GetAll(filter);
                return (List<RH_H_PERIODOS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<RH_H_PERIODOS>> GetByTipoNomina(int tipoNomina)
        {
            try
            {

                var result = await _repository.GetByTipoNomina(tipoNomina);
                return (List<RH_H_PERIODOS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<RhHPeriodosResponseDto>> GetByYear(int ano)
        {
            try
            {

                var result = await _repository.GetByYear(ano);
                var resultDto = MapListPeriodoDto(result);
                return resultDto;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public FechaDto GetFechaDto(DateTime fecha)
        {
            var FechaDesdeObj = new FechaDto();
            FechaDesdeObj.Year = fecha.Year.ToString();
            string month = "00" + fecha.Month.ToString();
            string day = "00" + fecha.Day.ToString();
            FechaDesdeObj.Month = month.Substring(month.Length - 2);
            FechaDesdeObj.Day = day.Substring(day.Length - 2);

            return FechaDesdeObj;
        }

        public async Task<RhHPeriodosResponseDto> MapHPeriodosDto(RH_H_PERIODOS dtos)
        {


            RhHPeriodosResponseDto itemResult = new RhHPeriodosResponseDto();
            itemResult.CodigoHPeriodo = dtos.CODIGO_H_PERIODO;
            itemResult.CodigoPeriodo = dtos.CODIGO_PERIODO;
            itemResult.FechaInsH = dtos.FECHA_INS_H;
            itemResult.UsuarioInsH = dtos.USUARIO_INS_H;
            itemResult.CodigoTipoNomina = dtos.CODIGO_TIPO_NOMINA;
            itemResult.FechaNomina = dtos.FECHA_NOMINA;
            itemResult.Periodo = dtos.PERIODO;
            itemResult.TipoNomina = dtos.TIPO_NOMINA;
            itemResult.EXTRA1 = dtos.EXTRA1;
            itemResult.EXTRA2 = dtos.EXTRA2;
            itemResult.EXTRA3 = dtos.EXTRA3;
            itemResult.UsuarioPreCierre = dtos.USUARIO_PRECIERRE;
            itemResult.FechaPreCierre = dtos.FECHA_PRECIERRE;
            itemResult.UsuarioCierre = dtos.USUARIO_CIERRE;
            itemResult.FechaCierre = dtos.FECHA_CIERRE;
            itemResult.CodigoCuentaEmpresa = dtos.CODIGO_CUENTA_EMPRESA;
            itemResult.UsuarioPreNomina = dtos.USUARIO_PRENOMINA;
            itemResult.FechaPrenomina = dtos.FECHA_PRENOMINA;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            itemResult.Descripcion = dtos.DESCRIPCION;
           



            return itemResult;



        }
        public List<RhHPeriodosResponseDto> MapListPeriodoDto(List<RH_H_PERIODOS> dtos)
        {
            List<RhHPeriodosResponseDto> result = new List<RhHPeriodosResponseDto>();

            foreach (var item in dtos)
            {

                RhHPeriodosResponseDto itemResult = new RhHPeriodosResponseDto();
                itemResult.CodigoHPeriodo = item.CODIGO_H_PERIODO;
                itemResult.CodigoPeriodo = item.CODIGO_PERIODO;
                itemResult.CodigoTipoNomina = item.CODIGO_TIPO_NOMINA;
                itemResult.FechaNomina = item.FECHA_NOMINA;
                itemResult.Periodo = item.PERIODO;
                itemResult.TipoNomina = item.TIPO_NOMINA;
                result.Add(itemResult);


            }
            return result;



        }

        public async Task<ResultDto<RhHPeriodosResponseDto>> Update(RhHPeriodosUpdate dto)
        {

            ResultDto<RhHPeriodosResponseDto> result = new ResultDto<RhHPeriodosResponseDto>(null);
            try
            {

                var hPeriodos = await _repository.GetByCodigo(dto.CodigoHPeriodo);
                if (hPeriodos == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Historico Periodo no existe";
                    return result;
                }

                var codigoPeriodos = await _rhPeriodoRepository.GetByCodigo(dto.CodigoPeriodo);
                if (codigoPeriodos == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Periodo invalido";
                    return result;
                }
                var codigoTipoNomina = await _rhTipoNominaRepository.GetByCodigo(dto.CodigoTipoNomina);
                if (codigoTipoNomina==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Tipo Nomina  Invalido";
                    return result;
                }
               
                FechaDto fechanomina = GetFechaDto(dto.FechaNomina);
                if (fechanomina==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Nomina Invalida";
                    return result;
                }
                
               if (dto.Periodo==null)
                  {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Periodo Invalido";
                    return result;
                  }
                
                if (dto.TipoNomina == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Nomina Invalido";
                    return result;
                }

                if (dto.EXTRA1 == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Extra1 Invalido";
                    return result;
                }

                if (dto.EXTRA2 == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Extra2 Invalido";
                    return result;
                }
                if (dto.EXTRA3 == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Extra3 Invalido";
                    return result;
                }
                if (dto.UsuarioPreCierre == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Usuario PreCierre Invalido";
                    return result;
                }
                if (dto.FechaPreCierre == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "FechaPrecierre invalida";
                    return result;
                }
                if (dto.UsuarioCierre == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Usuario Cierre Invalido";
                    return result;
                }
                if (dto.FechaCierre == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha cierre invalida";
                    return result;
                }
                if (dto.CodigoCuentaEmpresa == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo cuenta empresa Invalido";
                    return result;
                }
                if (dto.UsuarioPreNomina == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Usuario PreNomina invalido";
                    return result;
                }
                if (dto.FechaPrenomina == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha PreNomina Invalida";
                    return result;
                }
               
                if (dto.Descripcion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
                    return result;
                }

                hPeriodos.CODIGO_H_PERIODO = dto.CodigoHPeriodo;
                hPeriodos.CODIGO_PERIODO = dto.CodigoPeriodo;
                hPeriodos.CODIGO_TIPO_NOMINA = dto.CodigoTipoNomina;
                hPeriodos.FECHA_NOMINA = dto.FechaNomina;
                hPeriodos.PERIODO = dto.Periodo;
                hPeriodos.TIPO_NOMINA = dto.TipoNomina;
                hPeriodos.EXTRA1 = dto.EXTRA1;
                hPeriodos.EXTRA2 = dto.EXTRA2;
                hPeriodos.EXTRA3 = dto.EXTRA3;
                hPeriodos.USUARIO_CIERRE = dto.UsuarioCierre;
                hPeriodos.FECHA_CIERRE = dto.FechaCierre;
                hPeriodos.USUARIO_PRECIERRE = dto.UsuarioPreCierre;
                hPeriodos.FECHA_PRECIERRE=dto.FechaPreCierre;
                hPeriodos.CODIGO_CUENTA_EMPRESA = dto.CodigoCuentaEmpresa;
                hPeriodos.USUARIO_PRENOMINA = dto.UsuarioPreNomina;
                hPeriodos.FECHA_PRENOMINA = dto.FechaPrenomina;
                hPeriodos.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                hPeriodos.DESCRIPCION = dto.Descripcion;


                var conectado = await _sisUsuarioRepository.GetConectado();
                hPeriodos.CODIGO_EMPRESA = conectado.Empresa;
                hPeriodos.USUARIO_UPD = conectado.Usuario;
                hPeriodos.FECHA_UPD = DateTime.Now;
                


                await _repository.Update(hPeriodos);


               

                var resultDto = await MapHPeriodosDto(hPeriodos);
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

        public async Task<ResultDto<RhHPeriodosResponseDto>> Create(RhHPeriodosUpdate dto)
        {

            ResultDto<RhHPeriodosResponseDto> result = new ResultDto<RhHPeriodosResponseDto>(null);
            try
            {

                var hPeriodos = await _repository.GetByCodigo(dto.CodigoHPeriodo);
                if (hPeriodos is not null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo historico Periodo invalido";
                    return result;
                }
                var codigoPeriodos = await _rhPeriodoRepository.GetByCodigo(dto.CodigoPeriodo);
                if (codigoPeriodos == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Periodo invalido";
                    return result;
                }
                var codigoTipoNomina = await _rhTipoNominaRepository.GetByCodigo(dto.CodigoTipoNomina);
                if (codigoTipoNomina == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Tipo Nomina Invalido";
                    return result;
                }

                FechaDto fechanomina = GetFechaDto(dto.FechaNomina);
                if (fechanomina == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Invalida";
                    return result;
                }

                if (dto.Periodo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Periodo Invalido";
                    return result;
                }

                
                if (dto.TipoNomina == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Nomina Invalido";
                    return result;
                }

                if (dto.EXTRA1 == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Extra1 Invalido";
                    return result;
                }

                if (dto.EXTRA2 == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Extra2 Invalido";
                    return result;
                }
                if (dto.EXTRA3 == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Extra3 Invalido";
                    return result;
                }
                if (dto.UsuarioPreCierre == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Usuario PreCierre Invalido";
                    return result;
                }
                if (dto.FechaPreCierre == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "FechaPrecierre invalida";
                    return result;
                }
                if (dto.UsuarioCierre == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Usuario Cierre Invalido";
                    return result;
                }
                if (dto.FechaCierre == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha cierre invalida";
                    return result;
                }
                if (dto.CodigoCuentaEmpresa == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo cuenta empresa Invalido";
                    return result;
                }
                if (dto.UsuarioPreNomina == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Usuario PreNomina invalido";
                    return result;
                }
                if (dto.FechaPrenomina == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha PreNomina Invalida";
                    return result;
                }

                if (dto.Descripcion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
                    return result;
                }

                RH_H_PERIODOS entity = new RH_H_PERIODOS();
                entity.CODIGO_H_PERIODO = await _repository.GetNextKey();
                entity.CODIGO_PERIODO = dto.CodigoPeriodo;
                entity.CODIGO_TIPO_NOMINA = dto.CodigoTipoNomina;
                entity.FECHA_NOMINA = dto.FechaNomina;
                entity.PERIODO = dto.Periodo;
                entity.TIPO_NOMINA = dto.TipoNomina;
                entity.EXTRA1 = dto.EXTRA1;
                entity.EXTRA2 = dto.EXTRA2;
                entity.EXTRA3 = dto.EXTRA3;
                entity.USUARIO_CIERRE = dto.UsuarioCierre;
                entity.FECHA_CIERRE = dto.FechaCierre;
                entity.USUARIO_PRECIERRE = dto.UsuarioPreCierre;
                entity.FECHA_PRECIERRE = dto.FechaPreCierre;
                entity.CODIGO_CUENTA_EMPRESA = dto.CodigoCuentaEmpresa;
                entity.USUARIO_PRENOMINA = dto.UsuarioPreNomina;
                entity.FECHA_PRENOMINA = dto.FechaPrenomina;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                entity.DESCRIPCION = dto.Descripcion;


                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;
                entity.USUARIO_INS_H = conectado.Usuario;
                entity.FECHA_INS_H = DateTime.Now;


                var created = await _repository.Add(entity);

                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapHPeriodosDto(created.Data);
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

        public async Task<ResultDto<RhHPeriodosDeleteDto>> Delete(RhHPeriodosDeleteDto dto)
        {

            ResultDto<RhHPeriodosDeleteDto> result = new ResultDto<RhHPeriodosDeleteDto>(null);
            try
            {

                var periodo = await _repository.GetByCodigo(dto.CodigoHPeriodo);
                if (periodo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Periodo no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoHPeriodo);

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

