using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm;
using Convertidor.Utility;

namespace Convertidor.Services.Adm.Proveedores.AdmProveedores;

public partial class AdmProveedoresService
{
            public AdmProveedorResponseDto MapProveedorDto(ADM_PROVEEDORES dtos)
        {
            AdmProveedorResponseDto itemResult = new AdmProveedorResponseDto();
            itemResult.CodigoProveedor = dtos.CODIGO_PROVEEDOR;
            itemResult.NombreProveedor = dtos.NOMBRE_PROVEEDOR;
            itemResult.TipoProveedorId = dtos.TIPO_PROVEEDOR_ID;
            itemResult.Nacionalidad = dtos.NACIONALIDAD;
            itemResult.Cedula = dtos.CEDULA;
            itemResult.Rif = dtos.RIF;
            itemResult.FechaRif = dtos.FECHA_RIF;
            itemResult.FechaRifString =dtos.FECHA_RIF.ToString("u");   
            FechaDto fechaRifObj = FechaObj.GetFechaDto(dtos.FECHA_RIF);
            itemResult.FechaRifObj = fechaRifObj;
            itemResult.Nit = dtos.NIT;
            itemResult.FechaNit = dtos.FECHA_NIT;
            itemResult.FechaNitString =dtos.FECHA_NIT.ToString("u");  
            FechaDto fechaNitObj = FechaObj.GetFechaDto(dtos.FECHA_NIT);
            itemResult.FechaNitObj  =fechaNitObj;
            itemResult.NumeroRegistroContraloria= dtos.NUMERO_REGISTRO_CONTRALORIA;
            itemResult.FechaRegistroContraloria = dtos.FECHA_REGISTRO_CONTRALORIA;
            itemResult.FechaRegistroContraloriaString  =dtos.FECHA_REGISTRO_CONTRALORIA.ToString("u");  
            FechaDto fechaRegistroContraloriaObj = FechaObj.GetFechaDto(dtos.FECHA_REGISTRO_CONTRALORIA);
            itemResult.FechaRegistroContraloriaObj  =fechaRegistroContraloriaObj;
            itemResult.CapitalPagado = dtos.CAPITAL_PAGADO;
            itemResult.CapitalSuscrito = dtos.CAPITAL_SUSCRITO;
            itemResult.TipoImpuestoId = dtos.TIPO_IMPUESTO_ID;
            itemResult.Status = dtos.STATUS;
            itemResult.CodigoPersona = dtos.CODIGO_PERSONA;
            itemResult.CodigoAuxiliarGastoXPagar = dtos.CODIGO_AUXILIAR_ORDEN_PAGO;
            itemResult.CodigoAuxiliarOrdenPago = dtos.CODIGO_AUXILIAR_ORDEN_PAGO;
            itemResult.EstatusFisicoId= dtos.ESTATUS_FISCO_ID;
            itemResult.NumeroCuenta = dtos.NUMERO_CUENTA;
            return itemResult;
        }

        public List<AdmProveedorResponseDto> MapListProveedorDto(List<ADM_PROVEEDORES> dtos)
        {
            List<AdmProveedorResponseDto> result = new List<AdmProveedorResponseDto>();
           
            
            foreach (var item in dtos)
            {
                

                var itemResult =  MapProveedorDto(item);
               
                result.Add(itemResult);
            }
            return result;



        }

        
}