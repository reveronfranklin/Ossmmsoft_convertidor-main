using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.Sis;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Sis;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
	public class AdmLotePagoRepository: IAdmLotePagoRepository
    {
		
        private readonly DataContextAdm _context;

        public AdmLotePagoRepository(DataContextAdm context)
        {
            _context = context;
        }
      
        public async Task<ADM_LOTE_PAGO> GetByCodigo(int codigo)
        {
            try
            {
                var result = await _context.ADM_LOTE_PAGO.DefaultIfEmpty().Where(e => e.CODIGO_LOTE_PAGO == codigo).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
      
 
        public async Task<ResultDto<List<ADM_LOTE_PAGO>>> GetAll(AdmLotePagoFilterDto filter)
        {
            ResultDto<List<ADM_LOTE_PAGO>> result = new ResultDto<List<ADM_LOTE_PAGO>>(null);
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
              
                List<ADM_LOTE_PAGO> pageData = new List<ADM_LOTE_PAGO>();
                if ( filter.SearchText.Length==0)
                {
                    totalRegistros = _context.ADM_LOTE_PAGO
                        .Where(x=>x.CODIGO_EMPRESA==filter.CodigEmpresa && x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto)
                        .Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    
                    pageData = await _context.ADM_LOTE_PAGO.DefaultIfEmpty()
                        .Where(x=>x.CODIGO_EMPRESA==filter.CodigEmpresa && x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto)
                        .OrderByDescending(x => x.CODIGO_LOTE_PAGO)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
                if ( filter.SearchText.Length>0)
                {
                    totalRegistros = _context.ADM_LOTE_PAGO
                        .Where(x => x.CODIGO_EMPRESA==filter.CodigEmpresa && x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto && x.SEARCH_TEXT.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    
                    pageData = await _context.ADM_LOTE_PAGO.DefaultIfEmpty()
                        .Where(x =>x.CODIGO_EMPRESA==filter.CodigEmpresa && x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto &&  x.SEARCH_TEXT.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .OrderByDescending(x => x.CODIGO_LOTE_PAGO)
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
        

        public async Task<ResultDto<ADM_LOTE_PAGO>> Add(ADM_LOTE_PAGO entity)
        {
            ResultDto<ADM_LOTE_PAGO> result = new ResultDto<ADM_LOTE_PAGO>(null);
            try
            {



                await _context.ADM_LOTE_PAGO.AddAsync(entity);
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

        public async Task<ResultDto<ADM_LOTE_PAGO>> Update(ADM_LOTE_PAGO entity)
        {
            ResultDto<ADM_LOTE_PAGO> result = new ResultDto<ADM_LOTE_PAGO>(null);

            try
            {
                ADM_LOTE_PAGO entityUpdate = await GetByCodigo(entity.CODIGO_LOTE_PAGO);
                if (entityUpdate != null)
                {


                    _context.ADM_LOTE_PAGO.Update(entity);
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
                ADM_LOTE_PAGO entity = await GetByCodigo(id);
                if (entity != null)
                {
                    _context.ADM_LOTE_PAGO.Remove(entity);
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
                var last = await _context.ADM_LOTE_PAGO.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_LOTE_PAGO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_LOTE_PAGO + 1;
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

