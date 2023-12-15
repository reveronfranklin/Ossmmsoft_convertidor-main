using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AutoMapper;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Rh;
using Convertidor.Services.Rh;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Microsoft.Extensions.Caching.Distributed;

namespace Convertidor.Data.Repository.Rh
{
	public class SeedDb
    {
        

   
        private readonly IRhPersonasRepository _repository;
        private readonly IRhHistoricoMovimientoService _historicoMovimientoService;
        private readonly IRhEducacionService _rhEducacionService;
        private readonly IRhDireccionesService _rhDireccionesService;
        private readonly IRhConceptosService _rhConceptosService;
        private readonly ISisUbicacionNacionalRepository _sisUbicacionNacionalRepository;
        private readonly IRhComunicacionesRepository _rhComunicacionessRepository;
        private readonly IRH_HISTORICO_PERSONAL_CARGORepository _rhHistoricoPersonalCargorepository;
        private readonly IRhDescriptivasService _rhDescriptivasServices;
        private readonly IRhAdministrativosService _rhAdministrativosServices;
        private readonly IRhRelacionCargosRepository _rhRelacionCargosRepository;
        private readonly IPreCargosRepository _preCargosRepository;
        private readonly IPRE_INDICE_CAT_PRGRepository _preIndiceCatPrg;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IDistributedCache _distributedCache;

        public SeedDb(IRhPersonasRepository repository,
                                IRhHistoricoMovimientoService historicoMovimientoService,
                                IRhEducacionService rhEducacionService,
                                IRhDireccionesService rhDireccionesService,
                                IRhConceptosService rhConceptosService,
                                ISisUbicacionNacionalRepository sisUbicacionNacionalRepository,
                                IRhComunicacionesRepository rhComunicacionessRepository,
                                IRH_HISTORICO_PERSONAL_CARGORepository rhHistoricoPersonalCargorepository,
                                IRhDescriptivasService rhDescriptivasServices,
                                 IRhAdministrativosService rhAdministrativosServices,
                                IRhRelacionCargosRepository rhRelacionCargosRepository, 
                                IPreCargosRepository preCargosRepository,
                                IPRE_INDICE_CAT_PRGRepository preIndiceCatPrg, 
                                ISisUsuarioRepository sisUsuarioRepository ,
                                    IDistributedCache distributedCache)
        {
            
            _repository = repository;
            _historicoMovimientoService = historicoMovimientoService;
            _rhEducacionService= rhEducacionService;
            _rhDireccionesService = rhDireccionesService;
            _rhConceptosService = rhConceptosService;
            _sisUbicacionNacionalRepository = sisUbicacionNacionalRepository;
            _rhComunicacionessRepository = rhComunicacionessRepository;
            _rhHistoricoPersonalCargorepository = rhHistoricoPersonalCargorepository;
            _rhDescriptivasServices = rhDescriptivasServices;
            _rhAdministrativosServices = rhAdministrativosServices;
            _rhRelacionCargosRepository = rhRelacionCargosRepository;
            _preCargosRepository = preCargosRepository;
            _preIndiceCatPrg = preIndiceCatPrg;
            _sisUsuarioRepository = sisUsuarioRepository;
            _distributedCache = distributedCache;
        }
       
        
     
        public async  Task<ResultDto<List<ListSimplePersonaDto>>> GetAll()
        {
            ResultDto<List<ListSimplePersonaDto>> result = new ResultDto<List<ListSimplePersonaDto>>(null);
            try
            {
                var cacheKey = "GetAllListSimplePersonaDto";
                List<RH_PERSONAS> personas = new List<RH_PERSONAS>();
                List<ListSimplePersonaDto> resultData = new List<ListSimplePersonaDto>();
                //personas = await _repository.GetAll();

                // =await  MapListSimplePersonasDto(personas);
                var listPersonas= await _distributedCache.GetAsync(cacheKey);
                if (listPersonas != null)
                {
                    resultData = System.Text.Json.JsonSerializer.Deserialize<List<ListSimplePersonaDto>> (listPersonas);
                    result.Data =resultData;

                    result.IsValid = true;
                    result.Message = "";
                    return result;
                }
                else
                {
                    personas = await _repository.GetAll();

                    resultData =await  MapListSimplePersonasDto(personas);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddDays(20))
                        .SetSlidingExpiration(TimeSpan.FromDays(1));
                   var serializedList = System.Text.Json.JsonSerializer.Serialize(resultData);
                   var redisListBytes = Encoding.UTF8.GetBytes(serializedList);
                    await _distributedCache.SetAsync(cacheKey,redisListBytes,options);
                    result.Data =resultData;

                    result.IsValid = true;
                    result.Message = "";
                    return result;
                }

               
            }
            catch (Exception ex)
            {
                result.Data =null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }

        }


