using System;
using System.Collections.Generic;
using System.Globalization;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Data.Repository.Rh;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Rh;
using Convertidor.Services.Rh;
using Ganss.Excel;
using NuGet.Packaging;

namespace Convertidor.Services.Presupuesto
{
	public class RhRelacionCargosService: IRhRelacionCargosService
    {

        private readonly IRhRelacionCargosRepository _repository;
        private readonly IPRE_RELACION_CARGOSRepository _preRelacionCargosRepository;
        private readonly IRhPersonasRepository _rhPersonasRepository;
        private readonly IPreCargosRepository _preCargoRepository;

        private readonly IPRE_PRESUPUESTOSRepository _prePresupuestoRepository;
        private readonly IPreDescriptivaRepository _repositoryPreDescriptiva;
        private readonly IPRE_RELACION_CARGOSRepository _preRelacionCargoRepository;
        private readonly IPRE_INDICE_CAT_PRGRepository _preICPRepository;
        private readonly IRhTipoNominaRepository _rhTipoNominaRepository;
 
        private readonly IConfiguration _configuration;
        public RhRelacionCargosService(IRhRelacionCargosRepository repository,
                                        IPRE_RELACION_CARGOSRepository preRelacionCargosRepository,
                                        IPreCargosRepository preCargoRepository,
                                        IRhPersonasRepository rhPersonasRepository,
                                        IPRE_PRESUPUESTOSRepository prePresupuestoRepository,
                                      IConfiguration configuration,
                                      IPreDescriptivaRepository repositoryPreDescriptiva,
                                      IPRE_INDICE_CAT_PRGRepository preICPRepository,
                                      IPRE_RELACION_CARGOSRepository preRelacionCargoRepository,
                                      IRhTipoNominaRepository rhTipoNominaRepository)
		{
            _repository = repository;
            _preCargoRepository = preCargoRepository;
            _rhPersonasRepository = rhPersonasRepository;
            _configuration = configuration;
            _repositoryPreDescriptiva = repositoryPreDescriptiva;
            _preRelacionCargoRepository = preRelacionCargoRepository;
            _preICPRepository = preICPRepository;
            _prePresupuestoRepository = prePresupuestoRepository;
            _preRelacionCargosRepository = preRelacionCargosRepository;
            _rhTipoNominaRepository = rhTipoNominaRepository;

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
                var persona = await _rhPersonasRepository.GetCodogoPersona(dto.CodigoPersona);
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

            
                relacionCargoUpdate.CODIGO_PERSONA = dto.CodigoPersona;
                relacionCargoUpdate.CODIGO_TIPO_NOMINA = dto.TipoNomina;
                relacionCargoUpdate.SUELDO = dto.Sueldo;
                relacionCargoUpdate.FECHA_INI = Convert.ToDateTime(dto.FechaIni, CultureInfo.InvariantCulture);
                relacionCargoUpdate.FECHA_FIN = Convert.ToDateTime(dto.FechaFin, CultureInfo.InvariantCulture);     
                relacionCargoUpdate.FECHA_UPD = DateTime.Now;
                await _repository.Update(relacionCargoUpdate);

                var resultDto = await MapRhRelacionCargo(relacionCargoUpdate);
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
                decimal valor;
                if (Decimal.TryParse(dto.Value, out valor))
                {
                    
                    decimal val = Convert.ToDecimal(dto.Value, cultures);
                    if (dto.Field == "sueldo")
                    {
                        relacionCargoUpdate.SUELDO = val;
                    }
              


                }
                else
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Valor invalido";
                    return result;
                }


               

     
                relacionCargoUpdate.FECHA_UPD = DateTime.Now;


                await _repository.Update(relacionCargoUpdate);

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
                var persona = await _rhPersonasRepository.GetCodogoPersona(dto.CodigoPersona);
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
        public FechaDto GetFechaDto(DateTime fecha)
        {
            var FechaDesdeObj = new FechaDto();
            FechaDesdeObj.Year = fecha.Year.ToString();
            string month = "00" + fecha.Month.ToString();
            string day = "00" + fecha.Day.ToString();
            FechaDesdeObj.Month = month.Substring(month.Length - 2);
            FechaDesdeObj.Day = day.Substring(month.Length - 2);

            return FechaDesdeObj;
        }

        public async Task<RhRelacionCargoDto> MapRhRelacionCargo(RH_RELACION_CARGOS item)
        {
            RhRelacionCargoDto dto = new RhRelacionCargoDto();
            dto.CodigoRelacionCargo = item.CODIGO_RELACION_CARGO;
            dto.CodigoRelacionCargoPre = item.CODIGO_RELACION_CARGO_PRE;
            dto.TipoNomina = item.CODIGO_TIPO_NOMINA;
            dto.CodigoCargo = item.CODIGO_CARGO;
            dto.DenominacionCargo = "";
            dto.Sueldo = item.SUELDO;
            dto.FechaIni =item.FECHA_INI.ToString("u"); ;
            dto.FechaFin = item.FECHA_FIN.ToString("u"); ;
    

        
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
            var persona = await _rhPersonasRepository.GetCodogoPersona(item.CODIGO_PERSONA);
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

