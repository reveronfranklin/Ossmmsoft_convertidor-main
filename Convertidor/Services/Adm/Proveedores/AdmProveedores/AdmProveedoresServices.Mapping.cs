using System.Collections.Concurrent;
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
            if(dtos.STATUS=="A")
            {
                itemResult.Activo = true;
            }
            else
            {
                itemResult.Activo = false;
            }   
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

        public async Task<List<AdmProveedorResponseDto>> MapListProveedorDtoBk(List<ADM_PROVEEDORES> dtos)
        {
            List<AdmProveedorResponseDto> result = new List<AdmProveedorResponseDto>();
           
            
            foreach (var item in dtos)
            {
                

                var itemResult = await   MapProveedorDto(item);
               
                result.Add(itemResult);
            }
            return result;



        }



        ////////////////////////////////Procesos de prueba//////////////////////////////
        /// 
        /// 
        private AdmProveedorResponseDto MapProveedorDtoSync(ADM_PROVEEDORES dtos,Dictionary<int, string> tipoProveedorCache,Dictionary<int, string> estatusFisicoCache)
        {
            var itemResult = new AdmProveedorResponseDto
            {
                CodigoProveedor = dtos.CODIGO_PROVEEDOR,
                NombreProveedor = dtos.NOMBRE_PROVEEDOR,
                TipoProveedorId = dtos.TIPO_PROVEEDOR_ID,
                TipoProveeedor = tipoProveedorCache.TryGetValue(dtos.TIPO_PROVEEDOR_ID, out var tipoDesc) 
                    ? tipoDesc : "",
                Nacionalidad = dtos.NACIONALIDAD,
                Cedula = dtos.CEDULA ?? 0,
                Rif = dtos.RIF,
                FechaRif = dtos.FECHA_RIF,
                FechaRifString = FechaObj.GetFechaString(dtos.FECHA_RIF),
                Nit = dtos.NIT,
                FechaNit = dtos.FECHA_NIT,
                FechaNitString = FechaObj.GetFechaString(dtos.FECHA_NIT),
                NumeroRegistroContraloria = dtos.NUMERO_REGISTRO_CONTRALORIA,
                FechaRegistroContraloria = dtos.FECHA_REGISTRO_CONTRALORIA,
                FechaRegistroContraloriaString = FechaObj.GetFechaString(dtos.FECHA_REGISTRO_CONTRALORIA),
                CapitalPagado = dtos.CAPITAL_PAGADO ?? 0,
                CapitalSuscrito = dtos.CAPITAL_SUSCRITO ?? 0,
                Activo = dtos.STATUS == "A",
                EstatusFisicoId = dtos.ESTATUS_FISCO_ID ?? 0,
                EstatusFisico = (dtos.ESTATUS_FISCO_ID.HasValue && 
                                    estatusFisicoCache.TryGetValue(dtos.ESTATUS_FISCO_ID.Value, out var estatusDesc)) 
                    ? estatusDesc : "",
                NumeroCuenta = dtos.NUMERO_CUENTA
            };

            // Configurar objetos de fecha
            if (dtos.FECHA_RIF.HasValue)
            {
            itemResult.FechaRifObj = FechaObj.GetFechaDto(dtos.FECHA_RIF.Value);
            }

            if (dtos.FECHA_NIT.HasValue)
            {
            itemResult.FechaNitObj = FechaObj.GetFechaDto(dtos.FECHA_NIT.Value);
            }

            if (dtos.FECHA_REGISTRO_CONTRALORIA.HasValue)
            {
            itemResult.FechaRegistroContraloriaObj = FechaObj.GetFechaDto(dtos.FECHA_REGISTRO_CONTRALORIA.Value);
            }

            return itemResult;
        }


        public async Task<List<AdmProveedorResponseDto>> MapListProveedorDtoParallel(List<ADM_PROVEEDORES> dtos)
        {
            if (dtos == null || !dtos.Any())
                return new List<AdmProveedorResponseDto>();

            // 1. Obtener cachés (mismo código que MapListProveedorDto)
            var tipoProveedorIds = dtos.Select(p => p.TIPO_PROVEEDOR_ID).Distinct().ToList();
            var estatusFisicoIds = dtos.Select(p => p.ESTATUS_FISCO_ID ?? 0)
                                    .Where(id => id > 0)
                                    .Distinct()
                                    .ToList();
            
            var allCodes = tipoProveedorIds.Concat(estatusFisicoIds).Distinct().ToList();
            
            var tipoProveedorCache = new Dictionary<int, string>();
            var estatusFisicoCache = new Dictionary<int, string>();
            
            if (allCodes.Any())
            {
                var descriptivas = await _repositoryPreDescriptiva.GetByCodigos(allCodes);
                
                foreach (var desc in descriptivas)
                {
                    if (tipoProveedorIds.Contains(desc.DESCRIPCION_ID))
                        tipoProveedorCache[desc.DESCRIPCION_ID] = desc.DESCRIPCION;
                    
                    if (estatusFisicoIds.Contains(desc.DESCRIPCION_ID))
                        estatusFisicoCache[desc.DESCRIPCION_ID] = desc.DESCRIPCION;
                }
            }

            // 2. Procesar en paralelo
            var result = new ConcurrentBag<AdmProveedorResponseDto>();
            
            Parallel.ForEach(dtos, item =>
            {
                var mappedItem = MapProveedorDtoSync(item, tipoProveedorCache, estatusFisicoCache);
                result.Add(mappedItem);
            });
            
            return result.ToList();
        }





        //////////////////////////////////////////////////////////////////////////////// 
        
}