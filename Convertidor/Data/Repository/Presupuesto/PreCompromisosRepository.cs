
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Utility;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
	public class PreCompromisosRepository: IPreCompromisosRepository
    {
		
        private readonly DataContextPre _context;
        private readonly IAdmProveedoresRepository _admProveedoresRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;


        public PreCompromisosRepository(
                                        DataContextPre context,
                                        IAdmProveedoresRepository admProveedoresRepository,
                                        ISisUsuarioRepository sisUsuarioRepository )
        {
            _context = context;
            _admProveedoresRepository = admProveedoresRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }
      
        
        
        public async Task<string> AnularDesdeSolicitud(int codigoSolicitud)
        {

            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
                FormattableString xqueryAnulaSolicitud = $"UPDATE ADM.ADM_SOLICITUDES SET ADM.ADM_SOLICITUDES.STATUS='AN',USUARIO_UPD = { conectado.Usuario},FECHA_UPD = SYSDATE   WHERE CODIGO_SOLICITUD ={codigoSolicitud}";
                var resultXqueryAnulaSolicitud = _context.Database.ExecuteSqlInterpolated(xqueryAnulaSolicitud);
                
                FormattableString xqueryAdmPucSolicitud = $"UPDATE ADM.ADM_PUC_SOLICITUD SET MONTO_ANULADO = MONTO,MONTO_COMPROMETIDO = 0,USUARIO_UPD = { conectado.Usuario},FECHA_UPD = SYSDATE  WHERE CODIGO_SOLICITUD ={codigoSolicitud}";
                var resultAdmPucSolicitud = _context.Database.ExecuteSqlInterpolated(xqueryAdmPucSolicitud);
                
                //FormattableString xqueryPreCompromiso = $"UPDATE PRE.PRE_COMPROMISO SET PRE.PRE_COMPROMISOS.STATUS = 'AN',USUARIO_UPD = { conectado.Usuario},FECHA_UPD = SYSDATE  WHERE CODIGO_SOLICITUD ={codigoSolicitud}";
                //var resultPreCompromiso = _context.Database.ExecuteSqlInterpolated(xqueryAdmPucSolicitud);

                
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }




        }
        
        public async Task<string> AnularCompromiso(int codigoCompromiso,int codigoSolicitud)
        {

            try
            {
                
           
                
                
                var conectado = await _sisUsuarioRepository.GetConectado();
                FormattableString xqueryAnulaSolicitud =  $"UPDATE ADM.ADM_PUC_SOLICITUD SET MONTO_COMPROMETIDO = MONTO,USUARIO_UPD = { conectado.Usuario},FECHA_UPD = SYSDATE WHERE CODIGO_SOLICITUD = {codigoSolicitud}";
                var resultXqueryAnulaSolicitud = _context.Database.ExecuteSqlInterpolated(xqueryAnulaSolicitud);
                
                FormattableString xqueryPrePucCompromiso = $"UPDATE PRE.PRE_PUC_COMPROMISOS SET MONTO_ANULADO = MONTO,USUARIO_UPD = { conectado.Usuario},FECHA_UPD = SYSDATE WHERE CODIGO_COMPROMISO = {codigoCompromiso}";
                var resultAdmPucSolicitud = _context.Database.ExecuteSqlInterpolated(xqueryPrePucCompromiso);
                
                FormattableString xqueryPreDetalleCompromiso = $"UPDATE PRE.PRE_DETALLE_COMPROMISOS SET CANTIDAD_ANULADA =  CANTIDAD,USUARIO_UPD = { conectado.Usuario},FECHA_UPD = SYSDATEWHERE CODIGO_COMPROMISO = {codigoCompromiso}";
                var resultPreDetalleCompromiso = _context.Database.ExecuteSqlInterpolated(xqueryPreDetalleCompromiso);

                
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }




        }
        
        public async Task<PRE_COMPROMISOS> GetByCodigo(int codigoCompromiso)
        {
            try
            {
                var result = await _context.PRE_COMPROMISOS.DefaultIfEmpty().Where(e => e.CODIGO_COMPROMISO == codigoCompromiso).FirstOrDefaultAsync();

                return (PRE_COMPROMISOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
        public async Task<PRE_COMPROMISOS> GetByCodigoSolicitud(int codigoSolicitud)
        {
            try
            {
                var result = await _context.PRE_COMPROMISOS.DefaultIfEmpty().Where(e => e.CODIGO_SOLICITUD == codigoSolicitud).OrderByDescending(x=>x.CODIGO_COMPROMISO).FirstOrDefaultAsync();

                return (PRE_COMPROMISOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<PRE_COMPROMISOS> GetByNumeroYFecha(string numeroCompromiso,DateTime fechaCompromiso)
        {
            try
            {
                var result = await _context.PRE_COMPROMISOS.DefaultIfEmpty().Where(e => e.NUMERO_COMPROMISO == numeroCompromiso && e.FECHA_COMPROMISO == fechaCompromiso).FirstOrDefaultAsync();

                return (PRE_COMPROMISOS)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }


        public async Task<List<PRE_COMPROMISOS>> GetAll()
        {
            try
            {
                var result = await _context.PRE_COMPROMISOS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<string> UpdateSearchText(int codigoPresupuesto)
        {

            try
            {
                FormattableString xqueryDiario = $"UPDATE PRE.PRE_COMPROMISOS SET PRE.PRE_COMPROMISOS.SEARCH_TEXT = TRIM(NUMERO_COMPROMISO) || STATUS || TRIM(MOTIVO)  || (SELECT NOMBRE_PROVEEDOR FROM ADM.ADM_PROVEEDORES   WHERE  ADM.ADM_PROVEEDORES.CODIGO_PROVEEDOR  =PRE.PRE_COMPROMISOS.CODIGO_PROVEEDOR) WHERE CODIGO_PRESUPUESTO ={codigoPresupuesto}";

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }




        }
        
        
        
        public async Task<string> AprobarCompromiso(int codigoCompromiso)
        {

            try
            {
                FormattableString xqueryDiario = $"UPDATE PRE.PRE_COMPROMISOS SET PRE.PRE_COMPROMISOS.STATUS = 'AP' WHERE CODIGO_COMPROMISO ={codigoCompromiso}";

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }




        }
        
        
        public async Task<ResultDto<List<PreCompromisosResponseDto>>> GetByPresupuesto(PreCompromisosFilterDto filter) 
        {
            ResultDto<List<PreCompromisosResponseDto>> result = new ResultDto<List<PreCompromisosResponseDto>>(null);

            if (filter.PageNumber == 0) filter.PageNumber = 1;
            if (filter.PageSize == 0) filter.PageSize = 100;
            if (filter.PageSize >100) filter.PageSize = 100;
            if (filter.SearchText == null) filter.SearchText = "";
            if (filter.Status == null) filter.Status = "PE";
            try
            {
                var presupuesto = await _context.PRE_PRESUPUESTOS
                    .Where(x => x.CODIGO_PRESUPUESTO == filter.CodigoPresupuesto ).FirstOrDefaultAsync();
                var updateSearchText = await UpdateSearchText(filter.CodigoPresupuesto);
                var totalRegistros = 0;
                var totalPage = 0;
              
                List<PRE_COMPROMISOS> pageData = new List<PRE_COMPROMISOS>();
                if (filter.SearchText.Length > 0 && filter.Status.Length>0)
                {
                    totalRegistros = _context.PRE_COMPROMISOS
                        .Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto && x.STATUS==filter.Status && x.SEARCH_TEXT.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    
                    pageData = await _context.PRE_COMPROMISOS.DefaultIfEmpty()
                        .Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto  && x.STATUS==filter.Status && x.SEARCH_TEXT.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .OrderByDescending(x => x.FECHA_COMPROMISO)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
                if (filter.SearchText.Length == 0 && filter.Status.Length>0)
                {
                    totalRegistros = _context.PRE_COMPROMISOS.Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto  && x.STATUS==filter.Status).Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    pageData = await _context.PRE_COMPROMISOS.DefaultIfEmpty()
                        .Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto  && x.STATUS==filter.Status)
                        .OrderByDescending(x => x.FECHA_COMPROMISO)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
                if (filter.SearchText.Length == 0 && filter.Status.Length==0)
                {
                    totalRegistros = _context.PRE_COMPROMISOS.Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto ).Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    pageData = await _context.PRE_COMPROMISOS.DefaultIfEmpty()
                        .Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto  )
                        .OrderByDescending(x => x.FECHA_COMPROMISO)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
                
                List<PreCompromisosResponseDto> resultData = new List<PreCompromisosResponseDto>();
                foreach (var item in pageData)
                {
                    PreCompromisosResponseDto itemData = new PreCompromisosResponseDto();
                    itemData.CodigoCompromiso = item.CODIGO_SOLICITUD;
                    itemData.Ano = presupuesto.ANO;
                    itemData.CodigoSolicitud = item.CODIGO_SOLICITUD;
                    itemData.NumeroCompromiso = item.NUMERO_COMPROMISO;
                    itemData.FechaCompromiso = item.FECHA_COMPROMISO;
                    itemData.FechaCompromisoString = Fecha.GetFechaString(item.FECHA_COMPROMISO);
                    itemData.FechaCompromisoObj = Fecha.GetFechaDto(item.FECHA_COMPROMISO);
                    itemData.CodigoProveedor = item.CODIGO_PROVEEDOR;
                    itemData.Status = item.STATUS;
                    itemData.DescripcionStatus = Estatus.GetStatus(item.STATUS);
                    itemData.NombreProveedor = "";
                    var proveedor = await _admProveedoresRepository.GetByCodigo(item.CODIGO_PROVEEDOR);
                    if (proveedor != null)
                    {
                        itemData.NombreProveedor = proveedor.NOMBRE_PROVEEDOR;
                    }
                    itemData.Motivo = item.MOTIVO.Trim();
                  
                    itemData.CodigoPresupuesto = item.CODIGO_PRESUPUESTO;
                    itemData.CodigoDirEntrega = item.CODIGO_DIR_ENTREGA;

        
                    

                    
                    resultData.Add(itemData);
                }
                
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


        public async Task<ResultDto<PRE_COMPROMISOS>> Add(PRE_COMPROMISOS entity)
        {
            ResultDto<PRE_COMPROMISOS> result = new ResultDto<PRE_COMPROMISOS>(null);
            try
            {



                await _context.PRE_COMPROMISOS.AddAsync(entity);
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

        public async Task<ResultDto<PRE_COMPROMISOS>> Update(PRE_COMPROMISOS entity)
        {
            ResultDto<PRE_COMPROMISOS> result = new ResultDto<PRE_COMPROMISOS>(null);

            try
            {
                PRE_COMPROMISOS entityUpdate = await GetByCodigo(entity.CODIGO_COMPROMISO);
                if (entityUpdate != null)
                {


                    _context.PRE_COMPROMISOS.Update(entity);
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

        public async Task<string> Delete(int codigoCompromiso)
        {

            try
            {
                PRE_COMPROMISOS entity = await GetByCodigo(codigoCompromiso);
                if (entity != null)
                {
                    _context.PRE_COMPROMISOS.Remove(entity);
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
                var last = await _context.PRE_COMPROMISOS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_COMPROMISO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_COMPROMISO + 1;
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

