using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Entities.Sis;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Sis
{
	public class OssCalculoRepository: IOssCalculoRepository
    {
		
        private readonly DataContextSis _context;
   
        public OssCalculoRepository(DataContextSis context)
        {
            _context = context;
           
        }

      
        public async Task<OssCalculo> GetById(int id)
        {
            try
            {
                var result = await _context.OssCalculos.AsNoTracking().DefaultIfEmpty().Where(e => e.Id == id).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        
        public async Task<List<OssCalculo>> GetByIdCalculo(int idCalculo)
        {
            try
            {
                var result = await _context.OssCalculos.AsNoTracking().DefaultIfEmpty().Where(e => e.IdCalculo == idCalculo).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        public async Task<OssCalculo> GetByIdCalculoCode(int idCalculo,string codeVariable)
        {
            try
            {
                var result = await _context.OssCalculos.AsNoTracking().DefaultIfEmpty().Where(e => e.IdCalculo == idCalculo && e.CodeVariable==codeVariable).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        
        public async Task<List<OssCalculo>> GetFormulasByIdCalculo(int idCalculo)
        {
            try
            {
                var result = await _context.OssCalculos.AsNoTracking().DefaultIfEmpty().Where(e => e.IdCalculo == idCalculo && e.Formula!=null && e.Formula.Length>0).OrderBy(e=>e.OrdenCalculo).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        public async Task<ResultDto<OssCalculo>> Add(OssCalculo entity)
        {
            ResultDto<OssCalculo> result = new ResultDto<OssCalculo>(null);
            try
            {



                await _context.OssCalculos.AddAsync(entity);
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

        public async Task<ResultDto<OssCalculo>> Update(OssCalculo entity)
        {
            ResultDto<OssCalculo> result = new ResultDto<OssCalculo>(null);

            try
            {
                  
                    _context.OssCalculos.Update(entity);
                    await _context.SaveChangesAsync();
                    _context.Dispose();
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

        public async Task<string> Delete(int id)
        {

            try
            {
                OssCalculo entity = await GetById(id);
                if (entity != null)
                {
                    _context.OssCalculos.Remove(entity);
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
                var last = await _context.OssCalculos.AsNoTracking().DefaultIfEmpty()
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.Id + 1;
                }

                return (int)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }



        }

        public void ExecuteQueryUpdateValor(string valueFormula,int id,string tipoVariable)
        {
            var query = $"DECLARE \nBEGIN\n SIS.OSS_P_UPDATE_VALOR_CALCULO({ id.ToString()}, {valueFormula},{tipoVariable});\nEND;";
            FormattableString xquery = $"DECLARE \nBEGIN\n SIS.OSS_P_UPDATE_VALOR_CALCULO({ id.ToString()}, {valueFormula},{tipoVariable});\nEND;";
            //FormattableString xquery = $"DECLARE \nBEGIN\nPRE.PRE_ACTUALIZAR_SALDOS({codigo_presupuesto});\nEND;";
            var result = _context.Database.ExecuteSqlInterpolated(xquery);

        }
        
    }
}

