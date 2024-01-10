using System;
using System.Collections.Generic;
using System.Globalization;
using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Data.Repository.Rh;
using Convertidor.Dtos;
using Convertidor.Dtos.Bm;
using Convertidor.Dtos.Rh;
using Convertidor.Services.Rh;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using NuGet.Packaging;

namespace Convertidor.Services.Bm
{
    public class BmClasificacionBienesService: IBmClasificacionBienesService
    {

      
        private readonly IBmClasificacionBienesRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IConfiguration _configuration;
        public BmClasificacionBienesService(IBmClasificacionBienesRepository repository,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      IConfiguration configuration)
		{
            _repository = repository;

            _sisUsuarioRepository = sisUsuarioRepository;
            _configuration = configuration;
           

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
        public async Task<BmClasificacionBienesGetDto> MapBmClasificacionBienes(BM_CLASIFICACION_BIENES dtos)
        {


            BmClasificacionBienesGetDto itemResult = new BmClasificacionBienesGetDto();
            itemResult.CodigoClasificacionBien = dtos.CODIGO_CLASIFICACION_BIEN;
            itemResult.CodigoGrupo = dtos.CODIGO_GRUPO;
            itemResult.CodigoNivel1 = dtos.CODIGO_NIVEL1;
            itemResult.CodigoNivel2 = dtos.CODIGO_NIVEL2;
            itemResult.CodigoNivel3 = dtos.CODIGO_NIVEL3;
            itemResult.Denominacion = dtos.DENOMINACION;
            itemResult.Descripcion = dtos.DESCRIPCION;
            itemResult.FechaIni = dtos.FECHA_FIN;
            itemResult.Extra3 = dtos.EXTRA3;
            


            return itemResult;

        }
        public async Task<List<BmClasificacionBienesGetDto>> MapListClasificacionBienesDto(List<BM_CLASIFICACION_BIENES> dtos)
        {
            List<BmClasificacionBienesGetDto> result = new List<BmClasificacionBienesGetDto>();


            foreach (var item in dtos)
            {

                BmClasificacionBienesGetDto itemResult = new BmClasificacionBienesGetDto();

                itemResult = await MapBmClasificacionBienes(item);

                result.Add(itemResult);
            }
            return result;



        }

       
        public async Task<ResultDto<List<BmClasificacionBienesGetDto>>> GetAll()
        {

            ResultDto<List<BmClasificacionBienesGetDto>> result = new ResultDto<List<BmClasificacionBienesGetDto>>(null);
            try
            {

                var titulos = await _repository.GetAll();



                if (titulos.Count() > 0)
                {
                    List<BmClasificacionBienesGetDto> listDto = new List<BmClasificacionBienesGetDto>();

                    foreach (var item in titulos)
                    {
                        BmClasificacionBienesGetDto dto = new BmClasificacionBienesGetDto();
                        dto = await MapBmClasificacionBienes(item);

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


        public async Task<ResultDto<BmClasificacionBienesGetDto>> Update(BmClasificacionBienesUpdateDto dto)
        {

            ResultDto<BmClasificacionBienesGetDto> result = new ResultDto<BmClasificacionBienesGetDto>(null);
            try
            {
                var codigoClasificacionBien = await _repository.GetByCodigoClasificacionBien(dto.CodigoClasificacionBien);
                if (codigoClasificacionBien == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Clasificacion bien no invalido";
                    return result;
                }

                var codigoGrupo = await _repository.GetByCodigoGrupo(dto.CodigoGrupo);
                if (codigoGrupo.CODIGO_GRUPO == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo grupo Invalido";
                    return result;
                }

                var codigoCodigoNivel1 = dto.CodigoNivel1;
                if (codigoCodigoNivel1 == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo nivel1 Invalido";
                    return result;
                }

                var codigoCodigoNivel2 = dto.CodigoNivel2;
                if (codigoCodigoNivel2 == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo nivel2 Invalido";
                    return result;
                }
                var codigoCodigoNivel3 = dto.CodigoNivel3;
                if (codigoCodigoNivel3 == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo nivel3 Invalido";
                    return result;
                }
                var denominacion = dto.Denominacion;
                if (denominacion == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Invalida";
                    return result;
                }
                var descripcion = dto.Descripcion;
                if (descripcion == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
                    return result;
                }
                FechaDto fechaIni = GetFechaDto(dto.FechaIni);
                if (fechaIni == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Ini Invalida";
                    return result;
                }
                FechaDto fechaFin = GetFechaDto(dto.FechaFin);
                if (fechaFin == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Fin Invalida";
                    return result;
                }
                var extra3 = dto.Extra3;
                if (extra3 == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra3 Invalido";
                    return result;
                }





                codigoClasificacionBien.CODIGO_CLASIFICACION_BIEN = dto.CodigoClasificacionBien;
                codigoClasificacionBien.CODIGO_GRUPO = dto.CodigoGrupo;
                codigoClasificacionBien.CODIGO_NIVEL1 = dto.CodigoNivel1;
                codigoClasificacionBien.CODIGO_NIVEL2 = dto.CodigoNivel2;
                codigoClasificacionBien.CODIGO_NIVEL3 = dto.CodigoNivel3;
                codigoClasificacionBien.DENOMINACION = dto.Denominacion;
                codigoClasificacionBien.DESCRIPCION = dto.Descripcion;
                codigoClasificacionBien.FECHA_INI = dto.FechaIni;
                codigoClasificacionBien.FECHA_FIN = dto.FechaFin;
                codigoClasificacionBien.EXTRA3 = dto.Extra3;
                
               

                var conectado = await _sisUsuarioRepository.GetConectado();
                codigoClasificacionBien.CODIGO_EMPRESA = conectado.Empresa;
                codigoClasificacionBien.USUARIO_UPD = conectado.Usuario;
                codigoClasificacionBien.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoClasificacionBien);

                var resultDto = await MapBmClasificacionBienes(codigoClasificacionBien);
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

        public async Task<ResultDto<BmClasificacionBienesGetDto>> Create(BmClasificacionBienesUpdateDto dto)
        {

            ResultDto<BmClasificacionBienesGetDto> result = new ResultDto<BmClasificacionBienesGetDto>(null);
            try
            {

                var codigoClasificacionBien = await _repository.GetByCodigoClasificacionBien(dto.CodigoClasificacionBien);
                if (codigoClasificacionBien != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Clasificacion bien no invalido";
                    return result;
                }

                var codigoGrupo = await _repository.GetByCodigoGrupo(dto.CodigoGrupo);
                if (codigoGrupo.CODIGO_GRUPO == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo grupo Invalido";
                    return result;
                }

                var codigoCodigoNivel1 = await _repository.GetByCodigoNivel1(dto.CodigoNivel1);
                if (codigoCodigoNivel1 != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo nivel1 Invalido";
                    return result;
                }

                var codigoCodigoNivel2 = await _repository.GetByCodigoNivel2(dto.CodigoNivel2);
                if (codigoCodigoNivel2 != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo nivel2 Invalido";
                    return result;
                }
                var codigoCodigoNivel3 = dto.CodigoNivel3;
                if (codigoCodigoNivel3 == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo nivel3 Invalido";
                    return result;
                }
                var denominacion = dto.Denominacion;
                if (denominacion == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Invalida";
                    return result;
                }
                var descripcion = dto.Descripcion;
                if (descripcion == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
                    return result;
                }
                FechaDto fechaIni = GetFechaDto(dto.FechaIni);
                if (fechaIni == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Ini Invalida";
                    return result;
                }
                FechaDto fechaFin = GetFechaDto(dto.FechaFin);
                if (fechaFin == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Fin Invalida";
                    return result;
                }
                var extra3 = dto.Extra3;
                if (extra3 == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra3 Invalido";
                    return result;
                }

               




                BM_CLASIFICACION_BIENES entity = new BM_CLASIFICACION_BIENES();
                entity.CODIGO_CLASIFICACION_BIEN = await _repository.GetNextKey();
                entity.CODIGO_GRUPO = dto.CodigoGrupo;
                entity.CODIGO_NIVEL1 = dto.CodigoNivel1;
                entity.CODIGO_NIVEL2 = dto.CodigoNivel2;
                entity.CODIGO_NIVEL3 = dto.CodigoNivel3;
                entity.DENOMINACION = dto.Denominacion;
                entity.DESCRIPCION = dto.Descripcion;
                entity.FECHA_INI = dto.FechaIni;
                entity.FECHA_FIN =dto.FechaFin;
                entity.EXTRA3 = dto.Extra3;
                

                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data!=null)
                {
                    var resultDto = await MapBmClasificacionBienes(created.Data);
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

       

       


        public async Task<ResultDto<BmClasificacionBienesDeleteDto>> Delete(BmClasificacionBienesDeleteDto dto)
        {

            ResultDto<BmClasificacionBienesDeleteDto> result = new ResultDto<BmClasificacionBienesDeleteDto>(null);
            try
            {

                var codigoBien = await _repository.GetByCodigoClasificacionBien(dto.CodigoClasificacionBien);
                if (codigoBien == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Clasificacion Bien no existe";
                    return result;
                }

              
                var deleted = await _repository.Delete(dto.CodigoClasificacionBien);

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

