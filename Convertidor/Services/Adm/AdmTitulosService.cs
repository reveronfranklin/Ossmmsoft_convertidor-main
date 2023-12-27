using System;
using System.Collections.Generic;
using Convertidor.Data.Entities.ADM;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Adm;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository.Rh;
using Convertidor.Dtos;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Services.Rh;
using Convertidor.Utility;
using NPOI.POIFS.Properties;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NuGet.Packaging;

namespace Convertidor.Services.Adm
{
	public class AdmTituloService: IAdmTituloService
    {

      
        private readonly IAdmTitulosRepository _repository;
        private readonly IAdmDescriptivaRepository _repositoryPreDescriptiva;
        private readonly IConfiguration _configuration;
        public AdmTituloService(IAdmTitulosRepository repository,
                                      IConfiguration configuration,
                                      IAdmDescriptivaRepository repositoryPreDescriptiva)
		{
            _repository = repository;
            _configuration = configuration;
            _repositoryPreDescriptiva = repositoryPreDescriptiva;


        }


        public async Task<ResultDto<List<AdmTitulosGetDto>>> GetAll()
        {

            ResultDto<List<AdmTitulosGetDto>> result = new ResultDto<List<AdmTitulosGetDto>>(null);
            try
            {

                var titulos = await _repository.GetAll();

               

                if (titulos.Count() > 0)
                {
                    List<AdmTitulosGetDto> listDto = new List<AdmTitulosGetDto>();

                    foreach (var item in titulos)
                    {
                        AdmTitulosGetDto dto = new AdmTitulosGetDto();
                        dto.TituloId = item.TITULO_ID;
                        dto.TituloIdFk = item.TITULO_FK_ID;
                        dto.Titulo = item.TITULO;
                        dto.Codigo = item.CODIGO;
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
                itenNew.Id = item.TITULO_ID;

                itenNew.ParentId = (int)item.TITULO_FK_ID;
                var patchEvaluado = item.TITULO ;

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

        public async Task<ResultDto<AdmTitulosGetDto>> Update(AdmTitulosUpdateDto dto)
        {

            ResultDto<AdmTitulosGetDto> result = new ResultDto<AdmTitulosGetDto>(null);
            try
            {

                var tituloUpdate = await _repository.GetByCodigo(dto.TituloId);
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
                    result.Message = "Titulo Invalida";
                    return result;
                }

            

                if (dto.TituloIdFk > 0)
                {
                    var padre = await _repository.GetByCodigo(dto.TituloIdFk);
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
                    var tituloCodigo = await _repository.GetByCodigoString(dto.Codigo);
                    if (tituloCodigo != null && tituloCodigo.TITULO_ID != dto.TituloId)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = $"Ya existe titulo con ese codigo: {dto.Codigo}";
                        return result;
                    }

                }
                //No se permite modificar ni Extra1 ni Codigo
                //descriptiva.EXTRA1 = dto.Extra1;
                //descriptiva.CODIGO = dto.Codigo;

                tituloUpdate.TITULO = dto.Titulo;
                tituloUpdate.TITULO_FK_ID = dto.TituloIdFk;


                tituloUpdate.EXTRA2 = dto.Extra2;
                tituloUpdate.EXTRA3 = dto.Extra3;
                tituloUpdate.FECHA_UPD = DateTime.Now;




                await _repository.Update(tituloUpdate);

                var resultDto = MapPreTitulo(tituloUpdate);
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

        public async Task<ResultDto<AdmTitulosGetDto>> Create(AdmTitulosUpdateDto dto)
        {

            ResultDto<AdmTitulosGetDto> result = new ResultDto<AdmTitulosGetDto>(null);
            try
            {

                var titulo = await _repository.GetByCodigo(dto.TituloId);
                if (titulo != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Titulo existe";
                    return result;
                }
                if (dto.Titulo.Trim().Length <= 0)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Titulo Invalida";
                    return result;
                }

            

                if (dto.TituloIdFk > 0)
                {
                    var padre = await _repository.GetByCodigo(dto.TituloIdFk);
                    if (padre == null)
                    {
                        result.Data = null;
                        result.IsValid = false;
                        result.Message = "Padre Invalido";
                        return result;
                    }

                }
                var descriptivaCodigo = await _repository.GetByCodigoString(dto.Codigo);
                if (descriptivaCodigo != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Ya existe Titulo con ese codigo: {dto.Codigo}";
                    return result;
                }

                ADM_TITULOS entity = new ADM_TITULOS();
                entity.TITULO_ID = await _repository.GetNextKey();
                entity.TITULO_FK_ID = dto.TituloIdFk;
           
                entity.CODIGO = dto.Codigo;
                entity.TITULO = dto.Titulo;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;
                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = MapPreTitulo(created.Data);
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
 
        public async Task<ResultDto<AdmTitulosDeleteDto>> Delete(AdmTitulosDeleteDto dto)
        {

            ResultDto<AdmTitulosDeleteDto> result = new ResultDto<AdmTitulosDeleteDto>(null);
            try
            {

                var titulo = await _repository.GetByCodigo(dto.TituloId);
                if (titulo == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Titulo no existe";
                    return result;
                }

                var descriptiva = await  _repositoryPreDescriptiva.GetByTitulo(dto.TituloId);
                if (descriptiva != null && descriptiva.Count>0)
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

        public AdmTitulosGetDto MapPreTitulo(ADM_TITULOS entity)
        {
            AdmTitulosGetDto dto = new AdmTitulosGetDto();
            dto.TituloId = entity.TITULO_ID;
            dto.TituloIdFk = entity.TITULO_FK_ID;
            dto.Titulo = entity.TITULO;
            if (entity.CODIGO == null) entity.CODIGO = "";
            dto.Codigo = entity.CODIGO;
            dto.TituloId = entity.TITULO_ID;
            if (entity.EXTRA1 == null) entity.EXTRA1 = "";
            if (entity.EXTRA2 == null) entity.EXTRA2 = "";
            if (entity.EXTRA3 == null) entity.EXTRA3 = "";
            dto.Extra1 = entity.EXTRA1;
            dto.Extra2 = entity.EXTRA2;
            dto.Extra3 = entity.EXTRA3;

            return dto;

        }



        public async Task<ResultDto<List<TreePUC>>> GetTreeTitulos()
        {

            ResultDto<List<TreePUC>> result = new ResultDto<List<TreePUC>>(null);


            try
            {


                List<TreePUC> listTreePUC2 = new List<TreePUC>();
                var titulosArbol = await BuscarArbol();
              
                foreach (var item in titulosArbol)
                {
                    var patch = getPatch(item);
                  

                    TreePUC treePUC = new TreePUC();
                    treePUC.Path = patch;
                    treePUC.Id = item.Id;
                    treePUC.Denominacion = item.Text;
                    treePUC.Descripcion = item.Text;
                    var search = listTreePUC2.Where(x => x.Id == treePUC.Id).FirstOrDefault();
                    if (search == null)
                    {
                        listTreePUC2.Add(treePUC);
                    }

                    Console.WriteLine(patch);
                }
                result.Data = listTreePUC2.OrderBy(x => x.Id).ToList();
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

     

        private async Task<List<Comment>> BuscarArbol()
        {
            List<Comment> categories = new List<Comment>();
            var descriptivas = await _repository.GetAll();
            foreach (var item in descriptivas)
            {
                Comment itenNew = new Comment();
                itenNew.Id = item.TITULO_ID;
                itenNew.ParentId = (int)item.TITULO_FK_ID;
                if (item.TITULO == null) item.TITULO = "";
                itenNew.Text = item.TITULO;
                categories.Add(itenNew);

            }

            List<Comment> hierarchy = new List<Comment>();
            hierarchy = categories
                            //.Where(c => c.ParentId != 0)
                            .Select(c => new Comment()
                            {
                                Id = c.Id,
                                Text = c.Text,
                                ParentId = c.ParentId,
                                //hierarchy = "0000" + c.Id,
                                hierarchy =  c.Text,
                                Children = GetParent(categories, c)
                            })
                            .ToList();
         

           return hierarchy.OrderBy(x=>x.Id).ToList();
            

        }

        public List<string> getPatch(Comment item)
        {
            List<string> result = new List<string>();
            if (item.Children.Count == 0)
            {
                result.Add(item.Text);
                return result;
            }
            else
            {
                
                foreach (var itemChield in item.Children)
                {
                    result.Add(itemChield.Text);

                }
                return result;
            }
          

        }

        public List<Comment> GetParent(List<Comment> comments, Comment comment)
        {
            if (comment.Id == 15)
            {
                var detener = 1;
            }

            List<Comment> result = new List<Comment>();

            if (comment.ParentId == 0) {
                 result.Add(comment);
                return result;
            }
            var padre = comments.Where(c => c.Id == comment.ParentId).FirstOrDefault();
            if (padre != null)
            {
                if (padre.ParentId == 0)
                {
                    result.Add(padre);
                    result.Add(comment);
                    return result;

                }
                else
                {
                    result.AddRange(GetParent(comments, padre));
                    result.Add(comment);
                    return result;
                }
            }
            else
            {
                result.Add(comment);
                return result;
            }
        }

   



    }
}

