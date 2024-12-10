using System.Globalization;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Utility;

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

       
        public async Task<PreAsignacionesDetalleGetDto> MapPreAsignacionDetalleDto(PRE_ASIGNACIONES_DETALLE asignacion)
        {
            PreAsignacionesDetalleGetDto result = new PreAsignacionesDetalleGetDto();
            result.CodigoIcpConcat = "";
            result.CodigoPucConcat = "";
            var preAsignacion = await _preAsignacionService.GetByCodigo(asignacion.CODIGO_ASIGNACION);
            if (preAsignacion.Data != null)
            {
                result.CodigoIcpConcat = preAsignacion.Data.CodigoIcpConcat;
                result.CodigoPucConcat = preAsignacion.Data.CodigoPucConcat;
            }
            
            result.CodigoAsignacion = asignacion.CODIGO_ASIGNACION;
            result.CodigoAsignacionDetalle = asignacion.CODIGO_ASIGNACION_DETALLE;
            result.FechaDesembolso = asignacion.FECHA_DESEMBOLSO;
            result.FechaDesembolsoString= Fecha.GetFechaString(asignacion.FECHA_DESEMBOLSO);
            result.FechaDesembolsoObj= Fecha.GetFechaDto(asignacion.FECHA_DESEMBOLSO);
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
            decimal totalDesembolso = 0;
            var asignacionDetalle = await _repository.GetAllByAsignacion(codigoAsignacion);
            if (asignacionDetalle.Count > 0)
            {
                foreach (var item in asignacionDetalle)
                {
                    totalDesembolso = totalDesembolso + item.MONTO;
                }
            }

            return totalDesembolso;
        }
        public async Task ActualizaTotalDesembolsoAsignacion(int codigoAsignacion)
        {
            var conectado = await _sisUsuarioRepository.GetConectado();
            decimal totalDesembolso = 0;
            totalDesembolso = await GetTotal(codigoAsignacion);
            var asignacionUpdated = await _preAsignacionesRepository.GetByCodigo(codigoAsignacion);
            asignacionUpdated.TOTAL_DESEMBOLSO = totalDesembolso;
            asignacionUpdated.ORDINARIO = totalDesembolso;
            asignacionUpdated.USUARIO_UPD = conectado.Usuario;
            asignacionUpdated.FECHA_UPD = DateTime.Now;
            await _preAsignacionesRepository.Update(asignacionUpdated);
        }

        public async Task<bool> PresupuestoPermiteDesembolso(int codigoPresupuesto)
        {
            var result = true;
            var ultimoPresupuesto = await _presupuestosService.GetUltimo();
            if (ultimoPresupuesto != null)
            {
                if (ultimoPresupuesto.CODIGO_PRESUPUESTO < codigoPresupuesto)
                {
                    result = false;
                }
            }

                return result;
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

                var presupuestoValido = await PresupuestoPermiteDesembolso(asignacion.Data.CodigoPresupuesto);
                if (!presupuestoValido)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"No puede realizar desembolso a un presupuesto menor a el actual!";
                    return result;
                }
                
                
                decimal totalDesembolso = 0;
                totalDesembolso = await GetTotal(entity.CodigoAsignacion);
                if (asignacion.Data.Presupuestado<totalDesembolso + entity.Monto)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Monto total de desembolso supera el presupuestado: {asignacion.Data.Presupuestado}";
                    return result;
                }

                PRE_ASIGNACIONES_DETALLE asignacionNew = new PRE_ASIGNACIONES_DETALLE();
                asignacionNew.CODIGO_ASIGNACION_DETALLE = await _repository.GetNextKey();
                asignacionNew.CODIGO_ASIGNACION = entity.CodigoAsignacion;
                //asignacionNew.FECHA_DESEMBOLSO= Convert.ToDateTime(entity.FechaDesembolsoString, CultureInfo.InvariantCulture);
               
                DateTime fecha = DateTime.ParseExact(entity.FechaDesembolsoString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                asignacionNew.FECHA_DESEMBOLSO = fecha;
                asignacionNew.CODIGO_PRESUPUESTO = asignacion.Data.CodigoPresupuesto ;
                asignacionNew.MONTO = entity.Monto;
                asignacionNew.NOTAS = entity.Notas;
                var conectado = await _sisUsuarioRepository.GetConectado();
                asignacionNew.CODIGO_EMPRESA = conectado.Empresa;
                asignacionNew.USUARIO_INS = conectado.Usuario;
                asignacionNew.FECHA_INS = DateTime.Now;
                asignacionNew.FECHA_UPD = null; 
                var created = await _repository.Add(asignacionNew);
                
               
                
                
                if (created.IsValid && created.Data!=null)
                {
                    await ActualizaTotalDesembolsoAsignacion(entity.CodigoAsignacion);
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
                var presupuestoValido = await PresupuestoPermiteDesembolso(asignacion.Data.CodigoPresupuesto);
                if (!presupuestoValido)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"No puede realizar desembolso a un presupuesto menor a el actual!";
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

                decimal totaldesembolso = 0;
                totaldesembolso = await GetTotal(entity.CodigoAsignacion);
                if (asignacion.Data.Presupuestado<(totaldesembolso -asignacionDetalle.MONTO)+ entity.Monto)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Monto total de desembolso supera el presupuestado: {asignacion.Data.Presupuestado}";
                    return result;
                }

             
                asignacionDetalle.CODIGO_PRESUPUESTO = asignacion.Data.CodigoPresupuesto ;
                //asignacionDetalle.FECHA_DESEMBOLSO= Convert.ToDateTime(entity.FechaDesembolsoString, CultureInfo.InvariantCulture);
                DateTime fecha = DateTime.ParseExact(entity.FechaDesembolsoString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                asignacionDetalle.FECHA_DESEMBOLSO = fecha;
                asignacionDetalle.CODIGO_PRESUPUESTO = asignacion.Data.CodigoPresupuesto ;
                asignacionDetalle.MONTO = entity.Monto;
                asignacionDetalle.NOTAS = entity.Notas;
                var conectado = await _sisUsuarioRepository.GetConectado();
                asignacionDetalle.CODIGO_EMPRESA = conectado.Empresa;
                asignacionDetalle.USUARIO_INS = conectado.Usuario;
                asignacionDetalle.FECHA_UPD = DateTime.Now;
                var created = await _repository.Update(asignacionDetalle);
                if (created.IsValid && created.Data!=null)
                {
                    await ActualizaTotalDesembolsoAsignacion(entity.CodigoAsignacion);
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
                await ActualizaTotalDesembolsoAsignacion(asignacionDetalle.CODIGO_ASIGNACION);
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