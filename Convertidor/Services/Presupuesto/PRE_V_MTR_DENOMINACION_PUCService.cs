using System;
using AutoMapper;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Rh;

namespace Convertidor.Services.Presupuesto
{
	public class PRE_V_MTR_DENOMINACION_PUCService: IPRE_V_MTR_DENOMINACION_PUCService
    {
	
        private readonly IPRE_V_MTR_DENOMINACION_PUCRepository _repository;
      

        private readonly IMapper _mapper;

        public PRE_V_MTR_DENOMINACION_PUCService(IPRE_V_MTR_DENOMINACION_PUCRepository repository)
        {
            _repository = repository;
           
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


        public List<ListPreMtrDenominacionPuc> MapPreMtrDenominacionPuc(List<PRE_V_MTR_DENOMINACION_PUC> dtos) {

            List<ListPreMtrDenominacionPuc> result = new List<ListPreMtrDenominacionPuc>();
            int id = 0;
            foreach (var item in dtos.OrderBy(x=> x.CODIGO_PUC_CONCAT).ToList())
            {

                ListPreMtrDenominacionPuc itemResult = new ListPreMtrDenominacionPuc();
                id++;
                itemResult.Id = id;
                itemResult.CodigoPuc = item.CODIGO_PUC;
                itemResult.CodigoPucConcat = item.CODIGO_PUC_CONCAT;
                itemResult.DenominacionPuc = item.DENOMINACION_PUC;
                result.Add(itemResult);


            }

            return result;
        }
        




    }
}

