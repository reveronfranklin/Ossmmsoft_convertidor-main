﻿
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Presupuesto
{
    public class PRE_PLAN_UNICO_CUENTASRepository: IPRE_PLAN_UNICO_CUENTASRepository
    {

        private readonly DataContextPre _context;
     
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public PRE_PLAN_UNICO_CUENTASRepository(DataContextPre context, ISisUsuarioRepository sisUsuarioRepository)
        {
            _context = context;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        
       

        public async Task<IEnumerable<PRE_PLAN_UNICO_CUENTAS>> GetAll()
        {
            try
            {

                var result = await _context.PRE_PLAN_UNICO_CUENTAS.DefaultIfEmpty()
                            .OrderBy(x => x.CODIGO_GRUPO)
                            .ThenBy(x => x.CODIGO_NIVEL1)
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
                    
                            .ToListAsync();

                
                
                result = result
                 
                    .OrderBy(x => x.CODIGO_GRUPO)
                    .ThenBy(x => x.CODIGO_NIVEL1)
                    .ThenBy(x => x.CODIGO_NIVEL2)
                    .ThenBy(x => x.CODIGO_NIVEL3)
                    .ThenBy(x => x.CODIGO_NIVEL4)
                    .ThenBy(x => x.CODIGO_NIVEL5)
                    .ThenBy(x => x.CODIGO_NIVEL6)
                    .ToList();

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
                                x.CODIGO_GRUPO == filter.CodigoGrupo &&
                                x.CODIGO_NIVEL1 == filter.CodicoNivel1 &&
                                x.CODIGO_NIVEL2 == filter.CodicoNivel2 &&
                                x.CODIGO_NIVEL3 == filter.CodicoNivel3 &&
                                x.CODIGO_NIVEL4 == filter.CodicoNivel4 &&
                                x.CODIGO_NIVEL5 == filter.CodicoNivel5 &&
                                x.CODIGO_NIVEL6 == filter.CodicoNivel6


                     )
                       .OrderBy(x => x.CODIGO_GRUPO)
                             .ThenBy(x => x.CODIGO_NIVEL1)
                             .ThenBy(x => x.CODIGO_NIVEL2)
                              .ThenBy(x => x.CODIGO_NIVEL3)
                              .ThenBy(x => x.CODIGO_NIVEL4)
                              .ThenBy(x => x.CODIGO_NIVEL5)
                              .ThenBy(x => x.CODIGO_NIVEL6)

                    .FirstOrDefaultAsync();
                return (PRE_PLAN_UNICO_CUENTAS)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }

        public async Task<PRE_PLAN_UNICO_CUENTAS> GetHastaNivel5(int codigoPresupuesto,int codigoPuc,string grupo,string nivel1,string nivel2, string nivel3, string nivel4,string nivel5)
        {
          
            try
            {

                var result = await _context.PRE_PLAN_UNICO_CUENTAS.DefaultIfEmpty()
                            .Where(x => x.CODIGO_PUC!= codigoPuc &&
                                        x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                                        x.CODIGO_GRUPO == grupo &&
                                        x.CODIGO_NIVEL1 == nivel1 &&
                                        x.CODIGO_NIVEL2 == nivel2 &&
                                        x.CODIGO_NIVEL3 == nivel3 &&
                                        x.CODIGO_NIVEL4 == nivel4 &&
                                        x.CODIGO_NIVEL5 == nivel5
                             )
                                .OrderBy(x => x.CODIGO_GRUPO)
                             .ThenBy(x => x.CODIGO_NIVEL1)
                             .ThenBy(x => x.CODIGO_NIVEL2)
                              .ThenBy(x => x.CODIGO_NIVEL3)
                              .ThenBy(x => x.CODIGO_NIVEL4)
                              .ThenBy(x => x.CODIGO_NIVEL5)
                              .ThenBy(x => x.CODIGO_NIVEL6)

                            .FirstOrDefaultAsync();
                return (PRE_PLAN_UNICO_CUENTAS)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }


        }
        public async Task<PRE_PLAN_UNICO_CUENTAS> GetHastaNivel4(int codigoPresupuesto, int codigoPuc, string grupo, string nivel1, string nivel2, string nivel3, string nivel4)
        {

            try
            {

                var result = await _context.PRE_PLAN_UNICO_CUENTAS.DefaultIfEmpty()
                            .Where(x => x.CODIGO_PUC != codigoPuc &&
                                        x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                                        x.CODIGO_GRUPO == grupo &&
                                        x.CODIGO_NIVEL1 == nivel1 &&
                                        x.CODIGO_NIVEL2 == nivel2 &&
                                        x.CODIGO_NIVEL3 == nivel3 &&
                                        x.CODIGO_NIVEL4 == nivel4 
                             )
                                .OrderBy(x => x.CODIGO_GRUPO)
                             .ThenBy(x => x.CODIGO_NIVEL1)
                             .ThenBy(x => x.CODIGO_NIVEL2)
                              .ThenBy(x => x.CODIGO_NIVEL3)
                              .ThenBy(x => x.CODIGO_NIVEL4)
                              .ThenBy(x => x.CODIGO_NIVEL5)
                              .ThenBy(x => x.CODIGO_NIVEL6)

                            .FirstOrDefaultAsync();
                return (PRE_PLAN_UNICO_CUENTAS)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }


        }
        public async Task<PRE_PLAN_UNICO_CUENTAS> GetHastaNivel3(int codigoPresupuesto, int codigoPuc, string grupo, string nivel1, string nivel2, string nivel3)
        {

            try
            {

                var result = await _context.PRE_PLAN_UNICO_CUENTAS.DefaultIfEmpty()
                            .Where(x => x.CODIGO_PUC != codigoPuc &&
                                        x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                                        x.CODIGO_GRUPO == grupo &&
                                        x.CODIGO_NIVEL1 == nivel1 &&
                                        x.CODIGO_NIVEL2 == nivel2 &&
                                        x.CODIGO_NIVEL3 == nivel3 
                                    

                             )
                              .OrderBy(x => x.CODIGO_GRUPO)
                             .ThenBy(x => x.CODIGO_NIVEL1)
                             .ThenBy(x => x.CODIGO_NIVEL2)
                              .ThenBy(x => x.CODIGO_NIVEL3)
                              .ThenBy(x => x.CODIGO_NIVEL4)
                              .ThenBy(x => x.CODIGO_NIVEL5)
                              .ThenBy(x => x.CODIGO_NIVEL6)


                            .FirstOrDefaultAsync();
                return (PRE_PLAN_UNICO_CUENTAS)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }


        }
        public async Task<PRE_PLAN_UNICO_CUENTAS> GetHastaNivel2(int codigoPresupuesto, int codigoPuc, string grupo, string nivel1, string nivel2)
        {

            try
            {

                var result = await _context.PRE_PLAN_UNICO_CUENTAS.DefaultIfEmpty()
                            .Where(x => x.CODIGO_PUC != codigoPuc &&
                                        x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                                        x.CODIGO_GRUPO == grupo &&
                                        x.CODIGO_NIVEL1 == nivel1 &&
                                        x.CODIGO_NIVEL2 == nivel2
                                     

                             )
                               .OrderBy(x => x.CODIGO_GRUPO)
                             .ThenBy(x => x.CODIGO_NIVEL1)
                             .ThenBy(x => x.CODIGO_NIVEL2)
                              .ThenBy(x => x.CODIGO_NIVEL3)
                              .ThenBy(x => x.CODIGO_NIVEL4)
                              .ThenBy(x => x.CODIGO_NIVEL5)
                              .ThenBy(x => x.CODIGO_NIVEL6)

                            .FirstOrDefaultAsync();
                return (PRE_PLAN_UNICO_CUENTAS)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }


        }
        public async Task<PRE_PLAN_UNICO_CUENTAS> GetHastaNivel1(int codigoPresupuesto, int codigoPuc, string grupo, string nivel1)
        {

            try
            {

                var result = await _context.PRE_PLAN_UNICO_CUENTAS.DefaultIfEmpty()
                            .Where(x => x.CODIGO_PUC != codigoPuc &&
                                        x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                                        x.CODIGO_GRUPO == grupo &&
                                        x.CODIGO_NIVEL1 == nivel1

                             )
                               .OrderBy(x => x.CODIGO_GRUPO)
                             .ThenBy(x => x.CODIGO_NIVEL1)
                             .ThenBy(x => x.CODIGO_NIVEL2)
                              .ThenBy(x => x.CODIGO_NIVEL3)
                              .ThenBy(x => x.CODIGO_NIVEL4)
                              .ThenBy(x => x.CODIGO_NIVEL5)
                              .ThenBy(x => x.CODIGO_NIVEL6)

                            .FirstOrDefaultAsync();
                return (PRE_PLAN_UNICO_CUENTAS)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }


        }

        public async Task<PRE_PLAN_UNICO_CUENTAS> GetHastaGrupoDiferentePuc(int codigoPresupuesto, int codigoPuc, string grupo)
        {

            try
            {
              

                var result = await _context.PRE_PLAN_UNICO_CUENTAS.DefaultIfEmpty()
                            .Where(x => x.CODIGO_PUC != codigoPuc &&
                                        x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                                        x.CODIGO_GRUPO == grupo
                                       

                             )
                             .OrderBy(x => x.CODIGO_GRUPO)
                             .ThenBy(x => x.CODIGO_NIVEL1)
                             .ThenBy(x => x.CODIGO_NIVEL2)
                              .ThenBy(x => x.CODIGO_NIVEL3)
                              .ThenBy(x => x.CODIGO_NIVEL4)
                              .ThenBy(x => x.CODIGO_NIVEL5)
                              .ThenBy(x => x.CODIGO_NIVEL6)
                           




                            .FirstOrDefaultAsync();
                return (PRE_PLAN_UNICO_CUENTAS)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }


        }
        public async Task<PRE_PLAN_UNICO_CUENTAS> GetHastaGrupoIgualPuc(int codigoPresupuesto, int codigoPuc, string grupo)
        {

            try
            {


                var result = await _context.PRE_PLAN_UNICO_CUENTAS.DefaultIfEmpty()
                            .Where(x => x.CODIGO_PUC != codigoPuc &&
                                        x.CODIGO_PRESUPUESTO == codigoPresupuesto &&
                                        x.CODIGO_GRUPO == grupo


                             )
                             .OrderBy(x => x.CODIGO_GRUPO)
                             .ThenBy(x => x.CODIGO_NIVEL1)
                             .ThenBy(x => x.CODIGO_NIVEL2)
                              .ThenBy(x => x.CODIGO_NIVEL3)
                              .ThenBy(x => x.CODIGO_NIVEL4)
                              .ThenBy(x => x.CODIGO_NIVEL5)
                              .ThenBy(x => x.CODIGO_NIVEL6)





                            .FirstOrDefaultAsync();
                return (PRE_PLAN_UNICO_CUENTAS)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }


        }

        public async Task<PRE_PLAN_UNICO_CUENTAS> GetByCodigo(int codigoIcp)
        {
            try
            {

                var result = await _context.PRE_PLAN_UNICO_CUENTAS.DefaultIfEmpty()
                    .Where(x =>  x.CODIGO_PUC == codigoIcp)
                    .FirstOrDefaultAsync();
                return (PRE_PLAN_UNICO_CUENTAS)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }



        }
        public async Task<string> Delete(int codigoPuc)
        {

            try
            {
                PRE_PLAN_UNICO_CUENTAS entity = await GetByCodigo(codigoPuc);
                if (entity != null)
                {
                    _context.PRE_PLAN_UNICO_CUENTAS.Remove(entity);
                    await _context.SaveChangesAsync();
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }



        }
        
        public async Task<string> DeleteByPresupuesto(int codigoPresupuesto)
        {

            try
            {
                FormattableString xqueryDiario = $"DECLARE \nBEGIN\n DELETE FROM PRE.PRE_PLAN_UNICO_CUENTAS WHERE CODIGO_PRESUPUESTO= {codigoPresupuesto};\nEND;";

                var resultDiario = _context.Database.ExecuteSqlInterpolated(xqueryDiario);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }




        }
        public async Task<ResultDto<PRE_PLAN_UNICO_CUENTAS>> Update(PRE_PLAN_UNICO_CUENTAS entity)
        {
            ResultDto<PRE_PLAN_UNICO_CUENTAS> result = new ResultDto<PRE_PLAN_UNICO_CUENTAS>(null);

            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();
               

                PRE_PLAN_UNICO_CUENTAS entityUpdate = await GetByCodigo( entity.CODIGO_PUC);
                if (entityUpdate != null)
                {
                    entityUpdate.CODIGO_EMPRESA = conectado.Empresa;
                    if (entity.DENOMINACION == null) entity.DENOMINACION = "";
                    if (entity.DESCRIPCION == null) entity.DESCRIPCION = "";
                    entity.DENOMINACION = entity.DENOMINACION.ToUpper();
                    entity.DESCRIPCION = entity.DESCRIPCION.ToUpper();
                    entity.USUARIO_UPD = conectado.Usuario;
                    entity.FECHA_UPD = DateTime.Now;
                    _context.PRE_PLAN_UNICO_CUENTAS.Update(entity);
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
        public async Task<ResultDto<PRE_PLAN_UNICO_CUENTAS>> Create(PRE_PLAN_UNICO_CUENTAS entity)
        {
            ResultDto<PRE_PLAN_UNICO_CUENTAS> result = new ResultDto<PRE_PLAN_UNICO_CUENTAS>(null);

            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                entity.CODIGO_EMPRESA = conectado.Empresa;
                if (entity.DENOMINACION == null) entity.DENOMINACION = "";
                if (entity.DESCRIPCION == null) entity.DESCRIPCION = "";
                entity.DENOMINACION = entity.DENOMINACION.ToUpper();
                entity.DESCRIPCION = entity.DESCRIPCION.ToUpper();
              
                entity.CODIGO_PUC = await GetNextKey();
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;
                _context.PRE_PLAN_UNICO_CUENTAS.Add(entity);
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
                var last = await _context.PRE_PLAN_UNICO_CUENTAS.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_PUC)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_PUC + 1;
                }

                return (int)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return 0;
            }



        }
        public async Task<ResultDto<List<PRE_PLAN_UNICO_CUENTAS>>> ClonarByCodigoPresupuesto(int codigoPresupuestoOrigen,int codigoPresupuestoDestino,int codigoUsuario,int codigoEmpresa)
        {

            ResultDto<List<PRE_PLAN_UNICO_CUENTAS>> result = new ResultDto<List<PRE_PLAN_UNICO_CUENTAS>>(null);
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


                var pucDestino = await _context.PRE_PLAN_UNICO_CUENTAS.DefaultIfEmpty()
                           .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuestoDestino)
                           .FirstOrDefaultAsync();

                if (pucDestino != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ya existe PUC para este  presupuesto";
                    return result;
                }

                
                FormattableString xqueryDiario = $"DECLARE \nBEGIN\nPRE.PRE_PKG_CLONAR_PRESUPUESTO.CLONAR_PUC({codigoPresupuestoOrigen},{codigoPresupuestoDestino},{codigoUsuario},{codigoEmpresa});\nEND;";

                var resultDiario =  _context.Database.ExecuteSqlInterpolated(xqueryDiario);

                
                
                /*var pucOrigenResult = await _context.PRE_PLAN_UNICO_CUENTAS.DefaultIfEmpty()
                          .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuestoOrigen)
                           .OrderBy(x => x.CODIGO_GRUPO)
                             .ThenBy(x => x.CODIGO_NIVEL1)
                             .ThenBy(x => x.CODIGO_NIVEL2)
                             .ThenBy(x => x.CODIGO_NIVEL3)
                             .ThenBy(x => x.CODIGO_NIVEL4)
                              .ThenBy(x => x.CODIGO_NIVEL5)
                          .ToListAsync();

               

                 if (pucOrigenResult.Count > 0)
                {
                    foreach (var item in pucOrigenResult)
                    {
                        PRE_PLAN_UNICO_CUENTAS newItem = new PRE_PLAN_UNICO_CUENTAS();
                        newItem = item;
                        newItem.CODIGO_PUC = 0;
                        newItem.CODIGO_PUC_FK = 0;  
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
                }*/


                var icpDestinoResult = await _context.PRE_PLAN_UNICO_CUENTAS.DefaultIfEmpty()
                            .Where(x => x.CODIGO_PRESUPUESTO == codigoPresupuestoDestino)
                            .OrderBy(x => x.CODIGO_GRUPO)
                            .ThenBy(x => x.CODIGO_NIVEL1)
                            .ThenBy(x => x.CODIGO_NIVEL2)
                            .ThenBy(x => x.CODIGO_NIVEL3)
                            .ThenBy(x => x.CODIGO_NIVEL4)
                            .ThenBy(x => x.CODIGO_NIVEL5)
                            .ToListAsync();

                result.Data = icpDestinoResult;
                result.IsValid = true;
                result.Message = "";

                return result;
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = true;
                result.Message = ex.Message;

                return result;
            }



        }

    }
}
