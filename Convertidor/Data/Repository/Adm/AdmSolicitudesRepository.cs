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

      
        public async Task<ResultDto<List<AdmSolicitudesResponseDto>>> GetByPresupuesto(AdmSolicitudesFilterDto filter) 
        {
            ResultDto<List<AdmSolicitudesResponseDto>> result = new ResultDto<List<AdmSolicitudesResponseDto>>(null);

            if (filter.PageNumber == 0) filter.PageNumber = 1;
            if (filter.PageSize == 0) filter.PageSize = 100;
            if (filter.PageSize >100) filter.PageSize = 100;
            
            try
            {
                /*var LambdaQuery = _context.ADM_SOLICITUDES
                    .Join(_context.ADM_PROVEEDORES, e => e.CODIGO_PROVEEDOR, d => d.CODIGO_PROVEEDOR, (e, d) 
                        => new {
                                    e, d.NOMBRE_PROVEEDOR
                                });
                var res = LambdaQuery.ToList();*/
                
                
                /*if (filter.SearchText != null && filter.SearchText.Length > 0)
                {
                    result = await _context.AppGeneralQuotes
                        .AsNoTracking()
                        .Include(x => x.IdClienteNavigation)
                        .Include(x => x.IdVendedorNavigation)
                        .Include(x => x.IdContactoNavigation)
                        .Include(x => x.IdEstatusNavigation)
                        .Include(x => x.IdMtrTipoMonedaNavigation)
                        .Where(x => x.IdVendedor == filter.UsuarioConectado.ToString() && x.Fecha >= fechaDesde && x.Fecha <= fechaHasta && x.SearchText.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .OrderByDescending(x => x.Fecha)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();

                }*/
                
                
                
                var totalRegistros = _context.ADM_SOLICITUDES.Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto).Count();

                var totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                
                var pageData = await _context.ADM_SOLICITUDES.DefaultIfEmpty()
                    .Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto)
                    .OrderByDescending(x => x.FECHA_SOLICITUD)
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToListAsync();
                List<AdmSolicitudesResponseDto> resultData = new List<AdmSolicitudesResponseDto>();
                foreach (var item in pageData)
                {
                    AdmSolicitudesResponseDto itemData = new AdmSolicitudesResponseDto();
                    itemData.CodigoSolicitud = item.CODIGO_SOLICITUD;
                    itemData.Ano = 0;
                    itemData.NumeroSolicitud = item.NUMERO_SOLICITUD;
                    itemData.FechaSolicitud = item.FECHA_SOLICITUD;
                    itemData.FechaSolicitudString = Fecha.GetFechaString(item.FECHA_SOLICITUD);
                    itemData.FechaSolicitudObj = Fecha.GetFechaDto(item.FECHA_SOLICITUD);
                    itemData.CodigoSolicitante = item.CODIGO_SOLICITANTE;
                    itemData.DenominacionSolicitante = "";
                    itemData.TipoSolicitudId = item.TIPO_SOLICITUD_ID;
                    itemData.DescripcionTipoSolicitud = "";
                    itemData.CodigoProveedor = item.CODIGO_PROVEEDOR;
                    itemData.NombreProveedor = "";
                    itemData.Motivo = item.MOTIVO.Trim();
                    itemData.Nota = item.NOTA;
                    itemData.DescripcionStatus = Estatus.GetStatus(item.STATUS);
                    itemData.CodigoPresupuesto = item.CODIGO_PRESUPUESTO;
                    
                    resultData.Add(itemData);
                }
                
               /* var linqQuery = from sol in pageData
                    join prov in _context.ADM_PROVEEDORES on sol.CODIGO_PROVEEDOR equals prov.CODIGO_PROVEEDOR
                    join descTipoSol in _context.ADM_DESCRIPTIVAS on sol.TIPO_SOLICITUD_ID equals descTipoSol.DESCRIPCION_ID
                 
                    select new AdmSolicitudesResponseDto() {
                        CodigoSolicitud = sol.CODIGO_SOLICITUD,
                        Ano = 0 ,
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
                        Nota = "",
                        DescripcionStatus=Estatus.GetStatus(sol.STATUS),
                        CodigoPresupuesto=sol.CODIGO_PRESUPUESTO
                        
                    };*/
                
                
             
                result.CantidadRegistros = totalRegistros;
                result.TotalPage = totalPage;
                result.Page = filter.PageNumber;
                result.IsValid = true;
                result.Message = "";
                result.Data = resultData;
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
