using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Bm;
using MathNet.Numerics.RootFinding;

namespace Convertidor.Services.Adm
{
    public class AdmPucReintegroService : IAdmPucReintegroService
    {
        private readonly IAdmPucReintegroRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;

        public AdmPucReintegroService(IAdmPucReintegroRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
        }

       
        public async Task<AdmPucReintegroResponseDto> MapPucReintegroDto(ADM_PUC_REINTEGRO dtos)
        {
            AdmPucReintegroResponseDto itemResult = new AdmPucReintegroResponseDto();
            itemResult.CodigoPucReintegro = dtos.CODIGO_PUC_REINTEGRO;
            itemResult.CodigoReintegro = dtos.CODIGO_REINTEGRO;
            itemResult.CodigoPucCompromiso = dtos.CODIGO_PUC_COMPROMISO;
            itemResult.CodigoIcp = dtos.CODIGO_ICP;
            itemResult.CodigoPuc = dtos.CODIGO_PUC;
            itemResult.FinanciadoId = dtos.FINANCIADO_ID;
            itemResult.CodigoFinanciado = dtos.CODIGO_FINANCIADO;
            itemResult.CodigoSaldo = dtos.CODIGO_SALDO;
            itemResult.Monto = dtos.MONTO;
            itemResult.MontoAnulado = dtos.MONTO_ANULADO;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            return itemResult;
        }

