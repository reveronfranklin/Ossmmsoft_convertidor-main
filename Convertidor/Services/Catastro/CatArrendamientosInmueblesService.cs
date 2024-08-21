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
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public CatArrendamientosInmueblesService(ICatArrendamientosInmueblesRepository repository,
                                                 ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
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

        public async Task<ResultDto<CatArrendamientosInmueblesResponseDto>> Create(CatArrendamientosInmueblesUpdateDto dto)
        {

            ResultDto<CatArrendamientosInmueblesResponseDto> result = new ResultDto<CatArrendamientosInmueblesResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                
                if (dto.CodigoInmueble < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo inmueble Invalido";
                    return result;

                }

                if (dto.NumeroDeExpediente.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero de expediente Invalido ";
                    return result;
                }

                if (dto.FechaDonacion == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Donacion Invalida";
                    return result;

                }

                if (dto.NumeroConsecionDeUso.Length > 20)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero consecion de uso Invalido";
                    return result;

                }

                if (dto.NumeroResolucionXResicion.Length > 20)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Resolucion por Resicion Invalido";
                    return result;

                }

                if (dto.FechaResolucionXResicion == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Resolucion por Resicion Invalida";
                    return result;

                }

                if (dto.NumeroDeInforme.Length > 20)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Periodo Final Invalida";
                    return result;

                }

                if (dto.FechaInicioContrato == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha inicio contrato Invalida";
                    return result;

                }

                if (dto.FechaFinContrato == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha fin contrato Invalida";
                    return result;

                }

                if (dto.NumeroResolucion.Length > 50)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Resolucion Invalido";
                    return result;

                }

                if (dto.FechaResolucion == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Resolucion Invalida";
                    return result;

                }

                if (dto.NumeroNotificacion.Length > 30)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero notificacion Invalido";
                    return result;

                }

                if (dto.Canon <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Canon Invalido";
                    return result;

                }

                if (dto.Tomo.Length > 10)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tomo Invalido";
                    return result;

                }

                if (dto.Folio.Length > 10)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Folio Invalido";
                    return result;

                }

                if (dto.Registro.Length > 10)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Registro Invalido";
                    return result;

                }

                if (dto.CodigoContribuyente < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo contribuyente Invalido";
                    return result;

                }

                if (dto.NumeroContrato.Length > 15)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Contrato Invalido";
                    return result;

                }

                if (dto.Observaciones.Length > 500)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Observaciones Invalidas";
                    return result;

                }

                if (dto.NumeroArrendamiento.Length > 20)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Arrendamiento Invalido";
                    return result;

                }

                if (dto.TipoTransaccion.Length > 20)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Transaccion Invalida";
                    return result;

                }

                if (dto.Tributo <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tributo Invalido";
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

                if (dto.PrecioUnitario <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Precio Unitario Invalido";
                    return result;

                }

                if (dto.ValorTerreno <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Valor Terreno Invalido";
                    return result;

                }

                if (dto.NumeroAcuerdo.Length > 15)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Acuerdo Invalido";
                    return result;

                }

                if (dto.FechaAcuerdo == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Acuerdo Invalida";
                    return result;

                }

                if (dto.CodigoCatastro.Length > 20)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Catastro Invalido";
                    return result;

                }

                if (dto.FechaNotificacion == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Notificacion Invalida";
                    return result;

                }

                CAT_ARRENDAMIENTOS_INMUEBLES entity = new CAT_ARRENDAMIENTOS_INMUEBLES();
                entity.CODIGO_ARRENDAMIENTO_INMUEBLE = await _repository.GetNextKey();
                entity.CODIGO_INMUEBLE = dto.CodigoInmueble;
                entity.NUMERO_DE_EXPEDIENTE = dto.NumeroDeExpediente;
                entity.FECHA_DONACION = dto.FechaDonacion;
                entity.NUMERO_CONSECION_DE_USO = dto.NumeroConsecionDeUso;
                entity.NUMERO_RESOLUCION_X_RESICION = dto.NumeroResolucionXResicion;
                entity.FECHA_RESOLUCION_X_RESICION = dto.FechaResolucionXResicion;
                entity.NUMERO_DE_INFORME = dto.NumeroDeInforme;
                entity.FECHA_INICIO_CONTRATO = dto.FechaInicioContrato;
                entity.FECHA_FIN_CONTRATO = dto.FechaFinContrato;
                entity.NUMERO_RESOLUCION = dto.NumeroResolucion;
                entity.FECHA_RESOLUCION = dto.FechaResolucion;
                entity.NUMERO_NOTIFICACION = dto.NumeroNotificacion;
                entity.CANON = dto.Canon;
                entity.TOMO = dto.Tomo;
                entity.FOLIO = dto.Folio;
                entity.REGISTRO = dto.Registro;
                entity.CODIGO_CONTRIBUYENTE = dto.CodigoContribuyente;
                entity.NUMERO_CONTRATO = dto.NumeroContrato;
                entity.OBSERVACIONES = dto.Observaciones;
                entity.NUMERO_ARRENDAMIENTO = dto.NumeroArrendamiento;
                entity.TIPO_TRANSACCION = dto.TipoTransaccion;
                entity.TRIBUTO = dto.Tributo;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
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
                entity.PRECIO_UNITARIO = dto.PrecioUnitario;
                entity.VALOR_TERRENO = dto.ValorTerreno;
                entity.NUMERO_ACUERDO = dto.NumeroAcuerdo;
                entity.FECHA_ACUERDO = dto.FechaAcuerdo;
                entity.CODIGO_CATASTRO = dto.CodigoCatastro;
                entity.FECHA_NOTIFICACION = dto.FechaNotificacion;

                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapCatArrendamientosInmuebles(created.Data);
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

        public async Task<ResultDto<CatArrendamientosInmueblesResponseDto>> Update(CatArrendamientosInmueblesUpdateDto dto)
        {

            ResultDto<CatArrendamientosInmueblesResponseDto> result = new ResultDto<CatArrendamientosInmueblesResponseDto>(null);
            try
            {
               

                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoArrendamientoInmueble = await _repository.GetByCodigo(dto.CodigoArrendamientoInmueble);
                if(codigoArrendamientoInmueble == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Arrendamiento inmueble Invalido";
                    return result;

                }


                if (dto.CodigoInmueble < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo inmueble Invalido";
                    return result;

                }

                if (dto.NumeroDeExpediente.Length > 20)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero de expediente Invalido ";
                    return result;
                }

                if (dto.FechaDonacion == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Donacion Invalida";
                    return result;

                }

                if (dto.NumeroConsecionDeUso.Length > 20)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero consecion de uso Invalido";
                    return result;

                }

                if (dto.NumeroResolucionXResicion.Length > 20)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Resolucion por Resicion Invalido";
                    return result;

                }

                if (dto.FechaResolucionXResicion == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Resolucion por Resicion Invalida";
                    return result;

                }

                if (dto.NumeroDeInforme.Length > 20)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Periodo Final Invalida";
                    return result;

                }

                if (dto.FechaInicioContrato == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha inicio contrato Invalida";
                    return result;

                }

                if (dto.FechaFinContrato == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha fin contrato Invalida";
                    return result;

                }

                if (dto.NumeroResolucion.Length > 50)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Resolucion Invalido";
                    return result;

                }

                if (dto.FechaResolucion == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Resolucion Invalida";
                    return result;

                }

                if (dto.NumeroNotificacion.Length > 30)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero notificacion Invalido";
                    return result;

                }

                if (dto.Canon <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Canon Invalido";
                    return result;

                }

                if (dto.Tomo.Length > 10)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tomo Invalido";
                    return result;

                }

                if (dto.Folio.Length > 10)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Folio Invalido";
                    return result;

                }

                if (dto.Registro.Length > 10)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Registro Invalido";
                    return result;

                }

                if (dto.CodigoContribuyente < 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo contribuyente Invalido";
                    return result;

                }

                if (dto.NumeroContrato.Length > 15)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Contrato Invalido";
                    return result;

                }

                if (dto.Observaciones.Length > 500)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Observaciones Invalidas";
                    return result;

                }

                if (dto.NumeroArrendamiento.Length > 20)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Arrendamiento Invalido";
                    return result;

                }

                if (dto.TipoTransaccion.Length > 20)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Transaccion Invalida";
                    return result;

                }

                if (dto.Tributo <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tributo Invalido";
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

                if (dto.PrecioUnitario <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Precio Unitario Invalido";
                    return result;

                }

                if (dto.ValorTerreno <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Valor Terreno Invalido";
                    return result;

                }

                if (dto.NumeroAcuerdo.Length > 15)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Acuerdo Invalido";
                    return result;

                }

                if (dto.FechaAcuerdo == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Acuerdo Invalida";
                    return result;

                }

                if (dto.CodigoCatastro.Length > 20)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Catastro Invalido";
                    return result;

                }

                if (dto.FechaNotificacion == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Notificacion Invalida";
                    return result;

                }


                codigoArrendamientoInmueble.CODIGO_ARRENDAMIENTO_INMUEBLE = dto.CodigoArrendamientoInmueble;
                codigoArrendamientoInmueble.CODIGO_INMUEBLE = dto.CodigoInmueble;
                codigoArrendamientoInmueble.NUMERO_DE_EXPEDIENTE = dto.NumeroDeExpediente;
                codigoArrendamientoInmueble.FECHA_DONACION = dto.FechaDonacion;
                codigoArrendamientoInmueble.NUMERO_CONSECION_DE_USO = dto.NumeroConsecionDeUso;
                codigoArrendamientoInmueble.NUMERO_RESOLUCION_X_RESICION = dto.NumeroResolucionXResicion;
                codigoArrendamientoInmueble.FECHA_RESOLUCION_X_RESICION = dto.FechaResolucionXResicion;
                codigoArrendamientoInmueble.NUMERO_DE_INFORME = dto.NumeroDeInforme;
                codigoArrendamientoInmueble.FECHA_INICIO_CONTRATO = dto.FechaInicioContrato;
                codigoArrendamientoInmueble.FECHA_FIN_CONTRATO = dto.FechaFinContrato;
                codigoArrendamientoInmueble.NUMERO_RESOLUCION = dto.NumeroResolucion;
                codigoArrendamientoInmueble.FECHA_RESOLUCION = dto.FechaResolucion;
                codigoArrendamientoInmueble.NUMERO_NOTIFICACION = dto.NumeroNotificacion;
                codigoArrendamientoInmueble.CANON = dto.Canon;
                codigoArrendamientoInmueble.TOMO = dto.Tomo;
                codigoArrendamientoInmueble.FOLIO = dto.Folio;
                codigoArrendamientoInmueble.REGISTRO = dto.Registro;
                codigoArrendamientoInmueble.CODIGO_CONTRIBUYENTE = dto.CodigoContribuyente;
                codigoArrendamientoInmueble.NUMERO_CONTRATO = dto.NumeroContrato;
                codigoArrendamientoInmueble.OBSERVACIONES = dto.Observaciones;
                codigoArrendamientoInmueble.NUMERO_ARRENDAMIENTO = dto.NumeroArrendamiento;
                codigoArrendamientoInmueble.TIPO_TRANSACCION = dto.TipoTransaccion;
                codigoArrendamientoInmueble.TRIBUTO = dto.Tributo;
                codigoArrendamientoInmueble.EXTRA1 = dto.Extra1;
                codigoArrendamientoInmueble.EXTRA2 = dto.Extra2;
                codigoArrendamientoInmueble.EXTRA3 = dto.Extra3;
                codigoArrendamientoInmueble.EXTRA4 = dto.Extra4;
                codigoArrendamientoInmueble.EXTRA5 = dto.Extra5;
                codigoArrendamientoInmueble.EXTRA6 = dto.Extra6;
                codigoArrendamientoInmueble.EXTRA7 = dto.Extra7;
                codigoArrendamientoInmueble.EXTRA8 = dto.Extra8;
                codigoArrendamientoInmueble.EXTRA9 = dto.Extra9;
                codigoArrendamientoInmueble.EXTRA10 = dto.Extra10;
                codigoArrendamientoInmueble.EXTRA11 = dto.Extra11;
                codigoArrendamientoInmueble.EXTRA12 = dto.Extra12;
                codigoArrendamientoInmueble.EXTRA13 = dto.Extra13;
                codigoArrendamientoInmueble.EXTRA14 = dto.Extra14;
                codigoArrendamientoInmueble.EXTRA15 = dto.Extra15;
                codigoArrendamientoInmueble.PRECIO_UNITARIO = dto.PrecioUnitario;
                codigoArrendamientoInmueble.VALOR_TERRENO = dto.ValorTerreno;
                codigoArrendamientoInmueble.NUMERO_ACUERDO = dto.NumeroAcuerdo;
                codigoArrendamientoInmueble.FECHA_ACUERDO = dto.FechaAcuerdo;
                codigoArrendamientoInmueble.CODIGO_CATASTRO = dto.CodigoCatastro;
                codigoArrendamientoInmueble.FECHA_NOTIFICACION = dto.FechaNotificacion;

                codigoArrendamientoInmueble.CODIGO_EMPRESA = conectado.Empresa;
                codigoArrendamientoInmueble.USUARIO_UPD = conectado.Usuario;
                codigoArrendamientoInmueble.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoArrendamientoInmueble);
                var resultDto = await MapCatArrendamientosInmuebles(codigoArrendamientoInmueble);
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
