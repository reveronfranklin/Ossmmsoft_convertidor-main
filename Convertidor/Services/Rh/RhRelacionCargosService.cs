using System.Globalization;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Data.Repository.Rh;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Rh;
using Convertidor.Services.Rh;

namespace Convertidor.Services.Presupuesto
{
	public class RhRelacionCargosService: IRhRelacionCargosService
    {

        private readonly IRhRelacionCargosRepository _repository;
        private readonly IPRE_RELACION_CARGOSRepository _preRelacionCargosRepository;
        private readonly IRhPersonasRepository _rhPersonasRepository;
        private readonly IPreCargosRepository _preCargoRepository;

        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;
        private readonly IPreDescriptivaRepository _repositoryPreDescriptiva;
        private readonly IPRE_RELACION_CARGOSRepository _preRelacionCargoRepository;
        private readonly IPRE_INDICE_CAT_PRGRepository _preICPRepository;
        private readonly IRhTipoNominaRepository _rhTipoNominaRepository;
        private readonly IRhConceptosRepository _rhConceptosRepository;
        private readonly IRhMovNominaRepository _rhMovNominaRepository;
 
        private readonly IConfiguration _configuration;
        public RhRelacionCargosService(IRhRelacionCargosRepository repository,
                                        IPRE_RELACION_CARGOSRepository preRelacionCargosRepository,
                                        IPreCargosRepository preCargoRepository,
                                        IRhPersonasRepository rhPersonasRepository,
                                        IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository,
                                        IConfiguration configuration,
                                        IPreDescriptivaRepository repositoryPreDescriptiva,
                                        IPRE_INDICE_CAT_PRGRepository preICPRepository,
                                        IPRE_RELACION_CARGOSRepository preRelacionCargoRepository,
                                        IRhTipoNominaRepository rhTipoNominaRepository,
                                        IRhConceptosRepository rhConceptosRepository,
                                        IRhMovNominaRepository rhMovNominaRepository)
		{
            _repository = repository;
            _preCargoRepository = preCargoRepository;
            _rhPersonasRepository = rhPersonasRepository;
            _configuration = configuration;
            _repositoryPreDescriptiva = repositoryPreDescriptiva;
            _preRelacionCargoRepository = preRelacionCargoRepository;
            _preICPRepository = preICPRepository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
            _preRelacionCargosRepository = preRelacionCargosRepository;
            _rhTipoNominaRepository = rhTipoNominaRepository;
            _rhConceptosRepository = rhConceptosRepository;
            _rhMovNominaRepository = rhMovNominaRepository;

        }


