using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Cnt;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Utility;

namespace Convertidor.Services.Presupuesto
{
	public class PreCompromisosService: IPreCompromisosService
    {

      
        private readonly IPreCompromisosRepository _repository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;
        private readonly IPreDescriptivaRepository _repositoryPreDescriptiva;
        private readonly IAdmProveedoresRepository _admProveedoresRepository;
        private readonly IAdmSolicitudesRepository _admSolicitudesRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IConfiguration _configuration;
        public PreCompromisosService(IPreCompromisosRepository repository,
                                      IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository,
                                      IPreDescriptivaRepository repositoryPreDescriptiva,
                                      IAdmProveedoresRepository admProveedoresRepository,
                                      IAdmSolicitudesRepository admSolicitudesRepository,
                                      IConfiguration configuration,
                                      ISisUsuarioRepository sisUsuarioRepository
        )
		{
            _repository = repository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
            _configuration = configuration;
            _repositoryPreDescriptiva = repositoryPreDescriptiva;
            _admProveedoresRepository = admProveedoresRepository;
            _admSolicitudesRepository = admSolicitudesRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }


        public async Task<ResultDto<List<PreCompromisosResponseDto>>> GetAll()
        {

            ResultDto<List<PreCompromisosResponseDto>> result = new ResultDto<List<PreCompromisosResponseDto>>(null);
            try
            {

                var compromisos = await _repository.GetAll();

               

                if (compromisos.Count() > 0)
                {
                    List<PreCompromisosResponseDto> listDto = new List<PreCompromisosResponseDto>();

                    foreach (var item in compromisos)
                    {
                        var dto = await MapPreCompromisos(item);
                        listDto.Add(dto);
                    }


                    result.Data = listDto;

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



        
      

        public async Task<PreCompromisosResponseDto> MapPreCompromisos(PRE_COMPROMISOS dto)
        {
            PreCompromisosResponseDto itemResult = new PreCompromisosResponseDto();
            itemResult.CodigoCompromiso = dto.CODIGO_COMPROMISO;
            itemResult.Ano = dto.ANO;
            itemResult.CodigoSolicitud = dto.CODIGO_SOLICITUD;
            itemResult.NumeroCompromiso = dto.NUMERO_COMPROMISO;
            itemResult.FechaCompromiso = dto.FECHA_COMPROMISO;
            itemResult.FechaCompromisoString = Fecha.GetFechaString( dto.FECHA_COMPROMISO);
            FechaDto fechaCompromisoObj = Fecha.GetFechaDto(dto.FECHA_COMPROMISO);
            itemResult.FechaCompromisoObj = (FechaDto)fechaCompromisoObj;
            itemResult.CodigoProveedor = dto.CODIGO_PROVEEDOR;
            itemResult.FechaEntrega = dto.FECHA_ENTREGA;
            itemResult.FechaEntregaString = Fecha.GetFechaString( dto.FECHA_ENTREGA);
            FechaDto fechaEntregaObj = Fecha.GetFechaDto(dto.FECHA_ENTREGA);
            itemResult.FechaEntregaObj = (FechaDto) fechaEntregaObj;
            itemResult.CodigoDirEntrega = dto.CODIGO_DIR_ENTREGA;
            itemResult.TipoPagoId = dto.TIPO_PAGO_ID;
            itemResult.Extra1 = dto.EXTRA1;
            itemResult.Extra2 = dto.EXTRA2;
            itemResult.Extra3 = dto.EXTRA3;
            itemResult.CodigoPresupuesto = dto.CODIGO_PRESUPUESTO;
            itemResult.TipoRenglonId = dto.TIPO_RENGLON_ID;
            itemResult.NumeroOrden = dto.NUMERO_ORDEN;

            return itemResult;

        }


        public async Task<List<PreCompromisosResponseDto>> MapListPreCompromisosDto(List<PRE_COMPROMISOS> dtos)
        {
            List<PreCompromisosResponseDto> result = new List<PreCompromisosResponseDto>();


            foreach (var item in dtos)
            {

                PreCompromisosResponseDto itemResult = new PreCompromisosResponseDto();

                itemResult = await MapPreCompromisos(item);

                result.Add(itemResult);
            }
            return result;



        }

        public async Task<ResultDto<PreCompromisosResponseDto>> Update(PreCompromisosUpdateDto dto)
        {

            ResultDto<PreCompromisosResponseDto> result = new ResultDto<PreCompromisosResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if(dto.CodigoCompromiso < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Compromiso no existe";
                    return result;

                }
                var codigoCompromiso = await _repository.GetByCodigo(dto.CodigoCompromiso);
                if (codigoCompromiso == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Compromiso no existe";
                    return result;
                }
                if (dto.Ano < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Año Invalido";
                    return result;
                }

                if(dto.CodigoSolicitud < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitud Invalido";
                    return result;
                }
                var codigoSolicitud = await _admSolicitudesRepository.GetByCodigoSolicitud(dto.CodigoSolicitud);
                if (codigoSolicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitud Invalido";
                    return result;
                }

                if (dto.NumeroCompromiso == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero compromiso no puede estar vacio";
                    return result;

                }

                if (dto.NumeroCompromiso.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero compromiso Invalido";
                    return result;

                }

                if (dto.FechaCompromiso == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha compromiso Invalida";
                    return result;
                }
                
                
                if(dto.CodigoProveedor < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;

                }

                var codigoProveedor = await _admProveedoresRepository.GetByCodigo(dto.CodigoProveedor);
                if (codigoProveedor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;
                }

                if (dto.CodigoDirEntrega < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Dir entrega Invalido";
                    return result;
                }

                if (dto.Motivo.Length > 1150)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;
                }

                if (dto.Status.Length > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
                    return result;
                }
                if (dto.Extra1 is not null && dto.Extra1.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra1 Invalido";
                    return result;
                }
                if (dto.Extra2 is not null && dto.Extra2.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra2 Invalido";
                    return result;
                }

                if (dto.Extra3 is not null && dto.Extra3.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra3 Invalido";
                    return result;
                }

                if(dto.CodigoPresupuesto < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                var codigoPresupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (codigoPresupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

               

                codigoCompromiso.CODIGO_COMPROMISO = dto.CodigoCompromiso;
                codigoCompromiso.ANO = dto.Ano;
                codigoCompromiso.CODIGO_SOLICITUD = dto.CodigoSolicitud;
                codigoCompromiso.FECHA_COMPROMISO = dto.FechaCompromiso;
                codigoCompromiso.CODIGO_PROVEEDOR =dto.CodigoProveedor;
                codigoCompromiso.FECHA_ENTREGA =dto.FechaEntrega;
                codigoCompromiso.CODIGO_DIR_ENTREGA = dto.CodigoDirEntrega;
                codigoCompromiso.TIPO_PAGO_ID = dto.TipoPagoId;
                codigoCompromiso.MOTIVO = dto.Motivo;
                codigoCompromiso.STATUS = dto.Status;
                codigoCompromiso.EXTRA1 = dto.Extra1;
                codigoCompromiso.EXTRA2 = dto.Extra2;
                codigoCompromiso.EXTRA3 = dto.Extra3;
                codigoCompromiso.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                codigoCompromiso.TIPO_RENGLON_ID = dto.TipoRenglonId;
                codigoCompromiso.NUMERO_ORDEN= dto.NumeroOrden;


                codigoCompromiso.CODIGO_EMPRESA = conectado.Empresa;
                codigoCompromiso.USUARIO_UPD = conectado.Usuario;
                codigoCompromiso.FECHA_UPD = DateTime.Now;
                await _repository.Update(codigoCompromiso);

                var resultDto =await  MapPreCompromisos(codigoCompromiso);
                result.Data = resultDto;
                result.IsValid = true;
                result.Message = "";

            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }

        public async Task<ResultDto<PreCompromisosResponseDto>> Create(PreCompromisosUpdateDto dto)
        {

            ResultDto<PreCompromisosResponseDto> result = new ResultDto<PreCompromisosResponseDto>(null);
            try
            {

                var conectado = await _sisUsuarioRepository.GetConectado();



                if (dto.Ano < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Año Invalido";
                    return result;
                }

                if (dto.CodigoSolicitud < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitud Invalido";
                    return result;
                }
                var codigoSolicitud = await _admSolicitudesRepository.GetByCodigoSolicitud(dto.CodigoSolicitud);
                if (codigoSolicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitud Invalido";
                    return result;
                }

                if(dto.NumeroCompromiso == string.Empty) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero compromiso no puede estar vacio";
                    return result;

                }

                if(dto.NumeroCompromiso.Length > 20) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero compromiso Invalido";
                    return result;

                }
                if (dto.FechaCompromiso == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha compromiso Invalida";
                    return result;
                }


                if (dto.CodigoProveedor < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;

                }

                var codigoProveedor = await _admProveedoresRepository.GetByCodigo(dto.CodigoProveedor);
                if (codigoProveedor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Proveedor Invalido";
                    return result;
                }

                if (dto.CodigoDirEntrega < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Dir entrega Invalido";
                    return result;
                }

                if (dto.Motivo.Length > 1150)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Motivo Invalido";
                    return result;
                }

                if (dto.Status.Length > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
                    return result;
                }
                if (dto.Extra1 is not null && dto.Extra1.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra1 Invalido";
                    return result;
                }
                if (dto.Extra2 is not null && dto.Extra2.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra2 Invalido";
                    return result;
                }

                if (dto.Extra3 is not null && dto.Extra3.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra3 Invalido";
                    return result;
                }

                if (dto.CodigoPresupuesto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }

                var codigoPresupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (codigoPresupuesto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }



                PRE_COMPROMISOS entity = new PRE_COMPROMISOS();
                entity.CODIGO_COMPROMISO = await _repository.GetNextKey();
                entity.ANO = dto.Ano;
                entity.CODIGO_SOLICITUD = dto.CodigoSolicitud;
                entity.NUMERO_COMPROMISO = dto.NumeroCompromiso;
                entity.FECHA_COMPROMISO = dto.FechaCompromiso;
                entity.CODIGO_PROVEEDOR = dto.CodigoProveedor;
                entity.FECHA_ENTREGA = dto.FechaEntrega;
                entity.CODIGO_DIR_ENTREGA = dto.CodigoDirEntrega;
                entity.TIPO_PAGO_ID = dto.TipoPagoId;
                entity.MOTIVO = dto.Motivo;
                entity.STATUS = dto.Status;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                entity.TIPO_RENGLON_ID = dto.TipoRenglonId;
                entity.NUMERO_ORDEN = dto.NumeroOrden;

                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapPreCompromisos(created.Data);
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

                return result;



            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }
  


        public async Task<ResultDto<PreCompromisosDeleteDto>> Delete(PreCompromisosDeleteDto dto)
        {

            ResultDto<PreCompromisosDeleteDto> result = new ResultDto<PreCompromisosDeleteDto>(null);
            try
            {

                var codigoCompromiso = await _repository.GetByCodigo(dto.CodigoCompromiso);
                if (codigoCompromiso == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Compromiso no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoCompromiso);

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


        public async Task<ResultDto<PreCompromisosResponseDto>> GetByNumeroYFecha(string numeroCompromiso, DateTime fechaCompromiso)
        {
            ResultDto<PreCompromisosResponseDto> result = new ResultDto<PreCompromisosResponseDto>(null);
            try
            {

                var compromisos = await _repository.GetByNumeroYFecha(numeroCompromiso,fechaCompromiso);
                if (compromisos == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero compromiso y Fecha Compromiso incorrectos";
                    return result;
                }


                var resultDto = await MapPreCompromisos(compromisos);
                result.Data = resultDto;
                result.IsValid = true;
                result.Message = "";

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

