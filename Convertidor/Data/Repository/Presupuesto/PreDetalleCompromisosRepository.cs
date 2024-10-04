using Convertidor.Data.Entities.Presupuesto;
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

    
        public decimal GetTotal(int codigoCompromiso)
        {
            try
            {
                PRE_DETALLE_COMPROMISOS detalle = new PRE_DETALLE_COMPROMISOS();
                
                var result =  _context.PRE_DETALLE_COMPROMISOS.DefaultIfEmpty().Where(e => e.CODIGO_COMPROMISO == codigoCompromiso).Sum(x=>x.TOTAL_MAS_IMPUESTO);

                return (decimal)result;
            }
            catch (Exception ex)
            {
               
                return 0;
            }

        }
        
        public async Task<string> ActualizaMontos(int codigoPresupuesto)
        {
            
            try
            {
                FormattableString xqueryDiario =$"UPDATE  PRE.PRE_DETALLE_COMPROMISOS  SET  TOTAL_MAS_IMPUESTO=  decode(por_impuesto,0,round(cantidad*precio_unitario,2),((cantidad*precio_unitario)+(por_impuesto*round((cantidad*precio_unitario)/100,2) ))),TOTAL=(cantidad*precio_unitario),MONTO_IMPUESTO=decode(por_impuesto,0,0,(por_impuesto*round(cantidad*precio_unitario,2)/100 ) ) WHERE codigo_presupuesto = {codigoPresupuesto} AND TOTAL IS NULL OR TOTAL=0";
                

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }
        public decimal GetTotalImpuesto(int codigoCompromiso)
        {
            try
            {
                PRE_DETALLE_COMPROMISOS detalle = new PRE_DETALLE_COMPROMISOS();
                
                var result =  _context.PRE_DETALLE_COMPROMISOS.DefaultIfEmpty().Where(e => e.CODIGO_COMPROMISO == codigoCompromiso).Sum(x=>x.MONTO_IMPUESTO);

                return (decimal)result;
            }
            catch (Exception ex)
            {
               
                return 0;
            }

        }
        public decimal GetTotalMonto(int codigoCompromiso)
        {
            try
            {
                PRE_DETALLE_COMPROMISOS detalle = new PRE_DETALLE_COMPROMISOS();
                
                var result =  _context.PRE_DETALLE_COMPROMISOS.DefaultIfEmpty().Where(e => e.CODIGO_COMPROMISO == codigoCompromiso).Sum(x=>x.TOTAL);

                return (decimal)result;
            }
            catch (Exception ex)
            {
               
                return 0;
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

