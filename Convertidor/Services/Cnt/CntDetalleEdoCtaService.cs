using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Data.Repository.Cnt;
using Convertidor.Dtos.Cnt;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Utility;
namespace Convertidor.Services.Cnt
{
    //
    public class CntDetalleEdoCtaService : ICntDetalleEdoCtaService
    {
        private readonly ICntDetalleEdoCtaRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ICntEstadoCuentasRepository _cntEstadoCuentasRepository;
        private readonly ICntDescriptivaRepository _cntDescriptivaRepository;

        public CntDetalleEdoCtaService(ICntDetalleEdoCtaRepository repository,
                                        ISisUsuarioRepository sisUsuarioRepository,
                                        ICntEstadoCuentasRepository cntEstadoCuentasRepository,
                                        ICntDescriptivaRepository cntDescriptivaRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _cntEstadoCuentasRepository = cntEstadoCuentasRepository;
            _cntDescriptivaRepository = cntDescriptivaRepository;
        }

       

        public async Task<CntDetalleEdoCtaResponseDto> MapDetalleEdoCuenta(CNT_DETALLE_EDO_CTA dtos)
        {
            CntDetalleEdoCtaResponseDto itemResult = new CntDetalleEdoCtaResponseDto();
            itemResult.CodigoDetalleEdoCta = dtos.CODIGO_DETALLE_EDO_CTA;
            itemResult.CodigoEstadoCuenta = dtos.CODIGO_ESTADO_CUENTA;
            itemResult.TipoTransaccionId = dtos.TIPO_TRANSACCION_ID;
            itemResult.NumeroTransaccion = dtos.NUMERO_TRANSACCION;
            itemResult.FechaTransaccion = dtos.FECHA_TRANSACCION;
            itemResult.FechaTransaccionString = dtos.FECHA_TRANSACCION.ToString("u");
            FechaDto fechaTransaccionObj = FechaObj.GetFechaDto(dtos.FECHA_TRANSACCION);
            itemResult.FechaTransaccionObj = (FechaDto)fechaTransaccionObj;
            itemResult.Descripcion = dtos.DESCRIPCION;
            itemResult.Monto = dtos.MONTO;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.Status = dtos.STATUS;

            return itemResult;

        }

