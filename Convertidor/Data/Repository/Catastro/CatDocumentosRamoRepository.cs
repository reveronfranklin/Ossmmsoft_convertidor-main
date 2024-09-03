﻿using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
    public class CatDocumentosRamoRepository : ICatDocumentosRamoRepository
    {
        private readonly DataContextCat _context;

        public CatDocumentosRamoRepository(DataContextCat context)
        {
            _context = context;
        }

        public async Task<List<CAT_DOCUMENTOS_RAMO>> GetAll()
        {
            try
            {
                var result = await _context.CAT_DOCUMENTOS_RAMO.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<CAT_DOCUMENTOS_RAMO>> Add(CAT_DOCUMENTOS_RAMO entity)
        {
            ResultDto<CAT_DOCUMENTOS_RAMO> result = new ResultDto<CAT_DOCUMENTOS_RAMO>(null);
            try
            {



                await _context.CAT_DOCUMENTOS_RAMO.AddAsync(entity);
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
                var last = await _context.CAT_DOCUMENTOS_RAMO.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_DOCU_RAMO)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_DOCU_RAMO + 1;
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
