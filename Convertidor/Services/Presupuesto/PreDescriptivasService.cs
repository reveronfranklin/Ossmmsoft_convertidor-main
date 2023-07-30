using System;
using System.Collections.Generic;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Services.Rh;
using NuGet.Packaging;

namespace Convertidor.Services.Presupuesto
{
	public class PreDescriptivasService: IPreDescriptivasService
    {

      
        private readonly IPreDescriptivaRepository _repository;
        private readonly IPreTitulosRepository _preTitulosRepository;
        private readonly IConfiguration _configuration;
        public PreDescriptivasService(IPreDescriptivaRepository repository,
                                      IPreTitulosRepository preTitulosRepository,
                                      IConfiguration configuration)
		{
            _repository = repository;
            _configuration = configuration;
            _preTitulosRepository = preTitulosRepository;

        }

        public async Task<ResultDto<List<PreDescriptivasGetDto>>> GetAll()
        {

            ResultDto<List<PreDescriptivasGetDto>> result = new ResultDto<List<PreDescriptivasGetDto>>(null);
            try
            {

                var titulos = await _repository.GetAll();



                if (titulos.Count() > 0)
                {
                    List<PreDescriptivasGetDto> listDto = new List<PreDescriptivasGetDto>();

                    foreach (var item in titulos)
                    {
                        PreDescriptivasGetDto dto = new PreDescriptivasGetDto();
                        dto.DescripcionId = item.DESCRIPCION_ID;
                        dto.DescripcionIdFk = item.DESCRIPCION_FK_ID;
                        dto.Descripcion = item.DESCRIPCION;
                        if (item.CODIGO == null) item.CODIGO = "";
                        dto.Codigo = item.CODIGO;
                        dto.TituloId = item.TITULO_ID;
                        dto.DescripcionTitulo = "";
                        var titulo = await _preTitulosRepository.GetByCodigo(item.TITULO_ID);
                        if(titulo!=null) dto.DescripcionTitulo = titulo.TITULO;

                        if (item.EXTRA1 == null) item.EXTRA1 = "";
                        if (item.EXTRA2 == null) item.EXTRA2 = "";
                        if (item.EXTRA3 == null) item.EXTRA3 = "";
                        dto.Extra1 = item.EXTRA1;
                        dto.Extra2 = item.EXTRA2;
                        dto.Extra3 = item.EXTRA3;

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
    





    }
}

