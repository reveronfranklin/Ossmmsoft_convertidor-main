using System.Globalization;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto;

public class PreAsignacionService: IPreAsignacionService
{
      
        private readonly IPRE_ASIGNACIONESRepository _repository;
        private readonly IPRE_PRESUPUESTOSRepository _presupuestosService;
        private readonly IIndiceCategoriaProgramaService _indiceCategoriaProgramaService;
        private readonly IPrePlanUnicoCuentasService _prePlanUnicoCuentasService;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IPRE_V_SALDOSRepository _preVSaldosRepository;
        private readonly IPreAsignacionesDetalleRepository _preAsignacionesDetalleRepository;

        public PreAsignacionService(    IPRE_ASIGNACIONESRepository repository,
                                        IPRE_PRESUPUESTOSRepository presupuestosService,
                                        IIndiceCategoriaProgramaService indiceCategoriaProgramaService,
                                        IPrePlanUnicoCuentasService prePlanUnicoCuentasService,
                                        ISisUsuarioRepository sisUsuarioRepository,
                                        IPRE_V_SALDOSRepository preVSaldosRepository,
                                        IPreAsignacionesDetalleRepository preAsignacionesDetalleRepository)
        {
            _repository = repository;
            _presupuestosService = presupuestosService;
            _indiceCategoriaProgramaService = indiceCategoriaProgramaService;
            _prePlanUnicoCuentasService = prePlanUnicoCuentasService;
            _sisUsuarioRepository = sisUsuarioRepository;
            _preVSaldosRepository = preVSaldosRepository;
            _preAsignacionesDetalleRepository = preAsignacionesDetalleRepository;
        }

        public async Task<PreAsignacionesGetDto> MapPreAsignacionDto(PRE_ASIGNACIONES asignacion,List<PreIndiceCategoriaProgramaticaGetDto> listIcp,List<PrePlanUnicoCuentasGetDto> listPuc)
        {
            PreAsignacionesGetDto result = new PreAsignacionesGetDto();

            result.CodigoAsignacion = asignacion.CODIGO_ASIGNACION;
            result.CodigoPresupuesto = asignacion.CODIGO_PRESUPUESTO;
            result.Año = asignacion.ANO;
            result.Presupuestado = asignacion.PRESUPUESTADO;
            result.Escenario = asignacion.ESCENARIO;
            result.CodigoIcp = asignacion.CODIGO_ICP;
            result.DenominacionIcp = "";
            var icp = listIcp.Where(x=>x.CodigoIcp==asignacion.CODIGO_ICP).FirstOrDefault();
            if (icp != null)
            {
                result.DenominacionIcp = icp.Denominacion;
                result.CodigoIcpConcat = icp.CodigoIcpConcat;
            }
            result.CodigoPuc = asignacion.CODIGO_PUC;
            result.DenominacionPuc = "";
            var puc = listPuc.Where(x => x.CodigoPuc == asignacion.CODIGO_PUC).FirstOrDefault();
            if (puc != null)
            {
                result.DenominacionPuc = puc.Denominacion;
                result.CodigoPucConcat = puc.CodigoPucConcat;
            }
            result.Ordinario = asignacion.ORDINARIO;
            result.Coordinado = asignacion.COORDINADO;
            result.Laee = asignacion.LAEE;
            result.Fides = asignacion.FIDES;
            result.Total = asignacion.ORDINARIO + asignacion.COORDINADO + asignacion.LAEE + asignacion.FIDES;
            result.SearchText = $"{result.CodigoIcpConcat}-{result.DenominacionIcp}-{result.DenominacionPuc}-{ result.CodigoPucConcat }";
            result.TotalDesembolso = asignacion.TOTAL_DESEMBOLSO;
            return result;
        }

