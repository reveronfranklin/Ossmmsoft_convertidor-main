using System;
using System.Collections.Generic;
using System.Globalization;
using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository.Presupuesto;
using Convertidor.Data.Repository.Rh;
using Convertidor.Dtos;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Services.Rh;
using NuGet.Packaging;

namespace Convertidor.Services.Adm
{
	public class AdmDescriptivasService: IAdmDescriptivasService
    {

      
        private readonly IAdmDescriptivaRepository _repository;
        private readonly IAdmTitulosRepository _preTitulosRepository;
        private readonly IConfiguration _configuration;
        public AdmDescriptivasService(IAdmDescriptivaRepository repository,
            IAdmTitulosRepository preTitulosRepository,
                                      IConfiguration configuration)
		{
            _repository = repository;
            _configuration = configuration;
            _preTitulosRepository = preTitulosRepository;

        }

        public async Task<ResultDto<List<AdmDescriptivasGetDto>>> GetAll()
        {

            ResultDto<List<AdmDescriptivasGetDto>> result = new ResultDto<List<AdmDescriptivasGetDto>>(null);
            try
            {

                var titulos = await _repository.GetAll();



                if (titulos.Count() > 0)
                {
                    List<AdmDescriptivasGetDto> listDto = new List<AdmDescriptivasGetDto>();

                    foreach (var item in titulos)
                    {
                        AdmDescriptivasGetDto dto = new AdmDescriptivasGetDto();
                        dto = await MapPreDecriptiva(item);

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


        public async Task<ResultDto<List<AdmDescriptivasGetDto>>> GetByTitulo(int tituloId)
        {

            ResultDto<List<AdmDescriptivasGetDto>> result = new ResultDto<List<AdmDescriptivasGetDto>>(null);
            try
            {

                var titulos = await _repository.GetByTitulo(tituloId);



                if (titulos.Count() > 0)
                {
                    List<AdmDescriptivasGetDto> listDto = new List<AdmDescriptivasGetDto>();

                    foreach (var item in titulos)
                    {
                        AdmDescriptivasGetDto dto = new AdmDescriptivasGetDto();
                        dto = await MapPreDecriptiva(item);

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
        public async Task<ResultDto<List<AdmDescriptivasGetDto>>> GetByCodigoTitulo(string codigo)
        {

            ResultDto<List<AdmDescriptivasGetDto>> result = new ResultDto<List<AdmDescriptivasGetDto>>(null);
            try
            {




                var titulo = await _preTitulosRepository.GetByCodigoString(codigo);


                var titulos = await _repository.GetByTitulo(titulo.TITULO_ID);
                if (titulos.Count() > 0)
                {
                    List<AdmDescriptivasGetDto> listDto = new List<AdmDescriptivasGetDto>();

                    AdmDescriptivasGetDto itemDefault = new AdmDescriptivasGetDto();
                    itemDefault.DescripcionId = 0;
                    itemDefault.DescripcionIdFk = 0;
                    itemDefault.Descripcion = "Seleccione";
                    itemDefault.Codigo = "";
                    itemDefault.TituloId = 0;
                    itemDefault.DescripcionTitulo = "";
                    itemDefault.Extra1 = "";
                    itemDefault.Extra2 = "";
                    itemDefault.Extra3 = "";

                    List<AdmDescriptivasGetDto> lista = new List<AdmDescriptivasGetDto>();
                    lista.Add(GetDefaultDecriptiva());
                    itemDefault.ListaDescriptiva = lista;

                    listDto.Add(itemDefault);





                    foreach (var item in titulos)
                    {
                        AdmDescriptivasGetDto dto = new AdmDescriptivasGetDto();
                        dto = await MapPreDecriptiva(item);

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
                var patchEvaluado = item.DESCRIPCION ;

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

        public async Task<ResultDto<List<TreePUC>>> GetTreeDecriptiva()
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
                    if (search==null)
                    {
                        listTreePUC.Add(treePUC);
                    }
                   



                }
               
                result.Data = listTreePUC.OrderBy(x=>x.Id).ToList();
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


        public async Task<ResultDto<AdmDescriptivasGetDto>> Update(AdmDescriptivasUpdateDto dto)
        {

            ResultDto<AdmDescriptivasGetDto> result = new ResultDto<AdmDescriptivasGetDto>(null);
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

                var titulo = await _preTitulosRepository.GetByCodigo(dto.TituloId);
                if (titulo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Titulo Invalido";
                    return result;
                }

                if (dto.DescripcionIdFk > 0)
                {
                    var padre = await _repository.GetByCodigo(dto.DescripcionIdFk);
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
                descriptiva.DESCRIPCION_FK_ID = dto.DescripcionIdFk;
                descriptiva.TITULO_ID = dto.TituloId;
               
                descriptiva.EXTRA2 = dto.Extra2;
                descriptiva.EXTRA3 = dto.Extra3;
                descriptiva.FECHA_UPD = DateTime.Now;

              
                

                await _repository.Update(descriptiva);

                var resultDto = await MapPreDecriptiva(descriptiva);
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

        public async Task<ResultDto<AdmDescriptivasGetDto>> Create(AdmDescriptivasUpdateDto dto)
        {

            ResultDto<AdmDescriptivasGetDto> result = new ResultDto<AdmDescriptivasGetDto>(null);
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

                var titulo = await _preTitulosRepository.GetByCodigo(dto.TituloId);
                if (titulo == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Titulo Invalido";
                    return result;
                }

                if (dto.DescripcionIdFk > 0)
                {
                    var padre = await _repository.GetByCodigo(dto.DescripcionIdFk);
                    if (padre == null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Padre Invalido";
                        return result;
                    }

                }
                var descriptivaCodigo = await _repository.GetByCodigoDescriptivaTexto(dto.Codigo);
                if (descriptivaCodigo != null )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Ya existe Descriptiva con ese codigo: {dto.Codigo}";
                    return result;
                }

                ADM_DESCRIPTIVAS entity = new ADM_DESCRIPTIVAS();        
                entity.DESCRIPCION_ID = await _repository.GetNextKey();
                entity.DESCRIPCION_FK_ID = dto.DescripcionIdFk;
                entity.TITULO_ID = dto.TituloId;
                entity.CODIGO = dto.Codigo;
                entity.DESCRIPCION = dto.Descripcion;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data!=null)
                {
                    var resultDto = await MapPreDecriptiva(created.Data);
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

        public AdmDescriptivasGetDto GetDefaultDecriptiva()
        {
            AdmDescriptivasGetDto itemDefault = new AdmDescriptivasGetDto();
            itemDefault.DescripcionId = 0;
            itemDefault.DescripcionIdFk = 0;
            itemDefault.Descripcion = "Seleccione";
            itemDefault.Codigo = "";
            itemDefault.TituloId = 0;
            itemDefault.DescripcionTitulo = "";
            itemDefault.Extra1 = "";
            itemDefault.Extra2 = "";
            itemDefault.Extra3 = "";
            return itemDefault;
          
        }

        public async Task<AdmDescriptivasGetDto> MapPreDecriptiva(ADM_DESCRIPTIVAS entity)
        {
            AdmDescriptivasGetDto dto = new AdmDescriptivasGetDto();
            dto.DescripcionId = entity.DESCRIPCION_ID;
            dto.DescripcionIdFk = entity.DESCRIPCION_FK_ID;
            dto.Descripcion = entity.DESCRIPCION;
            if (entity.CODIGO == null) entity.CODIGO = "";
            dto.Codigo = entity.CODIGO;
            dto.TituloId = entity.TITULO_ID;
            dto.DescripcionTitulo = "";
            var titulo = await _preTitulosRepository.GetByCodigo(entity.TITULO_ID);
            if (titulo != null) dto.DescripcionTitulo = titulo.TITULO;

            if (entity.EXTRA1 == null) entity.EXTRA1 = "";
            if (entity.EXTRA2 == null) entity.EXTRA2 = "";
            if (entity.EXTRA3 == null) entity.EXTRA3 = "";
            dto.Extra1 = entity.EXTRA1;
            dto.Extra2 = entity.EXTRA2;
            dto.Extra3 = entity.EXTRA3;



            List<AdmDescriptivasGetDto> listDto = new List<AdmDescriptivasGetDto>();
            var hijos = await _repository.GetByFKID(entity.DESCRIPCION_ID);
            if (hijos.Count > 0)
            {
                AdmDescriptivasGetDto itemDefault = new AdmDescriptivasGetDto();
                itemDefault = GetDefaultDecriptiva();
                List<AdmDescriptivasGetDto> lista = new List<AdmDescriptivasGetDto>();
                lista.Add(GetDefaultDecriptiva());
                itemDefault.ListaDescriptiva= lista;


                listDto.Add(itemDefault);
                foreach (var item in hijos)
                {
                    AdmDescriptivasGetDto itemDto = new AdmDescriptivasGetDto();
                    itemDto.DescripcionId = item.DESCRIPCION_ID;
                    itemDto.DescripcionIdFk = item.DESCRIPCION_FK_ID;
                    itemDto.Descripcion = item.DESCRIPCION;
                    if (item.CODIGO == null) item.CODIGO = "";
                    itemDto.Codigo = item.CODIGO;
                    itemDto.TituloId = item.TITULO_ID;
                    itemDto.DescripcionTitulo = "";
                    var tituloItem = await _preTitulosRepository.GetByCodigo(item.TITULO_ID);
                    if (tituloItem != null) itemDto.DescripcionTitulo = tituloItem.TITULO;

                    if (item.EXTRA1 == null) item.EXTRA1 = "";
                    if (item.EXTRA2 == null) item.EXTRA2 = "";
                    if (item.EXTRA3 == null) item.EXTRA3 = "";
                    itemDto.Extra1 = item.EXTRA1;
                    itemDto.Extra2 = item.EXTRA2;
                    itemDto.Extra3 = item.EXTRA3;
                    listDto.Add(itemDto);
                }
                dto.ListaDescriptiva = listDto;
            }
            else
            {
                AdmDescriptivasGetDto itemDefault = new AdmDescriptivasGetDto();
                itemDefault = GetDefaultDecriptiva();
                List<AdmDescriptivasGetDto> lista = new List<AdmDescriptivasGetDto>();
                lista.Add(GetDefaultDecriptiva());
                itemDefault.ListaDescriptiva = lista;


                listDto.Add(itemDefault);
                dto.ListaDescriptiva = listDto;
            }


            return dto;

        }


        public async Task<ResultDto<AdmDescriptivaDeleteDto>> Delete(AdmDescriptivaDeleteDto dto)
        {

            ResultDto<AdmDescriptivaDeleteDto> result = new ResultDto<AdmDescriptivaDeleteDto>(null);
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

