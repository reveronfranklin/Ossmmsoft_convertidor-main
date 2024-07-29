using System.Text;
using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Convertidor.Data.Repository.Adm
{
	public class AdmProductosRepository: IAdmProductosRepository
    {
		
        private readonly DataContextAdm _context;
        private readonly IDistributedCache _distributedCache;

        public AdmProductosRepository(DataContextAdm context,IDistributedCache distributedCache)
        {
            _context = context;
            _distributedCache = distributedCache;
        }
      
        public async Task<string> UpdateSearchText()
        {

            try
            {
                FormattableString xqueryDiario = $"UPDATE ADM.ADM_PRODUCTOS SET ADM.ADM_PRODUCTOS.SEARCH_TEXT = TRIM(CODIGO_PRODUCTO) || CODIGO || '-' || TRIM(CODIGO_PRODUCTO1) || '-' || TRIM(CODIGO_PRODUCTO2) || '-' || TRIM(CODIGO_PRODUCTO3) || '-' || TRIM(CODIGO_PRODUCTO3) || '-' || TRIM(DESCRIPCION)  || '-' || TRIM(DESCRIPCION_REAL) WHERE SEARCH_TEXT IS NULL";

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }




        }

        
        public async Task<ADM_PRODUCTOS> GetByCodigo(int codigoProducto)
        {
            try
            {
                var result = await _context.ADM_PRODUCTOS.DefaultIfEmpty().Where(e => e.CODIGO_PRODUCTO == codigoProducto).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        public async Task<List<ADM_PRODUCTOS>> GetProductosCache()
        {
            try
            {
                var listHistorico = new List<ADM_PRODUCTOS>();

                var cacheKey = "listProductos";

                var serializedListNominaPeriodo = string.Empty;

                var redisListNominaPeriodo = await _distributedCache.GetAsync(cacheKey);
                if (redisListNominaPeriodo != null)
                {
                    serializedListNominaPeriodo = System.Text.Encoding.UTF8.GetString(redisListNominaPeriodo);
                    listHistorico = JsonConvert.DeserializeObject<List<ADM_PRODUCTOS>>(serializedListNominaPeriodo);
                 
                }
                else
                {
                    listHistorico = await GetAll();
                    serializedListNominaPeriodo = JsonConvert.SerializeObject(listHistorico);
                    redisListNominaPeriodo = Encoding.UTF8.GetBytes(serializedListNominaPeriodo);

                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddDays(60))
                        .SetSlidingExpiration(TimeSpan.FromDays(30));
                    await _distributedCache.SetAsync(cacheKey, redisListNominaPeriodo, options);

                }

                return listHistorico;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<ADM_PRODUCTOS>> GetAll()
        {
            try
            {
                var result = await _context.ADM_PRODUCTOS
                    .DefaultIfEmpty()
                    .Where(x=> x.CODIGO_PRODUCTO4!="00")
                    .ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        
        public async Task<ResultDto<List<AdmProductosResponse>>> GetAllPaginate(AdmProductosFilterDto filter) 
        {
            ResultDto<List<AdmProductosResponse>> result = new ResultDto<List<AdmProductosResponse>>(null);

            if (filter.PageNumber == 0) filter.PageNumber = 1;
            if (filter.PageSize == 0) filter.PageSize = 100;
            if (filter.PageSize >100) filter.PageSize = 100;
            await UpdateSearchText();
            try
            {

                var productos = await GetProductosCache();
                var totalRegistros = 0;
                var totalPage = 0;
              
                List<ADM_PRODUCTOS> pageData;
                if (filter.SearchText.Length > 0)
                {
                    totalRegistros =productos
                        .Where(x => x.CODIGO_PRODUCTO4!="00" && x.SEARCH_TEXT.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    
                    pageData = productos
                        .Where(x => x.CODIGO_PRODUCTO4!="00" && x.SEARCH_TEXT.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .OrderBy(x => x.DESCRIPCION)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToList();
                }
                else
                {
                    totalRegistros = productos.Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    pageData = productos
                        .DefaultIfEmpty()
                        .Where(x=>x.CODIGO_PRODUCTO4!="00")
                        .OrderBy(x => x.DESCRIPCION)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToList();
                }
             
                
                
                
                List<AdmProductosResponse> resultData = new List<AdmProductosResponse>();
                foreach (var item in pageData)
                {
                    AdmProductosResponse itemData = new AdmProductosResponse();
                    itemData.Codigo = item.CODIGO_PRODUCTO;
                    itemData.CodigoProducto1 = item.CODIGO_PRODUCTO1;
                    itemData.CodigoProducto2 = item.CODIGO_PRODUCTO2;
                    itemData.CodigoProducto3 = item.CODIGO_PRODUCTO3;
                    itemData.CodigoProducto4 = item.CODIGO_PRODUCTO4;
                    itemData.Descripcion = item.DESCRIPCION;
                    if (item.DESCRIPCION_REAL == null) item.DESCRIPCION_REAL = "";
                    itemData.DescripcionReal = item.DESCRIPCION_REAL;
                    itemData.CodigoConcat =
                        $"{item.CODIGO_PRODUCTO1}-{item.CODIGO_PRODUCTO1}-{item.CODIGO_PRODUCTO2}-{item.CODIGO_PRODUCTO3}-{item.CODIGO_PRODUCTO4}";
                    resultData.Add(itemData);
                }
                
                result.CantidadRegistros = totalRegistros;
                result.TotalPage = totalPage;
                result.Page = filter.PageNumber;
                result.IsValid = true;
                result.Message = "";
                result.Data = resultData;
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

        
   public async Task<ResultDto<List<AdmProductosResponse>>> GetAllPaginateCopia(AdmProductosFilterDto filter) 
        {
            ResultDto<List<AdmProductosResponse>> result = new ResultDto<List<AdmProductosResponse>>(null);

            if (filter.PageNumber == 0) filter.PageNumber = 1;
            if (filter.PageSize == 0) filter.PageSize = 100;
            if (filter.PageSize >100) filter.PageSize = 100;
            
            try
            {

              
                var totalRegistros = 0;
                var totalPage = 0;
              
                List<ADM_PRODUCTOS> pageData;
                if (filter.SearchText.Length > 0)
                {
                    totalRegistros = _context.ADM_PRODUCTOS
                        .Where(x => x.CODIGO_PRODUCTO4!="00" && x.DESCRIPCION.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    
                    pageData = await _context.ADM_PRODUCTOS.DefaultIfEmpty()
                        .Where(x => x.CODIGO_PRODUCTO4!="00" && x.DESCRIPCION.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .OrderBy(x => x.DESCRIPCION)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
                else
                {
                    totalRegistros = _context.ADM_PRODUCTOS.Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    pageData = await _context.ADM_PRODUCTOS
                        .DefaultIfEmpty()
                        .Where(x=>x.CODIGO_PRODUCTO4!="00")
                        .OrderBy(x => x.DESCRIPCION)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
             
                
                
                
                List<AdmProductosResponse> resultData = new List<AdmProductosResponse>();
                foreach (var item in pageData)
                {
                    AdmProductosResponse itemData = new AdmProductosResponse();
                    itemData.Codigo = item.CODIGO_PRODUCTO;
                    itemData.Descripcion = item.DESCRIPCION;
                    resultData.Add(itemData);
                }
                
                result.CantidadRegistros = totalRegistros;
                result.TotalPage = totalPage;
                result.Page = filter.PageNumber;
                result.IsValid = true;
                result.Message = "";
                result.Data = resultData;
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

        public async Task<ResultDto<ADM_PRODUCTOS>> Add(ADM_PRODUCTOS entity)
        {
            ResultDto<ADM_PRODUCTOS> result = new ResultDto<ADM_PRODUCTOS>(null);
            try
            {



                await _context.ADM_PRODUCTOS.AddAsync(entity);
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

        public async Task<ResultDto<ADM_PRODUCTOS>> Update(ADM_PRODUCTOS entity)
        {
            ResultDto<ADM_PRODUCTOS> result = new ResultDto<ADM_PRODUCTOS>(null);

            try
            {
                ADM_PRODUCTOS entityUpdate = await GetByCodigo(entity.CODIGO_PRODUCTO);
                if (entityUpdate != null)
                {


                    _context.ADM_PRODUCTOS.Update(entity);
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

        public async Task<string> Delete(int codigoProducto)
        {

            try
            {
                ADM_PRODUCTOS entity = await GetByCodigo(codigoProducto);
                if (entity != null)
                {
                    _context.ADM_PRODUCTOS.Remove(entity);
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
                var last = await _context.ADM_PRODUCTOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_PRODUCTO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_PRODUCTO + 1;
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

