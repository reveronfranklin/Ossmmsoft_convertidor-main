using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Cnt;
using Convertidor.Utility;

namespace Convertidor.Services.Cnt
{
    public class CntAuxiliaresPucService : ICntAuxiliaresPucService
    {
        private readonly ICntAuxiliaresPucRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ICntAuxiliaresService _cntAuxiliaresService;
        private readonly IPRE_PLAN_UNICO_CUENTASRepository _pRE_PLAN_UNICO_CUENTASRepository;
        private readonly ICntDescriptivaRepository _cntDescriptivaRepository;

        public CntAuxiliaresPucService(ICntAuxiliaresPucRepository repository,
                                        ISisUsuarioRepository sisUsuarioRepository,
                                        ICntAuxiliaresService cntAuxiliaresService,
                                        IPRE_PLAN_UNICO_CUENTASRepository pRE_PLAN_UNICO_CUENTASRepository,
                                        ICntDescriptivaRepository cntDescriptivaRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _cntAuxiliaresService = cntAuxiliaresService;
            _pRE_PLAN_UNICO_CUENTASRepository = pRE_PLAN_UNICO_CUENTASRepository;
            _cntDescriptivaRepository = cntDescriptivaRepository;
        }

        public async Task<CntAuxiliaresPucResponseDto> MapAuxiliaresPuc(CNT_AUXILIARES_PUC dtos)
        {
            CntAuxiliaresPucResponseDto itemResult = new CntAuxiliaresPucResponseDto();
            itemResult.CodigoAuxiliarPuc = dtos.CODIGO_AUXILIAR_PUC;
            itemResult.CodigoAuxiliar = dtos.CODIGO_AUXILIAR;
            itemResult.CodigoPuc = dtos.CODIGO_PUC;
            itemResult.TipoDocumentoId = dtos.TIPO_DOCUMENTO_ID;
           

            return itemResult;

        }

        public async Task<List<CntAuxiliaresPucResponseDto>> MapListAuxiliaresPuc(List<CNT_AUXILIARES_PUC> dtos)
        {
            List<CntAuxiliaresPucResponseDto> result = new List<CntAuxiliaresPucResponseDto>();


            foreach (var item in dtos)
            {
                if (item == null) continue;
                var itemResult = await MapAuxiliaresPuc(item);

                result.Add(itemResult);
            }
            return result;

        }

        public async Task<ResultDto<List<CntAuxiliaresPucResponseDto>>> GetAll()
        {

            ResultDto<List<CntAuxiliaresPucResponseDto>> result = new ResultDto<List<CntAuxiliaresPucResponseDto>>(null);
            try
            {
                var auxiliaresPuc = await _repository.GetAll();
                var cant = auxiliaresPuc.Count();
                if (auxiliaresPuc != null && auxiliaresPuc.Count() > 0)
                {
                    var listDto = await MapListAuxiliaresPuc(auxiliaresPuc);

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

        public async Task<ResultDto<CntAuxiliaresPucResponseDto>> Create(CntAuxiliaresPucUpdateDto dto)
        {
            ResultDto<CntAuxiliaresPucResponseDto> result = new ResultDto<CntAuxiliaresPucResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                if (dto.CodigoAuxiliar <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Auxiliar invalido";
                    return result;
                }

                var codigoAuxiliar = await _cntAuxiliaresService.GetByCodigo(dto.CodigoAuxiliar);
                if (codigoAuxiliar == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Auxiliar invalido";
                    return result;

                }

                if (dto.CodigoPuc <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Invalido";
                    return result;
                }
                var codigoPuc = await _pRE_PLAN_UNICO_CUENTASRepository.GetByCodigo(dto.CodigoPuc);
                if (codigoPuc == null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Invalido";
                    return result;

                }

                if(dto.TipoDocumentoId.Length > 2) 
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Documento ID Invalido";
                    return result;

                }

                
                var tipoDocumentoId = await _cntDescriptivaRepository.GetByIdAndTitulo(7,Convert.ToInt32( dto.TipoDocumentoId));
                if(tipoDocumentoId == false) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Documento ID Invalido";
                    return result;

                }


              


                CNT_AUXILIARES_PUC entity = new CNT_AUXILIARES_PUC();
                entity.CODIGO_AUXILIAR_PUC = await _repository.GetNextKey();
                entity.CODIGO_AUXILIAR = dto.CodigoAuxiliar;
                entity.CODIGO_PUC = dto.CodigoPuc;
                entity.TIPO_DOCUMENTO_ID = dto.TipoDocumentoId;


                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;
                entity.CODIGO_EMPRESA = conectado.Empresa;
             
                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapAuxiliaresPuc(created.Data);
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

        public async Task<ResultDto<CntAuxiliaresPucResponseDto>> Update(CntAuxiliaresPucUpdateDto dto)
        {
            ResultDto<CntAuxiliaresPucResponseDto> result = new ResultDto<CntAuxiliaresPucResponseDto>(null);
            try
            {
                var conectado = await _sisUsuarioRepository.GetConectado();

                var codigoAuxiliarPuc = await _repository.GetByCodigo(dto.CodigoAuxiliarPuc);
                if (codigoAuxiliarPuc == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Auxiliar Puc Invalido";
                    return result;

                }


                if (dto.CodigoAuxiliar <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Auxiliar invalido";
                    return result;
                }

                var codigoAuxiliar = await _cntAuxiliaresService.GetByCodigo(dto.CodigoAuxiliar);
                if (codigoAuxiliar == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "codigo Auxiliar invalido";
                    return result;

                }

                if (dto.CodigoPuc <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Invalido";
                    return result;
                }
                var codigoPuc = await _pRE_PLAN_UNICO_CUENTASRepository.GetByCodigo(dto.CodigoPuc);
                if (codigoPuc == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Puc Invalido";
                    return result;

                }

                if (dto.TipoDocumentoId.Length > 2)
                {

                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Documento ID Invalido";
                    return result;

                }


                var tipoDocumentoId = await _cntDescriptivaRepository.GetByIdAndTitulo(7, Convert.ToInt32(dto.TipoDocumentoId));
                if (tipoDocumentoId == false)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Documento ID Invalido";
                    return result;

                }





                codigoAuxiliarPuc.CODIGO_AUXILIAR_PUC = dto.CodigoAuxiliarPuc;
                codigoAuxiliarPuc.CODIGO_AUXILIAR = dto.CodigoAuxiliar;
                codigoAuxiliarPuc.CODIGO_PUC = dto.CodigoPuc;
                codigoAuxiliarPuc.TIPO_DOCUMENTO_ID = dto.TipoDocumentoId;




                codigoAuxiliarPuc.CODIGO_EMPRESA = conectado.Empresa;
                codigoAuxiliarPuc.USUARIO_UPD = conectado.Usuario;
                codigoAuxiliarPuc.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoAuxiliarPuc);

                var resultDto = await MapAuxiliaresPuc(codigoAuxiliarPuc);
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
