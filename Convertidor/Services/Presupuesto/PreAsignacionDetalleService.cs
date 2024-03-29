using System.Globalization;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using SkiaSharp.HarfBuzz;

namespace Convertidor.Services.Presupuesto;

public class PreAsignacionDetalleService: IPreAsignacionDetalleService
{
      
        private readonly IPreAsignacionesDetalleRepository _repository;
        private readonly IPreAsignacionService _preAsignacionService;
        private readonly IPRE_PRESUPUESTOSService _presupuestosService;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IPRE_ASIGNACIONESRepository _preAsignacionesRepository;

        public PreAsignacionDetalleService(    IPreAsignacionesDetalleRepository repository,
                                                IPreAsignacionService preAsignacionService,
                                                IPRE_PRESUPUESTOSService presupuestosService,
                                                ISisUsuarioRepository sisUsuarioRepository,
                                                IPRE_ASIGNACIONESRepository preAsignacionesRepository)
        {
            _repository = repository;
            _preAsignacionService = preAsignacionService;
            _presupuestosService = presupuestosService;
            _sisUsuarioRepository = sisUsuarioRepository;
            _preAsignacionesRepository = preAsignacionesRepository;
        }

        public FechaDto GetFechaDto(DateTime fecha)
        {
            var FechaDesdeObj = new FechaDto();
            FechaDesdeObj.Year = fecha.Year.ToString();
            string month = "00" + fecha.Month.ToString();
            string day = "00" + fecha.Day.ToString();
            FechaDesdeObj.Month = month.Substring(month.Length - 2);
            FechaDesdeObj.Day = day.Substring(day.Length - 2);

            return FechaDesdeObj;
        }
        public async Task<PreAsignacionesDetalleGetDto> MapPreAsignacionDetalleDto(PRE_ASIGNACIONES_DETALLE asignacion)
        {
            PreAsignacionesDetalleGetDto result = new PreAsignacionesDetalleGetDto();

            result.CodigoAsignacion = asignacion.CODIGO_ASIGNACION;
            result.CodigoAsignacionDetalle = asignacion.CODIGO_ASIGNACION_DETALLE;
            result.FechaDesembolso = asignacion.FECHA_DESEMBOLSO;
            result.FechaDesembolsoString= asignacion.FECHA_DESEMBOLSO.ToString("u");
            result.FechaDesembolsoObj= GetFechaDto(asignacion.FECHA_DESEMBOLSO);
            result.Monto = asignacion.MONTO;
            result.Notas = asignacion.NOTAS;
            return result;
        }

