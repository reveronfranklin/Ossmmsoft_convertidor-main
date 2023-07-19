
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;

namespace Convertidor.Data.Repository.Presupuesto
{
    public class PRE_INDICE_CAT_PRGRepository: IPRE_INDICE_CAT_PRGRepository
    {

        private readonly DataContextPre _context;
        private readonly IConfiguration _configuration;

        public PRE_INDICE_CAT_PRGRepository(DataContextPre context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        
        public async Task<IEnumerable<PRE_INDICE_CAT_PRG>> GetByLastDay(int days)
        {
            try
            {
                var fecha = DateTime.Now.AddDays(days * -1);
                var result = await _context.PRE_INDICE_CAT_PRG.DefaultIfEmpty().ToListAsync();
                return (IEnumerable<PRE_INDICE_CAT_PRG>)result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }

        public async Task<IEnumerable<PRE_INDICE_CAT_PRG>> GetAll()
        {
            try
            {

                var result = await _context.PRE_INDICE_CAT_PRG.DefaultIfEmpty()
                            .OrderBy(x => x.CODIGO_SECTOR)
                            .ThenBy(x => x.CODIGO_PROGRAMA)
                            .ThenBy(x => x.CODIGO_PROYECTO)
                            .ThenBy(x => x.CODIGO_SUBPROGRAMA)
                            .ThenBy(x => x.CODIGO_ACTIVIDAD)
                            .ThenBy(x => x.CODIGO_ACTIVIDAD)
                            .ToListAsync();

               
                return (IEnumerable<PRE_INDICE_CAT_PRG>)result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }


        public async Task<List<PRE_INDICE_CAT_PRG>> GetAllByCodigoPresupuesto(int codigoPresupuesto)
        {
            try
            {

                var result = await _context.PRE_INDICE_CAT_PRG.DefaultIfEmpty()
                            .Where(x=> x.CODIGO_PRESUPUESTO == codigoPresupuesto)
                            .OrderBy(x => x.CODIGO_SECTOR)
                            .ThenBy(x => x.CODIGO_PROGRAMA)
                            .ThenBy(x => x.CODIGO_PROYECTO)
                            .ThenBy(x => x.CODIGO_SUBPROGRAMA)
                            .ThenBy(x => x.CODIGO_ACTIVIDAD)
                            .ThenBy(x => x.CODIGO_ACTIVIDAD)
                            .ToListAsync();


                return (List<PRE_INDICE_CAT_PRG>)result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }

        public async Task<PRE_INDICE_CAT_PRG> GetByCodigos(PreIndiceCategoriaProgramaticaUpdateDto entity)
        {
            try
            {

        var result = await _context.PRE_INDICE_CAT_PRG.DefaultIfEmpty()
                    .Where(x => x.ANO == entity.Ano &&
                                x.CODIGO_SECTOR == entity.CodigoSector &&
                                x.CODIGO_PROGRAMA == entity.CodigoPrograma &&
                                x.CODIGO_SUBPROGRAMA == entity.CodigoSubPrograma &&
                                x.CODIGO_PROYECTO == entity.CodigoProyecto &&
                                 x.CODIGO_ACTIVIDAD == entity.CodigoActividad &&
                                 x.CODIGO_OFICINA ==entity.CodigoOficina
                     )
                    .FirstOrDefaultAsync();
                return (PRE_INDICE_CAT_PRG)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }

        public async Task<PRE_INDICE_CAT_PRG> GetHastaActividad(int ano,int codIcp,string sector,string programa,string subPrograma,string proyecto,string actividad)
        {
            try
            {

                var result = await _context.PRE_INDICE_CAT_PRG.DefaultIfEmpty()
                            .Where(x => x.CODIGO_ICP!= codIcp &&
                                        x.ANO == ano &&
                                        x.CODIGO_SECTOR == sector &&
                                        x.CODIGO_PROGRAMA == programa &&
                                        x.CODIGO_SUBPROGRAMA == subPrograma &&
                                        x.CODIGO_PROYECTO == proyecto &&
                                         x.CODIGO_ACTIVIDAD == actividad 
                             )
                             .OrderBy(x => x.CODIGO_SECTOR)
                             .ThenBy(x => x.CODIGO_PROGRAMA)
                             .ThenBy(x => x.CODIGO_SUBPROGRAMA)
                             .ThenBy(x => x.CODIGO_PROYECTO)
                             .ThenBy(x => x.CODIGO_ACTIVIDAD)
                            .FirstOrDefaultAsync();
                return (PRE_INDICE_CAT_PRG)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<PRE_INDICE_CAT_PRG> GetHastaProyecto(int ano, int codIcp, string sector, string programa, string subPrograma, string proyecto)
        {
            try
            {

                var result = await _context.PRE_INDICE_CAT_PRG.DefaultIfEmpty()
                            .Where(x => x.CODIGO_ICP != codIcp &&
                                        x.ANO == ano &&
                                        x.CODIGO_SECTOR == sector &&
                                        x.CODIGO_PROGRAMA == programa &&
                                        x.CODIGO_SUBPROGRAMA == subPrograma &&
                                        x.CODIGO_PROYECTO == proyecto 
                                         
                             )
                              .OrderBy(x => x.CODIGO_SECTOR)
                             .ThenBy(x => x.CODIGO_PROGRAMA)
                             .ThenBy(x => x.CODIGO_SUBPROGRAMA)
                             .ThenBy(x => x.CODIGO_PROYECTO)
                             .ThenBy(x => x.CODIGO_ACTIVIDAD)
                            .FirstOrDefaultAsync();
                return (PRE_INDICE_CAT_PRG)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }
        public async Task<PRE_INDICE_CAT_PRG> GetHastaSubPrograma(int ano, int codIcp, string sector, string programa, string subPrograma)
        {
            try
            {

                var result = await _context.PRE_INDICE_CAT_PRG.DefaultIfEmpty()
                            .Where(x => x.CODIGO_ICP != codIcp &&
                                        x.ANO == ano &&
                                        x.CODIGO_SECTOR == sector &&
                                        x.CODIGO_PROGRAMA == programa &&
                                        x.CODIGO_SUBPROGRAMA == subPrograma
                                     

                             )
                             .OrderBy(x => x.CODIGO_SECTOR)
                             .ThenBy(x => x.CODIGO_PROGRAMA)
                             .ThenBy(x => x.CODIGO_SUBPROGRAMA)
                             .ThenBy(x => x.CODIGO_PROYECTO)
                             .ThenBy(x => x.CODIGO_ACTIVIDAD)

                            .FirstOrDefaultAsync();
                return (PRE_INDICE_CAT_PRG)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }
        public async Task<PRE_INDICE_CAT_PRG> GetHastaPrograma(int ano, int codIcp, string sector, string programa)
        {
            try
            {

                var result = await _context.PRE_INDICE_CAT_PRG.DefaultIfEmpty()
                            .Where(x => x.CODIGO_ICP != codIcp &&
                                        x.ANO == ano &&
                                        x.CODIGO_SECTOR == sector &&
                                        x.CODIGO_PROGRAMA == programa 
                             )
                              .OrderBy(x => x.CODIGO_SECTOR)
                             .ThenBy(x => x.CODIGO_PROGRAMA)
                             .ThenBy(x => x.CODIGO_SUBPROGRAMA)
                             .ThenBy(x => x.CODIGO_PROYECTO)
                             .ThenBy(x => x.CODIGO_ACTIVIDAD)

                            .FirstOrDefaultAsync();
                return (PRE_INDICE_CAT_PRG)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }
        public async Task<PRE_INDICE_CAT_PRG> GetHastaSector(int ano, int codIcp, string sector)
        {
            try
            {

                var result = await _context.PRE_INDICE_CAT_PRG.DefaultIfEmpty()
                            .Where(x =>
                                        x.ANO == ano &&
                                        x.CODIGO_SECTOR == sector 
                             )
                              .OrderBy(x => x.CODIGO_SECTOR)
                             .ThenBy(x => x.CODIGO_PROGRAMA)
                             .ThenBy(x => x.CODIGO_SUBPROGRAMA)
                             .ThenBy(x => x.CODIGO_PROYECTO)
                             .ThenBy(x => x.CODIGO_ACTIVIDAD)
                            .FirstOrDefaultAsync();
                return (PRE_INDICE_CAT_PRG)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<bool> IcpContieneHijos(int codigoIcp)
        {
            bool result;
            try
            {
                var icp = await GetByCodigo(codigoIcp);
                if (icp == null)
                {
                    result = false;

                }
                else
                {
                    var resultSearch = await _context.PRE_INDICE_CAT_PRG.DefaultIfEmpty()
                          .Where(x => x.CODIGO_ICP!=codigoIcp &&
                                      x.ANO == icp.ANO &&
                                      x.CODIGO_SECTOR == icp.CODIGO_SECTOR &&
                                      x.CODIGO_PROGRAMA == icp.CODIGO_PROGRAMA &&
                                      x.CODIGO_SUBPROGRAMA == icp.CODIGO_SUBPROGRAMA &&
                                      x.CODIGO_PROYECTO == icp.CODIGO_PROYECTO &&
                                      x.CODIGO_ACTIVIDAD == icp.CODIGO_ACTIVIDAD &&
                                      x.CODIGO_OFICINA == icp.CODIGO_OFICINA
                           )
                          .FirstOrDefaultAsync();
                    result = true;
                }
              
                return (bool)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return false;
            }



        }


        public async Task<PRE_INDICE_CAT_PRG> GetByCodigo(int codigoIcp)
        {
            try
            {

                var result = await _context.PRE_INDICE_CAT_PRG.DefaultIfEmpty()
                    .Where(x =>  x.CODIGO_ICP == codigoIcp)
                    .FirstOrDefaultAsync();
                return (PRE_INDICE_CAT_PRG)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }
        public async Task<string> Delete(int codigoICP)
        {

            try
            {
                PRE_INDICE_CAT_PRG entity = await GetByCodigo(codigoICP);
                if (entity != null)
                {
                    _context.PRE_INDICE_CAT_PRG.Remove(entity);
                    _context.SaveChanges();
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }



        }

        public async Task<ResultDto<PRE_INDICE_CAT_PRG>> Update(PRE_INDICE_CAT_PRG entity)
        {
            ResultDto<PRE_INDICE_CAT_PRG> result = new ResultDto<PRE_INDICE_CAT_PRG>(null);

            try
            {

                var settings = _configuration.GetSection("Settings").Get<Settings>();
                var empresString = @settings.EmpresaConfig;
                var empresa = Int32.Parse(empresString);

                PRE_INDICE_CAT_PRG entityUpdate = await GetByCodigo( entity.CODIGO_ICP);
                if (entityUpdate != null)
                {
                    entityUpdate.CODIGO_EMPRESA = empresa;
                    _context.PRE_INDICE_CAT_PRG.Update(entity);
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


        public async Task<ResultDto<PRE_INDICE_CAT_PRG>> Create(PRE_INDICE_CAT_PRG entity)
        {
            ResultDto<PRE_INDICE_CAT_PRG> result = new ResultDto<PRE_INDICE_CAT_PRG>(null);

            try
            {

                var settings = _configuration.GetSection("Settings").Get<Settings>();
                var empresString = @settings.EmpresaConfig;
                var empresa = Int32.Parse(empresString);
                entity.CODIGO_EMPRESA = empresa;

                entity.CODIGO_ICP = await GetNextKey();

                _context.PRE_INDICE_CAT_PRG.Add(entity);
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
                var last = await _context.PRE_INDICE_CAT_PRG.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_ICP)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_ICP + 1;
                }

                return (int)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return 0;
            }



        }


    }
}
