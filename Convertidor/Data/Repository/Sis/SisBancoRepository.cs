using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos.Sis;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Sis
{
	public class SisBancoRepository: ISisBancoRepository
    {
		
        private readonly DataContextSis _context;

        public SisBancoRepository(DataContextSis context)
        {
            _context = context;
        }
      
        public async Task<SIS_BANCOS> GetByCodigo(int code)
        {
            try
            {
                var result = await _context.SIS_BANCOS.DefaultIfEmpty().Where(e => e.CODIGO_BANCO == code).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        public async Task<SIS_BANCOS> GetById(int id)
        {
            
         
            try
            {
                var result = await _context.SIS_BANCOS.DefaultIfEmpty().Where(e => e.CODIGO_BANCO == id).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        
        public async Task<SIS_BANCOS> GetByCodigoInterbancario(string codigoInterbancario)
        {
            
         
            try
            {
                var result = await _context.SIS_BANCOS.DefaultIfEmpty().Where(e => e.CODIGO_INTERBANCARIO == codigoInterbancario).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

 
        public async Task<ResultDto<List<SIS_BANCOS>>> GetAll(SisBancoFilterDto filter)
        {
            ResultDto<List<SIS_BANCOS>> result = new ResultDto<List<SIS_BANCOS>>(null);
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
              
                List<SIS_BANCOS> pageData = new List<SIS_BANCOS>();
                if ( filter.SearchText.Length==0)
                {
                    totalRegistros = _context.SIS_BANCOS
                        .Where(x=>x.CODIGO_EMPRESA==filter.CodigEmpresa)
                        .Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    
                    pageData = await _context.SIS_BANCOS.DefaultIfEmpty()
                        .Where(x=>x.CODIGO_EMPRESA==filter.CodigEmpresa)
                        .OrderBy(x => x.NOMBRE)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
                if ( filter.SearchText.Length>0)
                {
                    totalRegistros = _context.SIS_BANCOS
                        .Where(x => x.CODIGO_EMPRESA==filter.CodigEmpresa && x.NOMBRE.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    
                    pageData = await _context.SIS_BANCOS.DefaultIfEmpty()
                        .Where(x =>x.CODIGO_EMPRESA==filter.CodigEmpresa &&  x.NOMBRE.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .OrderBy(x => x.NOMBRE)
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
        

        public async Task<ResultDto<SIS_BANCOS>> Add(SIS_BANCOS entity)
        {
            ResultDto<SIS_BANCOS> result = new ResultDto<SIS_BANCOS>(null);
            try
            {



                await _context.SIS_BANCOS.AddAsync(entity);
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

        public async Task<ResultDto<SIS_BANCOS>> Update(SIS_BANCOS entity)
        {
            ResultDto<SIS_BANCOS> result = new ResultDto<SIS_BANCOS>(null);

            try
            {
                SIS_BANCOS entityUpdate = await GetById(entity.CODIGO_BANCO);
                if (entityUpdate != null)
                {


                    _context.SIS_BANCOS.Update(entity);
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
                SIS_BANCOS entity = await GetById(id);
                if (entity != null)
                {
                    _context.SIS_BANCOS.Remove(entity);
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
                var last = await _context.SIS_BANCOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_BANCO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_BANCO + 1;
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

