﻿using System.Globalization;
using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmRetencionesOpRepository : IAdmRetencionesOpRepository
    {
        private readonly DataContextAdm _context;
        public AdmRetencionesOpRepository(DataContextAdm context)
        {
            _context = context;
        }

        
        public async Task<ADM_RETENCIONES_OP> GetByOrdenPagoCodigoRetencionTipoRetencion(int codigoOrdenPago,int codigoRetencion,int tipoRetencionId)
        {
            try
            {
                var result = await _context.ADM_RETENCIONES_OP
                    .Where(e => e.CODIGO_ORDEN_PAGO == codigoOrdenPago && e.CODIGO_RETENCION==codigoRetencion && e.TIPO_RETENCION_ID==tipoRetencionId).FirstOrDefaultAsync();

                return (ADM_RETENCIONES_OP)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        public async Task<ADM_RETENCIONES_OP> GetCodigoRetencionOp(int codigoRetencionOp)
        {
            try
            {
                var result = await _context.ADM_RETENCIONES_OP
                    .Where(e => e.CODIGO_RETENCION_OP == codigoRetencionOp).FirstOrDefaultAsync();

                return (ADM_RETENCIONES_OP)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<string> UpdateMontos(int codigoRetencionOp,decimal montoRetencion,decimal baseImponible)
        {
            try
            {
              
                FormattableString xqueryDiario = $"UPDATE ADM.ADM_RETENCIONES_OP SET MONTO_RETENCION={montoRetencion},BASE_IMPONIBLE={baseImponible} WHERE CODIGO_RETENCION_OP= {codigoRetencionOp}";

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> DeleteByOrdePago(int codigoOrdenPago)
        {


           
            try
            {
            
                
                FormattableString xqueryDiario = $"DELETE FROM  ADM.ADM_RETENCIONES_OP WHERE CODIGO_ORDEN_PAGO= {codigoOrdenPago};";

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public async Task<string> DeleteByOrdePagoSinIva(int codigoOrdenPago,int idDescriptivaIva)
        {


           
            try
            {
            
                
                FormattableString xqueryDiario = $"DELETE FROM  ADM.ADM_RETENCIONES_OP WHERE CODIGO_ORDEN_PAGO= {codigoOrdenPago} AND TIPO_RETENCION_ID = {idDescriptivaIva};";

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        

        public async Task<List<ADM_RETENCIONES_OP>> GetAll() 
        {
            try
            {
                var result = await _context.ADM_RETENCIONES_OP.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex) 
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }
        public async Task<List<ADM_RETENCIONES_OP>> GetByOrdenPago(int codigoOrdenPago) 
        {
            try
            {
                var result = await _context.ADM_RETENCIONES_OP
                    
                    .Where(x=>x.CODIGO_ORDEN_PAGO==codigoOrdenPago).ToListAsync();
                return result;
            }
            catch (Exception ex) 
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }
      

        public async Task<ResultDto<ADM_RETENCIONES_OP>>Add(ADM_RETENCIONES_OP entity) 
        {

            ResultDto<ADM_RETENCIONES_OP> result = new ResultDto<ADM_RETENCIONES_OP>(null);
            try 
            {
                await _context.ADM_RETENCIONES_OP.AddAsync(entity);
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

        public async Task<ResultDto<ADM_RETENCIONES_OP>>Update(ADM_RETENCIONES_OP entity) 
        {
            ResultDto<ADM_RETENCIONES_OP> result = new ResultDto<ADM_RETENCIONES_OP>(null);

            try
            {
                ADM_RETENCIONES_OP entityUpdate = await GetCodigoRetencionOp(entity.CODIGO_RETENCION_OP);
                if (entityUpdate != null)
                {
                    _context.ADM_RETENCIONES_OP.Update(entity);
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
        public async Task<string>Delete(int codigoRetencionOp) 
        {
            try
            {
                ADM_RETENCIONES_OP entity = await GetCodigoRetencionOp(codigoRetencionOp);
                if (entity != null)
                {
                    _context.ADM_RETENCIONES_OP.Remove(entity);
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
                var last = await _context.ADM_RETENCIONES_OP.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_RETENCION_OP)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_RETENCION_OP + 1;
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
