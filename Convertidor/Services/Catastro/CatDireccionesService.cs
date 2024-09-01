using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public class CatDireccionesService : ICatDireccionesService
    {
        private readonly ICatDireccionesRepository _repository;

        public CatDireccionesService(ICatDireccionesRepository repository)
        {
            _repository = repository;
        }

        public async Task<CatDireccionesResponseDto> MapDirecciones(CAT_DIRECCIONES entity)
        {
            CatDireccionesResponseDto dto = new CatDireccionesResponseDto();

            dto.CodigoDireccion = entity.CODIGO_DIRECCION;
            dto.CodigoContribuyente = entity.CODIGO_CONTRIBUYENTE;
            dto.CodigoCuenta = entity.CODIGO_CUENTA;
            dto.CodigoContribuyente = entity.CODIGO_CONTRIBUYENTE;
            dto.CodigoInmueble = entity.CODIGO_INMUEBLE;
            dto.DireccionId = entity.DIRECCION_ID;
            dto.PaisId = entity.PAIS_ID;
            dto.EstadoId = entity.ESTADO_ID;
            dto.MunicipioId = entity.MUNICIPIO_ID;
            dto.ParroquiaId = entity.PARROQUIA_ID;
            dto.SectorId = entity.SECTOR_ID;
            dto.UrbanizacionId = entity.URBANIZACION_ID;
            dto.ManzanaId = entity.MANZANA_ID;
            dto.ParcelaId = entity.PARCELA_ID;
            dto.VialidadId = entity.VIALIDAD_ID;
            dto.Vialidad = entity.VIALIDAD;
            dto.TipoViviendaId = entity.TIPO_VIVIENDA_ID;
            dto.Vivienda = entity.VIVIENDA;
            dto.TipoNivelId = entity.TIPO_NIVEL_ID;
            dto.Nivel = entity.NIVEL;
            dto.TipoUnidadId = entity.TIPO_UNIDAD_ID;
            dto.NumeroUnidad = entity.NUMERO_UNIDAD;
            dto.ComplementoDir = entity.COMPLEMENTO_DIR;
            dto.TenenciaId = entity.TENENCIA_ID;
            dto.CodigoPostal = entity.CODIGO_POSTAL;
            dto.Principal = entity.PRINCIPAL;
            dto.Extra1 = entity.EXTRA1;
            dto.Extra2 = entity.EXTRA2;
            dto.Extra3 = entity.EXTRA3;
            dto.TipoTransaccion = entity.TIPO_TRANSACCION;
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
            dto.CodigoFicha = entity.CODIGO_FICHA;


            return dto;

        }

        public async Task<List<CatDireccionesResponseDto>> MapListDirecciones(List<CAT_DIRECCIONES> dtos)
        {
            List<CatDireccionesResponseDto> result = new List<CatDireccionesResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapDirecciones(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CatDireccionesResponseDto>>> GetAll()
        {

            ResultDto<List<CatDireccionesResponseDto>> result = new ResultDto<List<CatDireccionesResponseDto>>(null);
            try
            {
                var direcciones = await _repository.GetAll();
                var cant = direcciones.Count();
                if (direcciones != null && direcciones.Count() > 0)
                {
                    var listDto = await MapListDirecciones(direcciones);

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
