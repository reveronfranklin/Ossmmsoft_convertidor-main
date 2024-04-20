using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public class PRE_V_MTR_DENOMINACION_PUCService: IPRE_V_MTR_DENOMINACION_PUCService
    {
	
        private readonly IPRE_V_MTR_DENOMINACION_PUCRepository _repository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;



        public PRE_V_MTR_DENOMINACION_PUCService(IPRE_V_MTR_DENOMINACION_PUCRepository repository,IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository)
        {
            _repository = repository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;


        }

        public async Task<List<ListPreMtrDenominacionPuc>> GetAll()
        {
            try
            {
                    var denominacionPuc = await _repository.GetAll();

                var result = MapPreMtrDenominacionPuc(denominacionPuc);


                return (List<ListPreMtrDenominacionPuc>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }

        public async Task<List<ListPreMtrDenominacionPuc>> GetAllByPresupuesto(int codigoPresupuesto)
        {
            try
            {
                if (codigoPresupuesto == 0)
                {
                    var presupuesto = await _pRE_PRESUPUESTOSRepository.GetLast();
                    codigoPresupuesto = presupuesto.CODIGO_PRESUPUESTO;
                }
               

                var denominacionPuc = await _repository.GetByCodigoPresupuesto(codigoPresupuesto);

                var result = MapPreMtrDenominacionPuc(denominacionPuc);


                return (List<ListPreMtrDenominacionPuc>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


        public List<ListPreMtrDenominacionPuc> MapPreMtrDenominacionPuc(List<PRE_V_MTR_DENOMINACION_PUC> dtos) {

            List<ListPreMtrDenominacionPuc> result = new List<ListPreMtrDenominacionPuc>();
            int id = 0;
            foreach (var item in dtos.OrderBy(x=> x.CODIGO_PUC_CONCAT).ToList())
            {

                ListPreMtrDenominacionPuc itemResult = new ListPreMtrDenominacionPuc();
                id++;
                itemResult.Id = id;
                itemResult.CodigoPresupuesto = item.CODIGO_PRESUPUESTO;
                itemResult.CodigoPuc = item.CODIGO_PUC;
                itemResult.CodigoPucConcat = item.CODIGO_PUC_CONCAT;
                itemResult.DenominacionPuc = item.DENOMINACION_PUC;
                result.Add(itemResult);


            }

            return result;
        }
        




    }
}

