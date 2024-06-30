using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Data.Repository.Cnt;
using Convertidor.Dtos.Cnt;
using Convertidor.Utility;

namespace Convertidor.Services.Cnt
{
    public class CntReversoConciliacionService : ICntReversoConciliacionService
    {
        private readonly ICntReversoConciliacionRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ICntDetalleLibroService _cntDetalleLibroService;
        private readonly ICntDetalleEdoCtaService _cntDetalleEdoCtaService;

        public CntReversoConciliacionService(ICntReversoConciliacionRepository repository,
                                              ISisUsuarioRepository sisUsuarioRepository,
                                              ICntDetalleLibroService cntDetalleLibroService,
                                              ICntDetalleEdoCtaService cntDetalleEdoCtaService)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _cntDetalleLibroService = cntDetalleLibroService;
            _cntDetalleEdoCtaService = cntDetalleEdoCtaService;
        }

        public async Task<CntReversoConciliacionResponseDto> MapCntReversoConciliacion(CNT_REVERSO_CONCILIACION dtos)
        {
            CntReversoConciliacionResponseDto itemResult = new CntReversoConciliacionResponseDto();
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

        public async Task<List<CntReversoConciliacionResponseDto>> MapListCntReversoConciliacion(List<CNT_REVERSO_CONCILIACION> dtos)
        {
            List<CntReversoConciliacionResponseDto> result = new List<CntReversoConciliacionResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapCntReversoConciliacion(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntReversoConciliacionResponseDto>>> GetAllByCodigoConciliacion(int codigoConciliacion)
        {

            ResultDto<List<CntReversoConciliacionResponseDto>> result = new ResultDto<List<CntReversoConciliacionResponseDto>>(null);
            try
            {

                var conciliacion = await _repository.GetByCodigoConciliacion(codigoConciliacion);



                if (conciliacion.Count() > 0)
                {
                    List<CntReversoConciliacionResponseDto> listDto = new List<CntReversoConciliacionResponseDto>();

                    foreach (var item in conciliacion)
                    {
                        var dto = await MapCntReversoConciliacion(item);
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

        public async Task<ResultDto<CntReversoConciliacionResponseDto>> Create(CntReversoConciliacionUpdateDto dto)
        {
            ResultDto<CntReversoConciliacionResponseDto> result = new ResultDto<CntReversoConciliacionResponseDto>(null);
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

                var codigoDetalleLibro = await _cntDetalleLibroService.GetByCodigo(dto.CodigoDetalleLibro);
                if (codigoDetalleLibro == null)
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

                var codigoDetalleEdoCta = await _cntDetalleEdoCtaService.GetByCodigo(dto.CodigoDetalleEdoCta);
                if (codigoDetalleEdoCta == null)
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

                if (dto.Numero.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero invalido";
                    return result;
                }

                if (dto.Monto <= 0)
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






                CNT_REVERSO_CONCILIACION entity = new CNT_REVERSO_CONCILIACION();
                entity.CODIGO_HIST_CONCILIACION = await _repository.GetNextKey();
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
                    var resultDto = await MapCntReversoConciliacion(created.Data);
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
        public async Task<ResultDto<CntReversoConciliacionResponseDto>> Update(CntReversoConciliacionUpdateDto dto)
        {
            ResultDto<CntReversoConciliacionResponseDto> result = new ResultDto<CntReversoConciliacionResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoHistConciliacion = await _repository.GetByCodigo(dto.CodigoHistConciliacion);
                if (codigoHistConciliacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Historico conciliacion Invalido";
                    return result;

                }

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

                var codigoDetalleLibro = await _cntDetalleLibroService.GetByCodigo(dto.CodigoDetalleLibro);
                if (codigoDetalleLibro == null)
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

                var codigoDetalleEdoCta = await _cntDetalleEdoCtaService.GetByCodigo(dto.CodigoDetalleEdoCta);
                if (codigoDetalleEdoCta == null)
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

                if (dto.Numero.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero invalido";
                    return result;
                }

                if (dto.Monto <= 0)
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




                codigoHistConciliacion.CODIGO_HIST_CONCILIACION = dto.CodigoHistConciliacion;
                codigoHistConciliacion.CODIGO_CONCILIACION = dto.CodigoConciliacion;
                codigoHistConciliacion.CODIGO_PERIODO = dto.CodigoPeriodo;
                codigoHistConciliacion.CODIGO_CUENTA_BANCO = dto.CodigoCuentaBanco;
                codigoHistConciliacion.CODIGO_DETALLE_LIBRO = dto.CodigoDetalleLibro;
                codigoHistConciliacion.CODIGO_DETALLE_EDO_CTA = dto.CodigoDetalleEdoCta;
                codigoHistConciliacion.FECHA = dto.Fecha;
                codigoHistConciliacion.NUMERO = dto.Numero;
                codigoHistConciliacion.MONTO = dto.Monto;
                codigoHistConciliacion.EXTRA1 = dto.Extra1;
                codigoHistConciliacion.EXTRA2 = dto.Extra2;
                codigoHistConciliacion.EXTRA3 = dto.Extra3;




                codigoHistConciliacion.CODIGO_EMPRESA = conectado.Empresa;
                codigoHistConciliacion.USUARIO_UPD = conectado.Usuario;
                codigoHistConciliacion.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoHistConciliacion);

                var resultDto = await MapCntReversoConciliacion(codigoHistConciliacion);
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

    }
}
