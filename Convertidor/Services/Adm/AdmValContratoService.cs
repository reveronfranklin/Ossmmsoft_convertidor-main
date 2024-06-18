using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Adm;
using Convertidor.Utility;

namespace Convertidor.Services.Adm
{
    public class AdmValContratoService : IAdmValContratoService
    {
        private readonly IAdmValContratoRepository _repository;
        private readonly IAdmContratosRepository _admContratosRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IPRE_PRESUPUESTOSRepository _prePresupuestosRepository;
        private readonly IAdmDescriptivaRepository _admDescriptivaRepository;

        public AdmValContratoService(  IAdmValContratoRepository repository,
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

      
        public async Task<AdmValContratoResponseDto> MapValContratoDto(ADM_VAL_CONTRATO dtos)
        {
            AdmValContratoResponseDto itemResult = new AdmValContratoResponseDto();
            itemResult.CodigoValContrato = dtos.CODIGO_VAL_CONTRATO;
            itemResult.CodigoContrato = dtos.CODIGO_CONTRATO;
            itemResult.TipoValuacion = dtos.TIPO_VALUACION;
            itemResult.FechaValuacion = dtos.FECHA_VALUACION;
            itemResult.FechaValuacionString = Fecha.GetFechaString(dtos.FECHA_VALUACION);
            FechaDto fechaValuacionObj = Fecha.GetFechaDto(dtos.FECHA_VALUACION);
            itemResult.FechaValuacionObj = (FechaDto)fechaValuacionObj;
            itemResult.NumeroValuacion = dtos.NUMERO_VALUACION;
            itemResult.FechaIni = dtos.FECHA_INI;
            itemResult.FechaIniString =Fecha.GetFechaString(dtos.FECHA_INI);
            FechaDto fechaIniObj = Fecha.GetFechaDto(dtos.FECHA_INI);
            itemResult.FechaIniObj = (FechaDto)fechaIniObj;
            itemResult.FechaFin = dtos.FECHA_FIN;
            itemResult.FechaFinString = Fecha.GetFechaString(dtos.FECHA_FIN);
            FechaDto fechaFinObj = Fecha.GetFechaDto(dtos.FECHA_FIN);
            itemResult.FechaFinObj = (FechaDto)fechaFinObj;
            itemResult.FechaAprobacion = dtos.FECHA_APROBACION;
            itemResult.FechaAprobacionString = Fecha.GetFechaString(dtos.FECHA_APROBACION);
            FechaDto fechaAprobacionObj = Fecha.GetFechaDto(dtos.FECHA_APROBACION);
            itemResult.FechaAprobacionObj = (FechaDto)fechaAprobacionObj;
            itemResult.NumeroAprobacion = dtos.NUMERO_APROBACION; 
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.Status = dtos.STATUS;
            itemResult.CodigoPresupuesto = dtos.CODIGO_PRESUPUESTO;
            itemResult.Monto = dtos.MONTO;
            itemResult.MontoCausado = dtos.MONTO_CAUSADO;
            itemResult.MontoAnulado = dtos.MONTO_ANULADO;


            return itemResult;
        }

        public async Task<List<AdmValContratoResponseDto>> MapListValContratoDto(List<ADM_VAL_CONTRATO> dtos)
        {
            List<AdmValContratoResponseDto> result = new List<AdmValContratoResponseDto>();
            {
                foreach (var item in dtos)
                {

                    var itemResult = await MapValContratoDto(item);

                    result.Add(itemResult);
                }
                return result;
            }
        }

        public async Task<ResultDto<List<AdmValContratoResponseDto>>> GetAll()
        {

            ResultDto<List<AdmValContratoResponseDto>> result = new ResultDto<List<AdmValContratoResponseDto>>(null);
            try
            {
                var valContrato = await _repository.GetAll();
                var cant = valContrato.Count();
                if (valContrato != null && valContrato.Count() > 0)
                {
                    var listDto = await MapListValContratoDto(valContrato);

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
        public async Task<ResultDto<AdmValContratoResponseDto>> Update(AdmValContratoUpdateDto dto)
        {
            ResultDto<AdmValContratoResponseDto> result = new ResultDto<AdmValContratoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoValContrato = await _repository.GetCodigoValContrato(dto.CodigoValContrato);
                if (codigoValContrato == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Val contrato no existe";
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

                if(dto.TipoValuacion == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Valuacion invalido";
                    return result;
                }
                if(dto.FechaValuacion ==null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Valuacion Invalida";
                    return result;
                }

                
                if (dto.NumeroValuacion.Length >20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Valuacion Invalido";
                    return result;
                }
                if (dto.FechaIni == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha inicial Invalida";
                    return result;

                }
                if (dto.FechaFin == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Final Invalida";
                    return result;

                }

                if (dto.FechaAprobacion == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha de Aprobacion no existe";
                    return result;

                }

                if(dto.NumeroAprobacion.Length > 20) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Aprobacion Invalido";
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

                if (dto.Status.Length > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
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

                if(dto.Monto < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto  Invalido";
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


                codigoValContrato.CODIGO_VAL_CONTRATO = dto.CodigoValContrato;
                codigoValContrato.CODIGO_CONTRATO = dto.CodigoContrato;
                codigoValContrato.TIPO_VALUACION = dto.TipoValuacion;
                codigoValContrato.FECHA_VALUACION = dto.FechaValuacion;
                codigoValContrato.NUMERO_VALUACION = dto.NumeroValuacion;
                codigoValContrato.FECHA_INI = dto.FechaIni;
                codigoValContrato.FECHA_FIN = dto.FechaFin;
                codigoValContrato.FECHA_APROBACION = dto.FechaAprobacion;
                codigoValContrato.EXTRA1 = dto.Extra1;
                codigoValContrato.EXTRA2 = dto.Extra2;
                codigoValContrato.EXTRA3 = dto.Extra3;
                codigoValContrato.STATUS = dto.Status;
                codigoValContrato.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
                codigoValContrato.MONTO = dto.Monto;
                codigoValContrato.MONTO_CAUSADO = dto.MontoCausado;
                codigoValContrato.MONTO_ANULADO = dto.MontoAnulado;



                codigoValContrato.CODIGO_EMPRESA = conectado.Empresa;
                codigoValContrato.USUARIO_UPD = conectado.Usuario;
                codigoValContrato.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoValContrato);

                var resultDto = await MapValContratoDto(codigoValContrato);
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

        public async Task<ResultDto<AdmValContratoResponseDto>> Create(AdmValContratoUpdateDto dto)
        {
            ResultDto<AdmValContratoResponseDto> result = new ResultDto<AdmValContratoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoValContrato = await _repository.GetCodigoValContrato(dto.CodigoValContrato);
                if (codigoValContrato != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Val contrato no existe";
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

                if (dto.TipoValuacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Valuacion invalido";
                    return result;
                }
                if (dto.FechaValuacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Valuacion Invalida";
                    return result;
                }


                if (dto.NumeroValuacion.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Valuacion Invalido";
                    return result;
                }
                if (dto.FechaIni == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha inicial Invalida";
                    return result;

                }
                if (dto.FechaFin == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Final Invalida";
                    return result;

                }

                if (dto.FechaAprobacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha de Aprobacion no existe";
                    return result;

                }

                if (dto.NumeroAprobacion.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Aprobacion Invalido";
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

                if (dto.Status.Length > 2)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status Invalido";
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

                if (dto.Monto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto  Invalido";
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


            ADM_VAL_CONTRATO entity = new ADM_VAL_CONTRATO();
             
            entity.CODIGO_VAL_CONTRATO = await _repository.GetNextKey();
            entity.CODIGO_CONTRATO = dto.CodigoContrato;
            entity.TIPO_VALUACION = dto.TipoValuacion;
            entity.FECHA_VALUACION = dto.FechaValuacion;
            entity.NUMERO_VALUACION = dto.NumeroValuacion;
            entity.FECHA_INI = dto.FechaIni;
            entity.FECHA_FIN = dto.FechaFin;
            entity.FECHA_APROBACION = dto.FechaAprobacion;
            entity.EXTRA1 = dto.Extra1;
            entity.EXTRA2 = dto.Extra2;
            entity.EXTRA3 = dto.Extra3;
            entity.STATUS = dto.Status;
            entity.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
            entity.MONTO = dto.Monto;
            entity.MONTO_CAUSADO = dto.MontoCausado;
            entity.MONTO_ANULADO = dto.MontoAnulado;



            entity.CODIGO_EMPRESA = conectado.Empresa;
            entity.USUARIO_INS = conectado.Usuario;
            entity.FECHA_INS = DateTime.Now;

            var created = await _repository.Add(entity);
            if (created.IsValid && created.Data != null)
            {
                var resultDto = await MapValContratoDto(created.Data);
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

        public async Task<ResultDto<AdmValContratoDeleteDto>> Delete(AdmValContratoDeleteDto dto) 
        {
            ResultDto<AdmValContratoDeleteDto> result = new ResultDto<AdmValContratoDeleteDto>(null);
            try
            {

                var codigoValContrato = await _repository.GetCodigoValContrato(dto.CodigoValContrato);
                if (codigoValContrato == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Val Contrato no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoValContrato);

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

