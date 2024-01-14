using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Entities.Sis;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Sis
{
	public class OssFuncionRepository: IOssFuncionRepository
    {
		
        private readonly DataContextSis _context;

        public OssFuncionRepository(DataContextSis context)
        {
            _context = context;
        }
      
        public async Task<OssFuncion> GetByCodigo(int id)
        {
            try
            {
                var result = await _context.OssFunciones.DefaultIfEmpty().Where(e => e.Id == id).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        public async Task<OssFuncion> GetByFuncion(string funcion)
        {
            try
            {
                var result = await _context.OssFunciones.DefaultIfEmpty().Where(e => e.Funcion == funcion).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
 
        public async Task<List<OssFuncion>> GetByAll()
        {
            try
            {
                var result = await _context.OssFunciones.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        

        public async Task<ResultDto<OssFuncion>> Add(OssFuncion entity)
        {
            ResultDto<OssFuncion> result = new ResultDto<OssFuncion>(null);
            try
            {



                await _context.OssFunciones.AddAsync(entity);
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

        public async Task<ResultDto<OssFuncion>> Update(OssFuncion entity)
        {
            ResultDto<OssFuncion> result = new ResultDto<OssFuncion>(null);

            try
            {
                OssFuncion entityUpdate = await GetByCodigo(entity.Id);
                if (entityUpdate != null)
                {


                    _context.OssFunciones.Update(entity);
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
                OssFuncion entity = await GetByCodigo(id);
                if (entity != null)
                {
                    _context.OssFunciones.Remove(entity);
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
                var last = await _context.OssFunciones.DefaultIfEmpty()
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.Id + 1;
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

