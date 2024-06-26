using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Data.Repository.Cnt;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Cnt;
using Convertidor.Utility;
using NPOI.SS.Formula.Functions;

namespace Convertidor.Services.Cnt
{
    //
    public class CntEstadoCuentasService : ICntEstadoCuentasService
    {
        private readonly ICntEstadoCuentasRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;


        public CntEstadoCuentasService(ICntEstadoCuentasRepository repository,
                                      ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;

        }


       
        public async Task<CntEstadoCuentasResponseDto> MapCntEstadoCuentas(CNT_ESTADO_CUENTAS dtos) 
        {
            CntEstadoCuentasResponseDto itemResult = new CntEstadoCuentasResponseDto();
            itemResult.CodigoEstadoCuenta = dtos.CODIGO_ESTADO_CUENTA;
            itemResult.CodigoCuentaBanco = dtos.CODIGO_CUENTA_BANCO;
            itemResult.NumeroEstadoCuenta = dtos.NUMERO_ESTADO_CUENTA;
            itemResult.FechaDesde = dtos.FECHA_DESDE;
            itemResult.FechaDesdeString = dtos.FECHA_DESDE.ToString("u");
            FechaDto fechaDesdeObj = FechaObj.GetFechaDto(dtos.FECHA_DESDE);
            itemResult.FechaDesdeObj = (FechaDto)fechaDesdeObj;
            itemResult.FechaHasta = dtos.FECHA_HASTA;
            itemResult.FechaHastaString = dtos.FECHA_HASTA.ToString("u");
            FechaDto fechaHastaObj =FechaObj.GetFechaDto(dtos.FECHA_HASTA);
            itemResult.FechaHastaObj = (FechaDto)fechaHastaObj;
            itemResult.SaldoInicial = dtos.SALDO_FINAL;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;

        
            return itemResult;
        }

        public async Task<List<CntEstadoCuentasResponseDto>> MapListCntEstadoCuentas(List<CNT_ESTADO_CUENTAS> dtos)
        {
            List<CntEstadoCuentasResponseDto> result = new List<CntEstadoCuentasResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapCntEstadoCuentas(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntEstadoCuentasResponseDto>>> GetAll()
        {

            ResultDto<List<CntEstadoCuentasResponseDto>> result = new ResultDto<List<CntEstadoCuentasResponseDto>>(null);
            try
            {
                var estadoCuentas = await _repository.GetAll();
                var cant = estadoCuentas.Count();
                if (estadoCuentas != null && estadoCuentas.Count() > 0)
                {
                    var listDto = await MapListCntEstadoCuentas(estadoCuentas);

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


        public async Task<ResultDto<CntEstadoCuentasResponseDto>> Create(CntEstadoCuentasUpdateDto dto)
        {
            ResultDto<CntEstadoCuentasResponseDto> result = new ResultDto<CntEstadoCuentasResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();


                if (dto.CodigoCuentaBanco <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo cuenta Banco no existe";
                    return result;
                }

                if (dto.NumeroEstadoCuenta.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Estado Cuenta invalido";
                    return result;
                }

             
                if (dto.FechaDesde == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha desde invalida";
                    return result;

                }

                
                if (dto.FechaHasta == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Hasta invalida";
                    return result;

                }

                if (dto.SaldoInicial < 0 )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "saldo Inicial invalido";
                    return result;

                }

                if (dto.SaldoFinal < 0)
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

             




                CNT_ESTADO_CUENTAS entity = new CNT_ESTADO_CUENTAS();
                entity.CODIGO_ESTADO_CUENTA = await _repository.GetNextKey();
                entity.CODIGO_CUENTA_BANCO = dto.CodigoCuentaBanco;
                entity.NUMERO_ESTADO_CUENTA = dto.NumeroEstadoCuenta;
                entity.FECHA_DESDE = dto.FechaDesde;
                entity.FECHA_HASTA = dto.FechaHasta;
                entity.SALDO_INICIAL = dto.SaldoInicial;
                entity.SALDO_FINAL = dto.SaldoFinal;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
          




                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapCntEstadoCuentas(created.Data);
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

        public async Task<ResultDto<CntEstadoCuentasResponseDto>> Update(CntEstadoCuentasUpdateDto dto)
        {
            ResultDto<CntEstadoCuentasResponseDto> result = new ResultDto<CntEstadoCuentasResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoEstadoCuenta = await _repository.GetByCodigo(dto.CodigoEstadoCuenta);
                if(codigoEstadoCuenta== null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Estado cuenta no existe";
                    return result;

                }

                if (dto.CodigoCuentaBanco <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo cuenta Banco no existe";
                    return result;
                }

                if (dto.NumeroEstadoCuenta.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Estado Cuenta invalido";
                    return result;
                }


                if (dto.FechaDesde == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha desde invalida";
                    return result;

                }


                if (dto.FechaHasta == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Hasta invalida";
                    return result;

                }

                if (dto.SaldoInicial < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "saldo Inicial invalido";
                    return result;

                }

                if (dto.SaldoFinal < 0)
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



                codigoEstadoCuenta.CODIGO_ESTADO_CUENTA = dto.CodigoEstadoCuenta;
                codigoEstadoCuenta.CODIGO_CUENTA_BANCO = dto.CodigoCuentaBanco;
                codigoEstadoCuenta.NUMERO_ESTADO_CUENTA = dto.NumeroEstadoCuenta;
                codigoEstadoCuenta.FECHA_DESDE = dto.FechaDesde;
                codigoEstadoCuenta.FECHA_HASTA = dto.FechaHasta;
                codigoEstadoCuenta.SALDO_INICIAL = dto.SaldoInicial;
                codigoEstadoCuenta.SALDO_FINAL = dto.SaldoFinal;
                codigoEstadoCuenta.EXTRA1 = dto.Extra1;
                codigoEstadoCuenta.EXTRA2 = dto.Extra2;
                codigoEstadoCuenta.EXTRA3 = dto.Extra3;




                codigoEstadoCuenta.CODIGO_EMPRESA = conectado.Empresa;
                codigoEstadoCuenta.USUARIO_UPD = conectado.Usuario;
                codigoEstadoCuenta.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoEstadoCuenta);

                var resultDto = await MapCntEstadoCuentas(codigoEstadoCuenta);
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

        public async Task<ResultDto<CntEstadoCuentasDeleteDto>> Delete(CntEstadoCuentasDeleteDto dto)
        {
            ResultDto<CntEstadoCuentasDeleteDto> result = new ResultDto<CntEstadoCuentasDeleteDto>(null);
            try
            {

                var codigoEstadoCuenta = await _repository.GetByCodigo(dto.CodigoEstadoCuenta);
                if (codigoEstadoCuenta == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo estado Cuenta no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoEstadoCuenta);

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
