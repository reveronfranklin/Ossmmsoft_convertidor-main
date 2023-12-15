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
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Rh;
using Convertidor.Services.Rh;
using Convertidor.Utility;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class RhExpLaboralService : IRhExpLaboralService
    {
        
   
        private readonly IRhExpLaboralRepository _repository;
        private readonly IRhDescriptivasService _descriptivaService;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhPersonasRepository _rhPersonasRepository;
        private readonly IRhDocumentosRepository _rhDocumentosRepository;

        public RhExpLaboralService(IRhExpLaboralRepository repository, 
                                        IRhDescriptivasService descriptivaService, 
                                        ISisUsuarioRepository sisUsuarioRepository,
                                        IRhPersonasRepository rhPersonasRepository
                                        )
        {
            _repository = repository;
            _descriptivaService = descriptivaService;
            _sisUsuarioRepository = sisUsuarioRepository;
            _rhPersonasRepository = rhPersonasRepository;
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

        public async Task<List<RhExpLaboralResponseDto>> GetByCodigoPersona(int codigoPersona)
        {
            try
            {

                var ExpLaboral = await _repository.GetByCodigoPersona(codigoPersona);

                var result = await MapListRhExpLaboralDto(ExpLaboral);


                return (List<RhExpLaboralResponseDto>)result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }


        public async  Task<RhExpLaboralResponseDto> MapRhExpLaboralDto(RH_EXP_LABORAL dtos)
        {


                RhExpLaboralResponseDto itemResult = new RhExpLaboralResponseDto();
                itemResult.CodigoExpLaboral = dtos.CODIGO_EXP_LABORAL;
                itemResult.CodigoPersona = dtos.CODIGO_PERSONA;
                itemResult.NombreEmpresa = dtos.NOMBRE_EMPRESA;
                itemResult.TipoEmpresa = dtos.TIPO_EMPRESA;
                if (dtos.RAMO_ID == null) dtos.RAMO_ID = 0;
                itemResult.RamoId = dtos.RAMO_ID;
                itemResult.Cargo = dtos.CARGO;
                itemResult.FechaDesde = dtos.FECHA_DESDE;
                itemResult.FechaHasta = dtos.FECHA_HASTA;
                itemResult.FechaDesdeString = dtos.FECHA_DESDE.ToString("u"); 
                itemResult.FechaHastaString = dtos.FECHA_HASTA.ToString("u"); 
                FechaDto FechaDesdeObj = GetFechaDto(dtos.FECHA_DESDE);
                itemResult.FechaDesdeObj = (FechaDto)FechaDesdeObj;
                FechaDto FechaHastaObj = GetFechaDto(dtos.FECHA_HASTA);
                itemResult.FechaHastaObj = FechaHastaObj;
                itemResult.UltimoSueldo = dtos.ULTIMO_SUELDO;
                if(dtos.SUPERVISOR == null)  dtos.SUPERVISOR = "";
                itemResult.Supervisor = dtos.SUPERVISOR;
                if (dtos.CARGO_SUPERVISOR == null) dtos.CARGO_SUPERVISOR = "";
                itemResult.CargoSupervisor = dtos.CARGO_SUPERVISOR;
                itemResult.Telefono = dtos.TELEFONO;
                if (dtos.SUPERVISOR == null) dtos.SUPERVISOR = "";
                itemResult.Supervisor = dtos.SUPERVISOR;
                if (dtos.DESCRIPCION == null) dtos.DESCRIPCION = "";
                itemResult.Descripcion = dtos.DESCRIPCION;
             


            return itemResult;

        }

        public async Task<List<RhExpLaboralResponseDto>> MapListRhExpLaboralDto(List<RH_EXP_LABORAL> dtos)
        {
            List<RhExpLaboralResponseDto> result = new List<RhExpLaboralResponseDto>();


            foreach (var item in dtos)
            {

                RhExpLaboralResponseDto itemResult = new RhExpLaboralResponseDto();

                itemResult = await MapRhExpLaboralDto(item);

                result.Add(itemResult);
            }
            return result;



        }


        public async Task<ResultDto<RhExpLaboralResponseDto>> Update(RhExpLaboralUpdateDto dto)
        {

            ResultDto<RhExpLaboralResponseDto> result = new ResultDto<RhExpLaboralResponseDto>(null);
            try
            {
                var codigoExpLaboral = await _repository.GetByCodigo(dto.CodigoExpLaboral);
                if (codigoExpLaboral == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No existe el empleado";
                    return result;
                }


                var codigoPersona = await _rhPersonasRepository.GetCodigoPersona(dto.CodigoPersona);
                if (codigoPersona == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No existe La persona";
                    return result;
                }
                if (dto.NombreEmpresa == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nombre Empresa Invalido";
                    return result;
                }
                if (dto.TipoEmpresa == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Empresa Invalido";
                    return result;
                }
                if (dto.RamoId == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ramo Invalido";
                    return result;
                }
                if (dto.Cargo == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cargo Invalido";
                    return result;
                }
                if (!DateValidate.IsDate(dto.FechaDesde.ToShortDateString()))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha inicial Invalida";
                    result.LinkData = "";
                    return result;
                }
                if (!DateValidate.IsDate(dto.FechaHasta.ToShortDateString()))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Final Invalida";
                    result.LinkData = "";
                    return result;
                }

                if (dto.UltimoSueldo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ultimo sueldo Invalido";
                    return result;
                }
                if (dto.Supervisor == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Supervisor Invalido";
                    return result;
                }
                if (dto.CargoSupervisor == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cargo Supervisor Invalido";
                    return result;
                }
                if (dto.Telefono == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Telefono Invalido";
                    return result;
                }
                if (dto.Descripcion == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
                    return result;
                }


                codigoExpLaboral.CODIGO_EXP_LABORAL = dto.CodigoExpLaboral;
                codigoExpLaboral.NOMBRE_EMPRESA = dto.NombreEmpresa;
                codigoExpLaboral.TIPO_EMPRESA = dto.TipoEmpresa;
                codigoExpLaboral.RAMO_ID = dto.RamoId;
                codigoExpLaboral.CARGO = dto.Cargo;
                codigoExpLaboral.FECHA_DESDE = dto.FechaDesde;
                codigoExpLaboral.FECHA_HASTA = dto.FechaHasta;
                codigoExpLaboral.ULTIMO_SUELDO = dto.UltimoSueldo;
                codigoExpLaboral.SUPERVISOR = dto.Supervisor;
                codigoExpLaboral.CARGO_SUPERVISOR = dto.CargoSupervisor;
                codigoExpLaboral.TELEFONO = dto.Telefono;
                codigoExpLaboral.DESCRIPCION = dto.Descripcion;



                var conectado = await _sisUsuarioRepository.GetConectado();
                codigoExpLaboral.CODIGO_EMPRESA = conectado.Empresa;
                codigoExpLaboral.USUARIO_UPD = conectado.Usuario.ToString();

                await _repository.Update(codigoExpLaboral);



                var resultDto = await MapRhExpLaboralDto(codigoExpLaboral);
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

        public async Task<ResultDto<RhExpLaboralResponseDto>> Create(RhExpLaboralUpdateDto dto)
        {

            ResultDto<RhExpLaboralResponseDto> result = new ResultDto<RhExpLaboralResponseDto>(null);
            try
            {
                var codigoPersona = await _rhPersonasRepository.GetCodigoPersona(dto.CodigoPersona);
                if (codigoPersona == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No existe La persona";
                    return result;
                }
                if(dto.NombreEmpresa==string.Empty) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nombre Empresa Invalido";
                    return result;
                }
                if (dto.TipoEmpresa == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Empresa Invalido";
                    return result;
                }
                if (dto.RamoId == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ramo Invalido";
                    return result;
                }
                if (dto.Cargo == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cargo Invalido";
                    return result;
                }
                if (!DateValidate.IsDate(dto.FechaDesde.ToShortDateString()))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha inicial Invalida";
                    result.LinkData = "";
                    return result;
                }
                if (!DateValidate.IsDate(dto.FechaHasta.ToShortDateString()))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Final Invalida";
                    result.LinkData = "";
                    return result;
                }

                if (dto.UltimoSueldo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ultimo sueldo Invalido";
                    return result;
                }
                if (dto.Supervisor == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Supervisor Invalido";
                    return result;
                }
                if (dto.CargoSupervisor == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cargo Supervisor Invalido";
                    return result;
                }
                if (dto.Telefono == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Telefono Invalido";
                    return result;
                }
                if (dto.Descripcion == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
                    return result;
                }

                

                RH_EXP_LABORAL entity = new RH_EXP_LABORAL();
                entity.CODIGO_EXP_LABORAL = await _repository.GetNextKey();
                entity.CODIGO_PERSONA = dto.CodigoPersona;
                entity.NOMBRE_EMPRESA = dto.NombreEmpresa;
                entity.TIPO_EMPRESA = dto.TipoEmpresa;
                entity.RAMO_ID = dto.RamoId;
                entity.CARGO = dto.Cargo;
                entity.FECHA_DESDE = dto.FechaDesde;
                entity.FECHA_HASTA = dto.FechaHasta;
                entity.ULTIMO_SUELDO = dto.UltimoSueldo;
                entity.SUPERVISOR = dto.Supervisor;
                entity.CARGO_SUPERVISOR = dto.CargoSupervisor;
                entity.TELEFONO = dto.Telefono;
                entity.DESCRIPCION = dto.Descripcion;
            



                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario.ToString();
                entity.FECHA_INS = DateTime.Now;


                var created = await _repository.Add(entity);

                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapRhExpLaboralDto(created.Data);
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
 
        public async Task<ResultDto<RhExpLaboralDeleteDto>> Delete(RhExpLaboralDeleteDto dto)
        {

            ResultDto<RhExpLaboralDeleteDto> result = new ResultDto<RhExpLaboralDeleteDto>(null);
            try
            {

                var ExpLaboral = await _repository.GetByCodigo(dto.CodigoExpLaboral);
                if (ExpLaboral == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Persona no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoExpLaboral);

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

