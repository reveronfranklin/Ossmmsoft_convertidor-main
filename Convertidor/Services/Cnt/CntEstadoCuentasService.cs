using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
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

    }
}
