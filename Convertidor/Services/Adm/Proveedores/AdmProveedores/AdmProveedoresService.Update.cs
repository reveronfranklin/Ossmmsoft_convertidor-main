using Convertidor.Dtos.Adm;
using Convertidor.Utility;

namespace Convertidor.Services.Adm.Proveedores.AdmProveedores;

public partial class AdmProveedoresService
{
            public async Task<ResultDto<AdmProveedorResponseDto?>> Update(AdmProveedorUpdateDto dto)
        {

            ResultDto<AdmProveedorResponseDto?> result = new ResultDto<AdmProveedorResponseDto?>(null);
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
                if (dto.NombreProveedor.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Proveedor Invalida";
                    return result;
                }

                
                var tiposProveedor = await _repositoryPreDescriptiva.GetByTitulo(2);
                if (tiposProveedor.Count<=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Proveedor  Invalido";
                    return result;
                }
                else
                {
                    var tipoProveedor = tiposProveedor.Where(x => x.DESCRIPCION_ID== dto.TipoProveedorId);
                    if (tipoProveedor is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Tipo Proveedor  Invalido";
                        return result;
                    }
                }
               
               

                if (dto.Cedula<= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cedula Proveedor Invalida";
                    return result;
                }
                if (dto.Rif.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Rif Invalida";
                    return result;
                }

              
              
                
                if (dto.CapitalPagado<0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Capital Pagado Invalido";
                    return result;
                }
                if (dto.CapitalSuscrito<0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Capital Suscrito Invalido";
                    return result;
                }
                if (dto.Status.Length<=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
                    return result;
                }
               

                proveedor.NOMBRE_PROVEEDOR = dto.NombreProveedor;
                proveedor.TIPO_PROVEEDOR_ID = dto.TipoProveedorId;
                proveedor.NACIONALIDAD = dto.Nacionalidad;
                proveedor.CEDULA = dto.Cedula;
                proveedor.RIF = dto.Rif;
                proveedor.FECHA_RIF = dto.FechaRif;
                proveedor.NIT = dto.Nit;
                proveedor.FECHA_NIT = dto.FechaNit;
                proveedor.NUMERO_REGISTRO_CONTRALORIA = dto.NumeroRegistroContraloria;
                proveedor.FECHA_REGISTRO_CONTRALORIA = dto.FechaRegistroContraloria;
                proveedor.CAPITAL_PAGADO = dto.CapitalPagado;
                proveedor.CAPITAL_SUSCRITO = dto.CapitalSuscrito;
            
                proveedor.STATUS = dto.Status;
                proveedor.CODIGO_PERSONA = dto.CodigoPersona;
                proveedor.CODIGO_AUXILIAR_GASTO_X_PAGAR = dto.CodigoAuxiliarGastoXPagar;
                proveedor.CODIGO_AUXILIAR_ORDEN_PAGO = dto.CodigoAuxiliarOrdenPago;
                proveedor.ESTATUS_FISCO_ID = dto.EstatusFisicoId;
                proveedor.CODIGO_AUXILIAR_ORDEN_PAGO = dto.CodigoAuxiliarOrdenPago;
                proveedor.NUMERO_CUENTA = dto.NumeroCuenta;
                proveedor.FECHA_UPD = DateTime.Now;
                    var conectado = await _sisUsuarioRepository.GetConectado();
                    proveedor.CODIGO_EMPRESA = conectado.Empresa;
                proveedor.USUARIO_UPD = conectado.Usuario;
                await _repository.Update(proveedor);
                
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

}