using System;
using AutoMapper;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository.Presupuesto;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;
using Ganss.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Services.Presupuesto
{
	public class PRE_V_SALDOSServices: IPRE_V_SALDOSServices
    {
       

        private readonly IPRE_V_SALDOSRepository _repository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        
        public PRE_V_SALDOSServices(IPRE_V_SALDOSRepository repository,
                                    IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository,

                                      IMapper mapper,
                                      IConfiguration configuration)
        {
            _repository = repository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
            _configuration = configuration;
            _mapper = mapper;
        }


        public async Task<ResultDto<List<PreDenominacionPorPartidaDto>>> GetPreVDenominacionPuc(FilterPreDenominacionDto filter)
        {
            var result = await _repository.GetPreVDenominacionPorPartidaPuc(filter);
            ExcelMapper mapper = new ExcelMapper();


            var settings = _configuration.GetSection("Settings").Get<Settings>();


            var ruta = @settings.ExcelFiles;  //@"/Users/freveron/Documents/MM/App/full-version/public/ExcelFiles";
            var fileName = $"ResumenDenominacionPUC Presupuesto {filter.CodigoPresupuesto}-{filter.FechaDesde.Year.ToString()}-{filter.FechaDesde.Month.ToString()}-{filter.FechaDesde.Day.ToString()} Hasta {filter.FechaHasta.Year.ToString()}-{filter.FechaHasta.Month.ToString()}-{filter.FechaHasta.Day.ToString()}.xlsx";
            string newFile = Path.Combine(Directory.GetCurrentDirectory(), ruta, fileName);


            mapper.Save(newFile, result.Data, $"ResumenDenominacionPUC ", true);
            return result;
        }


        public async Task<ResultDto<List<PreVSaldosGetDto>>> GetAllByPresupuestoIpcPuc(FilterPresupuestoIpcPuc filter)
        {
            var presupuesto = await _pRE_PRESUPUESTOSRepository.GetLast();
          
            if (filter.CodigoPresupuesto == 0) filter.CodigoPresupuesto = presupuesto.CODIGO_PRESUPUESTO;
            await _repository.RecalcularSaldo(filter.CodigoPresupuesto);

            ResultDto<List<PreVSaldosGetDto>> result = new ResultDto<List<PreVSaldosGetDto>>(null);
            try
            {
                var preVSaldos = await _repository.GetAllByPresupuesto(filter.CodigoPresupuesto);
                if (preVSaldos.Count() > 0)
                {
                    if (filter.CodigoIPC>0) {
                        preVSaldos = preVSaldos.Where(x => x.CODIGO_ICP == filter.CodigoIPC).ToList();
                    }
                    if (filter.CodigoPuc > 0)
                    {
                        preVSaldos = preVSaldos.Where(x => x.CODIGO_PUC == filter.CodigoPuc).ToList();
                    }
                    List<PreVSaldosGetDto> resultList = new List<PreVSaldosGetDto>();
                    foreach (var item in preVSaldos)
                    {

                        resultList.Add(await MapPRE_V_SADOSTOPreVSaldosGetDto(item));
                    }


                 

                    result.Data = resultList;

                    result.IsValid = true;
                    result.Message = "";
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = " No existen Datos";

                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;


        }

        public async Task<ResultDto<List<PreVSaldosGetDto>>> GetAll(FilterPRE_V_SALDOSDto filter)
        {
            var presupuesto = await _pRE_PRESUPUESTOSRepository.GetLast();
            await _repository.RecalcularSaldo(presupuesto.CODIGO_PRESUPUESTO);

            ResultDto<List<PreVSaldosGetDto>> result = new ResultDto<List<PreVSaldosGetDto>>(null);
            try
            {
                var pRE_V_SALDOs = await _repository.GetAll(filter);
                if (pRE_V_SALDOs.Count() > 0)
                {

                    List<PreVSaldosGetDto> resultList = new List<PreVSaldosGetDto>();
                    foreach (var item in pRE_V_SALDOs)
                    {

                        resultList.Add(await MapPRE_V_SADOSTOPreVSaldosGetDto(item));
                    }


                   /* var groupByDescripcionFinanciado =
                        from item in resultList
                        group item by item.DescripcionFinanciado;
                    var groupByDenominacionIcp =
                       from item in resultList
                       group item by item.DenominacionIcp;
                    var groupByunidadEjecutora =
                                           from item in resultList
                                           group item by item.UnidadEjecutora;*/

                    result.Data = resultList;

                    result.IsValid = true;
                    result.Message = "";
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = " No existen Datos";

                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }

        public async Task<PreVSaldosGetDto> MapPRE_V_SADOSTOPreVSaldosGetDto(PRE_V_SALDOS entity)
        {
            try
            {
                PreVSaldosGetDto dto = new PreVSaldosGetDto();
                dto.CodigoSaldo = entity.CODIGO_SALDO;
                dto.Ano = entity.ANO;
                dto.FinanciadoId = entity.FINANCIADO_ID;
                dto.CodigoFinanciado =entity.CODIGO_FINANCIADO;
                dto.DescripcionFinanciado = entity.DESCRIPCION_FINANCIADO;
                dto.CodigoIcp = entity.CODIGO_ICP;
                dto.CodigoSector = entity.CODIGO_SECTOR;
                dto.CodigoPrograma = entity.CODIGO_PROGRAMA;
                dto.CodigoSubPrograma = entity.CODIGO_SUBPROGRAMA;
                dto.CodigoProyecto = entity.CODIGO_PROYECTO;
                dto.CodigoActividad = entity.CODIGO_ACTIVIDAD;
                dto.CodigoOficina = entity.CODIGO_OFICINA;
                dto.CodigoIcpConcat = entity.CODIGO_ICP_CONCAT;
                dto.DenominacionIcp = entity.DENOMINACION_ICP;
                dto.UnidadEjecutora = entity.UNIDAD_EJECUTORA;
                dto.CodigoPuc = entity.CODIGO_PUC;
                dto.CodigoGrupo = entity.CODIGO_GRUPO;
                dto.CodigoPartida = entity.CODIGO_PARTIDA;
                dto.CodigoGenerica = entity.CODIGO_GENERICA;
                dto.CodigoEspecifica = entity.CODIGO_ESPECIFICA;
                dto.CodigoSubEspecifica = entity.CODIGO_SUBESPECIFICA;
                dto.CodigoNivel5 = entity.CODIGO_NIVEL5;
                dto.CodigoPucConcat = entity.CODIGO_PUC_CONCAT;
                dto.DenominacionPuc = entity.DENOMINACION_PUC;
                dto.Presupuestado = entity.PRESUPUESTADO;
                dto.Asignacion = entity.ASIGNACION;
                dto.Bloqueado = entity.BLOQUEADO;
                dto.Modificado = entity.MODIFICADO;
                dto.Ajustado = entity.AJUSTADO;
                dto.Vigente = entity.VIGENTE;
                dto.Comprometido = entity.COMPROMETIDO;
                dto.PorComprometido = entity.POR_COMPROMETIDO;
                dto.Disponible = entity.DISPONIBLE;
                dto.Causado = entity.CAUSADO;
                dto.PorCausado = entity.POR_CAUSADO;
                dto.Pagado = entity.PAGADO;
                dto.PorPagado = entity.POR_PAGADO;
                dto.CodigoEmpresa = entity.CODIGO_EMPRESA;
                dto.CodigoPresupuesto = entity.CODIGO_PRESUPUESTO;
                dto.FechaSolicitud = entity.FECHA_SOLICITUD;
                var presupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo((int)dto.CodigoEmpresa, (int)dto.CodigoPresupuesto);
                if (presupuesto != null)
                {
                    dto.DescripcionPresupuesto = presupuesto.DENOMINACION;
                }
                else
                {
                    dto.DescripcionPresupuesto = "";
                }



                return dto;
            }
            catch (Exception ex)
            {
                var Msg = ex.Message;
                return null;
            }

          


        }


        public async Task<ResultDto<List<PreSaldoPorPartidaGetDto>>> GetAllByPresupuestoPucConcat(FilterPresupuestoPucConcat filter)
        {
            ResultDto<List<PreSaldoPorPartidaGetDto>> result = new ResultDto<List<PreSaldoPorPartidaGetDto>>(null);
            try
            {
              
                var pRE_V_SALDOs = await _repository.GetAllByPresupuestoPucConcat(filter);
                if (pRE_V_SALDOs.Count() > 0)
                {

                    var q = from s in pRE_V_SALDOs.OrderBy(x => x.CODIGO_SALDO).ToList()
                            group s by new { CodigoPresupuesto = s.CODIGO_PRESUPUESTO, CodigoPucConcat= s.CODIGO_PUC_CONCAT, DescripcionFinanciado =s.DESCRIPCION_FINANCIADO} into g
                            select new {
                                g.Key.CodigoPresupuesto,
                                g.Key.CodigoPucConcat,
                                g.Key.DescripcionFinanciado,
                                Presupuestado = g.Sum(s => s.PRESUPUESTADO),
                                Asignacion = g.Sum(s => s.ASIGNACION),
                                Modificado = g.Sum(s => s.MODIFICADO),
                                Bloqueado = g.Sum(s => s.BLOQUEADO),
                                Comprometido = g.Sum(s => s.COMPROMETIDO),
                                Causado = g.Sum(s => s.CAUSADO),
                                Pagado = g.Sum(s => s.PAGADO)
                            };
                   

                    List<PreSaldoPorPartidaGetDto> resultList = new List<PreSaldoPorPartidaGetDto>();
                    var id = 0;
                    decimal Presupuestado=0;
                    decimal Asignacion = 0;
                    decimal Modificado =0;
                    decimal Bloqueado =0;
                    decimal Comprometido =0 ;
                    decimal Causado =0;
                    decimal Pagado =0;
                    foreach (var item in q.ToList())
                    {
                            id++;
                            PreSaldoPorPartidaGetDto dto = new PreSaldoPorPartidaGetDto();
                            dto.Id = id; ;
                            dto.CodigoPresupuesto = (int)item.CodigoPresupuesto;
                            dto.CodigoPucConcat = item.CodigoPucConcat;
                            dto.DescripcionFinanciado = item.DescripcionFinanciado;
                            dto.Presupuestado = item.Presupuestado;
                            dto.Asignacion = item.Asignacion;
                            dto.Modificado = item.Modificado;
                            dto.Bloqueado = item.Bloqueado;
                            dto.Comprometido = item.Comprometido;
                            dto.Causado = item.Causado;
                            dto.Pagado = item.Pagado;
                            Presupuestado = Presupuestado + item.Presupuestado;
                            Asignacion = Asignacion + item.Asignacion;
                            Modificado = Modificado + item.Modificado;
                            Bloqueado = Bloqueado + item.Bloqueado;
                            Comprometido = Comprometido + item.Comprometido;
                            Causado = Causado + item.Causado;
                            Pagado = Pagado + item.Pagado;



                        resultList.Add(dto);
                       
                        
                    }


                    if (resultList.Count() > 0)
                    {
                        var totalExists = resultList.Where(x => x.DescripcionFinanciado == "TOTAL:").FirstOrDefault();
                        if (totalExists == null)
                        {
                            id++;
                            PreSaldoPorPartidaGetDto dtoTotal = new PreSaldoPorPartidaGetDto();
                            dtoTotal.Id = -1 ;
                            dtoTotal.CodigoPresupuesto = filter.CodigoPresupuesto;
                            dtoTotal.CodigoPucConcat = filter.CodigoPucConcat;
                            dtoTotal.DescripcionFinanciado = "TOTAL:";
                            dtoTotal.Presupuestado = Presupuestado;
                            dtoTotal.Asignacion = Asignacion;
                            dtoTotal.Modificado = Modificado;
                            dtoTotal.Bloqueado = Bloqueado;
                            dtoTotal.Comprometido = Comprometido;
                            dtoTotal.Causado = Causado;
                            dtoTotal.Pagado = Pagado;

                            resultList.Add(dtoTotal);
                        }

                        
                    }
                   




                    result.Data = resultList.OrderBy(x=>x.Id).ToList();

                    result.IsValid = true;
                    result.Message = "";
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = " No existen Datos";

                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }


       




            
    }
}

