using Convertidor.Data.Entities;
using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;
using Convertidor.Utility;

namespace Convertidor.Services.Cnt
{
    public class CntComprobantesService : ICntComprobantesService
    {
        private readonly ICntComprobantesRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ICntPeriodosService _cntPeriodosService;
        private readonly ICntDescriptivaRepository _cntDescriptivaRepository;

        public CntComprobantesService(ICntComprobantesRepository repository,
                                       ISisUsuarioRepository sisUsuarioRepository,
                                       ICntPeriodosService cntPeriodosService,
                                       ICntDescriptivaRepository cntDescriptivaRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _cntPeriodosService = cntPeriodosService;
            _cntDescriptivaRepository = cntDescriptivaRepository;
        }

        public async Task<CntComprobantesResponseDto> MapComprobantes(CNT_COMPROBANTES dtos)
        {
            CntComprobantesResponseDto itemResult = new CntComprobantesResponseDto();
            itemResult.CodigoComprobante = dtos.CODIGO_COMPROBANTE;
            itemResult.CodigoPeriodo = dtos.CODIGO_PERIODO;
            itemResult.TipoComprobanteId = dtos.TIPO_COMPROBANTE_ID;
            itemResult.NumeroComprobante = dtos.NUMERO_COMPROBANTE;
            itemResult.FechaComprobante = dtos.FECHA_COMPROBANTE;
            itemResult.FechaComprobanteString = dtos.FECHA_COMPROBANTE.ToString("u");
            FechaDto fechaComprobanteObj = FechaObj.GetFechaDto(dtos.FECHA_COMPROBANTE);
            itemResult.FechaComprobanteObj = (FechaDto)fechaComprobanteObj;
            itemResult.OrigenId = dtos.ORIGEN_ID;
            itemResult.Observacion = dtos.OBSERVACION;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;

            return itemResult;
        }

        public async Task<List<CntComprobantesResponseDto>> MapListComprobantes(List<CNT_COMPROBANTES> dtos)
        {
            List<CntComprobantesResponseDto> result = new List<CntComprobantesResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapComprobantes(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntComprobantesResponseDto>>> GetAll()
        {

            ResultDto<List<CntComprobantesResponseDto>> result = new ResultDto<List<CntComprobantesResponseDto>>(null);
            try
            {
                var comprobantes = await _repository.GetAll();
                var cant = comprobantes.Count();
                if (comprobantes != null && comprobantes.Count() > 0)
                {
                    var listDto = await MapListComprobantes(comprobantes);

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

        public async Task<ResultDto<CntComprobantesResponseDto>> Create(CntComprobantesUpdateDto dto)
        {
            ResultDto<CntComprobantesResponseDto> result = new ResultDto<CntComprobantesResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.CodigoPeriodo <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Periodo invalido";
                    return result;

                }

                var codigoPeriodo = await _cntPeriodosService.GetByCodigo(dto.CodigoPeriodo);
                if (codigoPeriodo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Periodo invalido";
                    return result;


                }


                if (dto.TipoComprobanteId <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Comprobante Id invalido";
                    return result;
                }

                var tipoComprobanteId = await _cntDescriptivaRepository.GetByIdAndTitulo(5, dto.TipoComprobanteId);
                if (tipoComprobanteId == false)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo comprobante Id invalido";
                    return result;

                }

                if (dto.NumeroComprobante.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Comprobante Invalido";
                    return result;

                }

                var numeroComprobante = await _repository.GetByNumeroComprobante(dto.NumeroComprobante);
                if (numeroComprobante != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Comprobante Invalido";
                    return result;
                }


                if (dto.FechaComprobante == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Comprobante Invalido";
                    return result;
                }

                if(dto.OrigenId <= 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Origen Id Invalido";
                    return result;

                }

                var origenId = await _cntDescriptivaRepository.GetByIdAndTitulo(1, dto.OrigenId);
                if(origenId == false) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Origen Id Invalido";
                    return result;

                }

                if (dto.Observacion.Length > 1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Onservacion Invalida";
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


                



                CNT_COMPROBANTES entity = new CNT_COMPROBANTES();
                entity.CODIGO_COMPROBANTE = await _repository.GetNextKey();
                entity.CODIGO_PERIODO = dto.CodigoPeriodo;
                entity.TIPO_COMPROBANTE_ID = dto.TipoComprobanteId;
                entity.NUMERO_COMPROBANTE = dto.NumeroComprobante; 
                entity.FECHA_COMPROBANTE = dto.FechaComprobante;
                entity.ORIGEN_ID = dto.OrigenId;
                entity.OBSERVACION = dto.Observacion;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;


                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapComprobantes(created.Data);
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

    }
}