        public async Task<ResultDto<PreAsignacionesGetDto>> UpdateField(UpdateFieldDto dto)
        {

            ResultDto<PreAsignacionesGetDto> result = new ResultDto<PreAsignacionesGetDto>(null);
            try
            {
                CultureInfo cultures = new CultureInfo("en-US");
              
               
                
                var asignacionUpdate = await _repository.GetByCodigo(dto.Id);
                if (asignacionUpdate == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Asignacion no existe";
                    return result;
                }

                var prevSaldo = await _preVSaldosRepository.PresupuestoExiste(asignacionUpdate.CODIGO_PRESUPUESTO);
                if (prevSaldo)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Presupuesto en ejecucion, No Puede ser modificado";
                    return result;
                }
                decimal valor;
                if (Decimal.TryParse(dto.Value, out valor))
                {
                    
                    decimal val = Convert.ToDecimal(dto.Value, cultures);
                    if (dto.Field == "presupuestado")
                    {
                        asignacionUpdate.PRESUPUESTADO = val;
                    }
                    if (dto.Field == "ordinario")
                    {
                        asignacionUpdate.ORDINARIO = (int)val;
                    }
                    if (dto.Field == "coordinado")
                    {
                        asignacionUpdate.COORDINADO = (int)val;
                    }
                    if (dto.Field == "fides")
                    {
                        asignacionUpdate.FIDES = (int)val;
                    }
                    if (dto.Field == "laee")
                    {
                        asignacionUpdate.LAEE = (int)val;
                    }


                }
                else
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Valor invalido";
                    return result;
                }
                decimal totalDesembolso= 0;
                var asignacionDetalle = await _preAsignacionesDetalleRepository.GetAllByAsignacion(dto.Id);
                if (asignacionDetalle.Count > 0)
                {
                    foreach (var item in asignacionDetalle)
                    {
                        totalDesembolso = totalDesembolso + item.MONTO;
                    }
                }

