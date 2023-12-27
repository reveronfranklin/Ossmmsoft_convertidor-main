using System;
using System;
using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Adm
{
	public class AdmDescriptivaRepository: IAdmDescriptivaRepository
    {
		
        private readonly DataContextAdm _context;

        public AdmDescriptivaRepository(DataContextAdm context)
        {
            _context = context;
        }
        public async Task<ADM_DESCRIPTIVAS> GetByCodigoDescriptiva(int descripcionId)
        {
            try
            {
                var result = await _context.ADM_DESCRIPTIVAS.DefaultIfEmpty().Where(e => e.DESCRIPCION_ID == descripcionId).FirstOrDefaultAsync();
        
                return (ADM_DESCRIPTIVAS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<ADM_DESCRIPTIVAS> GetByCodigoDescriptivaTexto(string codigo)
        {
            try
            {
                var result = await _context.ADM_DESCRIPTIVAS.DefaultIfEmpty().Where(e => e.CODIGO == codigo).FirstOrDefaultAsync();

                return (ADM_DESCRIPTIVAS)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<ADM_DESCRIPTIVAS>> GetByTitulo(int tituloId)
        {
            try
            {
                var result = await _context.ADM_DESCRIPTIVAS.DefaultIfEmpty().Where(e => e.TITULO_ID == tituloId).ToListAsync();

                return (List<ADM_DESCRIPTIVAS>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<ADM_DESCRIPTIVAS>> GetAll()
        {
            try
            {
                var result = await _context.ADM_DESCRIPTIVAS.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


        public async Task<ADM_DESCRIPTIVAS> GetByCodigo(int descripcionId)
        {
            try
            {

                var result = await _context.ADM_DESCRIPTIVAS.DefaultIfEmpty()
                    .Where(x => x.DESCRIPCION_ID == descripcionId)
                    .FirstOrDefaultAsync();
                return (ADM_DESCRIPTIVAS)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }



        }


        public async Task<List<ADM_DESCRIPTIVAS>> GetByFKID(int descripcionIdFk)
        {
            try
            {

                var result = await _context.ADM_DESCRIPTIVAS.DefaultIfEmpty()
                    .Where(x => x.DESCRIPCION_FK_ID== descripcionIdFk)
                    .ToListAsync();
                return (List<ADM_DESCRIPTIVAS>)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }



        }



        public async Task<ResultDto<ADM_DESCRIPTIVAS>> Add(ADM_DESCRIPTIVAS entity)
        {
            ResultDto<ADM_DESCRIPTIVAS> result = new ResultDto<ADM_DESCRIPTIVAS>(null);
            try
            {



                await _context.ADM_DESCRIPTIVAS.AddAsync(entity);
                _context.SaveChanges();


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

        public async Task<ResultDto<ADM_DESCRIPTIVAS>> Update(ADM_DESCRIPTIVAS entity)
        {
            ResultDto<ADM_DESCRIPTIVAS> result = new ResultDto<ADM_DESCRIPTIVAS>(null);

            try
            {
                ADM_DESCRIPTIVAS entityUpdate = await GetByCodigo(entity.DESCRIPCION_ID);
                if (entityUpdate != null)
                {


                    _context.ADM_DESCRIPTIVAS.Update(entity);
                    _context.SaveChanges();
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

        public async Task<string> Delete(int descripcionId)
        {

            try
            {
                ADM_DESCRIPTIVAS entity = await GetByCodigo(descripcionId);
                if (entity != null)
                {
                    _context.ADM_DESCRIPTIVAS.Remove(entity);
                    _context.SaveChanges();
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
                var last = await _context.ADM_DESCRIPTIVAS.DefaultIfEmpty()
                    .OrderByDescending(x => x.DESCRIPCION_ID)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.DESCRIPCION_ID + 1;
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

