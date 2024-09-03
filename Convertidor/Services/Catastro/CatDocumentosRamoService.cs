using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Dtos.Catastro;
using Convertidor.Utility;

namespace Convertidor.Services.Catastro
{
    public class CatDocumentosRamoService : ICatDocumentosRamoService
    {
        private readonly ICatDocumentosRamoRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ICatDescriptivasRepository _catDescriptivasRepository;

        public CatDocumentosRamoService(ICatDocumentosRamoRepository repository,
                                        ISisUsuarioRepository sisUsuarioRepository,
                                        ICatDescriptivasRepository catDescriptivasRepository)
        {
           _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _catDescriptivasRepository = catDescriptivasRepository;
        }

        public async Task<CatDocumentosRamoResponseDto> MapDocumentosRamo(CAT_DOCUMENTOS_RAMO entity)
        {
            CatDocumentosRamoResponseDto dto = new CatDocumentosRamoResponseDto();

            dto.CodigoDocuRamo = entity.CODIGO_DOCU_RAMO;
            dto.CodigoContribuyente = entity.CODIGO_CONTRIBUYENTE;
            dto.CodigoContribuyenteFk = entity.CODIGO_CONTRIBUYENTE_FK;
            dto.Tributo = entity.TRIBUTO;
            dto.CodigoIdentificador = entity.CODIGO_IDENTIFICADOR;
            dto.CodigoEstado = entity.CODIGO_ESTADO;
            dto.CodigoMunicipio = entity.CODIGO_MUNICIPIO;
            dto.TipoDocumentoId = entity.TIPO_DOCUMENTO_ID;
            dto.NumeroDocumento = entity.NUMERO_DOCUMENTO;
            dto.FechaDocumento = entity.FECHA_DOCUMENTO;
            dto.FechaDocumentoString = entity.FECHA_DOCUMENTO.ToString("u");
            FechaDto fechaDocumentoObj = FechaObj.GetFechaDto(entity.FECHA_DOCUMENTO);
            dto.FechaDocumentoObj = (FechaDto)fechaDocumentoObj;
            dto.Observacion = entity.OBSERVACION;
            dto.Descripcion = entity.DESCRIPCION;
            dto.Extra1 = entity.EXTRA1;
            dto.Extra2 = entity.EXTRA2;
            dto.Extra3 = entity.EXTRA3;
            dto.Origen = entity.ORIGEN;
            dto.TipoTransaccion = entity.TIPO_TRANSACCION;
            dto.TipoDocumentoId = entity.TIPO_DOCUMENTO_ID;
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

        public async Task<List<CatDocumentosRamoResponseDto>> MapListDirecciones(List<CAT_DOCUMENTOS_RAMO> dtos)
        {
            List<CatDocumentosRamoResponseDto> result = new List<CatDocumentosRamoResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapDocumentosRamo(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CatDocumentosRamoResponseDto>>> GetAll()
        {

            ResultDto<List<CatDocumentosRamoResponseDto>> result = new ResultDto<List<CatDocumentosRamoResponseDto>>(null);
            try
            {
                var documentosRamo = await _repository.GetAll();
                var cant = documentosRamo.Count();
                if (documentosRamo != null && documentosRamo.Count() > 0)
                {
                    var listDto = await MapListDirecciones(documentosRamo);

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

        public async Task<ResultDto<CatDocumentosRamoResponseDto>> Create(CatDocumentosRamoUpdateDto dto)
        {

            ResultDto<CatDocumentosRamoResponseDto> result = new ResultDto<CatDocumentosRamoResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();


                if (dto.CodigoContribuyente <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo contribuyente Invalido";
                    return result;

                }

                if (dto.CodigoContribuyenteFk < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Contribuyente Fk Numero Invalido ";
                    return result;
                }

                if (dto.Tributo <=0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tributo Invalido";
                    return result;

                }

                if (dto.CodigoIdentificador <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Identificador Invalido";
                    return result;

                }

                if (dto.CodigoEstado <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Prof Numero Invalido";
                    return result;

                }

                if (dto.CodigoMunicipio <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Municipio Invalido";
                    return result;

                }

                if (dto.TipoDocumentoId <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Documento Id Invalido";
                    return result;

                }

                var tipoDocumentoID = await _catDescriptivasRepository.GetByIdAndTitulo(7, dto.TipoDocumentoId);
                if(tipoDocumentoID == false) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Documento Id Invalido";
                    return result;


                }

                if (dto.NumeroDocumento.Length > 10)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Documento Invalido";
                    return result;

                }
                if (dto.FechaDocumento == null)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Fecha Documento Invalido";
                    return result;

                }
                if (dto.Observacion.Length > 100)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Observacion Invalida";
                    return result;

                }

                if (dto.Descripcion.Length > 100)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
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

                if (dto.Origen.Length > 1)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Origen Invalido";
                    return result;

                }

                if (dto.TipoTransaccion.Length > 20)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Transaccion Invalida";
                    return result;

                }

                if (dto.CodigoAplicacion <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Aplicacion Invalido";
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

                if (dto.CodigoFicha <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Origen Invalido";
                    return result;

                }




                CAT_DOCUMENTOS_RAMO entity = new CAT_DOCUMENTOS_RAMO();
                entity.CODIGO_DOCU_RAMO = await _repository.GetNextKey();
                entity.CODIGO_CONTRIBUYENTE = dto.CodigoContribuyente;
                entity.CODIGO_CONTRIBUYENTE_FK = dto.CodigoContribuyenteFk;
                entity.TRIBUTO = dto.Tributo;
                entity.CODIGO_IDENTIFICADOR = dto.CodigoIdentificador;
                entity.CODIGO_ESTADO = dto.CodigoEstado;
                entity.CODIGO_MUNICIPIO = dto.CodigoMunicipio;
                entity.TIPO_DOCUMENTO_ID = dto.TipoDocumentoId;
                entity.NUMERO_DOCUMENTO = dto.NumeroDocumento;
                entity.FECHA_DOCUMENTO = dto.FechaDocumento;
                entity.OBSERVACION = dto.Observacion;
                entity.DESCRIPCION = dto.Descripcion;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.ORIGEN = dto.Origen;
                entity.TIPO_TRANSACCION = dto.TipoTransaccion;
                entity.CODIGO_APLICACION = dto.CodigoAplicacion;
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
                    var resultDto = await MapDocumentosRamo(created.Data);
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
