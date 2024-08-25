using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Dtos.Catastro;
using Convertidor.Utility;

namespace Convertidor.Services.Catastro
{
    public class CatAvaluoTerrenoService : ICatAvaluoTerrenoService
    {
        private readonly ICatAvaluoTerrenoRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public CatAvaluoTerrenoService(ICatAvaluoTerrenoRepository repository,
                                       ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<CatAvaluoTerrenoResponseDto> MapCatAvaluoTerreno(CAT_AVALUO_TERRENO entity)
        {
            CatAvaluoTerrenoResponseDto dto = new CatAvaluoTerrenoResponseDto();

            dto.CodigoAvaluoTerreno = entity.CODIGO_AVALUO_TERRENO;
            dto.CodigoFicha = entity.CODIGO_FICHA;
            dto.AnoAvaluo = entity.ANO_AVALUO;
            dto.AnoAvaluoString = entity.ANO_AVALUO.ToString("u");
            FechaDto anoAvaluoObj = FechaObj.GetFechaDto(entity.ANO_AVALUO);
            dto.AnoAvaluoObj = (FechaDto)anoAvaluoObj;
            dto.UnidadMedidaId = entity.UNIDAD_MEDIDA_ID;
            dto.AreaM2 = entity.AREA_M2;
            dto.ValorUnitario = entity.VALOR_UNITARIO;
            dto.ValorAjustado = entity.VALOR_AJUSTADO;
            dto.FactorAjuste = entity.FACTOR_AJUSTE;
            dto.FactorFrente = entity.FACTOR_FRENTE;
            dto.FactorForma = entity.FACTOR_FORMA;
            dto.FactorEsquina = entity.FACTOR_ESQUINA;
            dto.FactorProf = entity.FACTOR_PROF;
            dto.FactorArea = entity.FACTOR_AREA;
            dto.ValorModificado = entity.VALOR_MODIFICADO;
            dto.AreaTotal = entity.AREA_TOTAL;
            dto.MontoAvaluo = entity.MONTO_AVALUO;
            dto.Observaciones = entity.OBSERVACIONES;
            dto.IncrementoEsquina = entity.INCREMENTO_ESQUINA;
            dto.Extra1 = entity.EXTRA1;
            dto.Extra2 = entity.EXTRA2;
            dto.Extra3 = entity.EXTRA3;
            dto.CodigoParcela = entity.CODIGO_PARCELA;
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
            dto.CodigoZonificacion = entity.CODIGO_ZONIFICACION;
            dto.FrenteParcela = entity.FRENTE_PARCELA;
            dto.CodigoVialidadPrincipal = entity.CODIGO_VIALIDAD_PRINCIPAL;
            dto.CodigoVialidadAdyacente1 = entity.CODIGO_VIALIDAD_ADYACENTE1;
            dto.CodigoVialidadAdyacente2 = entity.CODIGO_VIALIDAD_ADYACENTE2;
            dto.Vialidad1 = entity.VIALIDAD1;
            dto.Vialidad2 = entity.VIALIDAD2;
            dto.Vialidad3 = entity.VIALIDAD3;
            dto.Vialidad4 = entity.VIALIDAD4;
            dto.UbicacionTerreno = entity.UBICACION_TERRENO;
            dto.CodigoVialidad1 = entity.CODIGO_VIALIDAD1;
            dto.CodigoVialidad2 = entity.CODIGO_VIALIDAD2;
            dto.CodigoVialidad3 = entity.CODIGO_VIALIDAD3;
            dto.CodigoVialidad4 = entity.CODIGO_VIALIDAD4;
            dto.FactorProfundidad = entity.FACTOR_PROFUNDIDAD;
            dto.MontoTotalAvaluo = entity.MONTO_AVALUO;


            return dto;

        }

        public async Task<List<CatAvaluoTerrenoResponseDto>> MapListCatAvaluoTerreno(List<CAT_AVALUO_TERRENO> dtos)
        {
            List<CatAvaluoTerrenoResponseDto> result = new List<CatAvaluoTerrenoResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapCatAvaluoTerreno(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CatAvaluoTerrenoResponseDto>>> GetAll()
        {

            ResultDto<List<CatAvaluoTerrenoResponseDto>> result = new ResultDto<List<CatAvaluoTerrenoResponseDto>>(null);
            try
            {
                var avaluoTerreno = await _repository.GetAll();
                var cant = avaluoTerreno.Count();
                if (avaluoTerreno != null && avaluoTerreno.Count() > 0)
                {
                    var listDto = await MapListCatAvaluoTerreno(avaluoTerreno);

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

        public async Task<ResultDto<CatAvaluoTerrenoResponseDto>> Create(CatAvaluoTerrenoUpdateDto dto)
        {

            ResultDto<CatAvaluoTerrenoResponseDto> result = new ResultDto<CatAvaluoTerrenoResponseDto>(null);
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

                if (dto.UnidadMedidaId <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Unidad Medida Id Invalido";
                    return result;

                }

                if (dto.AreaM2 <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area M2 Invalida";
                    return result;

                }

                if (dto.ValorUnitario <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Valor Unitario Invalido";
                    return result;

                }

                if (dto.ValorAjustado <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Valor Ajustado Invalido";
                    return result;

                }

                if (dto.FactorAjuste <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Factor ajuste Invalido";
                    return result;

                }
                if (dto.FactorFrente <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Factor frente Invalido";
                    return result;

                }
                if (dto.FactorForma <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Factor forma Invalido";
                    return result;

                }
                if (dto.FactorEsquina <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Factor esquina Invalido";
                    return result;

                }

                if (dto.FactorProf <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Factor prof Invalido";
                    return result;

                }

                if (dto.FactorArea <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Factor area Invalido";
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
                    result.Message = "Monto avaluo Invalido";
                    return result;

                }

                if (dto.Observaciones.Length > 200)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Observaciones Invalida";
                    return result;

                }

                if (dto.IncrementoEsquina <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Incremento esquina Invalido";
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

                if (dto.CodigoZonificacion <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo zonificacion Invalido";
                    return result;

                }

                if (dto.FrenteParcela <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Frente Parcela Invalido";
                    return result;

                }

                if (dto.CodigoVialidadPrincipal <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo vialidad principal Invalido";
                    return result;

                }

                if (dto.CodigoVialidadAdyacente1 <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo vialidad adyacente 1 Invalido";
                    return result;

                }

                if (dto.CodigoVialidadAdyacente2 <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo vialidad adyacente 2 Invalido";
                    return result;

                }

                if (dto.Vialidad1 <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "vialidad 1 Invalido";
                    return result;

                }

                if (dto.Vialidad2 <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "vialidad 2 Invalido";
                    return result;

                }

                if (dto.Vialidad3 <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "vialidad 3 Invalido";
                    return result;

                }

                if (dto.Vialidad4 <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "vialidad 4 Invalido";
                    return result;

                }

                if(dto.UbicacionTerreno <= 0) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ubicacion Terreno Invalido";
                    return result;


                }

                if (dto.CodigoVialidad1 <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo vialidad 1 Invalido";
                    return result;


                }

                if (dto.CodigoVialidad2 <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo vialidad 2 Invalido";
                    return result;


                }

                if (dto.CodigoVialidad3 <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo vialidad 3 Invalido";
                    return result;


                }

                if (dto.CodigoVialidad4 <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo vialidad 4 Invalido";
                    return result;


                }

                if (dto.FactorProfundidad <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Factor profundidad Invalido";
                    return result;

                }

               
                if (dto.MontoTotalAvaluo <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Total Avaluo Invalido";
                    return result;

                }


                CAT_AVALUO_TERRENO entity = new CAT_AVALUO_TERRENO();
                entity.CODIGO_AVALUO_TERRENO = await _repository.GetNextKey();
                entity.CODIGO_FICHA = dto.CodigoFicha;
                entity.ANO_AVALUO = dto.AnoAvaluo;
                entity.UNIDAD_MEDIDA_ID = dto.UnidadMedidaId;
                entity.AREA_M2 = dto.AreaM2;
                entity.VALOR_UNITARIO = dto.ValorUnitario;
                entity.VALOR_AJUSTADO = dto.ValorAjustado;
                entity.FACTOR_AJUSTE = dto.FactorAjuste;
                entity.FACTOR_FRENTE = dto.FactorFrente;
                entity.FACTOR_FORMA = dto.FactorForma;
                entity.FACTOR_ESQUINA = dto.FactorEsquina;
                entity.FACTOR_PROF = dto.FactorProf;
                entity.FACTOR_AREA = dto.FactorArea;
                entity.VALOR_MODIFICADO = dto.ValorModificado;
                entity.AREA_TOTAL = dto.AreaTotal;
                entity.MONTO_AVALUO = dto.MontoAvaluo;
                entity.OBSERVACIONES = dto.Observaciones;
                entity.INCREMENTO_ESQUINA = dto.IncrementoEsquina;
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
                entity.CODIGO_ZONIFICACION = dto.CodigoZonificacion;
                entity.FRENTE_PARCELA = dto.FrenteParcela;
                entity.CODIGO_VIALIDAD_PRINCIPAL = dto.CodigoVialidadPrincipal;
                entity.CODIGO_VIALIDAD_ADYACENTE1 = dto.CodigoVialidadAdyacente1;
                entity.CODIGO_VIALIDAD_ADYACENTE2 = dto.CodigoVialidadAdyacente2;
                entity.VIALIDAD1 = dto.Vialidad1;
                entity.VIALIDAD2 = dto.Vialidad2;
                entity.VIALIDAD3 = dto.Vialidad3;
                entity.VIALIDAD4 = dto.Vialidad4;
                entity.UBICACION_TERRENO = dto.UbicacionTerreno;
                entity.CODIGO_VIALIDAD1 = dto.CodigoVialidad1;
                entity.CODIGO_VIALIDAD2 = dto.CodigoVialidad2;
                entity.CODIGO_VIALIDAD3 = dto.CodigoVialidad3;
                entity.CODIGO_VIALIDAD4 = dto.CodigoVialidad4;
                entity.FACTOR_PROFUNDIDAD = dto.FactorProfundidad;
                entity.MONTO_TOTAL_AVALUO = dto.MontoTotalAvaluo;

                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapCatAvaluoTerreno(created.Data);
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

        public async Task<ResultDto<CatAvaluoTerrenoResponseDto>> Update(CatAvaluoTerrenoUpdateDto dto)
        {

            ResultDto<CatAvaluoTerrenoResponseDto> result = new ResultDto<CatAvaluoTerrenoResponseDto>(null);
            try
            {


                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoAvaluoTerreno = await _repository.GetByCodigo(dto.CodigoAvaluoTerreno);
                if (codigoAvaluoTerreno == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Avaluo Terreno Invalido";
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

                if (dto.UnidadMedidaId <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Unidad Medida Id Invalido";
                    return result;

                }

                if (dto.AreaM2 <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area M2 Invalida";
                    return result;

                }

                if (dto.ValorUnitario <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Valor Unitario Invalido";
                    return result;

                }

                if (dto.ValorAjustado <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Valor Ajustado Invalido";
                    return result;

                }

                if (dto.FactorAjuste <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Factor ajuste Invalido";
                    return result;

                }
                if (dto.FactorFrente <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Factor frente Invalido";
                    return result;

                }
                if (dto.FactorForma <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Factor forma Invalido";
                    return result;

                }
                if (dto.FactorEsquina <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Factor esquina Invalido";
                    return result;

                }

                if (dto.FactorProf <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Factor prof Invalido";
                    return result;

                }

                if (dto.FactorArea <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Factor area Invalido";
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
                    result.Message = "Monto avaluo Invalido";
                    return result;

                }

                if (dto.Observaciones.Length > 200)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Observaciones Invalida";
                    return result;

                }

                if (dto.IncrementoEsquina <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Incremento esquina Invalido";
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

                if (dto.CodigoZonificacion <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo zonificacion Invalido";
                    return result;

                }

                if (dto.FrenteParcela <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Frente Parcela Invalido";
                    return result;

                }

                if (dto.CodigoVialidadPrincipal <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo vialidad principal Invalido";
                    return result;

                }

                if (dto.CodigoVialidadAdyacente1 <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo vialidad adyacente 1 Invalido";
                    return result;

                }

                if (dto.CodigoVialidadAdyacente2 <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo vialidad adyacente 2 Invalido";
                    return result;

                }

                if (dto.Vialidad1 <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "vialidad 1 Invalido";
                    return result;

                }

                if (dto.Vialidad2 <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "vialidad 2 Invalido";
                    return result;

                }

                if (dto.Vialidad3 <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "vialidad 3 Invalido";
                    return result;

                }

                if (dto.Vialidad4 <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "vialidad 4 Invalido";
                    return result;

                }

                if (dto.UbicacionTerreno <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ubicacion Terreno Invalido";
                    return result;


                }

                if (dto.CodigoVialidad1 <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo vialidad 1 Invalido";
                    return result;


                }

                if (dto.CodigoVialidad2 <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo vialidad 2 Invalido";
                    return result;


                }

                if (dto.CodigoVialidad3 <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo vialidad 3 Invalido";
                    return result;


                }

                if (dto.CodigoVialidad4 <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo vialidad 4 Invalido";
                    return result;


                }

                if (dto.FactorProfundidad <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Factor profundidad Invalido";
                    return result;

                }


                if (dto.MontoTotalAvaluo <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Total Avaluo Invalido";
                    return result;

                }


                codigoAvaluoTerreno.CODIGO_AVALUO_TERRENO = dto.CodigoAvaluoTerreno;
                codigoAvaluoTerreno.CODIGO_FICHA = dto.CodigoFicha;
                codigoAvaluoTerreno.ANO_AVALUO = dto.AnoAvaluo;
                codigoAvaluoTerreno.UNIDAD_MEDIDA_ID = dto.UnidadMedidaId;
                codigoAvaluoTerreno.AREA_M2 = dto.AreaM2;
                codigoAvaluoTerreno.VALOR_UNITARIO = dto.ValorUnitario;
                codigoAvaluoTerreno.VALOR_AJUSTADO = dto.ValorAjustado;
                codigoAvaluoTerreno.FACTOR_AJUSTE = dto.FactorAjuste;
                codigoAvaluoTerreno.FACTOR_FRENTE = dto.FactorFrente;
                codigoAvaluoTerreno.FACTOR_FORMA = dto.FactorForma;
                codigoAvaluoTerreno.FACTOR_ESQUINA = dto.FactorEsquina;
                codigoAvaluoTerreno.FACTOR_PROF = dto.FactorProf;
                codigoAvaluoTerreno.FACTOR_AREA = dto.FactorArea;
                codigoAvaluoTerreno.VALOR_MODIFICADO = dto.ValorModificado;
                codigoAvaluoTerreno.AREA_TOTAL = dto.AreaTotal;
                codigoAvaluoTerreno.MONTO_AVALUO = dto.MontoAvaluo;
                codigoAvaluoTerreno.OBSERVACIONES = dto.Observaciones;
                codigoAvaluoTerreno.INCREMENTO_ESQUINA = dto.IncrementoEsquina;
                codigoAvaluoTerreno.EXTRA1 = dto.Extra1;
                codigoAvaluoTerreno.EXTRA2 = dto.Extra2;
                codigoAvaluoTerreno.EXTRA3 = dto.Extra3;
                codigoAvaluoTerreno.CODIGO_PARCELA = dto.CodigoParcela;
                codigoAvaluoTerreno.EXTRA4 = dto.Extra4;
                codigoAvaluoTerreno.EXTRA5 = dto.Extra5;
                codigoAvaluoTerreno.EXTRA6 = dto.Extra6;
                codigoAvaluoTerreno.EXTRA7 = dto.Extra7;
                codigoAvaluoTerreno.EXTRA8 = dto.Extra8;
                codigoAvaluoTerreno.EXTRA9 = dto.Extra9;
                codigoAvaluoTerreno.EXTRA10 = dto.Extra10;
                codigoAvaluoTerreno.EXTRA11 = dto.Extra11;
                codigoAvaluoTerreno.EXTRA12 = dto.Extra12;
                codigoAvaluoTerreno.EXTRA13 = dto.Extra13;
                codigoAvaluoTerreno.EXTRA14 = dto.Extra14;
                codigoAvaluoTerreno.EXTRA15 = dto.Extra15;
                codigoAvaluoTerreno.CODIGO_ZONIFICACION = dto.CodigoZonificacion;
                codigoAvaluoTerreno.FRENTE_PARCELA = dto.FrenteParcela;
                codigoAvaluoTerreno.CODIGO_VIALIDAD_PRINCIPAL = dto.CodigoVialidadPrincipal;
                codigoAvaluoTerreno.CODIGO_VIALIDAD_ADYACENTE1 = dto.CodigoVialidadAdyacente1;
                codigoAvaluoTerreno.CODIGO_VIALIDAD_ADYACENTE2 = dto.CodigoVialidadAdyacente2;
                codigoAvaluoTerreno.VIALIDAD1 = dto.Vialidad1;
                codigoAvaluoTerreno.VIALIDAD2 = dto.Vialidad2;
                codigoAvaluoTerreno.VIALIDAD3 = dto.Vialidad3;
                codigoAvaluoTerreno.VIALIDAD4 = dto.Vialidad4;
                codigoAvaluoTerreno.UBICACION_TERRENO = dto.UbicacionTerreno;
                codigoAvaluoTerreno.CODIGO_VIALIDAD1 = dto.CodigoVialidad1;
                codigoAvaluoTerreno.CODIGO_VIALIDAD2 = dto.CodigoVialidad2;
                codigoAvaluoTerreno.CODIGO_VIALIDAD3 = dto.CodigoVialidad3;
                codigoAvaluoTerreno.CODIGO_VIALIDAD4 = dto.CodigoVialidad4;
                codigoAvaluoTerreno.FACTOR_PROFUNDIDAD = dto.FactorProfundidad;
                codigoAvaluoTerreno.MONTO_TOTAL_AVALUO = dto.MontoTotalAvaluo;


                codigoAvaluoTerreno.CODIGO_EMPRESA = conectado.Empresa;
                codigoAvaluoTerreno.USUARIO_UPD = conectado.Usuario;
                codigoAvaluoTerreno.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoAvaluoTerreno);
                var resultDto = await MapCatAvaluoTerreno(codigoAvaluoTerreno);
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
