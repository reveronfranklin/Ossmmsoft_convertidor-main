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
        private readonly IBmConteoDetalleHistoricoService _conteoDetalleHistoricoService;
        private readonly IBmDescriptivaRepository _bmDescriptivaRepository;
        private readonly IConfiguration _configuration;
        public BmConteoHistoricoService(IBmConteoHistoricoRepository repository,
                                ISisUsuarioRepository sisUsuarioRepository,
                                IRhPersonasRepository rhPersonasRepository,
                                IBmConteoDetalleHistoricoService conteoDetalleHistoricoService,
                                IBmDescriptivaRepository bmDescriptivaRepository,
                                IConfiguration configuration)
		{
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _rhPersonasRepository = rhPersonasRepository;
            _conteoDetalleHistoricoService = conteoDetalleHistoricoService;
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
        public async Task<BmConteoHistoricoResponseDto> MapBmConteo(BM_CONTEO_HISTORICO dtos)
        {


            BmConteoHistoricoResponseDto itemResult = new BmConteoHistoricoResponseDto();
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
            itemResult.TotalCantidad = dtos.TOTAL_CANTIDAD;
            itemResult.TotalCantidadContada = dtos.TOTAL_CANTIDAD_CONTADA;
            itemResult.TotalDiferencia = dtos.TOTAL_DIFERENCIA;
            return itemResult;

        }
        public async Task<List<BmConteoHistoricoResponseDto>> MapListConteoDto(List<BM_CONTEO_HISTORICO> dtos)
        {
            List<BmConteoHistoricoResponseDto> result = new List<BmConteoHistoricoResponseDto>();


            foreach (var item in dtos)
            {

                BmConteoHistoricoResponseDto itemResult = new BmConteoHistoricoResponseDto();

                itemResult = await MapBmConteo(item);

                result.Add(itemResult);
            }
            return result.OrderByDescending(x=>x.CodigoBmConteo).ToList();



        }
        
        public async Task<ResultDto<List<BmConteoHistoricoResponseDto>>> GetAll()
        {

            ResultDto<List<BmConteoHistoricoResponseDto>> result = new ResultDto<List<BmConteoHistoricoResponseDto>>(null);
            try
            {

                var conteos = await _repository.GetAll();



                if (conteos.Count() > 0)
                {
                    List<BmConteoHistoricoResponseDto> listDto = new List<BmConteoHistoricoResponseDto>();
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
        
        public async Task<ResultDto<BmConteoHistoricoResponseDto>> GetByCodigoConteo(int codigoConteo)
        {

            ResultDto<BmConteoHistoricoResponseDto> result = new ResultDto<BmConteoHistoricoResponseDto>(null);
            try
            {

                var conteo = await _repository.GetByCodigo(codigoConteo);

                

                if (conteo!=null)
                {
                   
                    var listDto = await MapBmConteo(conteo);
                

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

        public async Task CreateReportConteoHistorico(int codigoConteo)
        {

            var connteo =await  GetByCodigoConteo(codigoConteo);

            var detalle = await _conteoDetalleHistoricoService.GetAllByConteo(codigoConteo);


        }
    }
}

