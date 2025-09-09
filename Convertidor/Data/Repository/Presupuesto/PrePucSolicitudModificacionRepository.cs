using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
	public class PrePucSolicitudModificacionRepository : IPrePucSolicitudModificacionRepository
    {
	

        private readonly DataContextPre _context;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public PrePucSolicitudModificacionRepository(DataContextPre context, ISisUsuarioRepository sisUsuarioRepository)
        {
            _context = context;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        
        public async Task<PRE_PUC_SOL_MODIFICACION> GetByCodigo(int codigo)
        {
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
             
                var result = await _context.PRE_PUC_SOL_MODIFICACION.DefaultIfEmpty()
                    .Where(e => e.CODIGO_EMPRESA==conectado.Empresa && e.CODIGO_PUC_SOL_MODIFICACION == codigo).FirstOrDefaultAsync();

                return (PRE_PUC_SOL_MODIFICACION)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
       
        public async Task<List<PRE_PUC_SOL_MODIFICACION>> GetAll()
        {
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var result = await _context.PRE_PUC_SOL_MODIFICACION.DefaultIfEmpty().Where(x=>x.CODIGO_EMPRESA==conectado.Empresa).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<PRE_PUC_SOL_MODIFICACION>> GetAllByPresupuesto(int codigoPresupuesto)
        {
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var result = await _context.PRE_PUC_SOL_MODIFICACION.DefaultIfEmpty().Where(x=> x.CODIGO_EMPRESA==conectado.Empresa && x.CODIGO_PRESUPUESTO==codigoPresupuesto).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<PRE_PUC_SOL_MODIFICACION>> GetAllByIcp(int codigoPresupuesto,int codigoIcp)
        {
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var result = await _context.PRE_PUC_SOL_MODIFICACION.DefaultIfEmpty().Where(x=> x.CODIGO_EMPRESA==conectado.Empresa && x.CODIGO_PRESUPUESTO==codigoPresupuesto && x.CODIGO_ICP==codigoIcp).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<PRE_PUC_SOL_MODIFICACION>> GetAllByCodigoSolicitud(int codigoSolicitud)
        {
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var result = await _context.PRE_PUC_SOL_MODIFICACION.DefaultIfEmpty().Where(x=> x.CODIGO_EMPRESA==conectado.Empresa && x.CODIGO_SOL_MODIFICACION==codigoSolicitud).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        
        
        public async Task<List<PRE_PUC_SOL_MODIFICACION>> GetAllByIcpPuc(int codigoPresupuesto,int codigoIcp,int codigoPuc)
        {
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var result = await _context.PRE_PUC_SOL_MODIFICACION.DefaultIfEmpty().Where(x=> x.CODIGO_EMPRESA==conectado.Empresa  && x.CODIGO_PRESUPUESTO==codigoPresupuesto && x.CODIGO_ICP==codigoIcp && x.CODIGO_PUC==codigoPuc).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        
        public async Task<PRE_PUC_SOL_MODIFICACION> GetAllByIcpPucFinanciadoSolicitud(int codigoPresupuesto,int codigoIcp,int codigoPuc,string financiadoId,int codigoSolicitud)
        {
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var result = await _context.PRE_PUC_SOL_MODIFICACION.DefaultIfEmpty().Where(x=> x.CODIGO_EMPRESA==conectado.Empresa  && x.CODIGO_PRESUPUESTO==codigoPresupuesto && x.CODIGO_ICP==codigoIcp && x.CODIGO_PUC==codigoPuc && x.FINANCIADO_ID==financiadoId && x.CODIGO_SOL_MODIFICACION==codigoSolicitud).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<PRE_PUC_SOL_MODIFICACION> GetAllByIcpPucFinanciadoSolicitud(int codigoPresupuesto,int codigoIcp,int codigoPuc,string financiadoId,int codigoSolicitud,int codigoFinanciado)
        {
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var result = await _context.PRE_PUC_SOL_MODIFICACION.DefaultIfEmpty().Where(x=> x.CODIGO_EMPRESA==conectado.Empresa  && x.CODIGO_PRESUPUESTO==codigoPresupuesto && x.CODIGO_ICP==codigoIcp && x.CODIGO_PUC==codigoPuc && x.FINANCIADO_ID==financiadoId && x.CODIGO_SOL_MODIFICACION==codigoSolicitud && x.CODIGO_FINANCIADO==codigoFinanciado).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        
        public async Task<PRE_PUC_SOL_MODIFICACION> GetByCodigoSolModificacionCodigoSaldo(int codigoSolModificacion,int codigoSaldo)
        {
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var result = await _context.PRE_PUC_SOL_MODIFICACION.DefaultIfEmpty().Where(x=> x.CODIGO_SOL_MODIFICACION==codigoSolModificacion && x.CODIGO_SALDO==codigoSaldo).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


        public async Task<ResultDto<PRE_PUC_SOL_MODIFICACION>> Add(PRE_PUC_SOL_MODIFICACION entity)
        {
            ResultDto<PRE_PUC_SOL_MODIFICACION> result = new ResultDto<PRE_PUC_SOL_MODIFICACION>(null);
            try
            {



                await _context.PRE_PUC_SOL_MODIFICACION.AddAsync(entity);
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

        public async Task<ResultDto<PRE_PUC_SOL_MODIFICACION>> Update(PRE_PUC_SOL_MODIFICACION entity)
        {
            ResultDto<PRE_PUC_SOL_MODIFICACION> result = new ResultDto<PRE_PUC_SOL_MODIFICACION>(null);

            try
            {
                PRE_PUC_SOL_MODIFICACION entityUpdate = await GetByCodigo(entity.CODIGO_PUC_SOL_MODIFICACION);
                if (entityUpdate != null)
                {


                    _context.PRE_PUC_SOL_MODIFICACION.Update(entity);
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

        public async Task<bool> DeleteByCodigoSolicitud(int codigoSolModificacion)
        {

            try
            {
             
                FormattableString xqueryDiario = $"DECLARE \nBEGIN\n DELETE FROM PRE_PUC_SOL_MODIFICACION WHERE CODIGO_SOL_MODIFICACION= {codigoSolModificacion};\nEND;";

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                // _context.PRE_PUC_MODIFICACION.RemoveRange(listDto);
                // await _context.SaveChangesAsync();
          
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }



        }
        
        public async Task<string> Delete(int codigo)
        {

            try
            {
                PRE_PUC_SOL_MODIFICACION entity = await GetByCodigo(codigo);
                if (entity != null)
                {
                    _context.PRE_PUC_SOL_MODIFICACION.Remove(entity);
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
                var last = await _context.PRE_PUC_SOL_MODIFICACION.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_PUC_SOL_MODIFICACION)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_PUC_SOL_MODIFICACION + 1;
                }

                return (int)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }



        }

        public async Task<bool> PresupuestoExiste(int codPresupuesto)
        {

            bool result;
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var asignaciones = await _context.PRE_PUC_SOL_MODIFICACION.DefaultIfEmpty()
                    .Where(x => x.CODIGO_EMPRESA==conectado.Empresa  && x.CODIGO_PRESUPUESTO == codPresupuesto).FirstOrDefaultAsync();
                if (asignaciones != null)
                {
                    result = true;
                }
                else {
                    result = false;
                }


                return result;
            }
            catch (Exception ex)
            {

                return false;
            }

        }
       

        public async Task<bool> ICPExiste(int codigoICP)
        {

            bool result;
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var asignaciones = await _context.PRE_PUC_SOL_MODIFICACION.DefaultIfEmpty()
                    .Where(x => x.CODIGO_EMPRESA==conectado.Empresa && x.CODIGO_ICP == codigoICP).FirstOrDefaultAsync();
                if (asignaciones != null)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }


                return result;
            }
            catch (Exception ex)
            {

                return false;
            }

        }

        public async Task<bool> PUCExiste(int codigoPUC)
        {

            bool result;
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var asignaciones = await _context.PRE_PUC_SOL_MODIFICACION.DefaultIfEmpty()
                    .Where(x => x.CODIGO_EMPRESA==conectado.Empresa && x.CODIGO_PUC == codigoPUC).FirstOrDefaultAsync();
                if (asignaciones != null)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }


                return result;
            }
            catch (Exception ex)
            {

                return false;
            }

        }

    }
}

