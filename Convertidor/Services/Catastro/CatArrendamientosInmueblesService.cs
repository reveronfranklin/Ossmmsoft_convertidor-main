using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Data.Repository.Catastro;
using Convertidor.Dtos.Catastro;
using Convertidor.Utility;

namespace Convertidor.Services.Catastro
{
    public class CatArrendamientosInmueblesService : ICatArrendamientosInmueblesService
    {
        private readonly ICatArrendamientosInmueblesRepository _repository;

        public CatArrendamientosInmueblesService(ICatArrendamientosInmueblesRepository repository)
        {
            _repository = repository;
        }

        public async Task<CatArrendamientosInmueblesResponseDto> MapCatArrendamientosInmuebles(CAT_ARRENDAMIENTOS_INMUEBLES entity)
        {
            CatArrendamientosInmueblesResponseDto dto = new CatArrendamientosInmueblesResponseDto();
            dto.CodigoArrendamientoInmueble = entity.CODIGO_ARRENDAMIENTO_INMUEBLE;
            dto.CodigoInmueble = entity.CODIGO_INMUEBLE;
            dto.NumeroDeExpediente = entity.NUMERO_DE_EXPEDIENTE;
            dto.FechaDonacion = entity.FECHA_DONACION;
            dto.FechaDonacionString = entity.FECHA_DONACION.ToString("u");
            FechaDto fechaDonacionObj = FechaObj.GetFechaDto(entity.FECHA_DONACION);
            dto.FechaDonacionObj = (FechaDto) fechaDonacionObj;
            dto.NumeroConsecionDeUso = entity.NUMERO_CONSECION_DE_USO;
            dto.NumeroResolucionXResicion = entity.NUMERO_RESOLUCION_X_RESICION;
            dto.FechaResolucionXResicion = entity.FECHA_RESOLUCION_X_RESICION;
            dto.FechaResolucionXResicionString = entity.FECHA_RESOLUCION_X_RESICION.ToString("u");
            FechaDto fechaResolucionXResicionObj = FechaObj.GetFechaDto(entity.FECHA_RESOLUCION_X_RESICION);
            dto.FechaResolucionObj = (FechaDto)fechaResolucionXResicionObj;
            dto.NumeroDeInforme = entity.NUMERO_DE_INFORME;
            dto.FechaInicioContrato = entity.FECHA_INICIO_CONTRATO;
            dto.FechaInicioContratoString = entity.FECHA_INICIO_CONTRATO.ToString("u");
            FechaDto fechaInicioContratoObj = FechaObj.GetFechaDto(entity.FECHA_INICIO_CONTRATO);
            dto.FechaInicioContratoObj = (FechaDto)fechaInicioContratoObj;
            dto.FechaFinContrato = entity.FECHA_FIN_CONTRATO;
            dto.FechaFinContratoString = entity.FECHA_FIN_CONTRATO.ToString("u");
            FechaDto fechaFinContratoObj = FechaObj.GetFechaDto(entity.FECHA_FIN_CONTRATO);
            dto.FechaFinContratoObj = (FechaDto)fechaFinContratoObj;
            dto.NumeroResolucion = entity.NUMERO_RESOLUCION;
            dto.FechaResolucion = entity.FECHA_RESOLUCION;
            dto.FechaResolucionString = entity.FECHA_RESOLUCION.ToString("u");
            FechaDto FechaResolucionObj = FechaObj.GetFechaDto(entity.FECHA_RESOLUCION);
            dto.FechaResolucionObj = (FechaDto)FechaResolucionObj;
            dto.NumeroNotificacion = entity.NUMERO_NOTIFICACION;
            dto.Canon = entity.CANON;
            dto.Tomo = entity.TOMO;
            dto.Folio = entity.FOLIO;
            dto.Registro = entity.REGISTRO;
            dto.CodigoContribuyente = entity.CODIGO_CONTRIBUYENTE;
            dto.NumeroContrato = entity.NUMERO_CONTRATO;
            dto.Observaciones = entity.OBSERVACIONES;
            dto.NumeroArrendamiento = entity.NUMERO_ARRENDAMIENTO;
            dto.TipoTransaccion = entity.TIPO_TRANSACCION;
            dto.Tributo = entity.TRIBUTO;
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
            dto.PrecioUnitario = entity.PRECIO_UNITARIO;
            dto.ValorTerreno = entity.VALOR_TERRENO;
            dto.NumeroAcuerdo = entity.NUMERO_ACUERDO;
            dto.FechaAcuerdo = entity.FECHA_ACUERDO;
            dto.CodigoCatastro = entity.CODIGO_CATASTRO;
            dto.FechaNotificacion = entity.FECHA_NOTIFICACION;
            dto.FechaNotificacionString = entity.FECHA_NOTIFICACION.ToString("u");
            FechaDto fechaNotificacionObj = FechaObj.GetFechaDto(entity.FECHA_NOTIFICACION);
            dto.FechaNotificacionObj = (FechaDto) fechaNotificacionObj;

            return dto;

        }


        public async Task<List<CatArrendamientosInmueblesResponseDto>> MapListCatArrendamientosInmuebles(List<CAT_ARRENDAMIENTOS_INMUEBLES> dtos)
        {
            List<CatArrendamientosInmueblesResponseDto> result = new List<CatArrendamientosInmueblesResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapCatArrendamientosInmuebles(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CatArrendamientosInmueblesResponseDto>>> GetAll()
        {

            ResultDto<List<CatArrendamientosInmueblesResponseDto>> result = new ResultDto<List<CatArrendamientosInmueblesResponseDto>>(null);
            try
            {
                var arrendamientosInmuebles = await _repository.GetAll();
                var cant = arrendamientosInmuebles.Count();
                if (arrendamientosInmuebles != null && arrendamientosInmuebles.Count() > 0)
                {
                    var listDto = await MapListCatArrendamientosInmuebles(arrendamientosInmuebles);

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
