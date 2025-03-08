using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Bm
{
    public class BmPlacasCuarentenaRepository: IBmPlacasCuarentenaRepository
    {
		
        private readonly DataContextBm _context;

        public BmPlacasCuarentenaRepository(DataContextBm context)
        {
            _context = context;
        }
        public async Task<BM_PLACAS_CUARENTENA> GetByNumeroPlaca(string numeroPlaca)
        {
            try
            {
                var result = await _context.BM_PLACAS_CUARENTENA.DefaultIfEmpty()
                    .Where(e => e.NUMERO_PLACA == numeroPlaca).FirstOrDefaultAsync();
        
                return (BM_PLACAS_CUARENTENA)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<BM_PLACAS_CUARENTENA> GetByCodigo(int codigoPlacaCuarentena)
        {
            try
            {
                var result = await _context.BM_PLACAS_CUARENTENA.DefaultIfEmpty()
                    .Where(e => e.CODIGO_PLACA_CUARENTENA == codigoPlacaCuarentena).FirstOrDefaultAsync();

                return (BM_PLACAS_CUARENTENA)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
       
        public async Task<List<BM_PLACAS_CUARENTENA>> GetAll()
        {
            try
            {
                var result = await _context.BM_PLACAS_CUARENTENA.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


        public async Task<ResultDto<BM_PLACAS_CUARENTENA>> Add(BM_PLACAS_CUARENTENA entity)
        {
            ResultDto<BM_PLACAS_CUARENTENA> result = new ResultDto<BM_PLACAS_CUARENTENA>(null);
            try
            {



                await _context.BM_PLACAS_CUARENTENA.AddAsync(entity);
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

        public async Task<ResultDto<BM_PLACAS_CUARENTENA>> Update(BM_PLACAS_CUARENTENA entity)
        {
            ResultDto<BM_PLACAS_CUARENTENA> result = new ResultDto<BM_PLACAS_CUARENTENA>(null);

            try
            {
                BM_PLACAS_CUARENTENA entityUpdate = await GetByCodigo(entity.CODIGO_PLACA_CUARENTENA);
                if (entityUpdate != null)
                {


                    _context.BM_PLACAS_CUARENTENA.Update(entity);
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

        public async Task<string> Delete(int codigoPlacaCuarentena)
        {

            try
            {
                BM_PLACAS_CUARENTENA entity = await GetByCodigo(codigoPlacaCuarentena);
                if (entity != null)
                {
                    _context.BM_PLACAS_CUARENTENA.Remove(entity);
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
                var last = await _context.BM_PLACAS_CUARENTENA.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_PLACA_CUARENTENA)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_PLACA_CUARENTENA + 1;
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

