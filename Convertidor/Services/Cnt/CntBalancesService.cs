using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public class CntBalancesService : ICntBalancesService
    {
        private readonly ICntBalancesRepository _repository;
        private readonly ICntRubrosService _cntRubrosService;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public CntBalancesService(ICntBalancesRepository repository
                             ,ICntRubrosService cntRubrosService,
                              ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _cntRubrosService = cntRubrosService;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<CntBalancesResponseDto> MapBalances(CNT_BALANCES dtos)
        {
            CntBalancesResponseDto itemResult = new CntBalancesResponseDto();
            itemResult.CodigoBalance = dtos.CODIGO_BALANCE;
            itemResult.NumeroBalance = dtos.NUMERO_BALANCE;
            itemResult.Denominacion = dtos.DENOMINACION;
            itemResult.Descripcion = dtos.DESCRIPCION;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.CodigoRubro = dtos.CODIGO_RUBRO;

            return itemResult;

        }

        public async Task<List<CntBalancesResponseDto>> MapListBalances(List<CNT_BALANCES> dtos)
        {
            List<CntBalancesResponseDto> result = new List<CntBalancesResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapBalances(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntBalancesResponseDto>>> GetAll()
        {

            ResultDto<List<CntBalancesResponseDto>> result = new ResultDto<List<CntBalancesResponseDto>>(null);
            try
            {
                var balances = await _repository.GetAll();
                var cant = balances.Count();
                if (balances != null && balances.Count() > 0)
                {
                    var listDto = await MapListBalances(balances);

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

        public async Task<ResultDto<CntBalancesResponseDto>> Create(CntBalancesUpdateDto dto)
        {
            ResultDto<CntBalancesResponseDto> result = new ResultDto<CntBalancesResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.NumeroBalance.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Balance invalido";
                    return result;
                }

                var numeroBalance = Convert.ToInt32(dto.NumeroBalance);
                if (numeroBalance <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Balance invalido";
                    return result;

                }


                if (dto.Denominacion.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Invalida";
                    return result;
                }

                if(dto.Descripcion is not null && dto.Descripcion.Length > 1000) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
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

                if(dto.CodigoRubro <= 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Rubro Invalido";
                    return result;

                }

                var rubro = await _cntRubrosService.GetByCodigo(dto.CodigoRubro);
                if(rubro == null) 
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Rubro Invalido";
                    return result;

                }





                CNT_BALANCES entity = new CNT_BALANCES();
                entity.CODIGO_BALANCE = await _repository.GetNextKey();
                entity.NUMERO_BALANCE = dto.NumeroBalance;
                entity.DENOMINACION = dto.Denominacion;
                entity.DESCRIPCION = dto.Descripcion;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.CODIGO_RUBRO = dto.CodigoRubro;  





                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapBalances(created.Data);
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

        public async Task<ResultDto<CntBalancesResponseDto>> Update(CntBalancesUpdateDto dto)
        {
            ResultDto<CntBalancesResponseDto> result = new ResultDto<CntBalancesResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoBalance = await _repository.GetByCodigo(dto.CodigoBalance);
                if (codigoBalance == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Balance Invalido";
                    return result;

                }


                if (dto.NumeroBalance.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Balance invalido";
                    return result;
                }

                var numeroBalance = Convert.ToInt32(dto.NumeroBalance);
                if (numeroBalance <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Balance invalido";
                    return result;

                }


                if (dto.Denominacion.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Invalida";
                    return result;
                }

                if (dto.Descripcion is not null && dto.Descripcion.Length > 1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
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

                if (dto.CodigoRubro <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Rubro Invalido";
                    return result;

                }

                var rubro = await _cntRubrosService.GetByCodigo(dto.CodigoRubro);
                if (rubro == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Rubro Invalido";
                    return result;

                }





                codigoBalance.CODIGO_BALANCE = dto.CodigoBalance;
                codigoBalance.NUMERO_BALANCE = dto.NumeroBalance;
                codigoBalance.DENOMINACION = dto.Denominacion;
                codigoBalance.DESCRIPCION = dto.Descripcion;
                codigoBalance.EXTRA1 = dto.Extra1;
                codigoBalance.EXTRA2 = dto.Extra2;
                codigoBalance.EXTRA3 = dto.Extra3;
                codigoBalance.CODIGO_RUBRO = dto.CodigoRubro; ;




                codigoBalance.CODIGO_EMPRESA = conectado.Empresa;
                codigoBalance.USUARIO_UPD = conectado.Usuario;
                codigoBalance.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoBalance);

                var resultDto = await MapBalances(codigoBalance);
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
