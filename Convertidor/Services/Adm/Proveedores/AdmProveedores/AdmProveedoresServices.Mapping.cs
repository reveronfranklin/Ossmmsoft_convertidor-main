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
            itemResult.Cedula = (int)dtos.CEDULA;
            itemResult.Rif = dtos.RIF;
            itemResult.FechaRif = (DateTime)dtos.FECHA_RIF;
            itemResult.FechaRifString =FechaObj.GetFechaString((DateTime)dtos.FECHA_RIF);   
            FechaDto fechaRifObj = FechaObj.GetFechaDto((DateTime)dtos.FECHA_RIF);
            itemResult.FechaRifObj = fechaRifObj;
            itemResult.Nit = dtos.NIT;
            itemResult.FechaNit = (DateTime)dtos.FECHA_NIT;
            itemResult.FechaNitString =FechaObj.GetFechaString(dtos.FECHA_NIT);  
            FechaDto fechaNitObj = FechaObj.GetFechaDto((DateTime)dtos.FECHA_NIT);
            itemResult.FechaNitObj  =fechaNitObj;
            itemResult.NumeroRegistroContraloria= dtos.NUMERO_REGISTRO_CONTRALORIA;
            itemResult.FechaRegistroContraloria = (DateTime)dtos.FECHA_REGISTRO_CONTRALORIA;
            itemResult.FechaRegistroContraloriaString  =FechaObj.GetFechaString(dtos.FECHA_REGISTRO_CONTRALORIA);  
            FechaDto fechaRegistroContraloriaObj = FechaObj.GetFechaDto((DateTime)dtos.FECHA_REGISTRO_CONTRALORIA);
            itemResult.FechaRegistroContraloriaObj  =fechaRegistroContraloriaObj;
            itemResult.CapitalPagado = (decimal)dtos.CAPITAL_PAGADO;
            itemResult.CapitalSuscrito = (decimal)dtos.CAPITAL_SUSCRITO;
            itemResult.TipoImpuestoId = (int)dtos.TIPO_IMPUESTO_ID;
            itemResult.Status = dtos.STATUS;
            itemResult.CodigoPersona = (int)dtos.CODIGO_PERSONA;
            itemResult.CodigoAuxiliarGastoXPagar =  (int)dtos.CODIGO_AUXILIAR_ORDEN_PAGO;
            itemResult.CodigoAuxiliarOrdenPago =  (int)dtos.CODIGO_AUXILIAR_ORDEN_PAGO;
            itemResult.EstatusFisicoId=  (int)dtos.ESTATUS_FISCO_ID;
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