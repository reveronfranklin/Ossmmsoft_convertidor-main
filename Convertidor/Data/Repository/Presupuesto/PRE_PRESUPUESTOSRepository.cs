using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
	public class PRE_PRESUPUESTOSRepository: IPRE_PRESUPUESTOSRepository
    {
		
        private readonly DataContextPre _context;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public PRE_PRESUPUESTOSRepository(DataContextPre context,ISisUsuarioRepository sisUsuarioRepository)
        {
            _context = context;
            _sisUsuarioRepository = sisUsuarioRepository;
        }


     
        public async Task<IEnumerable<PRE_PRESUPUESTOS>> GetAll()
        {
            try
            {

                var result = await _context.PRE_PRESUPUESTOS.DefaultIfEmpty().ToListAsync();
                return (IEnumerable<PRE_PRESUPUESTOS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<PRE_PRESUPUESTOS> GetUltimo()
        {
            try
            {

                var result = await _context.PRE_PRESUPUESTOS.DefaultIfEmpty()
                    .OrderByDescending(x=>x.CODIGO_PRESUPUESTO).FirstOrDefaultAsync();
                return (PRE_PRESUPUESTOS)result!;
               
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }
        public async Task<PRE_PRESUPUESTOS> GetByCodigo(int codigoEmpresa ,int codigoPresupuesto)
        {
            try
            {
                
                var result = await _context.PRE_PRESUPUESTOS.DefaultIfEmpty()
                    .Where(x => x.CODIGO_EMPRESA == codigoEmpresa && x.CODIGO_PRESUPUESTO == codigoPresupuesto)
                    .FirstOrDefaultAsync();
                return (PRE_PRESUPUESTOS)result!;
               
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }

        public async Task<PRE_PRESUPUESTOS> GetByCodigoPresupuesto(int codigoPresupuesto)
        {
            try
            {

                var result = await _context.PRE_PRESUPUESTOS.DefaultIfEmpty()
                    .Where(x =>x.CODIGO_PRESUPUESTO == codigoPresupuesto)
                    .FirstOrDefaultAsync();
                return (PRE_PRESUPUESTOS)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }

        public async Task<ResultDto<PRE_PRESUPUESTOS>> GetByCodigoEmpresaPeriodo(int codigoEmpresa, int periodo)
        {

            ResultDto<PRE_PRESUPUESTOS> result = new ResultDto<PRE_PRESUPUESTOS>(null);

            try
            {

                var presupuesto = await _context.PRE_PRESUPUESTOS.DefaultIfEmpty()
                    .Where(x => x.CODIGO_EMPRESA == codigoEmpresa && x.ANO == periodo)
                    .FirstOrDefaultAsync();
                result.Data = presupuesto;
                result.IsValid = true;
                result.Message = "";
                return result;
               

            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.InnerException.Message;
                return result;
               
            }



        }


        public async Task<bool> ExisteEnPeriodo(int codigoEmpresa, DateTime desde,DateTime hasta)
        {


            try
            {
                bool result=false;
                var presupuestoMenor = await _context.PRE_PRESUPUESTOS.DefaultIfEmpty()
                  .Where(x => x.CODIGO_EMPRESA == codigoEmpresa && x.FECHA_HASTA >= hasta)
                  .FirstOrDefaultAsync();
                if (presupuestoMenor != null)
                {
                    result = true;
                }

                var presupuesto = await _context.PRE_PRESUPUESTOS.DefaultIfEmpty()
                    .Where(x => x.CODIGO_EMPRESA == codigoEmpresa && x.FECHA_DESDE ==  desde && x.FECHA_HASTA==hasta)
                    .FirstOrDefaultAsync();
                if (presupuesto != null) {
                    result = true;
                 } 
                return result;

            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return true;
            }



        }

        public async Task<ResultDto<PRE_PRESUPUESTOS>> Add(PRE_PRESUPUESTOS entity)
        {
            ResultDto<PRE_PRESUPUESTOS> result = new ResultDto<PRE_PRESUPUESTOS>(null);
            try
            {
                //var conectado = await _sisUsuarioRepository.GetConectado();
                entity.DENOMINACION= entity.DENOMINACION.ToUpper();
                entity.DESCRIPCION = entity.DESCRIPCION.ToUpper();
                entity.FECHA_DESDE = entity.FECHA_DESDE.Date;
                entity.FECHA_HASTA = entity.FECHA_HASTA.Date;
                entity.FECHA_APROBACION = null;
                entity.FECHA_UPD = null;
                entity.FECHA_INS = DateTime.Now;
                //entity.USUARIO_INS = conectado.Usuario;
                //entity.CODIGO_EMPRESA = conectado.Empresa;
                await _context.PRE_PRESUPUESTOS.AddAsync(entity);
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
                result.Message = ex.InnerException.Message;
                return result;
            }
            


        }

        public async Task<ResultDto<PRE_PRESUPUESTOS>> Update(PRE_PRESUPUESTOS entity)
        {
            ResultDto<PRE_PRESUPUESTOS> result = new ResultDto<PRE_PRESUPUESTOS>(null);

            try
            {
                PRE_PRESUPUESTOS entityUpdate = await GetByCodigo(entity.CODIGO_EMPRESA, entity.CODIGO_PRESUPUESTO);
                if (entityUpdate != null)
                {
                    entity.DESCRIPCION = entity.DESCRIPCION ?? "";
                    entity.DENOMINACION = entity.DENOMINACION ?? "";

                    entity.DENOMINACION = entity.DENOMINACION.ToUpper();
                    entity.DESCRIPCION = entity.DESCRIPCION.ToUpper();
                    entity.FECHA_DESDE = entity.FECHA_DESDE.Date;
                    entity.FECHA_HASTA = entity.FECHA_HASTA.Date;
                    entity.FECHA_UPD = DateTime.Now;
                   
                    _context.PRE_PRESUPUESTOS.Update(entity);
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
                result.Message = ex.InnerException.Message;
                return result;
            }






        }

        public async Task<string> Delete(int codigoEmpresa, int codigoPresupuesto)
        {

            try
            {
                
                FormattableString xqueryDiario = $"DECLARE \nBEGIN\nDELETE FROM PRE.PRE_PRESUPUESTOS WHERE CODIGO_PRESUPUESTO = {codigoPresupuesto};\nEND;";

                var resultDiario =  _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                
                
                /*PRE_PRESUPUESTOS entity = await GetByCodigo(codigoEmpresa, codigoPresupuesto);
                if (entity != null)
                {
                    _context.PRE_PRESUPUESTOS.Remove(entity);
                    await _context.SaveChangesAsync();
                }*/
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
         


        }

        
        public async Task<string> AprobarPresupuesto(int codigoPresupuesto,int codigoUsuario,int codigoEmpresa)
        {
            if(codigoUsuario==0) codigoUsuario=1;
            var result ="";
         
            try
            {
               
                FormattableString xqueryDiario = $"DECLARE \nBEGIN\nPRE.PRE_PKG_CLONAR_PRESUPUESTO.INSERT_PRE_SALDO({codigoPresupuesto},{codigoUsuario},{codigoEmpresa});\nEND;";

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                return result;

            }
            catch (Exception ex)
            {
                var mess = ex.InnerException.Message;
                result=mess;
               return result;
            }




        }

        public async Task RecalcularSaldo(int codigo_presupuesto)
        {

     
            try
            {
 

                FormattableString xquerySaldo = $"CALL PRE.PRE_ACTUALIZAR_SALDOS({codigo_presupuesto})";
                var result = await _context.Database.ExecuteSqlInterpolatedAsync(xquerySaldo);


            }
            catch (Exception ex)
            {
                var mess = ex.InnerException.Message;

                throw;
            }




        }

        public async Task<PRE_PRESUPUESTOS> GetLast()
        {
            try
            {
                int result = 0;
                var last = await _context.PRE_PRESUPUESTOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_PRESUPUESTO)
                    .FirstOrDefaultAsync();
                

                return last!;

            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return (PRE_PRESUPUESTOS)null;
            }



        }
        public async Task<int> GetNextKey()
        {
            try
            {
                int result = 0;
                var last = await _context.PRE_PRESUPUESTOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_PRESUPUESTO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result= last.CODIGO_PRESUPUESTO + 1;
                }

                return (int)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return 0;
            }



        }


    }
}

