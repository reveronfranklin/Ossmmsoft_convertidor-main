using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
	public class AdmContactosProveedorRepository: IAdmContactosProveedorRepository
    {
		
        private readonly DataContextAdm _context;

        public AdmContactosProveedorRepository(DataContextAdm context)
        {
            _context = context;
        }
      
        public async Task<ADM_CONTACTO_PROVEEDOR> GetByCodigo(int id)
        {
            try
            {
                var result = await _context.ADM_CONTACTO_PROVEEDOR.DefaultIfEmpty()
                    .Where(e => e.CODIGO_CONTACTO_PROVEEDOR == id).FirstOrDefaultAsync();

                return (ADM_CONTACTO_PROVEEDOR)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

    
        public async Task<List<ADM_CONTACTO_PROVEEDOR>> GetByProveedor(int codigoProveedor)
        {
            try
            {
                var result = await _context.ADM_CONTACTO_PROVEEDOR.DefaultIfEmpty().Where(e => e.CODIGO_PROVEEDOR == codigoProveedor).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


     
      



        public async Task<ResultDto<ADM_CONTACTO_PROVEEDOR>> Add(ADM_CONTACTO_PROVEEDOR entity)
        {
            ResultDto<ADM_CONTACTO_PROVEEDOR> result = new ResultDto<ADM_CONTACTO_PROVEEDOR>(null);
            try
            {



                await _context.ADM_CONTACTO_PROVEEDOR.AddAsync(entity);
                _context.SaveChanges();


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

        public async Task<ResultDto<ADM_CONTACTO_PROVEEDOR>> Update(ADM_CONTACTO_PROVEEDOR entity)
        {
            ResultDto<ADM_CONTACTO_PROVEEDOR> result = new ResultDto<ADM_CONTACTO_PROVEEDOR>(null);

            try
            {
                ADM_CONTACTO_PROVEEDOR entityUpdate = await GetByCodigo(entity.CODIGO_CONTACTO_PROVEEDOR);
                if (entityUpdate != null)
                {


                    _context.ADM_CONTACTO_PROVEEDOR.Update(entity);
                    _context.SaveChanges();
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
                ADM_CONTACTO_PROVEEDOR entity = await GetByCodigo(id);
                if (entity != null)
                {
                    _context.ADM_CONTACTO_PROVEEDOR.Remove(entity);
                    _context.SaveChanges();
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
                var last = await _context.ADM_CONTACTO_PROVEEDOR.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_CONTACTO_PROVEEDOR)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_CONTACTO_PROVEEDOR + 1;
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

