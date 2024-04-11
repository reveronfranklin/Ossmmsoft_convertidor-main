using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
	public class PRE_SALDOSRepository : IPRE_SALDOSRepository
    {
	

        private readonly DataContextPre _context;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public PRE_SALDOSRepository(DataContextPre context, ISisUsuarioRepository sisUsuarioRepository)
        {
            _context = context;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        
        public async Task<PRE_SALDOS> GetByCodigo(int codigo)
        {
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
             
                var result = await _context.PRE_SALDOS.DefaultIfEmpty()
                    .Where(e => e.CODIGO_EMPRESA==conectado.Empresa && e.CODIGO_SALDO== codigo).FirstOrDefaultAsync();

                return (PRE_SALDOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
       
        public async Task<List<PRE_SALDOS>> GetAll()
        {
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var result = await _context.PRE_SALDOS.DefaultIfEmpty().Where(x=>x.CODIGO_EMPRESA==conectado.Empresa).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<PRE_SALDOS>> GetAllByPresupuesto(int codigoPresupuesto)
        {
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var result = await _context.PRE_SALDOS.DefaultIfEmpty().Where(x=> x.CODIGO_EMPRESA==conectado.Empresa && x.CODIGO_PRESUPUESTO==codigoPresupuesto).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<PRE_SALDOS>> GetAllByIcp(int codigoPresupuesto,int codigoIcp)
        {
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var result = await _context.PRE_SALDOS.DefaultIfEmpty().Where(x=> x.CODIGO_EMPRESA==conectado.Empresa && x.CODIGO_PRESUPUESTO==codigoPresupuesto && x.CODIGO_ICP==codigoIcp).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<PRE_SALDOS>> GetAllByIcpPuc(int codigoPresupuesto,int codigoIcp,int codigoPuc)
        {
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                var result = await _context.PRE_SALDOS.DefaultIfEmpty().Where(x=> x.CODIGO_EMPRESA==conectado.Empresa  && x.CODIGO_PRESUPUESTO==codigoPresupuesto && x.CODIGO_ICP==codigoIcp && x.CODIGO_PUC==codigoPuc).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


        public async Task<ResultDto<PRE_SALDOS>> Add(PRE_SALDOS entity)
        {
            ResultDto<PRE_SALDOS> result = new ResultDto<PRE_SALDOS>(null);
            try
            {



                await _context.PRE_SALDOS.AddAsync(entity);
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

        public async Task<ResultDto<PRE_SALDOS>> Update(PRE_SALDOS entity)
        {
            ResultDto<PRE_SALDOS> result = new ResultDto<PRE_SALDOS>(null);

            try
            {
                PRE_SALDOS entityUpdate = await GetByCodigo(entity.CODIGO_SALDO);
                if (entityUpdate != null)
                {


                    _context.PRE_SALDOS.Update(entity);
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
                PRE_SALDOS entity = await GetByCodigo(codigoAsignacion);
                if (entity != null)
                {
                    _context.PRE_SALDOS.Remove(entity);
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
                FormattableString xqueryDiario = $"DECLARE \nBEGIN\n DELETE FROM PRE.PRE_SALDOS WHERE CODIGO_PRESUPUESTO= {codigoPresupuesto};\nEND;";

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
                var last = await _context.PRE_SALDOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_SALDO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_SALDO + 1;
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
                var asignaciones = await _context.PRE_SALDOS.DefaultIfEmpty()
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
                var asignaciones = await _context.PRE_SALDOS.DefaultIfEmpty()
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
                var asignaciones = await _context.PRE_SALDOS.DefaultIfEmpty()
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
                var asignaciones = await _context.PRE_SALDOS.DefaultIfEmpty()
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

