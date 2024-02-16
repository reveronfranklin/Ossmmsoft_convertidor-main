using System;
using System.Collections.Generic;
using System.Globalization;
using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Data.Repository.Rh;
using Convertidor.Dtos.Bm;
using NuGet.Packaging;

namespace Convertidor.Services.Bm
{
    public class BmArticulosService: IBmArticulosService
    {

      
        private readonly IBmArticulosRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IConfiguration _configuration;
        public BmArticulosService(IBmArticulosRepository repository,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      IConfiguration configuration)
		{
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _configuration = configuration;
           

        }

        public async Task<BmArticulosResponseDto> MapBmArticulos(BM_ARTICULOS dtos)
        {


            BmArticulosResponseDto itemResult = new BmArticulosResponseDto();
            itemResult.CodigoArticulo = dtos.CODIGO_ARTICULO;
            itemResult.CodigoClasificacionBien = dtos.CODIGO_CLASIFICACION_BIEN;
            itemResult.Codigo = dtos.CODIGO;
            itemResult.Denominacion = dtos.DENOMINACION;
            itemResult.Descripcion = dtos.DESCRIPCION;
       

            return itemResult;

        }
        public async Task<List<BmArticulosResponseDto>> MapListArticulosDto(List<BM_ARTICULOS> dtos)
        {
            List<BmArticulosResponseDto> result = new List<BmArticulosResponseDto>();


            foreach (var item in dtos)
            {

                BmArticulosResponseDto itemResult = new BmArticulosResponseDto();

                itemResult = await MapBmArticulos(item);

                result.Add(itemResult);
            }
            return result;



        }
        public async Task<ResultDto<List<BmArticulosResponseDto>>> GetAll()
        {

            ResultDto<List<BmArticulosResponseDto>> result = new ResultDto<List<BmArticulosResponseDto>>(null);
            try
            {

                var titulos = await _repository.GetAll();



                if (titulos.Count() > 0)
                {
                    List<BmArticulosResponseDto> listDto = new List<BmArticulosResponseDto>();

                    foreach (var item in titulos)
                    {
                        BmArticulosResponseDto dto = new BmArticulosResponseDto();
                        dto = await MapBmArticulos(item);

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

        public async Task<ResultDto<List<BmArticulosResponseDto>>> GetByCodigoArticulo(int codigoArticulo)
        {

            ResultDto<List<BmArticulosResponseDto>> result = new ResultDto<List<BmArticulosResponseDto>>(null);
            try
            {




                var articulo = await _repository.GetByCodigoArticulo(codigoArticulo);


                var articulos = await _repository.GetByCodigo(articulo.CODIGO);
                if (articulos.CODIGO != string.Empty)
                {
                    List<BmArticulosResponseDto> listDto = new List<BmArticulosResponseDto>();

                    BmArticulosResponseDto itemDefault = new BmArticulosResponseDto();
                    itemDefault.CodigoArticulo = 0;
                    itemDefault.CodigoClasificacionBien = 0;
                    itemDefault.Codigo = "";
                    itemDefault.Denominacion = "";
                    itemDefault.Descripcion = "Seleccione";
                    itemDefault.Extra1 = "";
                    itemDefault.Extra2 = "";
                    itemDefault.Extra3 = "";

                    List<BmArticulosResponseDto> lista = new List<BmArticulosResponseDto>();
                    lista.Add(GetDefaultArticulo());

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

        public async Task<ResultDto<List<BmArticulosResponseDto>>> GetByCodigo(string codigo)
            {

            ResultDto<List<BmArticulosResponseDto>> result = new ResultDto<List<BmArticulosResponseDto>>(null);
            try
            {




                var articulo = await _repository.GetByCodigo(codigo);


                var articulos = await _repository.GetByCodigo(articulo.CODIGO);
                if (articulos.CODIGO!=string.Empty)
                {
                    List<BmArticulosResponseDto> listDto = new List<BmArticulosResponseDto>();

                    BmArticulosResponseDto itemDefault = new BmArticulosResponseDto();
                    itemDefault.CodigoArticulo = 0;
                    itemDefault.CodigoClasificacionBien = 0;
                    itemDefault.Codigo = "";
                    itemDefault.Denominacion = "";
                    itemDefault.Descripcion = "Seleccione";
                    itemDefault.Extra1 = "";
                    itemDefault.Extra2 = "";
                    itemDefault.Extra3 = "";

                    List<BmArticulosResponseDto> lista = new List<BmArticulosResponseDto>();
                    lista.Add(GetDefaultArticulo());
                   
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

        
        


        public async Task<ResultDto<BmArticulosResponseDto>> Update(BmArticulosUpdateDto dto)
        {

            ResultDto<BmArticulosResponseDto> result = new ResultDto<BmArticulosResponseDto>(null);
            try
            {

                var codigoArticulo = await _repository.GetByCodigoArticulo(dto.CodigoArticulo);
                if (codigoArticulo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Articulo no existe";
                    return result;
                }
                var codigo = await _repository.GetByCodigo(dto.Codigo);
                if (codigo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo no invalido";
                    return result;
                }
                if (dto.Denominacion.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Invalida";
                    return result;
                }
                if (dto.Descripcion.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
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




                codigoArticulo.CODIGO_ARTICULO = dto.CodigoArticulo;
                codigoArticulo.CODIGO_CLASIFICACION_BIEN = dto.CodigoClasificacionBien;
                codigoArticulo.CODIGO = dto.Codigo;
                codigoArticulo.DENOMINACION = dto.Denominacion;
                codigoArticulo.DESCRIPCION = dto.Descripcion;
                codigoArticulo.EXTRA1 = dto.Extra1;
                codigoArticulo.EXTRA2 = dto.Extra2;
                codigoArticulo.EXTRA3 = dto.Extra3;

                var conectado = await _sisUsuarioRepository.GetConectado();
                codigoArticulo.CODIGO_EMPRESA = conectado.Empresa;
                codigoArticulo.USUARIO_UPD = conectado.Usuario;
                codigoArticulo.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoArticulo);

                var resultDto = await MapBmArticulos(codigoArticulo);
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

        public async Task<ResultDto<BmArticulosResponseDto>> Create(BmArticulosUpdateDto dto)
        {

            ResultDto<BmArticulosResponseDto> result = new ResultDto<BmArticulosResponseDto>(null);
            try
            {

                var CodigoArticulo = await _repository.GetByCodigoArticulo(dto.CodigoArticulo);
                if (CodigoArticulo != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Articulo ya existe";
                    return result;
                }
                if (dto.CodigoClasificacionBien == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Clasificacion Bien Invalido";
                    return result;
                }
                var codigo = await _repository.GetByCodigo(dto.Codigo);
                if (codigo != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo invalido";
                    return result;
                }
                if (dto.Denominacion.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Invalida";
                    return result;
                }
                if (dto.Descripcion.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
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


                BM_ARTICULOS entity = new BM_ARTICULOS();        
                entity.CODIGO_ARTICULO = await _repository.GetNextKey();
                entity.CODIGO_CLASIFICACION_BIEN = dto.CodigoClasificacionBien;
                entity.CODIGO = dto.Codigo;
                entity.DENOMINACION = dto.Denominacion;
                entity.DESCRIPCION = dto.Descripcion;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;

                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data!=null)
                {
                    var resultDto = await MapBmArticulos(created.Data);
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

        public BmArticulosResponseDto GetDefaultArticulo()
        {
            BmArticulosResponseDto itemDefault = new BmArticulosResponseDto();
            itemDefault.CodigoArticulo = 0;
            itemDefault.CodigoClasificacionBien = 0;
            itemDefault.Codigo = "";
            itemDefault.Descripcion = "Seleccione";
            itemDefault.Denominacion = "";
            itemDefault.Extra1 = "";
            itemDefault.Extra2 = "";
            itemDefault.Extra3 = "";
            return itemDefault;
          
        }

       


        public async Task<ResultDto<BmArticulosDeleteDto>> Delete(BmArticulosDeleteDto dto)
        {

            ResultDto<BmArticulosDeleteDto> result = new ResultDto<BmArticulosDeleteDto>(null);
            try
            {

                var articulo = await _repository.GetByCodigoArticulo(dto.CodigoArticulo);
                if (articulo == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Articulo no existe";
                    return result;
                }

              
                var deleted = await _repository.Delete(dto.CodigoArticulo);

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

