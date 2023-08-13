using System;
using System.Collections.Generic;
using AutoMapper;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Rh;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.RH;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Rh;
using Convertidor.Services.Rh;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Rh
{
	public class RhProcesosService: IRhProcesosService
    {
		
        private readonly DataContext _context;



   
        private readonly IRhProcesoRepository _repository;
        private readonly IRhProcesoDetalleRepository _rhProcesoDetalleRepository;
        private readonly IRhConceptosRepository _rhConceptosRepository;

        private readonly IMapper _mapper;

        public RhProcesosService(IRhProcesoRepository repository,
                                 IRhProcesoDetalleRepository rhProcesoDetalleRepository, IRhConceptosRepository rhConceptosRepository)
        {
            _repository = repository;
            _rhProcesoDetalleRepository = rhProcesoDetalleRepository;
            _rhConceptosRepository = rhConceptosRepository;


        }
       
        public async Task<RH_PROCESOS> GetByCodigo(int codigoProcesso)
        {
            try
            {
                var proceso = await _repository.GetByCodigo(codigoProcesso);
              

                return proceso;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<ResultDto<List<RhprocesosDto>>> GetAll()
        {

            ResultDto<List<RhprocesosDto>> result = new ResultDto<List<RhprocesosDto>>(null);
            try
            {
                var procesos = await _repository.GetAll();
                var listDto = await MapListProceso(procesos);

                result.Data = listDto;
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




        private async Task<List<RhprocesosDto>> MapListProceso(List<RH_PROCESOS> dto)
        {

            List<RhprocesosDto> result = new List<RhprocesosDto>();
         
            try
            {
                var resultNew = dto

                 .Select(e => new RhprocesosDto
                 {
                     CodigoProceso = e.CODIGO_PROCESO,
                     Descripcion = e.DESCRIPCION,
               

                 });
                result = resultNew.ToList();

                foreach (var item in result)
                {
                    var detalleConceptos = await _rhProcesoDetalleRepository.GetByCodigoProceso(item.CodigoProceso);
                    if (detalleConceptos.Count > 0)
                    {
                        List<ListConceptosDto> listConceptosDto = new List<ListConceptosDto>();
                        foreach (var itemDetalle in detalleConceptos)
                        {
                            var concepto = await _rhConceptosRepository.GetByCodigoTipoNomina(itemDetalle.CODIGO_CONCEPTO, itemDetalle.CODIGO_TIPO_NOMINA);
                            ListConceptosDto itemlistConceptosDto = new ListConceptosDto();
                            itemlistConceptosDto.Codigo = concepto.CODIGO;
                            itemlistConceptosDto.CodigoConcepto = concepto.CODIGO_CONCEPTO;
                            itemlistConceptosDto.CodigoTipoNomina = concepto.CODIGO_TIPO_NOMINA;
                            itemlistConceptosDto.Denominacion = concepto.DENOMINACION;

                            listConceptosDto.Add(itemlistConceptosDto);

                        }
                        item.Conceptos = listConceptosDto;
                    }
                }


               

                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }







        }

    }
}

