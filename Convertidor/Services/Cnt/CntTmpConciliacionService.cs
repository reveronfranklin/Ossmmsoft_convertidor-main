using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;
using Convertidor.Utility;
using NPOI.SS.Formula.Functions;

namespace Convertidor.Services.Cnt
{
    public class CntTmpConciliacionService : ICntTmpConciliacionService
    {
        private readonly ICntTmpConciliacionRepository _repository;
        private readonly ICntDetalleLibroService _cntDetalleLibroService;
        private readonly ICntDetalleEdoCtaService _cntDetalleEdoCtaService;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public CntTmpConciliacionService(ICntTmpConciliacionRepository repository,
                                          ICntDetalleLibroService cntDetalleLibroService,
                                          ICntDetalleEdoCtaService cntDetalleEdoCtaService,
                                          ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _cntDetalleLibroService = cntDetalleLibroService;
            _cntDetalleEdoCtaService = cntDetalleEdoCtaService;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<CntTmpConciliacionResponseDto> MapCntTmpConciliacion(CNT_TMP_CONCILIACION dtos)
        {
            CntTmpConciliacionResponseDto itemResult = new CntTmpConciliacionResponseDto();
            itemResult.CodigoTmpConciliacion = dtos.CODIGO_TMP_CONCILIACION;
            itemResult.CodigoConciliacion = dtos.CODIGO_CONCILIACION;
            itemResult.CodigoPeriodo = dtos.CODIGO_PERIODO;
            itemResult.CodigoCuentaBanco = dtos.CODIGO_CUENTA_BANCO;
            itemResult.CodigoDetalleLibro = dtos.CODIGO_DETALLE_LIBRO;
            itemResult.CodigoDetalleEdoCta = dtos.CODIGO_DETALLE_EDO_CTA;
            itemResult.Fecha = dtos.FECHA;
            itemResult.FechaString = dtos.FECHA.ToString("u");
            FechaDto fechaObj = FechaObj.GetFechaDto(dtos.FECHA);
            itemResult.FechaObj = (FechaDto)fechaObj;
            itemResult.Numero = dtos.NUMERO;
            itemResult.Monto = dtos.MONTO;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;


            return itemResult;
        }

        public async Task<List<CntTmpConciliacionResponseDto>> MapListCntTmpConciliacion(List<CNT_TMP_CONCILIACION> dtos)
        {
            List<CntTmpConciliacionResponseDto> result = new List<CntTmpConciliacionResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapCntTmpConciliacion(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntTmpConciliacionResponseDto>>> GetAll()
        {

            ResultDto<List<CntTmpConciliacionResponseDto>> result = new ResultDto<List<CntTmpConciliacionResponseDto>>(null);
            try
            {
                var tmpConciliacion = await _repository.GetAll();
                var cant = tmpConciliacion.Count();
                if (tmpConciliacion != null && tmpConciliacion.Count() > 0)
                {
                    var listDto = await MapListCntTmpConciliacion(tmpConciliacion);

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

        public async Task<ResultDto<CntTmpConciliacionResponseDto>> Create(CntTmpConciliacionUpdateDto dto)
        {
            ResultDto<CntTmpConciliacionResponseDto> result = new ResultDto<CntTmpConciliacionResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                
                if (dto.CodigoConciliacion <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo conciliacion invalido";
                    return result;


                }


                if (dto.CodigoPeriodo <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Periodo invalido";
                    return result;
                }

                if (dto.CodigoCuentaBanco <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Cuenta Banco Invalida";
                    return result;
                }

                if(dto.CodigoDetalleLibro <= 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Libro Invalido";
                    return result;

                }

                var codigoDetalleLibro = await _cntDetalleLibroService.GetByCodigo(dto.CodigoDetalleLibro);
                if (codigoDetalleLibro == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Libro Invalido";
                    return result;

                }

                if(dto.CodigoDetalleEdoCta <= 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Edo Cta Invalido";
                    return result;

                }

                var codigoDetalleEdoCta = await _cntDetalleEdoCtaService.GetByCodigo(dto.CodigoDetalleEdoCta);
                if(codigoDetalleEdoCta == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Edo Cta Invalido";
                    return result;

                }

                var fecha = dto.Fecha.ToShortDateString();
                if(fecha == "1/1/0001") 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Invalida";
                    return result;

                }

                var numero = Convert.ToInt32(dto.Numero);
                if(numero <= 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Invalido";
                    return result;

                }

                if(dto.Numero.Length > 20) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Invalido";
                    return result;

                }


                if(dto.Monto <= 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Invalido";
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






                CNT_TMP_CONCILIACION entity = new CNT_TMP_CONCILIACION();
                entity.CODIGO_TMP_CONCILIACION = await _repository.GetNextKey();
                entity.CODIGO_CONCILIACION = dto.CodigoConciliacion;
                entity.CODIGO_PERIODO = dto.CodigoPeriodo;
                entity.CODIGO_CUENTA_BANCO = dto.CodigoCuentaBanco;
                entity.CODIGO_DETALLE_LIBRO = dto.CodigoDetalleLibro;
                entity.CODIGO_DETALLE_EDO_CTA = dto.CodigoDetalleEdoCta;
                entity.FECHA = dto.Fecha;
                entity.NUMERO = dto.Numero;
                entity.MONTO = dto.Monto;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;





                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapCntTmpConciliacion(created.Data);
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
