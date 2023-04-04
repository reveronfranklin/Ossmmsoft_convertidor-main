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
	public class RhDireccionesService: IRhDireccionesService
    {
		
        private readonly DataContext _context;



   
        private readonly IRhDireccionesRepository _repository;
        private readonly IRhDescriptivasService _descriptivaService;

        private readonly IMapper _mapper;

        public RhDireccionesService(IRhDireccionesRepository repository, IRhDescriptivasService descriptivaService)
        {
            _repository = repository;
            _descriptivaService = descriptivaService;


        }
       
        public async Task<List<ListDireccionesDto>> GetByCodigoPersona(int codigoPersona)
        {
            try
            {
                var educacion = await _repository.GetByCodigoPersona(codigoPersona);

                var result = await MapListDireccionesDto(educacion);


                return (List<ListDireccionesDto>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

      
      

        public async  Task<List<ListDireccionesDto>> MapListDireccionesDto(List<RH_DIRECCIONES> dtos)
        {
            List<ListDireccionesDto> result = new List<ListDireccionesDto>();

            foreach (var item in dtos)
            {

                ListDireccionesDto itemResult = new ListDireccionesDto();

                itemResult.CodigoDireccion = item.CODIGO_DIRECCION;
                itemResult.CodigoPersona = item.CODIGO_PERSONA;
                itemResult.DireccionId = item.DIRECCION_ID;
                itemResult.DescripcionDireccion = await _descriptivaService.GetDescripcionByCodigoDescriptiva(item.DIRECCION_ID);
                itemResult.PaisId = item.PAIS_ID;
                itemResult.DescripcionPais = await _descriptivaService.GetDescripcionByCodigoDescriptiva(item.PAIS_ID);
                itemResult.EstadoId = item.ESTADO_ID;
                itemResult.DescripcionEsatado = await _descriptivaService.GetDescripcionByCodigoDescriptiva(item.ESTADO_ID);
                itemResult.MunicipioId = item.MUNICIPIO_ID;
                itemResult.DescripcionMunicipio = await _descriptivaService.GetDescripcionByCodigoDescriptiva(item.MUNICIPIO_ID);
                itemResult.CiudadId = item.CIUDAD_ID;
                itemResult.DescripcionCiudad = await _descriptivaService.GetDescripcionByCodigoDescriptiva(item.CIUDAD_ID);
                itemResult.ParroquiaId = item.PARROQUIA_ID;
                itemResult.DescripcionParroquia = await _descriptivaService.GetDescripcionByCodigoDescriptiva(item.PARROQUIA_ID);
                itemResult.SectorId = item.SECTOR_ID;
                itemResult.DescripcionSector = await _descriptivaService.GetDescripcionByCodigoDescriptiva(item.SECTOR_ID);
                itemResult.UrbanizacionId = item.URBANIZACION_ID;
                itemResult.DescripcionUrbanizacion = await _descriptivaService.GetDescripcionByCodigoDescriptiva(item.URBANIZACION_ID);
                itemResult.ManzanaId = item.MANZANA_ID;
                itemResult.DescripcionManzana = await _descriptivaService.GetDescripcionByCodigoDescriptiva(item.MANZANA_ID);
                itemResult.ParcelaId = item.PARCELA_ID;
                itemResult.DescripcionParcela = await _descriptivaService.GetDescripcionByCodigoDescriptiva(item.PARCELA_ID);
                itemResult.VialidadId = item.VIALIDAD_ID;
                itemResult.DescripcionVialidad = await _descriptivaService.GetDescripcionByCodigoDescriptiva(item.VIALIDAD_ID);
                itemResult.Vialidad = item.VIALIDAD;
                itemResult.TipoViviendaId = item.TIPO_VIVIENDA_ID;
                itemResult.DescripcionTipovivienda = await _descriptivaService.GetDescripcionByCodigoDescriptiva(item.TIPO_VIVIENDA_ID);
                itemResult.ViviendaId = item.VIVIENDA_ID;
                itemResult.DescripcionVivienda = await _descriptivaService.GetDescripcionByCodigoDescriptiva(item.VIVIENDA_ID);
                itemResult.Vivienda = item.VIVIENDA;
                itemResult.TipoNivelId = item.TIPO_NIVEL_ID;
                itemResult.DescripcionTipoNivel = await _descriptivaService.GetDescripcionByCodigoDescriptiva(item.TIPO_NIVEL_ID);
                itemResult.Nivel = item.NIVEL;
                itemResult.NroVivienda = item.NRO_VIVIENDA;
                itemResult.ComplementoDir = item.COMPLEMENTO_DIR;
                itemResult.TenenciaId = item.TENENCIA_ID;
                itemResult.DescripcionTenencia = await _descriptivaService.GetDescripcionByCodigoDescriptiva(item.TENENCIA_ID);
                itemResult.CodigoPostal = item.CODIGO_POSTAL;
                itemResult.Principal = item.PRINCIPAL;
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

