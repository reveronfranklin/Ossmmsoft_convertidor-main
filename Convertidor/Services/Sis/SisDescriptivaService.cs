using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Sis;


namespace Convertidor.Services.Sis
{
    
	public class SisDescriptivaService:ISisDescriptivaService
    {
        private readonly ISisDescriptivaRepository _repository;

        private readonly ISisUsuarioRepository _sisUsuarioRepository;
      


        public SisDescriptivaService(ISisDescriptivaRepository repository,
                                
                                      ISisUsuarioRepository sisUsuarioRepository)
		{
            _repository = repository;
          
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        
       
       
        public SisDescriptivasGetDto MapSisDescriptivasDto(SIS_DESCRIPTIVAS dtos)
        {
            SisDescriptivasGetDto itemResult = new SisDescriptivasGetDto();
            
            try
            {
                if (dtos == null)
                {
                    return itemResult;
                }
                
                
                SisDescriptivasGetDto dto = new SisDescriptivasGetDto();
                itemResult.DescripcionId = dtos.DESCRIPCION_ID;
                itemResult.DescripcionTituloId =dtos.DESCRIPCION_TITULO_ID;
                itemResult.Descripcion = dtos.DESCRIPCION;
                itemResult.CodigoDescripcion=dtos.CODIGO_DESCRIPCION;
             
                
                return itemResult;

            }
            catch (Exception e)
            {
                Console.WriteLine(dtos);
                Console.WriteLine(e);
                return itemResult;
            }
          
        }

        public List<SisDescriptivasGetDto> MapListSisDescriptivaDto(List<SIS_DESCRIPTIVAS> dtos)
        {
            List<SisDescriptivasGetDto> result = new List<SisDescriptivasGetDto>();
            if (dtos.Count > 0)
            {
                foreach (var item in dtos)
                {
                    if (item == null)
                    {
                        var detener = "";
                    }
                    else
                    {
                        var itemResult =   MapSisDescriptivasDto(item);
               
                        result.Add(itemResult);
                    }

                   
                }
            }
            
          
            return result;



        }
      
      


     
        public async Task<ResultDto<List<SisDescriptivasGetDto>>> GetAllByTitulo(SisDescriptivaFilterDto filter)
        {
            ResultDto<List<SisDescriptivasGetDto>> result = new ResultDto<List<SisDescriptivasGetDto>>(null);
            try
            {
        
                var descriptivas = await _repository.GetALLByTituloId(filter.TituloId);
                if (descriptivas == null)
                {
                    result.Data = null;
                    result.CantidadRegistros=0;
                    result.Page = 0;
                    result.TotalPage = 0;
                    result.IsValid = false;
                    result.Message = "No Data";
                    return result;
                }
                 
                var resultDto =   MapListSisDescriptivaDto(descriptivas);
                result.Data = resultDto;
                result.CantidadRegistros=descriptivas.Count;
                result.Page =1;
                result.TotalPage =1;
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

        
    }
}

