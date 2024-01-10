using System;
using System;
using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Dtos;
using Convertidor.Dtos.Bm;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;

namespace Convertidor.Data.Repository.Bm
{
    public class BmDetalleBienesRepository: IBmDetalleBienesRepository
    {
		
        private readonly DataContextBm _context;

        public BmDetalleBienesRepository(DataContextBm context)
        {
            _context = context;
        }
        public async Task<BM_DETALLE_BIENES> GetByCodigoDetalleBien(int codigoDetalleBien)
        {
            try
            {
                var result = await _context.BM_DETALLE_BIENES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_DETALLE_BIEN == codigoDetalleBien).FirstOrDefaultAsync();
        
                return (BM_DETALLE_BIENES)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


        public async Task<BM_DETALLE_BIENES> GetByCodigoBien(int codigoBien)
        {
            try
            {
                var result = await _context.BM_DETALLE_BIENES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_BIEN == codigoBien).FirstOrDefaultAsync();

                return (BM_DETALLE_BIENES)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
    
        public async Task<List<BM_DETALLE_BIENES>> GetAll()
        {
            try
            {
                var result = await _context.BM_DETALLE_BIENES.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


        public async Task<ResultDto<BM_DETALLE_BIENES>> Add(BM_DETALLE_BIENES entity)
        {
            ResultDto<BM_DETALLE_BIENES> result = new ResultDto<BM_DETALLE_BIENES>(null);
            try
            {



                await _context.BM_DETALLE_BIENES.AddAsync(entity);
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

        public async Task<ResultDto<BM_DETALLE_BIENES>> Update(BM_DETALLE_BIENES entity)
        {
            ResultDto<BM_DETALLE_BIENES> result = new ResultDto<BM_DETALLE_BIENES>(null);

            try
            {
                BM_DETALLE_BIENES entityUpdate = await GetByCodigoDetalleBien(entity.CODIGO_DETALLE_BIEN);
                if (entityUpdate != null)
                {


                    _context.BM_DETALLE_BIENES.Update(entity);
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

        public async Task<string> Delete(int codigoDetalleBien)
        {

            try
            {
                BM_DETALLE_BIENES entity = await GetByCodigoDetalleBien(codigoDetalleBien);
                if (entity != null)
                {
                    _context.BM_DETALLE_BIENES.Remove(entity);
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
                var last = await _context.BM_DETALLE_BIENES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_DETALLE_BIEN)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_DETALLE_BIEN + 1;
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


