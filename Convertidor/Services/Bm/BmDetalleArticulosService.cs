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
    public class BmDetalleArticulosService: IBmDetalleArticulosService
    { 


        private readonly IBmDetalleArticulosRepository _repository;
        private readonly IBmDescriptivasService _bmDescriptivasService;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IConfiguration _configuration;
        public BmDetalleArticulosService(IBmDetalleArticulosRepository repository,
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
        public async Task<BmDetalleArticulosResponseDto> MapBmDetalleArticulos(BM_DETALLE_ARTICULOS dtos)
        {


            BmDetalleArticulosResponseDto itemResult = new BmDetalleArticulosResponseDto();
            itemResult.CodigoDetalleArticulo = dtos.CODIGO_DETALLE_ARTICULO;
            itemResult.CodigoArticulo = dtos.CODIGO_ARTICULO;
            itemResult.TipoEspecificacionId = dtos.TIPO_ESPECIFICACION_ID;
            
            
            


            return itemResult;

        }
        public async Task<List<BmDetalleArticulosResponseDto>> MapListDetalleArticulosDto(List<BM_DETALLE_ARTICULOS> dtos)
        {
            List<BmDetalleArticulosResponseDto> result = new List<BmDetalleArticulosResponseDto>();


            foreach (var item in dtos)
            {

                BmDetalleArticulosResponseDto itemResult = new BmDetalleArticulosResponseDto();

                itemResult = await MapBmDetalleArticulos(item);

                result.Add(itemResult);
            }
            return result;



        }

       
        public async Task<ResultDto<List<BmDetalleArticulosResponseDto>>> GetAll()
        {

            ResultDto<List<BmDetalleArticulosResponseDto>> result = new ResultDto<List<BmDetalleArticulosResponseDto>>(null);
            try
            {

                var titulos = await _repository.GetAll();



                if (titulos.Count() > 0)
                {
                    List<BmDetalleArticulosResponseDto> listDto = new List<BmDetalleArticulosResponseDto>();

                    foreach (var item in titulos)
                    {
                        BmDetalleArticulosResponseDto dto = new BmDetalleArticulosResponseDto();
                        dto = await MapBmDetalleArticulos(item);

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


        public async Task<ResultDto<BmDetalleArticulosResponseDto>> Update(BmDetalleArticulosUpdateDto dto)
        {

            ResultDto<BmDetalleArticulosResponseDto> result = new ResultDto<BmDetalleArticulosResponseDto>(null);
            try
            {
                var codigoDetalleArticulo = await _repository.GetByCodigoDetalleArticulo(dto.CodigoDetalleArticulo);
                if (codigoDetalleArticulo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Articulo no invalido";
                    return result;
                }

                var codigoArticulo = await _repository.GetByCodigoArticulo(dto.CodigoArticulo);
                if (codigoArticulo.CODIGO_ARTICULO == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Articulo Invalido";
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

                if (dto.Extra1 is not null && dto.Extra1.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra1 invalido";
                    return result;
                }

                if (dto.Extra2 is not null && dto.Extra2.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra2 invalido";
                    return result;
                }

                if (dto.Extra3 is not null && dto.Extra3.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra3 invalido";
                    return result;
                }


                codigoDetalleArticulo.CODIGO_DETALLE_ARTICULO = dto.CodigoDetalleArticulo;
                codigoDetalleArticulo.CODIGO_ARTICULO = dto.CodigoArticulo;
                codigoDetalleArticulo.TIPO_ESPECIFICACION_ID = dto.TipoEspecificacionId;
              
                
               

                var conectado = await _sisUsuarioRepository.GetConectado();
                codigoDetalleArticulo.CODIGO_EMPRESA = conectado.Empresa;
                codigoDetalleArticulo.USUARIO_UPD = conectado.Usuario;
                codigoDetalleArticulo.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoDetalleArticulo);

                var resultDto = await MapBmDetalleArticulos(codigoDetalleArticulo);
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

        public async Task<ResultDto<BmDetalleArticulosResponseDto>> Create(BmDetalleArticulosUpdateDto dto)
        {

            ResultDto<BmDetalleArticulosResponseDto> result = new ResultDto<BmDetalleArticulosResponseDto>(null);
            try
            {

                var codigoDetalleArticulo = await _repository.GetByCodigoDetalleArticulo(dto.CodigoDetalleArticulo);
                if (codigoDetalleArticulo != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo detalle Articulo no valido";
                    return result;
                }

                var codigoArticulo = await _repository.GetByCodigoArticulo(dto.CodigoArticulo);
                if (codigoArticulo.CODIGO_ARTICULO == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Articulo Invalido";
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

                if (dto.Extra1 == string.Empty) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra1 Invalido";
                    return result;
                }

                if (dto.Extra1 is not null && dto.Extra1.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra1 invalido";
                    return result;
                }

                if (dto.Extra2 is not null && dto.Extra2.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra2 invalido";
                    return result;
                }

                if (dto.Extra3 is not null && dto.Extra3.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra3 invalido";
                    return result;
                }

                BM_DETALLE_ARTICULOS entity = new BM_DETALLE_ARTICULOS();
                entity.CODIGO_DETALLE_ARTICULO = await _repository.GetNextKey();
                entity.CODIGO_ARTICULO = dto.CodigoArticulo;
                entity.TIPO_ESPECIFICACION_ID = dto.TipoEspecificacionId;
               
                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data!=null)
                {
                    var resultDto = await MapBmDetalleArticulos(created.Data);
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

       

       


        public async Task<ResultDto<BmDetalleArticulosDeleteDto>> Delete(BmDetalleArticulosDeleteDto dto)
        {

            ResultDto<BmDetalleArticulosDeleteDto> result = new ResultDto<BmDetalleArticulosDeleteDto>(null);
            try
            {

                var codigoDetalleArticulo = await _repository.GetByCodigoDetalleArticulo(dto.CodigoDetalleArticulo);
                if (codigoDetalleArticulo == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo detalle Articulo no existe";
                    return result;
                }

              
                var deleted = await _repository.Delete(dto.CodigoDetalleArticulo);

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

