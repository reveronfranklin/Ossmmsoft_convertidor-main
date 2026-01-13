using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;

namespace Convertidor.Services.Cnt
{
    public class CntRelacionDocumentosService : ICntRelacionDocumentosService
    {
        private readonly ICntRelacionDocumentosRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ICntDescriptivaRepository _cntDescriptivaRepository;

        public CntRelacionDocumentosService(ICntRelacionDocumentosRepository repository,
                                            ISisUsuarioRepository sisUsuarioRepository,
                                            ICntDescriptivaRepository cntDescriptivaRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _cntDescriptivaRepository = cntDescriptivaRepository;
        }

        public async Task<CntRelacionDocumentosResponseDto> MapRelacionDocumentos(CNT_RELACION_DOCUMENTOS dtos)
        {
            CntRelacionDocumentosResponseDto itemResult = new CntRelacionDocumentosResponseDto();
            itemResult.CodigoRelacionDocumento = dtos.CODIGO_RELACION_DOCUMENTO;
            itemResult.TipoDocumentoId = dtos.TIPO_DOCUMENTO_ID;
            itemResult.TipoTransaccionId = dtos.TIPO_TRANSACCION_ID;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;


            return itemResult;

        }

        public async Task<List<CntRelacionDocumentosResponseDto>> MapListRelacionDocumentos(List<CNT_RELACION_DOCUMENTOS> dtos)
        {
            List<CntRelacionDocumentosResponseDto> result = new List<CntRelacionDocumentosResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapRelacionDocumentos(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntRelacionDocumentosResponseDto>>> GetAll()
        {

            ResultDto<List<CntRelacionDocumentosResponseDto>> result = new ResultDto<List<CntRelacionDocumentosResponseDto>>(null);
            try
            {
                var relacionDocumento = await _repository.GetAll();
                var cant = relacionDocumento.Count();
                if (relacionDocumento != null && relacionDocumento.Count() > 0)
                {
                    var listDto = await MapListRelacionDocumentos(relacionDocumento);

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

        public async Task<ResultDto<CntRelacionDocumentosResponseDto>> Create(CntRelacionDocumentosUpdateDto dto)
        {
            ResultDto<CntRelacionDocumentosResponseDto> result = new ResultDto<CntRelacionDocumentosResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.TipoDocumentoId <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo documento Id invalido";
                    return result;

                }

                var tipoDocumentoId = await _cntDescriptivaRepository.GetByIdAndTitulo(7, dto.TipoDocumentoId);
                if (tipoDocumentoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo documento Id invalido";
                    return result;


                }


                if (dto.TipoTransaccionId <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Transaccion Id invalida";
                    return result;
                }

                var tipoTransaccionId = await _cntDescriptivaRepository.GetByIdAndTitulo(6, dto.TipoTransaccionId);
                if (tipoTransaccionId == false)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Transaccion Id invalida";
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






                CNT_RELACION_DOCUMENTOS entity = new CNT_RELACION_DOCUMENTOS();
                entity.CODIGO_RELACION_DOCUMENTO = await _repository.GetNextKey();
                entity.TIPO_DOCUMENTO_ID = dto.TipoDocumentoId;
                entity.TIPO_TRANSACCION_ID = dto.TipoTransaccionId;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;


                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapRelacionDocumentos(created.Data);
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

        public async Task<ResultDto<CntRelacionDocumentosResponseDto>> Update(CntRelacionDocumentosUpdateDto dto)
        {
            ResultDto<CntRelacionDocumentosResponseDto> result = new ResultDto<CntRelacionDocumentosResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoRelacionDocumento = await _repository.GetByCodigo(dto.CodigoRelacionDocumento);
                if (codigoRelacionDocumento == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Relacion Documento no Existe";
                    return result;

                }


                if (dto.TipoDocumentoId <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo documento Id invalido";
                    return result;

                }

                var tipoDocumentoId = await _cntDescriptivaRepository.GetByIdAndTitulo(7, dto.TipoDocumentoId);
                if (tipoDocumentoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo documento Id invalido";
                    return result;


                }


                if (dto.TipoTransaccionId <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Transaccion Id invalida";
                    return result;
                }

                var tipoTransaccionId = await _cntDescriptivaRepository.GetByIdAndTitulo(6, dto.TipoTransaccionId);
                if (tipoTransaccionId == false)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Transaccion Id invalida";
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





                codigoRelacionDocumento.CODIGO_RELACION_DOCUMENTO = dto.CodigoRelacionDocumento;
                codigoRelacionDocumento.TIPO_DOCUMENTO_ID = dto.TipoDocumentoId;
                codigoRelacionDocumento.TIPO_TRANSACCION_ID = dto.TipoTransaccionId;
                codigoRelacionDocumento.EXTRA1 = dto.Extra1;
                codigoRelacionDocumento.EXTRA2 = dto.Extra2;
                codigoRelacionDocumento.EXTRA3 = dto.Extra3;




                codigoRelacionDocumento.CODIGO_EMPRESA = conectado.Empresa;
                codigoRelacionDocumento.USUARIO_UPD = conectado.Usuario;
                codigoRelacionDocumento.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoRelacionDocumento);

                var resultDto = await MapRelacionDocumentos(codigoRelacionDocumento);
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

        public async Task<ResultDto<CntRelacionDocumentosDeleteDto>> Delete(CntRelacionDocumentosDeleteDto dto)
        {
            ResultDto<CntRelacionDocumentosDeleteDto> result = new ResultDto<CntRelacionDocumentosDeleteDto>(null);
            try
            {

                var codigoRelacionDocumento = await _repository.GetByCodigo(dto.CodigoRelacionDocumento);
                if (codigoRelacionDocumento == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Codigo Relacion Documento no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoRelacionDocumento);

                if (deleted.Length > 0)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = deleted;
                }
                else
                {
                    result.Data = dto;
                    result.IsValid = true;
                    result.Message = deleted;

                }


            }
            catch (Exception ex)
            {
                result.Data = dto;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }

    }
}
