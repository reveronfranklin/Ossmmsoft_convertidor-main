using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Presupuesto;
using static SkiaSharp.HarfBuzz.SKShaper;

namespace Convertidor.Services.Adm
{
    public class AdmPucSolCompromisoService : IAdmPucSolCompromisoService

    {
        private readonly IAdmPucSolCompromisoRepository _repository;
        private readonly IAdmSolicitudesService _admSolicitudesService;
        private readonly IPRE_V_SALDOSRepository _pRE_V_SALDOSRepository;
        private readonly IIndiceCategoriaProgramaService _indiceCategoriaProgramaService;
        private readonly IPrePlanUnicoCuentasService _prePlanUnicoCuentasService;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;

        public AdmPucSolCompromisoService(IAdmPucSolCompromisoRepository repository,
                                     IAdmSolicitudesService admSolicitudesService,
                                     IPRE_V_SALDOSRepository pRE_V_SALDOSRepository,
                                     IIndiceCategoriaProgramaService indiceCategoriaProgramaService,
                                     IPrePlanUnicoCuentasService prePlanUnicoCuentasService,
                                     IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository)
        {
            _repository = repository;
            _admSolicitudesService = admSolicitudesService;
            _pRE_V_SALDOSRepository = pRE_V_SALDOSRepository;
            _indiceCategoriaProgramaService = indiceCategoriaProgramaService;
            _prePlanUnicoCuentasService = prePlanUnicoCuentasService;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
        }

        public async Task<AdmPucSolCompromisoResponseDto> MapPucSolCompromisoDto(ADM_PUC_SOL_COMPROMISO dtos)
        {
            AdmPucSolCompromisoResponseDto itemResult = new AdmPucSolCompromisoResponseDto();
            itemResult.CodigoPucSolicitud = dtos.CODIGO_PUC_SOLICITUD;
            itemResult.CodigoSolicitud = dtos.CODIGO_SOLICITUD;
            itemResult.CodigoSaldo = dtos.CODIGO_SALDO;
            itemResult.CodigoIcp = dtos.CODIGO_ICP;
            itemResult.CodigoPuc = dtos.CODIGO_PUC;
            itemResult.FinanciadoId = dtos.FINANCIADO_ID;
            itemResult.CodigoFinanciado = dtos.CODIGO_FINANCIADO;
            itemResult.Monto = dtos.MONTO;
            itemResult.MontoComprometido = dtos.MONTO_COMPROMETIDO;
            itemResult.MontoAnulado = dtos.MONTO_ANULADO;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            return itemResult;
        }

