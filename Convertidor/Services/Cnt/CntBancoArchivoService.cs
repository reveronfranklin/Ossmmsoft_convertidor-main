using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Cnt;
using NPOI.SS.Formula.Functions;

namespace Convertidor.Services.Cnt
{
    public class CntBancoArchivoService : ICntBancoArchivoService
    {
        private readonly ICntBancoArchivoRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ICntDescriptivaRepository _cntDescriptivaRepository;

        public CntBancoArchivoService(ICntBancoArchivoRepository repository,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      ICntDescriptivaRepository cntDescriptivaRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _cntDescriptivaRepository = cntDescriptivaRepository;
        }


        public FechaDto GetFechaDto(DateTime fecha)
        {
            var FechaDesdeObj = new FechaDto();
            FechaDesdeObj.Year = fecha.Year.ToString();
            string month = "00" + fecha.Month.ToString();
            string day = "00" + fecha.Day.ToString();
            FechaDesdeObj.Month = month.Substring(month.Length - 2);
            FechaDesdeObj.Day = day.Substring(day.Length - 2);

            return FechaDesdeObj;
        }
        public async Task<CntBancoArchivoResponseDto> MapCntBancoArchivo(CNT_BANCO_ARCHIVO dtos) 
        {
           CntBancoArchivoResponseDto itemResult = new CntBancoArchivoResponseDto();
            itemResult.CodigoBancoArchivo = dtos.CODIGO_BANCO_ARCHIVO;
            itemResult.CodigoBancoArchivoControl = dtos.CODIGO_BANCO_ARCHIVO_CONTROL;
            itemResult.NumeroBanco = dtos.NUMERO_BANCO;
            itemResult.NumeroCuenta = dtos.NUMERO_CUENTA;
            itemResult.FechaTransaccion = dtos.FECHA_TRANSACCION;
            itemResult.FechaTransaccionString = dtos.FECHA_TRANSACCION.ToString("u");
            FechaDto fechaTransaccionObj = GetFechaDto(dtos.FECHA_TRANSACCION);
            itemResult.FechaTransaccionObj = (FechaDto)fechaTransaccionObj;
            itemResult.NumeroTransaccion = dtos.NUMERO_TRANSACCION;
            itemResult.TipoTransaccionId = dtos.TIPO_TRANSACCION_ID;
            itemResult.TipoTransaccion = dtos.TIPO_TRANSACCION;
            itemResult.DescripcionTransaccion = dtos.DESCRIPCION_TRANSACCION;
            itemResult.MontoTransaccion = dtos.MONTO_TRANSACCION;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.CodigoDetalleEdoCta = dtos.CODIGO_DETALLE_EDO_CTA;
        
            return itemResult;
        }

        public async Task<List<CntBancoArchivoResponseDto>> MapListCntBancoArchivo(List<CNT_BANCO_ARCHIVO> dtos)
        {
            List<CntBancoArchivoResponseDto> result = new List<CntBancoArchivoResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapCntBancoArchivo(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntBancoArchivoResponseDto>>> GetAll()
        {

            ResultDto<List<CntBancoArchivoResponseDto>> result = new ResultDto<List<CntBancoArchivoResponseDto>>(null);
            try
            {
                var bancoArchivo = await _repository.GetAll();
                var cant = bancoArchivo.Count();
                if (bancoArchivo != null && bancoArchivo.Count() > 0)
                {
                    var listDto = await MapListCntBancoArchivo(bancoArchivo);

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

        public async Task<ResultDto<CntBancoArchivoResponseDto>> Create(CntBancoArchivoUpdateDto dto)
        {
            ResultDto<CntBancoArchivoResponseDto> result = new ResultDto<CntBancoArchivoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();


                if (dto.CodigoBancoArchivoControl <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Banco Archivo Control no existe";
                    return result;
                }

          
                if (dto.NumeroBanco.Length > 4) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Banco invalido";
                    return result;

                }

                var numeroBanco = dto.NumeroBanco;

                var comienzo = numeroBanco.IndexOf("1");

                if (comienzo != 01)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Banco invalido";
                    return result;
                }


                if (dto.NumeroCuenta.Length > 20)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Cuenta invalido";
                    return result;
                }

                if (numeroBanco != "0134" && numeroBanco != "0140")
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Banco invalido";
                    return result;
                }

         
                if (dto.FechaTransaccion == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "fecha transaccion invalida";
                    return result;
                }

                if (dto.NumeroTransaccion.Length > 20)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Transaccion invalido";
                    return result;
                }

                if(dto.TipoTransaccionId <= 0) 
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Transaccion Id invalido";
                    return result;

                }

                var tipoTransaccionId = await _cntDescriptivaRepository.GetByIdAndTitulo(6, dto.TipoTransaccionId);
                if(tipoTransaccionId == false) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Transaccion Id invalido";
                    return result;

                }

