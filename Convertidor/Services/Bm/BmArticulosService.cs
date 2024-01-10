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

        public async Task<BmArticulosGetDto> MapBmArticulos(BM_ARTICULOS dtos)
        {


            BmArticulosGetDto itemResult = new BmArticulosGetDto();
            itemResult.CodigoArticulo = dtos.CODIGO_ARTICULO;
            itemResult.CodigoClasificacionBien = dtos.CODIGO_CLASIFICACION_BIEN;
            itemResult.Codigo = dtos.CODIGO;
            itemResult.Denominacion = dtos.DENOMINACION;
            itemResult.Descripcion = dtos.DESCRIPCION;
       

            return itemResult;

        }
        public async Task<List<BmArticulosGetDto>> MapListArticulosDto(List<BM_ARTICULOS> dtos)
        {
            List<BmArticulosGetDto> result = new List<BmArticulosGetDto>();


            foreach (var item in dtos)
            {

                BmArticulosGetDto itemResult = new BmArticulosGetDto();

                itemResult = await MapBmArticulos(item);

                result.Add(itemResult);
            }
            return result;



        }
        public async Task<ResultDto<List<BmArticulosGetDto>>> GetAll()
        {

            ResultDto<List<BmArticulosGetDto>> result = new ResultDto<List<BmArticulosGetDto>>(null);
            try
            {

                var titulos = await _repository.GetAll();



                if (titulos.Count() > 0)
                {
                    List<BmArticulosGetDto> listDto = new List<BmArticulosGetDto>();

                    foreach (var item in titulos)
                    {
                        BmArticulosGetDto dto = new BmArticulosGetDto();
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

        public async Task<ResultDto<List<BmArticulosGetDto>>> GetByCodigoArticulo(int codigoArticulo)
        {

            ResultDto<List<BmArticulosGetDto>> result = new ResultDto<List<BmArticulosGetDto>>(null);
            try
            {




                var articulo = await _repository.GetByCodigoArticulo(codigoArticulo);


                var articulos = await _repository.GetByCodigo(articulo.CODIGO);
                if (articulos.CODIGO != string.Empty)
                {
                    List<BmArticulosGetDto> listDto = new List<BmArticulosGetDto>();

                    BmArticulosGetDto itemDefault = new BmArticulosGetDto();
                    itemDefault.CodigoArticulo = 0;
                    itemDefault.CodigoClasificacionBien = 0;
                    itemDefault.Codigo = "";
                    itemDefault.Denominacion = "";
                    itemDefault.Descripcion = "Seleccione";
                    itemDefault.Extra1 = "";
                    itemDefault.Extra2 = "";
                    itemDefault.Extra3 = "";

                    List<BmArticulosGetDto> lista = new List<BmArticulosGetDto>();
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

        public async Task<ResultDto<List<BmArticulosGetDto>>> GetByCodigo(string codigo)
            {

            ResultDto<List<BmArticulosGetDto>> result = new ResultDto<List<BmArticulosGetDto>>(null);
            try
            {




                var articulo = await _repository.GetByCodigo(codigo);


                var articulos = await _repository.GetByCodigo(articulo.CODIGO);
                if (articulos.CODIGO!=string.Empty)
                {
                    List<BmArticulosGetDto> listDto = new List<BmArticulosGetDto>();

                    BmArticulosGetDto itemDefault = new BmArticulosGetDto();
                    itemDefault.CodigoArticulo = 0;
                    itemDefault.CodigoClasificacionBien = 0;
                    itemDefault.Codigo = "";
                    itemDefault.Denominacion = "";
                    itemDefault.Descripcion = "Seleccione";
                    itemDefault.Extra1 = "";
                    itemDefault.Extra2 = "";
                    itemDefault.Extra3 = "";

                    List<BmArticulosGetDto> lista = new List<BmArticulosGetDto>();
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

        
        


        public async Task<ResultDto<BmArticulosGetDto>> Update(BmArticulosUpdateDto dto)
        {

            ResultDto<BmArticulosGetDto> result = new ResultDto<BmArticulosGetDto>(null);
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

        public async Task<ResultDto<BmArticulosGetDto>> Create(BmArticulosUpdateDto dto)
        {

            ResultDto<BmArticulosGetDto> result = new ResultDto<BmArticulosGetDto>(null);
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

        public BmArticulosGetDto GetDefaultArticulo()
        {
            BmArticulosGetDto itemDefault = new BmArticulosGetDto();
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

