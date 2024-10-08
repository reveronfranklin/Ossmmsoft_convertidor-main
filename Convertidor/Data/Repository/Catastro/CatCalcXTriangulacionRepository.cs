﻿using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
    public class CatCalcXTriangulacionRepository : ICatCalcXTriangulacionRepository
    {
        private readonly DataContextCat _context;

        public CatCalcXTriangulacionRepository(DataContextCat context)
        {
            _context = context;
        }

        public async Task<List<CAT_CALC_X_TRIANGULACION>> GetAll()
        {
            try
            {
                var result = await _context.CAT_CALC_X_TRIANGULACION.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<CAT_CALC_X_TRIANGULACION> GetByCodigo(int codigoTriangulacion)
        {
            try
            {

                var result = await _context.CAT_CALC_X_TRIANGULACION.DefaultIfEmpty()
                    .Where(x => x.CODIGO_TRIANGULACION == codigoTriangulacion)
                    .FirstOrDefaultAsync();
                return (CAT_CALC_X_TRIANGULACION)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }

        }

        public async Task<ResultDto<CAT_CALC_X_TRIANGULACION>> Add(CAT_CALC_X_TRIANGULACION entity)
        {
            ResultDto<CAT_CALC_X_TRIANGULACION> result = new ResultDto<CAT_CALC_X_TRIANGULACION>(null);
            try
            {



                await _context.CAT_CALC_X_TRIANGULACION.AddAsync(entity);
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

        public async Task<ResultDto<CAT_CALC_X_TRIANGULACION>> Update(CAT_CALC_X_TRIANGULACION entity)
        {
            ResultDto<CAT_CALC_X_TRIANGULACION> result = new ResultDto<CAT_CALC_X_TRIANGULACION>(null);

            try
            {
                CAT_CALC_X_TRIANGULACION entityUpdate = await GetByCodigo(entity.CODIGO_TRIANGULACION);
                if (entityUpdate != null)
                {


                    _context.CAT_CALC_X_TRIANGULACION.Update(entity);
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


        public async Task<string> Delete(int codigoTriangulacion)
        {

            try
            {
                CAT_CALC_X_TRIANGULACION entity = await GetByCodigo(codigoTriangulacion);
                if (entity != null)
                {
                    _context.CAT_CALC_X_TRIANGULACION.Remove(entity);
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
                var last = await _context.CAT_CALC_X_TRIANGULACION.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_TRIANGULACION)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_TRIANGULACION + 1;
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
