namespace Convertidor.Data.Repository.Rh
{
    public class RhConceptosService : IRhConceptosService
    {

        private readonly DataContext _context;

        private readonly IRhConceptosRepository _repository;
        private readonly IRhDescriptivasService _descriptivaService;
        private readonly IRhPersonasRepository _rhPersonasRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IPrePlanUnicoCuentasService _prePlanUnicoCuentasService;
        private readonly IRhTipoNominaRepository _rhTipoNominaRepository;

        private readonly IMapper _mapper;

        public RhConceptosService(IRhConceptosRepository repository,
                          IRhDescriptivasService descriptivaService,
                          IRhPersonasRepository rhPersonasRepository,
                          ISisUsuarioRepository sisUsuarioRepository,
                          IPrePlanUnicoCuentasService prePlanUnicoCuentasService,
                          IRhTipoNominaRepository rhTipoNominaRepository)
        {
            _repository = repository;
            _descriptivaService = descriptivaService;
            _rhPersonasRepository = rhPersonasRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _prePlanUnicoCuentasService = prePlanUnicoCuentasService;
            _rhTipoNominaRepository = rhTipoNominaRepository;
        }

        public async Task<List<RhConceptosResponseDto>> GetAll()
        {
            try
            {
                var conceptos = await _repository.GetAll();

                var result = await MapListConceptosDto(conceptos);


                return (List<RhConceptosResponseDto>)result;
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

        public async Task<RhConceptosResponseDto> GetByTipoNominaConcepto(int codigoTipoNomina, int codigoConcepto)
        {
            try
            {
                var conceptos = await _repository.GetByCodigoTipoNomina(codigoConcepto, codigoTipoNomina);


                var result = await MapConceptosDto(conceptos);


                return (RhConceptosResponseDto)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<RhConceptosResponseDto>> GetByTipoNomina(int codigoTipoNomina)
        {
            string descripcon = "";
            try
            {
                var conceptos = await _repository.GeTByTipoNomina(codigoTipoNomina);
                var tipoNomina = await _rhTipoNominaRepository.GetByCodigo(codigoTipoNomina);
                if (tipoNomina != null)
                {
                    descripcon = tipoNomina.DESCRIPCION;
                }
                var result = await MapListConceptosDto(conceptos, descripcon);


                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<RhConceptosResponseDto>> GetConceptosByCodigoPersona(int codigoPersona, DateTime desde, DateTime hasta)
        {
            try
            {
                var conceptos = await _repository.GetConceptosByCodigoPersona(codigoPersona, desde, hasta);

                var result = await MapListConceptosDto(conceptos);


                return (List<RhConceptosResponseDto>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        /*public static explicit operator CreateOrderDto(
            CreateOrderViewModel model) =>
            new CreateOrderDto(
                model.CustomerId, model.ShipAddress, model.ShipCity,
                model.ShipCountry, model.ShipPostalCode,
                model.OrderDetails.Select(d => new CreateOrderDetailDto(
                    d.ProductId, d.UnitPrice, d.Quantity)));

        }*/
        public async Task<RhConceptosResponseDto> MapConceptosDto(RH_CONCEPTOS dtos, string tipoNominaDescripcion = "")
        {


            RhConceptosResponseDto itemResult = new RhConceptosResponseDto();
            itemResult.CodigoConcepto = dtos.CODIGO_CONCEPTO;
            itemResult.Codigo = dtos.CODIGO;
            itemResult.CodigoTipoNomina = dtos.CODIGO_TIPO_NOMINA;
            itemResult.TipoNominaDescripcion = "";
            if (tipoNominaDescripcion.Length > 0)
            {
                itemResult.TipoNominaDescripcion = tipoNominaDescripcion;
            }
            else
            {
                var tipoNomina = await _rhTipoNominaRepository.GetByCodigo(dtos.CODIGO_TIPO_NOMINA);
                if (tipoNomina != null)
                {
                    itemResult.TipoNominaDescripcion = tipoNomina.DESCRIPCION;
                }
            }

            if (dtos.DENOMINACION == null) dtos.DENOMINACION = "";
            itemResult.Denominacion = dtos.DENOMINACION;
            if (dtos.DESCRIPCION == null) dtos.DESCRIPCION = "";
            itemResult.Descripcion = dtos.DESCRIPCION;
            itemResult.TipoConcepto = dtos.TIPO_CONCEPTO;
            itemResult.ModuloId = dtos.MODULO_ID;
            itemResult.ModuloDescripcion = "";
            if (dtos.MODULO_ID > 0)
            {
                var desriptivaModulo = await _descriptivaService.GetDescripcionByCodigoDescriptiva(dtos.MODULO_ID);
                itemResult.ModuloDescripcion = desriptivaModulo;
            }

            itemResult.CodigoPucConcat = "";
            itemResult.CodigoPuc = dtos.CODIGO_PUC;
            if (dtos.CODIGO_PUC > 0)
            {
                var puc = await _prePlanUnicoCuentasService.GetById(dtos.CODIGO_PUC);
                if (puc.Data != null)
                {
                    itemResult.CodigoPucConcat = puc.Data.CodigoPucConcat;
                }
            }

            itemResult.Status = dtos.STATUS;
            itemResult.FrecuenciaId = dtos.FRECUENCIA_ID;
            var desrcriptivaFrecuencia = await _descriptivaService.GetDescripcionByCodigoDescriptiva(dtos.FRECUENCIA_ID);
            itemResult.FrecuenciaDescripcion = desrcriptivaFrecuencia;
            itemResult.Dedusible = dtos.DEDUSIBLE;
            itemResult.Automatico = dtos.AUTOMATICO;
            if (dtos.ID_MODELO_CALCULO == null) dtos.ID_MODELO_CALCULO = 0;
            itemResult.IdModeloCalculo = dtos.ID_MODELO_CALCULO;
            if (dtos.EXTRA1 == null) dtos.EXTRA1 = "";
            itemResult.Extra1 = dtos.EXTRA1;


            return itemResult;

        }


        public async Task<List<RhConceptosResponseDto>> MapListConceptosDto(List<RH_CONCEPTOS> dtos, string tipoNominaDescripcion = "")
        {
            List<RhConceptosResponseDto> result = new List<RhConceptosResponseDto>();

            foreach (var item in dtos)
            {
                var itemResult = await MapConceptosDto(item, tipoNominaDescripcion);
                result.Add(itemResult);
            }
            return result;
        }

        public List<string> GetListTipoConcepto()
        {
            List<string> result = new List<string>();
            result.Add("A");
            result.Add("D");
            return result;
        }
        public List<string> GetListStatus()
        {
            List<string> result = new List<string>();
            result.Add("A");
            result.Add("I");
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

                var tipoNomina = await _rhTipoNominaRepository.GetByCodigo(dto.CodigoTipoNomina);
                if (tipoNomina == null)
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

                var tipoConcepto = GetListTipoConcepto().Where(x => x == dto.TipoConcepto).FirstOrDefault();
                if (String.IsNullOrEmpty(tipoConcepto))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Concepto Invalido";
                    return result;

                }


                if (dto.ModuloId > 0)
                {
                    var moduloId = await _descriptivaService.GetByCodigoDescriptiva(dto.ModuloId);
                    if (moduloId == null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Modulo Invalido";
                        return result;
                    }
                }

                if (dto.CodigoPuc > 0)
                {
                    var puc = _prePlanUnicoCuentasService.GetById(dto.CodigoPuc);
                    if (puc == null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "PUC Invalido";
                        return result;
                    }

                }

                var status = GetListStatus().Where(x => x == dto.Status).FirstOrDefault();
                if (String.IsNullOrEmpty(status))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estatus Invalido";
                    return result;

                }


                var frecuenciaId = await _descriptivaService.GetByCodigoDescriptiva(dto.FrecuenciaId);
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
                entity.FRECUENCIA_ID = dto.FrecuenciaId;
                entity.DEDUSIBLE = dto.Dedusible;
                entity.AUTOMATICO = dto.Automatico;
                if (dto.IdModeloCalculo == null) dto.IdModeloCalculo = 0;
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
                    result.Message = "Concepto  Invalido";
                    return result;
                }


                var codigo = await _repository.GetCodigoString(dto.Codigo);
                if (codigo is null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo Concepto  Invalido";
                    return result;
                }

                var tipoNomina = await _rhTipoNominaRepository.GetByCodigo(dto.CodigoTipoNomina);
                if (tipoNomina == null)
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

                var tipoConcepto = GetListTipoConcepto().Where(x => x == dto.TipoConcepto).FirstOrDefault();
                if (String.IsNullOrEmpty(tipoConcepto))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo Concepto Invalido";
                    return result;

                }


                if (dto.ModuloId > 0)
                {
                    var moduloId = await _descriptivaService.GetByCodigoDescriptiva(dto.ModuloId);
                    if (moduloId == null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Modulo Invalido";
                        return result;
                    }
                }

                if (dto.CodigoPuc > 0)
                {
                    var puc = _prePlanUnicoCuentasService.GetById(dto.CodigoPuc);
                    if (puc == null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "PUC Invalido";
                        return result;
                    }

                }

                var status = GetListStatus().Where(x => x == dto.Status).FirstOrDefault();
                if (String.IsNullOrEmpty(status))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Estatus Invalido";
                    return result;

                }



                var frecuenciaId = await _descriptivaService.GetByCodigoDescriptiva(dto.FrecuenciaId);
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


    }
}