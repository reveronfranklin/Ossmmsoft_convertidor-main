using Convertidor.Data.Entities.Cnt;
using Convertidor.Data.Interfaces.Cnt;
using Convertidor.Dtos.Cnt;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Cnt
{
    public class CntDescriptivasService: ICntDescriptivasService
    {

      
        private readonly ICntDescriptivaRepository _repository;
        private readonly ICntTitulosRepository _CntTitulosRepository;
        private readonly ISisUsuarioRepository _sisuarioRepository;
        private readonly IConfiguration _configuration;
        public CntDescriptivasService(ICntDescriptivaRepository repository,
            ICntTitulosRepository CntTitulosRepository,
            ISisUsuarioRepository sisuarioRepository,
                                      IConfiguration configuration)
		{
            _repository = repository;
            _configuration = configuration;
            _CntTitulosRepository = CntTitulosRepository;
            _sisuarioRepository = sisuarioRepository;
        }

        public async Task<CntDescriptivasResponseDto> MapCntDescriptiva(CNT_DESCRIPTIVAS entity)
        {
            CntDescriptivasResponseDto dto = new CntDescriptivasResponseDto();
            dto.DescripcionId = entity.DESCRIPCION_ID;
            dto.DescripcionFkId = entity.DESCRIPCION_FK_ID;
            dto.Descripcion = entity.DESCRIPCION;
            if (entity.CODIGO == null) entity.CODIGO = "";
            dto.Codigo = entity.CODIGO;
            dto.TituloId = entity.TITULO_ID;
            dto.Descripcion = "";
            var titulo = await _CntTitulosRepository.GetByCodigo(entity.TITULO_ID);
            if (titulo != null) dto.Descripcion = titulo.TITULO;

            if (entity.EXTRA1 == null) entity.EXTRA1 = "";
            if (entity.EXTRA2 == null) entity.EXTRA2 = "";
            if (entity.EXTRA3 == null) entity.EXTRA3 = "";
            dto.Extra1 = entity.EXTRA1;
            dto.Extra2 = entity.EXTRA2;
            dto.Extra3 = entity.EXTRA3;



            return dto;

        }
        public async Task<ResultDto<List<CntDescriptivasResponseDto>>> GetAll()
        {

            ResultDto<List<CntDescriptivasResponseDto>> result = new ResultDto<List<CntDescriptivasResponseDto>>(null);
            try
            {

                var titulos = await _repository.GetAll();



                if (titulos.Count() > 0)
                {
                    List<CntDescriptivasResponseDto> listDto = new List<CntDescriptivasResponseDto>();

                    foreach (var item in titulos)
                    {
                        CntDescriptivasResponseDto dto = new CntDescriptivasResponseDto();
                        dto = await MapCntDescriptiva(item);

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
                itenNew.Id = item.DESCRIPCION_ID;

                itenNew.ParentId = (int)item.DESCRIPCION_FK_ID;
                var patchEvaluado = item.DESCRIPCION;

                itenNew.Name = patchEvaluado;
                itenNew.Denominacion = item.DESCRIPCION;
                if (item.DESCRIPCION == null) item.DESCRIPCION = "";
                itenNew.Descripcion = item.DESCRIPCION;
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

        public async Task<ResultDto<List<TreePUC>>> GetTreeDescriptiva()
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

        public async Task<ResultDto<List<CntDescriptivasResponseDto>>> GetByTitulo(int tituloId)
        {

            ResultDto<List<CntDescriptivasResponseDto>> result = new ResultDto<List<CntDescriptivasResponseDto>>(null);
            try
            {

                var titulos = await _repository.GetByTitulo(tituloId);



                if (titulos.Count() > 0)
                {
                    List<CntDescriptivasResponseDto> listDto = new List<CntDescriptivasResponseDto>();

                    foreach (var item in titulos)
                    {
                        CntDescriptivasResponseDto dto = new CntDescriptivasResponseDto();
                        dto = await MapCntDescriptiva(item);

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
        public async Task<ResultDto<CntDescriptivasResponseDto>> Create(CntDescriptivasUpdateDto dto)
        {

            ResultDto<CntDescriptivasResponseDto> result = new ResultDto<CntDescriptivasResponseDto>(null);
            try
            {

                var descriptiva = await _repository.GetByCodigo(dto.DescripcionId);
                if (descriptiva != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Decriptiva existe";
                    return result;
                }
                if (dto.Descripcion.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
                    return result;
                }

                var titulo = await _CntTitulosRepository.GetByCodigo(dto.TituloId);
                if (titulo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Titulo Invalido";
                    return result;
                }

                if (dto.DescripcionFkId > 0)
                {
                    var padre = await _repository.GetByCodigo(dto.DescripcionFkId);
                    if (padre == null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Padre Invalido";
                        return result;
                    }

                }
                var descriptivaCodigo = await _repository.GetByCodigoDescriptivaTexto(dto.Codigo);
                if (descriptivaCodigo != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Ya existe Descriptiva con ese codigo: {dto.Codigo}";
                    return result;
                }

                CNT_DESCRIPTIVAS entity = new CNT_DESCRIPTIVAS();
                entity.DESCRIPCION_ID = await _repository.GetNextKey();
                entity.DESCRIPCION_FK_ID = dto.DescripcionFkId;
                entity.TITULO_ID = dto.TituloId;
                entity.CODIGO = dto.Codigo;
                entity.DESCRIPCION = dto.Descripcion;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;


                var conectado = await _sisuarioRepository.GetConectado();   

                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;
                var created = await _repository.Add(entity);
                
              

                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapCntDescriptiva(created.Data);
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


        public async Task<ResultDto<CntDescriptivasResponseDto>> Update(CntDescriptivasUpdateDto dto)
        {

            ResultDto<CntDescriptivasResponseDto> result = new ResultDto<CntDescriptivasResponseDto>(null);
            try
            {

                var descriptiva = await _repository.GetByCodigo(dto.DescripcionId);
                if (descriptiva == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Decriptiva no existe";
                    return result;
                }
                if (dto.Descripcion.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Descripcion Invalida";
                    return result;
                }

                var titulo = await _CntTitulosRepository.GetByCodigo(dto.TituloId);
                if (titulo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Titulo Invalido";
                    return result;
                }

                if (dto.DescripcionFkId > 0)
                {
                    var padre = await _repository.GetByCodigo(dto.DescripcionFkId);
                    if (padre == null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Padre Invalido";
                        return result;
                    }

                }
                if (dto.Codigo.Length > 0)
                {
                    var descriptivaCodigo = await _repository.GetByCodigoDescriptivaTexto(dto.Codigo);
                    if (descriptivaCodigo != null && descriptivaCodigo.CODIGO==dto.Codigo)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = $"Ya existe Descriptiva con ese codigo: {dto.Codigo}";
                        return result;
                    }

                }
                //No se permite modificar ni Extra1 ni Codigo
                //descriptiva.EXTRA1 = dto.Extra1;
                //descriptiva.CODIGO = dto.Codigo;

                descriptiva.DESCRIPCION = dto.Descripcion;
                descriptiva.DESCRIPCION_FK_ID = dto.DescripcionFkId;
                descriptiva.TITULO_ID = dto.TituloId;
               
                descriptiva.EXTRA2 = dto.Extra2;
                descriptiva.EXTRA3 = dto.Extra3;
                
                var conectado = await _sisuarioRepository.GetConectado();
                descriptiva.USUARIO_UPD = conectado.Usuario;
                descriptiva.CODIGO_EMPRESA = conectado.Empresa;
                descriptiva.FECHA_UPD = DateTime.Now;
                await _repository.Update(descriptiva);

                var resultDto = await MapCntDescriptiva(descriptiva);
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

        

        public CntDescriptivasResponseDto GetDefaultDecriptiva()
        {
            CntDescriptivasResponseDto itemDefault = new CntDescriptivasResponseDto();
            itemDefault.DescripcionId = 0;
            itemDefault.DescripcionFkId = 0;
            itemDefault.Descripcion = "Seleccione";
            itemDefault.Codigo = "";
            itemDefault.TituloId = 0;
            itemDefault.Descripcion = "";
            itemDefault.Extra1 = "";
            itemDefault.Extra2 = "";
            itemDefault.Extra3 = "";
            return itemDefault;
          
        }

       


        public async Task<ResultDto<CntDescriptivasDeleteDto>> Delete(CntDescriptivasDeleteDto dto)
        {

            ResultDto<CntDescriptivasDeleteDto> result = new ResultDto<CntDescriptivasDeleteDto>(null);
            try
            {

                var presupuesto = await _repository.GetByCodigo(dto.DescripcionId);
                if (presupuesto == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Descriptiva no existe";
                    return result;
                }

              
                var deleted = await _repository.Delete(dto.DescripcionId);

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

