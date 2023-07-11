using System.Globalization;
using AutoMapper;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.EntitiesDestino;
using Convertidor.Data.Interfaces;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Services.Presupuesto;
using static NPOI.HSSF.Record.UnicodeString;

namespace Convertidor.Services
{
    public class IndiceCategoriaProgramaService: IIndiceCategoriaProgramaService
    {
        
        private readonly IPRE_INDICE_CAT_PRGRepository _repository;
        private readonly IIndiceCategoriaProgramaRepository _destinoRepository;
        private readonly IPRE_PRESUPUESTOSRepository _presupuesttoRepository;
        private readonly IMapper _mapper;

        public IndiceCategoriaProgramaService(IPRE_INDICE_CAT_PRGRepository repository,
                                      IIndiceCategoriaProgramaRepository destinoRepository,
                                      IPRE_PRESUPUESTOSRepository presupuesttoRepository,
                                      IMapper mapper)
        {
            _repository = repository;
            _destinoRepository = destinoRepository;
            _presupuesttoRepository = presupuesttoRepository;
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

                if (codigoPresupuesto==0)
                {
                    var lastPresupuesto = await _presupuesttoRepository.GetLast();
                    if (lastPresupuesto != null) codigoPresupuesto = lastPresupuesto.CODIGO_PRESUPUESTO;
                }


                var icp = await _repository.GetAllByCodigoPresupuesto(codigoPresupuesto);
                if (icp.ToList().Count > 0)
                {

                    var qicpDto = from s in icp.ToList()
                            group s by new  { CodigoIcp = s.CODIGO_ICP,
                                              Ano=s.ANO,
                                              Escenario=s.ESCENARIO,
                                              CodigoSector=s.CODIGO_SECTOR,
                                              CodigoPrograma=s.CODIGO_PROGRAMA,
                                              CodigoSubPrograma= s.CODIGO_SUBPROGRAMA,
                                              CodigoProyecto=s.CODIGO_PROYECTO,
                                              CodigoActividad=s.CODIGO_ACTIVIDAD,
                                              Denominacion=s.DENOMINACION,
                                              UnidadEjecutora=s.UNIDAD_EJECUTORA,
                                              Descripcion=s.DENOMINACION,
                                              CodigoFuncionario=s.CODIGO_FUNCIONARIO,
                                              FechaIni=s.FECHA_INI,
                                              FechaFin=s.FECHA_FIN,
                                              Extra1=s.EXTRA1,
                                              Extra2 = s.EXTRA2,
                                              Extra3 = s.EXTRA3,
                                              CodigoOficina=s.CODIGO_OFICINA,
                                              CodigoPresupuesto = s.CODIGO_PRESUPUESTO} into g
                            select new PreIndiceCategoriaProgramaticaGetDto
                            {

                                CodigoIcp=g.Key.CodigoIcp,
                                Ano=g.Key.Ano,
                                Escenario= g.Key.Escenario,
                                CodigoSector= g.Key.CodigoSector,
                                CodigoPrograma= g.Key.CodigoPrograma,
                                CodigoSubPrograma= g.Key.CodigoSubPrograma,
                                CodigoProyecto= g.Key.CodigoProyecto,
                                CodigoActividad= g.Key.CodigoActividad,
                                Denominacion= g.Key.Denominacion,
                                UnidadEjecutora= g.Key.UnidadEjecutora,
                                Descripcion= g.Key.Denominacion,
                                CodigoFuncionario= g.Key.CodigoFuncionario,
                                FechaIni= g.Key.FechaIni.ToString("u"),
                                FechaFin = g.Key.FechaFin.ToString("u"),
                                FechaIniDate = g.Key.FechaIni,
                                FechaFinDate = g.Key.FechaFin,
                                Extra1 = g.Key.Extra1,
                                Extra2= g.Key.Extra2,
                                Extra3= g.Key.Extra3,
                                CodigoOficina= g.Key.CodigoOficina,
                                CodigoPresupuesto= g.Key.CodigoPresupuesto,
                                
                            };


                    result.Data = qicpDto.OrderBy(x=> x.CodigoSector)
                                         .ThenBy(x=>x.CodigoPrograma)
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

          
         
            icp.ANO = dto.Ano;
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

            icpNew.ANO = dto.Ano;
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


    }
}
