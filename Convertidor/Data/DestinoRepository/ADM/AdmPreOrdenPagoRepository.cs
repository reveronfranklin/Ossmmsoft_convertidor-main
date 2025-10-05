using System.Globalization;
using Convertidor.Data;
using Convertidor.Data.DestinoInterfaces.ADM;
using Convertidor.Data.EntitiesDestino.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Adm.PreOrdenPago;
using Microsoft.EntityFrameworkCore;


namespace Convertidor.Data.DestinoRepository.ADM{

    public class AdmPreOrdenPagoRepository: IAdmPreOrdenPagoRepository
    {
        private readonly DestinoDataContext _context;
        public AdmPreOrdenPagoRepository(DestinoDataContext context)
        {
            _context = context;
        }

        public async Task<ADM_PRE_ORDEN_PAGO> GetById(int id)
        {
            
            
            try
            {
                var result = await _context.ADM_PRE_ORDEN_PAGO
                    .Where(e => e.Id == id).DefaultIfEmpty().FirstOrDefaultAsync();

                return (ADM_PRE_ORDEN_PAGO)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        
        
        
        public async  Task<ResultDto<List<ADM_PRE_ORDEN_PAGO>>> GetAll(AdmPreOrdenPagoFilterDto filter)
        {
            
            ResultDto<List<ADM_PRE_ORDEN_PAGO>> result = new ResultDto<List<ADM_PRE_ORDEN_PAGO>>(null);
            
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
                
                List<ADM_PRE_ORDEN_PAGO> pageData = new List<ADM_PRE_ORDEN_PAGO>();
                if ( filter.SearchText.Length==0)
                {
                    totalRegistros = _context.ADM_PRE_ORDEN_PAGO
                        .Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    
                    pageData = await _context.ADM_PRE_ORDEN_PAGO.DefaultIfEmpty()
                       
                        .OrderByDescending(x => x.Id)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
                if (filter.SearchText.Length>0)
                {
                    totalRegistros = _context.ADM_PRE_ORDEN_PAGO
                        .Where(x => x.SEARCH_TEXT.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    
                    pageData = await _context.ADM_PRE_ORDEN_PAGO.DefaultIfEmpty()
                        .Where(x =>x.SEARCH_TEXT.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .OrderByDescending(x => x.Id)
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
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<ADM_PRE_ORDEN_PAGO> GetByNumeroFactura(string numeroFactura)
        {
            try
            {
                var result = await _context.ADM_PRE_ORDEN_PAGO
                    .Where(e => e.NumeroFactura == numeroFactura).DefaultIfEmpty().FirstOrDefaultAsync();

                return (ADM_PRE_ORDEN_PAGO)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        
        public async Task<ADM_PRE_ORDEN_PAGO> GetByRifNumeroFactura(string rif,string numeroFactura)
        {
            try
            {
                var result = await _context.ADM_PRE_ORDEN_PAGO
                    .Where(e =>e.Rif==rif && e.NumeroFactura == numeroFactura).DefaultIfEmpty().FirstOrDefaultAsync();

                return (ADM_PRE_ORDEN_PAGO)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        
     
        public async Task<ResultDto<ADM_PRE_ORDEN_PAGO>>Add(ADM_PRE_ORDEN_PAGO entity) 
        {

            ResultDto<ADM_PRE_ORDEN_PAGO> result = new ResultDto<ADM_PRE_ORDEN_PAGO>(null);
            try 
            {
                await _context.ADM_PRE_ORDEN_PAGO.AddAsync(entity);
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
                if (ex.InnerException != null)
                {
                    result.Message = ex.InnerException.Message;
                }
                else
                {
                    result.Message = ex.Message;
                }
                
                return result;
            }
        }

        public async Task<ResultDto<ADM_PRE_ORDEN_PAGO>>Update(ADM_PRE_ORDEN_PAGO entity) 
        {
            ResultDto<ADM_PRE_ORDEN_PAGO> result = new ResultDto<ADM_PRE_ORDEN_PAGO>(null);

            try
            {
                ADM_PRE_ORDEN_PAGO entityUpdate = await GetById(entity.Id);
                if (entityUpdate != null)
                {
                    _context.ADM_PRE_ORDEN_PAGO.Update(entity);
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
        public async Task<string>Delete(int id) 
        {
            try
            {
                ADM_PRE_ORDEN_PAGO entity = await GetById(id);
                if (entity != null)
                {
                   _context.ADM_PRE_ORDEN_PAGO.Remove(entity);
                    await _context.SaveChangesAsync();
                   
                 
                }
                
             
                
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
        public async Task<ResultDto<string>> DeleteALL() 
        {
            
            ResultDto<string> result = new ResultDto<string>("");
            try
            {
              
                // Verificar si hay registros primero
                var existenRegistros = await _context.ADM_PRE_ORDEN_PAGO.AnyAsync();
        
                if (!existenRegistros)
                {
                    result.IsValid = true;
                    result.Message = "No hay registros para eliminar";
                    return result;
                  
                }
        
                // Eliminar usando Entity Framework
                var registros = _context.ADM_PRE_ORDEN_PAGO.ToList();
                _context.ADM_PRE_ORDEN_PAGO.RemoveRange(registros);
                var resultAfected = await _context.SaveChangesAsync();
                result.IsValid = true;
                result.Message =$"Eliminados {result} registros correctamente";
                return result;
          
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }
        }
        public async Task<int> GetNextKey()
        {
            try
            {
                int result = 0;
                var last = await _context.ADM_PRE_ORDEN_PAGO.DefaultIfEmpty()
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
        
     
    }
}
