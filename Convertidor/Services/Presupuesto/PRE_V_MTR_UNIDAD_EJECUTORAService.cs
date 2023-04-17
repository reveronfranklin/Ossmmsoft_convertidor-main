using System;
using AutoMapper;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Presupuesto
{
	public class PRE_V_MTR_UNIDAD_EJECUTORAService : IPRE_V_MTR_UNIDAD_EJECUTORAService
    {
	
        private readonly IPRE_V_MTR_UNIDAD_EJECUTORARepository _repository;
      

        private readonly IMapper _mapper;

        public PRE_V_MTR_UNIDAD_EJECUTORAService(IPRE_V_MTR_UNIDAD_EJECUTORARepository repository)
        {
            _repository = repository;
           
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


        public List<ListPreMtrUnidadEjecutora> MapPreMtrUnidadEjecutora(List<PRE_V_MTR_UNIDAD_EJECUTORA> dtos) {

            List<ListPreMtrUnidadEjecutora> result = new List<ListPreMtrUnidadEjecutora>();
            int id = 0;
            foreach (var item in dtos)
            {

                ListPreMtrUnidadEjecutora itemResult = new ListPreMtrUnidadEjecutora();
                id++;
                itemResult.Id = id;
                itemResult.CodigoIcp = item.CODIGO_IPC;
                itemResult.CodigoIcpConcat = item.CODIGO_ICP_CONCAT;
                itemResult.UnidadEjecutora = item.UNIDAD_EJECUTORA;
                result.Add(itemResult);


            }
            return result;
        }
        




    }
}

