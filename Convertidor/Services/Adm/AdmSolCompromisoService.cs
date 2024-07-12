using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository.Adm;
using Convertidor.Dtos.Adm;
using Convertidor.Utility;

namespace Convertidor.Services.Adm
{
    public class AdmSolCompromisoService : IAdmSolCompromisoService
    {
        private readonly IAdmSolCompromisoRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IAdmSolicitudesService _admSolicitudesService;
        private readonly IAdmProveedoresRepository _admProveedoresRepository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;

        public AdmSolCompromisoService(IAdmSolCompromisoRepository repository,
                                       IAdmDescriptivaRepository admDescriptivaRepository,
                                       ISisUsuarioRepository sisUsuarioRepository,
                                       IAdmSolicitudesService admSolicitudesService,
                                       IAdmProveedoresRepository admProveedoresRepository,
                                       IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admSolicitudesService = admSolicitudesService;
            _admProveedoresRepository = admProveedoresRepository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
        }

        public async Task<AdmSolCompromisoResponseDto> MapSolCompromisoDto(ADM_SOL_COMPROMISO dtos)
        {
            AdmSolCompromisoResponseDto itemResult = new AdmSolCompromisoResponseDto();
            
                itemResult.CodigoSolCompromiso = dtos.CODIGO_SOL_COMPROMISO;
                itemResult.TipoSolCompromisoId = dtos.TIPO_SOL_COMPROMISO_ID;
                itemResult.NumeroSolicitud = dtos.NUMERO_SOLICITUD;
                itemResult.FechaSolicitud = dtos.FECHA_SOLICITUD;
                itemResult.FechaSolicitudString = Fecha.GetFechaString(dtos.FECHA_SOLICITUD);
                FechaDto fechaSolicitudObj = Fecha.GetFechaDto(dtos.FECHA_SOLICITUD);
                itemResult.FechaSolicitudObj = (FechaDto)fechaSolicitudObj;
                itemResult.CodigoSolicitante = dtos.CODIGO_SOLICITANTE;      
                itemResult.CodigoProveedor = dtos.CODIGO_PROVEEDOR;
                itemResult.Motivo = dtos.MOTIVO ;
                itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
                itemResult.Ano = dtos.ANO;
     


            return itemResult;
        }