        public async Task<ResultDto<PreAsignacionesDetalleGetDto>> GetByCodigo(int codigo)
        {
            ResultDto<PreAsignacionesDetalleGetDto> result = new ResultDto<PreAsignacionesDetalleGetDto>(null);
            try
            {
                var asignacion = await _repository.GetByCodigo(codigo);
                if (asignacion != null)
                {
                    var dto = await MapPreAsignacionDetalleDto(asignacion);
                    result.Data = dto;
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

      

        public async Task<ResultDto<List<PreAsignacionesDetalleGetDto>>> GetAllByPresupuesto(PreAsignacionesDetalleFilterDto filterDto)
        {
            ResultDto<List<PreAsignacionesDetalleGetDto>> result = new ResultDto<List<PreAsignacionesDetalleGetDto>>(null);
            
            try
            {
                
                var asignacion = await _repository.GetAllByPresupuesto(filterDto.CodigoPresupuesto);
                if (asignacion != null && asignacion.Count>0)
                {
                    List<PreAsignacionesDetalleGetDto> list = new List<PreAsignacionesDetalleGetDto>();
                    foreach (var item in asignacion)
                    {
                        list.Add(await MapPreAsignacionDetalleDto(item));
                    }
                   
                    result.Data =list;
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

        public async Task<ResultDto<List<PreAsignacionesDetalleGetDto>>> GetAllByAsignacion(PreAsignacionesDetalleFilterDto filter)
        {
            ResultDto<List<PreAsignacionesDetalleGetDto>> result = new ResultDto<List<PreAsignacionesDetalleGetDto>>(null);
            
            try
            {
                var asignacion = await _repository.GetAllByAsignacion(filter.CodigoAsignacion);
                if (asignacion != null && asignacion.Count>0)
                {
                    List<PreAsignacionesDetalleGetDto> list = new List<PreAsignacionesDetalleGetDto>();
                    foreach (var item in asignacion)
                    {
                        list.Add(await MapPreAsignacionDetalleDto(item));
                    }
                   
                    result.Data =list;
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
        public async Task<decimal> GetTotal(int codigoAsignacion)
        {
            var conectado = await _sisUsuarioRepository.GetConectado();
            decimal totalReembolso = 0;
            var asignacionDetalle = await _repository.GetAllByAsignacion(codigoAsignacion);
            if (asignacionDetalle.Count > 0)
            {
                foreach (var item in asignacionDetalle)
                {
                    totalReembolso = totalReembolso + item.MONTO;
                }
            }

            return totalReembolso;
        }
        public async Task ActualizaTotalReembolsoAsignacion(int codigoAsignacion)
        {
            var conectado = await _sisUsuarioRepository.GetConectado();
            decimal totalReembolso = 0;
            totalReembolso = await GetTotal(codigoAsignacion);
            var asignacionUpdated = await _preAsignacionesRepository.GetByCodigo(codigoAsignacion);
            asignacionUpdated.TOTAL_DESEMBOLSO = totalReembolso;
            asignacionUpdated.USUARIO_UPD = conectado.Usuario;
            asignacionUpdated.FECHA_UPD = DateTime.Now;
            await _preAsignacionesRepository.Update(asignacionUpdated);
        }
        public async Task<ResultDto<PreAsignacionesDetalleGetDto>> Add(PreAsignacionesDetalleUpdateDto entity)
        {
            ResultDto<PreAsignacionesDetalleGetDto> result = new ResultDto<PreAsignacionesDetalleGetDto>(null);
            try
            {
                if (entity.Monto <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Presupuestado Invalido";
                    return result;
                }
                if (entity.Notas.Length == 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Notas  Invalido";
                    return result;
                }
              


                var asignacion = await _preAsignacionService.GetByCodigo(entity.CodigoAsignacion);
                if (asignacion.Data == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Asignacion Invalido";
                    return result;
                }
                decimal totalReembolso = 0;
                totalReembolso = await GetTotal(entity.CodigoAsignacion);
                if (asignacion.Data.Presupuestado<totalReembolso + entity.Monto)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Monto total de reembolso supera el presupuestado: {asignacion.Data.Presupuestado}";
                    return result;
                }

                PRE_ASIGNACIONES_DETALLE asignacionNew = new PRE_ASIGNACIONES_DETALLE();
                asignacionNew.CODIGO_ASIGNACION_DETALLE = await _repository.GetNextKey();
                asignacionNew.CODIGO_ASIGNACION = entity.CodigoAsignacion;
                asignacionNew.FECHA_DESEMBOLSO= Convert.ToDateTime(entity.FechaDesembolsoString, CultureInfo.InvariantCulture);
                asignacionNew.CODIGO_PRESUPUESTO = asignacion.Data.CodigoPresupuesto ;
                asignacionNew.MONTO = entity.Monto;
                asignacionNew.NOTAS = entity.Notas;
                var conectado = await _sisUsuarioRepository.GetConectado();
                asignacionNew.CODIGO_EMPRESA = conectado.Empresa;
                asignacionNew.USUARIO_INS = conectado.Usuario;
                asignacionNew.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(asignacionNew);
                
               
                
                
                if (created.IsValid && created.Data!=null)
                {
                    await ActualizaTotalReembolsoAsignacion(entity.CodigoAsignacion);
                    var resultDto = await MapPreAsignacionDetalleDto(created.Data);
                    result.Data = resultDto;
                    result.IsValid = true;
                    result.Message = "";


                }
                else
                {
                    
                    result.Data = null;
                    result.IsValid = created.IsValid;
                    result.Message = created.Message;
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

        public async Task<ResultDto<PreAsignacionesDetalleGetDto>> Update(PreAsignacionesDetalleUpdateDto entity)
        {
             ResultDto<PreAsignacionesDetalleGetDto> result = new ResultDto<PreAsignacionesDetalleGetDto>(null);
            try
            {
                var asignacionDetalle = await _repository.GetByCodigo(entity.CodigoAsignacionDetalle);
                if (asignacionDetalle == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No Existe Asignacion Detalle";
                    return result;
                    

                }
                var asignacion = await _preAsignacionService.GetByCodigo(entity.CodigoAsignacion);
                if (asignacion.Data == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No Existe Asignacion";
                    return result;
                    

                }
                if (entity.Monto <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Presupuestado Invalido";
                    return result;
                }
                if (entity.Notas.Length == 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Notas  Invalido";
                    return result;
                }

                decimal totalReembolso = 0;
                totalReembolso = await GetTotal(entity.CodigoAsignacion);
                if (asignacion.Data.Presupuestado<(totalReembolso -asignacionDetalle.MONTO)+ entity.Monto)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Monto total de reembolso supera el presupuestado: {asignacion.Data.Presupuestado}";
                    return result;
                }

             
                asignacionDetalle.CODIGO_PRESUPUESTO = asignacion.Data.CodigoPresupuesto ;
                asignacionDetalle.FECHA_DESEMBOLSO= Convert.ToDateTime(entity.FechaDesembolsoString, CultureInfo.InvariantCulture);
                asignacionDetalle.CODIGO_PRESUPUESTO = asignacion.Data.CodigoPresupuesto ;
                asignacionDetalle.MONTO = entity.Monto;
                asignacionDetalle.NOTAS = entity.Notas;
                var conectado = await _sisUsuarioRepository.GetConectado();
                asignacionDetalle.CODIGO_EMPRESA = conectado.Empresa;
                asignacionDetalle.USUARIO_INS = conectado.Usuario;
                asignacionDetalle.FECHA_INS = DateTime.Now;


                var created = await _repository.Update(asignacionDetalle);
                if (created.IsValid && created.Data!=null)
                {
                    await ActualizaTotalReembolsoAsignacion(entity.CodigoAsignacion);
                    var resultDto = await MapPreAsignacionDetalleDto(created.Data);
                    result.Data = resultDto;
                    result.IsValid = true;
                    result.Message = "";


                }
                else
                {
                    
                    result.Data = null;
                    result.IsValid = created.IsValid;
                    result.Message = created.Message;
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

        public async  Task<ResultDto<string>> Delete(PreAsignacionesDetalleDeleteDto deleteDto)
        {
            
            ResultDto<string> result = new ResultDto<string>(null);
            try
            {

                var asignacionDetalle = await _repository.GetByCodigo(deleteDto.CodigoAsignacionDetalle);
                if (asignacionDetalle == null)
                {
                    result.Data = "";
                    result.IsValid = false;
                    result.Message = "No Existe Asignacion Detalle";
                    return result;
                    

                }

              
                var deleted = await _repository.Delete(deleteDto.CodigoAsignacionDetalle);

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

        public async Task<bool> AsignacionExiste(int codigoAsignacion)
        {
            return await _repository.AsignacionExiste(codigoAsignacion);
        }

      
}