        public async Task<List<CntDetalleEdoCtaResponseDto>> MapListDetalleEdoCta(List<CNT_DETALLE_EDO_CTA> dtos)
        {
            List<CntDetalleEdoCtaResponseDto> result = new List<CntDetalleEdoCtaResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapDetalleEdoCuenta(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntDetalleEdoCtaResponseDto>>> GetAll()
        {

            ResultDto<List<CntDetalleEdoCtaResponseDto>> result = new ResultDto<List<CntDetalleEdoCtaResponseDto>>(null);
            try
            {
                var detalleEdoCta = await _repository.GetAll();
                var cant = detalleEdoCta.Count();
                if (detalleEdoCta != null && detalleEdoCta.Count() > 0)
                {
                    var listDto = await MapListDetalleEdoCta(detalleEdoCta);

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
        public async Task<ResultDto<List<CntDetalleEdoCtaResponseDto>>> GetAllByCodigoEstadoCuenta(int codigoEstadoCuenta)
        {

            ResultDto<List<CntDetalleEdoCtaResponseDto>> result = new ResultDto<List<CntDetalleEdoCtaResponseDto>>(null);
            try
            {

                var estadoCuenta = await _repository.GetByCodigoEstadoCuenta(codigoEstadoCuenta);



                if (estadoCuenta.Count() > 0)
                {
                    List<CntDetalleEdoCtaResponseDto> listDto = new List<CntDetalleEdoCtaResponseDto>();

                    foreach (var item in estadoCuenta)
                    {
                        var dto = await MapDetalleEdoCuenta(item);
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

        public async Task<ResultDto<CntDetalleEdoCtaResponseDto>> Create(CntDetalleEdoCtaUpdateDto dto)
        {
            ResultDto<CntDetalleEdoCtaResponseDto> result = new ResultDto<CntDetalleEdoCtaResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.CodigoEstadoCuenta <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Estado cuenta no existe";
                    return result;
                }

                var codigoEstadoCuenta = _cntEstadoCuentasRepository.GetByCodigo(dto.CodigoEstadoCuenta);
                if(codigoEstadoCuenta== null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Estado cuenta no existe";
                    return result;

                }

                if (dto.TipoTransaccionId <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Transaccion Id invalido";
                    return result;

                }

                var tipoTransaccionId = await _cntDescriptivaRepository.GetByIdAndTitulo(6, dto.TipoTransaccionId);
                if (tipoTransaccionId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Transaccion Id invalido";
                    return result;

                }

                var numeroTransaccion = Convert.ToInt32(dto.NumeroTransaccion);
                if (numeroTransaccion < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero transaccion invalido";
                    return result;

                }

                if (dto.NumeroTransaccion.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero transaccion invalido";
                    return result;

                }

                if (dto.FechaTransaccion == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "fecha transaccion invalida";
                    return result;
                }

                if (dto.Descripcion.Length > 1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion invalida";
                    return result;

                }

                if (dto.Monto < 0)
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

                if (dto.Status != "C" && dto.Status != "T")
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status invalido";
                    return result;

                }

                if (dto.Status.Length > 1)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status invalido";
                    return result;

                }




                CNT_DETALLE_EDO_CTA entity = new CNT_DETALLE_EDO_CTA();
                entity.CODIGO_DETALLE_EDO_CTA = await _repository.GetNextKey();
                entity.CODIGO_ESTADO_CUENTA = dto.CodigoEstadoCuenta;
                entity.TIPO_TRANSACCION_ID = dto.TipoTransaccionId;
                entity.NUMERO_TRANSACCION = dto.NumeroTransaccion;
                entity.DESCRIPCION = dto.Descripcion;
                entity.MONTO = dto.Monto;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.STATUS = dto.Status;




                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapDetalleEdoCuenta(created.Data);
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


        public async Task<ResultDto<CntDetalleEdoCtaResponseDto>> Update(CntDetalleEdoCtaUpdateDto dto)
        {
            ResultDto<CntDetalleEdoCtaResponseDto> result = new ResultDto<CntDetalleEdoCtaResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();


                var codigoDetalleEdoCta = await _repository.GetByCodigo(dto.CodigoDetalleEdoCta);
                if (codigoDetalleEdoCta == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Edo cta no existe";
                    return result;

                }


                if (dto.CodigoEstadoCuenta <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Estado cuenta no existe";
                    return result;
                }

                var codigoEstadoCuenta = _cntEstadoCuentasRepository.GetByCodigo(dto.CodigoEstadoCuenta);
                if (codigoEstadoCuenta == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Estado cuenta no existe";
                    return result;

                }

                if (dto.TipoTransaccionId <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Transaccion Id invalido";
                    return result;

                }

                var tipoTransaccionId = await _cntDescriptivaRepository.GetByIdAndTitulo(6, dto.TipoTransaccionId);
                if (tipoTransaccionId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Transaccion Id invalido";
                    return result;

                }

                var numeroTransaccion = Convert.ToInt32(dto.NumeroTransaccion);
                if(numeroTransaccion < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero transaccion invalido";
                    return result;

                }

                if (dto.NumeroTransaccion.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero transaccion invalido";
                    return result;

                }

                if (dto.FechaTransaccion == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "fecha transaccion invalida";
                    return result;
                }

                if (dto.Descripcion.Length > 1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion invalida";
                    return result;

                }

                if (dto.Monto < 0)
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

                if(dto.Status != "C" && dto.Status != "T") 
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status invalido";
                    return result;

                }
                if (dto.Status.Length > 1)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status invalido";
                    return result;

                }



                codigoDetalleEdoCta.CODIGO_DETALLE_EDO_CTA = dto.CodigoDetalleEdoCta;
                codigoDetalleEdoCta.CODIGO_ESTADO_CUENTA = dto.CodigoEstadoCuenta;
                codigoDetalleEdoCta.TIPO_TRANSACCION_ID = dto.TipoTransaccionId;
                codigoDetalleEdoCta.NUMERO_TRANSACCION = dto.NumeroTransaccion;
                codigoDetalleEdoCta.DESCRIPCION = dto.Descripcion;
                codigoDetalleEdoCta.MONTO = dto.Monto;
                codigoDetalleEdoCta.EXTRA1 = dto.Extra1;
                codigoDetalleEdoCta.EXTRA2 = dto.Extra2;
                codigoDetalleEdoCta.EXTRA3 = dto.Extra3;
                codigoDetalleEdoCta.STATUS = dto.Status;




                codigoDetalleEdoCta.CODIGO_EMPRESA = conectado.Empresa;
                codigoDetalleEdoCta.USUARIO_UPD = conectado.Usuario;
                codigoDetalleEdoCta.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoDetalleEdoCta);

                var resultDto = await MapDetalleEdoCuenta(codigoDetalleEdoCta);
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

        public async Task<ResultDto<CntDetalleEdoCtaDeleteDto>> Delete(CntDetalleEdoCtaDeleteDto dto)
        {
            ResultDto<CntDetalleEdoCtaDeleteDto> result = new ResultDto<CntDetalleEdoCtaDeleteDto>(null);
            try
            {

                var codigoDetalleEdoCta = await _repository.GetByCodigo(dto.CodigoDetalleEdoCta);
                if (codigoDetalleEdoCta == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Detalle Edo Cta no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoDetalleEdoCta);

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

        public async Task<ResultDto<CntDetalleEdoCtaResponseDto>> GetByCodigo(int codigoDetalleEdoCta)
        {
            ResultDto<CntDetalleEdoCtaResponseDto> result = new ResultDto<CntDetalleEdoCtaResponseDto>(null);
            try
            {

                var detalleCuenta = await _repository.GetByCodigo(codigoDetalleEdoCta);
                if (detalleCuenta == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Detalle Estado Cuenta no existe";
                    return result;
                }

                var resultDto = await MapDetalleEdoCuenta(detalleCuenta);
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