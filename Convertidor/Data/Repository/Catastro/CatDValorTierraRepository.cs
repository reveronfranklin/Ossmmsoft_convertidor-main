﻿using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
    public class CatDValorTierraRepository : ICatDValorTierraRepository
    {
        private readonly DataContextCat _context;

        public CatDValorTierraRepository(DataContextCat context)
        {
            _context = context;
        }

        public async Task<List<CAT_D_VALOR_TIERRA>> GetAll()
        {
            try
            {
                var result = await _context.CAT_D_VALOR_TIERRA.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<CAT_D_VALOR_TIERRA>> Add(CAT_D_VALOR_TIERRA entity)
        {
            ResultDto<CAT_D_VALOR_TIERRA> result = new ResultDto<CAT_D_VALOR_TIERRA>(null);
            try
            {



                await _context.CAT_D_VALOR_TIERRA.AddAsync(entity);
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
                var last = await _context.CAT_D_VALOR_TIERRA.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_VALOR_TIERRA)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_VALOR_TIERRA + 1;
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
