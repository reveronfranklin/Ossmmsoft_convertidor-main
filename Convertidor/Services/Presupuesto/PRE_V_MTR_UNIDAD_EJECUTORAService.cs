using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public class PRE_V_MTR_UNIDAD_EJECUTORAService : IPRE_V_MTR_UNIDAD_EJECUTORAService
    {
	
        private readonly IPRE_V_MTR_UNIDAD_EJECUTORARepository _repository;
        private readonly IPRE_PRESUPUESTOSRepository _pRE_PRESUPUESTOSRepository;

     

        public PRE_V_MTR_UNIDAD_EJECUTORAService(IPRE_V_MTR_UNIDAD_EJECUTORARepository repository, IPRE_PRESUPUESTOSRepository pRE_PRESUPUESTOSRepository)
        {
            _repository = repository;
            _pRE_PRESUPUESTOSRepository = pRE_PRESUPUESTOSRepository;
        }

        public async Task<List<ListPreMtrUnidadEjecutora>> GetAll()
        {
            try
            {
                var unidadEjecutora = await _repository.GetAll();

                var result = MapPreMtrUnidadEjecutora(unidadEjecutora);


                return (List<ListPreMtrUnidadEjecutora>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<List<ListPreMtrUnidadEjecutora>> GetAllByPresupuesto(int codigoPresupuesto)
        {
            try
            {
                if (codigoPresupuesto == 0)
                {
                    var presupuesto = await _pRE_PRESUPUESTOSRepository.GetLast();
                    codigoPresupuesto = presupuesto.CODIGO_PRESUPUESTO;
                }

                var unidadEjecutora = await _repository.GetAllByPresupuesto(codigoPresupuesto);

                var result = MapPreMtrUnidadEjecutora(unidadEjecutora);


                return (List<ListPreMtrUnidadEjecutora>)result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }


        public List<ListPreMtrUnidadEjecutora> MapPreMtrUnidadEjecutora(List<PRE_V_MTR_UNIDAD_EJECUTORA> dtos) {

            List<ListPreMtrUnidadEjecutora> result = new List<ListPreMtrUnidadEjecutora>();
            int id = 0;
            foreach (var item in dtos)
            {

                ListPreMtrUnidadEjecutora itemResult = new ListPreMtrUnidadEjecutora();
                id++;
                itemResult.Id = id;
                itemResult.CodigoPresupuesto = item.CODIGO_PRESUPUESTO;
                itemResult.CodigoIcp = item.CODIGO_IPC;
                itemResult.CodigoIcpConcat = item.CODIGO_ICP_CONCAT;
                itemResult.UnidadEjecutora = item.UNIDAD_EJECUTORA;
                result.Add(itemResult);


            }
            return result;
        }
        




    }
}

