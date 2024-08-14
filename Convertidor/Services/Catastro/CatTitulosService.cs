using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Dtos.Catastro;
using Convertidor.Dtos.Cnt;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Catastro
{
    public class CatTitulosService : ICatTitulosService
    {
        private readonly ICatTitulosRepository _repository;

        public CatTitulosService(ICatTitulosRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultDto<List<CatTitulosResponseDto>>> GetAll()
        {

            ResultDto<List<CatTitulosResponseDto>> result = new ResultDto<List<CatTitulosResponseDto>>(null);
            try
            {

                var titulos = await _repository.GetAll();



                if (titulos.Count() > 0)
                {
                    List<CatTitulosResponseDto> listDto = new List<CatTitulosResponseDto>();

                    foreach (var item in titulos)
                    {
                        CatTitulosResponseDto dto = new CatTitulosResponseDto();
                        dto.TituloId = item.TITULO_ID;
                        dto.TituloFkID = item.TITULO_FK_ID;
                        dto.Titulo = item.TITULO;
                        dto.CodigoTitulo = item.CODIGO_TITULO;
                        if (item.EXTRA1 == null) item.EXTRA1 = "";
                        if (item.EXTRA2 == null) item.EXTRA2 = "";
                        if (item.EXTRA3 == null) item.EXTRA3 = "";
                        if (item.EXTRA4 == null) item.EXTRA4 = "";
                        if (item.EXTRA5 == null) item.EXTRA5 = "";
                        if (item.EXTRA6 == null) item.EXTRA6 = "";
                        if (item.EXTRA7 == null) item.EXTRA7 = "";
                        if (item.EXTRA8 == null) item.EXTRA8 = "";
                        if (item.EXTRA9 == null) item.EXTRA9 = "";
                        if (item.EXTRA10 == null) item.EXTRA10 = "";
                        if (item.EXTRA11 == null) item.EXTRA11 = "";
                        if (item.EXTRA12 == null) item.EXTRA12 = "";
                        if (item.EXTRA13 == null) item.EXTRA13 = "";
                        if (item.EXTRA14 == null) item.EXTRA14 = "";
                        if (item.EXTRA15 == null) item.EXTRA15 = "";
                        
                        dto.Extra1 = item.EXTRA1;
                        dto.Extra2 = item.EXTRA2;
                        dto.Extra3 = item.EXTRA3;
                        dto.Extra4 = item.EXTRA4;
                        dto.Extra5 = item.EXTRA5;
                        dto.Extra6 = item.EXTRA6;
                        dto.Extra7 = item.EXTRA7;
                        dto.Extra8 = item.EXTRA8;
                        dto.Extra9 = item.EXTRA9;
                        dto.Extra10 = item.EXTRA10;
                        dto.Extra11 = item.EXTRA11;
                        dto.Extra12 = item.EXTRA12;
                        dto.Extra13 = item.EXTRA13;
                        dto.Extra14 = item.EXTRA14;
                        dto.Extra15 = item.EXTRA15;
                    
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
