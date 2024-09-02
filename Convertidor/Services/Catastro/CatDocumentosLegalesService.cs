using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Dtos.Catastro;
using Convertidor.Utility;

namespace Convertidor.Services.Catastro
{
    public class CatDocumentosLegalesService : ICatDocumentosLegalesService
    {
        private readonly ICatDocumentosLegalesRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public CatDocumentosLegalesService(ICatDocumentosLegalesRepository repository,
                                           ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<CatDocumentosLegalesResponseDto> MapDocumentosLegales(CAT_DOCUMENTOS_LEGALES entity)
        {
            CatDocumentosLegalesResponseDto dto = new CatDocumentosLegalesResponseDto();

            dto.CodigoDocumentosLegales = entity.CODIGO_DOCUMENTOS_LEGALES;
            dto.CodigoFicha = entity.CODIGO_FICHA;
            dto.DocumentoNumero = entity.DOCUMENTO_NUMERO;
            dto.FolioNumero = entity.FOLIO_NUMERO;
            dto.TomoNumero = entity.TOMO_NUMERO;
            dto.ProfNumero = entity.PROF_NUMERO;
            dto.FechaRegistro = entity.FECHA_REGISTRO;
            dto.FechaRegistroString = entity.FECHA_REGISTRO.ToString("u");
            FechaDto fechaRegistroObj = FechaObj.GetFechaDto(entity.FECHA_REGISTRO);
            dto.FechaRegistroObj = (FechaDto)fechaRegistroObj;
            dto.AreaTerreno = entity.AREA_TERRENO;
            dto.AreaConstruccion = entity.AREA_CONSTRUCCION;
            dto.Protocolo = entity.PROTOCOLO;
            dto.MontoRegistro = entity.MONTO_REGISTRO;
            dto.ServTerreno = entity.SERV_TERRENO;
            dto.PrecioTerreno = entity.PRECIO_TERRENO;
            dto.NumeroCivico = entity.NUMERO_CIVICO;
            dto.FechaPrimeraVisita = entity.FECHA_PRIMERA_VISITA;
            dto.FechaPrimeraVisitaString = entity.FECHA_PRIMERA_VISITA.ToString("u");
            FechaDto fechaPrimeraVisita = FechaObj.GetFechaDto(entity.FECHA_PRIMERA_VISITA);
            dto.FechaPrimeraVisitaObj = (FechaDto) fechaPrimeraVisita;
            dto.FechaLevantamiento = entity.FECHA_LEVANTAMIENTO;
            dto.FechaLevantamientoString = entity.FECHA_LEVANTAMIENTO.ToString("u");
            FechaDto fechaLevantamiento = FechaObj.GetFechaDto(entity.FECHA_LEVANTAMIENTO);
            dto.FechaLevantamientoObj = (FechaDto)fechaLevantamiento;
            dto.ControlArchivo = entity.CONTROL_ARCHIVO;
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
           


            return dto;

        }

        public async Task<List<CatDocumentosLegalesResponseDto>> MapListDirecciones(List<CAT_DOCUMENTOS_LEGALES> dtos)
        {
            List<CatDocumentosLegalesResponseDto> result = new List<CatDocumentosLegalesResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapDocumentosLegales(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CatDocumentosLegalesResponseDto>>> GetAll()
        {

            ResultDto<List<CatDocumentosLegalesResponseDto>> result = new ResultDto<List<CatDocumentosLegalesResponseDto>>(null);
            try
            {
                var documentosLegales = await _repository.GetAll();
                var cant = documentosLegales.Count();
                if (documentosLegales != null && documentosLegales.Count() > 0)
                {
                    var listDto = await MapListDirecciones(documentosLegales);

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

        public async Task<ResultDto<CatDocumentosLegalesResponseDto>> Create(CatDocumentosLegalesUpdateDto dto)
        {

            ResultDto<CatDocumentosLegalesResponseDto> result = new ResultDto<CatDocumentosLegalesResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();


                if (dto.CodigoFicha <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Ficha Invalido";
                    return result;

                }

                if (dto.DocumentoNumero < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Documento Numero Invalido ";
                    return result;
                }

                if (dto.FolioNumero.Length > 15)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Folio Numero Invalido";
                    return result;

                }

                if (dto.TomoNumero.Length > 15)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tomo Numero Invalido";
                    return result;

                }

                if (dto.ProfNumero.Length > 15)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Prof Numero Invalido";
                    return result;

                }

                if (dto.FechaRegistro == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Registro Invalido";
                    return result;

                }

                if (dto.AreaTerreno <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area Terreno Invalido";
                    return result;

                }
                if (dto.AreaConstruccion <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Area Construccion Invalido";
                    return result;

                }
                if (dto.Protocolo.Length > 15)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Protocolo Invalido";
                    return result;

                }
                if (dto.MontoRegistro <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto Registro Invalido";
                    return result;

                }

                if (dto.ServTerreno <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Serv Terreno Invalido";
                    return result;

                }

                if (dto.PrecioTerreno <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Precio Terreno Invalido";
                    return result;

                }

                if (dto.NumeroCivico <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Civico Invalido";
                    return result;

                }

                if (dto.FechaPrimeraVisita == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Invalida";
                    return result;

                }

                if (dto.FechaLevantamiento == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Levantamiento Invalido";
                    return result;

                }

                if (dto.ControlArchivo <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Control Archivo Invalido";
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

               


                CAT_DOCUMENTOS_LEGALES entity = new CAT_DOCUMENTOS_LEGALES();
                entity.CODIGO_DOCUMENTOS_LEGALES = await _repository.GetNextKey();
                entity.CODIGO_FICHA = dto.CodigoFicha;
                entity.DOCUMENTO_NUMERO = dto.DocumentoNumero;
                entity.FOLIO_NUMERO = dto.FolioNumero;
                entity.TOMO_NUMERO = dto.TomoNumero;
                entity.PROF_NUMERO = dto.ProfNumero;
                entity.FECHA_REGISTRO = dto.FechaRegistro;
                entity.AREA_TERRENO = dto.AreaTerreno;
                entity.AREA_CONSTRUCCION = dto.AreaConstruccion;
                entity.PROTOCOLO = dto.Protocolo;
                entity.MONTO_REGISTRO = dto.MontoRegistro;
                entity.SERV_TERRENO = dto.ServTerreno;
                entity.PRECIO_TERRENO = dto.PrecioTerreno;
                entity.NUMERO_CIVICO = dto.NumeroCivico;
                entity.FECHA_PRIMERA_VISITA = dto.FechaPrimeraVisita;
                entity.FECHA_LEVANTAMIENTO = dto.FechaLevantamiento;
                entity.CONTROL_ARCHIVO = dto.ControlArchivo;
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


                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapDocumentosLegales(created.Data);
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


    }
}
