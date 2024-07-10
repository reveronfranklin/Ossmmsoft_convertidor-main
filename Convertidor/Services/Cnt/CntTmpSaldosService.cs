using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public class CntTmpSaldosService : ICntTmpSaldosService
    {
        private readonly ICntTmpSaldosRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ICntMayoresService _cntMayoresService;
        private readonly ICntAuxiliaresService _cntAuxiliaresService;
        private readonly ICntPeriodosService _cntPeriodosService;

        public CntTmpSaldosService(ICntTmpSaldosRepository repository,
                                    ISisUsuarioRepository sisUsuarioRepository,
                                    ICntPeriodosService cntPeriodosService,
                                    ICntMayoresService cntMayoresService,
                                    ICntAuxiliaresService cntAuxiliaresService)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _cntMayoresService = cntMayoresService;
            _cntAuxiliaresService = cntAuxiliaresService;
            _cntPeriodosService = cntPeriodosService;
        }

        public async Task<CntTmpSaldosResponseDto> MapTmpSaldos(CNT_TMP_SALDOS dtos)
        {
            CntTmpSaldosResponseDto itemResult = new CntTmpSaldosResponseDto();
            itemResult.CodigoTmpSaldo = dtos.CODIGO_TMP_SALDO;
            itemResult.CodigoPeriodo = dtos.CODIGO_PERIODO;
            itemResult.CodigoMayor = dtos.CODIGO_MAYOR;
            itemResult.CodigoAuxiliar = dtos.CODIGO_AUXILIAR;
            itemResult.SaldoInicial = dtos.SALDO_INICIAL;
            itemResult.Debitos = dtos.DEBITOS;
            itemResult.Creditos = dtos.CREDITOS;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;


            return itemResult;

        }

        public async Task<List<CntTmpSaldosResponseDto>> MapListTmpSaldos(List<CNT_TMP_SALDOS> dtos)
        {
            List<CntTmpSaldosResponseDto> result = new List<CntTmpSaldosResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapTmpSaldos(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntTmpSaldosResponseDto>>> GetAll()
        {

            ResultDto<List<CntTmpSaldosResponseDto>> result = new ResultDto<List<CntTmpSaldosResponseDto>>(null);
            try
            {
                var tmpSaldos = await _repository.GetAll();
                var cant = tmpSaldos.Count();
                if (tmpSaldos != null && tmpSaldos.Count() > 0)
                {
                    var listDto = await MapListTmpSaldos(tmpSaldos);

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

        public async Task<ResultDto<CntTmpSaldosResponseDto>> Create(CntTmpSaldosUpdateDto dto)
        {
            ResultDto<CntTmpSaldosResponseDto> result = new ResultDto<CntTmpSaldosResponseDto>(null);
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

                if(dto.SaldoInicial <= 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Saldo Inicial Invalido";
                    return result;

                }

                if (dto.Debitos <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Debitos Invalido";
                    return result;

                }

                if (dto.Creditos <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Creditos Invalido";
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






                CNT_TMP_SALDOS entity = new CNT_TMP_SALDOS();
                entity.CODIGO_TMP_SALDO = await _repository.GetNextKey();
                entity.CODIGO_PERIODO = dto.CodigoPeriodo;
                entity.CODIGO_MAYOR = dto.CodigoMayor;
                entity.CODIGO_AUXILIAR = dto.CodigoAuxiliar;
                entity.SALDO_INICIAL = dto.SaldoInicial;
                entity.DEBITOS = dto.Debitos;
                entity.CREDITOS = dto.Creditos;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;


                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapTmpSaldos(created.Data);
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

        public async Task<ResultDto<CntTmpSaldosResponseDto>> Update(CntTmpSaldosUpdateDto dto)
        {
            ResultDto<CntTmpSaldosResponseDto> result = new ResultDto<CntTmpSaldosResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoTmpSaldo = await _repository.GetByCodigo(dto.CodigoTmpSaldo);
                if (codigoTmpSaldo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Tmp Saldo no Existe";
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

                if (dto.SaldoInicial <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Saldo Inicial Invalido";
                    return result;

                }

                if (dto.Debitos <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Debitos Invalido";
                    return result;

                }

                if (dto.Creditos <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Creditos Invalido";
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





                codigoTmpSaldo.CODIGO_TMP_SALDO = dto.CodigoTmpSaldo;
                codigoTmpSaldo.CODIGO_PERIODO = dto.CodigoPeriodo;
                codigoTmpSaldo.CODIGO_MAYOR = dto.CodigoMayor;
                codigoTmpSaldo.CODIGO_AUXILIAR = dto.CodigoAuxiliar;
                codigoTmpSaldo.SALDO_INICIAL = dto.SaldoInicial;
                codigoTmpSaldo.DEBITOS = dto.Debitos;
                codigoTmpSaldo.CREDITOS = dto.Creditos;
                codigoTmpSaldo.EXTRA1 = dto.Extra1;
                codigoTmpSaldo.EXTRA2 = dto.Extra2;
                codigoTmpSaldo.EXTRA3 = dto.Extra3;




                codigoTmpSaldo.CODIGO_EMPRESA = conectado.Empresa;
                codigoTmpSaldo.USUARIO_UPD = conectado.Usuario;
                codigoTmpSaldo.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoTmpSaldo);

                var resultDto = await MapTmpSaldos(codigoTmpSaldo);
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

        public async Task<ResultDto<CntTmpSaldosDeleteDto>> Delete(CntTmpSaldosDeleteDto dto)
        {
            ResultDto<CntTmpSaldosDeleteDto> result = new ResultDto<CntTmpSaldosDeleteDto>(null);
            try
            {

                var codigoTmpSaldo = await _repository.GetByCodigo(dto.CodigoTmpSaldo);
                if (codigoTmpSaldo == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Tmp Saldo no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoTmpSaldo);

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

        public async Task<ResultDto<CntTmpSaldosResponseDto>> GetByCodigo(int codigoTmpSaldo)
        {
            ResultDto<CntTmpSaldosResponseDto> result = new ResultDto<CntTmpSaldosResponseDto>(null);
            try
            {

                var tmpSaldo = await _repository.GetByCodigo(codigoTmpSaldo);
                if (tmpSaldo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Tmp Saldo no existe";
                    return result;
                }

                var resultDto = await MapTmpSaldos(tmpSaldo);
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