                if(dto.TipoTransaccion.Length > 10) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Transaccion invalido";
                    return result;

                }

                if (dto.DescripcionTransaccion.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Transaccion invalido";
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

                
                if (dto.CodigoDetalleEdoCta <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo detalle estado Cuenta invalido";
                    return result;
                }


                CNT_BANCO_ARCHIVO entity = new CNT_BANCO_ARCHIVO();
                entity.CODIGO_BANCO_ARCHIVO = await _repository.GetNextKey();
                entity.CODIGO_BANCO_ARCHIVO_CONTROL = dto.CodigoBancoArchivoControl;
                entity.NUMERO_BANCO = dto.NumeroBanco;
                entity.NUMERO_CUENTA = dto.NumeroCuenta;
                entity.FECHA_TRANSACCION = dto.FechaTransaccion;
                entity.NUMERO_TRANSACCION = dto.NumeroTransaccion;
                entity.TIPO_TRANSACCION_ID = dto.TipoTransaccionId;
                entity.TIPO_TRANSACCION = dto.TipoTransaccion;
                entity.DESCRIPCION_TRANSACCION = dto.DescripcionTransaccion;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.CODIGO_DETALLE_EDO_CTA = dto.CodigoDetalleEdoCta;



                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapCntBancoArchivo(created.Data);
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

        public async Task<ResultDto<CntBancoArchivoResponseDto>> Update(CntBancoArchivoUpdateDto dto)
        {
            ResultDto<CntBancoArchivoResponseDto> result = new ResultDto<CntBancoArchivoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoBancoArchivo = await _repository.GetByCodigo(dto.CodigoBancoArchivo);
                if (codigoBancoArchivo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Banco Archivo no existe";
                    return result;
                }

                if (dto.CodigoBancoArchivoControl <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Banco Archivo Control no existe";
                    return result;
                }


                if (dto.NumeroBanco.Length > 4)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Banco invalido";
                    return result;

                }

                var numeroBanco = dto.NumeroBanco;

                var comienzo = numeroBanco.IndexOf("1");

                if (comienzo != 01)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Banco invalido";
                    return result;
                }


                if (dto.NumeroCuenta.Length > 20)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Cuenta invalido";
                    return result;
                }

                if (numeroBanco != "0134" && numeroBanco != "0140")
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Banco invalido";
                    return result;
                }


                if (dto.FechaTransaccion == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "fecha transaccion invalida";
                    return result;
                }

                if (dto.NumeroTransaccion.Length > 20)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Transaccion invalido";
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

                if (dto.TipoTransaccion.Length > 10)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Transaccion invalido";
                    return result;

                }

                if (dto.DescripcionTransaccion.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Transaccion invalido";
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


                if (dto.CodigoDetalleEdoCta <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo detalle estado Cuenta invalido";
                    return result;
                }




                codigoBancoArchivo.CODIGO_BANCO_ARCHIVO = dto.CodigoBancoArchivo;
                codigoBancoArchivo.CODIGO_BANCO_ARCHIVO_CONTROL = dto.CodigoBancoArchivoControl;
                codigoBancoArchivo.NUMERO_BANCO = dto.NumeroBanco;
                codigoBancoArchivo.NUMERO_CUENTA = dto.NumeroCuenta;
                codigoBancoArchivo.FECHA_TRANSACCION = dto.FechaTransaccion;
                codigoBancoArchivo.NUMERO_TRANSACCION = dto.NumeroTransaccion;
                codigoBancoArchivo.TIPO_TRANSACCION_ID = dto.TipoTransaccionId;
                codigoBancoArchivo.TIPO_TRANSACCION = dto.TipoTransaccion;
                codigoBancoArchivo.DESCRIPCION_TRANSACCION = dto.DescripcionTransaccion;
                codigoBancoArchivo.EXTRA1 = dto.Extra1;
                codigoBancoArchivo.EXTRA2 = dto.Extra2;
                codigoBancoArchivo.EXTRA3 = dto.Extra3;
                codigoBancoArchivo.CODIGO_DETALLE_EDO_CTA = dto.CodigoDetalleEdoCta;




                codigoBancoArchivo.CODIGO_EMPRESA = dto.CodigoBancoArchivo;
                codigoBancoArchivo.USUARIO_UPD = dto.CodigoBancoArchivo;
                codigoBancoArchivo.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoBancoArchivo);

                var resultDto = await MapCntBancoArchivo(codigoBancoArchivo);
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

        public async Task<ResultDto<CntBancoArchivoDeleteDto>> Delete(CntBancoArchivoDeleteDto dto)
        {
            ResultDto<CntBancoArchivoDeleteDto> result = new ResultDto<CntBancoArchivoDeleteDto>(null);
            try
            {

                var codigoBancoArchivo = await _repository.GetByCodigo(dto.CodigoBancoArchivo);
                if (codigoBancoArchivo == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Banco Archivo no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoBancoArchivo);

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
