using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class RhTipoNominaRepository: IRhTipoNominaRepository
    {
		
        private readonly DataContext _context;

        public RhTipoNominaRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<RH_TIPOS_NOMINA>> GetAll()
        {
            try
            {

                var result = await _context.RH_TIPOS_NOMINA.DefaultIfEmpty().Where(t=>t.DESCRIPCION!= null).OrderBy(t=>t.DESCRIPCION).ToListAsync();
                return (List<RH_TIPOS_NOMINA>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<RH_TIPOS_NOMINA> GetByCodigo(int codigoTipoNomina)
        {
            try
            {

                var result = await _context.RH_TIPOS_NOMINA.DefaultIfEmpty().Where(t => t.CODIGO_TIPO_NOMINA == codigoTipoNomina).FirstOrDefaultAsync();
                return (RH_TIPOS_NOMINA)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<RH_TIPOS_NOMINA>> GetTipoNominaByCodigoPersona(int codigoPersona,DateTime desde,DateTime hasta)
        {
            try
            {

                List<RH_V_HISTORICO_MOVIMIENTOS> historico = new List<RH_V_HISTORICO_MOVIMIENTOS>();

                if (codigoPersona > 0)
                {
                    historico = await _context.RH_V_HISTORICO_MOVIMIENTOS.DefaultIfEmpty().Where(h => h.CODIGO_PERSONA == codigoPersona && h.FECHA_NOMINA_MOV >= desde && h.FECHA_NOMINA_MOV <= hasta).OrderByDescending(h => h.FECHA_NOMINA_MOV).ThenBy(h => h.CEDULA).ToListAsync();

                }
                else
                {
                    historico = await _context.RH_V_HISTORICO_MOVIMIENTOS.DefaultIfEmpty().Where(h => h.FECHA_NOMINA_MOV >= desde && h.FECHA_NOMINA_MOV <= hasta).OrderByDescending(h => h.FECHA_NOMINA_MOV).ThenBy(h => h.CEDULA).ToListAsync();

                }

                var resumen = from s in historico
                              group s by new { CodigoTipoNomina = s.CODIGO_TIPO_NOMINA, TipoNomina = s.TIPO_NOMINA } into g
                              select new
                              {
                                  g.Key.CodigoTipoNomina,
                                  g.Key.TipoNomina,

                              };

                List<RH_TIPOS_NOMINA> result = new List<RH_TIPOS_NOMINA>();
                foreach (var item in resumen)
                {

                    var tipoNomina = await _context.RH_TIPOS_NOMINA.DefaultIfEmpty().Where(tn => tn.CODIGO_TIPO_NOMINA == item.CodigoTipoNomina).FirstOrDefaultAsync();
                    if (tipoNomina != null && result.Where(x => x.CODIGO_TIPO_NOMINA == item.CodigoTipoNomina).FirstOrDefault() == null)
                    {
                        result.Add(tipoNomina);
                    }
                }


                return (List<RH_TIPOS_NOMINA>)result;

            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }
        }

        public async Task<ResultDto<RH_TIPOS_NOMINA>> Add(RH_TIPOS_NOMINA entity)
        {
            ResultDto<RH_TIPOS_NOMINA> result = new ResultDto<RH_TIPOS_NOMINA>(null);
            try
            {



                await _context.RH_TIPOS_NOMINA.AddAsync(entity);
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

        public async Task<ResultDto<RH_TIPOS_NOMINA>> Update(RH_TIPOS_NOMINA entity)
        {
            ResultDto<RH_TIPOS_NOMINA> result = new ResultDto<RH_TIPOS_NOMINA>(null);

            try
            {
                RH_TIPOS_NOMINA entityUpdate = await GetByCodigo(entity.CODIGO_TIPO_NOMINA);
                if (entityUpdate != null)
                {


                    _context.RH_TIPOS_NOMINA.Update(entity);
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

        public async Task<string> Delete(int codigoTipoNomina)
        {

            try
            {
                RH_TIPOS_NOMINA entity = await GetByCodigo(codigoTipoNomina);
                if (entity != null)
                {
                    _context.RH_TIPOS_NOMINA.Remove(entity);
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
                var last = await _context.RH_TIPOS_NOMINA.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_TIPO_NOMINA)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_TIPO_NOMINA + 1;
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