        public async Task<List<AdmSolCompromisoResponseDto>> MapListSolCompromisoDto(List<ADM_SOL_COMPROMISO> dtos)
        {
            List<AdmSolCompromisoResponseDto> result = new List<AdmSolCompromisoResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapSolCompromisoDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public async Task<ResultDto<List<AdmSolCompromisoResponseDto>>> GetAll()
        {

            ResultDto<List<AdmSolCompromisoResponseDto>> result = new ResultDto<List<AdmSolCompromisoResponseDto>>(null);
            try
            {
                var solCompromiso = await _repository.GetAll();
                var cant = solCompromiso.Count();
                if (solCompromiso != null && solCompromiso.Count() > 0)
                {
                    var listDto = await MapListSolCompromisoDto(solCompromiso);

                    result.Data = listDto;
                    result.IsValid = true;
                    result.Message = "";


                    return result;
                }
                else
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No data";

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

        public async Task<ResultDto<AdmSolCompromisoResponseDto>> Create(AdmSolCompromisoUpdateDto dto)
        {
            ResultDto<AdmSolCompromisoResponseDto> result = new ResultDto<AdmSolCompromisoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if(dto.TipoSolCompromisoId <= 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo sol compromiso Id no existe";
                    return result;


                }

                var tipoSolCompromisoId = await _admDescriptivaRepository.GetByIdAndTitulo(22,dto.TipoSolCompromisoId);
                if (tipoSolCompromisoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo sol compromiso Id no existe";
                    return result;
                }

                if (dto.FechaSolicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Solicitud invalida";
                    return result;
                }

                if (dto.CodigoProveedor <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo proveedor invalido";
                    return result;
                }


                var codigoProveedor = await _admProveedoresRepository.GetByCodigo(dto.CodigoProveedor);

                if (codigoProveedor ==  null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo proveedor invalido";
                    return result;
                }

               
                if (dto.Motivo.Length > 2000)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "motivo invalido";
                    return result;
                }

                if (dto.Status.Length > 4)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status invalido";
                    return result;
                }


                if (dto.CodigoPresupuesto <= 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto invalido";
                    return result;


                }

                var codigoPresupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);

                if (codigoPresupuesto == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto invalido";
                    return result;
                }

                if(dto.Ano <= 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ano Invalido";
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

                


                ADM_SOL_COMPROMISO entity = new ADM_SOL_COMPROMISO();
                entity.CODIGO_SOL_COMPROMISO = await _repository.GetNextKey();
                entity.TIPO_SOL_COMPROMISO_ID = dto.TipoSolCompromisoId;
                entity.FECHA_SOLICITUD = dto.FechaSolicitud;
                entity.NUMERO_SOLICITUD = dto.NumeroSolicitud;
                entity.CODIGO_SOLICITANTE = dto.CodigoSolicitante;
                entity.CODIGO_PROVEEDOR = dto.CodigoProveedor;
                entity.MOTIVO = dto.Motivo;
                entity.STATUS = dto.Status;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                entity.ANO = dto.Ano;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;




                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapSolCompromisoDto(created.Data);
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

        public async Task<ResultDto<AdmSolCompromisoResponseDto>> Update(AdmSolCompromisoUpdateDto dto)
        {
            ResultDto<AdmSolCompromisoResponseDto> result = new ResultDto<AdmSolCompromisoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoSolCompromiso = await _repository.GetByCodigo(dto.CodigoSolCompromiso);
                if (codigoSolCompromiso == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitud Compromiso no existe";
                    return result;
                }

                var tipoSolCompromisoId = await _admDescriptivaRepository.GetByIdAndTitulo(22, dto.TipoSolCompromisoId);
                if (tipoSolCompromisoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo sol compromiso Id no existe";
                    return result;
                }

                if (dto.FechaSolicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Solicitud invalida";
                    return result;
                }

                if (dto.CodigoProveedor <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo proveedor invalido";
                    return result;
                }


                var codigoProveedor = await _admProveedoresRepository.GetByCodigo(dto.CodigoProveedor);

                if (codigoProveedor == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo proveedor invalido";
                    return result;
                }


                if (dto.Motivo.Length > 2000)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "motivo invalido";
                    return result;
                }

                if (dto.Status.Length > 4)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status invalido";
                    return result;
                }


                if (dto.CodigoPresupuesto <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto invalido";
                    return result;


                }

                var codigoPresupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);

                if (codigoPresupuesto == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto invalido";
                    return result;
                }

                if (dto.Ano <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ano Invalido";
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




                codigoSolCompromiso.CODIGO_SOL_COMPROMISO = dto.CodigoSolCompromiso;
                codigoSolCompromiso.TIPO_SOL_COMPROMISO_ID = dto.TipoSolCompromisoId;
                codigoSolCompromiso.FECHA_SOLICITUD = dto.FechaSolicitud;
                codigoSolCompromiso.NUMERO_SOLICITUD = dto.NumeroSolicitud;
                codigoSolCompromiso.CODIGO_SOLICITANTE = dto.CodigoSolicitante;
                codigoSolCompromiso.CODIGO_PROVEEDOR = dto.CodigoProveedor;
                codigoSolCompromiso.MOTIVO = dto.Motivo;
                codigoSolCompromiso.STATUS = dto.Status;
                codigoSolCompromiso.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                codigoSolCompromiso.ANO = dto.Ano;
                codigoSolCompromiso.EXTRA1 = dto.Extra1;
                codigoSolCompromiso.EXTRA2 = dto.Extra2;
                codigoSolCompromiso.EXTRA3 = dto.Extra3;

                codigoSolCompromiso.CODIGO_EMPRESA = conectado.Empresa;
                codigoSolCompromiso.USUARIO_UPD = conectado.Usuario;
                codigoSolCompromiso.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoSolCompromiso);

                var resultDto = await MapSolCompromisoDto(codigoSolCompromiso);
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
