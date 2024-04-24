using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;

namespace Convertidor.Services.Adm
{
    public class AdmPucContratoService : IAdmPucContratoService
    {
        private readonly IAdmPucContratoRepository _repository;
        private readonly IAdmContratosRepository _admContratosRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IPRE_PRESUPUESTOSRepository _prePresupuestosRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;

        public AdmPucContratoService( IAdmPucContratoRepository repository,
                                      IAdmContratosRepository admContratosRepository,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      IPRE_PRESUPUESTOSRepository prePresupuestosRepository,
                                      IAdmDescriptivaRepository admDescriptivaRepository)
        {
            _repository = repository;
            _admContratosRepository = admContratosRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _prePresupuestosRepository = prePresupuestosRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
        }

        public async Task<AdmPucContratoResponseDto> MapPucContratoDto(ADM_PUC_CONTRATO dtos)
        {
            AdmPucContratoResponseDto itemResult = new AdmPucContratoResponseDto();

            itemResult.CodigoPucContrato = dtos.CODIGO_PUC_CONTRATO;
            itemResult.CodigoContrato = dtos.CODIGO_CONTRATO;
            itemResult.CodigoIcp = dtos.CODIGO_ICP;
            itemResult.CodigoPuc = dtos.CODIGO_PUC;
            itemResult.FinanciadoID = dtos.FINANCIADO_ID;
            itemResult.CodigoFinanciado = dtos.CODIGO_FINANCIADO;
            itemResult.CodigoSaldo = dtos.CODIGO_SALDO;
            itemResult.Monto = dtos.MONTO;
            itemResult.MontoCausado = dtos.MONTO_CAUSADO;
            itemResult.MontoAnulado = dtos.MONTO_ANULADO;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
           


            return itemResult;
        }

