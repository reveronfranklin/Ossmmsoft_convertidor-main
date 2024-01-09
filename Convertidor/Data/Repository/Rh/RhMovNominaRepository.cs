using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class RhMovNominaRepository: IRhMovNominaRepository
    {
		
        private readonly DataContext _context;

        public RhMovNominaRepository(DataContext context)
        {
            _context = context;
        }
      
        public async Task<RH_MOV_NOMINA> GetByCodigo(int codigoMovNomina)
        {
            try
            {
                var result = await _context.RH_MOV_NOMINA.DefaultIfEmpty().Where(e => e.CODIGO_MOV_NOMINA == codigoMovNomina).FirstOrDefaultAsync();

                return (RH_MOV_NOMINA)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }


        public async Task<RH_MOV_NOMINA> GetByTipoNominaPersonaConcepto(int codigoTipoNomina,int codigoPersona,int codigoConcepto)
        {
            try
            {
                var result = await _context.RH_MOV_NOMINA.DefaultIfEmpty()
                            .Where(e => e.CODIGO_TIPO_NOMINA== codigoTipoNomina && e.CODIGO_PERSONA==codigoPersona && e.CODIGO_CONCEPTO==codigoConcepto)
                            .FirstOrDefaultAsync();

                return (RH_MOV_NOMINA)result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }


        public async Task<List<RH_MOV_NOMINA>> GetAll()
        {
            try
            {
                var result = await _context.RH_MOV_NOMINA.DefaultIfEmpty().ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


     
      



        public async Task<ResultDto<RH_MOV_NOMINA>> Add(RH_MOV_NOMINA entity)
        {
            ResultDto<RH_MOV_NOMINA> result = new ResultDto<RH_MOV_NOMINA>(null);
            try
            {



                await _context.RH_MOV_NOMINA.AddAsync(entity);
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

        public async Task<ResultDto<RH_MOV_NOMINA>> Update(RH_MOV_NOMINA entity)
        {
            ResultDto<RH_MOV_NOMINA> result = new ResultDto<RH_MOV_NOMINA>(null);

            try
            {
                RH_MOV_NOMINA entityUpdate = await GetByCodigo(entity.CODIGO_MOV_NOMINA);
                if (entityUpdate != null)
                {


                    _context.RH_MOV_NOMINA.Update(entity);
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

        public async Task<string> Delete(int codigoMovNomina)
        {

            try
            {
                RH_MOV_NOMINA entity = await GetByCodigo(codigoMovNomina);
                if (entity != null)
                {
                    _context.RH_MOV_NOMINA.Remove(entity);
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
                var last = await _context.RH_MOV_NOMINA.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_MOV_NOMINA)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_MOV_NOMINA + 1;
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

