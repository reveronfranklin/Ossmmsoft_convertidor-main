using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm.Proveedores.AdmProveedores;

public partial class AdmProveedoresService
{
    
    public async Task<ResultDto<AdmProveedorDeleteDto>> Delete(AdmProveedorDeleteDto dto)
    {

        ResultDto<AdmProveedorDeleteDto> result = new ResultDto<AdmProveedorDeleteDto>(null);
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


            var deleted = await _repository.Delete(dto.CodigoProveedor);

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