        public async Task<ResultDto<List<RhRelacionCargoDto>>> GetAll()
        {

            ResultDto<List<RhRelacionCargoDto>> result = new ResultDto<List<RhRelacionCargoDto>>(null);
            try
            {

                var relacionCargo = await _repository.GetAll();

               

                if (relacionCargo.Count() > 0)
                {
                    List<RhRelacionCargoDto> listDto = new List<RhRelacionCargoDto>();

                    foreach (var item in relacionCargo)
                    {
                        var dto = await MapRhRelacionCargo(item);
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
                    result.Message = " No existen Datos";

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



        public async Task<ResultDto<List<RhRelacionCargoDto>>> GetAllByPreRelacionCargo(int codigoPreRelacionCargo)
        {

            ResultDto<List<RhRelacionCargoDto>> result = new ResultDto<List<RhRelacionCargoDto>>(null);
            try
            {

               

                var rhRelacionCargos = await _repository.GetAllByPreCodigoRelacionCargos(codigoPreRelacionCargo);

               


                if (rhRelacionCargos.Count() > 0)
                {
                    List<RhRelacionCargoDto> listDto = new List<RhRelacionCargoDto>();

                        foreach (var item in rhRelacionCargos)
                    {
                        var dto = await MapRhRelacionCargo(item);
                        listDto.Add(dto);
                    }


                    result.Data = listDto;
                    result.LinkData = "";
                    result.IsValid = true;
                    result.Message = "";
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = " No existen Datos";
                    result.LinkData = "";

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


        public async Task<ResultDto<RhRelacionCargoDto>> Update(RhRelacionCargoUpdateDto dto)
        {

            ResultDto<RhRelacionCargoDto> result = new ResultDto<RhRelacionCargoDto>(null);
            try
            {

                var prerRelacionCargoUpdate = await _preRelacionCargoRepository.GetByCodigo(dto.CodigoRelacionCargoPre);
                if (prerRelacionCargoUpdate == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Pre Relacion Cargo no existe";
                    return result;
                }
                var relacionCargoUpdate = await _repository.GetByCodigo(dto.CodigoRelacionCargo);
                if (relacionCargoUpdate == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Relacion Cargo no existe";
                    return result;
                }
                var persona = await _rhPersonasRepository.GetCodigoPersona(dto.CodigoPersona);
                if (persona == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Persona no existe";
                    return result;
                }

                var cargo = await _preCargoRepository.GetByCodigo(dto.CodigoCargo);
                if (cargo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cargo no existe";
                    return result;
                }

                var tipoNomina = await _rhTipoNominaRepository.GetByCodigo(dto.TipoNomina);
                if (tipoNomina == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo de Nomina no existe";
                    return result;
                }


                if (dto.Sueldo <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Sueldo debe ser Mayor a Cero";
                    return result;
                }

                var codigoIcpActual = relacionCargoUpdate.CODIGO_ICP;
                if (codigoIcpActual != dto.CodigoIcp)
                {
                    var preRelacionCargoFind = await _preRelacionCargoRepository.GetByPresupuestoIcp(relacionCargoUpdate.CODIGO_PRESUPUESTO, dto.CodigoIcp);
                    if (preRelacionCargoFind == null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "No existe Pre Relacion de cargos para este ICP";
                        return result;
                    }
                    else
                    {
                        ResultDto<RhRelacionCargoDto> resultCreated = new ResultDto<RhRelacionCargoDto>(null);
                        if (relacionCargoUpdate.SUELDO != dto.Sueldo)
                        {
                          
                            //relacionCargoUpdate.CODIGO_PERSONA = dto.CodigoPersona;
                            //relacionCargoUpdate.CODIGO_TIPO_NOMINA = dto.TipoNomina;
                            relacionCargoUpdate.FECHA_INI = Convert.ToDateTime(dto.FechaIni, CultureInfo.InvariantCulture);
                            relacionCargoUpdate.FECHA_FIN = Convert.ToDateTime(DateTime.Now.ToString("u"), CultureInfo.InvariantCulture);
                            relacionCargoUpdate.FECHA_UPD = DateTime.Now;
                            await _repository.Update(relacionCargoUpdate);

                            RhRelacionCargoUpdateDto rhRelacionCargoDtoAdd = new RhRelacionCargoUpdateDto();
                            rhRelacionCargoDtoAdd.CodigoIcp = relacionCargoUpdate.CODIGO_ICP;
                            rhRelacionCargoDtoAdd.CodigoRelacionCargoPre = relacionCargoUpdate.CODIGO_RELACION_CARGO;
                            rhRelacionCargoDtoAdd.CodigoCargo = relacionCargoUpdate.CODIGO_CARGO;
                            rhRelacionCargoDtoAdd.CodigoPersona = relacionCargoUpdate.CODIGO_PERSONA;
                            rhRelacionCargoDtoAdd.CodigoRelacionCargo = 0;
                            rhRelacionCargoDtoAdd.TipoNomina = relacionCargoUpdate.CODIGO_TIPO_NOMINA;
                            rhRelacionCargoDtoAdd.FechaIni = relacionCargoUpdate.FECHA_INI.Value.ToString("u");
                            rhRelacionCargoDtoAdd.FechaFin = DateTime.Now.ToString("u");
                            rhRelacionCargoDtoAdd.Sueldo = dto.Sueldo;
                            resultCreated = await Create(rhRelacionCargoDtoAdd);

                            var concepto = await _rhConceptosRepository.GetByExtra1("SUELDO");
                            if (concepto != null)
                            {
                                var rhMovNomina = await _rhMovNominaRepository
                                        .GetByTipoNominaPersonaConcepto(dto.TipoNomina, dto.CodigoPersona, concepto.CODIGO_CONCEPTO);
                                if (rhMovNomina != null)
                                {
                                    rhMovNomina.MONTO = dto.Sueldo;
                                    await _rhMovNominaRepository.Update(rhMovNomina);

                                }
                            }
                        }
                        else
                        {
                           // relacionCargoUpdate.CODIGO_PERSONA = dto.CodigoPersona;
                           // relacionCargoUpdate.CODIGO_TIPO_NOMINA = dto.TipoNomina;
                            relacionCargoUpdate.FECHA_INI = Convert.ToDateTime(dto.FechaIni, CultureInfo.InvariantCulture);
                            relacionCargoUpdate.FECHA_FIN = Convert.ToDateTime(DateTime.Now.ToString("u"), CultureInfo.InvariantCulture);
                            relacionCargoUpdate.FECHA_UPD = DateTime.Now;
                            await _repository.Update(relacionCargoUpdate);
                            RhRelacionCargoUpdateDto rhRelacionCargoDto = new RhRelacionCargoUpdateDto();
                            rhRelacionCargoDto.CodigoIcp = preRelacionCargoFind.CODIGO_ICP;
                            rhRelacionCargoDto.CodigoRelacionCargoPre = preRelacionCargoFind.CODIGO_RELACION_CARGO;
                            rhRelacionCargoDto.CodigoCargo = relacionCargoUpdate.CODIGO_CARGO;
                            rhRelacionCargoDto.CodigoPersona = relacionCargoUpdate.CODIGO_PERSONA;
                            rhRelacionCargoDto.CodigoRelacionCargo = 0;
                            rhRelacionCargoDto.TipoNomina = relacionCargoUpdate.CODIGO_TIPO_NOMINA;
                            rhRelacionCargoDto.FechaIni = relacionCargoUpdate.FECHA_INI.Value.ToString("u");
                            rhRelacionCargoDto.Sueldo = dto.Sueldo;
                            resultCreated = await Create(rhRelacionCargoDto);
                            var concepto = await _rhConceptosRepository.GetByExtra1("SUELDO");
                            if (concepto != null)
                            {
                                var rhMovNomina = await _rhMovNominaRepository
                                        .GetByTipoNominaPersonaConcepto(dto.TipoNomina, dto.CodigoPersona, concepto.CODIGO_CONCEPTO);
                                if (rhMovNomina != null)
                                {
                                    rhMovNomina.MONTO = dto.Sueldo;
                                    await _rhMovNominaRepository.Update(rhMovNomina);

                                }
                            }
                        }
                      

                      
                      
                        result.Data = resultCreated.Data;
                        result.IsValid = true;
                        result.Message = "";

                        return result;

                    }
                }

                var sueldoActual = relacionCargoUpdate.SUELDO;
                if (sueldoActual != dto.Sueldo) {

                    //relacionCargoUpdate.CODIGO_PERSONA = dto.CodigoPersona;
                    //relacionCargoUpdate.CODIGO_TIPO_NOMINA = dto.TipoNomina;
                    relacionCargoUpdate.FECHA_INI = Convert.ToDateTime(dto.FechaIni, CultureInfo.InvariantCulture);
                    relacionCargoUpdate.FECHA_FIN = Convert.ToDateTime(DateTime.Now.ToString("u"), CultureInfo.InvariantCulture);
                    relacionCargoUpdate.FECHA_UPD = DateTime.Now;
                    await _repository.Update(relacionCargoUpdate);

                    RhRelacionCargoUpdateDto rhRelacionCargoDto = new RhRelacionCargoUpdateDto();
                    rhRelacionCargoDto.CodigoCargo = relacionCargoUpdate.CODIGO_CARGO;
                    rhRelacionCargoDto.CodigoPersona = relacionCargoUpdate.CODIGO_PERSONA;
                    rhRelacionCargoDto.CodigoRelacionCargoPre = relacionCargoUpdate.CODIGO_RELACION_CARGO;
                    rhRelacionCargoDto.CodigoRelacionCargo = 0;
                    rhRelacionCargoDto.TipoNomina = relacionCargoUpdate.CODIGO_TIPO_NOMINA;
                    rhRelacionCargoDto.FechaIni = relacionCargoUpdate.FECHA_INI.Value.ToString("u");
                    rhRelacionCargoDto.Sueldo = dto.Sueldo;
                    var resultCreated=await Create(rhRelacionCargoDto);

                    var conceptoSueldo = await _rhConceptosRepository.GetByExtra1("SUELDO");
                    if (conceptoSueldo != null)
                    {
                        var rhMovNomina = await _rhMovNominaRepository
                                .GetByTipoNominaPersonaConcepto(dto.TipoNomina, dto.CodigoPersona, conceptoSueldo.CODIGO_CONCEPTO);
                        if (rhMovNomina != null)
                        {
                            rhMovNomina.MONTO = dto.Sueldo;
                            await _rhMovNominaRepository.Update(rhMovNomina);

                        }
                    }

                    result.Data = resultCreated.Data;
                    result.IsValid = true;
                    result.Message = "";

                    return result;

                }

                //TODO Estos campos se actualizaran desde el proceso de promocion

                //relacionCargoUpdate.CODIGO_PERSONA = dto.CodigoPersona;
                //relacionCargoUpdate.CODIGO_TIPO_NOMINA = dto.TipoNomina;
                relacionCargoUpdate.FECHA_INI = Convert.ToDateTime(dto.FechaIni, CultureInfo.InvariantCulture);
                
                if (dto.FechaFin == "")
                {
                    relacionCargoUpdate.FECHA_FIN = default(DateTime?);
                }
                else
                {
                    relacionCargoUpdate.FECHA_FIN = Convert.ToDateTime(dto.FechaFin, CultureInfo.InvariantCulture);
                }
             
                if (relacionCargoUpdate.FECHA_INI.Value.Year <= 1900) relacionCargoUpdate.FECHA_INI = default(DateTime?);
                if (relacionCargoUpdate.FECHA_FIN != null && relacionCargoUpdate.FECHA_FIN.Value.Year <= 1900) relacionCargoUpdate.FECHA_FIN = default(DateTime?);

                relacionCargoUpdate.FECHA_UPD = DateTime.Now;
                await _repository.Update(relacionCargoUpdate);

              

              

                var resultDto = await MapRhRelacionCargo(relacionCargoUpdate);
                result.Data = resultDto;
                result.IsValid = true;
                result.Message = "";

                return result;

            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;

                return result;
            }



        }
        public async Task<ResultDto<RhRelacionCargoDto>> UpdateField(UpdateFieldDto dto)
        {

            ResultDto<RhRelacionCargoDto> result = new ResultDto<RhRelacionCargoDto>(null);
            try
            {
                CultureInfo cultures = new CultureInfo("en-US");


                var relacionCargoUpdate = await _repository.GetByCodigo(dto.Id);
                if (relacionCargoUpdate == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Relacion Cargo no existe";
                    return result;
                }
                var sueldoActual = relacionCargoUpdate.SUELDO;
                decimal valor;
                decimal sueldo = 0;

                if (Decimal.TryParse(dto.Value, out valor))
                {
                    
                    decimal val = Convert.ToDecimal(dto.Value, cultures);
                    if (dto.Field == "sueldo")
                    {
                        sueldo = val;
                    }
              


                }
                else
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Valor invalido";
                    return result;
                }


                decimal valorDto = Convert.ToDecimal(dto.Value, cultures);
                if (sueldoActual != valorDto && dto.Field=="sueldo") relacionCargoUpdate.FECHA_FIN = DateTime.Now;


                relacionCargoUpdate.FECHA_UPD = DateTime.Now;


                await _repository.Update(relacionCargoUpdate);
                if (sueldoActual != valorDto && dto.Field == "sueldo")
                {
                    RhRelacionCargoUpdateDto rhRelacionCargoDto = new RhRelacionCargoUpdateDto();
                    rhRelacionCargoDto.CodigoCargo = relacionCargoUpdate.CODIGO_CARGO;
                    rhRelacionCargoDto.CodigoPersona = relacionCargoUpdate.CODIGO_PERSONA;
                    rhRelacionCargoDto.CodigoRelacionCargoPre = relacionCargoUpdate.CODIGO_RELACION_CARGO;
                    rhRelacionCargoDto.CodigoRelacionCargo = 0;
                    rhRelacionCargoDto.TipoNomina = relacionCargoUpdate.CODIGO_TIPO_NOMINA;
                    rhRelacionCargoDto.FechaIni = relacionCargoUpdate.FECHA_INI.Value.ToString("u");
                    rhRelacionCargoDto.Sueldo = valorDto;
                    await Create(rhRelacionCargoDto);

                    var conceptoSueldo = await _rhConceptosRepository.GetByExtra1("SUELDO");
                    if (conceptoSueldo != null)
                    {
                        var rhMovNomina = await _rhMovNominaRepository
                                .GetByTipoNominaPersonaConcepto(rhRelacionCargoDto.TipoNomina, rhRelacionCargoDto.CodigoPersona, conceptoSueldo.CODIGO_CONCEPTO);
                        if (rhMovNomina != null)
                        {
                            rhMovNomina.MONTO = rhRelacionCargoDto.Sueldo;
                            await _rhMovNominaRepository.Update(rhMovNomina);

                        }
                    }
                }
                 



                var resultDto =await  MapRhRelacionCargo(relacionCargoUpdate);
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

        public async Task<ResultDto<RhRelacionCargoDto>> Create(RhRelacionCargoUpdateDto dto)
        {

            ResultDto<RhRelacionCargoDto> result = new ResultDto<RhRelacionCargoDto>(null);
            try
            {

                var preRelacionCargo = await _preRelacionCargoRepository.GetByCodigo(dto.CodigoRelacionCargoPre);
                if (preRelacionCargo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"No existe la relacion de cargo en presupuesto ";
                    return result;
                }
                var persona = await _rhPersonasRepository.GetCodigoPersona(dto.CodigoPersona);
                if (persona == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Persona no existe";
                    return result;
                }

             

                if (dto.Sueldo <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Sueldo debe ser Mayor a Cero";
                    return result;
                }

                var tipoNomina = await _rhTipoNominaRepository.GetByCodigo(dto.TipoNomina);
                if (tipoNomina == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Tipo de Nomina no existe";
                    return result;
                }


                RH_RELACION_CARGOS entity = new RH_RELACION_CARGOS();
                entity.CODIGO_RELACION_CARGO = await _repository.GetNextKey();
                entity.CODIGO_RELACION_CARGO_PRE = preRelacionCargo.CODIGO_RELACION_CARGO;
                entity.CODIGO_CARGO = preRelacionCargo.CODIGO_CARGO;
                entity.CODIGO_ICP = preRelacionCargo.CODIGO_ICP;  
                entity.CODIGO_PRESUPUESTO = preRelacionCargo.CODIGO_PRESUPUESTO;
                entity.CODIGO_EMPRESA = preRelacionCargo.CODIGO_EMPRESA;
                entity.CODIGO_TIPO_NOMINA = dto.TipoNomina;
                entity.SUELDO = dto.Sueldo;
                entity.CODIGO_PERSONA = dto.CodigoPersona;
                entity.EXTRA1 ="";
                entity.EXTRA2 ="";
                entity.EXTRA3 = "";
                entity.FECHA_FIN = Convert.ToDateTime(dto.FechaFin, CultureInfo.InvariantCulture);
                entity.FECHA_INI = Convert.ToDateTime(dto.FechaIni, CultureInfo.InvariantCulture);
                if (entity.FECHA_INI.Value.Year <= 1900) entity.FECHA_INI = null;
                if (entity.FECHA_FIN.Value.Year <= 1900) entity.FECHA_FIN = null;
                entity.FECHA_UPD = DateTime.Now;
                entity.FECHA_INS = DateTime.Now;
             
                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapRhRelacionCargo(created.Data);
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

        public FechaDto GetFechaDto(DateTime? fecha)
        {
            var FechaDesdeObj = new FechaDto();
            try
            {
                if (fecha != null)
                {
                    FechaDesdeObj.Year = fecha.Value.Year.ToString();
                    string month = "00" + fecha.Value.Month.ToString();
                    string day = "00" + fecha.Value.Day.ToString();
                    FechaDesdeObj.Month = month.Substring(month.Length - 2);
                    FechaDesdeObj.Day = day.Substring(month.Length - 2);
                }
                else
                {
                    FechaDesdeObj.Year = "1900";
                    FechaDesdeObj.Month = "01";
                    FechaDesdeObj.Day = "01";
                }
                return FechaDesdeObj;
            }
            catch (Exception ex)
            {
                FechaDesdeObj.Year = "1900";
                FechaDesdeObj.Month = "01";
                FechaDesdeObj.Day = "01";
                return FechaDesdeObj;
            }
         

           
        }

        public async Task<RhRelacionCargoDto> MapRhRelacionCargo(RH_RELACION_CARGOS item)
        {
            
           
            
            RhRelacionCargoDto dto = new RhRelacionCargoDto();
            dto.CodigoRelacionCargo = item.CODIGO_RELACION_CARGO;
            dto.CodigoRelacionCargoPre = item.CODIGO_RELACION_CARGO_PRE;
            dto.TipoNomina = item.CODIGO_TIPO_NOMINA;
            dto.CodigoIcp = item.CODIGO_ICP;
            dto.CodigoCargo = item.CODIGO_CARGO;
            dto.DenominacionCargo = "";
            dto.Sueldo = item.SUELDO;
            dto.FechaIni = (DateTime)item.FECHA_INI;
            if (item.FECHA_FIN == null)
            {
                item.FECHA_FIN = DateTime.MinValue;
            }
            dto.FechaFin = (DateTime)item.FECHA_FIN;
            
            
            
            dto.FechaIniString = item.FECHA_INI.Value.ToString("u");
            dto.FechaFinString = item.FECHA_FIN.Value.ToString("u");
            FechaDto FechaIniObj = GetFechaDto(item.FECHA_INI);
            dto.FechaIniObj = (FechaDto)FechaIniObj;
            FechaDto FechaFinObj = GetFechaDto(item.FECHA_FIN);
            dto.FechaFinObj = (FechaDto)FechaFinObj;

            var cargo = await _preCargoRepository.GetByCodigo(dto.CodigoCargo);
            if (cargo != null)
            {
                dto.DenominacionCargo = cargo.DENOMINACION;

            }
            dto.CodigoPersona = item.CODIGO_PERSONA;
            var persona = await _rhPersonasRepository.GetCodigoPersona(item.CODIGO_PERSONA);
            if (persona != null)
            {
                dto.Nombre = persona.NOMBRE;
                dto.Apellido = persona.APELLIDO;
                dto.Cedula = persona.CEDULA;

               
            }

            return dto;

        }


        public async Task<ResultDto<RhRelacionCargoDeleteDto>> Delete(RhRelacionCargoDeleteDto dto)
        {

            ResultDto<RhRelacionCargoDeleteDto> result = new ResultDto<RhRelacionCargoDeleteDto>(null);
            try
            {

                var relacionCargo = await _repository.GetByCodigo(dto.CodigoRelacionCargo);
                if (relacionCargo == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Relacion Cargo no existe";
                    return result;
                }

               //TODO VALIDAR CONTRA RH_RELACION_CARGO

                var deleted = await _repository.Delete(dto.CodigoRelacionCargo);

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