        public async Task<List<AdmPucSolCompromisoResponseDto>> MapListPucSolCompromisoDto(List<ADM_PUC_SOL_COMPROMISO> dtos)
        {
            List<AdmPucSolCompromisoResponseDto> result = new List<AdmPucSolCompromisoResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapPucSolCompromisoDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public async Task<AdmPucSolCompromisoResponseDto> GetbyCodigoPucSolicitud(int codigoPucSolicitud)
        {
            AdmPucSolCompromisoResponseDto result = new AdmPucSolCompromisoResponseDto();
            try
            {
                var pucSolicitud = await _repository.GetbyCodigoPucSolicitud(codigoPucSolicitud);
                if (pucSolicitud != null)
                {
                    var dto = await MapPucSolCompromisoDto(pucSolicitud);
                    result = dto;
                }
                else
                {
                    result = null;
                }

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                return null;
            }

        }

        public async Task<ResultDto<List<AdmPucSolCompromisoResponseDto>>> GetAll()
        {

            ResultDto<List<AdmPucSolCompromisoResponseDto>> result = new ResultDto<List<AdmPucSolCompromisoResponseDto>>(null);
            try
            {
                var pucSolicitud = await _repository.GetAll();
                var cant = pucSolicitud.Count();
                if (pucSolicitud != null && pucSolicitud.Count() > 0)
                {
                    var listDto = await MapListPucSolCompromisoDto(pucSolicitud);

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

        public async Task<List<AdmPucSolCompromisoResponseDto>> GetAllbyCodigoPucSolcicitud(int codigoPucSolicitud)
        {
            List<AdmPucSolCompromisoResponseDto> result = new List<AdmPucSolCompromisoResponseDto>();
            try
            {

                var pucSolicitud = await _repository.GetAllbyCodigoPucSolicitud(codigoPucSolicitud);
                pucSolicitud = pucSolicitud.OrderBy(x => x.CODIGO_PUC_SOLICITUD).ToList();


                if (pucSolicitud.Count() > 0)
                {
                    List<AdmPucSolCompromisoResponseDto> listDto = new List<AdmPucSolCompromisoResponseDto>();

                    foreach (var item in pucSolicitud)
                    {
                        var dto = await MapPucSolCompromisoDto(item);
                        listDto.Add(dto);
                    }


                    result = listDto;

                   
                }
                else
                {
                    result = null;
                   
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                result = null;
                
            }



            return result;
        }


        public async Task<ResultDto<AdmPucSolCompromisoResponseDto>> Update(AdmPucSolCompromisoUpdateDto dto)
        {
            ResultDto<AdmPucSolCompromisoResponseDto> result = new ResultDto<AdmPucSolCompromisoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoPucsolicitud = await _repository.GetbyCodigoPucSolicitud(dto.CodigoPucSolicitud);
                if (codigoPucsolicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Solicitud no existe";
                    return result;
                }
   

                if (dto.CodigoSolicitud <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitud invalido";
                    return result;

                }

                var codigoSolicitud = await _admSolicitudesService.GetByCodigoSolicitud(dto.CodigoSolicitud);
                if(codigoSolicitud == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitud invalido";
                    return result;

                }

                if (dto.CodigoSaldo <=0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Saldo Invalido";
                    return result;
                }

                var codigoSaldo = await _pRE_V_SALDOSRepository.GetByCodigo(dto.CodigoSaldo);
                if (codigoSaldo == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Saldo Invalido";
                    return result;
                }
                if (dto.CodigoIcp <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;
                }
                var codigoIcp = await _indiceCategoriaProgramaService.GetByCodigo(dto.CodigoIcp );
                if(codigoIcp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;
                }

                if (dto.CodigoPuc <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Invalido";
                    return result;
                }
                var codigoPuc = await _prePlanUnicoCuentasService.GetById(dto.CodigoPuc);
                if(codigoPuc == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Invalido";
                    return result;

                }
                
                if(dto.FinanciadoId <= 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "financiado Id no existe";
                    return result;

                }

                var financiadoId = await _admDescriptivaRepository.GetByIdAndTitulo(13,dto.FinanciadoId);
                if (financiadoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "financiado Id no existe";
                    return result;

                }

                if (dto.CodigoFinanciado < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Financiado Invalido";
                    return result;
                }

                if (dto.Monto <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Invalido";
                    return result;
                }

                if (dto.MontoComprometido <0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto comprometido Invalido";
                    return result;
                }
                if (dto.MontoAnulado<0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Anulado invalido";
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
                if (dto.CodigoPresupuesto <= 0)
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

                codigoPucsolicitud.CODIGO_PUC_SOLICITUD = dto.CodigoPucSolicitud;
                codigoPucsolicitud.CODIGO_SOLICITUD = dto.CodigoSolicitud;
                codigoPucsolicitud.CODIGO_SALDO = dto.CodigoSaldo;
                codigoPucsolicitud.CODIGO_ICP = dto.CodigoIcp;
                codigoPucsolicitud.CODIGO_PUC = dto.CodigoPuc;
                codigoPucsolicitud.FINANCIADO_ID = dto.FinanciadoId;
                codigoPucsolicitud.CODIGO_FINANCIADO = dto.CodigoFinanciado;
                codigoPucsolicitud.MONTO = dto.Monto;
                codigoPucsolicitud.MONTO_COMPROMETIDO = dto.MontoComprometido;
                codigoPucsolicitud.MONTO_ANULADO = dto.MontoAnulado;
                codigoPucsolicitud.EXTRA1 = dto.Extra1;
                codigoPucsolicitud.EXTRA2 = dto.Extra2;
                codigoPucsolicitud.EXTRA3 = dto.Extra3;
                codigoPucsolicitud.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;

                
                codigoPucsolicitud.CODIGO_EMPRESA = conectado.Empresa;
                codigoPucsolicitud.USUARIO_UPD = conectado.Usuario;
                codigoPucsolicitud.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoPucsolicitud);

                var resultDto = await MapPucSolCompromisoDto(codigoPucsolicitud);
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

        public async Task<ResultDto<AdmPucSolCompromisoResponseDto>> Create(AdmPucSolCompromisoUpdateDto dto)
        {
            ResultDto<AdmPucSolCompromisoResponseDto> result = new ResultDto<AdmPucSolCompromisoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.CodigoSolicitud <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitud invalido";
                    return result;

                }

                var codigoSolicitud = await _admSolicitudesService.GetByCodigoSolicitud(dto.CodigoSolicitud);
                if (codigoSolicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Solicitud invalido";
                    return result;

                }

                if (dto.CodigoSaldo <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Saldo Invalido";
                    return result;
                }

                var codigoSaldo = await _pRE_V_SALDOSRepository.GetByCodigo(dto.CodigoSaldo);
                if (codigoSaldo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Saldo Invalido";
                    return result;
                }
                if (dto.CodigoIcp <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;
                }
                var codigoIcp = await _indiceCategoriaProgramaService.GetByCodigo(dto.CodigoIcp);
                if (codigoIcp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;
                }

                if (dto.CodigoPuc <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Invalido";
                    return result;
                }
                var codigoPuc = await _prePlanUnicoCuentasService.GetById(dto.CodigoPuc);
                if (codigoPuc == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Invalido";
                    return result;

                }

                if (dto.FinanciadoId <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "financiado Id no existe";
                    return result;

                }

                var financiadoId = await _admDescriptivaRepository.GetByIdAndTitulo(13, dto.FinanciadoId);
                if (financiadoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "financiado Id no existe";
                    return result;

                }

                if (dto.CodigoFinanciado < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Financiado Invalido";
                    return result;
                }

                if(dto.Monto <= 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Invalido";
                    return result;
                }

                if (dto.MontoComprometido < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto comprometido Invalido";
                    return result;
                }
                if (dto.MontoAnulado < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Anulado invalido";
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
                if (dto.CodigoPresupuesto <= 0)
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


            ADM_PUC_SOL_COMPROMISO entity = new ADM_PUC_SOL_COMPROMISO();
            entity.CODIGO_PUC_SOLICITUD = await _repository.GetNextKey();
            entity.CODIGO_SOLICITUD = dto.CodigoSolicitud;
            entity.CODIGO_SALDO = dto.CodigoSaldo;
            entity.CODIGO_ICP = dto.CodigoIcp;
            entity.CODIGO_PUC = dto.CodigoPuc;
            entity.FINANCIADO_ID = dto.FinanciadoId;
            entity.CODIGO_FINANCIADO = dto.CodigoFinanciado;
            entity.MONTO=dto.Monto;
            entity.MONTO_COMPROMETIDO = dto.MontoComprometido;
            entity.MONTO_ANULADO = dto.MontoAnulado;
            entity.EXTRA1 = dto.Extra1;
            entity.EXTRA2 = dto.Extra2;
            entity.EXTRA3 = dto.Extra3;
            entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;


            
            entity.CODIGO_EMPRESA = conectado.Empresa;
            entity.USUARIO_INS = conectado.Usuario;
            entity.FECHA_INS = DateTime.Now;

            var created = await _repository.Add(entity);
            if (created.IsValid && created.Data != null)
            {
                var resultDto = await MapPucSolCompromisoDto(created.Data);
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

        public async Task<ResultDto<AdmPucSolCompromisoDeleteDto>> Delete(AdmPucSolCompromisoDeleteDto dto) 
        {
            ResultDto<AdmPucSolCompromisoDeleteDto> result = new ResultDto<AdmPucSolCompromisoDeleteDto>(null);
            try
            {

                var codigoPucSolicitud = await _repository.GetbyCodigoPucSolicitud(dto.CodigoPucSolicitud);
                if (codigoPucSolicitud == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Solicitud no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoPucSolicitud);

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
    }
 }

