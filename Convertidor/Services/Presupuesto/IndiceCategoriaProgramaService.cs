using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.EntitiesDestino;
using Convertidor.Data.Interfaces;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Data.Repository.Presupuesto;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Services.Presupuesto;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using NPOI.SS.Formula.Functions;
using static NPOI.HSSF.Record.UnicodeString;

namespace Convertidor.Services
{
    public class IndiceCategoriaProgramaService: IIndiceCategoriaProgramaService
    {
        
        private readonly IPRE_INDICE_CAT_PRGRepository _repository;
        private readonly IIndiceCategoriaProgramaRepository _destinoRepository;
        private readonly IPRE_PRESUPUESTOSRepository _presupuesttoRepository;
        private readonly IOssConfigRepository _ossConfigRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IPRE_ASIGNACIONESRepository _PRE_ASIGNACIONESRepository;

        public IndiceCategoriaProgramaService(IPRE_INDICE_CAT_PRGRepository repository,
                                      IIndiceCategoriaProgramaRepository destinoRepository,
                                      IPRE_PRESUPUESTOSRepository presupuesttoRepository,
                                      IOssConfigRepository ossConfigRepository,
                                      IConfiguration configuration,
                                      IPRE_ASIGNACIONESRepository PRE_ASIGNACIONESRepository,
                                        IMapper mapper)
        {
            _repository = repository;
            _destinoRepository = destinoRepository;
            _presupuesttoRepository = presupuesttoRepository;
            _ossConfigRepository = ossConfigRepository;
            _configuration = configuration;
            _PRE_ASIGNACIONESRepository = PRE_ASIGNACIONESRepository;
            _mapper = mapper;
        }


        public async Task<ResultDto<IndiceCategoriaPrograma>> TransferirIndiceCategoriaProgramaPorCantidadDeDias(int dias)
        {

            ResultDto<IndiceCategoriaPrograma> result = new ResultDto<IndiceCategoriaPrograma>(null);

            List<IndiceCategoriaPrograma> destinoList = new List<IndiceCategoriaPrograma>();

            try
            {


                var historico = await _repository.GetAll();
                if (historico.ToList().Count > 0)
                {
                    await _destinoRepository.DeleteAll();

                    foreach (var item in historico)
                    {

                        var destinoNew = _mapper.Map<IndiceCategoriaPrograma>(item);

                        
                        //destinoNew.EXTRA1 = item.EXTRA1 ?? "";
                       // destinoNew.EXTRA2 = item.EXTRA2 ?? "";
                       // destinoNew.EXTRA3 = item.EXTRA3 ?? "";
                        destinoList.Add(destinoNew);

                    }
                    var adicionados = await _destinoRepository.Add(destinoList);
                    result.IsValid = true;
                    result.Message = $" {historico.ToList().Count} Registros Transferidos";
                    return result;
                }
                else
                {

                    result.IsValid = true;
                    result.Message = $" No existen Datos para transferir";
                    return result;

                }



            }
            catch (Exception ex)
            {

                result.IsValid = false;
                result.Message = ex.InnerException.Message;
                return result;

            }
        }


