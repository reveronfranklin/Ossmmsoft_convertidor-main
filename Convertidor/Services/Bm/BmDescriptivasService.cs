using System;
using System.Collections.Generic;
using System.Globalization;
using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Data.Repository.Rh;
using Convertidor.Dtos.Bm;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace Convertidor.Services.Bm
{
    public class BmDescriptivasService: IBmDescriptivasService
    {

      
        private readonly IBmDescriptivaRepository _repository;
        private readonly IBmTitulosRepository _bMTitulosRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IConfiguration _configuration;
        public BmDescriptivasService(IBmDescriptivaRepository repository,
                                      IBmTitulosRepository bMTitulosRepository,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      IConfiguration configuration)
		{
            _repository = repository;
            _configuration = configuration;
            _bMTitulosRepository = bMTitulosRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
        }

        public async Task<ResultDto<List<BmDescriptivasResponseDto>>> GetAll()
        {

            ResultDto<List<BmDescriptivasResponseDto>> result = new ResultDto<List<BmDescriptivasResponseDto>>(null);
            try
            {

                var titulos = await _repository.GetAll();



                if (titulos.Count() > 0)
                {
                    List<BmDescriptivasResponseDto> listDto = new List<BmDescriptivasResponseDto>();

                    foreach (var item in titulos)
                    {
                        BmDescriptivasResponseDto dto = new BmDescriptivasResponseDto();
                        dto = await MapBmDescriptiva(item);

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
        public async Task<bool> GetByIdAndTitulo(int tituloId, int id)
        {
            try
            {
                
                var result = await _repository.GetByIdAndTitulo(tituloId, id);

                return result;
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return false;
            }

        }
        public async Task<List<SelectListDescriptiva>> GetByTitulo(int tituloId)
        {

            List<SelectListDescriptiva> result = new List<SelectListDescriptiva>();
            try
            {
                var descriptivas = await _repository.GetByTituloId(tituloId);
             
                if (descriptivas.Count>0)
                {

                    foreach (var item in descriptivas)
                    {
                        
                        SelectListDescriptiva resultItem  = new SelectListDescriptiva();
                        
                        resultItem.Id = item.DESCRIPCION_ID;
                        
                        resultItem.Descripcion = item.DESCRIPCION;
                        result.Add(resultItem);
                        
                        
                    }
                }


                return result.OrderBy(x=>x.Descripcion).ToList();
            }
            catch (Exception ex)
            {
                var res = ex.InnerException.Message;
                return null;
            }

        }
        public async Task<ResultDto<List<BmDescriptivasResponseDto>>> GetByTituloBk(int tituloId)
        {

            ResultDto<List<BmDescriptivasResponseDto>> result = new ResultDto<List<BmDescriptivasResponseDto>>(null);
            try
            {

                var titulos = await _repository.GetByTituloId(tituloId);



                if (titulos.Count() > 0)
                {
                    List<BmDescriptivasResponseDto> listDto = new List<BmDescriptivasResponseDto>();

                    foreach (var item in titulos)
                    {
                        BmDescriptivasResponseDto dto = new BmDescriptivasResponseDto();
                        dto = await MapBmDescriptiva(item);

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
        public async Task<ResultDto<List<BmDescriptivasResponseDto>>> GetByCodigoTitulo(string codigo)
        {

            ResultDto<List<BmDescriptivasResponseDto>> result = new ResultDto<List<BmDescriptivasResponseDto>>(null);
            try
            {




                var titulo = await _bMTitulosRepository.GetByCodigoString(codigo);


                var titulos = await _repository.GetByTituloId(titulo.TITULO_ID);
                if (titulos.Count() > 0)
                {
                    List<BmDescriptivasResponseDto> listDto = new List<BmDescriptivasResponseDto>();

                    BmDescriptivasResponseDto itemDefault = new BmDescriptivasResponseDto();
                    itemDefault.DescripcionId = 0;
                    itemDefault.DescripcionIdFk = 0;
                    itemDefault.Descripcion = "Seleccione";
                    itemDefault.Codigo = "";
                    itemDefault.TituloId = 0;
                    itemDefault.DescripcionTitulo = "";
                    itemDefault.Extra1 = "";
                    itemDefault.Extra2 = "";
                    itemDefault.Extra3 = "";

                    List<BmDescriptivasResponseDto> lista = new List<BmDescriptivasResponseDto>();
                    lista.Add(GetDefaultDecriptiva());
                    itemDefault.ListaDescriptiva = lista;

                    listDto.Add(itemDefault);





                    foreach (var item in titulos)
                    {
                        BmDescriptivasResponseDto dto = new BmDescriptivasResponseDto();
                        dto = await MapBmDescriptiva(item);

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

        public async Task<List<BmItem>> GetItems()
        {
            List<BmItem> result = new List<BmItem>();
            var descriptivas = await _repository.GetAll();

            foreach (var item in descriptivas)
            {
                BmItem itenNew = new BmItem();
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
            List<BmItem> items = await GetItems();


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

        public async Task<ResultDto<List<BmTreePUC>>> GetTreeDecriptiva()
        {

            List<BmTreePUC> listTreePUC = new List<BmTreePUC>();
            ResultDto<List<BmTreePUC>> result = new ResultDto<List<BmTreePUC>>(null);
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




                    BmTreePUC treePUC = new BmTreePUC();
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


        public async Task<ResultDto<BmDescriptivasResponseDto>> Update(BmDescriptivasUpdateDto dto)
        {

            ResultDto<BmDescriptivasResponseDto> result = new ResultDto<BmDescriptivasResponseDto>(null);
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

                var titulo = await _bMTitulosRepository.GetByCodigo(dto.TituloId);
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


                var conectado = await _sisUsuarioRepository.GetConectado();
                descriptiva.CODIGO_EMPRESA = conectado.Empresa;
                descriptiva.FECHA_UPD = DateTime.Now;
                descriptiva.USUARIO_UPD = conectado.Usuario;
              
                

                await _repository.Update(descriptiva);

                var resultDto = await MapBmDescriptiva(descriptiva);
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

        public async Task<ResultDto<BmDescriptivasResponseDto>> Create(BmDescriptivasUpdateDto dto)
        {

            ResultDto<BmDescriptivasResponseDto> result = new ResultDto<BmDescriptivasResponseDto>(null);
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

                var titulo = await _bMTitulosRepository.GetByCodigo(dto.TituloId);
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

                BM_DESCRIPTIVAS entity = new BM_DESCRIPTIVAS();        
                entity.DESCRIPCION_ID = await _repository.GetNextKey();
                entity.DESCRIPCION_FK_ID = dto.DescripcionIdFk;
                entity.TITULO_ID = dto.TituloId;
                entity.CODIGO = dto.Codigo;
                entity.DESCRIPCION = dto.Descripcion;
                entity.EXTRA1 = dto.Extra1;
                entity.EXTRA2 = dto.Extra2;
                entity.EXTRA3 = dto.Extra3;

                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data!=null)
                {
                    var resultDto = await MapBmDescriptiva(created.Data);
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

        public BmDescriptivasResponseDto GetDefaultDecriptiva()
        {
            BmDescriptivasResponseDto itemDefault = new BmDescriptivasResponseDto();
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

        public async Task<BmDescriptivasResponseDto> MapBmDescriptiva(BM_DESCRIPTIVAS entity)
        {
            BmDescriptivasResponseDto dto = new BmDescriptivasResponseDto();
            dto.DescripcionId = entity.DESCRIPCION_ID;
            dto.DescripcionIdFk = entity.DESCRIPCION_FK_ID;
            dto.Descripcion = entity.DESCRIPCION;
            if (entity.CODIGO == null) entity.CODIGO = "";
            dto.Codigo = entity.CODIGO;
            dto.TituloId = entity.TITULO_ID;
            dto.DescripcionTitulo = "";
            var titulo = await _bMTitulosRepository.GetByCodigo(entity.TITULO_ID);
            if (titulo != null) dto.DescripcionTitulo = titulo.TITULO;

            if (entity.EXTRA1 == null) entity.EXTRA1 = "";
            if (entity.EXTRA2 == null) entity.EXTRA2 = "";
            if (entity.EXTRA3 == null) entity.EXTRA3 = "";
            dto.Extra1 = entity.EXTRA1;
            dto.Extra2 = entity.EXTRA2;
            dto.Extra3 = entity.EXTRA3;



            List<BmDescriptivasResponseDto> listDto = new List<BmDescriptivasResponseDto>();
            var hijos = await _repository.GetByFKID(entity.DESCRIPCION_ID);
            if (hijos.Count > 0)
            {
                BmDescriptivasResponseDto itemDefault = new BmDescriptivasResponseDto();
                itemDefault = GetDefaultDecriptiva();
                List<BmDescriptivasResponseDto> lista = new List<BmDescriptivasResponseDto>();
                lista.Add(GetDefaultDecriptiva());
                itemDefault.ListaDescriptiva= lista;


                listDto.Add(itemDefault);
                foreach (var item in hijos)
                {
                    BmDescriptivasResponseDto itemDto = new BmDescriptivasResponseDto();
                    itemDto.DescripcionId = item.DESCRIPCION_ID;
                    itemDto.DescripcionIdFk = item.DESCRIPCION_FK_ID;
                    itemDto.Descripcion = item.DESCRIPCION;
                    if (item.CODIGO == null) item.CODIGO = "";
                    itemDto.Codigo = item.CODIGO;
                    itemDto.TituloId = item.TITULO_ID;
                    itemDto.DescripcionTitulo = "";
                    var tituloItem = await _bMTitulosRepository.GetByCodigo(item.TITULO_ID);
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
                BmDescriptivasResponseDto itemDefault = new BmDescriptivasResponseDto();
                itemDefault = GetDefaultDecriptiva();
                List<BmDescriptivasResponseDto> lista = new List<BmDescriptivasResponseDto>();
                lista.Add(GetDefaultDecriptiva());
                itemDefault.ListaDescriptiva = lista;


                listDto.Add(itemDefault);
                dto.ListaDescriptiva = listDto;
            }


            return dto;

        }


        public async Task<ResultDto<BmDescriptivaDeleteDto>> Delete(BmDescriptivaDeleteDto dto)
        {

            ResultDto<BmDescriptivaDeleteDto> result = new ResultDto<BmDescriptivaDeleteDto>(null);
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

