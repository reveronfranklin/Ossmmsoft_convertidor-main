
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;

namespace Convertidor.Data.Repository.Presupuesto
{
    public class PRE_PLAN_UNICO_CUENTASRepository 
    {

        private readonly DataContextPre _context;
        private readonly IConfiguration _configuration;

        public PRE_PLAN_UNICO_CUENTASRepository(DataContextPre context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        
       

        public async Task<IEnumerable<PRE_PLAN_UNICO_CUENTAS>> GetAll()
        {
            try
            {

                var result = await _context.PRE_PLAN_UNICO_CUENTAS.DefaultIfEmpty()
                            .OrderBy(x => x.CODIGO_NIVEL1)
                            .ThenBy(x => x.CODIGO_NIVEL2)
                            .ThenBy(x => x.CODIGO_NIVEL3)
                            .ThenBy(x => x.CODIGO_NIVEL4)
                            .ThenBy(x => x.CODIGO_NIVEL5)
                            .ThenBy(x => x.CODIGO_NIVEL6)
                            .ToListAsync();

               
                return (IEnumerable<PRE_PLAN_UNICO_CUENTAS>)result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }


        public async Task<List<PRE_PLAN_UNICO_CUENTAS>> GetAllByCodigoPresupuesto(int codigoPresupuesto)
        {
            try
            {

                var result = await _context.PRE_PLAN_UNICO_CUENTAS.DefaultIfEmpty()
                            .Where(x=> x.CODIGO_PRESUPUESTO == codigoPresupuesto)
                            .OrderBy(x => x.CODIGO_NIVEL1)
                            .ThenBy(x => x.CODIGO_NIVEL2)
                            .ThenBy(x => x.CODIGO_NIVEL3)
                            .ThenBy(x => x.CODIGO_NIVEL4)
                            .ThenBy(x => x.CODIGO_NIVEL5)
                            .ThenBy(x => x.CODIGO_NIVEL6)
                            .ToListAsync();


                return (List<PRE_PLAN_UNICO_CUENTAS>)result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }

        public async Task<PRE_PLAN_UNICO_CUENTAS> GetByCodigos(FilterPrePUCPresupuestoCodigos filter)
        {
            try
            {

        var result = await _context.PRE_PLAN_UNICO_CUENTAS.DefaultIfEmpty()
                    .Where(x => x.CODIGO_PRESUPUESTO == filter.CodigoPresupuesto &&
                                x.CODIGO_NIVEL1 == filter.CodicoNivel1 &&
                                x.CODIGO_NIVEL2 == filter.CodicoNivel2 &&
                                x.CODIGO_NIVEL3 == filter.CodicoNivel3 &&
                                x.CODIGO_NIVEL4 == filter.CodicoNivel4 &&
                                x.CODIGO_NIVEL5 == filter.CodicoNivel5 &&
                                x.CODIGO_NIVEL6 == filter.CodicoNivel6


                     )
                    .FirstOrDefaultAsync();
                return (PRE_PLAN_UNICO_CUENTAS)result!;

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
                    entity.DENOMINACION = entity.DENOMINACION.ToUpper();
                    entity.DESCRIPCION = entity.DESCRIPCION.ToUpper();
                    entity.UNIDAD_EJECUTORA = entity.UNIDAD_EJECUTORA.ToUpper();
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
                entity.DENOMINACION = entity.DENOMINACION.ToUpper();
                entity.DESCRIPCION = entity.DESCRIPCION.ToUpper();
                entity.UNIDAD_EJECUTORA = entity.UNIDAD_EJECUTORA.ToUpper();
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


        public async Task<ResultDto<List<PRE_INDICE_CAT_PRG>>> ClonarByCodigoPresupuesto(int codigoPresupuestoOrigen,int codigoPresupuestoDestino)
        {

            ResultDto<List<PRE_INDICE_CAT_PRG>> result = new ResultDto<List<PRE_INDICE_CAT_PRG>>(null);
            try
            {
                var presupuestoDestino = await _context.PRE_PRESUPUESTOS.Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuestoDestino).DefaultIfEmpty().FirstOrDefaultAsync();
                if (presupuestoDestino == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Presupuesto destino no existe";
                    return result;
                }
                var presupuestoOrigen = await _context.PRE_PRESUPUESTOS.Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuestoOrigen).DefaultIfEmpty().FirstOrDefaultAsync();
                if (presupuestoOrigen == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Presupuesto origen no existe";
                    return result;
                }


                var icpDestino = await _context.PRE_INDICE_CAT_PRG.DefaultIfEmpty()
                           .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuestoDestino)
                           .FirstOrDefaultAsync();

                if (icpDestino != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ya existe ICP para este  presupuesto";
                    return result;
                }

                var icpOrigenResult = await _context.PRE_INDICE_CAT_PRG.DefaultIfEmpty()
                          .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuestoOrigen)
                          .OrderBy(x => x.CODIGO_SECTOR)
                          .ThenBy(x => x.CODIGO_PROGRAMA)
                          .ThenBy(x => x.CODIGO_PROYECTO)
                          .ThenBy(x => x.CODIGO_SUBPROGRAMA)
                          .ThenBy(x => x.CODIGO_ACTIVIDAD)
                          .ThenBy(x => x.CODIGO_ACTIVIDAD)
                          .ToListAsync();

                if (icpOrigenResult.Count > 0)
                {
                    foreach (var item in icpOrigenResult)
                    {
                        PRE_INDICE_CAT_PRG newItem = new PRE_INDICE_CAT_PRG();
                        newItem = item;
                        newItem.CODIGO_ICP = 0;
                        newItem.CODIGO_ICP_FK = 0;
                        newItem.ANO = presupuestoDestino.ANO;
                        newItem.CODIGO_PRESUPUESTO = presupuestoDestino.CODIGO_PRESUPUESTO;
                        await Create(newItem);


                    }
                }
                else
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No existe ICP para el presupuesto Origen";
                    return result;
                }


                var icpDestinoResult = await _context.PRE_INDICE_CAT_PRG.DefaultIfEmpty()
                            .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuestoDestino)
                            .OrderBy(x => x.CODIGO_SECTOR)
                            .ThenBy(x => x.CODIGO_PROGRAMA)
                            .ThenBy(x => x.CODIGO_PROYECTO)
                            .ThenBy(x => x.CODIGO_SUBPROGRAMA)
                            .ThenBy(x => x.CODIGO_ACTIVIDAD)
                            .ThenBy(x => x.CODIGO_ACTIVIDAD)
                            .ToListAsync();

                result.Data = icpDestinoResult;
                result.IsValid = true;
                result.Message = "";

                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }

    }
}
