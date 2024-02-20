using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmImpuestosOpRepository : IAdmImpuestosOpRepository
    {
        private readonly DataContextAdm _context;
        public AdmImpuestosOpRepository(DataContextAdm context)
        {
            _context = context;
        }

        public async Task<ADM_IMPUESTOS_OP> GetCodigoImpuestoOp(int codigoImpuestoOp)
        {
            try
            {
                var result = await _context.ADM_IMPUESTOS_OP
                    .Where(e => e.CODIGO_IMPUESTO_OP == codigoImpuestoOp).FirstOrDefaultAsync();

                return (ADM_IMPUESTOS_OP)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<ADM_IMPUESTOS_OP> GetByOrdenDePagoTipoImpuesto(int codigoOrdenDePago, int tipoImpuestoId)
        {
            try
            {
                var result = await _context.ADM_IMPUESTOS_OP
                    .Where(e => e.CODIGO_ORDEN_PAGO == codigoOrdenDePago && e.TIPO_IMPUESTO_ID==tipoImpuestoId).FirstOrDefaultAsync();

                return (ADM_IMPUESTOS_OP)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        public async Task<List<ADM_IMPUESTOS_OP>> GetAll()
        {
            try
            {
                var result = await _context.ADM_IMPUESTOS_OP.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }

        public async Task<ResultDto<ADM_IMPUESTOS_OP>> Add(ADM_IMPUESTOS_OP entity)
        {

            ResultDto<ADM_IMPUESTOS_OP> result = new ResultDto<ADM_IMPUESTOS_OP>(null);
            try
            {
                await _context.ADM_IMPUESTOS_OP.AddAsync(entity);
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

        public async Task<ResultDto<ADM_IMPUESTOS_OP>> Update(ADM_IMPUESTOS_OP entity)
        {
            ResultDto<ADM_IMPUESTOS_OP> result = new ResultDto<ADM_IMPUESTOS_OP>(null);

            try
            {
                ADM_IMPUESTOS_OP entityUpdate = await GetCodigoImpuestoOp(entity.CODIGO_IMPUESTO_OP);
                if (entityUpdate != null)
                {
                    _context.ADM_IMPUESTOS_OP.Update(entity);
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
        public async Task<string> Delete(int codigoImpuestoOp)
        {
            try
            {
                ADM_IMPUESTOS_OP entity = await GetCodigoImpuestoOp (codigoImpuestoOp);
                if (entity != null)
                {
                    _context.ADM_IMPUESTOS_OP.Remove(entity);
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
                var last = await _context.ADM_IMPUESTOS_OP.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_IMPUESTO_OP)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_IMPUESTO_OP + 1;
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
