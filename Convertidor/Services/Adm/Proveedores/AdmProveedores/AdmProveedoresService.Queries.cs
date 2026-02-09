using System.Text;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Adm.Proveedores;
using Microsoft.Extensions.Caching.Distributed;
namespace Convertidor.Services.Adm.Proveedores.AdmProveedores;

public partial class AdmProveedoresService
{
            public async Task<ResultDto<AdmProveedorResponseDto>> GetByCodigo(AdmProveedorFilterDto dto)
        { 
            ResultDto<AdmProveedorResponseDto> result = new ResultDto<AdmProveedorResponseDto>(null);
            try
            {

                var proveedor = await _repository.GetByCodigo(dto.CodigoProveedor);
                if (proveedor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor no existe";
                    return result;
                }
                
                var resultDto = await MapProveedorDto(proveedor);
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

        public async Task<ResultDto<List<AdmProveedorResponseDto>>> GetAll()
        {
            ResultDto<List<AdmProveedorResponseDto>> result = new ResultDto<List<AdmProveedorResponseDto>>(null);
            try
            {

                 
                var proveedor = await _repository.GetByAll();
                if (proveedor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor no existe";
                    return result;
                }
            
                result.Data = await MapListProveedorDtoParallel(proveedor);
                    

              
                result.IsValid = true;
                result.Message = "";
                return result;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }
           
        }


    public async Task<ResultDto<List<AdmProveedorResponseDto>>> GetAll(AdmProveedoresFilterDto filter)
        {
            ResultDto<List<AdmProveedorResponseDto>> result = new ResultDto<List<AdmProveedorResponseDto>>(null);
            try
            {
                



              
           
                   
                var proveedor = await _repository.GetByAll();
                if (proveedor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor no existe";
                    return result;
                }
            
                result.Data = await MapListProveedorDtoParallel(proveedor);
         

                result.CantidadRegistros =result.Data.Count();
                result.TotalPage = 1;
                result.Page = filter.PageNumber;
                result.IsValid = true;
                result.Message = "";
             
                return result;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }
           
        }



}