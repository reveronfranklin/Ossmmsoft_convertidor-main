using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Data.Repository.Cnt;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Cnt;
using NPOI.SS.Formula.Functions;

namespace Convertidor.Services.Cnt
{
    public class CntBancoArchivoControlService : ICntBancoArchivoControlService
    {
        private readonly ICntBancoArchivoControlRepository _repository;
        private readonly ICntBancoArchivoRepository _cntBancoArchivoRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;


        public CntBancoArchivoControlService(ICntBancoArchivoControlRepository repository,
                                             ICntBancoArchivoRepository cntBancoArchivoRepository,
                                             ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _cntBancoArchivoRepository = cntBancoArchivoRepository;
            _sisUsuarioRepository = sisUsuarioRepository;

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
        public async Task<CntBancoArchivoControlResponseDto> MapCntBancoArchivoControl(CNT_BANCO_ARCHIVO_CONTROL dtos) 
        {
            CntBancoArchivoControlResponseDto itemResult = new CntBancoArchivoControlResponseDto();

            itemResult.CodigoBancoArchivoControl = dtos.CODIGO_BANCO_ARCHIVO_CONTROL;
            itemResult.CodigoBanco = dtos.CODIGO_BANCO;
            itemResult.CodigoCuentaBanco = dtos.CODIGO_CUENTA_BANCO;
            itemResult.NombreArchivo = dtos.NOMBRE_ARCHIVO;
            itemResult.FechaDesde = dtos.FECHA_DESDE;
            itemResult.FechaDesdeString = dtos.FECHA_DESDE.ToString("u");
            FechaDto fechaDesdeObj = GetFechaDto(dtos.FECHA_DESDE);
            itemResult.FechaDesdeObj = (FechaDto)fechaDesdeObj;
            itemResult.FechaHasta = dtos.FECHA_HASTA;
            itemResult.FechaHastaString = dtos.FECHA_HASTA.ToString("u");
            FechaDto fechaHastaObj = GetFechaDto(dtos.FECHA_HASTA);
            itemResult.FechaHastaObj =(FechaDto) fechaHastaObj;
            itemResult.SaldoInicial = dtos.SALDO_INICIAL;
            itemResult.SaldoFinal = dtos.SALDO_FINAL;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.CodigoEstadoCuenta = dtos.CODIGO_ESTADO_CUENTA;
        
            return itemResult;
        }

        public async Task<List<CntBancoArchivoControlResponseDto>> MapListCntBancoArchivoControl(List<CNT_BANCO_ARCHIVO_CONTROL> dtos)
        {
            List<CntBancoArchivoControlResponseDto> result = new List<CntBancoArchivoControlResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapCntBancoArchivoControl(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntBancoArchivoControlResponseDto>>> GetAll()
        {

            ResultDto<List<CntBancoArchivoControlResponseDto>> result = new ResultDto<List<CntBancoArchivoControlResponseDto>>(null);
            try
            {
                var bancoArchivoControl = await _repository.GetAll();
                var cant = bancoArchivoControl.Count();
                if (bancoArchivoControl != null && bancoArchivoControl.Count() > 0)
                {
                    var listDto = await MapListCntBancoArchivoControl(bancoArchivoControl);

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


        public async Task<ResultDto<CntBancoArchivoControlResponseDto>> Create(CntBancoArchivoControlUpdateDto dto)
        {
            ResultDto<CntBancoArchivoControlResponseDto> result = new ResultDto<CntBancoArchivoControlResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.CodigoBanco <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Banco invalido";
                    return result;

                }

                if (dto.CodigoCuentaBanco <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Cuenta Banco invalido";
                    return result;
                }

                if (dto.NombreArchivo.Length > 255)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nombre Archivo invalido";
                    return result;
                }


                if (dto.FechaDesde == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "fecha desde invalida";
                    return result;
                }

                if (dto.FechaHasta == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "fecha hasta invalida";
                    return result;
                }
                if (dto.SaldoInicial < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Saldo Inicial invalido";
                    return result;
                }

                if (dto.SaldoFinal <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Saldo Final invalido";
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


                if (dto.CodigoEstadoCuenta <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo estado Cuenta invalido";
                    return result;
                }


                CNT_BANCO_ARCHIVO_CONTROL entity = new CNT_BANCO_ARCHIVO_CONTROL();
                entity.CODIGO_BANCO_ARCHIVO_CONTROL = await _repository.GetNextKey();
                entity.CODIGO_BANCO = dto.CodigoBanco;
                entity.CODIGO_CUENTA_BANCO = dto.CodigoCuentaBanco;
                entity.NOMBRE_ARCHIVO = dto.NombreArchivo;
                entity.FECHA_DESDE = dto.FechaDesde;
                entity.FECHA_HASTA = dto.FechaHasta;
                entity.SALDO_INICIAL = dto.SaldoInicial;
                entity.SALDO_FINAL = dto.SaldoFinal;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.CODIGO_ESTADO_CUENTA = dto.CodigoEstadoCuenta;



                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapCntBancoArchivoControl(created.Data);
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
