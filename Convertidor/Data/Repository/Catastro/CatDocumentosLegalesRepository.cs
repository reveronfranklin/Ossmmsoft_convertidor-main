﻿using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
    public class CatDocumentosLegalesRepository : ICatDocumentosLegalesRepository
    {
        private readonly DataContextCat _context;

        public CatDocumentosLegalesRepository(DataContextCat context)
        {
            _context = context;
        }

        public async Task<List<CAT_DOCUMENTOS_LEGALES>> GetAll()
        {
            try
            {
                var result = await _context.CAT_DOCUMENTOS_LEGALES.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<CAT_DOCUMENTOS_LEGALES>> Add(CAT_DOCUMENTOS_LEGALES entity)
        {
            ResultDto<CAT_DOCUMENTOS_LEGALES> result = new ResultDto<CAT_DOCUMENTOS_LEGALES>(null);
            try
            {



                await _context.CAT_DOCUMENTOS_LEGALES.AddAsync(entity);
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
                var last = await _context.CAT_DOCUMENTOS_LEGALES.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_DOCUMENTOS_LEGALES)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_DOCUMENTOS_LEGALES + 1;
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
