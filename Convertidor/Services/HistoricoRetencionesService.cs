using Convertidor.Data.EntitiesDestino;
using Convertidor.Data.Interfaces;

namespace Convertidor.Services
{
    public class HistoricoRetencionesService: IHistoricoRetencionesService
    {
        private readonly IHistoricoRetencionesRepository _repository;
        private readonly IHistoricoNominaService _historicoNominaService;
        private readonly IHistoricoPersonalCargoService _historicoPersonalCargoService;
        private readonly IConceptosRetencionesService _conceptosRetencionesService;

        public HistoricoRetencionesService(IHistoricoRetencionesRepository repository,
                                           IHistoricoNominaService historicoNominaService,
                                           IHistoricoPersonalCargoService historicoPersonalCargoService,
                                           IConceptosRetencionesService conceptosRetencionesService)
                                      
                                     
        {
            _repository = repository;
            _historicoNominaService = historicoNominaService;
            _historicoPersonalCargoService = historicoPersonalCargoService;
            _conceptosRetencionesService = conceptosRetencionesService;
        }

        public async Task<ResultDto<HistoricoRetenciones>> GeneraHistoricoRetencionesPorCantidadDeDias(int dias)
        {

            ResultDto<HistoricoRetenciones> result = new ResultDto<HistoricoRetenciones>(null);

            List<HistoricoRetenciones> destinoList = new List<HistoricoRetenciones>();

            try
            {


                var historico = await _historicoNominaService.GetByLastDayWithRelation(dias);
                if (historico.ToList().Count > 0)
                {
                    await _repository.DeletePorDias(dias);

                    foreach (var item in historico)
                    {

                       

                        var conceptoRetencion = await _conceptosRetencionesService.GetByConcepto(item.CODIGO_CONCEPTO);
                        if (conceptoRetencion!=null)
                        {
                            /* TABLAS:
                              RH.RH_HISTORICO_PERSONAL_CARGO RVPC
                              RH.RH_HISTORICO_NOMINA RMN
                              PRE.PRE_INDICE_CAT_PRG PICP
                              'FAOV' RETENCION ==> Titulo
                              ,RVPC.TIPO_NOMINA 
                              ,PICP.UNIDAD_EJECUTORA 
                              ,RVPC.DESCRIPCION_CARGO 
                              ,RVPC.NACIONALIDAD 
                              ,RVPC.CEDULA 
                              ,RVPC.NOMBRE 
                              ,RVPC.APELLIDO 
                              ,TO_CHAR(RVPC.FECHA_INGRESO,'DD/MM/RRRR') FECHA_INGRESO  
                              ,TO_CHAR(RVPC.FECHA_NOMINA,'DD/MM/RRRR') FECHA_NOMINA  
                              ,RMN.MONTO MONTO_RETENCION
                                , RVPC.SUELDO*/
                            HistoricoRetenciones destinoNew = new HistoricoRetenciones();
                            destinoNew.CODIGO_HISTORICO_NOMINA = item.CODIGO_HISTORICO_NOMINA;
                            destinoNew.Titulo = conceptoRetencion.Titulo;
                            destinoNew.TIPO_NOMINA = item.Codigo.CODIGO_TIPO_NOMINA;
                            destinoNew.UNIDAD_EJECUTORA = item.Codigo.IndiceCategoriaPrograma.UNIDAD_EJECUTORA;
                            destinoNew.DESCRIPCION_CARGO = item.Codigo.DESCRIPCION_CARGO;
                            destinoNew.NACIONALIDAD = item.Codigo.NACIONALIDAD;
                            destinoNew.CEDULA = item.Codigo.CEDULA;
                            destinoNew.NOMBRE = item.Codigo.NOMBRE;
                            destinoNew.APELLIDO = item.Codigo.APELLIDO;
                            destinoNew.FECHA_NOMINA = item.Codigo.FECHA_NOMINA;
                            destinoNew.FECHA_INGRESO = item.Codigo.FECHA_INGRESO;
                            destinoNew.MONTO = item.MONTO;
                            destinoNew.SUELDO = item.Codigo.SUELDO;
                            destinoNew.FECHA_INS = item.FECHA_INS;
                            destinoList.Add(destinoNew);
                        }
                        

                    }
                    var adicionados = await _repository.Add(destinoList);
                    result.IsValid = true;
                    result.Message = $" {historico.ToList().Count} Registros Transferidos";
                    return result;
                }
                else
                {

                    result.IsValid = true;
                    result.Message = $" No existen Datos para transferir";
                    return result;

                }



            }
            catch (Exception ex)
            {

                result.IsValid = false;
                result.Message = ex.InnerException.Message;
                return result;

            }
        }




    }
}
