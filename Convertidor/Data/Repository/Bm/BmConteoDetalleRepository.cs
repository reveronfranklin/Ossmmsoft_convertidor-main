using System;
using System;
using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Services.Sis;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Bm
{
	public class BmConteoDetalleRepository: IBmConteoDetalleRepository
    {
		
        private readonly DataContextBm _context;
        private readonly IOssConfigServices _configServices;

        public BmConteoDetalleRepository(DataContextBm context,IOssConfigServices configServices)
        {
            _context = context;
            _configServices = configServices;
        }
      
        public async Task<BM_CONTEO_DETALLE> GetByCodigo(int conteoDetalleId)
        {
            try
            {
                var result = await _context.BM_CONTEO_DETALLE.DefaultIfEmpty().Where(e => e.CODIGO_BM_CONTEO_DETALLE == conteoDetalleId).FirstOrDefaultAsync();

                return (BM_CONTEO_DETALLE)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<List<BM_CONTEO_DETALLE>> GetAllByConteo(int codigoConteo)
        {
            try
            {
                var result = await _context.BM_CONTEO_DETALLE.DefaultIfEmpty().Where(c=>c.CODIGO_BM_CONTEO==codigoConteo).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        
        public async Task<bool> ExisteConteo(int codigoConteo)
        {
            
            try
            {
                var result = await _context.BM_CONTEO_DETALLE.DefaultIfEmpty().Where(c=>c.CODIGO_BM_CONTEO==codigoConteo).FirstOrDefaultAsync();
                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return false;
            }

        }


        public async Task<bool> ConteoIniciado(int codigoConteo)
        {
            
            try
            {
                var result = await _context.BM_CONTEO_DETALLE.DefaultIfEmpty().Where(c=>c.CODIGO_BM_CONTEO==codigoConteo && c.CANTIDAD_CONTADA>0).FirstOrDefaultAsync();
                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return false;
            }

        }
       
        public async Task<bool> ConteoIniciadoConDiferenciaSinComentario(int codigoConteo)
        {
            
            try
            {
                var result = await _context.BM_CONTEO_DETALLE.DefaultIfEmpty().Where(c=>c.CODIGO_BM_CONTEO==codigoConteo && 
                             c.CANTIDAD != c.CANTIDAD_CONTADA && (c.COMENTARIO == null || c.COMENTARIO.Length==0))
                            .FirstOrDefaultAsync();
                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return false;
            }

        }
        public async Task<List<BM_CONTEO_DETALLE>> GetAll()
        {
            try
            {
                var result = await _context.BM_CONTEO_DETALLE.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


     
      



        public async Task<ResultDto<BM_CONTEO_DETALLE>> Add(BM_CONTEO_DETALLE entity)
        {
            ResultDto<BM_CONTEO_DETALLE> result = new ResultDto<BM_CONTEO_DETALLE>(null);
            try
            {



                await _context.BM_CONTEO_DETALLE.AddAsync(entity);
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


        public ResultDto<bool> CrearDesdeBm1(string unidadTrabajo,int codigoEmpresa,int usuario,int codigoConteo,int cantidadConteos)
        {
            ResultDto<bool> result = new ResultDto<bool>(false);
            //CALL BM.BM_P_CONTEO('2211,2214', 13, -1, 2, 2)
           
            try
            {
                var query=  $"DECLARE \nBEGIN\nBM.BM_P_CONTEO('{unidadTrabajo}',{codigoEmpresa},{usuario},{codigoConteo},{cantidadConteos});\nEND;";
                //FormattableString xqueryDiario = $"DECLARE \nBEGIN\nBM.BM_P_CONTEO('{unidadTrabajo}',{codigoEmpresa},{usuario},{codigoConteo},{cantidadConteos});\nEND;";
                query =
                    $"CALL BM.BM_P_CONTEO(''{unidadTrabajo}'',{codigoEmpresa},{usuario},{codigoConteo},{cantidadConteos});";
                FormattableString xqueryDiario = $"CALL BM.BM_P_CONTEO(''{unidadTrabajo}'',{codigoEmpresa},{usuario},{codigoConteo},{cantidadConteos});";

                var resultDiario =  _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                result.Data = true;
                result.IsValid = true;
                result.Message = "";
              

            }
            catch (Exception e)
            {
                result.Data = false;
                result.IsValid = false;
                result.Message = e.Message;
         
            }
   
            return result;

        }
        
        public async Task<ResultDto<List<BM_CONTEO_DETALLE>>> AddRange(List<BM_CONTEO_DETALLE> entity)
        {
            ResultDto<List<BM_CONTEO_DETALLE>> result = new ResultDto<List<BM_CONTEO_DETALLE>>(null);
            try
            {



                await _context.BM_CONTEO_DETALLE.AddRangeAsync(entity);
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

        public async Task<ResultDto<BM_CONTEO_DETALLE>> Update(BM_CONTEO_DETALLE entity)
        {
            ResultDto<BM_CONTEO_DETALLE> result = new ResultDto<BM_CONTEO_DETALLE>(null);

            try
            {
         


                    _context.BM_CONTEO_DETALLE.Update(entity);
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

        public async Task<string> Delete(int tituloId)
        {
            try
            {
                BM_CONTEO_DETALLE entity = await GetByCodigo(tituloId);
                if (entity != null)
                {
                    _context.BM_CONTEO_DETALLE.Remove(entity);
                    await _context.SaveChangesAsync();
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        
        public async Task<bool> DeleteRangeConteo(int codigoConteo)
        {
            try
            {
               var entities = await GetAllByConteo(codigoConteo);
                if (entities != null)
                {
                    _context.BM_CONTEO_DETALLE.RemoveRange(entities);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        
        public async Task<int> GetNextKey()
        {
            try
            {
                int result = 0;
                var last = await _context.BM_CONTEO_DETALLE.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_BM_CONTEO_DETALLE)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_BM_CONTEO_DETALLE + 1;
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

