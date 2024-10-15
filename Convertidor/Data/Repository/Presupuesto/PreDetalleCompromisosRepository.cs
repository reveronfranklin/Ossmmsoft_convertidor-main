using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository.Sis;
using Convertidor.Dtos.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class PreDetalleCompromisosRepository: IPreDetalleCompromisosRepository
    {
		
        private readonly DataContextPre _context;
        private readonly DataContextSis _contextSis;


        public PreDetalleCompromisosRepository(DataContextPre context,DataContextSis contextSis)
        {
            _context = context;
            _contextSis = contextSis;
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
        
        public async Task<string> LimpiaEnrer()
        {
            
            try
            {
                FormattableString xqueryDiario =$" UPDATE PRE.PRE_DETALLE_COMPROMISOS  SET DESCRIPCION = REPLACE(DESCRIPCION, CHR(10), ' ') WHERE DESCRIPCION LIKE '%'||CHR(10)||'%';";
                

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                
                
                
                xqueryDiario =$" UPDATE PRE.PRE_DETALLE_COMPROMISOS  SET DESCRIPCION = REPLACE(DESCRIPCION, CHR(9), ' ') WHERE DESCRIPCION LIKE '%'||CHR(9)||'%';";

                _context.Database.ExecuteSqlInterpolated(xqueryDiario);

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }
        
       
        
        public async Task<TotalesResponseDto> GetTotales(int codigoPresupuesto, int codigoCompromiso)
        {
            TotalesResponseDto result = new TotalesResponseDto();

            var tipoImpuesto = 0;
            string variableImpuesto = "DESCRIPTIVA_IMPUESTO";
            var config = await _contextSis.OSS_CONFIG.Where(x=>x.CLAVE==variableImpuesto).FirstOrDefaultAsync();
            if (config != null)
            {

                tipoImpuesto = int.Parse(config.VALOR);

            }
            var detalle = await _context.PRE_DETALLE_COMPROMISOS.DefaultIfEmpty().Where(x =>x.CODIGO_COMPROMISO==codigoCompromiso && x.CODIGO_PRESUPUESTO==codigoPresupuesto ).ToListAsync();
            if (detalle.Count > 0)
            {
                decimal? sum = detalle.Where(x=>x.TIPO_IMPUESTO_ID!=tipoImpuesto).Sum(x => x.TOTAL);
                result.Base = (decimal)sum;
                
                decimal? sumImponible = detalle.Where(x=>x.TIPO_IMPUESTO_ID!=tipoImpuesto && x.POR_IMPUESTO>0).Sum(x => x.TOTAL);
                result.BaseImponible = (decimal)sumImponible;
                var detalleImpuesto = await _context.PRE_DETALLE_COMPROMISOS.DefaultIfEmpty().Where(x =>x.CODIGO_COMPROMISO==codigoCompromiso && x.CODIGO_PRESUPUESTO==codigoPresupuesto && x.TIPO_IMPUESTO_ID==tipoImpuesto).FirstOrDefaultAsync();
                if (detalleImpuesto != null)
                {
                    result.Impuesto = (decimal)detalleImpuesto.TOTAL;
          
                }
                else
                {
                    decimal? sumImpuesto = detalle.Sum(x => x.MONTO_IMPUESTO);
                    result.Impuesto = (decimal)sumImpuesto;
                }

                result.TotalMasImpuesto = result.Base + result.Impuesto;
                result.PorcentajeImpuesto = 0;
                if(result.BaseImponible != 0)
                {
                    result.PorcentajeImpuesto = (result.Impuesto / result.BaseImponible) * 100;
                }

            }
            else
            {
                result.Base = 0;
                result.Impuesto = 0;
                result.TotalMasImpuesto=0;
                result.PorcentajeImpuesto = 0;
            }
            
            
        


            return result;
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

