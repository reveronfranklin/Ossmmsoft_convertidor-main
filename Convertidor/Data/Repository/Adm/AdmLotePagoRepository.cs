﻿using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.Sis;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Sis;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
	public class AdmLotePagoRepository: IAdmLotePagoRepository
    {
		
        private readonly DataContextAdm _context;

        public AdmLotePagoRepository(DataContextAdm context)
        {
            _context = context;
        }
      
        public async Task<ADM_LOTE_PAGO> GetByCodigo(int codigo)
        {
            try
            {
                var result = await _context.ADM_LOTE_PAGO.DefaultIfEmpty().Where(e => e.CODIGO_LOTE_PAGO == codigo).FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }
     
 
        public async Task<ResultDto<List<ADM_LOTE_PAGO>>> GetAll(AdmLotePagoFilterDto filter)
        {
            ResultDto<List<ADM_LOTE_PAGO>> result = new ResultDto<List<ADM_LOTE_PAGO>>(null);
            if (filter.PageNumber == 0) filter.PageNumber = 1;
            if (filter.PageSize == 0) filter.PageSize = 100;
            if (filter.PageSize >100) filter.PageSize = 100;

            if (string.IsNullOrEmpty(filter.SearchText))
            {
                filter.SearchText = "";
            }
            try
            {
                var totalRegistros = 0;
                var totalPage = 0;
              
                List<ADM_LOTE_PAGO> pageData = new List<ADM_LOTE_PAGO>();
                if ( filter.SearchText.Length==0)
                {
                    totalRegistros = _context.ADM_LOTE_PAGO
                        .Where(x=>x.CODIGO_EMPRESA==filter.CodigEmpresa && 
                                  x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto && 
                                  x.FECHA_PAGO.Date >= filter.FechaInicio.Value.Date && 
                                  x.FECHA_PAGO.Date <= filter.FechaFin.Value.Date)
                        .Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    
                    
                    pageData = await _context.ADM_LOTE_PAGO
                        .Where(x => x.CODIGO_EMPRESA == filter.CodigEmpresa && 
                                    x.CODIGO_PRESUPUESTO == filter.CodigoPresupuesto &&
                                    x.FECHA_PAGO.Date >= filter.FechaInicio.Value.Date &&
                                    x.FECHA_PAGO.Date <= filter.FechaFin.Value.Date)
                        .OrderByDescending(x => x.CODIGO_LOTE_PAGO)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                    
                    
                 
                }
                if ( filter.SearchText.Length>0)
                {
                    totalRegistros = _context.ADM_LOTE_PAGO
                        .Where(x => x.CODIGO_EMPRESA==filter.CodigEmpresa && x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto &&  x.FECHA_PAGO.Date >= filter.FechaInicio.Value.Date &&
                                    x.FECHA_PAGO.Date <= filter.FechaFin.Value.Date &&  x.SEARCH_TEXT.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .Count();

                    totalPage = (totalRegistros + filter.PageSize - 1) / filter.PageSize;
                    
                    pageData = await _context.ADM_LOTE_PAGO.DefaultIfEmpty()
                        .Where(x =>x.CODIGO_EMPRESA==filter.CodigEmpresa && x.CODIGO_PRESUPUESTO==filter.CodigoPresupuesto &&  x.FECHA_PAGO.Date >= filter.FechaInicio.Value.Date &&
                                   x.FECHA_PAGO.Date <= filter.FechaFin.Value.Date && x.SEARCH_TEXT.Trim().ToLower().Contains(filter.SearchText.Trim().ToLower()))
                        .OrderByDescending(x => x.CODIGO_LOTE_PAGO)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
                }
                
                result.CantidadRegistros = totalRegistros;
                result.TotalPage = totalPage;
                result.Page = filter.PageNumber;
                result.IsValid = true;
                result.Message = "";
                result.Data = pageData;
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

        public async Task<string> GetSearchTextPagos(int codigoLote)
        {
            string result=string.Empty;
            try
            {
                var pagos =await  _context.ADM_CHEQUES.Where(x => x.CODIGO_LOTE_PAGO ==codigoLote)
                    .ToListAsync();
                if (pagos.Count() > 0)
                {
                    foreach (var item in pagos)
                    {
                        string nombreProveedor= string.Empty;
                        var proveedor = await _context.ADM_PROVEEDORES
                            .Where(X => X.CODIGO_PROVEEDOR == item.CODIGO_PROVEEDOR).FirstOrDefaultAsync();
                        if (proveedor != null)
                        {
                            if (proveedor.NOMBRE_PROVEEDOR is null) proveedor.NOMBRE_PROVEEDOR = "";
                            nombreProveedor = proveedor.NOMBRE_PROVEEDOR;
                        }
                        result = $"{result}{item.NUMERO_CHEQUE}{item.CODIGO_PROVEEDOR}{nombreProveedor}";
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
   
            return result;
                 
        }

        public async Task<string> UpdateSearchText(int codigoLote)
        {

            string result = string.Empty;
            
            try
            {

                if (codigoLote == 8386)
                {
                    var a = 1;
                }
                var searchTextPagos = await GetSearchTextPagos(codigoLote);

             
                
                FormattableString xqueryDiario = $"UPDATE ADM.ADM_LOTE_PAGO SET ADM.ADM_LOTE_PAGO.SEARCH_TEXT = (SELECT DESCRIPCION FROM ADM.ADM_DESCRIPTIVAS    WHERE ADM.ADM_DESCRIPTIVAS.DESCRIPCION_ID  =ADM.ADM_LOTE_PAGO.TIPO_PAGO_ID) || (SELECT NO_CUENTA FROM SIS.SIS_CUENTAS_BANCOS  WHERE SIS.SIS_CUENTAS_BANCOS.CODIGO_CUENTA_BANCO  =ADM.ADM_LOTE_PAGO.CODIGO_CUENTA_BANCO) || (SELECT CODIGO_BANCO FROM SIS.SIS_CUENTAS_BANCOS  WHERE SIS.SIS_CUENTAS_BANCOS.CODIGO_CUENTA_BANCO  =ADM.ADM_LOTE_PAGO.CODIGO_CUENTA_BANCO) || (SELECT NOMBRE FROM SIS.SIS_BANCOS WHERE SIS.SIS_BANCOS.CODIGO_BANCO=(SELECT CODIGO_BANCO FROM SIS.SIS_CUENTAS_BANCOS  WHERE SIS.SIS_CUENTAS_BANCOS.CODIGO_CUENTA_BANCO  =ADM.ADM_LOTE_PAGO.CODIGO_CUENTA_BANCO)) || ADM.ADM_LOTE_PAGO.TITULO || { searchTextPagos}  WHERE CODIGO_LOTE_PAGO ={codigoLote}";

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                var lote = await GetByCodigo(codigoLote);
                if (lote != null)
                {
                    result=lote.SEARCH_TEXT;
                    return result;
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }




        }


        public async Task ReconstruirSearchTextPago(int codigoPresupuesto)
        {
            var lotes =await  _context.ADM_LOTE_PAGO.Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuesto).ToListAsync();
            if (lotes.Count() > 0)
            {
                foreach (var item in lotes)
                {
                  await  UpdateSearchText(item.CODIGO_LOTE_PAGO);
                }
            }
        }
        
        public async Task<ResultDto<ADM_LOTE_PAGO>> Add(ADM_LOTE_PAGO entity)
        {
            ResultDto<ADM_LOTE_PAGO> result = new ResultDto<ADM_LOTE_PAGO>(null);
            try
            {



                await _context.ADM_LOTE_PAGO.AddAsync(entity);
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

        public async Task<ResultDto<ADM_LOTE_PAGO>> Update(ADM_LOTE_PAGO entity)
        {
            ResultDto<ADM_LOTE_PAGO> result = new ResultDto<ADM_LOTE_PAGO>(null);

            try
            {
                ADM_LOTE_PAGO entityUpdate = await GetByCodigo(entity.CODIGO_LOTE_PAGO);
                if (entityUpdate != null)
                {


                    _context.ADM_LOTE_PAGO.Update(entity);
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

        public async Task<string> Delete(int id)
        {

            try
            {
                ADM_LOTE_PAGO entity = await GetByCodigo(id);
                if (entity != null)
                {
                    _context.ADM_LOTE_PAGO.Remove(entity);
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
                var last = await _context.ADM_LOTE_PAGO.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_LOTE_PAGO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_LOTE_PAGO + 1;
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

