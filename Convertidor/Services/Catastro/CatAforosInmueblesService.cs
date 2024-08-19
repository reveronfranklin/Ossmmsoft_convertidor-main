using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Data.Repository.Catastro;
using Convertidor.Dtos.Catastro;
using Convertidor.Dtos.Cnt;
using Convertidor.Utility;

namespace Convertidor.Services.Catastro
{
    public class CatAforosInmueblesService : ICatAforosInmueblesService
    {
        private readonly ICatAforosInmueblesRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public CatAforosInmueblesService(ICatAforosInmueblesRepository repository,
                                         ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<CatAforosInmueblesResponseDto> MapCatAforosInmuebles(CAT_AFOROS_INMUEBLES entity)
        {
            CatAforosInmueblesResponseDto dto = new CatAforosInmueblesResponseDto();
            dto.CodigoAforoInmueble = entity.CODIGO_AFORO_INMUEBLE;
            dto.Tributo = entity.TRIBUTO;
            dto.CodigoInmueble = entity.CODIGO_INMUEBLE;
            dto.Monto = entity.MONTO;
            dto.MontoMinimo = entity.MONTO_MINIMO;
            dto.CodigoFormaLiquidacion = entity.CODIGO_FORMA_LIQUIDACION;
            dto.CodigoFormaLiqMinimo = entity.CODIGO_FORMA_LIQ_MINIMO;
            dto.FechaLiquidacion = entity.FECHA_LIQUIDACION;
            dto.FechaLiquidacionString = entity.FECHA_LIQUIDACION.ToString("u");
            FechaDto fechaLiquidacionObj = FechaObj.GetFechaDto(entity.FECHA_LIQUIDACION);
            dto.FechaLiquidacionObj = (FechaDto)fechaLiquidacionObj;
            dto.FechaPeriodoIni = entity.FECHA_PERIODO_INI;
            dto.FechaPeriodoIniString = entity.FECHA_PERIODO_INI.ToString("u");
            FechaDto fechaPeriodoIniObj = FechaObj.GetFechaDto(entity.FECHA_PERIODO_INI);
            dto.FechaPeriodoIniObj = (FechaDto)fechaPeriodoIniObj;
            dto.FechaPeriodoFin = entity.FECHA_PERIODO_FIN;
            dto.FechaPeriodoFinString = entity.FECHA_PERIODO_FIN.ToString("u");
            FechaDto fechaPeriodoFinObj = FechaObj.GetFechaDto(entity.FECHA_PERIODO_FIN);
            dto.FechaPeriodoFinObj = (FechaDto)fechaPeriodoFinObj;
            dto.AplicadoId = entity.APLICADO_ID;
            dto.CodigoAplicado = entity.CODIGO_APLICADO;
            dto.Estatus = entity.ESTATUS;
            dto.Extra1 = entity.EXTRA1;
            dto.Extra2 = entity.EXTRA2;
            dto.Extra3 = entity.EXTRA3;
            dto.FechaInicioExonera = entity.FECHA_INICIO_EXONERA;
            dto.FechaInicioExoneraString = entity.FECHA_INICIO_EXONERA.ToString("u");
            FechaDto fechaInicioExonera = FechaObj.GetFechaDto(entity.FECHA_INICIO_EXONERA);
            dto.FechaInicioExoneraObj = (FechaDto)fechaInicioExonera;
            dto.FechaFinExonera = entity.FECHA_FIN_EXONERA;
            dto.FechaFinExoneraString = entity.FECHA_FIN_EXONERA.ToString("u");
            FechaDto fechaFinExonera = FechaObj.GetFechaDto(entity.FECHA_FIN_EXONERA);
            dto.FechaFinExoneraObj = (FechaDto)fechaFinExonera;
            dto.Observacion = entity.OBSERVACION;
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
            dto.Codigoficha = entity.CODIGO_FICHA;
            dto.CodigoAvaluoConstruccion = entity.CODIGO_AVALUO_CONSTRUCCION;
            dto.CodigoAvaluoTerreno = entity.CODIGO_AVALUO_TERRENO;

            return dto;

        }

        public async Task<List<CatAforosInmueblesResponseDto>> MapListCatAforosInmuebles(List<CAT_AFOROS_INMUEBLES> dtos)
        {
            List<CatAforosInmueblesResponseDto> result = new List<CatAforosInmueblesResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapCatAforosInmuebles(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CatAforosInmueblesResponseDto>>> GetAll()
        {

            ResultDto<List<CatAforosInmueblesResponseDto>> result = new ResultDto<List<CatAforosInmueblesResponseDto>>(null);
            try
            {
                var aforosInmuebles = await _repository.GetAll();
                var cant = aforosInmuebles.Count();
                if (aforosInmuebles != null && aforosInmuebles.Count() > 0)
                {
                    var listDto = await MapListCatAforosInmuebles(aforosInmuebles);

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

        public async Task<ResultDto<CatAforosInmueblesResponseDto>> Create(CatAforosInmueblesUpdateDto dto)
        {

            ResultDto<CatAforosInmueblesResponseDto> result = new ResultDto<CatAforosInmueblesResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.Tributo < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "tributo Invalido";
                    return result;
                }

                if (dto.CodigoInmueble < 0)
                {
                   
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo inmueble Invalido";
                    return result;
                    
                }
                
                if (dto.Monto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Invalido ";
                    return result;
                }

                if (dto.CodigoFormaLiquidacion < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo forma liquidacion Invalido";
                    return result;

                }

                if (dto.CodigoFormaLiqMinimo < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo forma liquidacion minimo Invalido";
                    return result;

                }

                if (dto.FechaLiquidacion == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha liquidacion Invalida";
                    return result;

                }

                if (dto.FechaPeriodoIni == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Periodo Inicial Invalida";
                    return result;

                }

                if (dto.FechaPeriodoFin == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Periodo Final Invalida";
                    return result;

                }

                if (dto.AplicadoId < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Aplicado Id Invalido";
                    return result;

                }

                if (dto.CodigoAplicado < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Aplicado Invalido";
                    return result;

                }

                if (dto.Estatus < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Aplicado Id Invalido";
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

                if (dto.FechaInicioExonera == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Inicio Exonera Invalida";
                    return result;

                }

                if (dto.FechaFinExonera == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha fin exonera Invalida";
                    return result;

                }

                if (dto.Observacion is not null && dto.Observacion.Length > 50)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Observacion Invalida";
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

                if (dto.Codigoficha < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo ficha Invalido";
                    return result;

                }

                if (dto.CodigoAvaluoConstruccion < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Avaluo construccion Invalido";
                    return result;

                }

                if (dto.CodigoAvaluoTerreno < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Avaluo Terreno Invalido";
                    return result;

                }

                CAT_AFOROS_INMUEBLES entity = new CAT_AFOROS_INMUEBLES();
                entity.CODIGO_AFORO_INMUEBLE = await _repository.GetNextKey();
                entity.TRIBUTO = dto.Tributo;
                entity.CODIGO_INMUEBLE = dto.CodigoInmueble;
                entity.MONTO = dto.Monto;
                entity.MONTO_MINIMO = dto.MontoMinimo;
                entity.CODIGO_FORMA_LIQUIDACION = dto.CodigoFormaLiquidacion;
                entity.CODIGO_FORMA_LIQ_MINIMO = dto.CodigoFormaLiqMinimo;
                entity.FECHA_LIQUIDACION = dto.FechaLiquidacion;
                entity.FECHA_PERIODO_INI = dto.FechaPeriodoIni;
                entity.FECHA_PERIODO_FIN= dto.FechaPeriodoFin;
                entity.APLICADO_ID = dto.AplicadoId;
                entity.CODIGO_APLICADO = dto.CodigoAplicado;
                entity.ESTATUS = dto.Estatus;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.FECHA_INICIO_EXONERA = dto.FechaInicioExonera;
                entity.FECHA_FIN_EXONERA= dto.FechaFinExonera;
                entity.OBSERVACION = dto.Observacion;
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
                entity.CODIGO_FICHA = dto.Codigoficha;
                entity.CODIGO_AVALUO_CONSTRUCCION = dto.CodigoAvaluoConstruccion;
                entity.CODIGO_AVALUO_TERRENO = dto.CodigoAvaluoTerreno;

                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapCatAforosInmuebles(created.Data);
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

        public async Task<ResultDto<CatAforosInmueblesResponseDto>> Update(CatAforosInmueblesUpdateDto dto)
        {

            ResultDto<CatAforosInmueblesResponseDto> result = new ResultDto<CatAforosInmueblesResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoAforoInmueble = await _repository.GetByCodigo(dto.CodigoAforoInmueble);
                if (codigoAforoInmueble == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Aforo Inmueble Invalido";
                    return result;


                }

                if (dto.Tributo < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "tributo Invalido";
                    return result;
                }

                if (dto.CodigoInmueble < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo inmueble Invalido";
                    return result;

                }

                if (dto.Monto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Invalido ";
                    return result;
                }

                if (dto.CodigoFormaLiquidacion < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo forma liquidacion Invalido";
                    return result;

                }

                if (dto.CodigoFormaLiqMinimo < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo forma liquidacion minimo Invalido";
                    return result;

                }

                if (dto.FechaLiquidacion == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha liquidacion Invalida";
                    return result;

                }

                if (dto.FechaPeriodoIni == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Periodo Inicial Invalida";
                    return result;

                }

                if (dto.FechaPeriodoFin == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Periodo Final Invalida";
                    return result;

                }

                if (dto.AplicadoId < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Aplicado Id Invalido";
                    return result;

                }

                if (dto.CodigoAplicado < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Aplicado Invalido";
                    return result;

                }

                if (dto.Estatus < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Aplicado Id Invalido";
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

                if (dto.FechaInicioExonera == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Inicio Exonera Invalida";
                    return result;

                }

                if (dto.FechaFinExonera == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha fin exonera Invalida";
                    return result;

                }

                if (dto.Observacion is not null && dto.Observacion.Length > 50)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Observacion Invalida";
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

                if (dto.Codigoficha < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo ficha Invalido";
                    return result;

                }

                if (dto.CodigoAvaluoConstruccion < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Avaluo construccion Invalido";
                    return result;

                }

                if (dto.CodigoAvaluoTerreno < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Avaluo Terreno Invalido";
                    return result;

                }


                codigoAforoInmueble.CODIGO_AFORO_INMUEBLE = dto.CodigoAforoInmueble;
                codigoAforoInmueble.TRIBUTO = dto.Tributo;
                codigoAforoInmueble.CODIGO_INMUEBLE = dto.CodigoInmueble;
                codigoAforoInmueble.MONTO = dto.Monto;
                codigoAforoInmueble.MONTO_MINIMO = dto.MontoMinimo;
                codigoAforoInmueble.CODIGO_FORMA_LIQUIDACION = dto.CodigoFormaLiquidacion;
                codigoAforoInmueble.CODIGO_FORMA_LIQ_MINIMO = dto.CodigoFormaLiqMinimo;
                codigoAforoInmueble.FECHA_LIQUIDACION = dto.FechaLiquidacion;
                codigoAforoInmueble.FECHA_PERIODO_INI = dto.FechaPeriodoIni;
                codigoAforoInmueble.FECHA_PERIODO_FIN = dto.FechaPeriodoFin;
                codigoAforoInmueble.APLICADO_ID = dto.AplicadoId;
                codigoAforoInmueble.CODIGO_APLICADO = dto.CodigoAplicado;
                codigoAforoInmueble.ESTATUS = dto.Estatus;
                codigoAforoInmueble.EXTRA1 = dto.Extra1;
                codigoAforoInmueble.EXTRA2 = dto.Extra2;
                codigoAforoInmueble.EXTRA3 = dto.Extra3;
                codigoAforoInmueble.FECHA_INICIO_EXONERA = dto.FechaInicioExonera;
                codigoAforoInmueble.FECHA_FIN_EXONERA = dto.FechaFinExonera;
                codigoAforoInmueble.OBSERVACION = dto.Observacion;
                codigoAforoInmueble.EXTRA4 = dto.Extra4;
                codigoAforoInmueble.EXTRA5 = dto.Extra5;
                codigoAforoInmueble.EXTRA6 = dto.Extra6;
                codigoAforoInmueble.EXTRA7 = dto.Extra7;
                codigoAforoInmueble.EXTRA8 = dto.Extra8;
                codigoAforoInmueble.EXTRA9 = dto.Extra9;
                codigoAforoInmueble.EXTRA10 = dto.Extra10;
                codigoAforoInmueble.EXTRA11 = dto.Extra11;
                codigoAforoInmueble.EXTRA12 = dto.Extra12;
                codigoAforoInmueble.EXTRA13 = dto.Extra13;
                codigoAforoInmueble.EXTRA14 = dto.Extra14;
                codigoAforoInmueble.EXTRA15 = dto.Extra15;
                codigoAforoInmueble.CODIGO_FICHA = dto.Codigoficha;
                codigoAforoInmueble.CODIGO_AVALUO_CONSTRUCCION = dto.CodigoAvaluoConstruccion;
                codigoAforoInmueble.CODIGO_AVALUO_TERRENO = dto.CodigoAvaluoTerreno;

                codigoAforoInmueble.CODIGO_EMPRESA = conectado.Empresa;
                codigoAforoInmueble.USUARIO_UPD = conectado.Usuario;
                codigoAforoInmueble.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoAforoInmueble);
                var resultDto = await MapCatAforosInmuebles(codigoAforoInmueble);
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
