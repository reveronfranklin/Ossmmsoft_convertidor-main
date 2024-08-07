﻿using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class PreDetalleCompromisosRepository: IPreDetalleCompromisosRepository
    {
		
        private readonly DataContextPre _context;

        public PreDetalleCompromisosRepository(DataContextPre context)
        {
            _context = context;
        }
      
        public async Task<PRE_DETALLE_COMPROMISOS> GetByCodigo(int codigoDetalleCompromiso)
        {
            try
            {
                var result = await _context.PRE_DETALLE_COMPROMISOS.DefaultIfEmpty().Where(e => e.CODIGO_DETALLE_COMPROMISO == codigoDetalleCompromiso).FirstOrDefaultAsync();

                return (PRE_DETALLE_COMPROMISOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

    
        public async Task<List<PRE_DETALLE_COMPROMISOS>> GetAll()
        {
            try
            {
                var result = await _context.PRE_DETALLE_COMPROMISOS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

       

        public async Task<ResultDto<PRE_DETALLE_COMPROMISOS>> Add(PRE_DETALLE_COMPROMISOS entity)
        {
            ResultDto<PRE_DETALLE_COMPROMISOS> result = new ResultDto<PRE_DETALLE_COMPROMISOS>(null);
            try
            {



                await _context.PRE_DETALLE_COMPROMISOS.AddAsync(entity);
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

        public async Task<ResultDto<PRE_DETALLE_COMPROMISOS>> Update(PRE_DETALLE_COMPROMISOS entity)
        {
            ResultDto<PRE_DETALLE_COMPROMISOS> result = new ResultDto<PRE_DETALLE_COMPROMISOS>(null);

            try
            {
                PRE_DETALLE_COMPROMISOS entityUpdate = await GetByCodigo(entity.CODIGO_DETALLE_COMPROMISO);
                if (entityUpdate != null)
                {


                    _context.PRE_DETALLE_COMPROMISOS.Update(entity);
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

        public async Task<string> Delete(int codigoDetalleCompromiso)
        {

            try
            {
                PRE_DETALLE_COMPROMISOS entity = await GetByCodigo(codigoDetalleCompromiso);
                if (entity != null)
                {
                    _context.PRE_DETALLE_COMPROMISOS.Remove(entity);
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
                var last = await _context.PRE_DETALLE_COMPROMISOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_DETALLE_COMPROMISO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_DETALLE_COMPROMISO + 1;
                }

                return (int)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }



        }

        public async Task<List<PRE_DETALLE_COMPROMISOS>> GetByCodigoCompromiso(int codigoCompromiso)
        {
            try
            {
                var result = await _context.PRE_DETALLE_COMPROMISOS.DefaultIfEmpty().Where(e => e.CODIGO_COMPROMISO == codigoCompromiso).ToListAsync();

                return (List<PRE_DETALLE_COMPROMISOS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }


    }
}

