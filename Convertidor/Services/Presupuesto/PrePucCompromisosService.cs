using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public class PrePucCompromisosService: IPrePucCompromisosService
    {

      
        private readonly IPrePucCompromisosRepository _repository;
        private readonly IPreDetalleCompromisosRepository _preDetalleCompromisosRepository;
        private readonly IPRE_SALDOSRepository _pRE_SALDOSRepository;
        private readonly IPrePlanUnicoCuentasService _prePlanUnicoCuentasService;
        private readonly IIndiceCategoriaProgramaService _indiceCategoriaProgramaService;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;
        private readonly IPreDescriptivaRepository _repositoryPreDescriptiva;
        private readonly IAdmPucSolicitudRepository _admPucSolicitudRepository;
        private readonly IPRE_INDICE_CAT_PRGRepository _pRE_INDICE_CAT_PRGRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IPreDescriptivasService _preDescriptivasService;
        private readonly IPRE_V_SALDOSRepository _preVSaldosRepository;

        public PrePucCompromisosService(IPrePucCompromisosRepository repository,
                                      IPreDetalleCompromisosRepository preDetalleCompromisosRepository,
                                      IPRE_SALDOSRepository pRE_SALDOSRepository,
                                      IPrePlanUnicoCuentasService prePlanUnicoCuentasService,
                                      IIndiceCategoriaProgramaService indiceCategoriaProgramaService,
                                      IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository,
                                      IPreDescriptivaRepository repositoryPreDescriptiva,
                                      IAdmPucSolicitudRepository admPucSolicitudRepository,
                                      IPRE_INDICE_CAT_PRGRepository pRE_INDICE_CAT_PRGRepository,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      IPreDescriptivasService preDescriptivasService,
                                      IPRE_V_SALDOSRepository preVSaldosRepository
        )
		{
            _repository = repository;
            _preDetalleCompromisosRepository = preDetalleCompromisosRepository;
            _pRE_SALDOSRepository = pRE_SALDOSRepository;
            _prePlanUnicoCuentasService = prePlanUnicoCuentasService;
            _indiceCategoriaProgramaService = indiceCategoriaProgramaService;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
            _repositoryPreDescriptiva = repositoryPreDescriptiva;
            _admPucSolicitudRepository = admPucSolicitudRepository;
           _pRE_INDICE_CAT_PRGRepository = pRE_INDICE_CAT_PRGRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _preDescriptivasService = preDescriptivasService;
            _preVSaldosRepository = preVSaldosRepository;
        }


        public async Task<ResultDto<List<PrePucCompromisosResponseDto>>> GetAll()
        {

            ResultDto<List<PrePucCompromisosResponseDto>> result = new ResultDto<List<PrePucCompromisosResponseDto>>(null);
            try
            {

                var pucCompromisos = await _repository.GetAll();

               

                if (pucCompromisos.Count() > 0)
                {
                    List<PrePucCompromisosResponseDto> listDto = new List<PrePucCompromisosResponseDto>();

                    foreach (var item in pucCompromisos)
                    {
                        var dto = await MapPrePucCompromisos(item);
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


        public async Task<ResultDto<List<PrePucCompromisosResponseDto>>> GetByDetalleCompromiso(int codigoDetalleCompromiso)
        {

            ResultDto<List<PrePucCompromisosResponseDto>> result = new ResultDto<List<PrePucCompromisosResponseDto>>(null);
            try
            {

                var pucCompromisos = await _repository.GetListByCodigoDetalleCompromiso(codigoDetalleCompromiso);

               

                if (pucCompromisos.Count() > 0)
                {
                    List<PrePucCompromisosResponseDto> listDto = new List<PrePucCompromisosResponseDto>();

                    foreach (var item in pucCompromisos)
                    {
                        var dto = await MapPrePucCompromisos(item);
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

       

        public async Task<PrePucCompromisosResponseDto> MapPrePucCompromisos(PRE_PUC_COMPROMISOS dto)
        {
            PrePucCompromisosResponseDto itemResult = new PrePucCompromisosResponseDto();
            itemResult.CodigoPucCompromiso = dto.CODIGO_PUC_COMPROMISO;
            itemResult.CodigoDetalleCompromiso = dto.CODIGO_DETALLE_COMPROMISO;
            itemResult.CodigoPucSolicitud = dto.CODIGO_PUC_SOLICITUD;
            itemResult.CodigoSaldo = dto.CODIGO_SALDO;
            
            itemResult.CodigoIcp = dto.CODIGO_ICP;
            itemResult.DenominacionIcp = "";
            itemResult.CodigoIcpConcat = "";
            var icp = await _indiceCategoriaProgramaService.GetByCodigo(dto.CODIGO_ICP);
            if (icp != null)
            {
                itemResult.DenominacionIcp = icp.Denominacion;
                itemResult.CodigoIcpConcat = icp.CodigoIcpConcat;
            }
            itemResult.CodigoPuc = dto.CODIGO_PUC;
            itemResult.DenominacionPuc = "";
            itemResult.CodigoPucConcat = "";
            var puc = await _prePlanUnicoCuentasService.GetById(dto.CODIGO_PUC);
            if (puc.Data != null)
            {
                itemResult.DenominacionPuc = puc.Data.Denominacion;
                itemResult.CodigoPucConcat = puc.Data.CodigoPucConcat;
            }
            itemResult.FinanciadoId = dto.FINANCIADO_ID;
            itemResult.DescripcionFinanciado = "";
            var descriptiva = await _preDescriptivasService.GetByCodigo(dto.FINANCIADO_ID);
            if (descriptiva.Data != null)
            {
                itemResult.DescripcionFinanciado = descriptiva.Data.Descripcion;
            }

            var preSaldo= await _preVSaldosRepository.GetByCodigo(dto.CODIGO_SALDO);
            if (preSaldo != null)
            {
                itemResult.DescripcionFinanciado = preSaldo.DESCRIPCION_FINANCIADO;
            }
            itemResult.CodigoFinanciado = dto.CODIGO_FINANCIADO;
            itemResult.Monto = dto.MONTO;
            itemResult.MontoCausado = dto.MONTO_CAUSADO;
            itemResult.MontoAnulado = dto.MONTO_ANULADO;
            itemResult.Extra1 = dto.EXTRA1;
            itemResult.Extra2 = dto.EXTRA2;
            itemResult.Extra3 = dto.EXTRA3;
            itemResult.CodigoPresupuesto = dto.CODIGO_PRESUPUESTO;
           

            return itemResult;

        }
        

        
        
        public async Task<List<PrePucCompromisosResponseDto>> MapListPrePucCompromisosDto(List<PRE_PUC_COMPROMISOS> dtos)
        {
            List<PrePucCompromisosResponseDto> result = new List<PrePucCompromisosResponseDto>();


            foreach (var item in dtos)
            {

                PrePucCompromisosResponseDto itemResult = new PrePucCompromisosResponseDto();

                itemResult = await MapPrePucCompromisos(item);

                result.Add(itemResult);
            }
            return result;



        }

        public async Task<ResultDto<PrePucCompromisosResponseDto>> Update(PrePucCompromisosUpdateDto dto)
        {

            ResultDto<PrePucCompromisosResponseDto> result = new ResultDto<PrePucCompromisosResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.CodigoPucCompromiso < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Compromiso no existe";
                    return result;
                }

                var codigoPucCompromiso = await _repository.GetByCodigo(dto.CodigoPucCompromiso);
                if (codigoPucCompromiso == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Compromiso no existe";
                    return result;
                }

                if (dto.CodigoDetalleCompromiso < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Compromiso Invalido";
                    return result;

                }
                var codigoDetalleCompromiso = await _preDetalleCompromisosRepository.GetByCodigo(dto.CodigoDetalleCompromiso);
                if (codigoDetalleCompromiso == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Compromiso Invalido";
                    return result;
                }

                if (dto.CodigoPucSolicitud < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Solicitud Invalido";
                    return result;
                }
                var codigoPucSolicitud = await _admPucSolicitudRepository.GetCodigoPucSolicitud(dto.CodigoPucSolicitud);
                if (codigoPucSolicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Solicitud Invalido";
                    return result;
                }

                if (dto.CodigoSaldo < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Saldo Invalido";
                    return result;

                }
                var codigoSaldo = await _pRE_SALDOSRepository.GetByCodigo(dto.CodigoSaldo);
                if (codigoSaldo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Saldo Invalido";
                    return result;
                }

                if (dto.CodigoIcp < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Icp  Invalido";
                    return result;

                }

                var codigoIcp = await _pRE_INDICE_CAT_PRGRepository.GetByCodigo(dto.CodigoIcp);
                if (codigoIcp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Icp  Invalido";
                    return result;
                }



                if (dto.CodigoFinanciado < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Invalido";
                    return result;

                }

                if (dto.CodigoPuc < 0)
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

                if (dto.FinanciadoId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Financiado Id Invalido";
                    return result;

                }

                var financiadoId = await _repositoryPreDescriptiva.GetByIdAndTitulo(3, dto.FinanciadoId);
                if (financiadoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Financiado Id Invalido";
                    return result;
                }

                if (dto.CodigoFinanciado < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Financiado Invalido";
                    return result;
                }

                if (dto.Monto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Invalido";
                    return result;
                }

                if (dto.MontoCausado < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Causado Invalido";
                    return result;
                }

                if (dto.MontoAnulado < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Anulado Invalido";
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



                codigoPucCompromiso.CODIGO_PUC_COMPROMISO = dto.CodigoPucCompromiso;
                codigoPucCompromiso.CODIGO_DETALLE_COMPROMISO = dto.CodigoDetalleCompromiso;
                codigoPucCompromiso.CODIGO_PUC_SOLICITUD = dto.CodigoPucSolicitud;
                codigoPucCompromiso.CODIGO_SALDO = dto.CodigoSaldo;
                codigoPucCompromiso.CODIGO_ICP = dto.CodigoIcp;
                codigoPucCompromiso.CODIGO_PUC = dto.CodigoPuc;
                codigoPucCompromiso.FINANCIADO_ID = dto.FinanciadoId;
                codigoPucCompromiso.CODIGO_FINANCIADO = dto.CodigoFinanciado;
                codigoPucCompromiso.MONTO = dto.Monto;
                codigoPucCompromiso.MONTO_CAUSADO = dto.MontoCausado;
                codigoPucCompromiso.MONTO_ANULADO = dto.MontoAnulado;
                codigoPucCompromiso.EXTRA1 = dto.Extra1;
                codigoPucCompromiso.EXTRA2 = dto.Extra2;
                codigoPucCompromiso.EXTRA3 = dto.Extra3;
                codigoPucCompromiso.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
            


                codigoPucCompromiso.CODIGO_EMPRESA = conectado.Empresa;
                codigoPucCompromiso.USUARIO_UPD = conectado.Usuario;
                codigoPucCompromiso.FECHA_UPD = DateTime.Now;
                await _repository.Update(codigoPucCompromiso);
                await _preVSaldosRepository.RecalcularSaldo(codigoPucCompromiso.CODIGO_PRESUPUESTO);
                var resultDto = await MapPrePucCompromisos(codigoPucCompromiso);
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

        public async Task<ResultDto<PrePucCompromisosResponseDto>> Create(PrePucCompromisosUpdateDto dto)
        {

            ResultDto<PrePucCompromisosResponseDto> result = new ResultDto<PrePucCompromisosResponseDto>(null);
            try
            {

                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.CodigoDetalleCompromiso < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Compromiso Invalido";
                    return result;

                }
                var codigoDetalleCompromiso = await _preDetalleCompromisosRepository.GetByCodigo(dto.CodigoDetalleCompromiso);
                if (codigoDetalleCompromiso == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Compromiso Invalido";
                    return result;
                }

                if (dto.CodigoPucSolicitud < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Solicitud Invalido";
                    return result;
                }
                var codigoPucSolicitud = await _admPucSolicitudRepository.GetCodigoPucSolicitud(dto.CodigoPucSolicitud);
                if (codigoPucSolicitud == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Solicitud Invalido";
                    return result;
                }

                if (dto.CodigoSaldo < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Saldo Invalido";
                    return result;

                }
                var codigoSaldo = await _pRE_SALDOSRepository.GetByCodigo(dto.CodigoSaldo);
                if (codigoSaldo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Saldo Invalido";
                    return result;
                }

                if (dto.CodigoIcp < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Icp  Invalido";
                    return result;

                }

                var codigoIcp = await _pRE_INDICE_CAT_PRGRepository.GetByCodigo(dto.CodigoIcp);
                if (codigoIcp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Icp  Invalido";
                    return result;
                }



                if (dto.CodigoFinanciado < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Invalido";
                    return result;

                }

                if (dto.CodigoPuc < 0)
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

                if (dto.FinanciadoId < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Financiado Id Invalido";
                    return result;

                }

                var financiadoId = await _repositoryPreDescriptiva.GetByIdAndTitulo(3, dto.FinanciadoId);
                if (financiadoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Financiado Id Invalido";
                    return result;
                }

                if (dto.CodigoFinanciado < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Financiado Invalido";
                    return result;
                }

                if (dto.Monto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Invalido";
                    return result;
                }

                if (dto.MontoCausado < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Causado Invalido";
                    return result;
                }

                if (dto.MontoAnulado < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Anulado Invalido";
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




                PRE_PUC_COMPROMISOS entity = new PRE_PUC_COMPROMISOS();
                entity.CODIGO_PUC_COMPROMISO = await _repository.GetNextKey();
                entity.CODIGO_DETALLE_COMPROMISO = dto.CodigoDetalleCompromiso;
                entity.CODIGO_PUC_SOLICITUD = dto.CodigoPucSolicitud;
                entity.CODIGO_SALDO = dto.CodigoSaldo;
                entity.CODIGO_ICP = dto.CodigoIcp;
                entity.CODIGO_PUC = dto.CodigoPuc;
                entity.FINANCIADO_ID = dto.FinanciadoId;
                entity.CODIGO_FINANCIADO = dto.CodigoFinanciado;
                entity.MONTO = dto.Monto;
                entity.MONTO_CAUSADO = dto.MontoCausado;
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
                    
                    await _preVSaldosRepository.RecalcularSaldo(dto.CodigoPresupuesto);
                    var resultDto = await MapPrePucCompromisos(created.Data);
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



        public async Task<ResultDto<PrePucCompromisosDeleteDto>> Delete(PrePucCompromisosDeleteDto dto)
        {

            ResultDto<PrePucCompromisosDeleteDto> result = new ResultDto<PrePucCompromisosDeleteDto>(null);
            try
            {

                var codigoPucCompromiso = await _repository.GetByCodigo(dto.CodigoPucCompromiso);
                if (codigoPucCompromiso == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Compromiso no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoPucCompromiso);
                await _preVSaldosRepository.RecalcularSaldo(codigoPucCompromiso.CODIGO_PRESUPUESTO);
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

