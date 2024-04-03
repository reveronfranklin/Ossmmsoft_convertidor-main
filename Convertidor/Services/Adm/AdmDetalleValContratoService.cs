using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Bm;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Sis;
using MathNet.Numerics.RootFinding;
using Microsoft.JSInterop.Infrastructure;

namespace Convertidor.Services.Adm
{
    public class AdmDetalleValContratoService : IAdmDetalleValContratoService
    {
        private readonly IAdmDetalleValContratoRepository _repository;
        private readonly IAdmValContratoRepository _admValContratoRepository;
        private readonly IAdmContratosRepository _admContratosRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IPRE_PRESUPUESTOSRepository _prePresupuestosRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;

        public AdmDetalleValContratoService( IAdmDetalleValContratoRepository repository,
                                     IAdmValContratoRepository admValContratoRepository,
                                     IAdmContratosRepository admContratosRepository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     IPRE_PRESUPUESTOSRepository prePresupuestosRepository,
                                     IAdmDescriptivaRepository admDescriptivaRepository)
        {
            _repository = repository;
            _admValContratoRepository = admValContratoRepository;
            _admContratosRepository = admContratosRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _prePresupuestosRepository = prePresupuestosRepository;
            _admDescriptivaRepository = admDescriptivaRepository;
        }

      

        public async Task<AdmDetalleValContratoResponseDto> MapDetalleValContratoDto(ADM_DETALLE_VAL_CONTRATO dtos)
        {
            AdmDetalleValContratoResponseDto itemResult = new AdmDetalleValContratoResponseDto();

            itemResult.CodigoDetalleValContrato = dtos.CODIGO_DETALLE_VAL_CONTRATO;
            itemResult.CodigoValContrato = dtos.CODIGO_VAL_CONTRATO;
            itemResult.CodigoContrato = dtos.CODIGO_CONTRATO;
            itemResult.ConceptoID = dtos.CONCEPTO_ID;
            itemResult.ComplementoConcepto = dtos.COMPLEMENTO_CONCEPTO;
            itemResult.PorConcepto = dtos.POR_CONCEPTO;
            itemResult.MontoConcepto = dtos.MONTO_CONCEPTO;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
       


            return itemResult;
        }

        public async Task<List<AdmDetalleValContratoResponseDto>> MapListDetalleValContratoDto(List<ADM_DETALLE_VAL_CONTRATO> dtos)
        {
            List<AdmDetalleValContratoResponseDto> result = new List<AdmDetalleValContratoResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapDetalleValContratoDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public async Task<ResultDto<List<AdmDetalleValContratoResponseDto>>> GetAll()
        {

            ResultDto<List<AdmDetalleValContratoResponseDto>> result = new ResultDto<List<AdmDetalleValContratoResponseDto>>(null);
            try
            {
                var detalleValContrato = await _repository.GetAll();
                var cant = detalleValContrato.Count();
                if (detalleValContrato != null && detalleValContrato.Count() > 0)
                {
                    var listDto = await MapListDetalleValContratoDto(detalleValContrato);

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
        public async Task<ResultDto<AdmDetalleValContratoResponseDto>> Update(AdmDetalleValContratoUpdateDto dto)
        {
            ResultDto<AdmDetalleValContratoResponseDto> result = new ResultDto<AdmDetalleValContratoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoDetalleValContrato = await _repository.GetCodigoDetalleValContrato(dto.CodigoDetalleValContrato);
                if (codigoDetalleValContrato == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Val contrato no existe";
                    return result;
                }

                var codigoContrato = await _admContratosRepository.GetByCodigoContrato(dto.CodigoContrato);
                if(dto.CodigoContrato < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Contrato invalido";
                    return result;

                }

                var codigoValContrato = await _admValContratoRepository.GetCodigoValContrato(dto.CodigoValContrato);
                if (dto.CodigoValContrato < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Val Contrato invalido";
                    return result;

                }

                var conceptoId = await _admDescriptivaRepository.GetByIdAndTitulo(29, dto.ConceptoID);
                if (conceptoId == false) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Concepto Id invalido";
                    return result;
                }
                if(dto.ComplementoConcepto.Length > 100) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Complemento concepto Invalido";
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


                codigoDetalleValContrato.CODIGO_DETALLE_VAL_CONTRATO = dto.CodigoDetalleValContrato;
                codigoDetalleValContrato.CODIGO_CONTRATO = dto.CodigoContrato;
                codigoDetalleValContrato.CODIGO_VAL_CONTRATO = dto.CodigoValContrato;
                codigoDetalleValContrato.CONCEPTO_ID = dto.ConceptoID;
                codigoDetalleValContrato.COMPLEMENTO_CONCEPTO = dto.ComplementoConcepto;
                codigoDetalleValContrato.POR_CONCEPTO = dto.PorConcepto;
                codigoDetalleValContrato.MONTO_CONCEPTO = dto.MontoConcepto;
                codigoDetalleValContrato.EXTRA1 = dto.Extra1;
                codigoDetalleValContrato.EXTRA2 = dto.Extra2;
                codigoDetalleValContrato.EXTRA3 = dto.Extra3;
                codigoDetalleValContrato.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;





                codigoDetalleValContrato.CODIGO_EMPRESA = conectado.Empresa;
                codigoDetalleValContrato.USUARIO_UPD = conectado.Usuario;
                codigoDetalleValContrato.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoDetalleValContrato);

                var resultDto = await MapDetalleValContratoDto(codigoDetalleValContrato);
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

        public async Task<ResultDto<AdmDetalleValContratoResponseDto>> Create(AdmDetalleValContratoUpdateDto dto)
        {
            ResultDto<AdmDetalleValContratoResponseDto> result = new ResultDto<AdmDetalleValContratoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoDetalleValContrato = await _repository.GetCodigoDetalleValContrato(dto.CodigoDetalleValContrato);
                if (codigoDetalleValContrato != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Val contrato ya existe";
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

                var codigoValContrato = await _admValContratoRepository.GetCodigoValContrato(dto.CodigoValContrato);
                if (dto.CodigoValContrato < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Val Contrato invalido";
                    return result;

                }

                var conceptoId = await _admDescriptivaRepository.GetByIdAndTitulo(29, dto.ConceptoID);
                if (conceptoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Concepto Id invalido";
                    return result;
                }
                if (dto.ComplementoConcepto.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Complemento concepto Invalido";
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


            ADM_DETALLE_VAL_CONTRATO entity = new ADM_DETALLE_VAL_CONTRATO();
            
            entity.CODIGO_DETALLE_VAL_CONTRATO = await _repository.GetNextKey();
            entity.CODIGO_CONTRATO = dto.CodigoContrato;
            entity.CODIGO_VAL_CONTRATO = dto.CodigoValContrato;
            entity.CONCEPTO_ID = dto.ConceptoID;
            entity.COMPLEMENTO_CONCEPTO = dto.ComplementoConcepto;
            entity.POR_CONCEPTO = dto.PorConcepto;
            entity.MONTO_CONCEPTO = dto.MontoConcepto;
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
                var resultDto = await MapDetalleValContratoDto(created.Data);
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

        public async Task<ResultDto<AdmDetalleValContratoDeleteDto>> Delete(AdmDetalleValContratoDeleteDto dto) 
        {
            ResultDto<AdmDetalleValContratoDeleteDto> result = new ResultDto<AdmDetalleValContratoDeleteDto>(null);
            try
            {

                var codigoValContrato = await _repository.GetCodigoDetalleValContrato(dto.CodigoDetalleValContrato);
                if (codigoValContrato == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Val Contrato no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoDetalleValContrato);

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