        public async Task<List<AdmPucContratoResponseDto>> MapListPucContratoDto(List<ADM_PUC_CONTRATO> dtos)
        {
            List<AdmPucContratoResponseDto> result = new List<AdmPucContratoResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapPucContratoDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public async Task<ResultDto<List<AdmPucContratoResponseDto>>> GetAll()
        {

            ResultDto<List<AdmPucContratoResponseDto>> result = new ResultDto<List<AdmPucContratoResponseDto>>(null);
            try
            {
                var pucContrato = await _repository.GetAll();
                var cant = pucContrato.Count();
                if (pucContrato != null && pucContrato.Count() > 0)
                {
                    var listDto = await MapListPucContratoDto(pucContrato);

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
        public async Task<ResultDto<AdmPucContratoResponseDto>> Update(AdmPucContratoUpdateDto dto)
        {
            ResultDto<AdmPucContratoResponseDto> result = new ResultDto<AdmPucContratoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoPucContrato = await _repository.GetCodigoPucContrato(dto.CodigoPucContrato);
                if (codigoPucContrato == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc contrato no existe";
                    return result;
                }

                var codigoContrato = await _admContratosRepository.GetByCodigoContrato(dto.CodigoContrato);
                if (dto.CodigoContrato < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Contrato invalido";
                    return result;

                }

                if (dto.CodigoIcp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo icp invalido";
                    return result;
                }
                if (dto.CodigoPuc == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Invalido";
                    return result;
                }

                var financiadoID = await _admDescriptivaRepository.GetByIdAndTitulo(13, dto.FinanciadoID);

                if (financiadoID == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Financiado ID Invalido";
                    return result;
                }
                if (dto.CodigoFinanciado == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo financiado Invalido";
                    return result;

                }
                if (dto.CodigoSaldo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Saldo Invalido";
                    return result;

                }

                if (dto.MontoCausado + dto.Monto == dto.MontoAnulado)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "La suma del Monto Causado + Monto Anulado debe ser menor al Monto";
                    return result;
                }

                //(MONTO > 0 AND MONTO_ANULADO >= 0) OR (MONTO < 0 AND MONTO_ANULADO <= 0)
                //(MONTO > 0 AND MONTO_CAUSADO+MONTO_ANULADO <= MONTO) OR (MONTO < 0 AND MONTO_CAUSADO+MONTO_ANULADO >= MONTO)


                if (dto.Monto + dto.MontoAnulado == dto.MontoCausado)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Causado Invalido";
                    return result;

                }

                if (dto.MontoAnulado == dto.Monto)
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



                var codigopresupuesto = await _prePresupuestosRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (dto.CodigoPresupuesto < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }




                codigoPucContrato.CODIGO_PUC_CONTRATO = dto.CodigoPucContrato;
                codigoPucContrato.CODIGO_CONTRATO = dto.CodigoContrato;
                codigoPucContrato.CODIGO_ICP = dto.CodigoIcp;
                codigoPucContrato.CODIGO_PUC = dto.CodigoPuc;
                codigoPucContrato.FINANCIADO_ID = dto.FinanciadoID;
                codigoPucContrato.CODIGO_FINANCIADO = dto.CodigoFinanciado;
                codigoPucContrato.CODIGO_SALDO = dto.CodigoSaldo;
                codigoPucContrato.MONTO = dto.Monto;
                codigoPucContrato.MONTO_CAUSADO = dto.MontoCausado;
                codigoPucContrato.MONTO_ANULADO = dto.MontoAnulado;
                codigoPucContrato.EXTRA1 = dto.Extra1;
                codigoPucContrato.EXTRA2 = dto.Extra2;
                codigoPucContrato.EXTRA3 = dto.Extra3;
                codigoPucContrato.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;


                codigoPucContrato.CODIGO_EMPRESA = conectado.Empresa;
                codigoPucContrato.USUARIO_UPD = conectado.Usuario;
                codigoPucContrato.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoPucContrato);

                var resultDto = await MapPucContratoDto(codigoPucContrato);
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

        public async Task<ResultDto<AdmPucContratoResponseDto>> Create(AdmPucContratoUpdateDto dto)
        {
            ResultDto<AdmPucContratoResponseDto> result = new ResultDto<AdmPucContratoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoPucContrato = await _repository.GetCodigoPucContrato(dto.CodigoPucContrato);
                if (codigoPucContrato != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc contrato Ya existe";
                    return result;
                }

                var codigoContrato = await _admContratosRepository.GetByCodigoContrato(dto.CodigoContrato);
                if (dto.CodigoContrato < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Contrato invalido";
                    return result;

                }

                if (dto.CodigoIcp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo icp invalido";
                    return result;
                }
                if (dto.CodigoPuc == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Invalido";
                    return result;
                }

                var financiadoID = await _admDescriptivaRepository.GetByIdAndTitulo(13, dto.FinanciadoID);

                if (financiadoID == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Financiado ID Invalido";
                    return result;
                }
                if (dto.CodigoFinanciado == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo financiado Invalido";
                    return result;

                }
                if (dto.CodigoSaldo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Saldo Invalido";
                    return result;

                }

                if ( dto.MontoCausado + dto.Monto == dto.MontoAnulado)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "La suma del Monto Causado + Monto Anulado debe ser menor al Monto";
                    return result;
                }

                //(MONTO > 0 AND MONTO_ANULADO >= 0) OR (MONTO < 0 AND MONTO_ANULADO <= 0)
                //(MONTO > 0 AND MONTO_CAUSADO+MONTO_ANULADO <= MONTO) OR (MONTO < 0 AND MONTO_CAUSADO+MONTO_ANULADO >= MONTO)


                if (dto.MontoCausado + dto.Monto == dto.MontoAnulado)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Causado Invalido";
                    return result;

                }

                if (dto.MontoAnulado == dto.Monto)
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



                var codigopresupuesto = await _prePresupuestosRepository.GetByCodigo(conectado.Empresa, dto.CodigoPresupuesto);
                if (dto.CodigoPresupuesto < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Presupuesto Invalido";
                    return result;
                }



                ADM_PUC_CONTRATO entity = new ADM_PUC_CONTRATO();

                entity.CODIGO_PUC_CONTRATO = await _repository.GetNextKey();
                entity.CODIGO_CONTRATO = dto.CodigoContrato;
                entity.CODIGO_ICP = dto.CodigoIcp;
                entity.CODIGO_PUC = dto.CodigoPuc;
                entity.FINANCIADO_ID = dto.FinanciadoID;
                entity.CODIGO_FINANCIADO = dto.CodigoFinanciado;
                entity.CODIGO_SALDO = dto.CodigoSaldo;
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
                var resultDto = await MapPucContratoDto(created.Data);
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

        public async Task<ResultDto<AdmPucContratoDeleteDto>> Delete(AdmPucContratoDeleteDto dto) 
        {
            ResultDto<AdmPucContratoDeleteDto> result = new ResultDto<AdmPucContratoDeleteDto>(null);
            try
            {

                var codigoPucContrato = await _repository.GetCodigoPucContrato(dto.CodigoPucContrato);
                if (codigoPucContrato == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Contrato no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoPucContrato);

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

