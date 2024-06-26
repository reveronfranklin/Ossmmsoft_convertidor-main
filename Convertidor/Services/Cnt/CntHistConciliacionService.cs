using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;
using Convertidor.Utility;

namespace Convertidor.Services.Cnt
{
    public class CntHistConciliacionService : ICntHistConciliacionService
    {
        private readonly ICntHistConciliacionRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ICntDetalleLibroRepository _cntDetalleLibroRepository;
        private readonly ICntDetalleEdoCtaRepository _cntDetalleEdoCtaRepository;

        public CntHistConciliacionService(ICntHistConciliacionRepository repository,
                                           ISisUsuarioRepository sisUsuarioRepository,
                                           ICntDetalleLibroRepository cntDetalleLibroRepository,
                                           ICntDetalleEdoCtaRepository cntDetalleEdoCtaRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _cntDetalleLibroRepository = cntDetalleLibroRepository;
            _cntDetalleEdoCtaRepository = cntDetalleEdoCtaRepository;
        }




        public async Task<CntHistConciliacionResponseDto> MapCntHistConciliacion(CNT_HIST_CONCILIACION dtos)
        {
            CntHistConciliacionResponseDto itemResult = new CntHistConciliacionResponseDto();
            itemResult.CodigoHistConciliacion = dtos.CODIGO_HIST_CONCILIACION;
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

        public async Task<List<CntHistConciliacionResponseDto>> MapListCntHistoricoConciliacion(List<CNT_HIST_CONCILIACION> dtos)
        {
            List<CntHistConciliacionResponseDto> result = new List<CntHistConciliacionResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapCntHistConciliacion(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntHistConciliacionResponseDto>>> GetAll()
        {

            ResultDto<List<CntHistConciliacionResponseDto>> result = new ResultDto<List<CntHistConciliacionResponseDto>>(null);
            try
            {
                var historicoConciliacion = await _repository.GetAll();
                var cant = historicoConciliacion.Count();
                if (historicoConciliacion != null && historicoConciliacion.Count() > 0)
                {
                    var listDto = await MapListCntHistoricoConciliacion(historicoConciliacion);

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

        public async Task<ResultDto<List<CntHistConciliacionResponseDto>>> GetAllByCodigoConciliacion(int codigoConciliacion)
        {

            ResultDto<List<CntHistConciliacionResponseDto>> result = new ResultDto<List<CntHistConciliacionResponseDto>>(null);
            try
            {

                var conciliacion = await _repository.GetByCodigoConciliacion(codigoConciliacion);



                if (conciliacion.Count() > 0)
                {
                    List<CntHistConciliacionResponseDto> listDto = new List<CntHistConciliacionResponseDto>();

                    foreach (var item in conciliacion)
                    {
                        var dto = await MapCntHistConciliacion(item);
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
                    result.Message = "No existen Datos";

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

        public async Task<ResultDto<CntHistConciliacionResponseDto>> Create(CntHistConciliacionUpdateDto dto)
        {
            ResultDto<CntHistConciliacionResponseDto> result = new ResultDto<CntHistConciliacionResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();


                if (dto.CodigoConciliacion <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo conciliacion Invalido";
                    return result;
                }

                if (dto.CodigoPeriodo <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo periodo invalido";
                    return result;
                }


                if (dto.CodigoCuentaBanco <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Cuenta Banco invalido";
                    return result;

                }

                if (dto.CodigoDetalleLibro <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Detalle Libro Invalido";
                    return result;

                }

                var codigoDetalleLibro = await _cntDetalleLibroRepository.GetByCodigo(dto.CodigoDetalleLibro);
                if(codigoDetalleLibro == null) 
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Detalle Libro Invalido";
                    return result;

                }

                if (dto.CodigoDetalleEdoCta <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Detalle Edo Cta invalido";
                    return result;

                }

                var codigoDetalleEdoCta = await _cntDetalleEdoCtaRepository.GetByCodigo(dto.CodigoDetalleEdoCta);
                if(codigoDetalleEdoCta == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Detalle Edo Cta invalido";
                    return result;

                }

                if (dto.Fecha == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha invalida";
                    return result;
                }

                var numero = Convert.ToInt32(dto.Numero);
                if (numero < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero invalido";
                    return result;

                }

                if(dto.Numero.Length > 20) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero invalido";
                    return result;
                }

                if(dto.Monto <= 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto invalido";
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






                CNT_HIST_CONCILIACION entity = new CNT_HIST_CONCILIACION();
                entity.CODIGO_HIST_CONCILIACION = await _repository.GetNextKey();
                entity.CODIGO_CONCILIACION = dto.CodigoConciliacion;
                entity.CODIGO_PERIODO = dto.CodigoPeriodo;
                entity.CODIGO_CUENTA_BANCO = dto.CodigoCuentaBanco;
                entity.CODIGO_DETALLE_LIBRO = dto.CodigoDetalleLibro;
                entity.CODIGO_DETALLE_EDO_CTA = dto.CodigoDetalleEdoCta;
                entity.FECHA = dto.Fecha;
                entity.NUMERO = dto.Numero;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;





                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapCntHistConciliacion(created.Data);
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