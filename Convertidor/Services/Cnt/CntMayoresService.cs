using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public class CntMayoresService : ICntMayoresService
    {
        private readonly ICntMayoresRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ICntBalancesService _cntBalancesService;

        public CntMayoresService(ICntMayoresRepository repository,
                                ISisUsuarioRepository sisUsuarioRepository,
                                ICntBalancesService cntBalancesService)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _cntBalancesService = cntBalancesService;
        }

        public async Task<CntMayoresResponseDto> MapMayores(CNT_MAYORES dtos)
        {
            CntMayoresResponseDto itemResult = new CntMayoresResponseDto();
            itemResult.CodigoMayor = dtos.CODIGO_MAYOR;
            itemResult.NumeroMayor = dtos.NUMERO_MAYOR;
            itemResult.Denominacion = dtos.DENOMINACION;
            itemResult.Descripcion = dtos.DESCRIPCION;
            itemResult.CodigoBalance = dtos.CODIGO_BALANCE;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
           
            return itemResult;

        }

        public async Task<List<CntMayoresResponseDto>> MapListMayores(List<CNT_MAYORES> dtos)
        {
            List<CntMayoresResponseDto> result = new List<CntMayoresResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapMayores(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntMayoresResponseDto>>> GetAll()
        {

            ResultDto<List<CntMayoresResponseDto>> result = new ResultDto<List<CntMayoresResponseDto>>(null);
            try
            {
                var mayores = await _repository.GetAll();
                var cant = mayores.Count();
                if (mayores != null && mayores.Count() > 0)
                {
                    var listDto = await MapListMayores(mayores);

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

        public async Task<ResultDto<CntMayoresResponseDto>> Create(CntMayoresUpdateDto dto)
        {
            ResultDto<CntMayoresResponseDto> result = new ResultDto<CntMayoresResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.NumeroMayor.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero mayor invalido";
                    return result;
                }

                var numeroMayor = Convert.ToInt32(dto.NumeroMayor);
                if (numeroMayor <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero mayor invalido";
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

                if(dto.CodigoBalance <= 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo balance Invalido";
                    return result;


                }

                var codigoBalance = await _cntBalancesService.GetByCodigo(dto.CodigoBalance);
                if(codigoBalance == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo balance Invalido";
                    return result;

                }

                if (dto.ColumnaBalance.Length > 1 && dto.ColumnaBalance != "D" && dto.ColumnaBalance != "H") 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Columna balance Invalida";
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

               





                CNT_MAYORES entity = new CNT_MAYORES();
                entity.CODIGO_MAYOR = await _repository.GetNextKey();
                entity.NUMERO_MAYOR = dto.NumeroMayor;
                entity.DENOMINACION = dto.Denominacion;
                entity.DESCRIPCION = dto.Descripcion;
                entity.CODIGO_BALANCE = dto.CodigoBalance;
                entity.COLUMNA_BALANCE = dto.ColumnaBalance;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
            




                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapMayores(created.Data);
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

        public async Task<ResultDto<CntMayoresResponseDto>> Update(CntMayoresUpdateDto dto)
        {
            ResultDto<CntMayoresResponseDto> result = new ResultDto<CntMayoresResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoMayor = await _repository.GetByCodigo(dto.CodigoMayor);
                if (codigoMayor == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Mayor Invalido";
                    return result;

                }


                if (dto.NumeroMayor.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero mayor invalido";
                    return result;
                }

                var numeroMayor = Convert.ToInt32(dto.NumeroMayor);
                if (numeroMayor <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero mayor invalido";
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

                if (dto.CodigoBalance <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo balance Invalido";
                    return result;


                }

                var codigoBalance = await _cntBalancesService.GetByCodigo(dto.CodigoBalance);
                if (codigoBalance == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo balance Invalido";
                    return result;

                }

                if (dto.ColumnaBalance.Length > 1 && dto.ColumnaBalance != "D" && dto.ColumnaBalance != "H")
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Columna balance Invalida";
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





                codigoMayor.CODIGO_MAYOR = dto.CodigoMayor;
                codigoMayor.NUMERO_MAYOR = dto.NumeroMayor;
                codigoMayor.DENOMINACION = dto.Denominacion;
                codigoMayor.DESCRIPCION = dto.Descripcion;
                codigoMayor.CODIGO_BALANCE = dto.CodigoBalance;
                codigoMayor.COLUMNA_BALANCE = dto.ColumnaBalance;   
                codigoMayor.EXTRA1 = dto.Extra1;
                codigoMayor.EXTRA2 = dto.Extra2;
                codigoMayor.EXTRA3 = dto.Extra3;
        




                codigoMayor.CODIGO_EMPRESA = conectado.Empresa;
                codigoMayor.USUARIO_UPD = conectado.Usuario;
                codigoMayor.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoMayor);

                var resultDto = await MapMayores(codigoMayor);
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

        public async Task<ResultDto<CntMayoresDeleteDto>> Delete(CntMayoresDeleteDto dto)
        {
            ResultDto<CntMayoresDeleteDto> result = new ResultDto<CntMayoresDeleteDto>(null);
            try
            {

                var codigoMayor = await _repository.GetByCodigo(dto.CodigoMayor);
                if (codigoMayor == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Mayor no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoMayor);

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
