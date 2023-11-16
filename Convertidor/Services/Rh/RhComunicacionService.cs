﻿using System;
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
	public class RhComunicacionService: IRhComunicacionService
    {
        
   
        private readonly IRhComunicacionesRepository _repository;
        private readonly IRhDescriptivasService _descriptivaService;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
   

        public RhComunicacionService(IRhComunicacionesRepository repository, 
                                        IRhDescriptivasService descriptivaService, 
                                        ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _descriptivaService = descriptivaService;
            _sisUsuarioRepository = sisUsuarioRepository;
        }
       
        public async Task<ResultDto<List<RhComunicacionResponseDto>>> GetByCodigoPersona(int codigoPersona)
        {
            
            ResultDto<List<RhComunicacionResponseDto>> result = new ResultDto<List<RhComunicacionResponseDto>>(null);
            try
            {
                
                var comunicaciones = await _repository.GetByCodigoPersona(codigoPersona);

                var comunicacionesResult = await MapListComunicacionDto(comunicaciones);
                result.Data = comunicacionesResult;
                result.IsValid = true;
                result.Message = "";

                return ( ResultDto<List<RhComunicacionResponseDto>>)result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
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
       
        public async  Task<RhComunicacionResponseDto> MapComunicacionDto(RH_COMUNICACIONES dtos)
        {

                RhComunicacionResponseDto itemResult = new RhComunicacionResponseDto();
                itemResult.CodigoComunicacion = dtos.CODIGO_COMUNICACION;
                itemResult.CodigoPersona = dtos.CODIGO_PERSONA;
                itemResult.TipoComunicacionId = dtos.TIPO_COMUNICACION_ID;          
              
                itemResult.DescripcionTipoComunicacion = await _descriptivaService.GetDescripcionByCodigoDescriptiva(dtos.TIPO_COMUNICACION_ID);
                itemResult.CodigoArea = dtos.CODIGO_AREA;
                itemResult.LineaComunicacion = dtos.LINEA_COMUNICACION;
                itemResult.Extencion = dtos.EXTENSION;
                itemResult.Principal = dtos.PRINCIPAL;

                return itemResult;
        }

        public async  Task<List<RhComunicacionResponseDto>> MapListComunicacionDto(List<RH_COMUNICACIONES> dtos)
        {
            List<RhComunicacionResponseDto> result = new List<RhComunicacionResponseDto>();
            
            foreach (var item in dtos)
            {

                RhComunicacionResponseDto itemResult = new RhComunicacionResponseDto();

                itemResult = await MapComunicacionDto(item);
               
                result.Add(itemResult);
            }
            return result;

        }

        
        public async Task<ResultDto<RhComunicacionResponseDto>> Update(RhComunicacionUpdate dto)
        {

            ResultDto<RhComunicacionResponseDto> result = new ResultDto<RhComunicacionResponseDto>(null);
            try
            {

                var comunicacion = await _repository.GetByCodigo(dto.CodigoComunicacion);
                if (comunicacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Comunicacion no existe";
                    return result;
                }
                if (dto.LineaComunicacion.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Linea Comunizacion Invalida";
                    return result;
                }
               
                
                var tipoComunicaciones = await _descriptivaService.GetByTitulo(27);
                if (tipoComunicaciones.Count<=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Comunicacion  Invalido";
                    return result;
                }
                else
                {
                    var tipoComunicacion = tipoComunicaciones.Where(x => x.Id == dto.TipoComunicacionId);
                    if (tipoComunicacion is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Tipo Comunicacion   Invalido";
                        return result;
                    }
                }
                
                comunicacion.TIPO_COMUNICACION_ID= dto.TipoComunicacionId;
                comunicacion.CODIGO_PERSONA = dto.CodigoPersona;
                comunicacion.LINEA_COMUNICACION = dto.LineaComunicacion;
                comunicacion.EXTENSION = dto.Extencion;
                comunicacion.PRINCIPAL = dto.Principal;
                comunicacion.FECHA_UPD = DateTime.Now;
              
                var conectado = await _sisUsuarioRepository.GetConectado();
                comunicacion.CODIGO_EMPRESA = conectado.Empresa;
                comunicacion.USUARIO_UPD = conectado.Usuario;

                await _repository.Update(comunicacion);

                var resultDto = await MapComunicacionDto(comunicacion);
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

        public async Task<ResultDto<RhComunicacionResponseDto>> Create(RhComunicacionUpdate dto)
        {

            ResultDto<RhComunicacionResponseDto> result = new ResultDto<RhComunicacionResponseDto>(null);
            try
            {
                if (dto.LineaComunicacion.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Linea Comunizacion Invalida";
                    return result;
                }
               
                
                var tipoComunicaciones = await _descriptivaService.GetByTitulo(27);
                if (tipoComunicaciones.Count<=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Comunicacion  Invalido";
                    return result;
                }
                else
                {
                    var tipoComunicacion = tipoComunicaciones.Where(x => x.Id == dto.TipoComunicacionId);
                    if (tipoComunicacion is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Tipo Comunicacion   Invalido";
                        return result;
                    }
                }


                RH_COMUNICACIONES entity = new RH_COMUNICACIONES();
                entity.CODIGO_COMUNICACION = await _repository.GetNextKey();
                entity.CODIGO_PERSONA = dto.CodigoPersona;
                entity.TIPO_COMUNICACION_ID= dto.TipoComunicacionId;
                entity.LINEA_COMUNICACION = dto.LineaComunicacion;
                entity.EXTENSION = dto.Extencion;
                entity.PRINCIPAL = dto.Principal;
                entity.FECHA_INS = DateTime.Now;
                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;


                var created=await _repository.Add(entity);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapComunicacionDto(created.Data);
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
 
        public async Task<ResultDto<RhComunicacionDeleteDto>> Delete(RhComunicacionDeleteDto dto)
        {

            ResultDto<RhComunicacionDeleteDto> result = new ResultDto<RhComunicacionDeleteDto>(null);
            try
            {

                var comunicacion = await _repository.GetByCodigo(dto.CodigoComunicacion);
                if (comunicacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Comunicacion no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoComunicacion);

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

