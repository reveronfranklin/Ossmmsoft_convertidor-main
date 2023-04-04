using System;
using System.Collections.Generic;
using AutoMapper;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.RH;
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

        private readonly IMapper _mapper;

        public RhEducacionService(IRhEducacionRepository repository, IRhDescriptivasService descriptivaService)
        {
            _repository = repository;
            _descriptivaService = descriptivaService;


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

    }
}

