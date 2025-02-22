using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Dtos.Bm;

namespace Convertidor.Services.Bm
{
    public class BmUbicacionesService: IBmUbicacionesService
    {
        private readonly IBmUbicacionesRepository _repository;


        public BmUbicacionesService(IBmUbicacionesRepository repository)

        {
            _repository = repository;
          
        }

      
        
        public async Task<ResultDto<List<BmUbicacionesResponseDto>>> GetAll()
        {

            ResultDto<List<BmUbicacionesResponseDto>> result = new ResultDto<List<BmUbicacionesResponseDto>>(null);
            try
            {

                var ubicaciones = await _repository.GetAll();



                if (ubicaciones.Count() > 0)
                {
                    List<BmUbicacionesResponseDto> listDto = new List<BmUbicacionesResponseDto>();

                    foreach (var item in ubicaciones)
                    {
                        BmUbicacionesResponseDto dto = new BmUbicacionesResponseDto();
                        dto.CodigoIcp = item.CODIGO_ICP;
                        dto.CodigoDirBien = item.CODIGO_DIR_BIEN;
                        dto.UnidadEjecutora = item.UNIDAD_EJECUTORA;

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

