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
    public class BmDetalleBienesService: IBmDetalleBienesService
    { 


        private readonly IBmDetalleBienesRepository _repository;
        private readonly IBmDescriptivasService _bmDescriptivasService;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IConfiguration _configuration;
        public  BmDetalleBienesService(IBmDetalleBienesRepository repository,
                                       IBmDescriptivasService bmDescriptivasService,
                                       ISisUsuarioRepository sisUsuarioRepository,
                                       IConfiguration configuration)
		{
            _repository = repository;
            _bmDescriptivasService = bmDescriptivasService;
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
        public async Task<BmDetalleBienesGetDto> MapBmDetalleBienes(BM_DETALLE_BIENES dtos)
        {


            BmDetalleBienesGetDto itemResult = new BmDetalleBienesGetDto();
            itemResult.CodigoDetalleBien = dtos.CODIGO_DETALLE_BIEN;
            itemResult.CodigoBien = dtos.CODIGO_BIEN;
            itemResult.TipoEspecificacionId = dtos.TIPO_ESPECIFICACION_ID;
            itemResult.EspecificacionId = dtos.ESPECIFICACION_ID;
            itemResult.Especificacion = dtos.ESPECIFICACION;
            
            


            return itemResult;

        }
        public async Task<List<BmDetalleBienesGetDto>> MapListDetallesBienesDto(List<BM_DETALLE_BIENES> dtos)
        {
            List<BmDetalleBienesGetDto> result = new List<BmDetalleBienesGetDto>();


            foreach (var item in dtos)
            {

                BmDetalleBienesGetDto itemResult = new BmDetalleBienesGetDto();

                itemResult = await MapBmDetalleBienes(item);

                result.Add(itemResult);
            }
            return result;



        }

       
        public async Task<ResultDto<List<BmDetalleBienesGetDto>>> GetAll()
        {

            ResultDto<List<BmDetalleBienesGetDto>> result = new ResultDto<List<BmDetalleBienesGetDto>>(null);
            try
            {

                var titulos = await _repository.GetAll();



                if (titulos.Count() > 0)
                {
                    List<BmDetalleBienesGetDto> listDto = new List<BmDetalleBienesGetDto>();

                    foreach (var item in titulos)
                    {
                        BmDetalleBienesGetDto dto = new BmDetalleBienesGetDto();
                        dto = await MapBmDetalleBienes(item);

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


        public async Task<ResultDto<BmDetalleBienesGetDto>> Update(BmDetalleBienesUpdateDto dto)
        {

            ResultDto<BmDetalleBienesGetDto> result = new ResultDto<BmDetalleBienesGetDto>(null);
            try
            {
                var codigoDetalleBien = await _repository.GetByCodigoDetalleBien(dto.CodigoDetalleBien);
                if (codigoDetalleBien == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle bien no invalido";
                    return result;
                }

                var codigoBien = await _repository.GetByCodigoBien(dto.CodigoBien);
                if (codigoBien.CODIGO_BIEN == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Bien Invalido";
                    return result;
                }

                var tipoEspecificacionID = await _bmDescriptivasService.GetByIdAndTitulo(1,dto.TipoEspecificacionId);
                if (tipoEspecificacionID == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo especificacion Id Invalida";
                    return result;
                }
                var especificacionID = await _bmDescriptivasService.GetByIdAndTitulo(2,dto.EspecificacionId);
                if (especificacionID == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Especificacion Id Invalida";
                    return result;
                }
                var especificacion = dto.Especificacion;
                if (especificacion == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Especificacion Invalida";
                    return result;
                }

               

                codigoDetalleBien.CODIGO_DETALLE_BIEN = dto.CodigoDetalleBien;
                codigoDetalleBien.CODIGO_BIEN = dto.CodigoBien;
                codigoDetalleBien.TIPO_ESPECIFICACION_ID = dto.TipoEspecificacionId;
                codigoDetalleBien.ESPECIFICACION_ID = dto.EspecificacionId;
                codigoDetalleBien.ESPECIFICACION = dto.Especificacion;
               
                
               

                var conectado = await _sisUsuarioRepository.GetConectado();
                codigoDetalleBien.CODIGO_EMPRESA = conectado.Empresa;
                codigoDetalleBien.USUARIO_UPD = conectado.Usuario;
                codigoDetalleBien.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoDetalleBien);

                var resultDto = await MapBmDetalleBienes(codigoDetalleBien);
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

        public async Task<ResultDto<BmDetalleBienesGetDto>> Create(BmDetalleBienesUpdateDto dto)
        {

            ResultDto<BmDetalleBienesGetDto> result = new ResultDto<BmDetalleBienesGetDto>(null);
            try
            {

                var codigoDetalleBien = await _repository.GetByCodigoDetalleBien(dto.CodigoDetalleBien);
                if (codigoDetalleBien != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo detalle bien no valido";
                    return result;
                }

                var codigoBien = await _repository.GetByCodigoBien(dto.CodigoBien);
                if (codigoBien.CODIGO_BIEN == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Bien Invalido";
                    return result;
                }

                var tipoEspecificacionID = await _bmDescriptivasService.GetByIdAndTitulo(1,dto.TipoEspecificacionId);
                if (tipoEspecificacionID == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo especificacion Id Invalida";
                    return result;
                }
              
                var especificacionID = await _bmDescriptivasService.GetByIdAndTitulo(2,dto.EspecificacionId);
                if (especificacionID == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Especificacion Id Invalida";
                    return result;
                }
                var especificacion = dto.Especificacion;
                if (especificacion == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Especificacion Invalida";
                    return result;
                }






                BM_DETALLE_BIENES entity = new BM_DETALLE_BIENES();
                entity.CODIGO_DETALLE_BIEN = await _repository.GetNextKey();
                entity.CODIGO_BIEN = dto.CodigoBien;
                entity.TIPO_ESPECIFICACION_ID = dto.TipoEspecificacionId;
                entity.ESPECIFICACION_ID = dto.EspecificacionId;
                entity.ESPECIFICACION = dto.Especificacion;


                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data!=null)
                {
                    var resultDto = await MapBmDetalleBienes(created.Data);
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

       

       


        public async Task<ResultDto<BmDetalleBienesDeleteDto>> Delete(BmDetalleBienesDeleteDto dto)
        {

            ResultDto<BmDetalleBienesDeleteDto> result = new ResultDto<BmDetalleBienesDeleteDto>(null);
            try
            {

                var codigoDetalleBien = await _repository.GetByCodigoDetalleBien(dto.CodigoDetalleBien);
                if (codigoDetalleBien == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo detalle Bien no existe";
                    return result;
                }

              
                var deleted = await _repository.Delete(dto.CodigoDetalleBien);

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