        public async Task<List<AdmPucReintegroResponseDto>> MapListPucReintegrosDto(List<ADM_PUC_REINTEGRO> dtos)
        {
            List<AdmPucReintegroResponseDto> result = new List<AdmPucReintegroResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapPucReintegroDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public async Task<ResultDto<List<AdmPucReintegroResponseDto>>> GetAll()
        {

            ResultDto<List<AdmPucReintegroResponseDto>> result = new ResultDto<List<AdmPucReintegroResponseDto>>(null);
            try
            {
                var pucReintegro = await _repository.GetAll();
                var cant = pucReintegro.Count();
                if (pucReintegro != null && pucReintegro.Count() > 0)
                {
                    var listDto = await MapListPucReintegrosDto(pucReintegro);

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

        public async Task<ResultDto<AdmPucReintegroResponseDto>> Update(AdmPucReintegroUpdateDto dto)
        {
            ResultDto<AdmPucReintegroResponseDto> result = new ResultDto<AdmPucReintegroResponseDto>(null);
            try
            {
                var codigoPucReintegro = await _repository.GetCodigoPucReintegro(dto.CodigoPucReintegro);
                if (codigoPucReintegro == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo puc Reintegro no existe";
                    return result;
                }
                if (dto.CodigoReintegro<0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo reintegro invalido";
                    return result;
                }

                if (dto.CodigoPucCompromiso <0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc compromiso invalido";
                    return result;

                }

                if (dto.CodigoIcp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;
                }
                if (dto.CodigoPuc == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Invalido";
                    return result;
                }
                var financiadoId = await _admDescriptivaRepository.GetByIdAndTitulo(13, dto.FinanciadoId);
                if (financiadoId==false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Financiado Id Invalido";
                    return result;
                }
                if (dto.CodigoFinanciado <0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo financiado deposito Invalido";
                    return result;
                }

                if (dto.CodigoSaldo <0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo saldo  no existe";
                    return result;

                }

                if (dto.Monto <0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Invalido";
                    return result;
                }

                if (dto.MontoAnulado < 0 && dto.MontoAnulado>=dto.Monto)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto anulado Invalido";
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



                codigoPucReintegro.CODIGO_PUC_REINTEGRO = dto.CodigoPucReintegro;
                codigoPucReintegro.CODIGO_REINTEGRO = dto.CodigoReintegro;
                codigoPucReintegro.CODIGO_PUC_COMPROMISO = dto.CodigoPucCompromiso;
                codigoPucReintegro.CODIGO_ICP = dto.CodigoIcp;
                codigoPucReintegro.CODIGO_PUC = dto.CodigoPuc;
                codigoPucReintegro.FINANCIADO_ID = dto.FinanciadoId;
                codigoPucReintegro.CODIGO_FINANCIADO = dto.CodigoFinanciado;
                codigoPucReintegro.CODIGO_SALDO = dto.CodigoSaldo;
                codigoPucReintegro.MONTO = dto.Monto;
                codigoPucReintegro.MONTO_ANULADO = dto.MontoAnulado;
                codigoPucReintegro.EXTRA1 = dto.Extra1;
                codigoPucReintegro.EXTRA2 = dto.Extra2;
                codigoPucReintegro.EXTRA3 = dto.Extra3;
                codigoPucReintegro.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;

                var conectado = await _sisUsuarioRepository.GetConectado();
                codigoPucReintegro.CODIGO_EMPRESA = conectado.Empresa;
                codigoPucReintegro.USUARIO_UPD = conectado.Usuario;
                codigoPucReintegro.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoPucReintegro);

                var resultDto = await MapPucReintegroDto(codigoPucReintegro);
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

        public async Task<ResultDto<AdmPucReintegroResponseDto>> Create(AdmPucReintegroUpdateDto dto)
        {
            ResultDto<AdmPucReintegroResponseDto> result = new ResultDto<AdmPucReintegroResponseDto>(null);
            try
            {
                var codigoPucReintegro = await _repository.GetCodigoPucReintegro(dto.CodigoPucReintegro);
                if (codigoPucReintegro != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo puc Reintegro ya existe";
                    return result;
                }
                if (dto.CodigoReintegro < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo reintegro invalido";
                    return result;
                }

                if (dto.CodigoPucCompromiso < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc compromiso invalido";
                    return result;

                }

                if (dto.CodigoIcp == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Icp Invalido";
                    return result;
                }
                if (dto.CodigoPuc == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Invalido";
                    return result;
                }
                var financiadoId = await _admDescriptivaRepository.GetByIdAndTitulo(13, dto.FinanciadoId);
                if (financiadoId ==false)
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
                    result.Message = "Codigo financiado deposito Invalido";
                    return result;
                }

                if (dto.CodigoSaldo < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo saldo  no existe";
                    return result;

                }

                if (dto.Monto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Invalido";
                    return result;
                }

                if (dto.MontoAnulado < 0 && dto.MontoAnulado >= dto.Monto)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto anulado Invalido";
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

                ADM_PUC_REINTEGRO entity = new ADM_PUC_REINTEGRO();
                entity.CODIGO_PUC_REINTEGRO = await _repository.GetNextKey();
                entity.CODIGO_REINTEGRO = dto.CodigoReintegro;
                entity.CODIGO_PUC_COMPROMISO = dto.CodigoPucCompromiso;
                entity.CODIGO_ICP = dto.CodigoIcp;
                entity.CODIGO_PUC = dto.CodigoPuc;
                entity.FINANCIADO_ID = dto.FinanciadoId;
                entity.CODIGO_FINANCIADO = dto.CodigoFinanciado;
                entity.CODIGO_SALDO = dto.CodigoSaldo;
                entity.MONTO = dto.Monto;
                entity.MONTO_ANULADO = dto.MontoAnulado;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;


            var conectado = await _sisUsuarioRepository.GetConectado();
            entity.CODIGO_EMPRESA = conectado.Empresa;
            entity.USUARIO_INS = conectado.Usuario;
            entity.FECHA_INS = DateTime.Now;

            var created = await _repository.Add(entity);
            if (created.IsValid && created.Data != null)
            {
                var resultDto = await MapPucReintegroDto(created.Data);
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

        public async Task<ResultDto<AdmPucReintegroDeleteDto>> Delete(AdmPucReintegroDeleteDto dto) 
        {
            ResultDto<AdmPucReintegroDeleteDto> result = new ResultDto<AdmPucReintegroDeleteDto>(null);
            try
            {

                var codigoReintegro = await _repository.GetCodigoPucReintegro(dto.CodigoPucReintegro);
                if (codigoReintegro == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo puc Reintegro no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoPucReintegro);

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

