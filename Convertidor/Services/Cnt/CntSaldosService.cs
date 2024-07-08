using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;
using NuGet.Protocol.Core.Types;

namespace Convertidor.Services.Cnt
{
    public class CntSaldosService : ICntSaldosService
    {
        private readonly ICntSaldosRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ICntPeriodosService _cntPeriodosService;
        private readonly ICntMayoresService _cntMayoresService;
        private readonly ICntAuxiliaresService _cntAuxiliaresService;

        public CntSaldosService(ICntSaldosRepository repository,
                                ISisUsuarioRepository sisUsuarioRepository,
                                ICntPeriodosService cntPeriodosService,
                                ICntMayoresService cntMayoresService,
                                ICntAuxiliaresService cntAuxiliaresService)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _cntPeriodosService = cntPeriodosService;
            _cntMayoresService = cntMayoresService;
            _cntAuxiliaresService = cntAuxiliaresService;
        }

        public async Task<CntSaldosResponseDto> MapSaldos(CNT_SALDOS dtos)
        {
            CntSaldosResponseDto itemResult = new CntSaldosResponseDto();
            itemResult.CodigoSaldo = dtos.CODIGO_SALDO;
            itemResult.CodigoPeriodo = dtos.CODIGO_PERIODO;
            itemResult.CodigoMayor = dtos.CODIGO_MAYOR;
            itemResult.CodigoAuxiliar = dtos.CODIGO_AUXILIAR;
            itemResult.Debitos = dtos.DEBITOS;
            itemResult.Creditos = dtos.CREDITOS;
            itemResult.Monto = dtos.MONTO;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;


            return itemResult;

        }

        public async Task<List<CntSaldosResponseDto>> MapListSaldos(List<CNT_SALDOS> dtos)
        {
            List<CntSaldosResponseDto> result = new List<CntSaldosResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapSaldos(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntSaldosResponseDto>>> GetAll()
        {

            ResultDto<List<CntSaldosResponseDto>> result = new ResultDto<List<CntSaldosResponseDto>>(null);
            try
            {
                var saldos = await _repository.GetAll();
                var cant = saldos.Count();
                if (saldos != null && saldos.Count() > 0)
                {
                    var listDto = await MapListSaldos(saldos);

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

        public async Task<ResultDto<CntSaldosResponseDto>> Create(CntSaldosUpdateDto dto)
        {
            ResultDto<CntSaldosResponseDto> result = new ResultDto<CntSaldosResponseDto>(null);
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


                if (dto.CodigoMayor <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Mayor invalido";
                    return result;
                }

                var codigoMayor = await _cntMayoresService.GetByCodigo(dto.CodigoMayor);
                if (codigoMayor == null) 
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Mayor invalido";
                    return result;

                }

                if(dto.CodigoAuxiliar <= 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Auxiliar Invalido";
                    return result;

                }

                var codigoAuxiliar = await _cntAuxiliaresService.GetByCodigo(dto.CodigoAuxiliar);
                if (codigoAuxiliar==null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Auxiliar Invalido";
                    return result;
                }

                if(dto.Debitos < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Debitos Invalido";
                    return result;

                }

                if(dto.Creditos < 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Creditos Invalido";
                    return result;


                }

                if(dto.Monto <= 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Invalido";
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






                CNT_SALDOS entity = new CNT_SALDOS();
                entity.CODIGO_SALDO = await _repository.GetNextKey();
                entity.CODIGO_PERIODO = dto.CodigoPeriodo;
                entity.CODIGO_MAYOR = dto.CodigoMayor;
                entity.CODIGO_AUXILIAR = dto.CodigoAuxiliar;
                entity.DEBITOS = dto.Debitos;
                entity.CREDITOS = dto.Creditos;
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
                    var resultDto = await MapSaldos(created.Data);
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

        public async Task<ResultDto<CntSaldosResponseDto>> Update(CntSaldosUpdateDto dto)
        {
            ResultDto<CntSaldosResponseDto> result = new ResultDto<CntSaldosResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoSaldo = await _repository.GetByCodigo(dto.CodigoSaldo);
                if (codigoSaldo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Saldo no Existe";
                    return result;

                }


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


                if (dto.CodigoMayor <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Mayor invalido";
                    return result;
                }

                var codigoMayor = await _cntMayoresService.GetByCodigo(dto.CodigoMayor);
                if (codigoMayor == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Mayor invalido";
                    return result;

                }

                if (dto.CodigoAuxiliar <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Auxiliar Invalido";
                    return result;

                }

                var codigoAuxiliar = await _cntAuxiliaresService.GetByCodigo(dto.CodigoAuxiliar);
                if (codigoAuxiliar == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Auxiliar Invalido";
                    return result;
                }

                if (dto.Debitos < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Debitos Invalido";
                    return result;

                }

                if (dto.Creditos < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Creditos Invalido";
                    return result;


                }

                if (dto.Monto <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Invalido";
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





                codigoSaldo.CODIGO_SALDO = dto.CodigoSaldo;
                codigoSaldo.CODIGO_PERIODO = dto.CodigoPeriodo;
                codigoSaldo.CODIGO_MAYOR = dto.CodigoMayor;
                codigoSaldo.CODIGO_AUXILIAR = dto.CodigoAuxiliar;
                codigoSaldo.DEBITOS = dto.Debitos;
                codigoSaldo.CREDITOS = dto.Creditos;
                codigoSaldo.MONTO = dto.Monto;
                codigoSaldo.EXTRA1 = dto.Extra1;
                codigoSaldo.EXTRA2 = dto.Extra2;
                codigoSaldo.EXTRA3 = dto.Extra3;




                codigoSaldo.CODIGO_EMPRESA = conectado.Empresa;
                codigoSaldo.USUARIO_UPD = conectado.Usuario;
                codigoSaldo.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoSaldo);

                var resultDto = await MapSaldos(codigoSaldo);
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

        public async Task<ResultDto<CntSaldosDeleteDto>> Delete(CntSaldosDeleteDto dto)
        {
            ResultDto<CntSaldosDeleteDto> result = new ResultDto<CntSaldosDeleteDto>(null);
            try
            {

                var codigoSaldo = await _repository.GetByCodigo(dto.CodigoSaldo);
                if (codigoSaldo == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Saldo no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoSaldo);

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


        public async Task<ResultDto<CntSaldosResponseDto>> GetByCodigo(int codigoSaldo)
        {
            ResultDto<CntSaldosResponseDto> result = new ResultDto<CntSaldosResponseDto>(null);
            try
            {

                var saldos = await _repository.GetByCodigo(codigoSaldo);
                if (saldos == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Saldo no existe";
                    return result;
                }

                var resultDto = await MapSaldos(saldos);
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
