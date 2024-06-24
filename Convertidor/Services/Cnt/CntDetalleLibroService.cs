using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Data.Repository.Cnt;
using Convertidor.Dtos.Cnt;
using Convertidor.Dtos.Presupuesto;
namespace Convertidor.Services.Cnt
{
    
    //
    public class CntDetalleLibroService : ICntDetalleLibroService
    {
        private readonly ICntDetalleLibroRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ICntDescriptivaRepository _cntDescriptivaRepository;

        public CntDetalleLibroService(ICntDetalleLibroRepository repository,
                                        ISisUsuarioRepository sisUsuarioRepository,
                                        ICntDescriptivaRepository cntDescriptivaRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _cntDescriptivaRepository = cntDescriptivaRepository;
        }

       

        public async Task<CntDetalleLibroResponseDto> MapDetalleLibro(CNT_DETALLE_LIBRO dtos)
        {
            CntDetalleLibroResponseDto itemResult = new CntDetalleLibroResponseDto();
            itemResult.CodigoDetalleLibro = dtos.CODIGO_DETALLE_LIBRO;
            itemResult.CodigoLibro = dtos.CODIGO_LIBRO;
            itemResult.TipoDocumentoId = dtos.TIPO_DOCUMENTO_ID;
            itemResult.CodigoCheque = dtos.CODIGO_CHEQUE;
            itemResult.CodigoIdentificador = dtos.CODIGO_IDENTIFICADOR;
            itemResult.OrigenId = dtos.ORIGEN_ID;
            itemResult.NumeroDocumento = dtos.NUMERO_DOCUMENTO;
            itemResult.Descripcion = dtos.DESCRIPCION;
            itemResult.Monto = dtos.MONTO;
            itemResult.Status = dtos.STATUS;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.Status = dtos.STATUS;

            return itemResult;

        }

        public async Task<List<CntDetalleLibroResponseDto>> MapListDetalleLibro(List<CNT_DETALLE_LIBRO> dtos)
        {
            List<CntDetalleLibroResponseDto> result = new List<CntDetalleLibroResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapDetalleLibro(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntDetalleLibroResponseDto>>> GetAll()
        {

            ResultDto<List<CntDetalleLibroResponseDto>> result = new ResultDto<List<CntDetalleLibroResponseDto>>(null);
            try
            {
                var detalleLibro = await _repository.GetAll();
                var cant = detalleLibro.Count();
                if (detalleLibro != null && detalleLibro.Count() > 0)
                {
                    var listDto = await MapListDetalleLibro(detalleLibro);

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

        public async Task<ResultDto<List<CntDetalleLibroResponseDto>>> GetAllByCodigoLibro(int codigoLibro)
        {

            ResultDto<List<CntDetalleLibroResponseDto>> result = new ResultDto<List<CntDetalleLibroResponseDto>>(null);
            try
            {

                var detalleLibro = await _repository.GetByCodigoLibro(codigoLibro);



                if (detalleLibro.Count() > 0)
                {
                    List<CntDetalleLibroResponseDto> listDto = new List<CntDetalleLibroResponseDto>();

                    foreach (var item in detalleLibro)
                    {
                        var dto = await MapDetalleLibro(item);
                        listDto.Add(dto);
                    }


                    result.Data = listDto;

                    result.IsValid = true;
                    result.Message = "";
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = "No existen Datos";

                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }

        public async Task<ResultDto<CntDetalleLibroResponseDto>> Create(CntDetalleLibroUpdateDto dto)
        {
            ResultDto<CntDetalleLibroResponseDto> result = new ResultDto<CntDetalleLibroResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();


                if (dto.CodigoLibro < 0 )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Libro ya existe";
                    return result;
                }

                if (dto.TipoDocumentoId <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Documento Id invalido";
                    return result;

                }

                var tipodocumentoId = await _cntDescriptivaRepository.GetByIdAndTitulo(7, dto.TipoDocumentoId);
                if (tipodocumentoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Documento Id invalido";
                    return result;

                }

              

                if (dto.CodigoIdentificador <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Identificador invalido";
                    return result;

                }

                if (dto.OrigenId <= 0)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Origen Id invalida";
                    return result;
                }

                var origenId = await _cntDescriptivaRepository.GetByIdAndTitulo(8, dto.OrigenId);
                if(origenId == false) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Origen Id invalido";
                    return result;
                }

                if(dto.NumeroDocumento.Length > 20) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Documento invalido";
                    return result;

                }

                if (dto.Descripcion.Length > 1000)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion invalida";
                    return result;

                }

                if (dto.Monto < 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Monto invalido";
                    return result;

                }

                if (dto.Status != "C" && dto.Status != "T" && dto.Status != "A")
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status invalido";
                    return result;

                }

                if (dto.Status.Length > 1)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status invalido";
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

                




                CNT_DETALLE_LIBRO entity = new CNT_DETALLE_LIBRO();
                entity.CODIGO_DETALLE_LIBRO = await _repository.GetNextKey();
                entity.CODIGO_LIBRO = dto.CodigoLibro;
                entity.TIPO_DOCUMENTO_ID = dto.TipoDocumentoId;
                entity.CODIGO_CHEQUE = dto.CodigoCheque;
                entity.CODIGO_IDENTIFICADOR = dto.CodigoIdentificador;
                entity.ORIGEN_ID = dto.OrigenId;
                entity.NUMERO_DOCUMENTO = dto.NumeroDocumento;
                entity.DESCRIPCION = dto.Descripcion;
                entity.MONTO = dto.Monto;
                entity.STATUS = dto.Status;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
              




                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapDetalleLibro(created.Data);
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