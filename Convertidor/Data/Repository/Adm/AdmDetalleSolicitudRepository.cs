using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmDetalleSolicitudRepository: IAdmDetalleSolicitudRepository
    {
        private readonly DataContextAdm _context;
        public AdmDetalleSolicitudRepository(DataContextAdm context)
        {
            _context = context;
        }

        public async Task<ADM_DETALLE_SOLICITUD> GetCodigoDetalleSolicitud(int codigoDetalleSolicitud)
        {
            try
            {
                var result = await _context.ADM_DETALLE_SOLICITUD.DefaultIfEmpty()
                    .Where(e => e.CODIGO_DETALLE_SOLICITUD == codigoDetalleSolicitud).FirstOrDefaultAsync();

                return (ADM_DETALLE_SOLICITUD)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<ADM_DETALLE_SOLICITUD>> GetAll() 
        {
            try
            {
                var result = await _context.ADM_DETALLE_SOLICITUD.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex) 
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }
        
        public async  Task<ADM_DETALLE_SOLICITUD> GetByCodigoSolicitudProducto(int codigoSolicitud,int codigoProducto) 
        {
            try
            {
                
             var result = await _context.ADM_DETALLE_SOLICITUD.DefaultIfEmpty().Where(x =>x.CODIGO_SOLICITUD==codigoSolicitud && x.CODIGO_PRODUCTO==codigoProducto).FirstOrDefaultAsync();
             return result;


            }
            catch (Exception ex) 
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }
        public async Task<List<AdmDetalleSolicitudResponseDto>> GetByCodigoSolicitud(int codigoSolicitud) 
        {
            try
            {


                List<AdmDetalleSolicitudResponseDto> result = new List<AdmDetalleSolicitudResponseDto>();

                var detalle = await _context.ADM_DETALLE_SOLICITUD.DefaultIfEmpty().Where(x =>x.CODIGO_SOLICITUD==codigoSolicitud).ToListAsync();
                foreach (var item in detalle)
                {
                    AdmDetalleSolicitudResponseDto resultItem = new AdmDetalleSolicitudResponseDto();
                    resultItem.CodigoDetalleSolicitud = item.CODIGO_DETALLE_SOLICITUD;
                    resultItem.CodigoSolicitud = item.CODIGO_SOLICITUD;
                    resultItem.Cantidad = item.CANTIDAD;
                    resultItem.CantidadComprada = item.CANTIDAD_COMPRADA;
                    resultItem.CantidadAnulada = item.CANTIDAD_ANULADA;
                    resultItem.UdmId = item.UDM_ID;
                    var descriptivaUnidad = await _context.ADM_DESCRIPTIVAS.DefaultIfEmpty()
                        .Where(x => x.DESCRIPCION_ID == item.UDM_ID).FirstOrDefaultAsync();
                    resultItem.DescripcionUnidad = descriptivaUnidad.DESCRIPCION;
                    resultItem.Descripcion = item.DESCRIPCION;
                    resultItem.CodigoPresupuesto = item.CODIGO_PRESUPUESTO;
                    resultItem.PrecioUnitario = item.PRECIO_UNITARIO;
                    resultItem.PorDescuento = item.POR_DESCUENTO;
                    resultItem.MontoDescuento = item.MONTO_DESCUENTO == null ? 0 : item.MONTO_DESCUENTO;
                    resultItem.TipoImpuestoId = item.TIPO_IMPUESTO_ID;
                    resultItem.PorImpuesto = item.POR_IMPUESTO;
                    resultItem.MontoImpuesto = item.MONTO_IMPUESTO == null ? 0 : item.MONTO_IMPUESTO;
                    resultItem.Total = item.TOTAL == null ? 0 : item.TOTAL;
                    resultItem.TotalMasImpuesto = item.TOTAL_MAS_IMPUESTO == null ? 0 : item.TOTAL_MAS_IMPUESTO;
                    resultItem.CodigoProducto = item.CODIGO_PRODUCTO == null ? 0 : item.CODIGO_PRODUCTO;
                    result.Add(resultItem);
                }
                
                return result;


            }
            catch (Exception ex) 
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }
   public List<AdmDetalleSolicitudResponseDto> GetByCodigoSolicitudBk(int codigoSolicitud) 
        {
            try
            {
                
                var linqQuery = from sol in _context.ADM_DETALLE_SOLICITUD
                    join descTipoSol in _context.ADM_DESCRIPTIVAS on sol.UDM_ID equals descTipoSol.DESCRIPCION_ID
                 
                    select new AdmDetalleSolicitudResponseDto() {
                        CodigoDetalleSolicitud=sol.CODIGO_DETALLE_SOLICITUD,
                        CodigoSolicitud = sol.CODIGO_SOLICITUD,
                        Cantidad=sol.CANTIDAD,
                        CantidadComprada=sol.CANTIDAD_COMPRADA,
                        CantidadAnulada=sol.CANTIDAD_ANULADA,
                        UdmId=sol.UDM_ID,
                        DescripcionUnidad=descTipoSol.DESCRIPCION,
                        Descripcion=sol.DESCRIPCION,
                        CodigoPresupuesto=sol.CODIGO_PRESUPUESTO,
                        PrecioUnitario=sol.PRECIO_UNITARIO,
                        PorDescuento=sol.POR_DESCUENTO,
                        MontoDescuento=sol.MONTO_DESCUENTO == null ? 0: sol.MONTO_DESCUENTO,
                        TipoImpuestoId=sol.TIPO_IMPUESTO_ID,
                        PorImpuesto=sol.POR_IMPUESTO,
                        MontoImpuesto=sol.MONTO_IMPUESTO == null? 0 : sol.MONTO_IMPUESTO  ,
                        Total= sol.TOTAL == null? 0 : sol.TOTAL  ,
                        TotalMasImpuesto= sol.TOTAL_MAS_IMPUESTO ==null ? 0 : sol.TOTAL_MAS_IMPUESTO  ,
                        CodigoProducto = sol.CODIGO_PRODUCTO ==null ? 0 : sol.CODIGO_PRODUCTO
                   
                        
                    };
                var result = linqQuery.Where(x=>x.CodigoSolicitud==codigoSolicitud).ToList();
                //var result = await _context.ADM_SOLICITUDES.DefaultIfEmpty().Where(x =>x.CODIGO_PRESUPUESTO==codigoPresupuesto).ToListAsync();
                return result;


            }
            catch (Exception ex) 
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }
        public async Task<ResultDto<ADM_DETALLE_SOLICITUD>>Add(ADM_DETALLE_SOLICITUD entity) 
        {

            ResultDto<ADM_DETALLE_SOLICITUD> result = new ResultDto<ADM_DETALLE_SOLICITUD>(null);
            try 
            {
                await _context.ADM_DETALLE_SOLICITUD.AddAsync(entity);
                await _context.SaveChangesAsync();


                result.Data = entity;
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

        public async Task<ResultDto<ADM_DETALLE_SOLICITUD>>Update(ADM_DETALLE_SOLICITUD entity) 
        {
            ResultDto<ADM_DETALLE_SOLICITUD> result = new ResultDto<ADM_DETALLE_SOLICITUD>(null);

            try
            {
                ADM_DETALLE_SOLICITUD entityUpdate = await GetCodigoDetalleSolicitud(entity.CODIGO_DETALLE_SOLICITUD);
                if (entityUpdate != null)
                {
                    _context.ADM_DETALLE_SOLICITUD.Update(entity);
                    await _context.SaveChangesAsync();
                    result.Data = entity;
                    result.IsValid = true;
                    result.Message = "";
                    
                }
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
        public async Task<string>Delete(int codigoSolicitud) 
        {
            try
            {
                ADM_DETALLE_SOLICITUD entity = await GetCodigoDetalleSolicitud(codigoSolicitud);
                if (entity != null)
                {
                    _context.ADM_DETALLE_SOLICITUD.Remove(entity);
                    await _context.SaveChangesAsync();
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<int> GetNextKey()
        {
            try
            {
                int result = 0;
                var last = await _context.ADM_DETALLE_SOLICITUD.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_DETALLE_SOLICITUD)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_DETALLE_SOLICITUD + 1;
                }

                return (int)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }



        }
    }
}
