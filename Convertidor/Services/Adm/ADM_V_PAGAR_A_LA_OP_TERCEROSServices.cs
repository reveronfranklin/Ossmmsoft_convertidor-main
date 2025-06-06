using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Utility;

namespace Convertidor.Services.Adm
{
	public class ADM_V_PAGAR_A_LA_OP_TERCEROSServices: IADM_V_PAGAR_A_LA_OP_TERCEROSServices
    {

      
        private readonly IAdmAdmProveedoresContactoRepository _repository;

     

        public ADM_V_PAGAR_A_LA_OP_TERCEROSServices(IAdmAdmProveedoresContactoRepository repository
                                     )
		{
            _repository = repository;
           
        }

        public async Task<ResultDto<List<ADM_V_PAGAR_A_LA_OP_TERCEROS>>> GetAll()
        {
            
            ResultDto<List<ADM_V_PAGAR_A_LA_OP_TERCEROS>> result = new ResultDto<List<ADM_V_PAGAR_A_LA_OP_TERCEROS>>(null);
            var proveedores = await _repository.GetAll();
            result.Data = proveedores;
            result.IsValid = true;
            result.Message = string.Empty;
            result.CantidadRegistros = proveedores.Count;
            result.TotalPage = 1;
            return result;
            
            
            return result;
        }



    }
}

