using Convertidor.Data.Entities.Sis;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Dtos;
using Convertidor.Dtos.Sis;

namespace Convertidor.Services.Sis
{
	public class OssServices: IOssConfigServices
    {
		
        private readonly IOssConfigRepository _repository;
        private readonly IConfiguration _configuration;



        private readonly IHttpContextAccessor _httpContextAccessor;

        public OssServices(IOssConfigRepository repository,
                                    IHttpContextAccessor httpContextAccessor,
                                    IConfiguration configuration)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<ResultDto<List<OssConfigGetDto>>> GetListByClave(string clave)
        {

            ResultDto<List<OssConfigGetDto>> result = new ResultDto<List<OssConfigGetDto>>(null);
            try
            {
                var ossConfig = await _repository.GetListByClave(clave);
                var qossConfig = from s in ossConfig.ToList()
                              group s by new
                              {
                                  Clave = s.CLAVE,
                                  Valor = s.VALOR,
                                
                              } into g
                              select new OssConfigGetDto
                              {

                                  Clave = g.Key.Clave,
                                  Valor = g.Key.Valor,
                                

                              };
                result.Data= qossConfig.OrderBy(X=>X.Valor).ToList();
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

        public async Task<int> GetNextByClave(string clave)
        {

            int result = 0;
            try
            {
                var ossConfig = await _repository.GetByClave(clave);
                if(ossConfig == null) 
                {
                  OSS_CONFIG config= new OSS_CONFIG();
                  config.CLAVE = clave;
                    config.VALOR = "1";
                    await _repository.Add(config);
                    result = 1;
                }
                else 
                {
                    int ultimo = Int32.Parse(ossConfig.VALOR);
                    result = ultimo+1;
                    ossConfig.VALOR = result.ToString();
                    await _repository.Update(ossConfig);
                }
                
                return result;

            }
            catch (Exception ex)
            {
                return result;
            }


        }





    }
}