        public async Task<ResultDto<List<ListSimplePersonaDto>>> GetAllSimple()
        {
            ResultDto<List<ListSimplePersonaDto>> result = new ResultDto<List<ListSimplePersonaDto>>(null);

            try
            {
                
                var cacheKey = "GetAllSimpleListSimplePersonaDto";
                List<RH_PERSONAS> personas = new List<RH_PERSONAS>();
                List<ListSimplePersonaDto> resultData = new List<ListSimplePersonaDto>();
                
                //personas = await _repository.GetAll();

                //result =await MapListSimplePersonasDto(personas);
                
               
                var listPersonas= await _distributedCache.GetAsync(cacheKey);
                
                if (listPersonas != null)
                {
                    resultData = System.Text.Json.JsonSerializer.Deserialize<List<ListSimplePersonaDto>> (listPersonas);
                    result.Data =resultData;

                    result.IsValid = true;
                    result.Message = "";
                    return result;
                }
                else
                {
                    personas = await _repository.GetAll();

                    resultData =await MapListSimplePersonasDto(personas);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(20))
                        .SetSlidingExpiration(TimeSpan.FromDays(1));
                    var serializedList = System.Text.Json.JsonSerializer.Serialize(resultData);
                    var redisListBytes = Encoding.UTF8.GetBytes(serializedList);
                    await _distributedCache.SetAsync(cacheKey,redisListBytes,options);
                    result.Data =resultData;
                    result.IsValid = true;
                    result.Message = "";
                    return result;
                }

              
            }
            catch (Exception ex)
            {
                result.Data =null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
              
            }

        }
      
        public async Task<ListPersonasDto> MapPersonasDto(RH_PERSONAS dtos)
        {
           

                ListPersonasDto itemResult = new ListPersonasDto();



                itemResult.CodigoPersona = dtos.CODIGO_PERSONA;
                itemResult.Cedula = dtos.CEDULA;
                itemResult.Nombre = dtos.NOMBRE;
                itemResult.Apellido = dtos.APELLIDO;
                itemResult.Nacionalidad = dtos.NACIONALIDAD;
                itemResult.Sexo = dtos.SEXO;
                itemResult.FechaNacimiento = dtos.FECHA_NACIMIENTO.ToShortDateString();
                itemResult.PaisNacimientoId = dtos.PAIS_NACIMIENTO_ID;
                itemResult.EstadoNacimientoId = dtos.ESTADO_NACIMIENTO_ID;
                itemResult.NumeroGacetaNacional = dtos.NUMERO_GACETA_NACIONAL;
                itemResult.EstadoCivilId = dtos.ESTADO_CIVIL_ID;
                itemResult.Estatura = dtos.ESTATURA;
                itemResult.Peso = dtos.PESO;
                if (string.IsNullOrEmpty(dtos.MANO_HABIL))
                {
                    dtos.MANO_HABIL = "D";
                }
                itemResult.ManoHabil = dtos.MANO_HABIL;
                itemResult.Extra1 = dtos.EXTRA1;
                itemResult.Extra2 = dtos.EXTRA2;
                itemResult.Extra3 = dtos.EXTRA3;
                itemResult.Status = dtos.STATUS;
                itemResult.IdentificacionId = dtos.IDENTIFICACION_ID;
                itemResult.NumeroIdentificacion = dtos.NUMERO_IDENTIFICACION;
                var educacion = await _rhEducacionService.GetByCodigoPersona(dtos.CODIGO_PERSONA);
                if (educacion != null)
                {
                    itemResult.EducacionDto = educacion;
                }

                var direcciones = await _rhDireccionesService.GetByCodigoPersona(dtos.CODIGO_PERSONA);
                if (direcciones != null)
                {
                    itemResult.DireccionesDto = direcciones;
                }
                var historico = await _historicoMovimientoService.GetByCodigoPersona(dtos.CODIGO_PERSONA);
                if(historico!= null)
                {
                    itemResult.HistoricoMovimientoDto = historico;
                }
                var desde = DateTime.Now;
                
                desde = await FechaIngresoTrabajador(dtos.CODIGO_PERSONA);
                var hasta =DateTime.Now;
                var tiempoServicio = TiempoServicio(desde, hasta);
                itemResult.TiempoServicio = tiempoServicio;

            return itemResult;


        }
        public FechaDto GetFechaDto(DateTime fecha)
        {
            var FechaDesdeObj = new FechaDto();
            FechaDesdeObj.Year = fecha.Year.ToString();
            string month = "00" + fecha.Month.ToString();
            string day = "00" + fecha.Day.ToString();
            FechaDesdeObj.Month = month.Substring(month.Length - 2);
            FechaDesdeObj.Day = day.Substring(day.Length - 2);
    
            return FechaDesdeObj;
        }
        public async Task<PersonasDto> MapObjPersonasDto(RH_PERSONAS dtos)
        {
               
               PersonasDto itemResult = new PersonasDto();
               try
               {
                 
                itemResult.CodigoPersona = dtos.CODIGO_PERSONA;
                itemResult.Cedula = dtos.CEDULA;
                itemResult.Nombre = dtos.NOMBRE;
                itemResult.Apellido = dtos.APELLIDO;
                itemResult.Nacionalidad = dtos.NACIONALIDAD;
                itemResult.Sexo = dtos.SEXO;
            
             
               
                itemResult.FechaNacimiento = dtos.FECHA_NACIMIENTO; 
                itemResult.FechaNacimientoString = dtos.FECHA_NACIMIENTO.ToString("u");
                FechaDto FechaNacimientoObj = GetFechaDto(dtos.FECHA_NACIMIENTO);
                itemResult.FechaNacimientoObj = (FechaDto)FechaNacimientoObj;
                
                var desdeEdad = dtos.FECHA_NACIMIENTO;
          
                var hastaEdad = DateTime.Now;
                var edad = TiempoServicio(desdeEdad, hastaEdad);
                itemResult.Edad = edad.CantidadAños;
                itemResult.PaisNacimientoId = dtos.PAIS_NACIMIENTO_ID;
                itemResult.EstadoNacimientoId = dtos.ESTADO_NACIMIENTO_ID;
                itemResult.NumeroGacetaNacional = dtos.NUMERO_GACETA_NACIONAL;
                itemResult.EstadoCivilId = dtos.ESTADO_CIVIL_ID;
                itemResult.DescripcionEstadoCivil = await _rhDescriptivasServices.GetDescripcionByCodigoDescriptiva(dtos.ESTADO_CIVIL_ID);
                itemResult.Estatura = dtos.ESTATURA;
                itemResult.Peso = dtos.PESO;
                if (string.IsNullOrEmpty(dtos.MANO_HABIL))
                {
                    dtos.MANO_HABIL = "D";
                }
                
                itemResult.ManoHabil = dtos.MANO_HABIL;
                itemResult.Extra1 = dtos.EXTRA1;
                itemResult.Extra2 = dtos.EXTRA2;
                itemResult.Extra3 = dtos.EXTRA3;
                itemResult.Status = dtos.STATUS;
                itemResult.IdentificacionId = dtos.IDENTIFICACION_ID;
                itemResult.NumeroIdentificacion = dtos.NUMERO_IDENTIFICACION;
                itemResult.Email = "";

                var pais = await _sisUbicacionNacionalRepository.GetPais(dtos.PAIS_NACIMIENTO_ID);
                if (pais is not null)
                {
                    itemResult.PaisNacimiento = pais.EXTRA1!;
                }
                var comunicaciones = await _rhComunicacionessRepository.GetByCodigoPersona(dtos.CODIGO_PERSONA);
                if (comunicaciones.Count > 0)
                {
                    var email = comunicaciones.Where(x => x.LINEA_COMUNICACION.Contains("@")).FirstOrDefault();
                    if (email is not null) itemResult.Email = email.LINEA_COMUNICACION;


                }
                if (itemResult.Sexo == "F")
                {
                    itemResult.Avatar = "/images/avatars/4.png";
                }
                else
                {
                    itemResult.Avatar = "/images/avatars/1.png";
                }

                itemResult.Avatar = $"/images/avatars/{dtos.CEDULA.ToString()}.jpg";
                var desde = DateTime.Now;
                //var primerMovimiento = await _rhHistoricoPersonalCargorepository.GetPrimerMovimientoByCodigoPersona(dtos.CODIGO_PERSONA);
                //desde = primerMovimiento.FECHA_NOMINA;

                desde = await FechaIngresoTrabajador(dtos.CODIGO_PERSONA);
                var hasta = DateTime.Now;
                var tiempoServicio = TiempoServicio(desde, hasta);
                itemResult.TiempoServicio = tiempoServicio;
                var relacionCargo = await CargoActual(dtos.CODIGO_PERSONA);
                if(relacionCargo is not null)
                {
                    itemResult.Sueldo = relacionCargo.SUELDO;
                    itemResult.CodigoCargo = relacionCargo.CODIGO_CARGO;
                    var cargo = await _preCargosRepository.GetByCodigo(relacionCargo.CODIGO_CARGO);
                    itemResult.DescripcionCargo = cargo.DENOMINACION;
                    var icp = await _preIndiceCatPrg.GetByCodigo(relacionCargo.CODIGO_ICP);
                    if(icp is not null)
                    {
                        itemResult.CodigoIcp = relacionCargo.CODIGO_ICP;
                        itemResult.DescripcionIcp = icp.DENOMINACION;
                    }
                    

                }
      
                return itemResult;
               }
               catch (Exception e)
               {
                   Console.WriteLine(e);
                   var result = dtos;
                   throw;
               }
               
              



        }
        public async Task<DateTime>  FechaIngresoTrabajador(int codigoPersona)
        {
            DateTime result= new DateTime();

            List<DateTime> listFechas = new List<DateTime>();

            var primerMovimiento = await _rhHistoricoPersonalCargorepository.GetPrimerMovimientoByCodigoPersona(codigoPersona);
            if (primerMovimiento is not null)
            {

                listFechas.Add(primerMovimiento.FECHA_NOMINA);
            }

            var administrativos = await _rhAdministrativosServices.GetPrimerMovimientoByCodigoPersona(codigoPersona);
            if (administrativos is not null)
            {
                listFechas.Add(administrativos.FECHA_INGRESO);
            }
          

            if (listFechas.Count > 0)
            {
                result = listFechas.OrderBy(x => x).FirstOrDefault();
                return result;
            }
            else
            {
                result=DateTime.Now;
                return result;
            }

           
         

        }

        public async  Task<List<PersonasDto>> MapListPersonasDto(List<RH_PERSONAS> dtos)
        {
            List<PersonasDto> result = new List<PersonasDto>();

            foreach (var item in dtos)
            {

                PersonasDto itemResult = new PersonasDto();
                itemResult = await MapObjPersonasDto(item);


                result.Add(itemResult);

                
            }
            return result;



        }


        public async Task<List<ListSimplePersonaDto>> MapListSimplePersonasDto(List<RH_PERSONAS> dtos)
        {
            List<ListSimplePersonaDto> result = new List<ListSimplePersonaDto>();

            foreach (var item in dtos)
            {

                ListSimplePersonaDto itemResult = new ListSimplePersonaDto();



                itemResult.CodigoPersona = item.CODIGO_PERSONA;
                itemResult.Cedula = item.CEDULA;
                itemResult.Nombre = item.NOMBRE;
                itemResult.Apellido = item.APELLIDO;
                itemResult.Status = item.STATUS;
                itemResult.Nacionalidad = item.NACIONALIDAD;
                itemResult.Sexo = item.SEXO;
                itemResult.FechaNacimiento = item.FECHA_NACIMIENTO.ToShortDateString();
                itemResult.Email = "";
                
           
                var pais = await _sisUbicacionNacionalRepository.GetPais(item.PAIS_NACIMIENTO_ID);
                if(pais is not null)
                {
                    itemResult.PaisNacimiento = pais.EXTRA1!;
                }
                var comunicaciones = await _rhComunicacionessRepository.GetByCodigoPersona(item.CODIGO_PERSONA);
                if (comunicaciones.Count > 0)
                {
                    var email = comunicaciones.Where(x => x.LINEA_COMUNICACION.Contains("@")).FirstOrDefault();
                    if (email is not null) itemResult.Email = email.LINEA_COMUNICACION;
                

                }


                if (item.SEXO == "F")
                {
                    itemResult.Avatar = "/images/avatars/4.png";    
                }
                else
                {
                    itemResult.Avatar = "/images/avatars/1.png";
                }
                itemResult.Avatar = $"/images/avatars/{item.CEDULA.ToString()}.jpg";

                result.Add(itemResult);


            }
                return result.OrderBy(p=>p.NombreCompleto).ToList();



        }


        public  TiempoServicioResponseDto TiempoServicio(DateTime desde, DateTime hasta)
        {
            TiempoServicioResponseDto result = new TiempoServicioResponseDto();

            var añoDesde = desde.Year;
            var mesDesde = desde.Month;
            var diaDesde = desde.Day;

            var añoHasta = hasta.Year;
            var mesHasta = hasta.Month;
            var diaHasta = hasta.Day;

            var cantidadAños = 0;
            var cantidadMeses = 0;
            var cantidadDias = 0;

            cantidadAños = añoHasta - añoDesde;
            cantidadMeses = mesHasta - mesDesde;
            cantidadDias = diaHasta - diaDesde;


            if (cantidadDias > 28)
            {
                cantidadDias = 0;
                cantidadMeses++;
            }
            else if (cantidadDias < 0)
            {
                cantidadDias = 30 + cantidadDias;
                cantidadMeses--;

            }
            if (cantidadMeses == 12)
            {
                cantidadMeses = 0;
                cantidadAños++;
            }
            else if (cantidadMeses < 0)
            {
                cantidadMeses = 12 + cantidadMeses;
                cantidadAños--;
            }
            
            result.CantidadAños = cantidadAños;
            result.CantidadMeses = cantidadMeses;
            result.CantidadDias = cantidadDias;

            result.FechaDesde = desde;
            result.FechaHasta = hasta;
            result.FechaDesdeString = desde.ToShortDateString();
            result.FechaHastaString = hasta.ToShortDateString();



            return result;


        }

        public async Task<RH_RELACION_CARGOS> CargoActual(int codigoPersona)
        {
            RH_RELACION_CARGOS result;
            result = await _rhRelacionCargosRepository.GetUltimoCargoPorPersona(codigoPersona);
            return result;
        }

        public List<string> GetListNacionalidad()
        {
            List<string> result = new List<string>();
            result.Add("V");
            result.Add("E");
            return result;
        }
        public List<string> GetListSexo()
        {
            List<string> result = new List<string>();
            result.Add("M");
            result.Add("F");
            return result;
        }
        public List<string> GetListStatus()
        {
            List<string> result = new List<string>();
            result.Add("A");
            result.Add("E");
            result.Add("S");
            return result;
        }
        public List<string> GetListManoHabil()
        {
            List<string> result = new List<string>();
            result.Add("D");
            result.Add("I");
          
            return result;
        }

    }
}

