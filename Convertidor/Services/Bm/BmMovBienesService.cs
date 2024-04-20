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
    public class BmMovBienesService: IBmMovBienesService
    {

      
        private readonly IBmMovBienesRepository _repository;
        private readonly IBmDescriptivaRepository _bmDescriptivaRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
 
        public BmMovBienesService(IBmMovBienesRepository repository,
                                  IBmDescriptivaRepository bmDescriptivaRepository, 
                                  ISisUsuarioRepository sisUsuarioRepository
                             )
		{
            _repository = repository;
            _bmDescriptivaRepository = bmDescriptivaRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
   
           

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

        public async Task<BmMovBienesResponseDto> MapBmMovBienesDto(BM_MOV_BIENES dtos)
        {


            BmMovBienesResponseDto itemResult = new BmMovBienesResponseDto();
            itemResult.CodigoMovBien = dtos.CODIGO_MOV_BIEN;
            itemResult.CodigoBien = dtos.CODIGO_BIEN;
            itemResult.TipoMovimiento = dtos.TIPO_MOVIMIENTO;
            itemResult.FechaMovimiento = dtos.FECHA_MOVIMIENTO;
            itemResult.FechaMovimientoString = dtos.FECHA_MOVIMIENTO.ToString("u");
            FechaDto fechaMovimientoObj = GetFechaDto(dtos.FECHA_MOVIMIENTO);
            itemResult.FechaMovimientoObj = (FechaDto)fechaMovimientoObj;
            itemResult.CodigoDirBien = dtos.CODIGO_DIR_BIEN;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;    
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.ConceptoMovId=dtos.CONCEPTO_MOV_ID;
            itemResult.CodigoSolMovBien = dtos.CODIGO_SOL_MOV_BIEN;
       

            return itemResult;

        }
        public async Task<List<BmMovBienesResponseDto>> MapListBmMovBienesDto(List<BM_MOV_BIENES> dtos)
        {
            List<BmMovBienesResponseDto> result = new List<BmMovBienesResponseDto>();


            foreach (var item in dtos)
            {

                BmMovBienesResponseDto itemResult = new BmMovBienesResponseDto();
                itemResult = await MapBmMovBienesDto(item);

                result.Add(itemResult);
            }
            return result;
        }

        public async Task<BmMovBienesResponseDto> GetByCodigoMovBien(int codigoMovBien)
        {
            try
            {
                var movBien = await _repository.GetByCodigoMovBien(codigoMovBien);

                var result = await MapBmMovBienesDto(movBien);


                return (BmMovBienesResponseDto)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<ResultDto<List<BmMovBienesResponseDto>>> GetAll()
        {

            ResultDto<List<BmMovBienesResponseDto>> result = new ResultDto<List<BmMovBienesResponseDto>>(null);
            try
            {

                var titulos = await _repository.GetAll();



                if (titulos.Count() > 0)
                {
                    List<BmMovBienesResponseDto> listDto = new List<BmMovBienesResponseDto>();

                    foreach (var item in titulos)
                    {
                        BmMovBienesResponseDto dto = new BmMovBienesResponseDto();
                        dto = await MapBmMovBienesDto(item);

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

        


        public async Task<ResultDto<BmMovBienesResponseDto>> Update(BmMovBienesUpdateDto dto)
        {

            ResultDto<BmMovBienesResponseDto> result = new ResultDto<BmMovBienesResponseDto>(null);
            try
            {

                var codigoMovBien = await _repository.GetByCodigoMovBien(dto.CodigoMovBien);
                if (codigoMovBien == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo mov bien no existe";
                    return result;
                }
                var codigoBien = await _repository.GetByCodigoBien(dto.CodigoBien);
                if (codigoBien == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo bien invalido";
                    return result;
                }
                if (dto.TipoMovimiento==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Movimiento Invalido";
                    return result;
                }
                if (dto.FechaMovimiento==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha movimiento Invalido";
                    return result;
                }
                if (dto.CodigoDirBien == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo dir bien Invalido";
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

                var conceptoMovimientoId = await _bmDescriptivaRepository.GetByIdAndTitulo(4, dto.ConceptoMovId);
                if(conceptoMovimientoId==false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Concepto movimiento Id invalido";
                    return result;
                }

                if (dto.CodigoSolMovBien < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Sol Mov Bien invalido";
                    return result;
                }

                codigoMovBien.CODIGO_MOV_BIEN = dto.CodigoMovBien;
                codigoMovBien.CODIGO_BIEN = dto.CodigoBien;
                codigoMovBien.TIPO_MOVIMIENTO = dto.TipoMovimiento;
                codigoMovBien.FECHA_MOVIMIENTO = dto.FechaMovimiento;
                codigoMovBien.CODIGO_DIR_BIEN = dto.CodigoDirBien;
                codigoMovBien.EXTRA1 = dto.Extra1;
                codigoMovBien.EXTRA2 = dto.Extra2;
                codigoMovBien.EXTRA3 = dto.Extra3;
                codigoMovBien.CONCEPTO_MOV_ID=dto.ConceptoMovId;
                codigoMovBien.CODIGO_SOL_MOV_BIEN=dto.CodigoSolMovBien;

                var conectado = await _sisUsuarioRepository.GetConectado();
                codigoMovBien.CODIGO_EMPRESA = conectado.Empresa;
                codigoMovBien.USUARIO_UPD = conectado.Usuario;
                codigoMovBien.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoMovBien);

                var resultDto = await MapBmMovBienesDto(codigoMovBien);
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

        public async Task<ResultDto<BmMovBienesResponseDto>> Create(BmMovBienesUpdateDto dto)
        {

            ResultDto<BmMovBienesResponseDto> result = new ResultDto<BmMovBienesResponseDto>(null);
            try
            {

                var codigoMovBien = await _repository.GetByCodigoMovBien(dto.CodigoMovBien);
                if (codigoMovBien != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Mov Bien ya existe";
                    return result;
                }
                var codigoBien = await _repository.GetByCodigoBien(dto.CodigoBien);
                if (dto.CodigoBien == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Bien Invalido";
                    return result;
                }
                
                if (dto.TipoMovimiento == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Movimiento invalido";
                    return result;
                }
                if (dto.FechaMovimiento==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Movimiento Invalido";
                    return result;
                }
                if (dto.CodigoDirBien==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo dir bien Invalido";
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

                var conceptoMovimientoId = await _bmDescriptivaRepository.GetByIdAndTitulo(4, dto.ConceptoMovId);
                if (conceptoMovimientoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Concepto movimiento Id invalido";
                    return result;
                }

                if (dto.CodigoSolMovBien < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Sol Mov Bien invalido";
                    return result;
                }


                BM_MOV_BIENES entity = new BM_MOV_BIENES();        
                entity.CODIGO_MOV_BIEN = await _repository.GetNextKey();
                entity.CODIGO_BIEN = dto.CodigoBien;
                entity.TIPO_MOVIMIENTO = dto.TipoMovimiento;
                entity.FECHA_MOVIMIENTO = dto.FechaMovimiento;
                entity.CODIGO_DIR_BIEN = dto.CodigoDirBien;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.CONCEPTO_MOV_ID = dto.ConceptoMovId;
                entity.CODIGO_SOL_MOV_BIEN = dto.CodigoSolMovBien;

                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data!=null)
                {
                    var resultDto = await MapBmMovBienesDto(created.Data);
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

        public async Task<ResultDto<BmMovBienesDeleteDto>>Delete(BmMovBienesDeleteDto dto)
        {

            ResultDto<BmMovBienesDeleteDto> result = new ResultDto<BmMovBienesDeleteDto>(null);
            try
            {

                var codigoMovBien = await _repository.GetByCodigoMovBien(dto.CodigoMovBien);
                if (codigoMovBien == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Mov Bien no existe";
                    return result;
                }

              
                var deleted = await _repository.Delete(dto.CodigoMovBien);

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

