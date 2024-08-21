using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
    public class CatArrendamientosInmueblesRepository : ICatArrendamientosInmueblesRepository
    {
        private readonly DataContextCat _context;

        public CatArrendamientosInmueblesRepository(DataContextCat context)
        {
            _context = context;
        }

        public async Task<List<CAT_ARRENDAMIENTOS_INMUEBLES>> GetAll()
        {
            try
            {
                var result = await _context.CAT_ARRENDAMIENTOS_INMUEBLES.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<CAT_ARRENDAMIENTOS_INMUEBLES> GetByCodigo(int codigoArrendamientoInmueble)
        {
            try
            {

                var result = await _context.CAT_ARRENDAMIENTOS_INMUEBLES.DefaultIfEmpty()
                    .Where(x => x.CODIGO_ARRENDAMIENTO_INMUEBLE == codigoArrendamientoInmueble)
                    .FirstOrDefaultAsync();
                return (CAT_ARRENDAMIENTOS_INMUEBLES)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }

        }

        public async Task<ResultDto<CAT_ARRENDAMIENTOS_INMUEBLES>> Add(CAT_ARRENDAMIENTOS_INMUEBLES entity)
        {
            ResultDto<CAT_ARRENDAMIENTOS_INMUEBLES> result = new ResultDto<CAT_ARRENDAMIENTOS_INMUEBLES>(null);
            try
            {



                await _context.CAT_ARRENDAMIENTOS_INMUEBLES.AddAsync(entity);
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


        public async Task<ResultDto<CAT_ARRENDAMIENTOS_INMUEBLES>> Update(CAT_ARRENDAMIENTOS_INMUEBLES entity)
        {
            ResultDto<CAT_ARRENDAMIENTOS_INMUEBLES> result = new ResultDto<CAT_ARRENDAMIENTOS_INMUEBLES>(null);

            try
            {
                CAT_ARRENDAMIENTOS_INMUEBLES entityUpdate = await GetByCodigo(entity.CODIGO_ARRENDAMIENTO_INMUEBLE);
                if (entityUpdate != null)
                {


                    _context.CAT_ARRENDAMIENTOS_INMUEBLES.Update(entity);
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
        public async Task<int> GetNextKey()
        {
            try
            {
                int result = 0;
                var last = await _context.CAT_ARRENDAMIENTOS_INMUEBLES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_ARRENDAMIENTO_INMUEBLE)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_ARRENDAMIENTO_INMUEBLE + 1;
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
