using AutoMapper;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;
using Convertidor.Services.Rh;

namespace Convertidor.Data.Repository.Rh
{
	public class RhConceptosService: IRhConceptosService
    {
		
        private readonly DataContext _context;



   
        private readonly IRhConceptosRepository _repository;
        private readonly IRhDescriptivasService _descriptivaService;
        private readonly IRhPersonasRepository _rhPersonasRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IMapper _mapper;

        public RhConceptosService(IRhConceptosRepository repository,
                          IRhDescriptivasService descriptivaService,
                          IRhPersonasRepository rhPersonasRepository,
                          ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
            _descriptivaService = descriptivaService;
            _rhPersonasRepository = rhPersonasRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }
       
        public async Task<List<ListConceptosDto>> GetAll()
        {
            try
            {
                var conceptos = await _repository.GetAll();

                var result = MapListConceptosDto(conceptos);


                return (List<ListConceptosDto>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<RhConceptosResponseDto> GetByCodigo(RhConceptosFilterDto filter)
        {
            try
            {
                var conceptos = await _repository.GetByCodigo(filter.CodigoConcepto);

                var result = await MapConceptosDto(conceptos);


                return (RhConceptosResponseDto)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<ListConceptosDto>> GetConceptosByCodigoPersona(int codigoPersona, DateTime desde, DateTime hasta)
        {
            try
            {
                var conceptos = await _repository.GetConceptosByCodigoPersona(codigoPersona,desde,hasta);

                var result = MapListConceptosDto(conceptos);


                return (List<ListConceptosDto>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<RhConceptosResponseDto> MapConceptosDto(RH_CONCEPTOS dtos)
        {


            RhConceptosResponseDto itemResult = new RhConceptosResponseDto();
            itemResult.CodigoConcepto = dtos.CODIGO_CONCEPTO;
            itemResult.Codigo = dtos.CODIGO;
            itemResult.CodigoTipoNomina = dtos.CODIGO_TIPO_NOMINA;
            itemResult.Denominacion = dtos.DENOMINACION;
            itemResult.Descripcion = dtos.DESCRIPCION;
            itemResult.TipoConcepto = dtos.TIPO_CONCEPTO;
            itemResult.ModuloId = dtos.MODULO_ID;
            itemResult.CodigoPuc = dtos.CODIGO_PUC;
            itemResult.Status = dtos.STATUS;
            itemResult.Extra1 = dtos.EXTRA1;
            itemResult.Extra2 = dtos.EXTRA2;
            itemResult.Extra3 = dtos.EXTRA3;
            itemResult.FrecuenciaId = dtos.FRECUENCIA_ID;
            itemResult.Dedusible = dtos.DEDUSIBLE;
            itemResult.Automatico = dtos.AUTOMATICO;
            itemResult.UsuarioIns = dtos.USUARIO_INS;
            itemResult.FechaIns = dtos.FECHA_INS;
            itemResult.UsuarioUpd = dtos.USUARIO_UPD;
            itemResult.CodigoEmpresa = dtos.CODIGO_EMPRESA;
            itemResult.IdModeloCalculo = dtos.ID_MODELO_CALCULO;

            return itemResult;

        }


        public List<ListConceptosDto> MapListConceptosDto(List<RH_CONCEPTOS> dtos)
        {
            List<ListConceptosDto> result = new List<ListConceptosDto>();

            foreach (var item in dtos)
            {

                ListConceptosDto itemResult = new ListConceptosDto();

                itemResult.CodigoConcepto = item.CODIGO_CONCEPTO;
                itemResult.Codigo = item.CODIGO;
                itemResult.CodigoTipoNomina =item.CODIGO_TIPO_NOMINA;
                itemResult.Denominacion = item.DENOMINACION;

     
             
                result.Add(itemResult);


            }
            return result;



        }

        public async Task<ResultDto<RhConceptosResponseDto>> Create(RhConceptosUpdateDto dto)
        {

            ResultDto<RhConceptosResponseDto> result = new ResultDto<RhConceptosResponseDto>(null);
            try
            {

                var codigo = await _repository.GetCodigoString(dto.Codigo);
                if (codigo is not null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Concepto  Invalido";
                    return result;
                }

                
                if (dto.CodigoTipoNomina == Int32.MinValue)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Nomina Invalido";
                    return result;
                }


                if (dto.Denominacion == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Invalida";
                    return result;
                }

                if (dto.Descripcion == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion invalida";
                    return result;
                }

                if (dto.TipoConcepto == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo concepto invalido";
                    return result;
                }

                var moduloId = await _descriptivaService.GetByTitulo(37);
                if (moduloId == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Modulo Invalido";
                    return result;
                }

                var codigoPuc = dto.CodigoPuc;
                if (codigoPuc == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Modulo Invalido";
                    return result;
                }

                if (dto.Status == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status concepto invalido";
                    return result;
                }
                if (dto.Extra1 == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra1 invalido";
                    return result;
                }
                if (dto.Extra2 == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra2 invalido";
                    return result;
                }
                if (dto.Extra3 == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Extra3 invalido";
                    return result;
                }

                var frecuenciaId = await _descriptivaService.GetByTitulo(49);
                if (frecuenciaId == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Frecuencia Invalida";
                    return result;
                }

                var dedusible = dto.Dedusible;
                if (dedusible == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Deducible Invalido";
                    return result;
                }

                var automatico = dto.Automatico;
                if (automatico == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Automatico Invalido";
                    return result;
                }



                RH_CONCEPTOS entity = new RH_CONCEPTOS();
                entity.CODIGO_CONCEPTO = await _repository.GetNextKey();
                entity.CODIGO = dto.Codigo;
                entity.CODIGO_TIPO_NOMINA = dto.CodigoTipoNomina;
                entity.DENOMINACION = dto.Denominacion;
                entity.DESCRIPCION = dto.Descripcion;
                entity.TIPO_CONCEPTO = dto.TipoConcepto;
                entity.MODULO_ID = dto.ModuloId;
                entity.CODIGO_PUC = dto.CodigoPuc;
                entity.STATUS = dto.Status;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.FRECUENCIA_ID = dto.FrecuenciaId;
                entity.DEDUSIBLE = dto.Dedusible;
                entity.AUTOMATICO = dto.Automatico;
                entity.ID_MODELO_CALCULO = dto.IdModeloCalculo;

                

                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;


                var created = await _repository.Add(entity);

                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapConceptosDto(created.Data);
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

        public async Task<ResultDto<RhConceptosResponseDto>> Update(RhConceptosUpdateDto dto)
        {

            ResultDto<RhConceptosResponseDto> result = new ResultDto<RhConceptosResponseDto>(null);
            try
            {
                var concepto = await _repository.GetByCodigo(dto.CodigoConcepto);
                if (concepto is null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message ="Concepto  Invalido";
                    return result;
                }

                var codigo = await _repository.GetCodigoString(dto.Codigo);
                if (codigo is null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message ="Concepto  Invalido";
                    return result;
                }

                var codigoTipoNomina = await _repository.GetByCodigoTipoNomina(dto.CodigoConcepto, dto.CodigoTipoNomina);
                if (codigoTipoNomina == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Nomina Invalido";
                    return result;
                }


                if (dto.Denominacion == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Denominacion Invalida";
                    return result;
                }

                if (dto.Descripcion == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion invalida";
                    return result;
                }

                if (dto.TipoConcepto == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo concepto invalido";
                    return result;
                }

                var moduloId = await _descriptivaService.GetByTitulo(37);
                if (moduloId == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Modulo Invalido";
                    return result;
                }

                var codigoPuc = dto.CodigoPuc;
                if (codigoPuc == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Modulo Invalido";
                    return result;
                }

                if (dto.Status == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Status concepto invalido";
                    return result;
                }
                if (dto.Extra1 == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Concepto invalido";
                    return result;
                }
                if (dto.Extra2 == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Concepto invalido";
                    return result;
                }
                if (dto.Extra3 == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Concepto invalido";
                    return result;
                }

                var frecuenciaId = await _descriptivaService.GetByTitulo(49);
                if (frecuenciaId == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Frecuencia Invalida";
                    return result;
                }

                var dedusible = dto.Dedusible;
                if (dedusible == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Deducible Invalido";
                    return result;
                }

                var automatico = dto.Automatico;
                if (automatico == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Automatico Invalido";
                    return result;
                }



                RH_CONCEPTOS entity = new RH_CONCEPTOS();
                concepto.CODIGO_CONCEPTO = dto.CodigoConcepto;
                concepto.CODIGO = dto.Codigo;
                concepto.CODIGO_TIPO_NOMINA = dto.CodigoTipoNomina;
                concepto.DENOMINACION = dto.Denominacion;
                concepto.DESCRIPCION = dto.Descripcion;
                concepto.TIPO_CONCEPTO = dto.TipoConcepto;
                concepto.MODULO_ID = dto.ModuloId;
                concepto.CODIGO_PUC = dto.CodigoPuc;
                concepto.STATUS = dto.Status;
                concepto.EXTRA1 = dto.Extra1;
                concepto.EXTRA2 = dto.Extra2;
                concepto.EXTRA3 = dto.Extra3;
                concepto.FRECUENCIA_ID = dto.FrecuenciaId;
                concepto.DEDUSIBLE = dto.Dedusible;
                concepto.AUTOMATICO = dto.Automatico;
                concepto.ID_MODELO_CALCULO = dto.IdModeloCalculo;

                var conectado = await _sisUsuarioRepository.GetConectado();
                concepto.CODIGO_EMPRESA = conectado.Empresa;
                concepto.USUARIO_UPD = conectado.Usuario;
                concepto.FECHA_UPD = DateTime.Now;

                await _repository.Update(concepto);



                var resultDto = await MapConceptosDto(concepto);
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

        //public async Task<ResultDto<RhConceptosDeleteDto>> Delete(RhConceptosDeleteDto dto)
        //{

        //    ResultDto<RhConceptosDeleteDto> result = new ResultDto<RhConceptosDeleteDto>(null);
        //    try
        //    {

        //        var Direccion = await _repository.GetByCodigo(dto.CodigoConcepto);
        //        if (Direccion == null)
        //        {
        //            result.Data = null;
        //            result.IsValid = false;
        //            result.Message = "Concepto no existe";
        //            return result;
        //        }


        //        var deleted = await _repository.Delete(dto.CodigoConcepto);

        //        if (deleted.Length > 0)
        //        {
        //            result.Data = dto;
        //            result.IsValid = false;
        //            result.Message = deleted;
        //        }
        //        else
        //        {
        //            result.Data = dto;
        //            result.IsValid = true;
        //            result.Message = deleted;

        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        result.Data = dto;
        //        result.IsValid = false;
        //        result.Message = ex.Message;
        //    }



        //    return result;
        //}
    }
}