        public async Task<ResultDto<List<PreIndiceCategoriaProgramaticaGetDto>>> GetAllByCodigoPresupuesto(int codigoPresupuesto)
        {

            ResultDto<List<PreIndiceCategoriaProgramaticaGetDto>> result = new ResultDto<List<PreIndiceCategoriaProgramaticaGetDto>>(null);
            try
            {

                if (codigoPresupuesto == 0)
                {
                    var lastPresupuesto = await _presupuesttoRepository.GetLast();
                    if (lastPresupuesto != null) codigoPresupuesto = lastPresupuesto.CODIGO_PRESUPUESTO;
                }
              

                var icp = await _repository.GetAllByCodigoPresupuesto(codigoPresupuesto);
                if (icp.ToList().Count > 0)
                {





                    var qicpDto = from s in icp.ToList()
                                  group s by new
                                  {
                                      CodigoIcp = s.CODIGO_ICP,
                                      Ano = s.ANO,
                                      Escenario = s.ESCENARIO,
                                      CodigoSector = s.CODIGO_SECTOR,
                                      CodigoPrograma = s.CODIGO_PROGRAMA,
                                      CodigoSubPrograma = s.CODIGO_SUBPROGRAMA,
                                      CodigoProyecto = s.CODIGO_PROYECTO,
                                      CodigoActividad = s.CODIGO_ACTIVIDAD,
                                      Denominacion = s.DENOMINACION,
                                      UnidadEjecutora = s.UNIDAD_EJECUTORA,
                                      Descripcion = s.DENOMINACION,
                                      CodigoFuncionario = s.CODIGO_FUNCIONARIO,
                                      FechaIni = s.FECHA_INI,
                                      FechaFin = s.FECHA_FIN,
                                      Extra1 = s.EXTRA1,
                                      Extra2 = s.EXTRA2,
                                      Extra3 = s.EXTRA3,
                                      CodigoOficina = s.CODIGO_OFICINA,
                                      CodigoPresupuesto = s.CODIGO_PRESUPUESTO,
                                      CodigoIcpPadre=s.CODIGO_ICP_FK
                                  } into g
                                  select new PreIndiceCategoriaProgramaticaGetDto
                                  {

                                      CodigoIcp = g.Key.CodigoIcp,
                                      Ano = g.Key.Ano,
                                      Escenario = g.Key.Escenario,
                                      CodigoSector = g.Key.CodigoSector,
                                      CodigoPrograma = g.Key.CodigoPrograma,
                                      CodigoSubPrograma = g.Key.CodigoSubPrograma,
                                      CodigoProyecto = g.Key.CodigoProyecto,
                                      CodigoActividad = g.Key.CodigoActividad,
                                      Denominacion = g.Key.Denominacion,
                                      UnidadEjecutora = g.Key.UnidadEjecutora,
                                      Descripcion = g.Key.Denominacion,
                                      CodigoFuncionario = g.Key.CodigoFuncionario,
                                      FechaIni = g.Key.FechaIni.ToString("u"),
                                      FechaFin = g.Key.FechaFin.ToString("u"),
                                      FechaIniDate = g.Key.FechaIni,
                                      FechaFinDate = g.Key.FechaFin,
                                      Extra1 = g.Key.Extra1,
                                      Extra2 = g.Key.Extra2,
                                      Extra3 = g.Key.Extra3,
                                      CodigoOficina = g.Key.CodigoOficina,
                                      CodigoPresupuesto = g.Key.CodigoPresupuesto,
                                      CodigoIcpPadre=g.Key.CodigoIcpPadre

                                  };


                    result.Data = qicpDto.OrderBy(x => x.CodigoSector)
                                         .ThenBy(x => x.CodigoPrograma)
                                         .ThenBy(x => x.CodigoSubPrograma)
                                         .ThenBy(x => x.CodigoProyecto)
                                         .ThenBy(x => x.CodigoActividad)
                                         .ThenBy(x => x.CodigoOficina)
                                         .ToList();
                    result.IsValid = true;
                    result.Message = $"";
                    return result;
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = $" No existen Datos";
                    return result;

                }



            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;

            }



        }

