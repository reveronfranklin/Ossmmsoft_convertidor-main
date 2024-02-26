using System;
using System.Collections.Generic;
using System.Globalization;
using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Data.Repository.Rh;
using Convertidor.Dtos.Bm;
using Convertidor.Utility;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using NuGet.Packaging;

namespace Convertidor.Services.Bm
{
    public class BmConteoHistoricoService: IBmConteoHistoricoService
    {

      
        private readonly IBmConteoHistoricoRepository _repository;
     
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IRhPersonasRepository _rhPersonasRepository;
        private readonly IBmConteoDetalleService _conteoDetalleService;
        private readonly IBmDescriptivaRepository _bmDescriptivaRepository;
        private readonly IConfiguration _configuration;
        public BmConteoHistoricoService(IBmConteoHistoricoRepository repository,
                                ISisUsuarioRepository sisUsuarioRepository,
                                IRhPersonasRepository rhPersonasRepository,
                                IBmConteoDetalleService conteoDetalleService,
                                IBmDescriptivaRepository bmDescriptivaRepository,
                                IConfiguration configuration)
		{
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _rhPersonasRepository = rhPersonasRepository;
            _conteoDetalleService = conteoDetalleService;
            _bmDescriptivaRepository = bmDescriptivaRepository;
            _configuration = configuration;
           

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
        public async Task<BmConteoResponseDto> MapBmConteo(BM_CONTEO_HISTORICO dtos)
        {


            BmConteoResponseDto itemResult = new BmConteoResponseDto();
            itemResult.CodigoBmConteo = dtos.CODIGO_BM_CONTEO;
            itemResult.Titulo = dtos.TITULO;
            itemResult.Comentario = dtos.COMENTARIO;
            itemResult.CodigoPersonaResponsable = dtos.CODIGO_PERSONA_RESPONSABLE;
            itemResult.NombrePersonaResponsable = "";
            var persona = await _rhPersonasRepository.GetCodigoPersona(dtos.CODIGO_PERSONA_RESPONSABLE);
            if (persona!=null)
            {
                itemResult.NombrePersonaResponsable = $"{persona.NOMBRE} {persona.APELLIDO}";
            }

          
            itemResult.ConteoId = dtos.CANTIDAD_CONTEOS_ID;
            itemResult.Fecha = dtos.FECHA;
            itemResult.FechaString = dtos.FECHA.ToString("u");
            FechaDto fechaObj = GetFechaDto(dtos.FECHA);
            itemResult.FechaObj = (FechaDto)fechaObj;
            var resumen = await _conteoDetalleService.GetResumen(dtos.CODIGO_BM_CONTEO);
            itemResult.ResumenConteo = resumen.Data;
            itemResult.TotalCantidad = itemResult.ResumenConteo.Sum(r => r.Cantidad);
            itemResult.TotalCantidadContado = itemResult.ResumenConteo.Sum(r => r.CantidadContada);
            return itemResult;

        }
        public async Task<List<BmConteoResponseDto>> MapListConteoDto(List<BM_CONTEO_HISTORICO> dtos)
        {
            List<BmConteoResponseDto> result = new List<BmConteoResponseDto>();


            foreach (var item in dtos)
            {

                BmConteoResponseDto itemResult = new BmConteoResponseDto();

                itemResult = await MapBmConteo(item);

                result.Add(itemResult);
            }
            return result.OrderByDescending(x=>x.CodigoBmConteo).ToList();



        }
        
        public async Task<ResultDto<List<BmConteoResponseDto>>> GetAll()
        {

            ResultDto<List<BmConteoResponseDto>> result = new ResultDto<List<BmConteoResponseDto>>(null);
            try
            {

                var conteos = await _repository.GetAll();



                if (conteos.Count() > 0)
                {
                    List<BmConteoResponseDto> listDto = new List<BmConteoResponseDto>();
                    listDto = await MapListConteoDto(conteos);
                

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



    }
}

