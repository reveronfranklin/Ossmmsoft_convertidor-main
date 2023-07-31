using System;
using System;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class PreDescriptivaRepository: IPreDescriptivaRepository
    {
		
        private readonly DataContextPre _context;

        public PreDescriptivaRepository(DataContextPre context)
        {
            _context = context;
        }
        public async Task<PRE_DESCRIPTIVAS> GetByCodigoDescriptiva(int descripcionId)
        {
            try
            {
                var result = await _context.PRE_DESCRIPTIVAS.DefaultIfEmpty().Where(e => e.DESCRIPCION_ID == descripcionId).FirstOrDefaultAsync();
        
                return (PRE_DESCRIPTIVAS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<PRE_DESCRIPTIVAS> GetByCodigoDescriptivaTexto(string codigo)
        {
            try
            {
                var result = await _context.PRE_DESCRIPTIVAS.DefaultIfEmpty().Where(e => e.CODIGO == codigo).FirstOrDefaultAsync();

                return (PRE_DESCRIPTIVAS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<PRE_DESCRIPTIVAS>> GetByTitulo(int tituloId)
        {
            try
            {
                var result = await _context.PRE_DESCRIPTIVAS.DefaultIfEmpty().Where(e => e.TITULO_ID == tituloId).ToListAsync();

                return (List<PRE_DESCRIPTIVAS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<PRE_DESCRIPTIVAS>> GetAll()
        {
            try
            {
                var result = await _context.PRE_DESCRIPTIVAS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


        public async Task<PRE_DESCRIPTIVAS> GetByCodigo(int descripcionId)
        {
            try
            {

                var result = await _context.PRE_DESCRIPTIVAS.DefaultIfEmpty()
                    .Where(x => x.DESCRIPCION_ID == descripcionId)
                    .FirstOrDefaultAsync();
                return (PRE_DESCRIPTIVAS)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }



        }

      



        public async Task<ResultDto<PRE_DESCRIPTIVAS>> Add(PRE_DESCRIPTIVAS entity)
        {
            ResultDto<PRE_DESCRIPTIVAS> result = new ResultDto<PRE_DESCRIPTIVAS>(null);
            try
            {



                await _context.PRE_DESCRIPTIVAS.AddAsync(entity);
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

        public async Task<ResultDto<PRE_DESCRIPTIVAS>> Update(PRE_DESCRIPTIVAS entity)
        {
            ResultDto<PRE_DESCRIPTIVAS> result = new ResultDto<PRE_DESCRIPTIVAS>(null);

            try
            {
                PRE_DESCRIPTIVAS entityUpdate = await GetByCodigo(entity.DESCRIPCION_ID);
                if (entityUpdate != null)
                {


                    _context.PRE_DESCRIPTIVAS.Update(entity);
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

        public async Task<string> Delete(int descripcionId)
        {

            try
            {
                PRE_DESCRIPTIVAS entity = await GetByCodigo(descripcionId);
                if (entity != null)
                {
                    _context.PRE_DESCRIPTIVAS.Remove(entity);
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
                var last = await _context.PRE_DESCRIPTIVAS.DefaultIfEmpty()
                    .OrderByDescending(x => x.DESCRIPCION_ID)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.DESCRIPCION_ID + 1;
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

