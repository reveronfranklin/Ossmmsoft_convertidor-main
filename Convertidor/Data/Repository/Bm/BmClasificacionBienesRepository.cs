using System;
using System;
using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Bm;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;

namespace Convertidor.Data.Repository.Bm
{
    public class BmClasificacionBienesRepository: IBmClasificacionBienesRepository
    {
		
        private readonly DataContextBm _context;

        public BmClasificacionBienesRepository(DataContextBm context)
        {
            _context = context;
        }
        public async Task<BM_CLASIFICACION_BIENES> GetByCodigoClasificacionBien(int codigoClasificacionBien)
        {
            try
            {
                var result = await _context.BM_CLASIFICACION_BIENES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_CLASIFICACION_BIEN == codigoClasificacionBien).FirstOrDefaultAsync();
        
                return (BM_CLASIFICACION_BIENES)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<BM_CLASIFICACION_BIENES> GetByCodigoGrupo(string codigoGrupo)
        {
            try
            {
                var result = await _context.BM_CLASIFICACION_BIENES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_GRUPO == codigoGrupo).FirstOrDefaultAsync();

                return (BM_CLASIFICACION_BIENES)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<BM_CLASIFICACION_BIENES>> GetAll()
        {
            try
            {
                var result = await _context.BM_CLASIFICACION_BIENES.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<BM_CLASIFICACION_BIENES> GetByCodigoNivel1(string codigo)
        {
            try
            {
                var result = await _context.BM_CLASIFICACION_BIENES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_NIVEL1 == codigo)
                    .FirstOrDefaultAsync();

                return (BM_CLASIFICACION_BIENES)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<BM_CLASIFICACION_BIENES> GetByCodigoNivel2(string codigo)
        {
            try
            {
                var result = await _context.BM_CLASIFICACION_BIENES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_NIVEL2 == codigo)
                    .FirstOrDefaultAsync();

                return (BM_CLASIFICACION_BIENES)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


        public async Task<ResultDto<BM_CLASIFICACION_BIENES>> Add(BM_CLASIFICACION_BIENES entity)
        {
            ResultDto<BM_CLASIFICACION_BIENES> result = new ResultDto<BM_CLASIFICACION_BIENES>(null);
            try
            {



                await _context.BM_CLASIFICACION_BIENES.AddAsync(entity);
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

        public async Task<ResultDto<BM_CLASIFICACION_BIENES>> Update(BM_CLASIFICACION_BIENES entity)
        {
            ResultDto<BM_CLASIFICACION_BIENES> result = new ResultDto<BM_CLASIFICACION_BIENES>(null);

            try
            {
                BM_CLASIFICACION_BIENES entityUpdate = await GetByCodigoClasificacionBien(entity.CODIGO_CLASIFICACION_BIEN);
                if (entityUpdate != null)
                {


                    _context.BM_CLASIFICACION_BIENES.Update(entity);
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

        public async Task<string> Delete(int codigoClasificacionBien)
        {

            try
            {
                BM_CLASIFICACION_BIENES entity = await GetByCodigoClasificacionBien(codigoClasificacionBien);
                if (entity != null)
                {
                    _context.BM_CLASIFICACION_BIENES.Remove(entity);
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
                var last = await _context.BM_CLASIFICACION_BIENES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_CLASIFICACION_BIEN)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_CLASIFICACION_BIEN + 1;
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


