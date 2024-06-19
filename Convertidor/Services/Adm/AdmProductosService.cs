using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Utility;

namespace Convertidor.Services.Adm
{
	public class AdmProductosService: IAdmProductosService
    {

      
        private readonly IAdmProductosRepository _repository;

        private readonly IConfiguration _configuration;
        public AdmProductosService(IAdmProductosRepository repository,
                                      IConfiguration configuration)
		{
            _repository = repository;
            _configuration = configuration;


        }

        public async Task<ResultDto<List<AdmProductosResponse>>> GetAllPaginate(AdmProductosFilterDto filter)
        {
            var result = await _repository.GetAllPaginate(filter);
            return result;
        }
        public async Task<ResultDto<List<AdmProductosResponse>>> GetAll()
        {

            ResultDto<List<AdmProductosResponse>> result = new ResultDto<List<AdmProductosResponse>>(null);
            try
            {

                var productos = await _repository.GetAll();

               

                if (productos.Count() > 0)
                {
                    List<AdmProductosResponse> listDto = new List<AdmProductosResponse>();

                    foreach (var item in productos)
                    {
                        AdmProductosResponse dto = new AdmProductosResponse();
                        dto.Codigo = item.CODIGO_PRODUCTO;
                        dto.Descripcion = item.DESCRIPCION;

                        listDto.Add(dto);
                    }


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

