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
            proveedor.STATUS="I";

            var conectado = await _sisUsuarioRepository.GetConectado();
            proveedor.CODIGO_EMPRESA = conectado.Empresa;
            proveedor.USUARIO_UPD = conectado.Usuario;
            await _repository.Update(proveedor);
            result.Data = dto;
            result.IsValid = true;
            result.Message = "Proveedor Eliminado";
           


        }
        catch (Exception ex)
        {
            result.Data = dto;
            result.IsValid = false;
            result.Message = ex.Message;
        }



        return result;
    }

  public async Task<ResultDto<AdmProveedorDeleteDto>> Activar(AdmProveedorDeleteDto dto)
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
            proveedor.STATUS="A";

            var conectado = await _sisUsuarioRepository.GetConectado();
            proveedor.CODIGO_EMPRESA = conectado.Empresa;
            proveedor.USUARIO_UPD = conectado.Usuario;
            await _repository.Update(proveedor);
            result.Data = dto;
            result.IsValid = true;
            result.Message = "Proveedor Activado";
           


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