using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Catastro;
using Convertidor.Dtos.Catastro;
using Convertidor.Dtos.Cnt;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Catastro
{
    public class CatTitulosService : ICatTitulosService
    {
        private readonly ICatTitulosRepository _repository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly ICatDescriptivasRepository _catDescriptivasRepository;

        public CatTitulosService(ICatTitulosRepository repository,
                                 ISisUsuarioRepository sisUsuarioRepository,
                                 ICatDescriptivasRepository catDescriptivasRepository)
        {
            _repository = repository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _catDescriptivasRepository = catDescriptivasRepository;
        }

        public CatTitulosResponseDto MapCatTitulo(CAT_TITULOS entity)
        {
            CatTitulosResponseDto dto = new CatTitulosResponseDto();
            dto.TituloId = entity.TITULO_ID;
            dto.TituloFkID = entity.TITULO_FK_ID;
            dto.Titulo = entity.TITULO;
            if (entity.CODIGO_TITULO == null) entity.CODIGO_TITULO = "";
            dto.CodigoTitulo = entity.CODIGO_TITULO;
            dto.TituloId = entity.TITULO_ID;
            if (dto.Extra1 == null) entity.EXTRA1 = "";
            if (dto.Extra2 == null) entity.EXTRA2 = "";
            if (dto.Extra3 == null) entity.EXTRA3 = "";
            if (dto.Extra4 == null) entity.EXTRA4 = "";
            if (dto.Extra5 == null) entity.EXTRA5 = "";
            if (dto.Extra6 == null) entity.EXTRA6 = "";
            if (dto.Extra7 == null) entity.EXTRA7 = "";
            if (dto.Extra8 == null) entity.EXTRA8 = "";
            if (dto.Extra9 == null) entity.EXTRA9 = "";
            if (dto.Extra10 == null) entity.EXTRA10 = "";
            if (dto.Extra11 == null) entity.EXTRA11 = "";
            if (dto.Extra12 == null) entity.EXTRA12 = "";
            if (dto.Extra13 == null) entity.EXTRA13 = "";
            if (dto.Extra14 == null) entity.EXTRA14 = "";
            if (dto.Extra15 == null) entity.EXTRA15 = "";

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

            return dto;

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

        public async Task<List<Item>> GetItems()
        {
            List<Item> result = new List<Item>();
            var descriptivas = await _repository.GetAll();

            foreach (var item in descriptivas)
            {
                Item itenNew = new Item();
                itenNew.Id = item.TITULO_ID;

                itenNew.ParentId = (int)item.TITULO_FK_ID;
                var patchEvaluado = item.TITULO;

                itenNew.Name = patchEvaluado;
                itenNew.Denominacion = item.TITULO;
                if (item.TITULO == null) item.TITULO = "";
                itenNew.Descripcion = item.TITULO;
                result.Add(itenNew);

            }

            return result;
        }

        public async Task<List<string>> WriteStrings()
        {

            List<string> result = new List<string>();
            List<Item> items = await GetItems();


            IEnumerable<string> resultingStrings = from parent in items.Where(x => x.ParentId == 0)
                                                   join child in items on parent.Id equals child.ParentId
                                                   //join grandChild in items on child.Id equals grandChild.ParentId


                                                   select string.Format("{0}:{1}:{2}:{3}:{4}:{5}", parent.Id.ToString() + "-" + parent.Name, child.Id.ToString() + "-" + child.Name, child.Id, child.ParentId, child.Denominacion, child.Descripcion);


            var lista = resultingStrings.ToList();

            foreach (var item in lista)
            {
                var search = result.Where(x => x == item).FirstOrDefault();
                if (search == null)
                {
                    result.Add(item);
                }


            }

            IEnumerable<string> resultingAddStrings = from parent in items
                                                          //join child in items on parent.Id equals child.ParentId
                                                          //join grandChild in items on child.Id equals grandChild.ParentId


                                                      select string.Format("{0}:{1}:{2}:{3}:{4}:{5}", parent.Id.ToString() + "-" + parent.Name, parent.Id.ToString() + "-" + parent.Name, parent.Id, parent.ParentId, parent.Denominacion, parent.Descripcion);



            lista = resultingAddStrings.ToList();
            foreach (var item in lista)
            {
                var search = result.Where(x => x == item).FirstOrDefault();
                if (search == null)
                {
                    result.Add(item);
                }



            }


            return result;
        }

        public async Task<ResultDto<List<TreePUC>>> GetTreeTitulosRespaldo()
        {



            List<TreePUC> listTreePUC = new List<TreePUC>();
            ResultDto<List<TreePUC>> result = new ResultDto<List<TreePUC>>(null);
            try
            {


                var treePuc = await WriteStrings();

                foreach (var item in treePuc)
                {
                    var arraIcp = item.Split(":");
                    var id = arraIcp[2];
                    var parentId = arraIcp[3];
                    List<string> icpString = new List<string>();
                    var match = icpString.FirstOrDefault(stringToCheck => stringToCheck.Contains(arraIcp[0]));
                    if (match == null)
                        icpString.Add(arraIcp[0]);

                    match = icpString.FirstOrDefault(stringToCheck => stringToCheck.Contains(arraIcp[1]));
                    if (match == null)
                        icpString.Add(arraIcp[1]);

                    /*match = icpString.FirstOrDefault(stringToCheck => stringToCheck.Contains(arraIcp[2]));
                    if (match == null)
                        icpString.Add(arraIcp[2]);
                    match = icpString.FirstOrDefault(stringToCheck => stringToCheck.Contains(arraIcp[3]));
                    if (match == null)
                        icpString.Add(arraIcp[3]);*/




                    TreePUC treePUC = new TreePUC();
                    treePUC.Path = icpString;
                    treePUC.Id = Int32.Parse(id);
                    treePUC.Denominacion = arraIcp[4];
                    treePUC.Descripcion = arraIcp[5];
                    var search = listTreePUC.Where(x => x.Id == treePUC.Id).FirstOrDefault();
                    if (search == null)
                    {
                        listTreePUC.Add(treePUC);
                    }




                }

                result.Data = listTreePUC.OrderBy(x => x.Id).ToList();
                result.IsValid = true;
                result.Message = $"";
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

        public async Task<ResultDto<CatTitulosResponseDto>> Create(CatTitulosUpdateDto dto)
        {

            ResultDto<CatTitulosResponseDto> result = new ResultDto<CatTitulosResponseDto>(null);
            try
            {

                
                if (dto.Titulo.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Titulo Invalido";
                    return result;
                }



                if (dto.TituloFkID > 0)
                {
                    var padre = await _repository.GetByTitulo(dto.TituloFkID);
                    if (padre == null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Padre Invalido";
                        return result;
                    }

                }
                var descriptivaCodigo = await _repository.GetByCodigoString(dto.CodigoTitulo);
                if (descriptivaCodigo != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Ya existe Titulo con ese codigo: {dto.CodigoTitulo}";
                    return result;
                }

                CAT_TITULOS entity = new CAT_TITULOS();
                entity.TITULO_ID = await _repository.GetNextKey();
                entity.TITULO_FK_ID = dto.TituloFkID;
                entity.TITULO = dto.Titulo;
                entity.CODIGO_TITULO = dto.CodigoTitulo;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                entity.EXTRA4 = dto.Extra4;
                entity.EXTRA5 = dto.Extra5;
                entity.EXTRA6 = dto.Extra6;
                entity.EXTRA7 = dto.Extra7;
                entity.EXTRA8 = dto.Extra8;
                entity.EXTRA9 = dto.Extra9;
                entity.EXTRA10 = dto.Extra10;
                entity.EXTRA11 = dto.Extra11;
                entity.EXTRA12 = dto.Extra12;
                entity.EXTRA13 = dto.Extra13;
                entity.EXTRA14 = dto.Extra14;
                entity.EXTRA15 = dto.Extra15;

                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;


                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = MapCatTitulo (created.Data);
                    result.Data = resultDto;
                    result.IsValid = true;
                    result.Message = "";


                }
                else
                {

                    result.Data = null;
                    result.IsValid = created.IsValid;
                    result.Message = created.Message;
                }

                return result;



            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }

        public async Task<ResultDto<CatTitulosResponseDto>> Update(CatTitulosUpdateDto dto)
        {

            ResultDto<CatTitulosResponseDto> result = new ResultDto<CatTitulosResponseDto>(null);
            try
            {

                var tituloUpdate = await _repository.GetByTitulo(dto.TituloId);
                if (tituloUpdate == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Titulo no existe";
                    return result;
                }
                if (dto.Titulo.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Titulo Invalido";
                    return result;
                }



                if (dto.TituloFkID > 0)
                {
                    var padre = await _repository.GetByTitulo(dto.TituloFkID);
                    if (padre == null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Padre Invalido";
                        return result;
                    }

                }
                if (dto.CodigoTitulo.Length > 0)
                {
                    var tituloCodigo = await _repository.GetByCodigoString(dto.CodigoTitulo);
                    if (tituloCodigo != null && tituloCodigo.TITULO_ID != dto.TituloId)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = $"Ya existe titulo con ese codigo: {dto.CodigoTitulo}";
                        return result;
                    }

                }
                //No se permite modificar ni Extra1 ni Codigo
                //descriptiva.EXTRA1 = dto.Extra1;
                //descriptiva.CODIGO = dto.Codigo;

                tituloUpdate.TITULO = dto.Titulo;
                tituloUpdate.TITULO_FK_ID = dto.TituloFkID;


                tituloUpdate.EXTRA2 = dto.Extra2;
                tituloUpdate.EXTRA3 = dto.Extra3;
                tituloUpdate.FECHA_UPD = DateTime.Now;




                var conectado = await _sisUsuarioRepository.GetConectado();
                tituloUpdate.CODIGO_EMPRESA = conectado.Empresa;
                tituloUpdate.USUARIO_UPD = conectado.Usuario;

                await _repository.Update(tituloUpdate);

                var resultDto = MapCatTitulo(tituloUpdate);
                result.Data = resultDto;
                result.IsValid = true;
                result.Message = "";

            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }

        public async Task<ResultDto<CatTitulosDeleteDto>> Delete(CatTitulosDeleteDto dto)
        {

            ResultDto<CatTitulosDeleteDto> result = new ResultDto<CatTitulosDeleteDto>(null);
            try
            {

                var titulo = await _repository.GetByTitulo(dto.TituloId);
                if (titulo == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Titulo no existe";
                    return result;
                }

                var descriptiva = await _catDescriptivasRepository.GetByTitulo(dto.TituloId);
                if (descriptiva != null && descriptiva.Count > 0)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = $"Titulo esta asociado a una  descriptiva";
                    return result;
                }


                var deleted = await _repository.Delete(dto.TituloId);

                if (deleted.Length > 0)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = deleted;
                }
                else
                {
                    result.Data = dto;
                    result.IsValid = true;
                    result.Message = deleted;

                }


            }
            catch (Exception ex)
            {
                result.Data = dto;
                result.IsValid = false;
                result.Message = ex.Message;
            }

            return result;
        }

    }
}
