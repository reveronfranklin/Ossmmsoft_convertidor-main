using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos.Sis;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Sis
{
	public class SisCuentaBancoRepository: ISisCuentaBancoRepository
    {
		
        private readonly DataContextSis _context;

        public SisCuentaBancoRepository(DataContextSis context)
        {
            _context = context;
        }
      
        public async Task<SIS_CUENTAS_BANCOS> GetByCodigo(int code)
        {
            try
            {
                var result = await _context.SIS_CUENTAS_BANCOS.DefaultIfEmpty().Where(e => e.CODIGO_CUENTA_BANCO == code).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        public async Task<SIS_CUENTAS_BANCOS> GetById(int id)
        {
            
         
            try
            {
                var result = await _context.SIS_CUENTAS_BANCOS.DefaultIfEmpty().Where(e => e.CODIGO_CUENTA_BANCO == id).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
 
        public async Task<bool> ExisteBanco(int codigoBanco)
        {
            var result = false;
            try
            {
             
                var cuenta = await _context.SIS_CUENTAS_BANCOS.DefaultIfEmpty().Where(e => e.CODIGO_BANCO == codigoBanco).FirstOrDefaultAsync();
                if (cuenta != null)
                {
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
               
                return result;
            }

        }
        
        public async Task<ResultDto<List<SIS_CUENTAS_BANCOS>>> GetAll(SisCuentaBancoFilterDto filter)
        {
            ResultDto<List<SIS_CUENTAS_BANCOS>> result = new ResultDto<List<SIS_CUENTAS_BANCOS>>(null);
            if (filter.PageNumber == 0) filter.PageNumber = 1;
            if (filter.PageSize == 0) filter.PageSize = 100;
            if (filter.PageSize >100) filter.PageSize = 100;

            if (string.IsNullOrEmpty(filter.SearchText))
            {
                filter.SearchText = "";
            }
            try
            {
                await UpdateSearchText();
                var totalRegistros = 0;
                var totalPage = 0;
              
                List<SIS_CUENTAS_BANCOS> pageData = new List<SIS_CUENTAS_BANCOS>();
                if ( filter.SearchText.Length==0)
                {
                    totalRegistros = _context.SIS_CUENTAS_BANCOS
                        .Where(x=>x.CODIGO_EMPRESA==filter.CodigEmpresa)
                        .Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    
                    pageData = await _context.SIS_CUENTAS_BANCOS.DefaultIfEmpty()
                        .Where(x=>x.CODIGO_EMPRESA==filter.CodigEmpresa)
                        .OrderBy(x => x.NO_CUENTA)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
                if ( filter.SearchText.Length>0)
                {
                    totalRegistros = _context.SIS_CUENTAS_BANCOS
                        .Where(x => x.CODIGO_EMPRESA==filter.CodigEmpresa && x.SEARCH_TEXT.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    
                    pageData = await _context.SIS_CUENTAS_BANCOS.DefaultIfEmpty()
                        .Where(x =>x.CODIGO_EMPRESA==filter.CodigEmpresa &&  x.SEARCH_TEXT.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .OrderBy(x => x.NO_CUENTA)
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
        

        public async Task<ResultDto<SIS_CUENTAS_BANCOS>> Add(SIS_CUENTAS_BANCOS entity)
        {
            ResultDto<SIS_CUENTAS_BANCOS> result = new ResultDto<SIS_CUENTAS_BANCOS>(null);
            try
            {



                await _context.SIS_CUENTAS_BANCOS.AddAsync(entity);
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

        public async Task<ResultDto<SIS_CUENTAS_BANCOS>> Update(SIS_CUENTAS_BANCOS entity)
        {
            ResultDto<SIS_CUENTAS_BANCOS> result = new ResultDto<SIS_CUENTAS_BANCOS>(null);

            try
            {
                SIS_CUENTAS_BANCOS entityUpdate = await GetById(entity.CODIGO_BANCO);
                if (entityUpdate != null)
                {


                    _context.SIS_CUENTAS_BANCOS.Update(entity);
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

        public async Task<string> Delete(int id)
        {

            try
            {
                SIS_CUENTAS_BANCOS entity = await GetById(id);
                if (entity != null)
                {
                    _context.SIS_CUENTAS_BANCOS.Remove(entity);
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
                var last = await _context.SIS_CUENTAS_BANCOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_CUENTA_BANCO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_CUENTA_BANCO + 1;
                }

                return (int)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }



        }


        
        public async Task<string> UpdateSearchText()
        {

            try
            {
                FormattableString xqueryDiario = $"UPDATE SIS.SIS_CUENTAS_BANCOS SET SIS.SIS_CUENTAS_BANCOS.SEARCH_TEXT =   TRIM(NO_CUENTA) || (SELECT NOMBRE FROM SIS.SIS_BANCOS WHERE SIS.SIS_BANCOS.CODIGO_BANCO  = SIS.SIS_CUENTAS_BANCOS.CODIGO_BANCO)";

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        
    }
}

