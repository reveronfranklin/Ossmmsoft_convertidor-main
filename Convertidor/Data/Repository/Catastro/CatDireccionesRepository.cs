﻿using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
    public class CatDireccionesRepository : ICatDireccionesRepository
    {
        private readonly DataContextCat _context;

        public CatDireccionesRepository(DataContextCat context)
        {
            _context = context;
        }

        public async Task<List<CAT_DIRECCIONES>> GetAll()
        {
            try
            {
                var result = await _context.CAT_DIRECCIONES.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<CAT_DIRECCIONES> GetByCodigo(int codigoDireccion)
        {
            try
            {

                var result = await _context.CAT_DIRECCIONES.DefaultIfEmpty()
                    .Where(x => x.CODIGO_DIRECCION == codigoDireccion)
                    .FirstOrDefaultAsync();
                return (CAT_DIRECCIONES)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }

        }

        public async Task<ResultDto<CAT_DIRECCIONES>> Add(CAT_DIRECCIONES entity)
        {
            ResultDto<CAT_DIRECCIONES> result = new ResultDto<CAT_DIRECCIONES>(null);
            try
            {



                await _context.CAT_DIRECCIONES.AddAsync(entity);
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

        public async Task<ResultDto<CAT_DIRECCIONES>> Update(CAT_DIRECCIONES entity)
        {
            ResultDto<CAT_DIRECCIONES> result = new ResultDto<CAT_DIRECCIONES>(null);

            try
            {
                CAT_DIRECCIONES entityUpdate = await GetByCodigo(entity.CODIGO_DIRECCION);
                if (entityUpdate != null)
                {


                    _context.CAT_DIRECCIONES.Update(entity);
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

        public async Task<string> Delete(int codigoDireccion)
        {

            try
            {
                CAT_DIRECCIONES entity = await GetByCodigo(codigoDireccion);
                if (entity != null)
                {
                    _context.CAT_DIRECCIONES.Remove(entity);
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
                var last = await _context.CAT_DIRECCIONES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_DIRECCION)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_DIRECCION + 1;
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
