using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Data.Repository.Catastro;
using Convertidor.Dtos.Catastro;
using Convertidor.Utility;

namespace Convertidor.Services.Catastro
{
    public class CatAvaluoConstruccionService : ICatAvaluoConstruccionService
    {
        private readonly ICatAvaluoConstruccionRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public CatAvaluoConstruccionService(ICatAvaluoConstruccionRepository repository,
                                            ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<CatAvaluoConstruccionResponseDto> MapCatAvaluoConstruccion(CAT_AVALUO_CONSTRUCCION entity)
        {
            CatAvaluoConstruccionResponseDto dto = new CatAvaluoConstruccionResponseDto();

            dto.CodigoAvaluoConstruccion = entity.CODIGO_AVALUO_CONSTRUCCION;
            dto.CodigoFicha = entity.CODIGO_FICHA;
            dto.AnoAvaluo = entity.ANO_AVALUO;
            dto.AnoAvaluoString = entity.ANO_AVALUO.ToString("u");
            FechaDto anoAvaluoObj = FechaObj.GetFechaDto(entity.ANO_AVALUO);
            dto.AnoAvaluoObj = (FechaDto)anoAvaluoObj;
            dto.PlantaId = entity.PLANTA_ID;
            dto.UnidadMedidaId = entity.UNIDAD_MEDIDA_ID;
            dto.ValorMedida = entity.VALOR_MEDIDA;
            dto.FactorDepreciacion = entity.FACTOR_DEPRECIACION;
            dto.ValorModificado = entity.VALOR_MODIFICADO;
            dto.AreaTotal = entity.AREA_TOTAL;
            dto.MontoAvaluo = entity.MONTO_AVALUO;
            dto.Observaciones = entity.OBSERVACIONES;
            dto.ValorReposicion = entity.VALOR_REPOSICION;
            dto.AreaConstruccion = entity.AREA_CONSTRUCCION;
            dto.Extra1 = entity.EXTRA1;
            dto.Extra2 = entity.EXTRA2;
            dto.Extra3 = entity.EXTRA3;
            dto.Extra4 = entity.EXTRA4;
            dto.Extra5 = entity.EXTRA5;
            dto.Extra6 = entity.EXTRA6;
            dto.Extra7 = entity.EXTRA7;
            dto.Extra8 = entity.EXTRA8;
            dto.Extra9 = entity.EXTRA9;
            dto.Extra10 = entity.EXTRA10;
            dto.Extra11 = entity.EXTRA11;
            dto.Extra12 = entity.EXTRA12;
            dto.Extra13 = entity.EXTRA13;
            dto.Extra14 = entity.EXTRA14;
            dto.Extra15 = entity.EXTRA15;
            dto.Tipologia = entity.TIPOLOGIA;
            dto.FrenteParcela = entity.FRENTE_PARCELA;
            dto.MontoComplemento = entity.MONTO_COMPLEMENTO;
            dto.MontoComplementoUsuario = entity.MONTO_COMPLEMENTO_USUARIO;
            dto.MontoTotalAvaluo = entity.MONTO_AVALUO;


            return dto;

        }

        public async Task<List<CatAvaluoConstruccionResponseDto>> MapListCatAvaluoConstruccion(List<CAT_AVALUO_CONSTRUCCION> dtos)
        {
            List<CatAvaluoConstruccionResponseDto> result = new List<CatAvaluoConstruccionResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapCatAvaluoConstruccion(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CatAvaluoConstruccionResponseDto>>> GetAll()
        {

            ResultDto<List<CatAvaluoConstruccionResponseDto>> result = new ResultDto<List<CatAvaluoConstruccionResponseDto>>(null);
            try
            {
                var avaluoContruccion = await _repository.GetAll();
                var cant = avaluoContruccion.Count();
                if (avaluoContruccion != null && avaluoContruccion.Count() > 0)
                {
                    var listDto = await MapListCatAvaluoConstruccion(avaluoContruccion);

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

        public async Task<ResultDto<CatAvaluoConstruccionResponseDto>> Create(CatAvaluoConstruccionUpdateDto dto)
        {

            ResultDto<CatAvaluoConstruccionResponseDto> result = new ResultDto<CatAvaluoConstruccionResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

        
                if (dto.CodigoFicha <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Ficha Invalido";
                    return result;

                }

                if (dto.AnoAvaluo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ano avaluo Invalido ";
                    return result;
                }

                if (dto.PlantaId <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Planta Id Invalido";
                    return result;

                }

                if (dto.UnidadMedidaId <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Unidad Medida Id Invalido";
                    return result;

                }

                if (dto.ValorMedida <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Valor medida Invalido";
                    return result;

                }

                if (dto.FactorDepreciacion <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Factor Depreciacion Invalida";
                    return result;

                }

                if (dto.ValorModificado <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Valor Modificacion Invalido";
                    return result;

                }

                if (dto.AreaTotal <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area Total Invalida";
                    return result;

                }

                if (dto.MontoAvaluo <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Aplicado Invalido";
                    return result;

                }

                if (dto.Observaciones.Length > 200)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Observaciones Invalida";
                    return result;

                }

                if (dto.ValorReposicion <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Valor Reposicion Invalido";
                    return result;

                }

                if (dto.AreaConstruccion <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area Construccion Invalida";
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

                if (dto.CodigoParcela <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Parcela Invalido";
                    return result;

                }

                if (dto.Extra4 is not null && dto.Extra4.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra4 Invalido";
                    return result;
                }
                if (dto.Extra5 is not null && dto.Extra5.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra5 Invalido";
                    return result;
                }

                if (dto.Extra6 is not null && dto.Extra6.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra6 Invalido";
                    return result;
                }

                if (dto.Extra7 is not null && dto.Extra7.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra7 Invalido";
                    return result;
                }
                if (dto.Extra8 is not null && dto.Extra8.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra8 Invalido";
                    return result;
                }

                if (dto.Extra9 is not null && dto.Extra9.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra9 Invalido";
                    return result;
                }

                if (dto.Extra10 is not null && dto.Extra10.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra10 Invalido";
                    return result;
                }
                if (dto.Extra11 is not null && dto.Extra11.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra11 Invalido";
                    return result;
                }

                if (dto.Extra12 is not null && dto.Extra12.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra12 Invalido";
                    return result;
                }

                if (dto.Extra13 is not null && dto.Extra13.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra13 Invalido";
                    return result;
                }
                if (dto.Extra14 is not null && dto.Extra14.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra14 Invalido";
                    return result;
                }

                if (dto.Extra15 is not null && dto.Extra15.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra15 Invalido";
                    return result;
                }

                if (dto.Tipologia.Length > 20)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipologia Invalido";
                    return result;

                }

                if (dto.FrenteParcela <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Frente Parcela Invalido";
                    return result;

                }

                if (dto.MontoComplemento <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Complemento Invalido";
                    return result;

                }

                if (dto.MontoComplementoUsuario <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Complemento Usuario Invalido";
                    return result;

                }

                if (dto.MontoTotalAvaluo <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Total Avaluo Invalido";
                    return result;

                }


                CAT_AVALUO_CONSTRUCCION entity = new CAT_AVALUO_CONSTRUCCION();
                entity.CODIGO_AVALUO_CONSTRUCCION = await _repository.GetNextKey();
                entity.CODIGO_FICHA = dto.CodigoFicha;
                entity.ANO_AVALUO = dto.AnoAvaluo;
                entity.PLANTA_ID = dto.PlantaId;
                entity.UNIDAD_MEDIDA_ID = dto.UnidadMedidaId;
                entity.VALOR_MEDIDA = dto.ValorMedida;
                entity.AREA_TOTAL = dto.AreaTotal;
                entity.MONTO_AVALUO = dto.MontoAvaluo;
                entity.OBSERVACIONES = dto.Observaciones;
                entity.VALOR_REPOSICION = dto.ValorReposicion;
                entity.AREA_CONSTRUCCION = dto.AreaConstruccion;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.CODIGO_PARCELA = dto.CodigoParcela;
                entity.EXTRA4 = dto.Extra4;
                entity.EXTRA5 = dto.Extra5;
                entity.EXTRA6 = dto.Extra6;
                entity.EXTRA7 = dto.Extra7;
                entity.EXTRA8 = dto.Extra8;
                entity.EXTRA9 = dto.Extra9;
                entity.EXTRA10 = dto.Extra10;
                entity.EXTRA11 = dto.Extra11;
                entity.EXTRA12 = dto.Extra12;
                entity.EXTRA13 = dto.Extra13;
                entity.EXTRA14 = dto.Extra14;
                entity.EXTRA15 = dto.Extra15;
                entity.TIPOLOGIA = dto.Tipologia;
                entity.FRENTE_PARCELA = dto.FrenteParcela;
                entity.MONTO_COMPLEMENTO = dto.MontoComplemento;
                entity.MONTO_COMPLEMENTO_USUARIO = dto.MontoComplementoUsuario;
                entity.MONTO_TOTAL_AVALUO = dto.MontoTotalAvaluo;

                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapCatAvaluoConstruccion(created.Data);
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

        public async Task<ResultDto<CatAvaluoConstruccionResponseDto>> Update(CatAvaluoConstruccionUpdateDto dto)
        {

            ResultDto<CatAvaluoConstruccionResponseDto> result = new ResultDto<CatAvaluoConstruccionResponseDto>(null);
            try
            {


                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoAvaluoConstruccion = await _repository.GetByCodigo(dto.CodigoAvaluoConstruccion);
                if (codigoAvaluoConstruccion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Avaluo Construccion Invalido";
                    return result;

                }


                if (dto.CodigoFicha <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Ficha Invalido";
                    return result;

                }

                if (dto.AnoAvaluo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ano avaluo Invalido ";
                    return result;
                }

                if (dto.PlantaId <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Planta Id Invalido";
                    return result;

                }

                if (dto.UnidadMedidaId <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Unidad Medida Id Invalido";
                    return result;

                }

                if (dto.ValorMedida <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Valor medida Invalido";
                    return result;

                }

                if (dto.FactorDepreciacion <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Factor Depreciacion Invalida";
                    return result;

                }

                if (dto.ValorModificado <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Valor Modificacion Invalido";
                    return result;

                }

                if (dto.AreaTotal <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area Total Invalida";
                    return result;

                }

                if (dto.MontoAvaluo <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Aplicado Invalido";
                    return result;

                }

                if (dto.Observaciones.Length > 200)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Observaciones Invalida";
                    return result;

                }

                if (dto.ValorReposicion <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Valor Reposicion Invalido";
                    return result;

                }

                if (dto.AreaConstruccion <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area Construccion Invalida";
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

                if (dto.CodigoParcela <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Parcela Invalido";
                    return result;

                }

                if (dto.Extra4 is not null && dto.Extra4.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra4 Invalido";
                    return result;
                }
                if (dto.Extra5 is not null && dto.Extra5.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra5 Invalido";
                    return result;
                }

                if (dto.Extra6 is not null && dto.Extra6.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra6 Invalido";
                    return result;
                }

                if (dto.Extra7 is not null && dto.Extra7.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra7 Invalido";
                    return result;
                }
                if (dto.Extra8 is not null && dto.Extra8.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra8 Invalido";
                    return result;
                }

                if (dto.Extra9 is not null && dto.Extra9.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra9 Invalido";
                    return result;
                }

                if (dto.Extra10 is not null && dto.Extra10.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra10 Invalido";
                    return result;
                }
                if (dto.Extra11 is not null && dto.Extra11.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra11 Invalido";
                    return result;
                }

                if (dto.Extra12 is not null && dto.Extra12.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra12 Invalido";
                    return result;
                }

                if (dto.Extra13 is not null && dto.Extra13.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra13 Invalido";
                    return result;
                }
                if (dto.Extra14 is not null && dto.Extra14.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra14 Invalido";
                    return result;
                }

                if (dto.Extra15 is not null && dto.Extra15.Length > 100)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra15 Invalido";
                    return result;
                }

                if (dto.Tipologia.Length > 20)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipologia Invalido";
                    return result;

                }

                if (dto.FrenteParcela <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Frente Parcela Invalido";
                    return result;

                }

                if (dto.MontoComplemento <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Complemento Invalido";
                    return result;

                }

                if (dto.MontoComplementoUsuario <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Complemento Usuario Invalido";
                    return result;

                }

                if (dto.MontoTotalAvaluo <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Total Avaluo Invalido";
                    return result;

                }


                codigoAvaluoConstruccion.CODIGO_AVALUO_CONSTRUCCION = dto.CodigoAvaluoConstruccion;
                codigoAvaluoConstruccion.CODIGO_FICHA = dto.CodigoFicha;
                codigoAvaluoConstruccion.ANO_AVALUO = dto.AnoAvaluo;
                codigoAvaluoConstruccion.PLANTA_ID = dto.PlantaId;
                codigoAvaluoConstruccion.UNIDAD_MEDIDA_ID = dto.UnidadMedidaId;
                codigoAvaluoConstruccion.VALOR_MEDIDA = dto.ValorMedida;
                codigoAvaluoConstruccion.AREA_TOTAL = dto.AreaTotal;
                codigoAvaluoConstruccion.MONTO_AVALUO = dto.MontoAvaluo;
                codigoAvaluoConstruccion.OBSERVACIONES = dto.Observaciones;
                codigoAvaluoConstruccion.VALOR_REPOSICION = dto.ValorReposicion;
                codigoAvaluoConstruccion.AREA_CONSTRUCCION = dto.AreaConstruccion;
                codigoAvaluoConstruccion.EXTRA1 = dto.Extra1;
                codigoAvaluoConstruccion.EXTRA2 = dto.Extra2;
                codigoAvaluoConstruccion.EXTRA3 = dto.Extra3;
                codigoAvaluoConstruccion.CODIGO_PARCELA = dto.CodigoParcela;
                codigoAvaluoConstruccion.EXTRA4 = dto.Extra4;
                codigoAvaluoConstruccion.EXTRA5 = dto.Extra5;
                codigoAvaluoConstruccion.EXTRA6 = dto.Extra6;
                codigoAvaluoConstruccion.EXTRA7 = dto.Extra7;
                codigoAvaluoConstruccion.EXTRA8 = dto.Extra8;
                codigoAvaluoConstruccion.EXTRA9 = dto.Extra9;
                codigoAvaluoConstruccion.EXTRA10 = dto.Extra10;
                codigoAvaluoConstruccion.EXTRA11 = dto.Extra11;
                codigoAvaluoConstruccion.EXTRA12 = dto.Extra12;
                codigoAvaluoConstruccion.EXTRA13 = dto.Extra13;
                codigoAvaluoConstruccion.EXTRA14 = dto.Extra14;
                codigoAvaluoConstruccion.EXTRA15 = dto.Extra15;
                codigoAvaluoConstruccion.TIPOLOGIA = dto.Tipologia;
                codigoAvaluoConstruccion.FRENTE_PARCELA = dto.FrenteParcela;
                codigoAvaluoConstruccion.MONTO_COMPLEMENTO = dto.MontoComplemento;
                codigoAvaluoConstruccion.MONTO_COMPLEMENTO_USUARIO = dto.MontoComplementoUsuario;
                codigoAvaluoConstruccion.MONTO_TOTAL_AVALUO = dto.MontoTotalAvaluo;


                codigoAvaluoConstruccion.CODIGO_EMPRESA = conectado.Empresa;
                codigoAvaluoConstruccion.USUARIO_UPD = conectado.Usuario;
                codigoAvaluoConstruccion.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoAvaluoConstruccion);
                var resultDto = await MapCatAvaluoConstruccion(codigoAvaluoConstruccion);
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
