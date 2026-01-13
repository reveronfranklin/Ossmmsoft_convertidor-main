using System.Threading.Tasks;
using Convertidor.Data.Entities.Adm;
using Convertidor.Dtos.Adm;
using Convertidor.Utility;

namespace Convertidor.Services.Adm.Proveedores.AdmProveedores;

public partial class AdmProveedoresService
{
        public async Task<AdmProveedorResponseDto> MapProveedorDto(ADM_PROVEEDORES dtos)
        {
            AdmProveedorResponseDto itemResult = new AdmProveedorResponseDto();
            itemResult.CodigoProveedor = dtos.CODIGO_PROVEEDOR;
            itemResult.NombreProveedor = dtos.NOMBRE_PROVEEDOR;
            itemResult.TipoProveedorId = dtos.TIPO_PROVEEDOR_ID;
            itemResult.TipoProveeedor="";
            var tipoProveedor =await _repositoryPreDescriptiva.GetByCodigo((int)dtos.TIPO_PROVEEDOR_ID);
            if (tipoProveedor != null)
            {
                itemResult.TipoProveeedor = tipoProveedor.DESCRIPCION;
            }
            itemResult.Nacionalidad = dtos.NACIONALIDAD;
            if (dtos.CEDULA==null) dtos.CEDULA = 0;
            itemResult.Cedula = (int)dtos.CEDULA;
            itemResult.Rif = dtos.RIF;
            itemResult.FechaRif =dtos.FECHA_RIF;
            itemResult.FechaRifString =FechaObj.GetFechaString(dtos.FECHA_RIF);
            if (dtos.FECHA_RIF != null)
            {
                FechaDto fechaRifObj = FechaObj.GetFechaDto((DateTime)dtos.FECHA_RIF);
                itemResult.FechaRifObj = fechaRifObj;
            }
           
            itemResult.Nit = dtos.NIT;
            itemResult.FechaNit = dtos.FECHA_NIT;
            if (dtos.FECHA_NIT != null)
            {
                FechaDto fechaNitObj = FechaObj.GetFechaDto((DateTime)dtos.FECHA_NIT);
                itemResult.FechaNitObj  =fechaNitObj;
            }
            itemResult.FechaNitString =FechaObj.GetFechaString(dtos.FECHA_NIT);  
          
            itemResult.NumeroRegistroContraloria= dtos.NUMERO_REGISTRO_CONTRALORIA;
            itemResult.FechaRegistroContraloria = dtos.FECHA_REGISTRO_CONTRALORIA;
            itemResult.FechaRegistroContraloriaString  =FechaObj.GetFechaString(dtos.FECHA_REGISTRO_CONTRALORIA);
            if (dtos.FECHA_REGISTRO_CONTRALORIA != null)
            {
                FechaDto fechaRegistroContraloriaObj = FechaObj.GetFechaDto((DateTime)dtos.FECHA_REGISTRO_CONTRALORIA);
                itemResult.FechaRegistroContraloriaObj  =fechaRegistroContraloriaObj;

            }
            if (dtos.CAPITAL_PAGADO==null) dtos.CAPITAL_PAGADO = 0;
            itemResult.CapitalPagado = dtos.CAPITAL_PAGADO;
            if (dtos.CAPITAL_SUSCRITO==null) dtos.CAPITAL_SUSCRITO = 0;
            itemResult.CapitalSuscrito = dtos.CAPITAL_SUSCRITO;
            itemResult.Status = dtos.STATUS;    
            if(dtos.ESTATUS_FISCO_ID==null) dtos.ESTATUS_FISCO_ID = 0;
            itemResult.EstatusFisicoId=  dtos.ESTATUS_FISCO_ID;
            itemResult.EstatusFisico = "";
            var estatusFisico =await _repositoryPreDescriptiva.GetByCodigo((int)dtos.ESTATUS_FISCO_ID);
            if (estatusFisico != null)
            {
                itemResult.EstatusFisico = estatusFisico.DESCRIPCION;
            }
            itemResult.NumeroCuenta = dtos.NUMERO_CUENTA;
            return itemResult;
        }

        public async Task<List<AdmProveedorResponseDto>> MapListProveedorDto(List<ADM_PROVEEDORES> dtos)
        {
            List<AdmProveedorResponseDto> result = new List<AdmProveedorResponseDto>();
           
            
            foreach (var item in dtos)
            {
                

                var itemResult = await   MapProveedorDto(item);
               
                result.Add(itemResult);
            }
            return result;



        }

        
}