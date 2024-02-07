using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;
using Convertidor.Services.Rh;

namespace Convertidor.Data.Repository.Rh
{
	public class RhTipoNominaService: IRhTipoNominaService
    {
        private readonly IRhTipoNominaRepository _repository;
        private readonly IRhDescriptivasService _descriptivasService;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public RhTipoNominaService(IRhTipoNominaRepository repository,
                                   IRhDescriptivasService rhDescriptivasService,
                                   ISisUsuarioRepository sisUsuarioRepository)
        {
            _repository = repository;
           _descriptivasService = rhDescriptivasService;
            _sisUsuarioRepository = sisUsuarioRepository;
        }
       
        public async Task<List<RhTiposNominaResponseDto>> GetAll()
        {
            try
            {
                var tipoNomina = await _repository.GetAll();

                var result = await MapListTipoNominaDto(tipoNomina);


                return (List<RhTiposNominaResponseDto>)result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        public async Task<RhTiposNominaResponseDto> GetByCodigo(RhTiposNominaFilterDto filter)
        {
            try
            {
                var tipoNomina = await _repository.GetByCodigo(filter.CodigoTipoNomina);

                var result = await MapTiposNominaDto(tipoNomina);
                
                return (RhTiposNominaResponseDto)result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        public async Task<List<RhTiposNominaResponseDto>> GetTipoNominaByCodigoPersona(int codigoPersona,DateTime desde,DateTime hasta)
        {
            try
            {
                var tipoNomina = await _repository.GetTipoNominaByCodigoPersona(codigoPersona,desde,hasta);

                var result = await MapListTipoNominaDto(tipoNomina);


                return (List<RhTiposNominaResponseDto>)result; 
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<RhTiposNominaResponseDto> MapTiposNominaDto(RH_TIPOS_NOMINA dtos)
        {


            RhTiposNominaResponseDto itemResult = new RhTiposNominaResponseDto();
            itemResult.CodigoTipoNomina = dtos.CODIGO_TIPO_NOMINA;
            itemResult.Descripcion = dtos.DESCRIPCION;
            itemResult.SiglasTipoNomina = dtos.SIGLAS_TIPO_NOMINA;
            itemResult.FrecuenciaPagoId = dtos.FRECUENCIA_PAGO_ID;
            itemResult.FrecuenciaPago = await _descriptivasService.GetDescripcionByCodigoDescriptiva(dtos.FRECUENCIA_PAGO_ID);
            itemResult.SueldoMinimo = dtos.SUELDO_MINIMO;
            


            return itemResult;



        }


        public async Task<List<RhTiposNominaResponseDto>> MapListTipoNominaDto(List<RH_TIPOS_NOMINA> dtos)
        {
            List<RhTiposNominaResponseDto> result = new List<RhTiposNominaResponseDto>();

            foreach (var item in dtos)
            {

                var itemResult = await MapTiposNominaDto(item);
                    
               
                result.Add(itemResult);


            }
            return result;



        }

        public async Task<ResultDto<RhTiposNominaResponseDto>> Update(RhTiposNominaUpdateDto dto)
        {

            ResultDto<RhTiposNominaResponseDto> result = new ResultDto<RhTiposNominaResponseDto>(null);
            try
            {

                var codigoTipoNomina = await _repository.GetByCodigo(dto.CodigoTipoNomina);
                if (codigoTipoNomina == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo tipo nomina no existe";
                    return result;
                }

                if (dto.Descripcion==string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion  Invalida";
                    return result;
                }
                if (dto.SiglasTipoNomina is null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Siglas tipo nomina  Invalidas";
                    return result;
                }
                
                var frecuenciaPagoId = await _descriptivasService.GetByTitulo(38);
                if (frecuenciaPagoId is null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Frecuencia de Pago Invalido";
                    return result;
                }
              

                if(dto.SueldoMinimo==null) 
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "sueldo minimo invalido";
                    return result;

                }

                codigoTipoNomina.CODIGO_TIPO_NOMINA = dto.CodigoTipoNomina;
                codigoTipoNomina.DESCRIPCION = dto.Descripcion.ToUpper();
                codigoTipoNomina.SIGLAS_TIPO_NOMINA = dto.SiglasTipoNomina.ToUpper();
                codigoTipoNomina.FRECUENCIA_PAGO_ID = dto.FrecuenciaPagoId;
                codigoTipoNomina.SUELDO_MINIMO = dto.SueldoMinimo;

                var conectado = await _sisUsuarioRepository.GetConectado();
                codigoTipoNomina.CODIGO_EMPRESA = conectado.Empresa;
                codigoTipoNomina.USUARIO_UPD = conectado.Usuario;
                codigoTipoNomina.FECHA_UPD = DateTime.Now;


                await _repository.Update(codigoTipoNomina);



                var resultDto = await MapTiposNominaDto(codigoTipoNomina);
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

        public async Task<ResultDto<RhTiposNominaResponseDto>> Create(RhTiposNominaUpdateDto dto)
        {

            ResultDto<RhTiposNominaResponseDto> result = new ResultDto<RhTiposNominaResponseDto>(null);
            try
            {
                var codigoTipoNomina = await _repository.GetByCodigo(dto.CodigoTipoNomina);
                if (codigoTipoNomina is not null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo tipo nomina ya existe";
                    return result;
                }

                if (dto.Descripcion == string.Empty)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion  Invalida";
                    return result;
                }
                if (dto.SiglasTipoNomina is null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Siglas tipo nomina  Invalidas";
                    return result;
                }

                var frecuenciaPagoId = await _descriptivasService.GetByTitulo(38);
                if (frecuenciaPagoId is null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Frecuencia de Pago Invalido";
                    return result;
                }

               
                if (dto.SueldoMinimo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "sueldo minimo invalido";
                    return result;

                }

                RH_TIPOS_NOMINA entity = new RH_TIPOS_NOMINA();
                entity.CODIGO_TIPO_NOMINA = await _repository.GetNextKey();
                entity.DESCRIPCION = dto.Descripcion.ToUpper();
                entity.SIGLAS_TIPO_NOMINA = dto.SiglasTipoNomina.ToUpper();
                entity.FRECUENCIA_PAGO_ID = dto.FrecuenciaPagoId;
                entity.SUELDO_MINIMO = dto.SueldoMinimo;
               

                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;


                var created = await _repository.Add(entity);

                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapTiposNominaDto(created.Data);
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

        public async Task<ResultDto<RhTiposNominaDeleteDto>> Delete(RhTiposNominaDeleteDto dto)
        {

            ResultDto<RhTiposNominaDeleteDto> result = new ResultDto<RhTiposNominaDeleteDto>(null);
            try
            {

                var codigoTipoNomina = await _repository.GetByCodigo(dto.CodigoTipoNomina);
                if (codigoTipoNomina == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Tipo nomina no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoTipoNomina);

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

