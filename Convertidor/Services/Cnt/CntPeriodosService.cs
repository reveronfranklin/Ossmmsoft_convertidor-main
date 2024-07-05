using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Data.Repository.Cnt;
using Convertidor.Dtos.Cnt;
using Convertidor.Utility;

namespace Convertidor.Services.Cnt
{
    public class CntPeriodosService : ICntPeriodosService
    {
        private readonly ICntPeriodosRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public CntPeriodosService(ICntPeriodosRepository repository,
                                  ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<CntPeriodosResponseDto> MapPeriodos(CNT_PERIODOS dtos)
        {
            CntPeriodosResponseDto itemResult = new CntPeriodosResponseDto();
            itemResult.CodigoPeriodo = dtos.CODIGO_PERIODO;
            itemResult.NombrePeriodo = dtos.NOMBRE_PERIODO;
            itemResult.FechaDesde = dtos.FECHA_DESDE;
            itemResult.FechaDesdeString = dtos.FECHA_DESDE.ToString("u");
            FechaDto fechaDesdeObj = FechaObj.GetFechaDto(dtos.FECHA_DESDE);
            itemResult.FechaDesdeObj = (FechaDto)fechaDesdeObj;
            itemResult.FechaHasta = dtos.FECHA_HASTA;
            itemResult.FechaHastaString = dtos.FECHA_HASTA.ToString("u");
            FechaDto FechaHastaObj = FechaObj.GetFechaDto(dtos.FECHA_HASTA);
            itemResult.FechaHastaObj = (FechaDto)FechaHastaObj;
            itemResult.AnoPeriodo = dtos.ANO_PERIODO;
            itemResult.NumeroPeriodo = dtos.NUMERO_PERIODO;
            itemResult.UsuarioPreCierre = dtos.USUARIO_PRECIERRE;
            itemResult.FechaPreCierre = dtos.FECHA_PRECIERRE;
            itemResult.FechaPreCierreString = dtos.FECHA_PRECIERRE.ToString("u");
            FechaDto fechaPreCierreObj = FechaObj.GetFechaDto(dtos.FECHA_PRECIERRE);
            itemResult.FechaPreCierreObj = (FechaDto)fechaPreCierreObj;
            itemResult.UsuarioCierre = dtos.USUARIO_CIERRE;
            itemResult.FechaCierre = dtos.FECHA_CIERRE;
            itemResult.FechaCierreString = dtos.FECHA_CIERRE.ToString("u");
            FechaDto FechaCierreObj = FechaObj.GetFechaDto(dtos.FECHA_CIERRE);
            itemResult.FechaCierreObj = (FechaDto)FechaCierreObj;
            itemResult.UsuarioPreCierreConc = dtos.USUARIO_PRECIERRE_CONC;
            itemResult.FechaPreCierreConc = dtos.FECHA_PRECIERRE_CONC;
            itemResult.FechaPreCierreConcString = dtos.FECHA_PRECIERRE_CONC.ToString("u");
            FechaDto FechaPreCierreConcObj = FechaObj.GetFechaDto(dtos.FECHA_PRECIERRE_CONC);
            itemResult.FechaPreCierreConcObj = (FechaDto)FechaPreCierreConcObj;
            itemResult.UsuarioCierreConc = dtos.USUARIO_CIERRE_CONC;
            itemResult.FechaCierreConc = dtos.FECHA_CIERRE_CONC;
            itemResult.FechaCierreConcString = dtos.FECHA_CIERRE_CONC.ToString("u");
            FechaDto FechaCierreConcObj = FechaObj.GetFechaDto(dtos.FECHA_CIERRE_CONC);
            itemResult.FechaCierreConcObj = (FechaDto)FechaCierreConcObj;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
   

            return itemResult;

        }

        public async Task<List<CntPeriodosResponseDto>> MapListPeriodos(List<CNT_PERIODOS> dtos)
        {
            List<CntPeriodosResponseDto> result = new List<CntPeriodosResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapPeriodos(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntPeriodosResponseDto>>> GetAll()
        {

            ResultDto<List<CntPeriodosResponseDto>> result = new ResultDto<List<CntPeriodosResponseDto>>(null);
            try
            {
                var periodos = await _repository.GetAll();
                var cant = periodos.Count();
                if (periodos != null && periodos.Count() > 0)
                {
                    var listDto = await MapListPeriodos(periodos);

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

        public async Task<ResultDto<CntPeriodosResponseDto>> Create(CntPeriodosUpdateDto dto)
        {
            ResultDto<CntPeriodosResponseDto> result = new ResultDto<CntPeriodosResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.NombrePeriodo is not null && dto.NombrePeriodo.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nombre Periodo invalido";
                    return result;
                }

               
                if (dto.FechaDesde == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Desde invalido";
                    return result;

                }

                if (dto.FechaHasta == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Hasta Invalido";
                    return result;
                }
                
                if (dto.AnoPeriodo <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "año Periodo Invalido";
                    return result;

                }

                if (dto.NumeroPeriodo <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Periodo Invalido";
                    return result;
                }

                if (dto.UsuarioPreCierre <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Usuario PreCierre Invalido";
                    return result;

                }

                if(dto.FechaPreCierre == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha PreCierre Invalido";
                    return result;

                }

                if (dto.UsuarioCierre <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Usuario Cierre Invalido";
                    return result;

                }

                if (dto.FechaCierre == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Cierre Invalido";
                    return result;

                }

                if(dto.UsuarioPreCierreConc <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Usuario Pre Cierre Conc Invalido";
                    return result;
                }

                if (dto.FechaPreCierreConc == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha PreCierreConc Invalido";
                    return result;

                }

                if (dto.UsuarioCierreConc <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Usuario Cierre Conc Invalido";
                    return result;

                }

                if (dto.FechaCierreConc == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Cierre Conc Invalido";
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




                CNT_PERIODOS entity = new CNT_PERIODOS();
                entity.CODIGO_PERIODO = await _repository.GetNextKey();
                entity.NOMBRE_PERIODO = dto.NombrePeriodo;
                entity.FECHA_DESDE = dto.FechaDesde;
                entity.FECHA_HASTA = dto.FechaHasta;
                entity.ANO_PERIODO = dto.AnoPeriodo;
                entity.NUMERO_PERIODO = dto.NumeroPeriodo;
                entity.USUARIO_PRECIERRE = dto.UsuarioPreCierre;
                entity.FECHA_PRECIERRE = dto.FechaPreCierre;
                entity.USUARIO_CIERRE = dto.UsuarioCierre;
                entity.FECHA_CIERRE = dto.FechaCierre;
                entity.USUARIO_PRECIERRE_CONC = dto.UsuarioPreCierreConc;
                entity.FECHA_PRECIERRE_CONC = dto.FechaPreCierreConc;
                entity.USUARIO_CIERRE_CONC = dto.UsuarioCierreConc;
                entity.FECHA_CIERRE_CONC = dto.FechaCierreConc;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;

                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;
                entity.CODIGO_EMPRESA = conectado.Empresa;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapPeriodos(created.Data);
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

        public async Task<ResultDto<CntPeriodosResponseDto>> Update(CntPeriodosUpdateDto dto)
        {
            ResultDto<CntPeriodosResponseDto> result = new ResultDto<CntPeriodosResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoPeriodo = await _repository.GetByCodigo(dto.CodigoPeriodo);
                if (codigoPeriodo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Periodo Invalido";
                    return result;

                }


                if (dto.NombrePeriodo is not null && dto.NombrePeriodo.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nombre Periodo invalido";
                    return result;
                }


                if (dto.FechaDesde == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Desde invalido";
                    return result;

                }

                if (dto.FechaHasta == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Hasta Invalido";
                    return result;
                }

                if (dto.AnoPeriodo <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "año Periodo Invalido";
                    return result;

                }

                if (dto.NumeroPeriodo <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Periodo Invalido";
                    return result;
                }

                if (dto.UsuarioPreCierre <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Usuario PreCierre Invalido";
                    return result;

                }

                if (dto.FechaPreCierre == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha PreCierre Invalido";
                    return result;

                }

                if (dto.UsuarioCierre <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Usuario Cierre Invalido";
                    return result;

                }

                if (dto.FechaCierre == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Cierre Invalido";
                    return result;

                }

                if (dto.UsuarioPreCierreConc <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Usuario Pre Cierre Conc Invalido";
                    return result;
                }

                if (dto.FechaPreCierreConc == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha PreCierreConc Invalido";
                    return result;

                }

                if (dto.UsuarioCierreConc <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Usuario Cierre Conc Invalido";
                    return result;

                }

                if (dto.FechaCierreConc == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Cierre Conc Invalido";
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





                codigoPeriodo.CODIGO_PERIODO = dto.CodigoPeriodo;
                codigoPeriodo.NOMBRE_PERIODO = dto.NombrePeriodo;
                codigoPeriodo.FECHA_DESDE = dto.FechaDesde;
                codigoPeriodo.FECHA_HASTA = dto.FechaHasta;
                codigoPeriodo.ANO_PERIODO = dto.AnoPeriodo;
                codigoPeriodo.NUMERO_PERIODO = dto.NumeroPeriodo;
                codigoPeriodo.USUARIO_PRECIERRE = dto.UsuarioPreCierre;
                codigoPeriodo.FECHA_PRECIERRE = dto.FechaPreCierre;
                codigoPeriodo.USUARIO_CIERRE = dto.UsuarioCierre;
                codigoPeriodo.FECHA_CIERRE = dto.FechaCierre;
                codigoPeriodo.USUARIO_PRECIERRE_CONC = dto.UsuarioPreCierreConc;
                codigoPeriodo.FECHA_PRECIERRE_CONC = dto.FechaPreCierreConc;
                codigoPeriodo.USUARIO_CIERRE_CONC = dto.UsuarioCierreConc;
                codigoPeriodo.FECHA_CIERRE_CONC = dto.FechaCierreConc;
                codigoPeriodo.EXTRA1 = dto.Extra1;
                codigoPeriodo.EXTRA2 = dto.Extra2;
                codigoPeriodo.EXTRA3 = dto.Extra3;




                codigoPeriodo.CODIGO_EMPRESA = conectado.Empresa;
                codigoPeriodo.USUARIO_UPD = conectado.Usuario;
                codigoPeriodo.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoPeriodo);

                var resultDto = await MapPeriodos(codigoPeriodo);
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
