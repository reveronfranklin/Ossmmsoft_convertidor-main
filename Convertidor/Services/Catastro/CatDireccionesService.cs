using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public class CatDireccionesService : ICatDireccionesService
    {
        private readonly ICatDireccionesRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ICatDescriptivasRepository _catDescriptivasRepository;
        private readonly ICAT_UBICACION_NACService _cAT_UBICACION_NACService;

        public CatDireccionesService(ICatDireccionesRepository repository,
                                     ISisUsuarioRepository sisUsuarioRepository,
                                     ICatDescriptivasRepository catDescriptivasRepository,
                                     ICAT_UBICACION_NACService cAT_UBICACION_NACService)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _catDescriptivasRepository = catDescriptivasRepository;
            _cAT_UBICACION_NACService = cAT_UBICACION_NACService;
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

        public async Task<ResultDto<CatDireccionesResponseDto>> Create(CatDireccionesUpdateDto dto)
        {

            ResultDto<CatDireccionesResponseDto> result = new ResultDto<CatDireccionesResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();


                if (dto.CodigoContribuyente <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Contribuyente Invalido";
                    return result;

                }

                if (dto.CodigoCuenta <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Cuenta Invalido ";
                    return result;
                }

                if (dto.CodigoInmueble <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Inmueble Invalido";
                    return result;

                }

                var direccionId = await _catDescriptivasRepository.GetByIdAndTitulo(9, dto.DireccionId);
                if (direccionId == false)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "DireccionId Invalido";
                    return result;

                }



                var pais = await _cAT_UBICACION_NACService.GetPais(dto.PaisId);

                if (pais is null)
                {
                    pais.Id = dto.PaisId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Pais  Invalido";
                    return result;
                }

                var estado = await _cAT_UBICACION_NACService.GetEstado(dto.PaisId, dto.EstadoId);
                if (estado is null)
                {
                    estado.Id = dto.EstadoId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estado Invalido";
                    return result;
                }

                var municipio = await _cAT_UBICACION_NACService.GetMunicipio(dto.PaisId, dto.EstadoId, dto.MunicipioId);
                if (municipio is null)
                {
                    municipio.Id = dto.MunicipioId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Municipio Invalido";
                    return result;
                }

                var ciudad = await _cAT_UBICACION_NACService.GetCiudad(dto.PaisId, dto.EstadoId, dto.MunicipioId, dto.CiudadId);
                if (ciudad is null)
                {
                    ciudad.Id = dto.CiudadId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ciudad Invalida";
                    return result;
                }

                var parroquia = await _cAT_UBICACION_NACService.GetParroquia(dto.PaisId, dto.EstadoId, dto.MunicipioId,
                    dto.CiudadId, dto.ParroquiaId);
                if (parroquia is null)
                {
                    parroquia.Id = dto.ParroquiaId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Parroquia Invalida";
                    return result;
                }

                var sector = await _cAT_UBICACION_NACService.GetSector(dto.PaisId, dto.EstadoId, dto.MunicipioId,
                    dto.CiudadId, dto.ParroquiaId, dto.SectorId);
                if (sector is null)
                {
                    sector.Id = dto.SectorId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Sector Invalido";
                    return result;
                }

                var urbanizacion = await _cAT_UBICACION_NACService.GetUrbanizacion(dto.PaisId, dto.EstadoId, dto.MunicipioId,
                    dto.CiudadId, dto.ParroquiaId, dto.SectorId, dto.UrbanizacionId);
                if (urbanizacion is null)
                {
                    urbanizacion.Id = dto.UrbanizacionId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Urbanizacion Invalida";
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

                if (dto.TipoTransaccion.Length > 20)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Transaccion Invalida";
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

               


                CAT_DIRECCIONES entity = new CAT_DIRECCIONES();
                entity.CODIGO_DIRECCION = await _repository.GetNextKey();
                entity.CODIGO_CONTRIBUYENTE = dto.CodigoContribuyente;
                entity.CODIGO_CUENTA = dto.CodigoCuenta;
                entity.CODIGO_INMUEBLE = dto.CodigoInmueble;
                entity.DIRECCION_ID = dto.DireccionId;
                entity.PAIS_ID = dto.PaisId;
                entity.ESTADO_ID = dto.EstadoId;
                entity.MUNICIPIO_ID = dto.MunicipioId;
                entity.CIUDAD_ID = dto.CiudadId;
                entity.PARROQUIA_ID = dto.ParroquiaId;
                entity.SECTOR_ID = dto.SectorId;
                entity.URBANIZACION_ID = dto.UrbanizacionId;
                entity.MANZANA_ID = dto.ManzanaId;
                entity.PARCELA_ID = dto.ParcelaId;
                entity.VIALIDAD_ID = dto.VialidadId;
                entity.VIALIDAD = dto.Vialidad;
                entity.TIPO_VIVIENDA_ID = dto.TipoViviendaId;
                entity.VIVIENDA = dto.Vivienda;
                entity.TIPO_NIVEL_ID = dto.TipoNivelId;
                entity.NIVEL = dto.Nivel;
                entity.TIPO_UNIDAD_ID = dto.TipoUnidadId;
                entity.NUMERO_UNIDAD = dto.NumeroUnidad;
                entity.COMPLEMENTO_DIR = dto.ComplementoDir;
                entity.TENENCIA_ID = dto.TenenciaId;
                entity.CODIGO_POSTAL = dto.CodigoPostal;
                entity.PRINCIPAL = dto.Principal;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.TIPO_TRANSACCION = dto.TipoTransaccion;
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
                entity.CODIGO_FICHA = dto.CodigoFicha;

                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapDirecciones(created.Data);
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

        public async Task<ResultDto<CatDireccionesResponseDto>> Update(CatDireccionesUpdateDto dto)
        {

            ResultDto<CatDireccionesResponseDto> result = new ResultDto<CatDireccionesResponseDto>(null);
            try
            {


                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoDireccion = await _repository.GetByCodigo(dto.CodigoDireccion);



                if (codigoDireccion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Direccion Invalido";
                    return result;

                }

                if (dto.CodigoContribuyente <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Contribuyente Invalido";
                    return result;

                }

                if (dto.CodigoCuenta <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Cuenta Invalido ";
                    return result;
                }

                if (dto.CodigoInmueble <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Inmueble Invalido";
                    return result;

                }

                var direccionId = await _catDescriptivasRepository.GetByIdAndTitulo(9, dto.DireccionId);
                if (direccionId == false)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "DireccionId Invalido";
                    return result;

                }



                var pais = await _cAT_UBICACION_NACService.GetPais(dto.PaisId);

                if (pais is null)
                {
                    pais.Id = dto.PaisId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Pais  Invalido";
                    return result;
                }

                var estado = await _cAT_UBICACION_NACService.GetEstado(dto.PaisId, dto.EstadoId);
                if (estado is null)
                {
                    estado.Id = dto.EstadoId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estado Invalido";
                    return result;
                }

                var municipio = await _cAT_UBICACION_NACService.GetMunicipio(dto.PaisId, dto.EstadoId, dto.MunicipioId);
                if (municipio is null)
                {
                    municipio.Id = dto.MunicipioId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Municipio Invalido";
                    return result;
                }

                var ciudad = await _cAT_UBICACION_NACService.GetCiudad(dto.PaisId, dto.EstadoId, dto.MunicipioId, dto.CiudadId);
                if (ciudad is null)
                {
                    ciudad.Id = dto.CiudadId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Ciudad Invalida";
                    return result;
                }

                var parroquia = await _cAT_UBICACION_NACService.GetParroquia(dto.PaisId, dto.EstadoId, dto.MunicipioId,
                    dto.CiudadId, dto.ParroquiaId);
                if (parroquia is null)
                {
                    parroquia.Id = dto.ParroquiaId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Parroquia Invalida";
                    return result;
                }

                var sector = await _cAT_UBICACION_NACService.GetSector(dto.PaisId, dto.EstadoId, dto.MunicipioId,
                    dto.CiudadId, dto.ParroquiaId, dto.SectorId);
                if (sector is null)
                {
                    sector.Id = dto.SectorId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Sector Invalido";
                    return result;
                }

                var urbanizacion = await _cAT_UBICACION_NACService.GetUrbanizacion(dto.PaisId, dto.EstadoId, dto.MunicipioId,
                    dto.CiudadId, dto.ParroquiaId, dto.SectorId, dto.UrbanizacionId);
                if (urbanizacion is null)
                {
                    urbanizacion.Id = dto.UrbanizacionId;
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Urbanizacion Invalida";
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

                if (dto.TipoTransaccion.Length > 20)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Transaccion Invalida";
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


             
                codigoDireccion.CODIGO_DIRECCION = dto.CodigoDireccion;
                codigoDireccion.CODIGO_CONTRIBUYENTE = dto.CodigoContribuyente;
                codigoDireccion.CODIGO_CUENTA = dto.CodigoCuenta;
                codigoDireccion.CODIGO_INMUEBLE = dto.CodigoInmueble;
                codigoDireccion.DIRECCION_ID = dto.DireccionId;
                codigoDireccion.PAIS_ID = dto.PaisId;
                codigoDireccion.ESTADO_ID = dto.EstadoId;
                codigoDireccion.MUNICIPIO_ID = dto.MunicipioId;
                codigoDireccion.CIUDAD_ID = dto.CiudadId;
                codigoDireccion.PARROQUIA_ID = dto.ParroquiaId;
                codigoDireccion.SECTOR_ID = dto.SectorId;
                codigoDireccion.URBANIZACION_ID = dto.UrbanizacionId;
                codigoDireccion.MANZANA_ID = dto.ManzanaId;
                codigoDireccion.PARCELA_ID = dto.ParcelaId;
                codigoDireccion.VIALIDAD_ID = dto.VialidadId;
                codigoDireccion.VIALIDAD = dto.Vialidad;
                codigoDireccion.TIPO_VIVIENDA_ID = dto.TipoViviendaId;
                codigoDireccion.VIVIENDA = dto.Vivienda;
                codigoDireccion.TIPO_NIVEL_ID = dto.TipoNivelId;
                codigoDireccion.NIVEL = dto.Nivel;
                codigoDireccion.TIPO_UNIDAD_ID = dto.TipoUnidadId;
                codigoDireccion.NUMERO_UNIDAD = dto.NumeroUnidad;
                codigoDireccion.COMPLEMENTO_DIR = dto.ComplementoDir;
                codigoDireccion.TENENCIA_ID = dto.TenenciaId;
                codigoDireccion.CODIGO_POSTAL = dto.CodigoPostal;
                codigoDireccion.PRINCIPAL = dto.Principal;
                codigoDireccion.EXTRA1 = dto.Extra1;
                codigoDireccion.EXTRA2 = dto.Extra2;
                codigoDireccion.EXTRA3 = dto.Extra3;
                codigoDireccion.TIPO_TRANSACCION = dto.TipoTransaccion;
                codigoDireccion.EXTRA4 = dto.Extra4;
                codigoDireccion.EXTRA5 = dto.Extra5;
                codigoDireccion.EXTRA6 = dto.Extra6;
                codigoDireccion.EXTRA7 = dto.Extra7;
                codigoDireccion.EXTRA8 = dto.Extra8;
                codigoDireccion.EXTRA9 = dto.Extra9;
                codigoDireccion.EXTRA10 = dto.Extra10;
                codigoDireccion.EXTRA11 = dto.Extra11;
                codigoDireccion.EXTRA12 = dto.Extra12;
                codigoDireccion.EXTRA13 = dto.Extra13;
                codigoDireccion.EXTRA14 = dto.Extra14;
                codigoDireccion.EXTRA15 = dto.Extra15;
                codigoDireccion.CODIGO_FICHA = dto.CodigoFicha;


                codigoDireccion.CODIGO_EMPRESA = conectado.Empresa;
                codigoDireccion.USUARIO_INS = conectado.Usuario;
                codigoDireccion.FECHA_INS = DateTime.Now;

                await _repository.Update(codigoDireccion);
                var resultDto = await MapDirecciones(codigoDireccion);
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
