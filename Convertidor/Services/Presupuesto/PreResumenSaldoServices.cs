using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using Ganss.Excel;

namespace Convertidor.Services.Presupuesto
{
	public class PreResumenSaldoServices: IPreResumenSaldoServices
    {
       

        private readonly IPreResumenSaldoRepository _repository;
      
        
        public PreResumenSaldoServices(IPreResumenSaldoRepository repository)
        {
            _repository = repository;
           
        }

        

        public async Task<ResultDto<List<PreResumenSaldoGetDto>>> GetAllByPresupuesto(int codigoPresupuesto)
        {
            ResultDto<List<PreResumenSaldoGetDto>> result = new ResultDto<List<PreResumenSaldoGetDto>>(null);
            try
            {
              
                var resumen = await _repository.GetAllByPresupuesto(codigoPresupuesto);
                if (resumen.Count() > 0)
                {

                    var q = from s in resumen.OrderBy(x => x.TITULO).ToList()
                            group s by new { 
                                CodigoPresupuesto = s.CODIGO_PRESUPUESTO, 
                                Titulo= s.TITULO,
                                CodigoIcpConcat =s.CODIGO_ICP_CONCAT,
                                Partida =s.PARTIDA,
                                Presupuestado =s.PRESUPUESTADO,
                                Modificacion =s.MODIFICACION,
                                AsignacionModificada =s.ASIGNACION_MODIFICADA,
                                Comprometido =s.CODIGO_PRESUPUESTO,
                                Causado =s.CAUSADO,
                                Pagado =s.PAGADO,
                              
                                
                            } into g
                            select new PreResumenSaldoGetDto {
                                CodigoPresupuesto = g.Key.CodigoPresupuesto,
                                Titulo = g.Key.Titulo,
                                CodigoIcpConcat = g.Key.CodigoIcpConcat,
                                Partida = g.Key.Partida,
                                Presupuestado = g.Key.Presupuestado,
                                Modificacion = g.Key.Modificacion,
                                AsignacionModificada = g.Key.AsignacionModificada,
                                Comprometido = g.Key.Comprometido,
                                Causado = g.Key.Causado,
                                Pagado = g.Key.Pagado,
                              
                            };
                   




                    result.Data = q.ToList();

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

