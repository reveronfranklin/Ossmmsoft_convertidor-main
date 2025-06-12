using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Bm
{
    public class BmBienesFotoRepository: IBmBienesFotoRepository
    {
		
        private readonly DataContextBmConteo _context;

        public BmBienesFotoRepository(DataContextBmConteo context)
        {
            _context = context;
        }
        public async Task<BM_BIENES_FOTO> GetByCodigo(int codigoBienFoto)
        {
            try
            {
                var result = await _context.BM_BIENES_FOTO.DefaultIfEmpty()
                    .Where(e => e.CODIGO_BIEN_FOTO == codigoBienFoto).FirstOrDefaultAsync();
        
                return (BM_BIENES_FOTO)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<BM_BIENES_FOTO>> GetByCodigoBien(int codigoBien)
        {
            try
            {
                var result = await _context.BM_BIENES_FOTO.DefaultIfEmpty()
                    .Where(e => e.CODIGO_BIEN == codigoBien).ToListAsync();
        
                return (List<BM_BIENES_FOTO>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<BM_BIENES_FOTO>> GetByNumeroPlaca(string numeroPlaca)
        {
            try
            {
                var result = await _context.BM_BIENES_FOTO.DefaultIfEmpty()
                    .Where(e => e.NUMERO_PLACA == numeroPlaca).ToListAsync();
        
                return (List<BM_BIENES_FOTO>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public IQueryable<BM_BIENES> BienesConFoto()
        {
            var consulta = from bienes in  _context.BM_BIENES
                join fotos in _context.BM_BIENES_FOTO
                    on bienes.NUMERO_PLACA equals fotos.NUMERO_PLACA
               
                select bienes;
            
          
            return consulta;
        }
        public async Task<int> CantidadFotosPorPlaca(string numeroPlaca)
        {
            

           
            
            var cont = (from d in _context.BM_BIENES_FOTO.DefaultIfEmpty() 
               
                where d.NUMERO_PLACA == numeroPlaca
                select d).Count();
            if (cont == null) cont = 0;
            return cont;
        }
        public async Task<BM_BIENES_FOTO> GetByNumeroPlacaFoto(string numeroPlaca,string foto)
        {
            try
            {
                var result = await _context.BM_BIENES_FOTO.DefaultIfEmpty()
                    .Where(e => e.NUMERO_PLACA == numeroPlaca && e.FOTO == foto).FirstOrDefaultAsync();
        
                return (BM_BIENES_FOTO)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        

        public async Task<ResultDto<BM_BIENES_FOTO>> Add(BM_BIENES_FOTO entity)
        {
            ResultDto<BM_BIENES_FOTO> result = new ResultDto<BM_BIENES_FOTO>(null);
            try
            {



                await _context.BM_BIENES_FOTO.AddAsync(entity);
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

        public async Task<ResultDto<BM_BIENES_FOTO>> Update(BM_BIENES_FOTO entity)
        {
            ResultDto<BM_BIENES_FOTO> result = new ResultDto<BM_BIENES_FOTO>(null);

            try
            {
                BM_BIENES_FOTO entityUpdate = await GetByCodigo(entity.CODIGO_BIEN_FOTO);
                if (entityUpdate != null)
                {


                    _context.BM_BIENES_FOTO.Update(entity);
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

        public async Task<string> Delete(int codigoBien)
        {

            try
            {
                BM_BIENES_FOTO entity = await GetByCodigo(codigoBien);
                if (entity != null)
                {
                    _context.BM_BIENES_FOTO.Remove(entity);
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
                var last = await _context.BM_BIENES_FOTO.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_BIEN_FOTO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_BIEN_FOTO + 1;
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


