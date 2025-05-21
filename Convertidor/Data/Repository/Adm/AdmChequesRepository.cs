using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmChequesRepository: IAdmChequesRepository
    {
        private readonly DataContextAdm _context;
        public AdmChequesRepository(DataContextAdm context)
        {
            _context = context;
        }

        public async Task<ADM_CHEQUES> GetByCodigoCheque(int codigoCheque)
        {
            try
            {
                var result = await _context.ADM_CHEQUES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_CHEQUE == codigoCheque).FirstOrDefaultAsync();

                return (ADM_CHEQUES)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

      
        
        public async Task<ResultDto<List<ADM_CHEQUES>>> GetByLote(AdmChequeFilterDto filter) 
        {
            
            ResultDto<List<ADM_CHEQUES>> result = new ResultDto<List<ADM_CHEQUES>>(null);
            if (filter.PageNumber == 0) filter.PageNumber = 1;
            if (filter.PageSize == 0) filter.PageSize = 100;
            if (filter.PageSize >100) filter.PageSize = 100;

            if (string.IsNullOrEmpty(filter.SearchText))
            {
                filter.SearchText = "";
            }
            try
            {
                var totalRegistros = 0;
                var totalPage = 0;
           
                List<ADM_CHEQUES> pageData = new List<ADM_CHEQUES>();
                if (filter.SearchText.Length == 0)
                {
                    totalRegistros =  _context.ADM_CHEQUES.DefaultIfEmpty()
                        .Where(e => e.CODIGO_LOTE_PAGO == filter.CodigoLote).Count();
                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    pageData = await _context.ADM_CHEQUES
                        .Where(x => x.CODIGO_LOTE_PAGO == filter.CodigoLote)
                        .OrderByDescending(x => x.CODIGO_CHEQUE)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
                else
                {
                    
                    totalRegistros =  _context.ADM_CHEQUES.DefaultIfEmpty()
                        .Where(e => e.CODIGO_LOTE_PAGO == filter.CodigoLote  && e.SEARCH_TEXT.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower())).Count();
                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    pageData = await _context.ADM_CHEQUES
                            .Where(x => x.CODIGO_LOTE_PAGO == filter.CodigoLote  && x.SEARCH_TEXT.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                            .OrderByDescending(x => x.CODIGO_CHEQUE)
                            .Skip((filter.PageNumber - 1) * filter.PageSize)
                            .Take(filter.PageSize)
                            .ToListAsync();
                   
                }
             
                result.CantidadRegistros = totalRegistros;
                result.TotalPage = totalPage;
                result.Page = filter.PageNumber;
                result.IsValid = true;
                result.Message = "";
                result.Data = pageData;
                return result;
            }
            catch (Exception ex) 
            {
                result.CantidadRegistros = 0;
                result.IsValid = false;
                result.Message = ex.Message;
                result.Data = null;
                return result;
            }
        }

        public async Task<ResultDto<ADM_CHEQUES>>Add(ADM_CHEQUES entity) 
        {

            ResultDto<ADM_CHEQUES> result = new ResultDto<ADM_CHEQUES>(null);
            try 
            {
                await _context.ADM_CHEQUES.AddAsync(entity);
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

        
        public async Task<string> UpdateSearchText(int codigoLote)
        {

            try
            {
                FormattableString xqueryDiario = $"UPDATE ADM.ADM_CHEQUES SET ADM.ADM_CHEQUES.SEARCH_TEXT = (SELECT DESCRIPCION FROM ADM.ADM_DESCRIPTIVAS    WHERE ADM.ADM_DESCRIPTIVAS.DESCRIPCION_ID  =ADM.ADM_CHEQUES.TIPO_CHEQUE_ID) ||\n(SELECT NO_CUENTA FROM SIS.SIS_CUENTAS_BANCOS  WHERE SIS.SIS_CUENTAS_BANCOS.CODIGO_CUENTA_BANCO  =ADM.ADM_CHEQUES.CODIGO_CUENTA_BANCO) ||\n(SELECT CODIGO_BANCO FROM SIS.SIS_CUENTAS_BANCOS  WHERE SIS.SIS_CUENTAS_BANCOS.CODIGO_CUENTA_BANCO  =ADM.ADM_CHEQUES.CODIGO_CUENTA_BANCO) ||\n(SELECT NOMBRE FROM SIS.SIS_BANCOS WHERE SIS.SIS_BANCOS.CODIGO_BANCO=(SELECT CODIGO_BANCO FROM SIS.SIS_CUENTAS_BANCOS  WHERE SIS.SIS_CUENTAS_BANCOS.CODIGO_CUENTA_BANCO  =ADM.ADM_CHEQUES.CODIGO_CUENTA_BANCO)) ||\n(SELECT NOMBRE_PROVEEDOR FROM ADM.ADM_PROVEEDORES WHERE CODIGO_PROVEEDOR=ADM.ADM_CHEQUES.CODIGO_PROVEEDOR)\nWHERE CODIGO_LOTE_PAGO ={codigoLote}";

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }




        }
        
        
   
        public async Task<ResultDto<ADM_CHEQUES>>Update(ADM_CHEQUES entity) 
        {
            ResultDto<ADM_CHEQUES> result = new ResultDto<ADM_CHEQUES>(null);

            try
            {
                ADM_CHEQUES entityUpdate = await GetByCodigoCheque(entity.CODIGO_CHEQUE);
                if (entityUpdate != null)
                {
                    _context.ADM_CHEQUES.Update(entity);
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
        public async Task<string>Delete(int codigoCheque) 
        {
            try
            {
                ADM_CHEQUES entity = await GetByCodigoCheque(codigoCheque);
                if (entity != null)
                {
                    
                    FormattableString xqueryBeneficiarioPago = $"DELETE FROM  ADM.ADM_BENEFICIARIOS_CH  WHERE CODIGO_CHEQUE = {codigoCheque}";
                    var resultBeneficiario = await _context.Database.ExecuteSqlInterpolatedAsync(xqueryBeneficiarioPago);

                    
                    
                    FormattableString xqueryPago = $"DELETE FROM ADM.ADM_CHEQUES WHERE CODIGO_CHEQUE = {codigoCheque}";
                    var resulPagot = await _context.Database.ExecuteSqlInterpolatedAsync(xqueryPago);

            
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
                var last = await _context.ADM_CHEQUES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_CHEQUE)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_CHEQUE + 1;
                }

                return (int)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }



        }
        
        public async Task<int> GetNextCheque(int numeroChequera,int codigoPresupuesto)
        {
            try
            {
                int result = 0;
                var last = await _context.ADM_CHEQUES.Where(x=>x.CODIGO_PRESUPUESTO==codigoPresupuesto && x.NUMERO_CHEQUERA == numeroChequera)
                   .OrderByDescending(x=>x.NUMERO_CHEQUE)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.NUMERO_CHEQUE + 1;
                }

                return (int)result;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }



        }

    }
}
