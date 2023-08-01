﻿using System;
using System.Collections.Generic;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Repository.Rh;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Services.Rh;
using NuGet.Packaging;

namespace Convertidor.Services.Presupuesto
{
	public class PreTituloService: IPreTituloService
    {

      
        private readonly IPreTitulosRepository _repository;
        private readonly IPreDescriptivaRepository _repositoryPreDescriptiva;
        private readonly IConfiguration _configuration;
        public PreTituloService(IPreTitulosRepository repository,
                                      IConfiguration configuration,
                                      IPreDescriptivaRepository repositoryPreDescriptiva)
		{
            _repository = repository;
            _configuration = configuration;
            _repositoryPreDescriptiva = repositoryPreDescriptiva;


        }


        public async Task<ResultDto<List<PreTitulosGetDto>>> GetAll()
        {

            ResultDto<List<PreTitulosGetDto>> result = new ResultDto<List<PreTitulosGetDto>>(null);
            try
            {

                var titulos = await _repository.GetAll();

               

                if (titulos.Count() > 0)
                {
                    List<PreTitulosGetDto> listDto = new List<PreTitulosGetDto>();

                    foreach (var item in titulos)
                    {
                        PreTitulosGetDto dto = new PreTitulosGetDto();
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

        public async Task<ResultDto<List<TreePUC>>> GetTreeTitulos()
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



        public async Task<ResultDto<PreTitulosGetDto>> Update(PreTitulosUpdateDto dto)
        {

            ResultDto<PreTitulosGetDto> result = new ResultDto<PreTitulosGetDto>(null);
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

        public async Task<ResultDto<PreTitulosGetDto>> Create(PreTitulosUpdateDto dto)
        {

            ResultDto<PreTitulosGetDto> result = new ResultDto<PreTitulosGetDto>(null);
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

                PRE_TITULOS entity = new PRE_TITULOS();
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
        public PreTitulosGetDto MapPreTitulo(PRE_TITULOS entity)
        {
            PreTitulosGetDto dto = new PreTitulosGetDto();
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


        public async Task<ResultDto<PreTitulosDeleteDto>> Delete(PreTitulosDeleteDto dto)
        {

            ResultDto<PreTitulosDeleteDto> result = new ResultDto<PreTitulosDeleteDto>(null);
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




    }
}

