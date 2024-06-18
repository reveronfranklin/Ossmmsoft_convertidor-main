
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
    public class PrePucModificacionRepository : IPrePucModificacionRepository
    {
        private readonly DataContextPre _context;

        public PrePucModificacionRepository(DataContextPre context)
        {
            _context = context;
        }

        public async Task<PRE_PUC_MODIFICACION> GetByCodigo(int codigoPucModificacion)
        {
            try
            {
                var result = await _context.PRE_PUC_MODIFICACION.DefaultIfEmpty().Where(e => e.CODIGO_PUC_MODIFICACION == codigoPucModificacion).FirstOrDefaultAsync();

                return (PRE_PUC_MODIFICACION)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<PRE_PUC_MODIFICACION>> GetAll()
        {
            try
            {
                var result = await _context.PRE_PUC_MODIFICACION.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<PRE_PUC_MODIFICACION>> GetByCodigoModificacion(int codigoModificacion)
        {
            try
            {
                var result = await _context.PRE_PUC_MODIFICACION.DefaultIfEmpty().Where(x=>x.CODIGO_MODIFICACION==codigoModificacion).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<ResultDto<PRE_PUC_MODIFICACION>> Add(PRE_PUC_MODIFICACION entity)
        {
            ResultDto<PRE_PUC_MODIFICACION> result = new ResultDto<PRE_PUC_MODIFICACION>(null);
            try
            {



                await _context.PRE_PUC_MODIFICACION.AddAsync(entity);
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

        public async Task<ResultDto<PRE_PUC_MODIFICACION>> Update(PRE_PUC_MODIFICACION entity)
        {
            ResultDto<PRE_PUC_MODIFICACION> result = new ResultDto<PRE_PUC_MODIFICACION>(null);

            try
            {
                PRE_PUC_MODIFICACION entityUpdate = await GetByCodigo(entity.CODIGO_PUC_MODIFICACION);
                if (entityUpdate != null)
                {


                    _context.PRE_PUC_MODIFICACION.Update(entity);
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

        public async Task<string> Delete(int codigoPucModificacion)
        {

            try
            {
                FormattableString xqueryDiario = $"DECLARE \nBEGIN\n DELETE FROM PRE_PUC_MODIFICACION WHERE CODIGO_PUC_MODIFICACION= {codigoPucModificacion};\nEND;";

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }



        }
        public async Task<bool> DeleteRange(int codigoModificacion)
        {

            try
            {
             
                FormattableString xqueryDiario = $"DECLARE \nBEGIN\n DELETE FROM PRE_PUC_MODIFICACION WHERE CODIGO_MODIFICACION= {codigoModificacion};\nEND;";

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                   // _context.PRE_PUC_MODIFICACION.RemoveRange(listDto);
                   // await _context.SaveChangesAsync();
          
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }



        }

        
        public async Task<int> GetNextKey()
        {
            try
            {
                int result = 0;
                var last = await _context.PRE_PUC_MODIFICACION.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_PUC_MODIFICACION)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_PUC_MODIFICACION + 1;
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
