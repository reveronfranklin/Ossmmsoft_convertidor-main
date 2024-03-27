using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
    public class PRE_INDICE_CAT_PRGRepository: IPRE_INDICE_CAT_PRGRepository
    {

        private readonly DataContextPre _context;
      
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public PRE_INDICE_CAT_PRGRepository(DataContextPre context,
                                         
                                            ISisUsuarioRepository sisUsuarioRepository)
        {
            _context = context;
         
            _sisUsuarioRepository = sisUsuarioRepository;
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

        
        public async Task<PRE_INDICE_CAT_PRG> GetByIcpConcat(int codigoPresupuesto,string sector,string programa,string subPrograma,string proyecto,string actividad)
        {
            try
            {

                var result = await _context.PRE_INDICE_CAT_PRG.DefaultIfEmpty()
                    .Where(x => 
                                x.CODIGO_PRESUPUESTO==codigoPresupuesto&&
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

        public async Task<PreIndiceCategoriaProgramaticaDescripciones> GetDescripcionIcp(int codIcp)
        {
            PreIndiceCategoriaProgramaticaDescripciones result = new PreIndiceCategoriaProgramaticaDescripciones();

            try
            {

                var icp = await _context.PRE_INDICE_CAT_PRG.DefaultIfEmpty()
                            .Where(x =>
                                        x.CODIGO_ICP == codIcp)
                  
                            .FirstOrDefaultAsync();
                if (icp != null)
                {
                    result.CodigoIcp = codIcp;
                    result.CodigoSector = icp.CODIGO_SECTOR;
                    result.CodigoPrograma = icp.CODIGO_PROGRAMA;
                    result.CodigoSubPrograma = icp.CODIGO_SUBPROGRAMA;
                    result.CodigoProyecto = icp.CODIGO_PROYECTO;
                    result.CodigoActividad = icp.CODIGO_ACTIVIDAD;
                    result.CodigoOficina = icp.CODIGO_OFICINA;
                    var sector = await _context.PRE_INDICE_CAT_PRG.DefaultIfEmpty()
                          .Where(x => x.CODIGO_PRESUPUESTO == icp.CODIGO_PRESUPUESTO &&
                                      x.CODIGO_SECTOR == icp.CODIGO_SECTOR &&
                                      x.CODIGO_PROGRAMA == "00" &&
                                      x.CODIGO_SUBPROGRAMA == "00" &&
                                      x.CODIGO_PROYECTO == "00" &&
                                      x.CODIGO_ACTIVIDAD == "00" &&
                                      x.CODIGO_OFICINA == "00"
                                      )
                          .FirstOrDefaultAsync();
                    if (sector != null)
                    {
                        result.DescripcionSector = sector.DENOMINACION;
                    }
                    else {
                        result.DescripcionSector = "S/S";
                    }

                    var programa = await _context.PRE_INDICE_CAT_PRG.DefaultIfEmpty()
                         .Where(x => x.CODIGO_PRESUPUESTO == icp.CODIGO_PRESUPUESTO &&
                                     x.CODIGO_SECTOR == icp.CODIGO_SECTOR &&
                                     x.CODIGO_PROGRAMA == icp.CODIGO_PROGRAMA &&
                                     x.CODIGO_SUBPROGRAMA == "00" &&
                                     x.CODIGO_PROYECTO == "00" &&
                                     x.CODIGO_ACTIVIDAD == "00" &&
                                     x.CODIGO_OFICINA == "00"
                                     )
                         .FirstOrDefaultAsync();
                    if (programa != null)
                    {
                        result.DescripcionPrograma = programa.DENOMINACION;
                    }
                    else
                    {
                        result.DescripcionPrograma = "S/P";
                    }

                    var subPrograma = await _context.PRE_INDICE_CAT_PRG.DefaultIfEmpty()
                         .Where(x => x.CODIGO_PRESUPUESTO == icp.CODIGO_PRESUPUESTO &&
                                     x.CODIGO_SECTOR == icp.CODIGO_SECTOR &&
                                     x.CODIGO_PROGRAMA == icp.CODIGO_PROGRAMA &&
                                     x.CODIGO_SUBPROGRAMA == icp.CODIGO_SUBPROGRAMA &&
                                     x.CODIGO_PROYECTO == "00" &&
                                     x.CODIGO_ACTIVIDAD == "00" &&
                                     x.CODIGO_OFICINA == "00"
                                     )
                         .FirstOrDefaultAsync();
                    if (subPrograma != null)
                    {
                        result.DescripcionSubPrograma = subPrograma.DENOMINACION;
                    }
                    else
                    {
                        result.DescripcionSubPrograma = "S/SP";
                    }

                    var proyecto = await _context.PRE_INDICE_CAT_PRG.DefaultIfEmpty()
                        .Where(x => x.CODIGO_PRESUPUESTO == icp.CODIGO_PRESUPUESTO &&
                                    x.CODIGO_SECTOR == icp.CODIGO_SECTOR &&
                                    x.CODIGO_PROGRAMA == icp.CODIGO_PROGRAMA &&
                                    x.CODIGO_SUBPROGRAMA == icp.CODIGO_SUBPROGRAMA &&
                                    x.CODIGO_PROYECTO == icp.CODIGO_PROYECTO &&
                                    x.CODIGO_ACTIVIDAD == "00" &&
                                    x.CODIGO_OFICINA == "00"
                                    )
                        .FirstOrDefaultAsync();
                    if (proyecto != null)
                    {
                        result.DescripcionProyecto = proyecto.DENOMINACION;
                    }
                    else
                    {
                        result.DescripcionProyecto = "S/SP";
                    }
                    var actividad = await _context.PRE_INDICE_CAT_PRG.DefaultIfEmpty()
                       .Where(x => x.CODIGO_PRESUPUESTO == icp.CODIGO_PRESUPUESTO &&
                                   x.CODIGO_SECTOR == icp.CODIGO_SECTOR &&
                                   x.CODIGO_PROGRAMA == icp.CODIGO_PROGRAMA &&
                                   x.CODIGO_SUBPROGRAMA == icp.CODIGO_SUBPROGRAMA &&
                                   x.CODIGO_PROYECTO == icp.CODIGO_PROYECTO &&
                                   x.CODIGO_ACTIVIDAD == icp.CODIGO_ACTIVIDAD &&
                                   x.CODIGO_OFICINA == "00"
                                   )
                       .FirstOrDefaultAsync();
                    if (actividad != null)
                    {
                        result.DescripcionActividad = actividad.DENOMINACION;
                    }
                    else
                    {
                        result.DescripcionActividad = "S/A";
                    }
                    var oficina = await _context.PRE_INDICE_CAT_PRG.DefaultIfEmpty()
                     .Where(x => x.CODIGO_PRESUPUESTO == icp.CODIGO_PRESUPUESTO &&
                                 x.CODIGO_SECTOR == icp.CODIGO_SECTOR &&
                                 x.CODIGO_PROGRAMA == icp.CODIGO_PROGRAMA &&
                                 x.CODIGO_SUBPROGRAMA == icp.CODIGO_SUBPROGRAMA &&
                                 x.CODIGO_PROYECTO == icp.CODIGO_PROYECTO &&
                                 x.CODIGO_ACTIVIDAD == icp.CODIGO_ACTIVIDAD &&
                                 x.CODIGO_OFICINA == icp.CODIGO_OFICINA
                                 )
                     .FirstOrDefaultAsync();
                    if (oficina != null)
                    {
                        result.CodigoOficina = oficina.DENOMINACION;
                    }
                    else
                    {
                        result.CodigoOficina = "S/O"; 
                    }

                    if (result.CodigoSector == "00") result.DescripcionSector = icp.DENOMINACION;          
                    if (result.CodigoPrograma == "00") result.DescripcionPrograma = icp.DENOMINACION;
                    if (result.CodigoSubPrograma == "00") result.DescripcionSubPrograma = icp.DENOMINACION;
                    if (result.CodigoProyecto == "00") result.DescripcionProyecto = icp.DENOMINACION;
                    if (result.CodigoActividad == "00") result.DescripcionActividad = icp.DENOMINACION;
                    if (result.CodigoOficina == "00") result.DescripcionOficina = icp.DENOMINACION;
                }


                return (PreIndiceCategoriaProgramaticaDescripciones)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return result;
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
                    await _context.SaveChangesAsync();
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

          
                var conectado = await _sisUsuarioRepository.GetConectado();



                PRE_INDICE_CAT_PRG entityUpdate = await GetByCodigo( entity.CODIGO_ICP);
                if (entityUpdate != null)
                {
                    entityUpdate.CODIGO_EMPRESA = conectado.Empresa;
                    if (entity.DENOMINACION == null) entity.DENOMINACION = "";
                    if (entity.DESCRIPCION == null) entity.DESCRIPCION = "";
                    if (entity.UNIDAD_EJECUTORA == null) entity.UNIDAD_EJECUTORA = "";
                    entity.DENOMINACION = entity.DENOMINACION.ToUpper();
                    entity.DESCRIPCION = entity.DESCRIPCION.ToUpper();
                    entity.UNIDAD_EJECUTORA = entity.UNIDAD_EJECUTORA.ToUpper();
                    entity.FECHA_UPD = DateTime.Now;
                    entity.USUARIO_UPD = conectado.Usuario;
                    _context.PRE_INDICE_CAT_PRG.Update(entity);
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


        public async Task<ResultDto<PRE_INDICE_CAT_PRG>> Create(PRE_INDICE_CAT_PRG entity)
        {
            ResultDto<PRE_INDICE_CAT_PRG> result = new ResultDto<PRE_INDICE_CAT_PRG>(null);
           
            try
            {

              

                var conectado = await _sisUsuarioRepository.GetConectado();


                entity.CODIGO_EMPRESA = conectado.Empresa;
                if (entity.DENOMINACION == null) entity.DENOMINACION = "";
                if (entity.DESCRIPCION == null) entity.DESCRIPCION = "";
                if (entity.UNIDAD_EJECUTORA == null) entity.UNIDAD_EJECUTORA = "";
                entity.DENOMINACION = entity.DENOMINACION.ToUpper();
                entity.DESCRIPCION = entity.DESCRIPCION.ToUpper();
                entity.UNIDAD_EJECUTORA = entity.UNIDAD_EJECUTORA.ToUpper();
                entity.CODIGO_ICP = await GetNextKey();
                entity.FECHA_INS = DateTime.Now;
                entity.USUARIO_INS = conectado.Usuario;

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
