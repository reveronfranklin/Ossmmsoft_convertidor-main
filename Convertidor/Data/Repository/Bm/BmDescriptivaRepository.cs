using System;
using System;
using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Bm
{
    public class BmDescriptivaRepository: IBmDescriptivaRepository
    {
		
        private readonly DataContextBm _context;

        public BmDescriptivaRepository(DataContextBm context)
        {
            _context = context;
        }
        public async Task<BM_DESCRIPTIVAS> GetByCodigoDescriptiva(int descripcionId)
        {
            try
            {
                var result = await _context.BM_DESCRIPTIVAS.DefaultIfEmpty().Where(e => e.DESCRIPCION_ID == descripcionId).FirstOrDefaultAsync();
        
                return (BM_DESCRIPTIVAS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<BM_DESCRIPTIVAS> GetByCodigoDescriptivaTexto(string codigo)
        {
            try
            {
                var result = await _context.BM_DESCRIPTIVAS.DefaultIfEmpty().Where(e => e.CODIGO == codigo).FirstOrDefaultAsync();

                return (BM_DESCRIPTIVAS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<BM_DESCRIPTIVAS>> GetByTituloId(int tituloId)
        {
            try
            {
                var result = await _context.BM_DESCRIPTIVAS.DefaultIfEmpty().Where(e => e.TITULO_ID == tituloId).ToListAsync();

                return (List<BM_DESCRIPTIVAS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<bool> GetByIdAndTitulo(int tituloId, int id)
        {
            try
            {
                var descriptiva = await _context.BM_DESCRIPTIVAS.DefaultIfEmpty()
                    .Where(e => e.TITULO_ID == tituloId && e.DESCRIPCION_ID == id).FirstOrDefaultAsync();
                var result = false;
                if (descriptiva != null)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return false;
            }

        }
        public async Task<List<BM_DESCRIPTIVAS>> GetAll()
        {
            try
            {
                var result = await _context.BM_DESCRIPTIVAS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


        public async Task<BM_DESCRIPTIVAS> GetByCodigo(int descripcionId)
        {
            try
            {

                var result = await _context.BM_DESCRIPTIVAS.DefaultIfEmpty()
                    .Where(x => x.DESCRIPCION_ID == descripcionId)
                    .FirstOrDefaultAsync();
                return (BM_DESCRIPTIVAS)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }



        }


        public async Task<List<BM_DESCRIPTIVAS>> GetByFKID(int descripcionIdFk)
        {
            try
            {

                var result = await _context.BM_DESCRIPTIVAS.DefaultIfEmpty()
                    .Where(x => x.DESCRIPCION_FK_ID== descripcionIdFk)
                    .ToListAsync();
                return (List<BM_DESCRIPTIVAS>)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }



        }



        public async Task<ResultDto<BM_DESCRIPTIVAS>> Add(BM_DESCRIPTIVAS entity)
        {
            ResultDto<BM_DESCRIPTIVAS> result = new ResultDto<BM_DESCRIPTIVAS>(null);
            try
            {



                await _context.BM_DESCRIPTIVAS.AddAsync(entity);
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

        public async Task<ResultDto<BM_DESCRIPTIVAS>> Update(BM_DESCRIPTIVAS entity)
        {
            ResultDto<BM_DESCRIPTIVAS> result = new ResultDto<BM_DESCRIPTIVAS>(null);

            try
            {
                BM_DESCRIPTIVAS entityUpdate = await GetByCodigo(entity.DESCRIPCION_ID);
                if (entityUpdate != null)
                {


                    _context.BM_DESCRIPTIVAS.Update(entity);
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

        public async Task<string> Delete(int descripcionId)
        {

            try
            {
                BM_DESCRIPTIVAS entity = await GetByCodigo(descripcionId);
                if (entity != null)
                {
                    _context.BM_DESCRIPTIVAS.Remove(entity);
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
                var last = await _context.BM_DESCRIPTIVAS.DefaultIfEmpty()
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

