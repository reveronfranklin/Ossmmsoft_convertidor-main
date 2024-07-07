﻿using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Cnt
{
    public class CntSaldosRepository : ICntSaldosRepository
    {
        private readonly DataContextCnt _context;

        public CntSaldosRepository(DataContextCnt context)
        {
            _context = context;
        }

        public async Task<List<CNT_SALDOS>> GetAll()
        {
            try
            {
                var result = await _context.CNT_SALDOS.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<CNT_SALDOS>> Add(CNT_SALDOS entity)
        {

            ResultDto<CNT_SALDOS> result = new ResultDto<CNT_SALDOS>(null);
            try
            {
                await _context.CNT_SALDOS.AddAsync(entity);
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

        public async Task<int> GetNextKey()
        {
            try
            {
                int result = 0;
                var last = await _context.CNT_SALDOS.DefaultIfEmpty()
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
    }
}
