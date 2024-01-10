using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
	public class AdmComunicacionProveedorRepository: IAdmComunicacionProveedorRepository
    {
		
        private readonly DataContextAdm _context;

        public AdmComunicacionProveedorRepository(DataContextAdm context)
        {
            _context = context;
        }
      
        public async Task<ADM_COM_PROVEEDOR> GetByCodigo(int id)
        {
            try
            {
                var result = await _context.ADM_COM_PROVEEDOR.DefaultIfEmpty().Where(e => e.CODIGO_COM_PROVEEDOR == id).FirstOrDefaultAsync();

                return (ADM_COM_PROVEEDOR)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

    
        public async Task<List<ADM_COM_PROVEEDOR>> GetByProveedor(int codigoProveedor)
        {
            try
            {
                var result = await _context.ADM_COM_PROVEEDOR.DefaultIfEmpty().Where(e => e.CODIGO_PROVEEDOR == codigoProveedor).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


     
      



        public async Task<ResultDto<ADM_COM_PROVEEDOR>> Add(ADM_COM_PROVEEDOR entity)
        {
            ResultDto<ADM_COM_PROVEEDOR> result = new ResultDto<ADM_COM_PROVEEDOR>(null);
            try
            {



                await _context.ADM_COM_PROVEEDOR.AddAsync(entity);
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

        public async Task<ResultDto<ADM_COM_PROVEEDOR>> Update(ADM_COM_PROVEEDOR entity)
        {
            ResultDto<ADM_COM_PROVEEDOR> result = new ResultDto<ADM_COM_PROVEEDOR>(null);

            try
            {
                ADM_COM_PROVEEDOR entityUpdate = await GetByCodigo(entity.CODIGO_COM_PROVEEDOR);
                if (entityUpdate != null)
                {


                    _context.ADM_COM_PROVEEDOR.Update(entity);
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
                ADM_COM_PROVEEDOR entity = await GetByCodigo(id);
                if (entity != null)
                {
                    _context.ADM_COM_PROVEEDOR.Remove(entity);
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
                var last = await _context.ADM_COM_PROVEEDOR.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_COM_PROVEEDOR)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_COM_PROVEEDOR + 1;
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

