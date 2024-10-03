using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmDetalleSolicitudRepository: IAdmDetalleSolicitudRepository
    {
        private readonly DataContextAdm _context;
        private readonly IOssConfigRepository _ossConfigRepository;

        public AdmDetalleSolicitudRepository(DataContextAdm context,IOssConfigRepository ossConfigRepository)
        {
            _context = context;
            _ossConfigRepository = ossConfigRepository;
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

        public async Task<TotalesResponseDto> GetTotales(int codigoPresupuesto, int codigoSolicitud)
        {
            TotalesResponseDto result = new TotalesResponseDto();

            var tipoImpuesto = 0;
            string variableImpuesto = "DESCRIPTIVA_IMPUESTO";
            var config = await _ossConfigRepository.GetByClave(variableImpuesto);
            if (config != null)
            {

                tipoImpuesto = int.Parse(config.VALOR);

            }
            var detalle = await _context.ADM_DETALLE_SOLICITUD.DefaultIfEmpty().Where(x =>x.CODIGO_SOLICITUD==codigoSolicitud && x.CODIGO_PRESUPUESTO==codigoPresupuesto ).ToListAsync();
            if (detalle.Count > 0)
            {
                decimal? sum = detalle.Where(x=>x.TIPO_IMPUESTO_ID!=tipoImpuesto).Sum(x => x.TOTAL);
                result.Base = (decimal)sum;
                
                decimal? sumImponible = detalle.Where(x=>x.TIPO_IMPUESTO_ID!=tipoImpuesto && x.MONTO_IMPUESTO>0).Sum(x => x.TOTAL);
                result.BaseImponible = (decimal)sumImponible;
                var detalleImpuesto = await _context.ADM_DETALLE_SOLICITUD.DefaultIfEmpty().Where(x =>x.CODIGO_SOLICITUD==codigoSolicitud && x.CODIGO_PRESUPUESTO==codigoPresupuesto && x.TIPO_IMPUESTO_ID==tipoImpuesto).FirstOrDefaultAsync();
                if (detalleImpuesto != null)
                {
                    result.Impuesto = (decimal)detalleImpuesto.TOTAL;
                    result.BaseImponible = result.Base;
                }
                else
                {
                    decimal? sumImpuesto = detalle.Sum(x => x.MONTO_IMPUESTO);
                    result.Impuesto = (decimal)sumImpuesto;
                }

                result.TotalMasImpuesto = result.Base + result.Impuesto;
                result.PorcentajeImpuesto = 0;
                if(result.BaseImponible != 0)
                {
                    result.PorcentajeImpuesto = (result.Impuesto / result.BaseImponible) * 100;
                }

            }
            else
            {
                result.Base = 0;
                result.Impuesto = 0;
                result.TotalMasImpuesto=0;
                result.PorcentajeImpuesto = 0;
            }
            
            
        


            return result;
        }


        public async Task<bool> ExisteImpuesto(int codigoPresupuesto, int codigoSolicitud)
        {
            var result = false;
            var tipoImpuesto = 0;
            string variableImpuesto = "DESCRIPTIVA_IMPUESTO";
            var config = await _ossConfigRepository.GetByClave(variableImpuesto);
            if (config != null)
            {

                tipoImpuesto = int.Parse(config.VALOR);

            }
            
            var detalleImpuesto = await _context.ADM_DETALLE_SOLICITUD.DefaultIfEmpty().Where(x =>x.CODIGO_SOLICITUD==codigoSolicitud && x.CODIGO_PRESUPUESTO==codigoPresupuesto && x.TIPO_IMPUESTO_ID==tipoImpuesto).FirstOrDefaultAsync();
            if (detalleImpuesto != null)
            {
                result = true;
            }

            return result;
        }
        
        public async  Task RecalculaImpuesto(int codigoPresupuesto,int codigoSolicitud)
        {
            

            
            try
            {
                var tipoImpuesto = 0;
                string variableImpuesto = "DESCRIPTIVA_IMPUESTO";
                var config = await _ossConfigRepository.GetByClave(variableImpuesto);
                if (config != null)
                {

                    tipoImpuesto = int.Parse(config.VALOR);

                }
                var detalleImpuesto = await _context.ADM_DETALLE_SOLICITUD.DefaultIfEmpty().Where(x =>x.CODIGO_SOLICITUD==codigoSolicitud && x.CODIGO_PRESUPUESTO==codigoPresupuesto && x.TIPO_IMPUESTO_ID==tipoImpuesto).FirstOrDefaultAsync();
                if (detalleImpuesto != null)
                {
                    FormattableString xqueryDiario = $"UPDATE ADM.ADM_DETALLE_SOLICITUD SET POR_IMPUESTO=0,MONTO_IMPUESTO=0,TOTAL_MAS_IMPUESTO=ROUND(TOTAL, 2)  WHERE CODIGO_PRESUPUESTO={codigoPresupuesto} AND CODIGO_SOLICITUD ={codigoSolicitud}";

                    var resultDiario =  _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                    
                    FormattableString xqueryDiarioTipoImpuesto = $"UPDATE ADM.ADM_DETALLE_SOLICITUD SET TIPO_IMPUESTO_ID =528 ,POR_IMPUESTO=0,MONTO_IMPUESTO=0,TOTAL_MAS_IMPUESTO=ROUND(TOTAL, 2)  WHERE CODIGO_PRESUPUESTO={codigoPresupuesto} AND CODIGO_SOLICITUD ={codigoSolicitud} AND TIPO_IMPUESTO_ID <> {tipoImpuesto}";

                    var resultDiarioTipoImpuesto =  _context.Database.ExecuteSqlInterpolated(xqueryDiarioTipoImpuesto);
                    
                    
                    FormattableString xqueryDiarioDESCRIPCION = $"UPDATE ADM.ADM_DETALLE_SOLICITUD SET DESCRIPCION='IVA'  WHERE CODIGO_PRESUPUESTO={codigoPresupuesto} AND CODIGO_SOLICITUD ={codigoSolicitud} AND TIPO_IMPUESTO_ID = {tipoImpuesto}";

                    var resultDiarioDescripcion =  _context.Database.ExecuteSqlInterpolated(xqueryDiarioDESCRIPCION);
                    
                }

            }
            catch (Exception ex)
            {
                var mess = ex.InnerException.Message;

                throw;
            }

            
        }
        
        
        public async Task<decimal> GetTotalMonto(List<ADM_DETALLE_SOLICITUD> detalleSolicitud)
        {

            decimal result = 0;
            try
            {
             
                if (detalleSolicitud != null && detalleSolicitud.Count > 0)
                {
                  
                    
                    var total  = detalleSolicitud.Sum(p => p.TOTAL_MAS_IMPUESTO);
                    result = (decimal)total; 
                   
                    
                }
                else
                {
                    result = 0;
                }
              


                return result;
              
            }
            catch (Exception ex)
            {
                return result;
            }

        }
        
        public async Task<decimal> GetTotalImpuesto(List<ADM_DETALLE_SOLICITUD> detalleSolicitud)
        {

            decimal result = 0;
            try
            {
             
                if (detalleSolicitud != null && detalleSolicitud.Count > 0)
                {
                  
                    
                    var total  = detalleSolicitud.Sum(p => p.MONTO_IMPUESTO);
                    result = (decimal)total; 
                   
                    
                }
                else
                {
                    result = 0;
                }
              


                return result;
              
            }
            catch (Exception ex)
            {
                return result;
            }

        }

        
        public async Task<decimal> GetTotal(List<ADM_DETALLE_SOLICITUD> detalleSolicitud)
        {

            decimal result = 0;
            try
            {
             
                if (detalleSolicitud != null && detalleSolicitud.Count > 0)
                {
                  
                    
                    var total  = detalleSolicitud.Sum(p => p.TOTAL);
                    result = (decimal)total; 
                   
                    
                }
                else
                {
                    result = 0;
                }
              


                return result;
              
            }
            catch (Exception ex)
            {
                return result;
            }

        }

        
        
        
        public async Task<string> UpdateSearchText(int codigoSolicitud)
        {

            try
            {
                FormattableString xqueryDiario = $"UPDATE ADM.ADM_DETALLE_SOLICITUD SET ADM.ADM_DETALLE_SOLICITUD.SEARCH_TEXT = TRIM(DESCRIPCION) || (SELECT DESCRIPCION FROM ADM.ADM_DESCRIPTIVAS    WHERE ADM.ADM_DESCRIPTIVAS.DESCRIPCION_ID  = ADM.ADM_DETALLE_SOLICITUD.TIPO_IMPUESTO_ID) WHERE CODIGO_SOLICITUD ={codigoSolicitud}";

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }




        }
        public async Task<ResultDto<List<AdmDetalleSolicitudResponseDto>>> GetByCodigoSolicitud(AdmSolicitudesFilterDto filter) 
        {
            try
            {


                await UpdateSearchText(filter.CodigoSolicitud);
                if (filter.PageNumber == 0) filter.PageNumber = 1;
                if (filter.PageSize == 0) filter.PageSize = 4000;

                if (string.IsNullOrEmpty(filter.SearchText))
                {
                    filter.SearchText = "";
                }
                var totalRegistros = 0;
                var totalPage = 0;
                List<AdmDetalleSolicitudResponseDto> alldata = new List<AdmDetalleSolicitudResponseDto>();
                ResultDto<List<AdmDetalleSolicitudResponseDto>> result = new ResultDto<List<AdmDetalleSolicitudResponseDto>>(null);
               
                List<ADM_DETALLE_SOLICITUD> pageData = new List<ADM_DETALLE_SOLICITUD>();
                var detalle = await _context.ADM_DETALLE_SOLICITUD.DefaultIfEmpty()
                    .Where(x =>x.CODIGO_SOLICITUD==filter.CodigoSolicitud)
                    .OrderBy(X=>X.CODIGO_DETALLE_SOLICITUD)
                    .ToListAsync();
               
                totalRegistros = detalle.Count;

                totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                if (filter.SearchText.Length>0)
                {
                   
                    
                    pageData = detalle
                        .Where(x =>x.SEARCH_TEXT.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToList();
                }
                else
                {
                    pageData = detalle
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToList();
                }
                
                
                
                foreach (var item in pageData)
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
                    resultItem.DescripcionProducto = "";
                    var producto = await _context.ADM_PRODUCTOS.DefaultIfEmpty().Where(x=>x.CODIGO_PRODUCTO== resultItem.CodigoProducto).FirstOrDefaultAsync();
                    if (producto != null)
                    {
                        resultItem.DescripcionProducto =producto.DESCRIPCION;
                    }
                    resultItem.LineaImpuesto = false;
                    string variableImpuesto = "DESCRIPTIVA_IMPUESTO";
                    var config = await _ossConfigRepository.GetByClave(variableImpuesto);
                    if (config != null)
                    {
                        if (resultItem.TipoImpuestoId ==int.Parse(config.VALOR))
                        {
                            resultItem.LineaImpuesto = true;
                        }
                    }
                    
                    alldata.Add(resultItem);
                }

                var totales =await  GetTotales(filter.CodigoPresupuesto,filter.CodigoSolicitud);
                var totalMasImpuesto = totales.TotalMasImpuesto;
                var totalImpuesto = totales.Impuesto;
                var total = totales.Base;
                result.Total3 = total;
                result.Total1 = totalMasImpuesto;
                result.Total4 = totalImpuesto;
                
                result.CantidadRegistros = totalRegistros;
                result.TotalPage = totalPage;
                result.Page = filter.PageNumber;
                result.IsValid = true;
                result.Message = "";
                result.Data = alldata;
                return result;



            }
            catch (Exception ex) 
            {
                var res = ex.Message;
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


        public async Task<string> ActualizaMontos(int codigoPresupuesto)
        {
            
            try
            {
                FormattableString xqueryDiario =$"UPDATE  adm.ADM_DETALLE_SOLICITUD  SET  TOTAL_MAS_IMPUESTO=  decode(por_impuesto,0,round(cantidad*precio_unitario,2),((cantidad*precio_unitario)+(por_impuesto*round((cantidad*precio_unitario)/100,2) ))),TOTAL=(cantidad*precio_unitario),MONTO_IMPUESTO=decode(por_impuesto,0,0,(por_impuesto*round(cantidad*precio_unitario,2)/100 ) ) WHERE codigo_presupuesto = {codigoPresupuesto} AND TOTAL IS NULL";
                ;

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }
        
        public async Task<string> DeleteBySolicitud(int codigoSolicitud)
        {

            try
            {
                FormattableString xqueryDiario = $"DELETE FROM  ADM.ADM_DETALLE_SOLICITUD  WHERE CODIGO_SOLICITUD ={codigoSolicitud}";

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                
                
                FormattableString xqueryDiarioPUC = $"DELETE FROM  ADM.ADM_PUC_SOLICITUD  WHERE CODIGO_SOLICITUD ={codigoSolicitud}";

                var resultDiarioPUC = _context.Database.ExecuteSqlInterpolated(xqueryDiarioPUC);

                
                
                
                
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