        public async Task<ResultDto<List<TreeICP>>> GetTreeByPresupuesto(int codigoPresupuesto)
        {

            List<TreeICP> listTreeICP = new List<TreeICP>();
            ResultDto<List<TreeICP>> result = new ResultDto<List<TreeICP>>(null);
            try
            {

                if (codigoPresupuesto==0)
                {
                    var lastPresupuesto = await _presupuesttoRepository.GetLast();
                    if (lastPresupuesto != null) codigoPresupuesto = lastPresupuesto.CODIGO_PRESUPUESTO;
                }
                //string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}", parent.Name, child.Name, grandChild.Name,grandChild.Id,grandChild.ParentId,grandChild.Denominacion,grandChild.Descripcion)
                var treeIcp = await WriteStrings(codigoPresupuesto);
                foreach (var item in treeIcp)
                {
                    var arraIcp = item.Split(":");
                    var id = arraIcp[3];
                    var parentId = arraIcp[4];
                    List<string> icpString = new List<string>();
                    if (id == parentId)
                    {
                        icpString.Add(arraIcp[0]);
                    }
                    else
                    {
                        icpString.Add(arraIcp[0]);
                        icpString.Add(arraIcp[1]);
                        icpString.Add(arraIcp[2]);
                    }
                    TreeICP treeICP = new TreeICP();
                    treeICP.Path = icpString;
                    treeICP.Id = Int32.Parse(id);
                    treeICP.Denominacion = arraIcp[5];
                    treeICP.Descripcion = arraIcp[6];
                    treeICP.UnidadEjecutora = arraIcp[7];
                    listTreeICP.Add(treeICP);



                }

                result.Data = listTreeICP;
                result.IsValid = true;
                result.Message = $"";
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

        public async Task<ResultDto<List<PreIndiceCategoriaProgramaticaGetDto>>> UpdateIcpPadre(int codigoPresupuesto)
        {

            ResultDto<List<PreIndiceCategoriaProgramaticaGetDto>> result = new ResultDto<List<PreIndiceCategoriaProgramaticaGetDto>>(null);
            try
            {


                if (codigoPresupuesto == 0)
                {
                    var lastPresupuesto = await _presupuesttoRepository.GetLast();
                    if (lastPresupuesto != null) codigoPresupuesto = lastPresupuesto.CODIGO_PRESUPUESTO;
                }

                var icp = await GetAllByCodigoPresupuesto(codigoPresupuesto);

                var presupuestoObj = await _presupuesttoRepository.GetByCodigo(13,codigoPresupuesto);

                var icpList = icp.Data.Where(x => x.CodigoIcpPadre == null || x.CodigoIcpPadre == 0).ToList();


                if (icpList.Count > 0)
                {

                    foreach (var item in icpList)
                    {
                        var icpObj = await _repository.GetByCodigo(item.CodigoIcp);
                        if (icpObj != null)
                        {
                            PRE_INDICE_CAT_PRG padre = new PRE_INDICE_CAT_PRG();

                            if (item.CodigoActividad != "00")
                            {
                                padre = await _repository.GetHastaProyecto(presupuestoObj.ANO,
                                                                           icpObj.CODIGO_ICP,
                                                                           icpObj.CODIGO_SECTOR,
                                                                           icpObj.CODIGO_PROGRAMA,
                                                                           icpObj.CODIGO_SUBPROGRAMA,
                                                                           icpObj.CODIGO_PROYECTO);
                            }
                            else if (item.CodigoProyecto != "00")
                            {
                                padre = await _repository.GetHastaSubPrograma(presupuestoObj.ANO,
                                                                          icpObj.CODIGO_ICP,
                                                                          icpObj.CODIGO_SECTOR,
                                                                          icpObj.CODIGO_PROGRAMA,
                                                                          icpObj.CODIGO_SUBPROGRAMA);
                            }
                            else if (item.CodigoSubPrograma != "00")
                            {
                                padre = await _repository.GetHastaPrograma(presupuestoObj.ANO,
                                                                          icpObj.CODIGO_ICP,
                                                                          icpObj.CODIGO_SECTOR,
                                                                          icpObj.CODIGO_PROGRAMA);
                            }
                            else if (item.CodigoPrograma != "00")
                            {
                                padre = await _repository.GetHastaSector(presupuestoObj.ANO,
                                                                        icpObj.CODIGO_ICP,
                                                                        icpObj.CODIGO_SECTOR);

                            }
                            else if (item.CodigoSector != "00")
                            {
                                padre = await _repository.GetHastaSector(presupuestoObj.ANO,
                                                                        icpObj.CODIGO_ICP,
                                                                        icpObj.CODIGO_SECTOR);

                            }

                            if (padre != null)
                            {
                                icpObj.CODIGO_ICP_FK = padre.CODIGO_ICP;
                            }
                            else
                            {
                                icpObj.CODIGO_ICP_FK = icpObj.CODIGO_ICP;
                            }
                            await _repository.Update(icpObj);


                        }
                    }



                    result= await GetAllByCodigoPresupuesto(codigoPresupuesto);
                   
                    return result;
                }



                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = $" No existen Datos";
                    return result;

                }



            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;

            }



        }


        public async Task<ResultDto<List<PreIndiceCategoriaProgramaticaGetDto>>> UpdateIcpPadreBK(int codigoPresupuesto)
        {

            ResultDto<List<PreIndiceCategoriaProgramaticaGetDto>> result = new ResultDto<List<PreIndiceCategoriaProgramaticaGetDto>>(null);
            try
            {

                var presupuesto = await GetAllByCodigoPresupuesto(codigoPresupuesto);

                var presupuestoObj = await _presupuesttoRepository.GetByCodigo(13, codigoPresupuesto);

                var presupuestoList = presupuesto.Data.ToList();


                if (presupuestoList.Count > 0)
                {

                    var qSector = from s in presupuestoList
                                  group s by new
                                  {

                                      CodigoSector = s.CodigoSector,

                                  } into g
                                  select new PreIndiceCategoriaProgramaticaGetDto
                                  {


                                      CodigoSector = g.Key.CodigoSector,


                                  };
                    if (qSector != null)
                    {
                        foreach (var itemSectores in qSector.ToList())
                        {
                            var listSectores = presupuestoList.Where(x => x.CodigoSector == itemSectores.CodigoSector).ToList();
                            var qPrograma = from s in listSectores
                                            group s by new
                                            {


                                                CodigoPrograma = s.CodigoPrograma

                                            } into g
                                            select new PreIndiceCategoriaProgramaticaGetDto
                                            {



                                                CodigoPrograma = g.Key.CodigoPrograma


                                            };
                            if (qPrograma != null)
                            {
                                foreach (var itemPrograma in qPrograma.ToList())
                                {
                                    var listPrograma = listSectores.Where(x => x.CodigoPrograma == itemPrograma.CodigoPrograma).ToList();
                                    var qSubPrograma = from s in listPrograma
                                                       group s by new
                                                       {


                                                           CodoSubPrograma = s.CodigoSubPrograma

                                                       } into g
                                                       select new PreIndiceCategoriaProgramaticaGetDto
                                                       {



                                                           CodigoSubPrograma = g.Key.CodoSubPrograma


                                                       };
                                    if (qSubPrograma != null)
                                    {

                                        foreach (var itemSubProgram in qSubPrograma.ToList())
                                        {
                                            var listSubPrograma = listPrograma.Where(x => x.CodigoSubPrograma == itemSubProgram.CodigoSubPrograma).ToList();
                                            var qProyecto = from s in listSubPrograma
                                                            group s by new
                                                            {


                                                                CodigoProyecto = s.CodigoProyecto

                                                            } into g
                                                            select new PreIndiceCategoriaProgramaticaGetDto
                                                            {



                                                                CodigoProyecto = g.Key.CodigoProyecto


                                                            };
                                            if (qProyecto != null)
                                            {
                                                foreach (var itemProyecto in qProyecto)
                                                {
                                                    var listProyecto = listPrograma.Where(x => x.CodigoProyecto == itemProyecto.CodigoProyecto).ToList();
                                                    var qActividad = from s in listProyecto
                                                                     group s by new
                                                                     {


                                                                         CodigoActividad = s.CodigoActividad

                                                                     } into g
                                                                     select new PreIndiceCategoriaProgramaticaGetDto
                                                                     {



                                                                         CodigoActividad = g.Key.CodigoActividad


                                                                     };
                                                    if (qActividad != null)
                                                    {
                                                        foreach (var itemActividad in qActividad)
                                                        {
                                                            var listActividad = listProyecto.Where(x => x.CodigoActividad == itemActividad.CodigoActividad).ToList();
                                                            var qOficina = from s in listActividad
                                                                           group s by new
                                                                           {


                                                                               CodigoOficina = s.CodigoOficina

                                                                           } into g
                                                                           select new PreIndiceCategoriaProgramaticaGetDto
                                                                           {



                                                                               CodigoOficina = g.Key.CodigoOficina


                                                                           };
                                                            if (qOficina != null)
                                                            {
                                                                foreach (var itemOficina in qOficina)
                                                                {
                                                                    var listOficina = listActividad.Where(x => x.CodigoOficina == itemOficina.CodigoOficina).ToList();
                                                                    foreach (var itemOficinaUpdate in listOficina)
                                                                    {
                                                                        var oficina = await _repository.GetByCodigo(itemOficinaUpdate.CodigoIcp);
                                                                        if (oficina != null)
                                                                        {
                                                                            PRE_INDICE_CAT_PRG padre = new PRE_INDICE_CAT_PRG();

                                                                            if (itemOficinaUpdate.CodigoActividad != "00")
                                                                            {
                                                                                padre = await _repository.GetHastaProyecto(presupuestoObj.ANO,
                                                                                                                           oficina.CODIGO_ICP,
                                                                                                                           oficina.CODIGO_SECTOR,
                                                                                                                           oficina.CODIGO_PROGRAMA,
                                                                                                                           oficina.CODIGO_SUBPROGRAMA,
                                                                                                                           oficina.CODIGO_PROYECTO);
                                                                            }
                                                                            else if (itemOficinaUpdate.CodigoProyecto != "00")
                                                                            {
                                                                                padre = await _repository.GetHastaSubPrograma(presupuestoObj.ANO,
                                                                                                                          oficina.CODIGO_ICP,
                                                                                                                          oficina.CODIGO_SECTOR,
                                                                                                                          oficina.CODIGO_PROGRAMA,
                                                                                                                          oficina.CODIGO_SUBPROGRAMA);
                                                                            }
                                                                            else if (itemOficinaUpdate.CodigoSubPrograma != "00")
                                                                            {
                                                                                padre = await _repository.GetHastaPrograma(presupuestoObj.ANO,
                                                                                                                          oficina.CODIGO_ICP,
                                                                                                                          oficina.CODIGO_SECTOR,
                                                                                                                          oficina.CODIGO_PROGRAMA);
                                                                            }
                                                                            else if (itemOficinaUpdate.CodigoPrograma != "00")
                                                                            {
                                                                                padre = await _repository.GetHastaSector(presupuestoObj.ANO,
                                                                                                                        oficina.CODIGO_ICP,
                                                                                                                        oficina.CODIGO_SECTOR);

                                                                            }
                                                                            else if (itemOficinaUpdate.CodigoSector != "00")
                                                                            {
                                                                                padre = await _repository.GetHastaSector(presupuestoObj.ANO,
                                                                                                                        oficina.CODIGO_ICP,
                                                                                                                        oficina.CODIGO_SECTOR);

                                                                            }

                                                                            if (padre != null)
                                                                            {
                                                                                oficina.EXTRA3 = padre.CODIGO_ICP.ToString();
                                                                            }
                                                                            else
                                                                            {
                                                                                oficina.EXTRA3 = oficina.CODIGO_ICP.ToString();
                                                                            }
                                                                            await _repository.Update(oficina);


                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }




                    result = await GetAllByCodigoPresupuesto(codigoPresupuesto);

                    return result;
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = $" No existen Datos";
                    return result;

                }



            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;

            }



        }


        public PreIndiceCategoriaProgramaticaGetDto MapIcpToDto(PRE_INDICE_CAT_PRG entity)
        {
            PreIndiceCategoriaProgramaticaGetDto result = new PreIndiceCategoriaProgramaticaGetDto();

            result.CodigoIcp = entity.CODIGO_ICP;
            result.Ano = entity.ANO;
            result.Escenario = entity.ESCENARIO;
            result.CodigoSector = entity.CODIGO_SECTOR;
            result.CodigoPrograma = entity.CODIGO_PROGRAMA;
            result.CodigoSubPrograma = entity.CODIGO_SUBPROGRAMA;
            result.CodigoProyecto = entity.CODIGO_PROYECTO;
            result.CodigoActividad = entity.CODIGO_ACTIVIDAD;
            result.Denominacion = entity.DENOMINACION;
            result.UnidadEjecutora = entity.UNIDAD_EJECUTORA;
            result.Descripcion = entity.DESCRIPCION;
            result.CodigoFuncionario = entity.CODIGO_FUNCIONARIO;
            result.FechaIni = entity.FECHA_INI.ToString("u");
            result.FechaFin = entity.FECHA_FIN.ToString("u");
            result.FechaIniDate = entity.FECHA_INI;
            result.FechaFinDate = entity.FECHA_FIN;
            result.Extra1 = entity.EXTRA1;
            result.Extra2 = entity.EXTRA2;
            result.Extra3 = entity.EXTRA3;
            result.CodigoOficina = entity.CODIGO_OFICINA;
            result.CodigoPresupuesto = entity.CODIGO_PRESUPUESTO;

            return result;

        }

        public async Task<ResultDto<PreIndiceCategoriaProgramaticaGetDto>> Update(PreIndiceCategoriaProgramaticaUpdateDto dto)
        {
            ResultDto<PreIndiceCategoriaProgramaticaGetDto> result = new ResultDto<PreIndiceCategoriaProgramaticaGetDto>(null);
            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var empresString = @settings.EmpresaConfig;
            var empresa = Int32.Parse(empresString);

            var icp = await _repository.GetByCodigo(dto.CodigoIcp);
            if (icp == null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "No existe Codigo ICP";
                return result;
            }

            result = await ValidateDto(dto);
            if (result.IsValid == false) return result;

            var presupuesto = await _presupuesttoRepository.GetByCodigo(empresa, dto.CodigoPresupuesto);

            icp.ANO = presupuesto.ANO;
            icp.ESCENARIO = dto.Escenario;
            icp.CODIGO_SECTOR = dto.CodigoSector;
            icp.CODIGO_PROGRAMA = dto.CodigoPrograma;
            icp.CODIGO_SUBPROGRAMA = dto.CodigoSubPrograma;
            icp.CODIGO_PROYECTO = dto.CodigoProyecto;
            icp.CODIGO_ACTIVIDAD = dto.CodigoActividad;
            icp.DENOMINACION = dto.Denominacion;
            icp.UNIDAD_EJECUTORA = dto.UnidadEjecutora;
            icp.DESCRIPCION = dto.Descripcion;
            icp.CODIGO_FUNCIONARIO = dto.CodigoFuncionario;
            icp.CODIGO_OFICINA = dto.CodigoOficina;
            icp.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
            icp.CODIGO_ICP_FK = dto.CodigoIcpPadre;
            var  updated = await _repository.Update(icp);
            if (updated.Data != null)
            {
                result.Data = MapIcpToDto(updated.Data);
                result.IsValid = updated.IsValid;
                result.Message = updated.Message;
            }
            else
            {
                result.Data = null;
                result.IsValid = updated.IsValid;
                result.Message = updated.Message;
            }
            

            return result;
         }

        public async Task<ResultDto<PreIndiceCategoriaProgramaticaGetDto>> Create(PreIndiceCategoriaProgramaticaUpdateDto dto)
        {
            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var empresString = @settings.EmpresaConfig;
            var empresa = Int32.Parse(empresString);

            ResultDto<PreIndiceCategoriaProgramaticaGetDto> result = new ResultDto<PreIndiceCategoriaProgramaticaGetDto>(null);

            var icp = await _repository.GetByCodigo(dto.CodigoIcp);
            if (icp != null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "Existe Codigo ICP";
                return result;
            }

            result = await ValidateDto(dto);
            if (result.IsValid == false) return result;

            PRE_INDICE_CAT_PRG icpNew = new PRE_INDICE_CAT_PRG();
            var presupuesto = await _presupuesttoRepository.GetByCodigo(empresa,dto.CodigoPresupuesto);
            icpNew.ANO = presupuesto.ANO;
            icpNew.ESCENARIO = dto.Escenario;
            icpNew.CODIGO_SECTOR = dto.CodigoSector;
            icpNew.CODIGO_PROGRAMA = dto.CodigoPrograma;
            icpNew.CODIGO_SUBPROGRAMA = dto.CodigoSubPrograma;
            icpNew.CODIGO_PROYECTO = dto.CodigoProyecto;
            icpNew.CODIGO_ACTIVIDAD = dto.CodigoActividad;
            icpNew.DENOMINACION = dto.Denominacion;
            icpNew.UNIDAD_EJECUTORA = dto.UnidadEjecutora;
            icpNew.DESCRIPCION = dto.Descripcion;
            icpNew.CODIGO_FUNCIONARIO = dto.CodigoFuncionario;
            icpNew.CODIGO_OFICINA = dto.CodigoOficina;
            icpNew.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
            icpNew.CODIGO_ICP_FK = dto.CodigoIcpPadre;
            var created = await _repository.Create(icpNew);
            if (created.Data != null)
            {
                result.Data = MapIcpToDto(created.Data);
                result.IsValid = created.IsValid;
                result.Message = created.Message;
            }
            else
            {
                result.Data = null;
                result.IsValid = created.IsValid;
                result.Message = created.Message;
            }


            return result;
        }


        public async Task DeleteByCodigoPresupuesto(int codigoPresupuesto)
        {
            var icps= await  GetAllByCodigoPresupuesto(codigoPresupuesto);
            if (icps!=null && icps.Data != null)
            {
                foreach (var item in icps.Data)
                {
                    DeletePreIcpDto dto = new DeletePreIcpDto();
                    dto.CodigoIcp = item.CodigoIcp;
                    await Delete(dto);
                }
            }

        }

        public async Task<ResultDto<DeletePreIcpDto>> Delete(DeletePreIcpDto dto)
        {

            ResultDto<DeletePreIcpDto> result = new ResultDto<DeletePreIcpDto>(null);
            try
            {

                var icp = await _repository.GetByCodigo(dto.CodigoIcp);
                if (icp == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "ICP no existe";
                    return result;
                }
                var listDto = await _repository.GetAllByCodigoPresupuesto(icp.CODIGO_PRESUPUESTO);
                var hijos = await ListarHijos(listDto, dto.CodigoIcp);
                if (hijos.Count > 0)
                {
                    foreach (var item in hijos)
                    {
                        var icpExiste = await _PRE_ASIGNACIONESRepository.ICPExiste(item);
                        if (icpExiste)
                        {
                            result.Data = dto;
                            result.IsValid = false;
                            result.Message = "ICP no puede ser eliminado,tiene Movimiento Creado";
                            return result;
                        }
                    }

                }
                else
                {
                    var icpExiste = await _PRE_ASIGNACIONESRepository.ICPExiste(dto.CodigoIcp);
                    if (icpExiste)
                    {
                        result.Data = dto;
                        result.IsValid = false;
                        result.Message = "ICP no puede ser eliminado,tiene Movimiento Creado";
                        return result;
                    }
                }
               

                var deleted = await _repository.Delete(dto.CodigoIcp);

                if (deleted.Length > 0)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = deleted;
                }
                else
                {
                    result.Data = dto;
                    result.IsValid = true;
                    result.Message = deleted;

                }


            }
            catch (Exception ex)
            {
                result.Data = dto;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }


      
      

        public async Task<List<int>> ListarHijos(List<PRE_INDICE_CAT_PRG> listDto, int codIcp)
        {

            List<int> result = new List<int>();
            try
            {

              

              

                var listHijos = listDto.Where(x => x.CODIGO_ICP_FK == codIcp).ToList();
                if (listHijos.Count > 0)
                {

                    foreach (var item in listHijos)
                    {
                        if (item.CODIGO_ICP==codIcp)
                        {
                            result.Add(item.CODIGO_ICP);
                        }
                        else
                        {
                            result.Add(item.CODIGO_ICP);
                            var newList = await ListarHijos(listDto, item.CODIGO_ICP);
                            if (newList.Count > 0)
                            {
                                result.AddRange(newList);
                            }
                           

                        }
                        
                    }
                }


                return result;
             


            }
            catch (Exception ex)
            {
                return null;
            }



            return result;
        }


        private IList<Item> GetChild(int id, IList<Item> items)
        {
            var childs = items
                .Where(x => x.ParentId == id || x.Id == id)
                .Union(items.Where(x => x.ParentId == id)
                .SelectMany(y => GetChild(y.Id, items)));

            return childs.ToList();
        }


        public async  Task<List<Item>> GetItems(int codigoPresupuesto)
        {
            List<Item> result = new List<Item>();
            var categoriasList = await _repository.GetAllByCodigoPresupuesto(codigoPresupuesto);

            foreach (var item in categoriasList)
            {
                Item itenNew = new Item();
                itenNew.Id = item.CODIGO_ICP;
                itenNew.ParentId = item.CODIGO_ICP_FK;
                var patchEvaluado = item.CODIGO_SECTOR + "-" + item.CODIGO_PROGRAMA + "-" + item.CODIGO_SUBPROGRAMA + "-" + item.CODIGO_PROYECTO + "-" + item.CODIGO_ACTIVIDAD + "-" + item.CODIGO_OFICINA;

                itenNew.Name = patchEvaluado;
                itenNew.Denominacion = item.DENOMINACION;
                itenNew.Descripcion = item.DESCRIPCION;
                itenNew.UnidadEjecutora = item.UNIDAD_EJECUTORA;
                result.Add(itenNew);

            }

            return result;
        }

        public async Task<List<string>> WriteStrings(int codigoPresupuesto)
        {

            List<string> result = new List<string>();
            List<Item> items = await  GetItems(codigoPresupuesto);
            IEnumerable<string> resultingStrings = from parent in items.Where(x => x.ParentId == x.Id)
                                                   join child in items on parent.Id equals child.ParentId
                                                   join grandChild in items on child.Id equals grandChild.ParentId
                                                   select string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}:{7}", parent.Name, child.Name, grandChild.Name,grandChild.Id,grandChild.ParentId,grandChild.Denominacion,grandChild.Descripcion, grandChild.UnidadEjecutora);
          
         

            foreach (var item in resultingStrings)
                result.Add(item);
            return result;
        }
      

   

        public async Task<ResultDto<PreIndiceCategoriaProgramaticaGetDto>> ValidateDto(PreIndiceCategoriaProgramaticaUpdateDto dto)
        {
            ResultDto<PreIndiceCategoriaProgramaticaGetDto> result = new ResultDto<PreIndiceCategoriaProgramaticaGetDto>(null);

          

            var icpCodigos = await _repository.GetByCodigos(dto);
            if (icpCodigos != null && icpCodigos.CODIGO_ICP != dto.CodigoIcp)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "Categorizacion existe en otro registro";
                return result;
            }
            if (dto.CodigoSector.Length != 2)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "CodigoSector debe ser de 2 digitos";
                return result;
            }
            if (dto.CodigoPrograma.Length != 2)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "CodigoPrograma debe ser de 2 digitos";
                return result;
            }
            if (dto.CodigoSubPrograma.Length != 2)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "CodigoSubPrograma debe ser de 2 digitos";
                return result;
            }
            if (dto.CodigoProyecto.Length != 2)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "CodigoProyecto debe ser de 2 digitos";
                return result;
            }
            if (dto.CodigoActividad.Length != 2)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "CodigoActividad debe ser de 2 digitos";
                return result;
            }
            if (dto.CodigoOficina.Length != 2)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "CodigoOficina debe ser de 2 digitos";
                return result;
            }
            result.Data = null;
            result.IsValid = true;
            result.Message = "";


            return result;
        }

        public async Task<ResultDto<List<PreCodigosIcp>>> ListCodigosValidosIcp()
        {
            ResultDto<List<PreCodigosIcp>> result = new ResultDto<List<PreCodigosIcp>>(null);

            List<PreCodigosIcp> resultList = new List<PreCodigosIcp>();
            var listSector = await _ossConfigRepository.GetListByClave("CODIGO_SECTOR");
            listSector = listSector.Where(x => x.VALOR == "01" ).ToList();
            var listPrograma= await _ossConfigRepository.GetListByClave("CODIGO_PROGRAMA");
            var listSubPrograma = await _ossConfigRepository.GetListByClave("CODIGO_SUBPROGRAMA");
            var listProyecto = await _ossConfigRepository.GetListByClave("CODIGO_PROYECTO");
            var listActividad = await _ossConfigRepository.GetListByClave("CODIGO_ACTIVIDAD");
            listActividad = listActividad.Where(x => Int32.Parse(x.VALOR) <= 60).ToList();
            var listOficina = await _ossConfigRepository.GetListByClave("CODIGO_OFICINA");

            foreach (var itemSector in listSector)
            {
                
                foreach (var itemPrograma in listPrograma)
                {
                    foreach (var itemSubPrograma in listSubPrograma)
                    {
                        foreach (var itemProyecto in listProyecto)
                        {
                            foreach (var itemActividad in listActividad)
                            {
                                foreach (var itemOficina in listOficina)
                                {
                                    PreCodigosIcp itemNew = new PreCodigosIcp();
                                    itemNew.CodigoSector = itemSector.VALOR;
                                    itemNew.CodigoPrograma = itemPrograma.VALOR;
                                    itemNew.CodigoSubPrograma = itemSubPrograma.VALOR;
                                    itemNew.CodigoProyecto = itemProyecto.VALOR;
                                    itemNew.CodigoActividad = itemActividad.VALOR;
                                    itemNew.CodigoOficina = itemOficina.VALOR;
                                    resultList.Add(itemNew);
                                }
                            }
                        }
                    }
                }
            }


            result.Data = resultList;
            result.IsValid = true;
            result.Message = "";


            return result;
        }

        public async Task<ResultDto<List<PreCodigosIcp>>> ListCodigosHistoricoIcp()
        {
            ResultDto<List<PreCodigosIcp>> result = new ResultDto<List<PreCodigosIcp>>(null);

            List<PreCodigosIcp> resultList = new List<PreCodigosIcp>();


            var icp = await _repository.GetAll();

                var qicpDto = from s in icp.ToList()
                              group s by new
                              {
                               
                                  CodigoSector = s.CODIGO_SECTOR,
                                  CodigoPrograma = s.CODIGO_PROGRAMA,
                                  CodigoSubPrograma = s.CODIGO_SUBPROGRAMA,
                                  CodigoProyecto = s.CODIGO_PROYECTO,
                                  CodigoActividad = s.CODIGO_ACTIVIDAD,
                               
                                  CodigoOficina = s.CODIGO_OFICINA,
                                 
                              } into g
                              select new PreCodigosIcp
                              {

                               
                                  CodigoSector = g.Key.CodigoSector,
                                  CodigoPrograma = g.Key.CodigoPrograma,
                                  CodigoSubPrograma = g.Key.CodigoSubPrograma,
                                  CodigoProyecto = g.Key.CodigoProyecto,
                                  CodigoActividad = g.Key.CodigoActividad,
                                  CodigoOficina = g.Key.CodigoOficina,
                                

                              };

            resultList = qicpDto.ToList();
            result.Data = resultList;
            result.IsValid = true;
            result.Message = "";


            return result;
        }

    }
}