                asignacionUpdate.TOTAL_DESEMBOLSO = totalDesembolso;
                asignacionUpdate.ORDINARIO = totalDesembolso;
                asignacionUpdate.FECHA_UPD = DateTime.Now;
                await _repository.Update(asignacionUpdate);
                FilterByPresupuestoDto filterPresupuesto = new FilterByPresupuestoDto();
                filterPresupuesto.CodigoPresupuesto = asignacionUpdate
                    .CODIGO_PRESUPUESTO;
                var icp = await _indiceCategoriaProgramaService.GetAllByCodigoPresupuesto(filterPresupuesto);
                var puc = await _prePlanUnicoCuentasService.GetAllByCodigoPresupuesto(asignacionUpdate
                    .CODIGO_PRESUPUESTO);
                var resultDto =await  MapPreAsignacionDto(asignacionUpdate,icp.Data,puc.Data);
                result.Data = resultDto;
                result.IsValid = true;
                result.Message = "";
                result.LinkData = "";

            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                result.LinkData = "";
            }



            return result;
        }

        
        public async Task<ResultDto<PreAsignacionesGetDto>> GetByCodigo(int codigo)
        {
            ResultDto<PreAsignacionesGetDto> result = new ResultDto<PreAsignacionesGetDto>(null);
            try
            {
                var asignacion = await _repository.GetByCodigo(codigo);
                if (asignacion != null)
                {
                    FilterByPresupuestoDto filterPresupuesto = new FilterByPresupuestoDto();
                    filterPresupuesto.CodigoPresupuesto = asignacion
                        .CODIGO_PRESUPUESTO;
                    var icp = await _indiceCategoriaProgramaService.GetAllByCodigoPresupuesto(filterPresupuesto);
                    var puc = await _prePlanUnicoCuentasService.GetAllByCodigoPresupuesto(asignacion
                        .CODIGO_PRESUPUESTO);

                    var dto = await MapPreAsignacionDto(asignacion,icp.Data,puc.Data);
                    result.Data = dto;
                    result.IsValid = true;
                    result.Message = "";
                    result.LinkData = "";
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = " No existen Datos";
                    result.LinkData = "";

                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                result.LinkData = "";
            }
           

           
            return result;
        }



    public async Task<decimal> GetTotalAsignacionByIcpPuc(int codigoPresupuesto, int codigoIcp, int codigoPuc)
    {
        decimal totalAsignacion = 0;
        try
        {
            totalAsignacion = await _repository.GetTotalAsignacionByIcpPuc(codigoPresupuesto, codigoIcp, codigoPuc);
            return totalAsignacion;
        }
        catch (Exception ex)
        {
            var res = ex.InnerException.Message;
            return 0;
        }

    }

    public async Task<ResultDto<List<PreAsignacionesGetDto>>> GetAllByPresupuesto(PreAsignacionesFilterDto filterDto)
        {
            ResultDto<List<PreAsignacionesGetDto>> result = new ResultDto<List<PreAsignacionesGetDto>>(null);
            
            try
            {
                var asignacion = await _repository.GetAllByPresupuesto(filterDto.CodigoPresupuesto);
                if (asignacion != null && asignacion.Count>0)
                {
                    
                    FilterByPresupuestoDto filterPresupuesto = new FilterByPresupuestoDto();
                    filterPresupuesto.CodigoPresupuesto = filterDto.CodigoPresupuesto;
                    var icp = await _indiceCategoriaProgramaService.GetAllByCodigoPresupuesto(filterPresupuesto);
                    var puc = await _prePlanUnicoCuentasService.GetAllByCodigoPresupuesto(filterDto.CodigoPresupuesto);

                    
                    List<PreAsignacionesGetDto> list = new List<PreAsignacionesGetDto>();
                    foreach (var item in asignacion)
                    {
                        list.Add(await MapPreAsignacionDto(item,icp.Data,puc.Data));
                    }
                   
                    result.Data =list;
                    result.IsValid = true;
                    result.Message = "";
                    result.LinkData = "";
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = " No existen Datos";
                    result.LinkData = "";

                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                result.LinkData = "";
            }

            return result;


        }

        public async Task<ResultDto<List<PreAsignacionesGetDto>>> GetAllByIcp(PreAsignacionesFilterDto filter)
        {
            ResultDto<List<PreAsignacionesGetDto>> result = new ResultDto<List<PreAsignacionesGetDto>>(null);
            
            try
            {
                var asignacion = await _repository.GetAllByIcp(filter.CodigoPresupuesto,filter.CodigoIcp);
                if (asignacion != null && asignacion.Count>0)
                {
                    FilterByPresupuestoDto filterPresupuesto = new FilterByPresupuestoDto();
                    filterPresupuesto.CodigoPresupuesto = filter.CodigoPresupuesto;
                    var icp = await _indiceCategoriaProgramaService.GetAllByCodigoPresupuesto(filterPresupuesto);
                    var puc = await _prePlanUnicoCuentasService.GetAllByCodigoPresupuesto(filter.CodigoPresupuesto);

                    List<PreAsignacionesGetDto> list = new List<PreAsignacionesGetDto>();
                    foreach (var item in asignacion)
                    {
                        list.Add(await MapPreAsignacionDto(item,icp.Data,puc.Data));
                    }
                   
                    result.Data =list;
                    result.IsValid = true;
                    result.Message = "";
                    result.LinkData = "";
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = " No existen Datos";
                    result.LinkData = "";

                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                result.LinkData = "";
            }

            return result;

        }

        public async Task< ResultDto<List<PreAsignacionesGetDto>>> GetAllByIcpPuc(PreAsignacionesFilterDto filter)
        {
            ResultDto<List<PreAsignacionesGetDto>> result = new ResultDto<List<PreAsignacionesGetDto>>(null);
            
            try
            {
                var asignacion = await _repository.GetAllByIcpPuc(filter.CodigoPresupuesto,filter.CodigoIcp,filter.CodigoPuc);
                if (asignacion != null && asignacion.Count>0)
                {
                    FilterByPresupuestoDto filterPresupuesto = new FilterByPresupuestoDto();
                    filterPresupuesto.CodigoPresupuesto = filter.CodigoPresupuesto;
                    var icp = await _indiceCategoriaProgramaService.GetAllByCodigoPresupuesto(filterPresupuesto);
                    var puc = await _prePlanUnicoCuentasService.GetAllByCodigoPresupuesto(filter.CodigoPresupuesto);

                    List<PreAsignacionesGetDto> list = new List<PreAsignacionesGetDto>();
                    foreach (var item in asignacion)
                    {
                        list.Add(await MapPreAsignacionDto(item,icp.Data,puc.Data));
                    }
                   
                    result.Data =list;
                    result.IsValid = true;
                    result.Message = "";
                    result.LinkData = "";
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = " No existen Datos";
                    result.LinkData = "";

                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                result.LinkData = "";
            }

            return result;
        }

          public async Task<ResultDto<PreAsignacionesGetDto>> CreateListAsignaciones(PreAsignacionesExcel excel)
        {
            var conectado = await _sisUsuarioRepository.GetConectado();
            ResultDto<PreAsignacionesGetDto> result = new ResultDto<PreAsignacionesGetDto>(null);
            try
            {
                var validar = await ValidarListAsignaciones(excel);

                if (!validar.IsValid)
                {
                    return validar;
                }
                
                var prevSaldo = await _preVSaldosRepository.PresupuestoExiste(excel.CodigoPresupuesto);
                if (prevSaldo)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Presupuesto en ejecucion, No Puede ser modificado";
                    return result;
                }
                

                result.IsValid = true;
                foreach (var entity in excel.Asignaciones)
                {
                    PreAsignacionesUpdateDto dto = new PreAsignacionesUpdateDto();
                    dto.CodigoAsignacion = 0;
                    dto.CodigoPresupuesto = excel.CodigoPresupuesto;
                    dto.Año = 0;
                    dto.Escenario = 0;
                    dto.CodigoIcp = 0;
                    var icp = await _indiceCategoriaProgramaService.GetByIcpConcat(excel.CodigoPresupuesto,entity.CodigoIcpConcat);
                    if (icp != null)
                    {
                        dto.CodigoIcp = icp.CODIGO_ICP;
                    }
                    string[] pucList = entity.CodigoPucConcat.Split(".");
                    FilterPrePUCPresupuestoCodigos filter = new FilterPrePUCPresupuestoCodigos();
                    filter.CodigoPresupuesto = excel.CodigoPresupuesto;
                    filter.CodigoGrupo = pucList[0];
                    filter.CodicoNivel1 = pucList[1];
                    filter.CodicoNivel2 = pucList[2];
                    filter.CodicoNivel3 = pucList[3];
                    filter.CodicoNivel4 = pucList[4];
                    filter.CodicoNivel5 = pucList[5];
                    filter.CodicoNivel6 = pucList[6];
                    dto.CodigoPuc = 0;
                    var puc = await _prePlanUnicoCuentasService.GetByCodigos(filter);
                    if (puc != null)
                    {
                        dto.CodigoPuc = puc.CODIGO_PUC;
                    }
                    
                    dto.Presupuestado = entity.Presupuestado;
                    dto.Ordinario = entity.Ordinario;
                    dto.Coordinado = entity.Coordinado;
                    dto.Laee = entity.Laee;
                    dto.Fides = entity.Fides;
                    var created = await Add(dto);
                    result = created;

                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = $"{ex.Message}: Datos no cumplen con el formato requerido de Asignaciones ";
                result.LinkData = "";
            }

            return result;
        }
        
        
        public async Task<ResultDto<PreAsignacionesGetDto>> ValidarListAsignaciones(PreAsignacionesExcel excel)
        {
            var conectado = await _sisUsuarioRepository.GetConectado();
            ResultDto<PreAsignacionesGetDto> result = new ResultDto<PreAsignacionesGetDto>(null);
            try
            {
                var prevSaldo = await _preVSaldosRepository.PresupuestoExiste(excel.CodigoPresupuesto);
                if (prevSaldo)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Presupuesto en ejecucion, No Puede ser modificado";
                    return result;
                }
                

                result.IsValid = true;
                foreach (var entity in excel.Asignaciones)
                {
                    PreAsignacionesUpdateDto dto = new PreAsignacionesUpdateDto();
                    dto.CodigoAsignacion = 0;
                    dto.CodigoPresupuesto = excel.CodigoPresupuesto;
                    dto.Año = 0;
                    dto.Escenario = 0;
                    dto.CodigoIcp = 0;
                    var icp = await _indiceCategoriaProgramaService.GetByIcpConcat(excel.CodigoPresupuesto,entity.CodigoIcpConcat);
                    if (icp != null)
                    {
                        dto.CodigoIcp = icp.CODIGO_ICP;
                    }
                    string[] pucList = entity.CodigoPucConcat.Split(".");
                    FilterPrePUCPresupuestoCodigos filter = new FilterPrePUCPresupuestoCodigos();
                    filter.CodigoPresupuesto = excel.CodigoPresupuesto;
                    filter.CodigoGrupo = pucList[0];
                    filter.CodicoNivel1 = pucList[1];
                    filter.CodicoNivel2 = pucList[2];
                    filter.CodicoNivel3 = pucList[3];
                    filter.CodicoNivel4 = pucList[4];
                    filter.CodicoNivel5 = pucList[5];
                    filter.CodicoNivel6 = pucList[6];
                    dto.CodigoPuc = 0;
                    var puc = await _prePlanUnicoCuentasService.GetByCodigos(filter);
                    if (puc != null)
                    {
                        dto.CodigoPuc = puc.CODIGO_PUC;
                    }
                    
                    dto.Presupuestado = entity.Presupuestado;
                    dto.Ordinario = entity.Ordinario;
                    dto.Coordinado = entity.Coordinado;
                    dto.Laee = entity.Laee;
                    dto.Fides = entity.Fides;
                    var resultValidacion = await  ValidarAsignacion(dto);
                    if (resultValidacion.IsValid == false)
                    {
                       result.Message = $"{ result.Message } {entity.CodigoIcpConcat}-{entity.CodigoPucConcat} Error: {resultValidacion.Message}" ;
                       result.IsValid = false;
                    }
                   

                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = $"{ex.Message}: Datos no cumplen con el formato requerido de Asignaciones ";
                result.LinkData = "";
            }

            return result;
        }
        
        public async Task<ResultDto<PreAsignacionesGetDto>> ValidarAsignacion(PreAsignacionesUpdateDto entity)
        {
            var conectado = await _sisUsuarioRepository.GetConectado();
            ResultDto<PreAsignacionesGetDto> result = new ResultDto<PreAsignacionesGetDto>(null);
            try
            {
                if (entity.Presupuestado <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Presupuestado Invalido";
                    result.LinkData = "";
                    return result;
                }
                /*if (entity.Ordinario <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Ordinario Invalido";
                    result.LinkData = "";
                    return result;
                }*/
                
                var presupuesto = await _presupuestosService.GetByCodigo(conectado.Empresa, entity.CodigoPresupuesto);
                if (presupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Presupuesto Invalido";
                    result.LinkData = "";
                    return result;
                }
                var prevSaldo = await _preVSaldosRepository.PresupuestoExiste(entity.CodigoPresupuesto);
                if (prevSaldo)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Presupuesto en ejecucion, No Puede ser modificado";
                    return result;
                }

                var icp = await _indiceCategoriaProgramaService.GetByCodigo(entity.CodigoIcp);
                if (icp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "ICP Invalido";
                    result.LinkData = "";
                    return result;
                }
                var puc = await _prePlanUnicoCuentasService.GetById(entity.CodigoPuc);
                if (puc.IsValid == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "PUC Invalido";
                    result.LinkData = "";
                    return result;
                }

              /*  var asignacion =
                    await _repository.GetAllByIcpPuc(entity.CodigoPresupuesto, entity.CodigoIcp, entity.CodigoPuc);
                if (asignacion != null && asignacion.Count > 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ya existe registro para este Presupuesto,ICP,PUC";
                    result.LinkData = "";
                    return result;
                }*/
                
              
                result.Data = null;
                result.IsValid = true;
                result.Message = "";
                result.LinkData = "";
             
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                result.LinkData = "";
            }
            
            return result;
        }
        
        public async Task<ResultDto<PreAsignacionesGetDto>> Add(PreAsignacionesUpdateDto entity)
        {
            var conectado = await _sisUsuarioRepository.GetConectado();
            ResultDto<PreAsignacionesGetDto> result = new ResultDto<PreAsignacionesGetDto>(null);
            try
            {

                result = await  ValidarAsignacion(entity);
                if (result.IsValid == false)
                {
                    return result;
                }
             
                
                var presupuesto = await _presupuestosService.GetByCodigo(conectado.Empresa, entity.CodigoPresupuesto);
                if (presupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Presupuesto Invalido";
                    result.LinkData = "";
                    return result;
                }
                
                PRE_ASIGNACIONES asignacionNew = new PRE_ASIGNACIONES();
                asignacionNew.CODIGO_ASIGNACION = await _repository.GetNextKey();
                asignacionNew.CODIGO_PRESUPUESTO = entity.CodigoPresupuesto;
                asignacionNew.ESCENARIO = entity.Escenario;
                asignacionNew.CODIGO_ICP = entity.CodigoIcp;
                asignacionNew.CODIGO_PUC = entity.CodigoPuc;
                asignacionNew.PRESUPUESTADO = entity.Presupuestado;
                asignacionNew.ORDINARIO = entity.Ordinario;
                asignacionNew.FIDES = entity.Fides;
                asignacionNew.LAEE = entity.Laee;
                asignacionNew.COORDINADO = entity.Coordinado;
                asignacionNew.ANO = presupuesto.ANO;
               
                asignacionNew.CODIGO_EMPRESA = conectado.Empresa;
                asignacionNew.USUARIO_INS = conectado.Usuario;
                asignacionNew.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(asignacionNew);
                if (created.IsValid && created.Data!=null)
                {
                    FilterByPresupuestoDto filterPresupuesto = new FilterByPresupuestoDto();
                    filterPresupuesto.CodigoPresupuesto = entity.CodigoPresupuesto;
                    var icp = await _indiceCategoriaProgramaService.GetAllByCodigoPresupuesto(filterPresupuesto);
                    var puc = await _prePlanUnicoCuentasService.GetAllByCodigoPresupuesto(entity.CodigoPresupuesto);

                    var resultDto = await MapPreAsignacionDto(created.Data,icp.Data,puc.Data);
                    result.Data = resultDto;
                    result.IsValid = true;
                    result.Message = "";
                    result.LinkData = "";


                }
                else
                {
                    
                    result.Data = null;
                    result.IsValid = created.IsValid;
                    result.Message = created.Message;
                    result.LinkData = "";
                }
             
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                result.LinkData = "";
            }
           

           
            return result;
        }

        public async Task<ResultDto<PreAsignacionesGetDto>> Update(PreAsignacionesUpdateDto entity)
        {
            
            var conectado = await _sisUsuarioRepository.GetConectado();
             ResultDto<PreAsignacionesGetDto> result = new ResultDto<PreAsignacionesGetDto>(null);
            try
            {
                var asignacion = await _repository.GetByCodigo(entity.CodigoAsignacion);
                if (asignacion == null)
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = "No Existe Asignacion";
                    result.LinkData = "";
                    return result;
                    

                }
                result = await  ValidarAsignacion(entity);
                if (result.IsValid == false)
                {
                    return result;
                }
                
             
                var presupuesto = await _presupuestosService.GetByCodigo(conectado.Empresa, entity.CodigoPresupuesto);
                if (presupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = "Presupuesto Invalido";
                    result.LinkData = "";
                    return result;
                }
                
              
                decimal totalDesembolso = 0;
                var asignacionDetalle = await _preAsignacionesDetalleRepository.GetAllByAsignacion(entity.CodigoAsignacion);
                if (asignacionDetalle.Count > 0)
                {
                    foreach (var item in asignacionDetalle)
                    {
                        totalDesembolso = totalDesembolso + item.MONTO;
                    }
                }

                asignacion.TOTAL_DESEMBOLSO = totalDesembolso;
                asignacion.ORDINARIO = totalDesembolso;
             
                //asignacion.CODIGO_PRESUPUESTO = entity.CodigoPresupuesto;
                asignacion.ESCENARIO = entity.Escenario;
                //asignacion.CODIGO_ICP = entity.CodigoIcp;
                //asignacion.CODIGO_PUC = entity.CodigoPuc;
                asignacion.PRESUPUESTADO = entity.Presupuestado;
                asignacion.ORDINARIO = entity.Ordinario;
                asignacion.FIDES = entity.Fides;
                asignacion.LAEE = entity.Laee;
                asignacion.COORDINADO = entity.Coordinado;
                asignacion.ANO = presupuesto.ANO;
              
                asignacion.CODIGO_EMPRESA = conectado.Empresa;
                asignacion.USUARIO_UPD = conectado.Usuario;
                asignacion.FECHA_UPD = DateTime.Now;

                var created = await _repository.Update(asignacion);
                if (created.IsValid && created.Data!=null)
                {
                    
                    FilterByPresupuestoDto filterPresupuesto = new FilterByPresupuestoDto();
                    filterPresupuesto.CodigoPresupuesto = entity.CodigoPresupuesto;
                    var icp = await _indiceCategoriaProgramaService.GetAllByCodigoPresupuesto(filterPresupuesto);
                    var puc = await _prePlanUnicoCuentasService.GetAllByCodigoPresupuesto(entity.CodigoPresupuesto);

                    var resultDto = await MapPreAsignacionDto(created.Data,icp.Data,puc.Data);
                    result.Data = resultDto;
                    result.IsValid = true;
                    result.Message = "";
                    result.LinkData = "";


                }
                else
                {
                    
                    result.Data = null;
                    result.IsValid = created.IsValid;
                    result.Message = created.Message;
                    result.LinkData = "";
                }
             
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                result.LinkData = "";
            }
           

           
            return result;
        }

        public async  Task<ResultDto<string>> Delete(PreAsignacionesDeleteDto deleteDto)
        {
            
            ResultDto<string> result = new ResultDto<string>(null);
            try
            {

                var asignacion = await _repository.GetByCodigo(deleteDto.CodigoAsignacion);
                if (asignacion == null)
                {
                    result.Data = "";
                    result.IsValid = false;
                    result.Message = "No Existe Asignacion";
                    return result;
                    

                }
                var prevSaldo = await _preVSaldosRepository.PresupuestoExiste(asignacion.CODIGO_PRESUPUESTO);
                if (prevSaldo)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Presupuesto en ejecucion, No Puede ser Eliminado";
                    return result;
                }
              
                var deleted = await _repository.Delete(deleteDto.CodigoAsignacion);

                if (deleted.Length > 0)
                {
                    result.Data = "";
                    result.IsValid = false;
                    result.Message = deleted;
                }
                else
                {
                    result.Data = "";
                    result.IsValid = true;
                    result.Message = deleted;

                }

                return result;


            }
            catch (Exception ex)
            {
                result.Data = "";
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }
        }

        
        public async  Task<ResultDto<string>> DeleteByPresupuesto(int codigoPresupuesto)
        {
            
            ResultDto<string> result = new ResultDto<string>(null);
            try
            {

               
                var deleted = await _repository.DeleteByPresupuesto(codigoPresupuesto);

                if (deleted.Length > 0)
                {
                    result.Data = "";
                    result.IsValid = false;
                    result.Message = deleted;
                }
                else
                {
                    result.Data = "";
                    result.IsValid = true;
                    result.Message = deleted;

                }

                return result;


            }
            catch (Exception ex)
            {
                result.Data = "";
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }
        }
        public async Task<bool> PresupuestoExiste(int codPresupuesto)
        {
            return await _repository.PresupuestoExiste(codPresupuesto);
        }

        public async Task<bool> ICPExiste(int codigoICP)
        {
            return await _repository.ICPExiste(codigoICP);
        }

        public async Task<bool> PUCExiste(int codigoPUC)
        {
            return await _repository.PUCExiste(codigoPUC);
        }
}