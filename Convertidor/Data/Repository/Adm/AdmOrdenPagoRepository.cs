using System.Globalization;
using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmOrdenPagoRepository: IAdmOrdenPagoRepository
    {
        private readonly DataContextAdm _context;
        public AdmOrdenPagoRepository(DataContextAdm context)
        {
            _context = context;
        }

        public async Task<ADM_ORDEN_PAGO> GetCodigoOrdenPago(int codigoOrdenPago)
        {
            try
            {
                var result = await _context.ADM_ORDEN_PAGO
                    .Where(e => e.CODIGO_ORDEN_PAGO == codigoOrdenPago).DefaultIfEmpty().FirstOrDefaultAsync();

                return (ADM_ORDEN_PAGO)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

         public async Task<string> UpdateSearchText(int codigoPresupuesto)
        {

            try
            {
                FormattableString xqueryDiario = $"UPDATE ADM.ADM_ORDEN_PAGO SET ADM.ADM_ORDEN_PAGO.SEARCH_TEXT =  STATUS || TRIM(MOTIVO) || (SELECT DESCRIPCION FROM ADM.ADM_DESCRIPTIVAS WHERE ADM.ADM_DESCRIPTIVAS.DESCRIPCION_ID  = ADM.ADM_ORDEN_PAGO.TIPO_ORDEN_PAGO_ID) || (SELECT DESCRIPCION FROM ADM.ADM_DESCRIPTIVAS    WHERE ADM.ADM_DESCRIPTIVAS.DESCRIPCION_ID  = ADM.ADM_ORDEN_PAGO.FRECUENCIA_PAGO_ID) || (SELECT NOMBRE_PROVEEDOR FROM ADM.ADM_PROVEEDORES   WHERE  ADM.ADM_PROVEEDORES.CODIGO_PROVEEDOR  =ADM.ADM_ORDEN_PAGO.CODIGO_PROVEEDOR) || (SELECT DESCRIPCION FROM ADM.ADM_DESCRIPTIVAS    WHERE ADM.ADM_DESCRIPTIVAS.DESCRIPCION_ID  = ADM.ADM_ORDEN_PAGO.TIPO_PAGO_ID) WHERE CODIGO_PRESUPUESTO ={codigoPresupuesto}";

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }




        }
        
        public async Task<ResultDto<List<ADM_ORDEN_PAGO>>> GetByPresupuesto(AdmOrdenPagoFilterDto filter) 
        {
            ResultDto<List<ADM_ORDEN_PAGO>> result = new ResultDto<List<ADM_ORDEN_PAGO>>(null);

            if (filter.PageNumber == 0) filter.PageNumber = 1;
            if (filter.PageSize == 0) filter.PageSize = 100;
            if (filter.PageSize >100) filter.PageSize = 100;

            if (string.IsNullOrEmpty(filter.SearchText))
            {
                filter.SearchText = "";
            }
            if (string.IsNullOrEmpty(filter.Status))
            {
                filter.Status = "";
            }
            try
            {

                var updateSearchText = await UpdateSearchText(filter.CodigoPresupuesto);
                var totalRegistros = 0;
                var totalPage = 0;
              
                List<ADM_ORDEN_PAGO> pageData = new List<ADM_ORDEN_PAGO>();
                
                if (filter.Status.Length > 0 && filter.SearchText.Length==0)
                {
                    totalRegistros = _context.ADM_ORDEN_PAGO
                        .Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto && x.STATUS==filter.Status )
                        .Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    
                    pageData = await _context.ADM_ORDEN_PAGO.DefaultIfEmpty()
                        .Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto &&   x.STATUS==filter.Status )
                        .OrderByDescending(x => x.CODIGO_ORDEN_PAGO)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
                if (filter.Status.Length >  0 && filter.SearchText.Length>0)
                {
                    totalRegistros = _context.ADM_ORDEN_PAGO
                        .Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto && x.STATUS==filter.Status && x.SEARCH_TEXT.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    
                    pageData = await _context.ADM_ORDEN_PAGO.DefaultIfEmpty()
                        .Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto &&   x.STATUS==filter.Status && x.SEARCH_TEXT.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .OrderByDescending(x => x.CODIGO_ORDEN_PAGO)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
                if (filter.SearchText.Length > 0 && filter.Status.Length==0)
                {
                    totalRegistros = _context.ADM_ORDEN_PAGO
                        .Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto && x.SEARCH_TEXT.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    
                    pageData = await _context.ADM_ORDEN_PAGO.DefaultIfEmpty()
                        .Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto && x.SEARCH_TEXT.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .OrderByDescending(x => x.CODIGO_ORDEN_PAGO)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
                if (filter.SearchText.Length == 0 && filter.Status.Length==0)
                {
                    totalRegistros = _context.ADM_ORDEN_PAGO.Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto).Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    pageData = await _context.ADM_ORDEN_PAGO.DefaultIfEmpty()
                        .Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto)
                        .OrderByDescending(x => x.CODIGO_ORDEN_PAGO)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
             
                
                result.CantidadRegistros = totalRegistros;
                result.TotalPage = totalPage;
                result.Page = filter.PageNumber;
                result.IsValid = true;
                result.Message = "";
                result.Data = pageData;
                return result;
                
            }
            catch (Exception ex) 
            {
                result.CantidadRegistros = 0;
                result.IsValid = false;
                result.Message = ex.Message;
                result.Data = null;
                return result;
            }
        }


        public async Task<ResultDto<ADM_ORDEN_PAGO>>Add(ADM_ORDEN_PAGO entity) 
        {

            ResultDto<ADM_ORDEN_PAGO> result = new ResultDto<ADM_ORDEN_PAGO>(null);
            try 
            {
                await _context.ADM_ORDEN_PAGO.AddAsync(entity);
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
                if (ex.InnerException != null)
                {
                    result.Message = ex.InnerException.Message;
                }
                else
                {
                    result.Message = ex.Message;
                }
                
                return result;
            }
        }

        public async Task<ResultDto<ADM_ORDEN_PAGO>>Update(ADM_ORDEN_PAGO entity) 
        {
            ResultDto<ADM_ORDEN_PAGO> result = new ResultDto<ADM_ORDEN_PAGO>(null);

            try
            {
                ADM_ORDEN_PAGO entityUpdate = await GetCodigoOrdenPago(entity.CODIGO_ORDEN_PAGO);
                if (entityUpdate != null)
                {
                    _context.ADM_ORDEN_PAGO.Update(entity);
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
        public async Task<string>Delete(int codigoOrdenPago) 
        {
            try
            {
                ADM_ORDEN_PAGO entity = await GetCodigoOrdenPago(codigoOrdenPago);
                if (entity != null)
                {
                    _context.ADM_ORDEN_PAGO.Remove(entity);
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
                var last = await _context.ADM_ORDEN_PAGO.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_ORDEN_PAGO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_ORDEN_PAGO + 1;
                }

                return (int)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }



        }
        
        
        public async Task<string> UpdateMontoEnLetras(int codigoOrdenPago,decimal monto)
        {


            string montoString = monto.ToString(CultureInfo.InvariantCulture);
            try
            {
            
                
                FormattableString xqueryDiario = $"CALL ADM.ADM_P_MONTO_LETRAS_OP({codigoOrdenPago}, {monto});";

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);

                
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }




        }
        
        public async Task<string> GetNextOrdenPago(int codigoPresupuesto)
        {
            try
            {
                string result = "";
                var ordenesPago =  await _context.ADM_ORDEN_PAGO.DefaultIfEmpty()
                    .Where(x=>x.CODIGO_PRESUPUESTO==codigoPresupuesto)
                    .ToListAsync();
                var last = ordenesPago
                    .OrderByDescending(x => int.Parse(x.NUMERO_ORDEN_PAGO))
                    .FirstOrDefault();
                if (last == null)
                {
                    result = "1";
                }
                else
                {
                    var ultimo=Convert.ToInt32(last.NUMERO_ORDEN_PAGO) + 1;
                    result =ultimo.ToString();
                }

                return result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return "";
            }



        }
    }
}
