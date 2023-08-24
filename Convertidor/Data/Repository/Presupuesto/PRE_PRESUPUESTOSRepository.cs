using System;
using Convertidor.Data.Entities;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.EntitiesDestino;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static Convertidor.Dtos.PetroBsGetDto;

namespace Convertidor.Data.Repository.Presupuesto
{
	public class PRE_PRESUPUESTOSRepository: IPRE_PRESUPUESTOSRepository
    {
		
        private readonly DataContextPre _context;

        public PRE_PRESUPUESTOSRepository(DataContextPre context)
        {
            _context = context;
           
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
                entity.FECHA_INS = DateTime.Now;
                //entity.USUARIO_INS = conectado.Usuario;
                //entity.CODIGO_EMPRESA = conectado.Empresa;
                await _context.PRE_PRESUPUESTOS.AddAsync(entity);
                _context.SaveChanges();


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
                    
                    entity.DENOMINACION = entity.DENOMINACION.ToUpper();
                    entity.DESCRIPCION = entity.DESCRIPCION.ToUpper();
                    entity.FECHA_DESDE = entity.FECHA_DESDE.Date;
                    entity.FECHA_HASTA = entity.FECHA_HASTA.Date;
                    entity.FECHA_UPD = DateTime.Now;
                   
                    _context.PRE_PRESUPUESTOS.Update(entity);
                    _context.SaveChanges();
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
                PRE_PRESUPUESTOS entity = await GetByCodigo(codigoEmpresa, codigoPresupuesto);
                if (entity != null)
                {
                    _context.PRE_PRESUPUESTOS.Remove(entity);
                    _context.SaveChanges();
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
         


        }

        public async Task RecalcularSaldo(int codigo_presupuesto)
        {

            var codigo_presupuestoP = new SqlParameter("@Codigo_Presupuesto", codigo_presupuesto);
          
            try
            {
                var result = await _context.PRE_PRESUPUESTOS.FromSqlRaw<PRE_PRESUPUESTOS>("execute PRE.PRE_ACTUALIZAR_SALDOS @Codigo_Presupuesto", codigo_presupuestoP).ToListAsync();
                var aprobacion = result.FirstOrDefault();

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

