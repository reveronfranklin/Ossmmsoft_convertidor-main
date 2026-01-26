using System.Text;
using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Adm.Proveedores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;


namespace Convertidor.Data.Repository.Adm
{
	public class AdmProveedoresRepository: IAdmProveedoresRepository
    {
		
        private readonly DataContextAdm _context;

         private readonly IDistributedCache _distributedCache;
         private const string ProveedoresCacheKey = "ResultDto<List<AdmProveedorResponseDto>>";

        public AdmProveedoresRepository(DataContextAdm context, IDistributedCache distributedCache)
        {
            _context = context;
            _distributedCache = distributedCache;
        
        }
      
        public async Task<ADM_PROVEEDORES> GetByCodigo(int id)
        {
            try
            {
                var result = await _context.ADM_PROVEEDORES.DefaultIfEmpty().Where(e => e.CODIGO_PROVEEDOR == id).FirstOrDefaultAsync();

                return (ADM_PROVEEDORES)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

 public async Task<ADM_PROVEEDORES> GetByNombre(int codigoEmpresa, string nombreProveedor)
        {
            try
            {
                var result = await _context.ADM_PROVEEDORES.DefaultIfEmpty().Where(e => e.CODIGO_EMPRESA == codigoEmpresa && e.NOMBRE_PROVEEDOR == nombreProveedor).FirstOrDefaultAsync();

                return (ADM_PROVEEDORES)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<ADM_PROVEEDORES> GetByTipo(int idTipoProveedor)
        {
            try
            {
                var result = await _context.ADM_PROVEEDORES.DefaultIfEmpty().Where(e => e.TIPO_PROVEEDOR_ID == idTipoProveedor).FirstOrDefaultAsync();

                return (ADM_PROVEEDORES)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
    
        public async Task<List<ADM_PROVEEDORES>> GetByAll()
        {
            try
            {
               

                var  result = await _context.ADM_PROVEEDORES.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


        public async Task<ResultDto<List<ADM_PROVEEDORES>>> GetByAll(AdmProveedoresFilterDto filter)
        {

            ResultDto<List<ADM_PROVEEDORES>> result = new ResultDto<List<ADM_PROVEEDORES>>(null);

            if (filter.PageNumber == 0) filter.PageNumber = 1;
            if (filter.PageSize == 0) filter.PageSize = 100;
            if (filter.PageSize >100) filter.PageSize = 100;

            if (string.IsNullOrEmpty(filter.SearchText))
            {
                filter.SearchText = "";
            }

             var totalRegistros = 0;
             var totalPage = 0;
            List<ADM_PROVEEDORES> pageData = new List<ADM_PROVEEDORES>(); 

            try
            {

           
               if ( filter.SearchText.Length==0)
                {
                    totalRegistros = _context.ADM_PROVEEDORES
                        
                        .Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                   
                    
                    pageData = await _context.ADM_PROVEEDORES.DefaultIfEmpty()
                        
                        .OrderBy(x => x.NOMBRE_PROVEEDOR)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
                if ( filter.SearchText.Length>0)
                {
                    totalRegistros = _context.ADM_PROVEEDORES
                        .Where(x => x.NOMBRE_PROVEEDOR.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()) || x.RIF.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()) )
                        .Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    
                    pageData = await _context.ADM_PROVEEDORES.DefaultIfEmpty()
                        .Where(x => x.NOMBRE_PROVEEDOR.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()) || x.RIF.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()) )
                           .OrderBy(x => x.NOMBRE_PROVEEDOR)
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



     
      



        public async Task<ResultDto<ADM_PROVEEDORES>> Add(ADM_PROVEEDORES entity)
        {
            
            ResultDto<ADM_PROVEEDORES> result = new ResultDto<ADM_PROVEEDORES>(null);
            try
            {



                await _context.ADM_PROVEEDORES.AddAsync(entity);
                await _context.SaveChangesAsync();
                await _distributedCache.RemoveAsync(ProveedoresCacheKey);

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

        public async Task<ResultDto<ADM_PROVEEDORES>> Update(ADM_PROVEEDORES entity)
        {
            ResultDto<ADM_PROVEEDORES> result = new ResultDto<ADM_PROVEEDORES>(null);

            try
            {
                ADM_PROVEEDORES entityUpdate = await GetByCodigo(entity.CODIGO_PROVEEDOR);
                if (entityUpdate != null)
                {


                    _context.ADM_PROVEEDORES.Update(entity);
                    await _context.SaveChangesAsync();
                      await _distributedCache.RemoveAsync(ProveedoresCacheKey);
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

        public async Task<string> Delete(int id)
        {

            try
            {
                ADM_PROVEEDORES entity = await GetByCodigo(id);
                if (entity != null)
                {
                    _context.ADM_PROVEEDORES.Remove(entity);
                    await _context.SaveChangesAsync();
                    await _distributedCache.RemoveAsync(ProveedoresCacheKey);
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
                var last = await _context.ADM_PROVEEDORES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_PROVEEDOR)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_PROVEEDOR + 1;
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

