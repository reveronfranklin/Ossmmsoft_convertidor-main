using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;
using Convertidor.Utility;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
    public class AdmSolicitudesRepository:IAdmSolicitudesRepository
    {
        private readonly DataContextAdm _context;


        public AdmSolicitudesRepository(DataContextAdm context)
        {
            _context = context;
            
        }

        public async Task<ADM_SOLICITUDES> GetByCodigoSolicitud(int codigoSolicitud)
        {
            try
            {
                var result = await _context.ADM_SOLICITUDES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_SOLICITUD == codigoSolicitud).FirstOrDefaultAsync();

                return (ADM_SOLICITUDES)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

      
        public List<AdmSolicitudesResponseDto> GetByPresupuesto(int codigoPresupuesto) 
        {
            try
            {
                /*var LambdaQuery = _context.ADM_SOLICITUDES
                    .Join(_context.ADM_PROVEEDORES, e => e.CODIGO_PROVEEDOR, d => d.CODIGO_PROVEEDOR, (e, d) 
                        => new {
                                    e, d.NOMBRE_PROVEEDOR
                                });
                var res = LambdaQuery.ToList();*/
                
                
                var linqQuery = from sol in _context.ADM_SOLICITUDES
                    join prov in _context.ADM_PROVEEDORES on sol.CODIGO_PROVEEDOR equals prov.CODIGO_PROVEEDOR
                    join descTipoSol in _context.ADM_DESCRIPTIVAS on sol.TIPO_SOLICITUD_ID equals descTipoSol.DESCRIPCION_ID
                 
                    select new AdmSolicitudesResponseDto() {
                        CodigoSolicitud = sol.CODIGO_SOLICITUD,
                        Ano = sol.ANO ,
                        NumeroSolicitud=sol.NUMERO_SOLICITUD,
                        FechaSolicitud=sol.FECHA_SOLICITUD,
                        FechaSolicitudString= Fecha.GetFechaString(sol.FECHA_SOLICITUD),
                        FechaSolicitudObj = Fecha.GetFechaDto(sol.FECHA_SOLICITUD),
                        CodigoSolicitante=sol.CODIGO_SOLICITANTE,
                        DenominacionSolicitante="",
                        TipoSolicitudId=sol.TIPO_SOLICITUD_ID,
                        DescripcionTipoSolicitud=descTipoSol.DESCRIPCION,
                        CodigoProveedor=sol.CODIGO_PROVEEDOR,
                        NombreProveedor = prov.NOMBRE_PROVEEDOR,
                        Motivo=sol.MOTIVO.Trim(),
                        Nota = sol.NOTA,
                        DescripcionStatus=Estatus.GetStatus(sol.STATUS),
                        CodigoPresupuesto=sol.CODIGO_PRESUPUESTO
                        
                    };
                var result = linqQuery.Where(x=>x.CodigoPresupuesto==codigoPresupuesto).ToList();
                //var result = await _context.ADM_SOLICITUDES.DefaultIfEmpty().Where(x =>x.CODIGO_PRESUPUESTO==codigoPresupuesto).ToListAsync();
                return result;
            }
            catch (Exception ex) 
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }

        public async Task<ResultDto<ADM_SOLICITUDES>>Add(ADM_SOLICITUDES entity) 
        {

            ResultDto<ADM_SOLICITUDES> result = new ResultDto<ADM_SOLICITUDES>(null);
            try 
            {
                await _context.ADM_SOLICITUDES.AddAsync(entity);
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

        public async Task<ResultDto<ADM_SOLICITUDES>>Update(ADM_SOLICITUDES entity) 
        {
            ResultDto<ADM_SOLICITUDES> result = new ResultDto<ADM_SOLICITUDES>(null);

            try
            {
                ADM_SOLICITUDES entityUpdate = await GetByCodigoSolicitud(entity.CODIGO_SOLICITUD);
                if (entityUpdate != null)
                {
                    _context.ADM_SOLICITUDES.Update(entity);
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
        public async Task<string>Delete(int codigoSolicitud) 
        {
            try
            {
                ADM_SOLICITUDES entity = await GetByCodigoSolicitud(codigoSolicitud);
                if (entity != null)
                {
                    _context.ADM_SOLICITUDES.Remove(entity);
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
                var last = await _context.ADM_SOLICITUDES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_SOLICITUD)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_SOLICITUD + 1;
                }

                return (int)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }



        }
        
        public async Task<string> UpdateStatus(int codigoSolicitud,string status)
        {

            try
            {
                
                FormattableString xqueryDiario = $"DECLARE \nBEGIN\n UPDATE  ADM_SOLICITUDESSET STATUS = {status} WHERE CODIGO_SOLICITUD= {codigoSolicitud};\nEND;";

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                // _context.PRE_MODIFICACION.Remove(entity);
                //  await _context.SaveChangesAsync();
                
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
