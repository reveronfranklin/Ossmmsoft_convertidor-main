using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
	public class AdmDireccionProveedorRepository: IAdmDireccionProveedorRepository
    {
		
        private readonly DataContextAdm _context;

        public AdmDireccionProveedorRepository(DataContextAdm context)
        {
            _context = context;
        }
      
        public async Task<ADM_DIR_PROVEEDOR> GetByCodigo(int id)
        {
            try
            {
                var result = await _context.ADM_DIR_PROVEEDOR.DefaultIfEmpty()
                    .Where(e => e.CODIGO_DIR_PROVEEDOR == id).FirstOrDefaultAsync();

                return (ADM_DIR_PROVEEDOR)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

    
        public async Task<List<ADM_DIR_PROVEEDOR>> GetByProveedor(int codigoProveedor)
        {
            try
            {
                var result = await _context.ADM_DIR_PROVEEDOR.DefaultIfEmpty().Where(e => e.CODIGO_PROVEEDOR == codigoProveedor).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


     
      



        public async Task<ResultDto<ADM_DIR_PROVEEDOR>> Add(ADM_DIR_PROVEEDOR entity)
        {
            ResultDto<ADM_DIR_PROVEEDOR> result = new ResultDto<ADM_DIR_PROVEEDOR>(null);
            try
            {



                await _context.ADM_DIR_PROVEEDOR.AddAsync(entity);
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

        public async Task<ResultDto<ADM_DIR_PROVEEDOR>> Update(ADM_DIR_PROVEEDOR entity)
        {
            ResultDto<ADM_DIR_PROVEEDOR> result = new ResultDto<ADM_DIR_PROVEEDOR>(null);

            try
            {
                ADM_DIR_PROVEEDOR entityUpdate = await GetByCodigo(entity.CODIGO_DIR_PROVEEDOR);
                if (entityUpdate != null)
                {


                    _context.ADM_DIR_PROVEEDOR.Update(entity);
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

        public async Task<string> Delete(int id)
        {

            try
            {
                ADM_DIR_PROVEEDOR entity = await GetByCodigo(id);
                if (entity != null)
                {
                    _context.ADM_DIR_PROVEEDOR.Remove(entity);
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
                var last = await _context.ADM_DIR_PROVEEDOR.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_DIR_PROVEEDOR)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_DIR_PROVEEDOR + 1;
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

