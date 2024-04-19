using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository.Presupuesto;
using Convertidor.Data.Repository.Rh;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
    public class PrePucModificacionService : IPrePucModificacionService
    {
        private readonly IPrePucModificacionRepository _repository;
        private readonly IPreModificacionRepository _preModificacionRepository;
        private readonly IPRE_SALDOSRepository _pRE_SALDOSRepository;
        private readonly IPRE_INDICE_CAT_PRGRepository _pRE_INDICE_CAT_PRGRepository;
        private readonly IPRE_PLAN_UNICO_CUENTASRepository _pRE_PLAN_UNICO_CUENTASRepository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public PrePucModificacionService(IPrePucModificacionRepository repository,
                                         IPreModificacionRepository preModificacionRepository,
                                         IPRE_SALDOSRepository pRE_SALDOSRepository,
                                         IPRE_INDICE_CAT_PRGRepository pRE_INDICE_CAT_PRGRepository,
                                         IPRE_PLAN_UNICO_CUENTASRepository pRE_PLAN_UNICO_CUENTASRepository,
                                         IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository,
                                         ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _preModificacionRepository = preModificacionRepository;
            _pRE_SALDOSRepository = pRE_SALDOSRepository;
            _pRE_INDICE_CAT_PRGRepository = pRE_INDICE_CAT_PRGRepository;
            _pRE_PLAN_UNICO_CUENTASRepository = pRE_PLAN_UNICO_CUENTASRepository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<ResultDto<List<PrePucModificacionResponseDto>>> GetAll()
        {

            ResultDto<List<PrePucModificacionResponseDto>> result = new ResultDto<List<PrePucModificacionResponseDto>>(null);
            try
            {

                var pucModificacion = await _repository.GetAll();



                if (pucModificacion.Count() > 0)
                {
                    List<PrePucModificacionResponseDto> listDto = new List<PrePucModificacionResponseDto>();

                    foreach (var item in pucModificacion)
                    {
                        var dto = await MapPrePucModificacion(item);
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

       
        public async Task<PrePucModificacionResponseDto> MapPrePucModificacion(PRE_PUC_MODIFICACION dto)
        {
            PrePucModificacionResponseDto itemResult = new PrePucModificacionResponseDto();
            itemResult.CodigoPucModificacion = dto.CODIGO_PUC_MODIFICACION;
            itemResult.CodigoModificacion = dto.CODIGO_MODIFICACION;
            itemResult.CodigoSaldo = dto.CODIGO_SALDO;
            itemResult.FinanciadoId = dto.FINANCIADO_ID;
            itemResult.CodigoFinanciado = dto.CODIGO_FINANCIADO;
            itemResult.CodigoIcp = dto.CODIGO_ICP;
            itemResult.CodigoPuc = dto.CODIGO_PUC;
            itemResult.Monto = dto.MONTO;
            itemResult.DePara = dto.DE_PARA;
            itemResult.Extra1 = dto.EXTRA1;
            itemResult.Extra2 = dto.EXTRA2;
            itemResult.Extra3 = dto.EXTRA3;
            itemResult.CodigoPucSolModificacion = dto.CODIGO_PUC_SOL_MODIFICACION;
            itemResult.MontoAnulado = dto.MONTO_ANULADO;
            itemResult.CodigoPresupuesto = dto.CODIGO_PRESUPUESTO;

            return itemResult;

        }

        public async Task<List<PrePucModificacionResponseDto>> MapListPrePucModificacionDto(List<PRE_PUC_MODIFICACION> dtos)
        {
            List<PrePucModificacionResponseDto> result = new List<PrePucModificacionResponseDto>();


            foreach (var item in dtos)
            {

                PrePucModificacionResponseDto itemResult = new PrePucModificacionResponseDto();

                itemResult = await MapPrePucModificacion(item);

                result.Add(itemResult);
            }
            return result;



        }

        public async Task<ResultDto<PrePucModificacionResponseDto>> Update(PrePucModificacionUpdateDto dto)
        {

            ResultDto<PrePucModificacionResponseDto> result = new ResultDto<PrePucModificacionResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoPucModificacion = await _repository.GetByCodigo(dto.CodigoPucModificacion);
                if (codigoPucModificacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Modificacion no existe";
                    return result;
                }
                var codigoModificacion = await _preModificacionRepository.GetByCodigo(dto.CodigoModificacion);
                if (dto.CodigoModificacion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Modificacion Invalido";
                    return result;
                }

                var CodigoSaldo = await _pRE_SALDOSRepository.GetByCodigo(dto.CodigoSaldo);
                if (dto.CodigoSaldo < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo saldo Invalido";
                    return result;
                }
                if (dto.FinanciadoId == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Financiado Id";
                    return result;
                }
                if (dto.CodigoFinanciado == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Financiado Invalido";
                    return result;
                }

                var codigoIcp = await _pRE_INDICE_CAT_PRGRepository.GetByCodigo(dto.CodigoIcp);
                if (dto.CodigoIcp < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;
                }

                var codigoPuc = await _pRE_PLAN_UNICO_CUENTASRepository.GetByCodigo(dto.CodigoPuc );
                if (dto.CodigoPuc < 0 )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Invalido";
                    return result;
                }
                if (dto.Monto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Invalido";
                    return result;

                }
                if (dto.DePara.Length  > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "De Para Invalido";
                    return result;
                }

                if (dto.Extra1 < 0)
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

                var codigoPresupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (dto.CodigoPresupuesto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }



                codigoPucModificacion.CODIGO_PUC_MODIFICACION = dto.CodigoPucModificacion;
                codigoPucModificacion.CODIGO_MODIFICACION = dto.CodigoModificacion;
                codigoPucModificacion.CODIGO_SALDO = dto.CodigoSaldo;
                codigoPucModificacion.FINANCIADO_ID = dto.FinanciadoId;
                codigoPucModificacion.CODIGO_FINANCIADO = dto.CodigoFinanciado;
                codigoPucModificacion.CODIGO_ICP = dto.CodigoIcp;
                codigoPucModificacion.CODIGO_PUC = dto.CodigoPuc;
                codigoPucModificacion.MONTO = dto.Monto;
                codigoPucModificacion.DE_PARA = dto.DePara;
                codigoPucModificacion.EXTRA1 = dto.Extra1;
                codigoPucModificacion.EXTRA2 = dto.Extra2;
                codigoPucModificacion.EXTRA3 = dto.Extra3;
                codigoPucModificacion.CODIGO_PUC_SOL_MODIFICACION = dto.CodigoPucSolModificacion;
                codigoPucModificacion.MONTO_ANULADO = dto.MontoAnulado;
                codigoPucModificacion.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;



                codigoPucModificacion.CODIGO_EMPRESA = conectado.Empresa;
                codigoPucModificacion.USUARIO_UPD = conectado.Usuario;
                codigoPucModificacion.FECHA_UPD = DateTime.Now;
                await _repository.Update(codigoPucModificacion);

                var resultDto = await MapPrePucModificacion(codigoPucModificacion);
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

        public async Task<ResultDto<PrePucModificacionResponseDto>> Create(PrePucModificacionUpdateDto dto)
        {

            ResultDto<PrePucModificacionResponseDto> result = new ResultDto<PrePucModificacionResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoPucModificacion = await _repository.GetByCodigo(dto.CodigoPucModificacion);
                if (codigoPucModificacion != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Modificacion no existe";
                    return result;
                }
                var codigoModificacion = await _preModificacionRepository.GetByCodigo(dto.CodigoModificacion);
                if (dto.CodigoModificacion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Modificacion Invalido";
                    return result;
                }

                var CodigoSaldo = await _pRE_SALDOSRepository.GetByCodigo(dto.CodigoSaldo);
                if (dto.CodigoSaldo < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo saldo Invalido";
                    return result;
                }
                if (dto.FinanciadoId == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Financiado Id";
                    return result;
                }
                if (dto.CodigoFinanciado == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Financiado Invalido";
                    return result;
                }

                var codigoIcp = await _pRE_INDICE_CAT_PRGRepository.GetByCodigo(dto.CodigoIcp);
                if (dto.CodigoIcp < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;
                }

                var codigoPuc = await _pRE_PLAN_UNICO_CUENTASRepository.GetByCodigo(dto.CodigoPuc);
                if (dto.CodigoPuc < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Invalido";
                    return result;
                }
                if (dto.Monto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Invalido";
                    return result;

                }
                if (dto.DePara.Length > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "De Para Invalido";
                    return result;
                }

                if (dto.Extra1 < 0)
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

                var codigoPresupuesto = await _pRE_PRESUPUESTOSRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (dto.CodigoPresupuesto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }



                PRE_PUC_MODIFICACION entity = new PRE_PUC_MODIFICACION();
                entity.CODIGO_PUC_MODIFICACION = await _repository.GetNextKey();
                entity.CODIGO_MODIFICACION = dto.CodigoModificacion;
                entity.CODIGO_SALDO = dto.CodigoSaldo;
                entity.FINANCIADO_ID = dto.FinanciadoId;
                entity.CODIGO_FINANCIADO = dto.CodigoFinanciado;
                entity.CODIGO_ICP = dto.CodigoIcp;
                entity.CODIGO_PUC = dto.CodigoPuc;
                entity.MONTO = dto.Monto;
                entity.DE_PARA = dto.DePara;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.CODIGO_PUC_SOL_MODIFICACION = dto.CodigoPucSolModificacion;
                entity.MONTO_ANULADO = dto.MontoAnulado;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;

                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapPrePucModificacion(created.Data);
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

        public async Task<ResultDto<PrePucModificacionDeleteDto>> Delete(PrePucModificacionDeleteDto dto)
        {

            ResultDto<PrePucModificacionDeleteDto> result = new ResultDto<PrePucModificacionDeleteDto>(null);
            try
            {

                var codigoPucModificacion = await _repository.GetByCodigo(dto.CodigoPucModificacion);
                if (codigoPucModificacion == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Modificacion no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoPucModificacion);

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
