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
      
       
        public async Task<string> UpdateMontoEnLetras(int codigoSolicitud,decimal monto)
        {

            try
            {
                FormattableString xqueryDiario = $"UPDATE ADM_SOLICITUDES  SET MONTO_LETRAS= UPPER(SIS.SIS_MONTOESCRITO({monto},2)) WHERE ADM_SOLICITUDES.CODIGO_SOLICITUD ={codigoSolicitud}";

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);


                xqueryDiario=
                    $"UPDATE ADM_DETALLE_SOLICITUD SET DESCRIPCION = REPLACE(DESCRIPCION , CHR(10), '')   WHERE ADM_DETALLE_SOLICITUD.CODIGO_SOLICITUD ={codigoSolicitud}";
                resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                
                return "";
            }
            catch (Exception ex)
            {
                    return ex.Message;
            }




        }
        
        public async Task<string> LimpiaCaractereDetalle(int codigoSolicitud)
        {

            try
            {
           

                FormattableString xqueryDiario = 
                    $"UPDATE ADM_DETALLE_SOLICITUD SET DESCRIPCION = REPLACE(DESCRIPCION , CHR(10), '')   WHERE ADM_DETALLE_SOLICITUD.CODIGO_SOLICITUD ={codigoSolicitud}";
                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }




        }
        
        public async Task<string> UpdateSearchText(int codigoPresupuesto)
        {

            try
            {
                FormattableString xqueryDiario = $"UPDATE ADM.ADM_SOLICITUDES SET ADM.ADM_SOLICITUDES.SEARCH_TEXT = TRIM(NUMERO_SOLICITUD) || STATUS || TRIM(MOTIVO) || (SELECT DENOMINACION FROM PRE.PRE_INDICE_CAT_PRG WHERE PRE.PRE_INDICE_CAT_PRG.CODIGO_ICP  = ADM.ADM_SOLICITUDES.CODIGO_SOLICITANTE) || (SELECT DESCRIPCION FROM ADM.ADM_DESCRIPTIVAS    WHERE ADM.ADM_DESCRIPTIVAS.DESCRIPCION_ID  = ADM.ADM_SOLICITUDES.TIPO_SOLICITUD_ID) || (SELECT NOMBRE_PROVEEDOR FROM ADM.ADM_PROVEEDORES   WHERE  ADM.ADM_PROVEEDORES.CODIGO_PROVEEDOR  =ADM.ADM_SOLICITUDES.CODIGO_PROVEEDOR) WHERE CODIGO_PRESUPUESTO ={codigoPresupuesto}";

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }




        }
        
        public async Task<ResultDto<List<AdmSolicitudesResponseDto>>> GetByPresupuesto(AdmSolicitudesFilterDto filter) 
        {
            ResultDto<List<AdmSolicitudesResponseDto>> result = new ResultDto<List<AdmSolicitudesResponseDto>>(null);

            if (filter.PageNumber == 0) filter.PageNumber = 1;
            if (filter.PageSize == 0) filter.PageSize = 100;
            if (filter.PageSize >100) filter.PageSize = 100;

            if (string.IsNullOrEmpty(filter.SearchText))
            {
                filter.SearchText = "";
            }
            if (string.IsNullOrEmpty(filter.Status))
            {
                filter.Status = "";
            }
            try
            {

                var updateSearchText = await UpdateSearchText(filter.CodigoPresupuesto);
                var totalRegistros = 0;
                var totalPage = 0;
              
                List<ADM_SOLICITUDES> pageData = new List<ADM_SOLICITUDES>();
                
                if (filter.Status.Length > 0 && filter.SearchText.Length==0)
                {
                    totalRegistros = _context.ADM_SOLICITUDES
                        .Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto && x.STATUS==filter.Status )
                        .Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    
                    pageData = await _context.ADM_SOLICITUDES.DefaultIfEmpty()
                        .Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto &&   x.STATUS==filter.Status )
                        .OrderByDescending(x => x.FECHA_SOLICITUD)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
                if (filter.Status.Length >  0 && filter.SearchText.Length>0)
                {
                    totalRegistros = _context.ADM_SOLICITUDES
                        .Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto && x.STATUS==filter.Status && x.SEARCH_TEXT.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    
                    pageData = await _context.ADM_SOLICITUDES.DefaultIfEmpty()
                        .Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto &&   x.STATUS==filter.Status && x.SEARCH_TEXT.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .OrderByDescending(x => x.FECHA_SOLICITUD)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
                if (filter.SearchText.Length > 0 && filter.Status.Length==0)
                {
                    totalRegistros = _context.ADM_SOLICITUDES
                        .Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto && x.SEARCH_TEXT.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    
                    pageData = await _context.ADM_SOLICITUDES.DefaultIfEmpty()
                        .Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto && x.SEARCH_TEXT.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .OrderByDescending(x => x.FECHA_SOLICITUD)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
                if (filter.SearchText.Length == 0 && filter.Status.Length==0)
                {
                    totalRegistros = _context.ADM_SOLICITUDES.Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto).Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    pageData = await _context.ADM_SOLICITUDES.DefaultIfEmpty()
                        .Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto)
                        .OrderByDescending(x => x.FECHA_SOLICITUD)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
             
                
                
                
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
                    itemData.Status = item.STATUS;
                    itemData.DescripcionStatus = Estatus.GetStatus(item.STATUS);
                    itemData.CodigoPresupuesto = item.CODIGO_PRESUPUESTO;
                    if (item.FIRMANTE == null) item.FIRMANTE = "";
                    itemData.Firmante = item.FIRMANTE;
                    if (item.MONTO_LETRAS == null) item.MONTO_LETRAS = "";
                    itemData.MontoLetras = item.MONTO_LETRAS;
                    
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


        
        
        public async Task<ResultDto<List<AdmSolicitudesResponseDto>>> GetByPresupuestoPendientes(AdmSolicitudesFilterDto filter) 
        {
            ResultDto<List<AdmSolicitudesResponseDto>> result = new ResultDto<List<AdmSolicitudesResponseDto>>(null);

            if (filter.PageNumber == 0) filter.PageNumber = 1;
            if (filter.PageSize == 0) filter.PageSize = 100;
            if (filter.PageSize >100) filter.PageSize = 100;
            
            try
            {

                var updateSearchText = await UpdateSearchText(filter.CodigoPresupuesto);
                var totalRegistros = 0;
                var totalPage = 0;
              
                List<ADM_SOLICITUDES> pageData;
                if (filter.SearchText.Length > 0)
                {
                    totalRegistros = _context.ADM_SOLICITUDES
                        .Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto && x.STATUS=="PE" && x.SEARCH_TEXT.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    
                    pageData = await _context.ADM_SOLICITUDES.DefaultIfEmpty()
                        .Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto && x.STATUS=="PE" && x.SEARCH_TEXT.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .OrderByDescending(x => x.FECHA_SOLICITUD)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
                else
                {
                    totalRegistros = _context.ADM_SOLICITUDES.Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto && x.STATUS=="PE").Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    pageData = await _context.ADM_SOLICITUDES.DefaultIfEmpty()
                        .Where(x =>x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto && x.STATUS=="PE")
                        .OrderByDescending(x => x.FECHA_SOLICITUD)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
             
                
                
                
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
                    itemData.Status = item.STATUS;
                    itemData.DescripcionStatus = Estatus.GetStatus(item.STATUS);
                    itemData.CodigoPresupuesto = item.CODIGO_PRESUPUESTO;
                    if (item.FIRMANTE == null) item.FIRMANTE = "";
                    itemData.Firmante = item.FIRMANTE;
                    if (item.MONTO_LETRAS == null) item.MONTO_LETRAS = "";
                    itemData.MontoLetras = item.MONTO_LETRAS;
                    
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
