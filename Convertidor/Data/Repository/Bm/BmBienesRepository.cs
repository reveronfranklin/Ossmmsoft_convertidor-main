using System;
using System;
using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Dtos;
using Convertidor.Dtos.Bm;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;

namespace Convertidor.Data.Repository.Bm
{
    public class BmBienesRepository: IBmBienesRepository
    {
		
        private readonly DataContextBm _context;

        public BmBienesRepository(DataContextBm context)
        {
            _context = context;
        }
        public async Task<BM_BIENES> GetByCodigoBien(int codigoBien)
        {
            try
            {
                var result = await _context.BM_BIENES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_BIEN == codigoBien).FirstOrDefaultAsync();
        
                return (BM_BIENES)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<BM_BIENES> GetByCodigoArticulo(int codigoArticulo)
        {
            try
            {
                var result = await _context.BM_BIENES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_ARTICULO == codigoArticulo).FirstOrDefaultAsync();

                return (BM_BIENES)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<BM_BIENES>> GetByProveedor(int codigoProveedor)
        {
            try
            {
                var result = await _context.BM_BIENES.DefaultIfEmpty().
                    Where(e => e.CODIGO_PROVEEDOR == codigoProveedor).ToListAsync();

                return (List<BM_BIENES>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<BM_BIENES>> GetByOrdenCompra(int codigoOrdenCompra)
        {
            try
            {
                var result = await _context.BM_BIENES.DefaultIfEmpty()
                    .Where(e => e.CODIGO_ORDEN_COMPRA == codigoOrdenCompra).ToListAsync();

                return (List<BM_BIENES>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<BM_BIENES> GetByNumeroPlaca(string numeroPlaca)
        {
            try
            {
                var result = await _context.BM_BIENES.DefaultIfEmpty()
                    .Where(e => e.NUMERO_PLACA == numeroPlaca)
                    .FirstOrDefaultAsync();

                return (BM_BIENES)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<BM_BIENES>> GetAll()
        {
            try
            {
                var result = await _context.BM_BIENES.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


        public async Task<ResultDto<BM_BIENES>> Add(BM_BIENES entity)
        {
            ResultDto<BM_BIENES> result = new ResultDto<BM_BIENES>(null);
            try
            {



                await _context.BM_BIENES.AddAsync(entity);
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

        public async Task<ResultDto<BM_BIENES>> Update(BM_BIENES entity)
        {
            ResultDto<BM_BIENES> result = new ResultDto<BM_BIENES>(null);

            try
            {
                BM_BIENES entityUpdate = await GetByCodigoBien(entity.CODIGO_BIEN);
                if (entityUpdate != null)
                {


                    _context.BM_BIENES.Update(entity);
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

        public async Task<string> Delete(int codigoBien)
        {

            try
            {
                BM_BIENES entity = await GetByCodigoBien(codigoBien);
                if (entity != null)
                {
                    _context.BM_BIENES.Remove(entity);
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
                var last = await _context.BM_BIENES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_BIEN)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_BIEN + 1;
                }

                return (int)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }



        }

        //public async Task<string> GetNextKeyNumeroPlaca()
        //{
        //    try
        //    {
                
        //        string result = "";
        //        var last = await _context.BM_BIENES.DefaultIfEmpty()
        //            .OrderByDescending(x => x.NUMERO_PLACA)
        //            .FirstOrDefaultAsync();
                
        //        if (last == null)
        //        {
        //            result = "1";
        //        }
        //        else
        //        {
                    
        //            result = last.NUMERO_PLACA +1 ;
                     
        //        }

        //        return  result;
                

        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message;
                
        //        return "";
        //    }



        }
    }


