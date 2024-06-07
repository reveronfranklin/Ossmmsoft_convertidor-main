using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
	public class PRE_ASIGNACIONESRepository : IPRE_ASIGNACIONESRepository
    {
	

        private readonly DataContextPre _context;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public PRE_ASIGNACIONESRepository(DataContextPre context, ISisUsuarioRepository sisUsuarioRepository)
        {
            _context = context;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        
        public async Task<PRE_ASIGNACIONES> GetByCodigo(int codigo)
        {
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
             
                var result = await _context.PRE_ASIGNACIONES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_EMPRESA==conectado.Empresa && e.CODIGO_ASIGNACION == codigo).FirstOrDefaultAsync();

                return (PRE_ASIGNACIONES)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
       
        public async Task<List<PRE_ASIGNACIONES>> GetAll()
        {
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var result = await _context.PRE_ASIGNACIONES.DefaultIfEmpty().Where(x=>x.CODIGO_EMPRESA==conectado.Empresa).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<PRE_ASIGNACIONES>> GetAllByPresupuesto(int codigoPresupuesto)
        {
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var result = await _context.PRE_ASIGNACIONES.DefaultIfEmpty().Where(x=> x.CODIGO_EMPRESA==conectado.Empresa && x.CODIGO_PRESUPUESTO==codigoPresupuesto).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<PRE_ASIGNACIONES>> GetAllByIcp(int codigoPresupuesto,int codigoIcp)
        {
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var result = await _context.PRE_ASIGNACIONES.DefaultIfEmpty().Where(x=> x.CODIGO_EMPRESA==conectado.Empresa && x.CODIGO_PRESUPUESTO==codigoPresupuesto && x.CODIGO_ICP==codigoIcp).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<PRE_ASIGNACIONES>> GetAllByIcpPuc(int codigoPresupuesto,int codigoIcp,int codigoPuc)
        {
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var result = await _context.PRE_ASIGNACIONES.DefaultIfEmpty().Where(x=> x.CODIGO_EMPRESA==conectado.Empresa  && x.CODIGO_PRESUPUESTO==codigoPresupuesto && x.CODIGO_ICP==codigoIcp && x.CODIGO_PUC==codigoPuc).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<decimal> GetTotalAsignacionByIcpPuc(int codigoPresupuesto, int codigoIcp, int codigoPuc)
        {
            decimal totalAsignacion = 0;
            try
            {
                
                var conectado = await _sisUsuarioRepository.GetConectado();
                var asignaciones = await _context.PRE_ASIGNACIONES.DefaultIfEmpty().Where(x => x.CODIGO_EMPRESA == conectado.Empresa && x.CODIGO_PRESUPUESTO == codigoPresupuesto && x.CODIGO_ICP == codigoIcp && x.CODIGO_PUC == codigoPuc).ToListAsync();
                if (asignaciones.Count > 0)
                {
                    foreach (var item in asignaciones)
                    {
                        totalAsignacion = totalAsignacion + item.TOTAL_DESEMBOLSO;
                    }
                }
                return totalAsignacion;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return 0;
            }

        }


        public async Task<ResultDto<PRE_ASIGNACIONES>> Add(PRE_ASIGNACIONES entity)
        {
            ResultDto<PRE_ASIGNACIONES> result = new ResultDto<PRE_ASIGNACIONES>(null);
            try
            {



                await _context.PRE_ASIGNACIONES.AddAsync(entity);
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

        public async Task<ResultDto<PRE_ASIGNACIONES>> Update(PRE_ASIGNACIONES entity)
        {
            ResultDto<PRE_ASIGNACIONES> result = new ResultDto<PRE_ASIGNACIONES>(null);

            try
            {
                PRE_ASIGNACIONES entityUpdate = await GetByCodigo(entity.CODIGO_ASIGNACION);
                if (entityUpdate != null)
                {


                    _context.PRE_ASIGNACIONES.Update(entity);
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

        public async Task<string> Delete(int codigoAsignacion)
        {

            try
            {
                PRE_ASIGNACIONES entity = await GetByCodigo(codigoAsignacion);
                if (entity != null)
                {
                    _context.PRE_ASIGNACIONES.Remove(entity);
                    await _context.SaveChangesAsync();
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }



        }
        
        public async Task<string> DeleteByPresupuesto(int codigoPresupuesto)
        {

            try
            {
                FormattableString xqueryDiario = $"DECLARE \nBEGIN\n DELETE FROM PRE.PRE_ASIGNACIONES WHERE CODIGO_PRESUPUESTO= {codigoPresupuesto};\nEND;";

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
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
                var last = await _context.PRE_ASIGNACIONES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_ASIGNACION)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_ASIGNACION + 1;
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
                var asignaciones = await _context.PRE_ASIGNACIONES.DefaultIfEmpty()
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
        public async Task<bool> PresupuestoExisteConMonto(int codPresupuesto)
        {

            bool result;
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var asignaciones = await _context.PRE_ASIGNACIONES.DefaultIfEmpty()
                    .Where(x => x.CODIGO_EMPRESA==conectado.Empresa  && x.CODIGO_PRESUPUESTO == codPresupuesto &&  x.PRESUPUESTADO>0).FirstOrDefaultAsync();
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
                var asignaciones = await _context.PRE_ASIGNACIONES.DefaultIfEmpty()
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
                var asignaciones = await _context.PRE_ASIGNACIONES.DefaultIfEmpty()
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

