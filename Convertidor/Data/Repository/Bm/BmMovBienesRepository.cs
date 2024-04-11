using System;
using System;
using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Bm
{
    public class BmMovBienesRepository: IBmMovBienesRepository
    {
		
        private readonly DataContextBm _context;

        public BmMovBienesRepository(DataContextBm context)
        {
            _context = context;
        }
        public async Task<BM_MOV_BIENES> GetByCodigoMovBien(int codigoMovBien)
        {
            try
            {
                var result = await _context.BM_MOV_BIENES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_MOV_BIEN == codigoMovBien).FirstOrDefaultAsync();
        
                return (BM_MOV_BIENES)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<BM_MOV_BIENES> GetByCodigoBien(int codigoBien)
        {
            try
            {
                var result = await _context.BM_MOV_BIENES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_BIEN == codigoBien).FirstOrDefaultAsync();

                return (BM_MOV_BIENES)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<DateTime> GetByCodigoBienFecha(int codigoBien,DateTime hasta)
        {
            DateTime result= DateTime.Now;
            try
            {
                var movimiento = await _context.BM_MOV_BIENES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_BIEN == codigoBien && e.FECHA_MOVIMIENTO <= hasta).OrderByDescending(x=>x.FECHA_MOVIMIENTO).FirstOrDefaultAsync();
                if (movimiento != null)
                {
                    result = movimiento.FECHA_MOVIMIENTO;
                }
              
                return (DateTime)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return result;
            }

        }
        public async Task<bool> CodigoBienActivo(int codigoBien,DateTime hasta)
        {
            bool result= false;
            try
            {
                var movimiento = await _context.BM_MOV_BIENES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_BIEN == codigoBien && e.FECHA_MOVIMIENTO <= hasta && e.TIPO_MOVIMIENTO=="D").OrderByDescending(x=>x.FECHA_MOVIMIENTO).FirstOrDefaultAsync();
                if (movimiento != null)
                {
                    result = true;
                }
              
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return result;
            }

        }
       
        public async Task<List<BM_MOV_BIENES>> GetAll()
        {
            try
            {
                var result = await _context.BM_MOV_BIENES.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


        public async Task<ResultDto<BM_MOV_BIENES>> Add(BM_MOV_BIENES entity)
        {
            ResultDto<BM_MOV_BIENES> result = new ResultDto<BM_MOV_BIENES>(null);
            try
            {



                await _context.BM_MOV_BIENES.AddAsync(entity);
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

        public async Task<ResultDto<BM_MOV_BIENES>> Update(BM_MOV_BIENES entity)
        {
            ResultDto<BM_MOV_BIENES> result = new ResultDto<BM_MOV_BIENES>(null);

            try
            {
                BM_MOV_BIENES entityUpdate = await GetByCodigoMovBien(entity.CODIGO_MOV_BIEN);
                if (entityUpdate != null)
                {


                    _context.BM_MOV_BIENES.Update(entity);
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

        public async Task<string> Delete(int codigoMovBien)
        {

            try
            {
                BM_MOV_BIENES entity = await GetByCodigoMovBien(codigoMovBien);
                if (entity != null)
                {
                    _context.BM_MOV_BIENES.Remove(entity);
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
                var last = await _context.BM_MOV_BIENES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_MOV_BIEN)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_MOV_BIEN + 1;
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

