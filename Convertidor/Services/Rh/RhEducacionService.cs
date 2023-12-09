using System;
using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Dtos;
using Convertidor.Dtos.Rh;
using Convertidor.Services.Rh;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class RhEducacionService: IRhEducacionService
    {
		
        private readonly DataContext _context;



   
        private readonly IRhEducacionRepository _repository;
        private readonly IRhDescriptivasService _descriptivaService;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhPersonasRepository _personasRepository;
        private readonly IMapper _mapper;

        public RhEducacionService(IRhEducacionRepository repository,
                           IRhDescriptivasService descriptivaService,
                           ISisUsuarioRepository sisUsuarioRepository,
                           IRhPersonasRepository personasRepository)
        {
            _repository = repository;
            _descriptivaService = descriptivaService;
            _sisUsuarioRepository = sisUsuarioRepository;
            _personasRepository = personasRepository;
           
        }
       
        public async Task<List<ListEducacionDto>> GetByCodigoPersona(int codigoPersona)
        {
            try
            {
                var educacion = await _repository.GetByCodigoPersona(codigoPersona);

                var result = await MapListEducacionDto(educacion);


                return (List<ListEducacionDto>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<RH_EDUCACION> GetByCodigo(int codigoEducacion)
        {
            try
            {
                var result = await _context.RH_EDUCACION.DefaultIfEmpty()
                    .Where(e => e.CODIGO_EDUCACION == codigoEducacion)
                    .OrderBy(x => x.FECHA_INI).FirstOrDefaultAsync();

                return (RH_EDUCACION)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<RhEducacionResponseDto> MapEducacionDto(RH_EDUCACION dtos)
        {


            RhEducacionResponseDto itemResult = new RhEducacionResponseDto();
            itemResult.CodigoEducacion = dtos.CODIGO_EDUCACION;
            itemResult.CodigoPersona = dtos.CODIGO_PERSONA;
            itemResult.NivelId = dtos.NIVEL_ID;
            itemResult.NombreInstituto = dtos.NOMBRE_INSTITUTO;
            itemResult.LocalidadInstituto =dtos.LOCALIDAD_INSTITUTO;
            itemResult.ProfesionID = dtos.PROFESION_ID;
            itemResult.FechaIni = dtos.FECHA_INI;
            itemResult.FechaFin = dtos.FECHA_FIN;
            itemResult.UltimoAñoAprobado = dtos.ULTIMO_ANO_APROBADO;
            itemResult.Graduado = dtos.GRADUADO;
            itemResult.TituloId = dtos.TITULO_ID;
            itemResult.MencionEspecialidadId = dtos.MENCION_ESPECIALIDAD_ID;
            itemResult.Extra1 =dtos.EXTRA1;
            itemResult.Extra2 =dtos.EXTRA2;
            itemResult.Extra3 =dtos.EXTRA3;
            itemResult.USuarioIns = dtos.USUARIO_INS;
            itemResult.FechaIns = dtos.FECHA_INS;
            itemResult.UsuarioUpd = dtos.USUARIO_UPD;
            itemResult.FechaUpd = dtos.FECHA_UPD;
            itemResult.CodigoEmpresa = dtos.CODIGO_EMPRESA;
            


            return itemResult;

        }


        public async  Task<List<ListEducacionDto>> MapListEducacionDto(List<RH_EDUCACION> dtos)
        {
            List<ListEducacionDto> result = new List<ListEducacionDto>();

            foreach (var item in dtos)
            {

                ListEducacionDto itemResult = new ListEducacionDto();

                itemResult.CodigoEducacion = item.CODIGO_EDUCACION;
                itemResult.CodigoPersona = item.CODIGO_PERSONA;
                itemResult.NivelId = item.NIVEL_ID;
                itemResult.DescripcionNivel = await _descriptivaService.GetDescripcionByCodigoDescriptiva(item.NIVEL_ID);
                itemResult.NombreInstituto = item.NOMBRE_INSTITUTO;
                itemResult.LocalidadInstituto = item.LOCALIDAD_INSTITUTO;
                itemResult.ProfesionId = item.PROFESION_ID;
                itemResult.DescripcionProfesion = await _descriptivaService.GetDescripcionByCodigoDescriptiva(item.PROFESION_ID);
                itemResult.FechaIni = item.FECHA_INI;
                itemResult.FechaFin = item.FECHA_FIN;
                itemResult.UltimoAnoAprobado = item.ULTIMO_ANO_APROBADO;
                itemResult.Graduado = item.GRADUADO;
                itemResult.TituloId = item.TITULO_ID;
                itemResult.DescripcionTitulo = await _descriptivaService.GetDescripcionByCodigoDescriptiva(item.TITULO_ID);
                itemResult.MencionEspecialidadId = item.MENCION_ESPECIALIDAD_ID;
                itemResult.DescripcionMencionEspecialidad = await _descriptivaService.GetDescripcionByCodigoDescriptiva(item.MENCION_ESPECIALIDAD_ID);
                itemResult.Extra1 = item.EXTRA1;
                itemResult.Extra2 = item.EXTRA2;
                itemResult.Extra3 = item.EXTRA3;
                itemResult.CodigoEmpresa = item.CODIGO_EMPRESA;


      
                result.Add(itemResult);


            }
            return result;

        }
        public async Task<ResultDto<RhEducacionResponseDto>> Update(RhEducacionUpdate dto)
        {

            ResultDto<RhEducacionResponseDto> result = new ResultDto<RhEducacionResponseDto>(null);
            try
            {

                var educacion = await _repository.GetByCodigo(dto.CodigoEducacion);
                if (educacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Educacion no existe";
                    return result;
                }
                var persona = await _personasRepository.GetCodigoPersona(dto.CodigoPersona);
                if (persona == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Persona no existe";
                    return result;
                }

              
                var educaciones = await _descriptivaService.GetByTitulo(dto.TituloId);
                
                {
                    var educa = educaciones.Where(x => x.Id == dto.TituloId);
                    if (educa is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Titulo invalido";
                        return result;
                    }
                }

                var niveles = await _descriptivaService.GetByTitulo(dto.NivelId);

                {
                    var nivel = niveles.Where(x => x.Id == dto.NivelId);
                    if (niveles is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Nivel Invalido";
                        return result;
                    }
                }

                var profesiones = await _descriptivaService.GetByTitulo(dto.ProfesionId);

                {
                    var profesion = profesiones.Where(x => x.Id == dto.ProfesionId);
                    if (profesiones is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Profesion Invalida";
                        return result;
                    }
                }
                var mencionEspecialdades = await _descriptivaService.GetByTitulo(dto.MencionEspecialidadId);
                
                {
                    var mencionEspecialdad = mencionEspecialdades.Where(x => x.Id == dto.MencionEspecialidadId);
                    if (mencionEspecialdades is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Mencion Invalida";
                        return result;
                    }
                }

                if (dto.LocalidadInstituto == string.Empty)
                {
                    result.Message = "Localidad instituto invalida";
                    result.IsValid = false;
                    return result;
                }


                if (dto.NombreInstituto == string.Empty)
                {
                    result.Message = "Instituto invalido";
                    result.IsValid = false;
                    return result;
                }
                if (dto.UltimoAñoAprobado == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Año aprobado invalido";
                }

                if (dto.Graduado == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Graduado invalido";
                }



                educacion.CODIGO_EDUCACION = dto.CodigoEducacion;
                educacion.CODIGO_PERSONA = dto.CodigoPersona;
                educacion.NIVEL_ID = dto.NivelId;
                educacion.NOMBRE_INSTITUTO = dto.NombreInstituto;
                educacion.LOCALIDAD_INSTITUTO = dto.LocalidadInstituto;
                educacion.PROFESION_ID = dto.ProfesionId;
                educacion.FECHA_INI = dto.FechaIni;
                educacion.FECHA_FIN = dto.FechaFin;
                educacion.ULTIMO_ANO_APROBADO = dto.UltimoAñoAprobado;
                educacion.GRADUADO = dto.Graduado;
                educacion.TITULO_ID = dto.TituloId;
                educacion.MENCION_ESPECIALIDAD_ID = dto.MencionEspecialidadId;
                educacion.CODIGO_EMPRESA = dto.CodigoEmpresa;
                

                var conectado = await _sisUsuarioRepository.GetConectado();
                educacion.CODIGO_EMPRESA = conectado.Empresa;
                educacion.USUARIO_UPD = conectado.Usuario;



                await _repository.Update(educacion);



                var resultDto = await MapEducacionDto(educacion);
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

        public async Task<ResultDto<RhEducacionResponseDto>> Create (RhEducacionUpdate dto)
        {

            ResultDto<RhEducacionResponseDto> result = new ResultDto<RhEducacionResponseDto>(null);
            try
            {

                var niveles = await _descriptivaService.GetByTitulo(5);

                {
                    var nivel = niveles.Where(x => x.Id == dto.NivelId);
                    if (niveles is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Nivel Invalido";
                        return result;
                    }
                }
                var profesiones = await _descriptivaService.GetByTitulo(8);

                {
                    var profesion = profesiones.Where(x => x.Id == dto.ProfesionId);
                    if (profesiones is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Profesion Invalida";
                        return result;
                    }
                }

                var educaciones = await _descriptivaService.GetByTitulo(16);

                {
                    var educa = educaciones.Where(x => x.Id == dto.TituloId);
                    if (educa is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Titulo invalido";
                        return result;
                    }
                }

                
                var mencionEspecialdades = await _descriptivaService.GetByTitulo(25);

                {
                    var mencionEspecialdad = mencionEspecialdades.Where(x => x.Id == dto.MencionEspecialidadId);
                    if (mencionEspecialdades is null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Mencion Invalida";
                        return result;
                    }
                }

                if(dto.LocalidadInstituto == string.Empty) 
                {
                    result.Message = "Localidad invalida";
                    result.IsValid = false;
                    return result;
                }
                
                
                if(dto.NombreInstituto== string.Empty) 
                {
                    result.Message = "Instituto invalido";
                    result.IsValid = false;
                    return result;
                }
                if (dto.UltimoAñoAprobado == null) 
                {
                  result.Data= null;
                  result.IsValid = false;
                  result.Message = "Año aprobado invalido";
                }

                if (dto.Graduado == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Graduado invalido";
                }


                RH_EDUCACION entity = new RH_EDUCACION();
                entity.CODIGO_EDUCACION = await _repository.GetNextKey();
                entity.CODIGO_PERSONA = dto.CodigoPersona;
                entity.NIVEL_ID = dto.NivelId;
                entity.NOMBRE_INSTITUTO = dto.NombreInstituto;
                entity.LOCALIDAD_INSTITUTO = dto.LocalidadInstituto;
                entity.PROFESION_ID = dto.ProfesionId;
                entity.FECHA_INI = dto.FechaIni;
                entity.FECHA_FIN = dto.FechaFin;
                entity.ULTIMO_ANO_APROBADO = dto.UltimoAñoAprobado;
                entity.GRADUADO = dto.Graduado;
                entity.TITULO_ID = dto.TituloId;
                entity.MENCION_ESPECIALIDAD_ID = dto.MencionEspecialidadId;
                entity.CODIGO_EMPRESA = dto.CodigoEmpresa;





                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_UPD = conectado.Usuario;


                var created = await _repository.Add(entity);

                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapEducacionDto(created.Data);
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

        public async Task<ResultDto<RhEducacionDeleteDto>> Delete(RhEducacionDeleteDto dto)
        {

            ResultDto<RhEducacionDeleteDto> result = new ResultDto<RhEducacionDeleteDto>(null);
            try
            {

                var educacion = await _repository.GetByCodigo(dto.CodigoEducacion);
                if (educacion == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Educacion no existe";
                    return result;
                }


                var deleted = await _repository.Delete(dto.CodigoEducacion);

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


