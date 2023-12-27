using System;
using System;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
	public class AdmProveedoresRepository: IAdmProveedoresRepository
    {
		
        private readonly DataContextAdm _context;

        public AdmProveedoresRepository(DataContextAdm context)
        {
            _context = context;
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

    
        public async Task<List<ADM_PROVEEDORES>> GetByAll()
        {
            try
            {
                var result = await _context.ADM_PROVEEDORES.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


     
      



        public async Task<ResultDto<ADM_PROVEEDORES>> Add(ADM_PROVEEDORES entity)
        {
            ResultDto<ADM_PROVEEDORES> result = new ResultDto<ADM_PROVEEDORES>(null);
            try
            {



                await _context.ADM_PROVEEDORES.AddAsync(entity);
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

        public async Task<ResultDto<ADM_PROVEEDORES>> Update(ADM_PROVEEDORES entity)
        {
            ResultDto<ADM_PROVEEDORES> result = new ResultDto<ADM_PROVEEDORES>(null);

            try
            {
                ADM_PROVEEDORES entityUpdate = await GetByCodigo(entity.CODIGO_PROVEEDOR);
                if (entityUpdate != null)
                {


                    _context.ADM_PROVEEDORES.Update(entity);
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
                ADM_PROVEEDORES entity = await GetByCodigo(id);
                if (entity != null)
                {
                    _context.ADM_PROVEEDORES.Remove(entity);
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

