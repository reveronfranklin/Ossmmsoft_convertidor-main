using System;
using System.Collections.Generic;
using AutoMapper;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Rh;
using Convertidor.Services.Rh;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace Convertidor.Data.Repository.Rh
{
	public class RhPersonaService: IRhPersonaService
    {
        

   
        private readonly IRhPersonasRepository _repository;
        private readonly IRhHistoricoMovimientoService _historicoMovimientoService;
        private readonly IRhEducacionService _rhEducacionService;
        private readonly IRhDireccionesService _rhDireccionesService;
        private readonly IRhConceptosService _rhConceptosService;
        private readonly ISisUbicacionNacionalRepository _sisUbicacionNacionalRepository;
        private readonly IRhComunicacionessRepository _rhComunicacionessRepository;
        private readonly IRH_HISTORICO_PERSONAL_CARGORepository _rhHistoricoPersonalCargorepository;
        private readonly IRhDescriptivasService _rhDescriptivasServices;
        private readonly IRhAdministrativosService _rhAdministrativosServices;
        private readonly IRhRelacionCargosRepository _rhRelacionCargosRepository;
        private readonly IPreCargosRepository _preCargosRepository;
        private readonly IPRE_INDICE_CAT_PRGRepository _preIndiceCatPrg;
     

        public RhPersonaService(IRhPersonasRepository repository,
                                IRhHistoricoMovimientoService historicoMovimientoService,
                                IRhEducacionService rhEducacionService,
                                IRhDireccionesService rhDireccionesService,
                                IRhConceptosService rhConceptosService,
                                ISisUbicacionNacionalRepository sisUbicacionNacionalRepository,
                                IRhComunicacionessRepository rhComunicacionessRepository,
                                IRH_HISTORICO_PERSONAL_CARGORepository rhHistoricoPersonalCargorepository,
                                IRhDescriptivasService rhDescriptivasServices,
                                 IRhAdministrativosService rhAdministrativosServices,
                                IRhRelacionCargosRepository rhRelacionCargosRepository, 
                                IPreCargosRepository preCargosRepository,
                                IPRE_INDICE_CAT_PRGRepository preIndiceCatPrg)
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
        }
       
        public async Task<List<PersonasDto>> GetAll()
        {
            try
            {
                var personas = await _repository.GetAll();

                var result =await  MapListPersonasDto(personas);


                return (List<PersonasDto>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


        public async Task<List<ListSimplePersonaDto>> GetAllSimple()
        {
            try
            {
                var personas = await _repository.GetAll();

                var result =await MapListSimplePersonasDto(personas);


                return (List<ListSimplePersonaDto>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<PersonasDto> GetPersona(int codigoPersona)
        {
            try
            {
                var personas = await _repository.GetCodigoPersona(codigoPersona);

                var result = await MapObjPersonasDto(personas);


                return (PersonasDto)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<ListPersonasDto> GetByCodigoPersona(int codigoPersona)
        {
            try
            {
                var personas = await _repository.GetCodigoPersona(codigoPersona);

                var result = await MapPersonasDto(personas);


                return (ListPersonasDto)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
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
        
        public async Task<PersonasDto> MapObjPersonasDto(RH_PERSONAS dtos)
        {
               PersonasDto result;
               PersonasDto itemResult = new PersonasDto();
                itemResult.CodigoPersona = dtos.CODIGO_PERSONA;
                itemResult.Cedula = dtos.CEDULA;
                itemResult.Nombre = dtos.NOMBRE;
                itemResult.Apellido = dtos.APELLIDO;
                itemResult.Nacionalidad = dtos.NACIONALIDAD;
                itemResult.Sexo = dtos.SEXO;
            
                itemResult.FechaNacimiento = dtos.FECHA_NACIMIENTO.ToShortDateString();
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
                var primerMovimiento = await _rhHistoricoPersonalCargorepository.GetPrimerMovimientoByCodigoPersona(dtos.CODIGO_PERSONA);
                desde = primerMovimiento.FECHA_NOMINA;

                desde = await FechaIngresoTrabajador(dtos.CODIGO_PERSONA);
                var hasta = DateTime.Now;
                var tiempoServicio = TiempoServicio(desde, hasta);
                itemResult.TiempoServicio = tiempoServicio;
                var relacionCargo = await CargoActual(dtos.CODIGO_PERSONA);
                {
                    itemResult.CodigoCargo = relacionCargo.CODIGO_CARGO;
                    var cargo = await _preCargosRepository.GetByCodigo(relacionCargo.CODIGO_CARGO);
                    itemResult.DescripcionCargo = cargo.DENOMINACION;
                    var icp = await _preIndiceCatPrg.GetByCodigo(relacionCargo.CODIGO_ICP);
                    {
                        itemResult.CodigoIcp = relacionCargo.CODIGO_ICP;
                        itemResult.DescripcionIcp = icp.DENOMINACION;
                    }
                    

                }


                result =itemResult;

            
      
            return result;



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
            listFechas.Add(administrativos.FECHA_INGRESO);

            if (listFechas.Count > 0)
            {
                result = listFechas.OrderBy(x => x).FirstOrDefault();
                return result;
            }
            else
            {
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
            var p_dias = cantidadDias;
            var p_meses = cantidadMeses;
            var p_anos = cantidadAños;


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
            //else if (cantidadMeses == 0)
                    {
                cantidadMeses = 12 + cantidadMeses;
                cantidadAños--;
            }


            /*if (p_dias > 28) {
                p_dias= 0;
                p_meses= p_meses + 1;
            }
            else if (p_dias < 0 ) {
                p_dias  = 30 + p_dias;
                p_meses= p_meses - 1;
            }
            if (p_meses == 12) {
                p_meses = 0;
                p_anos = p_anos + 1;
            }
            else if (p_meses< 0) {
                p_meses = 12 + p_meses;
                p_anos= p_anos - 1;
            }*/

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
        
    }
}

