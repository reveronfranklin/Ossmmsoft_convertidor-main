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
    public class BmBienesService: IBmBienesService
    {

      
        private readonly IBmBienesRepository _repository;
        private readonly IBmDescriptivaRepository _bmDescriptivaRepository;
        private readonly IBmArticulosRepository _bmArticulosRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IConfiguration _configuration;
        public BmBienesService(IBmBienesRepository repository,
                                      IBmDescriptivaRepository bmDescriptivaRepository,
                                      IBmArticulosRepository bmArticulosRepository,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      IConfiguration configuration)
		{
            _repository = repository;
            _bmDescriptivaRepository = bmDescriptivaRepository;
            _bmArticulosRepository = bmArticulosRepository;
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
        public async Task<BmBienesGetDto> MapBmBienes(BM_BIENES dtos)
        {


            BmBienesGetDto itemResult = new BmBienesGetDto();
            itemResult.CodigoBien = dtos.CODIGO_BIEN;
            itemResult.CodigoArticulo = dtos.CODIGO_ARTICULO;
            itemResult.CodigoProveedor = dtos.CODIGO_PROVEEDOR;
            itemResult.CodigoOrdenCompra = dtos.CODIGO_ORDEN_COMPRA;
            itemResult.OrigenId = dtos.ORIGEN_ID;
            itemResult.FechaFabricacion = dtos.FECHA_FABRICACION;
            itemResult.NumeroOrdenCompra = dtos.NUMERO_ORDEN_COMPRA;
            itemResult.FechaCompra = dtos.FECHA_COMPRA;
            itemResult.NumeroPlaca = dtos.NUMERO_PLACA;
            itemResult.NumeroLote = dtos.NUMERO_LOTE;
            itemResult.ValorInicial = dtos.VALOR_INICIAL;
            itemResult.ValorActual = dtos.VALOR_ACTUAL;
            itemResult.OrigenId = dtos.ORIGEN_ID;
            itemResult.NumeroFactura = dtos.NUMERO_FACTURA;
            itemResult.FechaFactura = dtos.FECHA_FACTURA;
            itemResult.TipoImpuestoId = dtos.TIPO_IMPUESTO_ID;
            itemResult.OrigenId = dtos.ORIGEN_ID;


            return itemResult;

        }
        public async Task<List<BmBienesGetDto>> MapListBienesDto(List<BM_BIENES> dtos)
        {
            List<BmBienesGetDto> result = new List<BmBienesGetDto>();


            foreach (var item in dtos)
            {

                BmBienesGetDto itemResult = new BmBienesGetDto();

                itemResult = await MapBmBienes(item);

                result.Add(itemResult);
            }
            return result;



        }

       
        public async Task<ResultDto<List<BmBienesGetDto>>> GetAll()
        {

            ResultDto<List<BmBienesGetDto>> result = new ResultDto<List<BmBienesGetDto>>(null);
            try
            {

                var titulos = await _repository.GetAll();



                if (titulos.Count() > 0)
                {
                    List<BmBienesGetDto> listDto = new List<BmBienesGetDto>();

                    foreach (var item in titulos)
                    {
                        BmBienesGetDto dto = new BmBienesGetDto();
                        dto = await MapBmBienes(item);

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


        public async Task<ResultDto<BmBienesGetDto>> Update(BmBienesUpdateDto dto)
        {

            ResultDto<BmBienesGetDto> result = new ResultDto<BmBienesGetDto>(null);
            try
            {
                var codigoBien = await _repository.GetByCodigoBien(dto.CodigoBien);
                if (codigoBien == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo bien no invalido";
                    return result;
                }

                var codigoArticulo = await _bmArticulosRepository.GetByCodigoArticulo(dto.CodigoArticulo);
                if (codigoArticulo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Articulo Invalido";
                    return result;
                }

                var codigoProveedor = await _repository.GetByProveedor(dto.CodigoProveedor);
                if (codigoProveedor==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;
                }

                var codigoOrdenCompra = await _repository.GetByOrdenCompra(dto.CodigoOrdenCompra);
                if (codigoOrdenCompra == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Orden Compra Invalido";
                    return result;
                }
                var origenId = await _bmDescriptivaRepository.GetByTituloId(3);
                if (codigoOrdenCompra == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Origen Id Invalido";
                    return result;
                }
                FechaDto fechaFabricacion = GetFechaDto(dto.FechaFabricacion);
                if (fechaFabricacion == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Fabricacion Invalida";
                    return result;
                }
                
                if (dto.NumeroOrdenCompra == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Orden Compra Invalido";
                    return result;
                }
                FechaDto fechaCompra = GetFechaDto(dto.FechaCompra);
                if (fechaCompra == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha compra Invalida";
                    return result;
                }
                
               
                if (dto.NumeroLote == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Lote Invalido";
                    return result;
                }

                if (dto.ValorInicial == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Valor Inicial Invalido";
                    return result;
                }
                if (dto.ValorActual == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Valor Actual Invalido";
                    return result;
                }
                if (dto.NumeroFactura == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero factura Invalido";
                    return result;
                }

                FechaDto fechaFactura = GetFechaDto(dto.FechaFactura);
                if (fechaCompra == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha compra Invalida";
                    return result;
                }
                if (dto.TipoImpuestoId == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo de Impuesto Id Invalido";
                    return result;
                }



                codigoBien.CODIGO_BIEN = dto.CodigoBien;
                codigoBien.CODIGO_ARTICULO = dto.CodigoArticulo;
                codigoBien.CODIGO_PROVEEDOR = dto.CodigoProveedor;
                codigoBien.CODIGO_ORDEN_COMPRA = dto.CodigoOrdenCompra;
                codigoBien.ORIGEN_ID = dto.OrigenId;
                codigoBien.FECHA_FABRICACION = dto.FechaFabricacion;
                codigoBien.NUMERO_ORDEN_COMPRA = dto.NumeroOrdenCompra;
                codigoBien.FECHA_COMPRA = dto.FechaCompra;
                codigoBien.NUMERO_LOTE = dto.NumeroLote;
                codigoBien.VALOR_INICIAL = dto.ValorInicial;
                codigoBien.VALOR_ACTUAL = dto.ValorActual;
                codigoBien.NUMERO_FACTURA = dto.NumeroFactura;
                codigoBien.FECHA_FACTURA = dto.FechaFactura;
                codigoBien.TIPO_IMPUESTO_ID = dto.TipoImpuestoId;
               

                var conectado = await _sisUsuarioRepository.GetConectado();
                codigoBien.CODIGO_EMPRESA = conectado.Empresa;
                codigoBien.USUARIO_UPD = conectado.Usuario;
                codigoBien.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoBien);

                var resultDto = await MapBmBienes(codigoBien);
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

        public async Task<ResultDto<BmBienesGetDto>> Create(BmBienesUpdateDto dto)
        {

            ResultDto<BmBienesGetDto> result = new ResultDto<BmBienesGetDto>(null);
            try
            {

                var codigoBien = await _repository.GetByCodigoBien(dto.CodigoBien);
                if (codigoBien != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo bien ya existe";
                    return result;
                }

                var codigoArticulo = await _bmArticulosRepository.GetByCodigoArticulo(dto.CodigoArticulo);
                if (codigoArticulo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Articulo Invalido";
                    return result;
                }

                var codigoProveedor = await _repository.GetByProveedor(dto.CodigoProveedor);
                if (codigoProveedor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;
                }

                var codigoOrdenCompra = await _repository.GetByOrdenCompra(dto.CodigoOrdenCompra);
                if (codigoOrdenCompra == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Orden Compra Invalido";
                    return result;
                }
                var origenId = await _bmDescriptivaRepository.GetByTituloId(3);
                if (codigoOrdenCompra == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Origen Id Invalido";
                    return result;
                }
                FechaDto fechaFabricacion = GetFechaDto(dto.FechaFabricacion);
                if (fechaFabricacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Fabricacion Invalida";
                    return result;
                }

                if (dto.NumeroOrdenCompra == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Orden Compra Invalido";
                    return result;
                }
                FechaDto fechaCompra = GetFechaDto(dto.FechaCompra);
                if (fechaCompra == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha compra Invalida";
                    return result;
                }
               
                if (dto.NumeroLote == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Lote Invalido";
                    return result;
                }

                if (dto.ValorInicial == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Valor Inicial Invalido";
                    return result;
                }
                if (dto.ValorActual == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Valor Actual Invalido";
                    return result;
                }
                var numeroPlaca = await _repository.GetByNumeroPlaca(dto.NumeroPlaca);
                if (numeroPlaca != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero placa Invalido";
                    return result;
                }
                
                FechaDto fechaFactura = GetFechaDto(dto.FechaFactura);
                if (fechaCompra == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha compra Invalida";
                    return result;
                }
                if (dto.TipoImpuestoId == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo de Impuesto Id Invalido";
                    return result;
                }




                BM_BIENES entity = new BM_BIENES();
                entity.CODIGO_BIEN = await _repository.GetNextKey();
                entity.CODIGO_ARTICULO = dto.CodigoArticulo;
                entity.CODIGO_PROVEEDOR = dto.CodigoProveedor;
                entity.CODIGO_ORDEN_COMPRA = dto.CodigoOrdenCompra;
                entity.ORIGEN_ID = dto.OrigenId;
                entity.FECHA_FABRICACION = dto.FechaFabricacion;
                entity.NUMERO_ORDEN_COMPRA = dto.NumeroOrdenCompra;
                entity.FECHA_COMPRA = dto.FechaCompra;
                entity.NUMERO_PLACA =dto.NumeroPlaca;
                entity.NUMERO_LOTE = dto.NumeroLote;
                entity.VALOR_INICIAL = dto.ValorInicial;
                entity.VALOR_ACTUAL = dto.ValorActual;
                entity.NUMERO_FACTURA = dto.NumeroFactura;
                entity.FECHA_FACTURA = dto.FechaFactura;
                entity.TIPO_IMPUESTO_ID = dto.TipoImpuestoId;

                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data!=null)
                {
                    var resultDto = await MapBmBienes(created.Data);
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

        public BmBienesGetDto GetDefaultBien()
        {
            BmBienesGetDto itemDefault = new BmBienesGetDto();
            itemDefault.CodigoBien = 0;
            itemDefault.CodigoArticulo=0;
            itemDefault.CodigoProveedor = 0;
            itemDefault.CodigoOrdenCompra=0;
            itemDefault.OrigenId=0;
            itemDefault.NumeroPlaca="";
            itemDefault.NumeroLote="";
            itemDefault.ValorInicial=0;
            itemDefault.ValorActual=0;
            itemDefault.NumeroFactura="";
            itemDefault.TipoImpuestoId=0;
            return itemDefault;
          
        }

       


        public async Task<ResultDto<BmBienesDeleteDto>> Delete(BmBienesDeleteDto dto)
        {

            ResultDto<BmBienesDeleteDto> result = new ResultDto<BmBienesDeleteDto>(null);
            try
            {

                var codigoBien = await _repository.GetByCodigoBien(dto.CodigoBien);
                if (codigoBien == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Bien no existe";
                    return result;
                }

              
                var deleted = await _repository.Delete(dto.CodigoBien);

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

