using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
    public class CatDescriptivasService : ICatDescriptivasService
    {
        private readonly ICatDescriptivasRepository _repository;
        private readonly ICatTitulosRepository _catTitulosRepository;

        public CatDescriptivasService(ICatDescriptivasRepository repository,
                                      ICatTitulosRepository catTitulosRepository)
        {
            _repository = repository;
            _catTitulosRepository = catTitulosRepository;
        }

        public async Task<ResultDto<List<CatDescriptivasResponseDto>>> GetAll()
        {

            ResultDto<List<CatDescriptivasResponseDto>> result = new ResultDto<List<CatDescriptivasResponseDto>>(null);
            try
            {

                var titulos = await _repository.GetAll();



                if (titulos.Count() > 0)
                {
                    List<CatDescriptivasResponseDto> listDto = new List<CatDescriptivasResponseDto>();

                    foreach (var item in titulos)
                    {
                        CatDescriptivasResponseDto dto = new CatDescriptivasResponseDto();
                        dto = await MapCatDescriptiva(item);

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

        public CatDescriptivasResponseDto GetDefaultDecriptiva()
        {
            CatDescriptivasResponseDto itemDefault = new CatDescriptivasResponseDto();
            itemDefault.DescripcionId = 0;
            itemDefault.DescripcionFkId = 0;
            itemDefault.Descripcion = "Seleccione";
            itemDefault.Codigo = "";
            itemDefault.TituloId = 0;
            itemDefault.Descripcion = "";
            itemDefault.Extra1 = "";
            itemDefault.Extra2 = "";
            itemDefault.Extra3 = "";
            itemDefault.Extra4 = "";
            itemDefault.Extra5 = "";
            itemDefault.Extra6 = "";
            itemDefault.Extra7 = "";
            itemDefault.Extra8 = "";
            itemDefault.Extra9 = "";
            itemDefault.Extra10 = "";
            itemDefault.Extra11 = "";
            itemDefault.Extra12 = "";
            itemDefault.Extra13 = "";
            itemDefault.Extra14 = "";
            itemDefault.Extra15 = "";
            return itemDefault;

        }

        public async Task<CatDescriptivasResponseDto> MapCatDescriptiva(CAT_DESCRIPTIVAS entity)
        {
            CatDescriptivasResponseDto dto = new CatDescriptivasResponseDto();
            dto.DescripcionId = entity.DESCRIPCION_ID;
            dto.Id = entity.DESCRIPCION_ID;
            dto.DescripcionFkId = entity.DESCRIPCION_FK_ID;
            dto.Descripcion = entity.DESCRIPCION;
            if (entity.CODIGO == null) entity.CODIGO = "";
            dto.Codigo = entity.CODIGO;
            dto.TituloId = entity.TITULO_ID;
            dto.Descripcion = "";
            var titulo = await _catTitulosRepository.GetByTitulo(entity.TITULO_ID);
            if (titulo != null) dto.DescripcionTitulo = titulo.TITULO;

            if (entity.EXTRA1 == null) entity.EXTRA1 = "";
            if (entity.EXTRA2 == null) entity.EXTRA2 = "";
            if (entity.EXTRA3 == null) entity.EXTRA3 = "";
            if (entity.EXTRA4 == null) entity.EXTRA4 = "";
            if (entity.EXTRA5 == null) entity.EXTRA5 = "";
            if (entity.EXTRA6 == null) entity.EXTRA6 = "";
            if (entity.EXTRA7 == null) entity.EXTRA7 = "";
            if (entity.EXTRA8 == null) entity.EXTRA8 = "";
            if (entity.EXTRA9 == null) entity.EXTRA9 = "";
            if (entity.EXTRA10 == null) entity.EXTRA10 = "";
            if (entity.EXTRA11 == null) entity.EXTRA11 = "";
            if (entity.EXTRA12 == null) entity.EXTRA12 = "";
            if (entity.EXTRA13 == null) entity.EXTRA13 = "";
            if (entity.EXTRA14 == null) entity.EXTRA14 = "";
            if (entity.EXTRA15 == null) entity.EXTRA15 = "";

            dto.Extra1 = entity.EXTRA1;
            dto.Extra2 = entity.EXTRA2;
            dto.Extra3 = entity.EXTRA3;
            dto.Extra4 = entity.EXTRA4;
            dto.Extra5 = entity.EXTRA5;
            dto.Extra6 = entity.EXTRA6;
            dto.Extra7 = entity.EXTRA7;
            dto.Extra8 = entity.EXTRA8;
            dto.Extra9 = entity.EXTRA9;
            dto.Extra10 = entity.EXTRA10;
            dto.Extra11 = entity.EXTRA11;
            dto.Extra12 = entity.EXTRA12;
            dto.Extra13 = entity.EXTRA13;
            dto.Extra14 = entity.EXTRA14;
            dto.Extra15 = entity.EXTRA15;


            List<CatDescriptivasResponseDto> listDto = new List<CatDescriptivasResponseDto>();
            var hijos = await _repository.GetByFKID(entity.DESCRIPCION_ID);

            if (hijos != null && hijos.Count > 0)
            {

                CatDescriptivasResponseDto itemDefault = new CatDescriptivasResponseDto();
                itemDefault = GetDefaultDecriptiva();
                List<CatDescriptivasResponseDto> lista = new List<CatDescriptivasResponseDto>();
                lista.Add(GetDefaultDecriptiva());
                itemDefault.ListaDescriptiva = lista;


                listDto.Add(itemDefault);
                foreach (var item in hijos)
                {
                    CatDescriptivasResponseDto itemDto = new CatDescriptivasResponseDto();
                    itemDto.DescripcionId = item.DESCRIPCION_ID;
                    itemDto.DescripcionFkId = item.DESCRIPCION_FK_ID;
                    itemDto.Descripcion = item.DESCRIPCION;
                    if (item.CODIGO == null) item.CODIGO = "";
                    itemDto.Codigo = item.CODIGO;
                    itemDto.TituloId = item.TITULO_ID;
                    itemDto.DescripcionTitulo = "";
                    var tituloItem = await _catTitulosRepository.GetByTitulo(item.TITULO_ID);
                    if (tituloItem != null) itemDto.DescripcionTitulo = tituloItem.TITULO;

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
                    itemDto.Extra1 = item.EXTRA1;
                    itemDto.Extra2 = item.EXTRA2;
                    itemDto.Extra3 = item.EXTRA3;

                    listDto.Add(itemDto);
                }
                dto.ListaDescriptiva = listDto;
            }
            else
            {
                CatDescriptivasResponseDto itemDefault = new CatDescriptivasResponseDto();
                itemDefault = GetDefaultDecriptiva();
                List<CatDescriptivasResponseDto> lista = new List<CatDescriptivasResponseDto>();
                lista.Add(GetDefaultDecriptiva());
                itemDefault.ListaDescriptiva = lista;


                listDto.Add(itemDefault);
                dto.ListaDescriptiva = listDto;
            }


            return dto;

        }

    }
}
