﻿using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class TmpDetalleLibroRepository : ITmpDetalleLibroRepository
    {
        private readonly DataContextCnt _context;

        public TmpDetalleLibroRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<TMP_DETALLE_LIBRO>> GetAll()
        {
            try
            {

                var result = await _context.TMP_DETALLE_LIBRO.DefaultIfEmpty().ToListAsync();
                return result;

            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;

            }

        }

        public async Task<TMP_DETALLE_LIBRO> GetByCodigo(int codigoDetalleLibro)
        {
            try
            {
                var result = await _context.TMP_DETALLE_LIBRO.DefaultIfEmpty().Where(x => x.CODIGO_DETALLE_LIBRO == codigoDetalleLibro).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<TMP_DETALLE_LIBRO>> Add(TMP_DETALLE_LIBRO entity)
        {

            ResultDto<TMP_DETALLE_LIBRO> result = new ResultDto<TMP_DETALLE_LIBRO>(null);
            try
            {
                await _context.TMP_DETALLE_LIBRO.AddAsync(entity);
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

        public async Task<ResultDto<TMP_DETALLE_LIBRO>> Update(TMP_DETALLE_LIBRO entity)
        {
            ResultDto<TMP_DETALLE_LIBRO> result = new ResultDto<TMP_DETALLE_LIBRO>(null);

            try
            {
                TMP_DETALLE_LIBRO entityUpdate = await GetByCodigo(entity.CODIGO_DETALLE_LIBRO);
                if (entityUpdate != null)
                {
                    _context.TMP_DETALLE_LIBRO.Update(entity);
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

        public async Task<string> Delete(int codigoDetalleLibro)
        {
            try
            {
                TMP_DETALLE_LIBRO entity = await GetByCodigo(codigoDetalleLibro);
                if (entity != null)
                {
                    _context.TMP_DETALLE_LIBRO.Remove(entity);
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
                var last = await _context.TMP_DETALLE_LIBRO.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_DETALLE_LIBRO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_DETALLE_LIBRO + 1;
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
