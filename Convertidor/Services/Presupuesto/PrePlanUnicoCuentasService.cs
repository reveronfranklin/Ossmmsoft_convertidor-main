using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Utility;

namespace Convertidor.Services
{
    public class PrePlanUnicoCuentasService : IPrePlanUnicoCuentasService
    {
        
        private readonly IPRE_PLAN_UNICO_CUENTASRepository _repository;
     
        private readonly IPRE_PRESUPUESTOSRepository _presupuesttoRepository;
        private readonly IOssConfigRepository _ossConfigRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IPRE_ASIGNACIONESRepository _PRE_ASIGNACIONESRepository;

        public PrePlanUnicoCuentasService(IPRE_PLAN_UNICO_CUENTASRepository repository,
                                          IPRE_PRESUPUESTOSRepository presupuesttoRepository,
                                          IOssConfigRepository ossConfigRepository,
                                          IConfiguration configuration,
                                          IPRE_ASIGNACIONESRepository PRE_ASIGNACIONESRepository,
                                          IMapper mapper)
        {
            _repository = repository;
            _presupuesttoRepository = presupuesttoRepository;
            _ossConfigRepository = ossConfigRepository;
            _configuration = configuration;
            _PRE_ASIGNACIONESRepository = PRE_ASIGNACIONESRepository;
            _mapper = mapper;
        }


     


        public async Task<ResultDto<List<PrePlanUnicoCuentasGetDto>>> GetAllByCodigoPresupuesto(int codigoPresupuesto)
        {

            ResultDto<List<PrePlanUnicoCuentasGetDto>> result = new ResultDto<List<PrePlanUnicoCuentasGetDto>>(null);
            try
            {

                if (codigoPresupuesto == 0)
                {
                    var lastPresupuesto = await _presupuesttoRepository.GetLast();
                    if (lastPresupuesto != null) codigoPresupuesto = lastPresupuesto.CODIGO_PRESUPUESTO;
                }
              

                var puc = await _repository.GetAllByCodigoPresupuesto(codigoPresupuesto);
                if (puc.ToList().Count > 0)
                {





                    var qicpDto = from s in puc.ToList()
                                  group s by new
                                  {
                                      CodigoPuc = s.CODIGO_PUC,
                                     
                                      CodigoGrupo = s.CODIGO_GRUPO,
                                      CodigoNivel1 = s.CODIGO_NIVEL1,
                                      CodigoNivel2 = s.CODIGO_NIVEL2,
                                      CodigoNivel3 = s.CODIGO_NIVEL3,
                                      CodigoNivel4 = s.CODIGO_NIVEL4,
                                      CodigoNivel5 = s.CODIGO_NIVEL5,
                                      CodigoNivel6 = s.CODIGO_NIVEL6,
                                      Denominacion = s.DENOMINACION,
                                      Descripcion = s.DESCRIPCION,
                                      CodigoPresupuesto = s.CODIGO_PRESUPUESTO,
                                      CodigoPucPadre = s.CODIGO_PUC_FK
                                  } into g
                                  select new PrePlanUnicoCuentasGetDto
                                  {

                                      CodigoPuc = g.Key.CodigoPuc,
                                      CodigoGrupo = g.Key.CodigoGrupo,
                                      CodigoNivel1 = g.Key.CodigoNivel1,
                                      CodigoNivel2 = g.Key.CodigoNivel2,
                                      CodigoNivel3 = g.Key.CodigoNivel3,
                                      CodigoNivel4 = g.Key.CodigoNivel4,
                                      CodigoNivel5 = g.Key.CodigoNivel5,
                                      CodigoNivel6 = g.Key.CodigoNivel6,
                                      Denominacion = g.Key.Denominacion,
                                      Descripcion = g.Key.Descripcion,   
                                      CodigoPresupuesto = g.Key.CodigoPresupuesto,
                                      CodigoPucPadre = g.Key.CodigoPucPadre

                                  };


                    result.Data = qicpDto.OrderBy(x => x.CodigoGrupo)
                                         .ThenBy(x => x.CodigoNivel1)
                                         .ThenBy(x => x.CodigoNivel2)
                                         .ThenBy(x => x.CodigoNivel3)
                                         .ThenBy(x => x.CodigoNivel4)
                                         .ThenBy(x => x.CodigoNivel5)
                                         .ThenBy(x => x.CodigoNivel6)
                                         .ToList();
                    result.IsValid = true;
                    result.Message = $"";
                    return result;
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = $" No existen Datos";
                    return result;

                }



            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;

            }



        }
        private async Task<List<Comment>> BuscarArbol(int codigoPresupuesto)
        {
            List<Comment> categories = new List<Comment>();
            var descriptivas = await _repository.GetAllByCodigoPresupuesto(codigoPresupuesto);
            foreach (var item in descriptivas)
            {
                Comment itemNew = new Comment();
                itemNew.Id = item.CODIGO_PUC;
                itemNew.ParentId = (int)item.CODIGO_PUC_FK;
                var pucConcat = item.CODIGO_GRUPO + "-" + item.CODIGO_NIVEL1 + "-" + item.CODIGO_NIVEL2 + "-" + item.CODIGO_NIVEL3 + "-" + item.CODIGO_NIVEL4 + "-" + item.CODIGO_NIVEL5 + "-" + item.CODIGO_NIVEL6;
                itemNew.Text = pucConcat;
                itemNew.Denominacion = item.DENOMINACION;
                itemNew.Descripcion = item.DESCRIPCION;
                categories.Add(itemNew);

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
                                hierarchy = c.Text,
                                Children = GetParent(categories, c)
                            })
                            .ToList();


            return hierarchy.OrderBy(x => x.Id).ToList();


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

            List<Comment> result = new List<Comment>();

            if (comment.ParentId == 0)
            {
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
        public async Task<ResultDto<List<TreePUC>>> GetTreeByPresupuesto(int codigoPresupuesto)
        {

            List<TreePUC> listTreePUC = new List<TreePUC>();
            ResultDto<List<TreePUC>> result = new ResultDto<List<TreePUC>>(null);
            try
            {

                if (codigoPresupuesto==0)
                {
                    var lastPresupuesto = await _presupuesttoRepository.GetLast();
                    if (lastPresupuesto != null) codigoPresupuesto = lastPresupuesto.CODIGO_PRESUPUESTO;
                }



                List<TreePUC> listTreePUC2 = new List<TreePUC>();
                var titulosArbol = await BuscarArbol(codigoPresupuesto);

                foreach (var item in titulosArbol)
                {
                    var patch = getPatch(item);

                    var icp = await _repository.GetByCodigo(item.Id);
                    TreePUC treePUC = new TreePUC();
                    treePUC.Path = patch;
                    treePUC.Id = item.Id;
                    treePUC.Denominacion = icp.DENOMINACION;
                    treePUC.Descripcion = icp.DESCRIPCION;
                  
                    var search = listTreePUC2.Where(x => x.Id == treePUC.Id).FirstOrDefault();
                    if (search == null)
                    {
                        listTreePUC2.Add(treePUC);
                    }

                    Console.WriteLine(patch);
                }

                var treePuc = await WriteStrings(codigoPresupuesto);
                foreach (var item in treePuc)
                {
                    var arraIcp = item.Split(":");
                    var id = arraIcp[6];
                    var parentId = arraIcp[7];
                    List<string> icpString = new List<string>();
                    var match = icpString.FirstOrDefault(stringToCheck => stringToCheck.Contains(arraIcp[0]));
                    if (match == null)
                        icpString.Add(arraIcp[0]);

                    match = icpString.FirstOrDefault(stringToCheck => stringToCheck.Contains(arraIcp[1]));
                    if (match == null)
                        icpString.Add(arraIcp[1]);

                    match = icpString.FirstOrDefault(stringToCheck => stringToCheck.Contains(arraIcp[2]));
                    if (match == null)
                        icpString.Add(arraIcp[2]);
                    match = icpString.FirstOrDefault(stringToCheck => stringToCheck.Contains(arraIcp[3]));
                    if (match == null)
                        icpString.Add(arraIcp[3]);
                    match = icpString.FirstOrDefault(stringToCheck => stringToCheck.Contains(arraIcp[4]));
                    if (match == null)
                        icpString.Add(arraIcp[4]);
                    match = icpString.FirstOrDefault(stringToCheck => stringToCheck.Contains(arraIcp[5]));
                    if (match == null)
                        icpString.Add(arraIcp[5]);

                   

                    TreePUC treePUC = new TreePUC();
                    treePUC.Path = icpString;
                    treePUC.Id = Int32.Parse(id);
                    treePUC.Denominacion = arraIcp[8];
                    treePUC.Descripcion = arraIcp[9];

                    listTreePUC.Add(treePUC);



                }
                //listTreePUC = await ListTreeByPresupuesto(codigoPresupuesto);
                result.Data = listTreePUC2;
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

        public async Task<ResultDto<List<PrePlanUnicoCuentasGetDto>>> UpdatePucPadre(int codigoPresupuesto)
        {

            ResultDto<List<PrePlanUnicoCuentasGetDto>> result = new ResultDto<List<PrePlanUnicoCuentasGetDto>>(null);
            try
            {


                if (codigoPresupuesto == 0)
                {
                    var lastPresupuesto = await _presupuesttoRepository.GetLast();
                    if (lastPresupuesto != null) codigoPresupuesto = lastPresupuesto.CODIGO_PRESUPUESTO;
                }

                var puc = await GetAllByCodigoPresupuesto(codigoPresupuesto);

                var presupuestoObj = await _presupuesttoRepository.GetByCodigo(13,codigoPresupuesto);

                
                var pucList = puc.Data.Where(x=>x.CodigoPucPadre==null).ToList();
              
                if (pucList.Count > 0)
                {

                    foreach (var item in pucList)
                    {
                        var icpObj = await _repository.GetByCodigo(item.CodigoPuc);
                        if (icpObj != null)
                        {
                            PRE_PLAN_UNICO_CUENTAS padre = new PRE_PLAN_UNICO_CUENTAS();
                            FilterPrePUCPresupuestoCodigos filter = new FilterPrePUCPresupuestoCodigos();
                            filter.CodigoPresupuesto = item.CodigoPresupuesto;
                            filter.CodigoPuc = item.CodigoPuc;
                            filter.CodigoGrupo = item.CodigoGrupo;
                            filter.CodicoNivel1 = item.CodigoNivel1;
                            filter.CodicoNivel2 = item.CodigoNivel2;
                            filter.CodicoNivel3 = item.CodigoNivel3;
                            filter.CodicoNivel4 = item.CodigoNivel4;
                            filter.CodicoNivel5 = item.CodigoNivel5;
                            filter.CodicoNivel6 = item.CodigoNivel6;

                         
                            var newPadre = await GetPucPadre(filter);


                            if (newPadre != null)
                            {
                               
                                if (icpObj.CODIGO_PUC == newPadre.CODIGO_PUC)
                                {
                                    icpObj.CODIGO_PUC_FK = 0;
                                }
                                else
                                {
                                    icpObj.CODIGO_PUC_FK = newPadre.CODIGO_PUC;
                                }
                            }
                            else
                            {
                                icpObj.CODIGO_PUC_FK = 0;
                            }
                            await _repository.Update(icpObj);


                        }
                    }



                    result= await GetAllByCodigoPresupuesto(codigoPresupuesto);
                   
                    return result;
                }



                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = $" No existen Datos";
                    return result;

                }



            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<PRE_PLAN_UNICO_CUENTAS> GetPucPadre(FilterPrePUCPresupuestoCodigos dto)
        {

            PRE_PLAN_UNICO_CUENTAS padre = new PRE_PLAN_UNICO_CUENTAS();
            try
            {


                if (dto != null)
                {
                           

                            if (dto.CodicoNivel5 != "00")
                            {
                                padre = await _repository.GetHastaNivel4((int)dto.CodigoPresupuesto,
                                                                           (int)dto.CodigoPuc,
                                                                           dto.CodigoGrupo,
                                                                           dto.CodicoNivel1,
                                                                           dto.CodicoNivel2,
                                                                           dto.CodicoNivel3,
                                                                           "00");
                            }
                            else if (dto.CodicoNivel4 != "00")
                            {
                                padre = await _repository.GetHastaNivel3((int)dto.CodigoPresupuesto,
                                                                           (int)dto.CodigoPuc,
                                                                           dto.CodigoGrupo,
                                                                           dto.CodicoNivel1,
                                                                           dto.CodicoNivel2,
                                                                           "00");
                            }
                            else if (dto.CodicoNivel3 != "00")
                            {
                                padre = await _repository.GetHastaNivel2((int)dto.CodigoPresupuesto,
                                                                           (int)dto.CodigoPuc,
                                                                           dto.CodigoGrupo,
                                                                           dto.CodicoNivel1,
                                                                           "00");
                            }
                            else if (dto.CodicoNivel2 != "00")
                            {
                                padre = await _repository.GetHastaNivel1((int)dto.CodigoPresupuesto,
                                                                           (int)dto.CodigoPuc,
                                                                           dto.CodigoGrupo,
                                                                           "00");

                            }
                            else if (dto.CodicoNivel1 != "00")
                            {
                                padre = await _repository.GetHastaGrupoDiferentePuc((int)dto.CodigoPresupuesto,
                                                                           (int)dto.CodigoPuc,
                                                                           dto.CodigoGrupo);

                            }else if (dto.CodigoGrupo != "0")
                    {
                        padre = await _repository.GetHastaNivel5((int)dto.CodigoPresupuesto, (int)dto.CodigoPuc, dto.CodigoGrupo, "00", "00", "00", "00", "00");
                                                                         

                            }

                            


                 }
                    


 

                  return padre;
                



              


            }
            catch (Exception ex)
            {
              
                return padre;

            }



        }

        public async Task<PRE_PLAN_UNICO_CUENTAS> GetPucPadreNew(FilterPrePUCPresupuestoCodigos dto)
        {

            PRE_PLAN_UNICO_CUENTAS padre = new PRE_PLAN_UNICO_CUENTAS();
            try
            {


                if (dto != null)
                {


                    if (dto.CodicoNivel5 != "00")
                    {
                        padre = await _repository.GetHastaNivel4((int)dto.CodigoPresupuesto,
                                                                   (int)dto.CodigoPuc,
                                                                   dto.CodigoGrupo,
                                                                   dto.CodicoNivel1,
                                                                   dto.CodicoNivel2,
                                                                   dto.CodicoNivel3,
                                                                   dto.CodicoNivel4);
                    }
                    else if (dto.CodicoNivel4 != "00")
                    {
                        padre = await _repository.GetHastaNivel3((int)dto.CodigoPresupuesto,
                                                                   (int)dto.CodigoPuc,
                                                                   dto.CodigoGrupo,
                                                                   dto.CodicoNivel1,
                                                                   dto.CodicoNivel2,
                                                                   dto.CodicoNivel3);
                    }
                    else if (dto.CodicoNivel3 != "00")
                    {
                        padre = await _repository.GetHastaNivel2((int)dto.CodigoPresupuesto,
                                                                   (int)dto.CodigoPuc,
                                                                   dto.CodigoGrupo,
                                                                   dto.CodicoNivel1,
                                                                   dto.CodicoNivel2);
                    }
                    else if (dto.CodicoNivel2 != "00")
                    {
                        padre = await _repository.GetHastaNivel1((int)dto.CodigoPresupuesto,
                                                                   (int)dto.CodigoPuc,
                                                                   dto.CodigoGrupo,
                                                                   dto.CodicoNivel1);

                    }
                    else if (dto.CodicoNivel1 != "00")
                    {
                        padre = await _repository.GetHastaGrupoDiferentePuc((int)dto.CodigoPresupuesto,
                                                                   (int)dto.CodigoPuc,
                                                                   dto.CodigoGrupo);

                    }
                    else if (dto.CodigoGrupo != "0")
                    {
                        padre = await _repository.GetHastaGrupoIgualPuc((int)dto.CodigoPresupuesto,
                                                                   (int)dto.CodigoPuc,
                                                                   dto.CodigoGrupo);

                    }




                }





                return padre;







            }
            catch (Exception ex)
            {

                return padre;

            }



        }


        public PrePlanUnicoCuentasGetDto MapPucToDto(PRE_PLAN_UNICO_CUENTAS entity)
        {
            PrePlanUnicoCuentasGetDto result = new PrePlanUnicoCuentasGetDto();

            result.CodigoPuc = entity.CODIGO_PUC;
            result.CodigoGrupo = entity.CODIGO_GRUPO;
            result.CodigoNivel1 = entity.CODIGO_NIVEL1;
            result.CodigoNivel2 = entity.CODIGO_NIVEL2;
            result.CodigoNivel3 = entity.CODIGO_NIVEL3;
            result.CodigoNivel4 = entity.CODIGO_NIVEL4;
            result.CodigoNivel5 = entity.CODIGO_NIVEL5;
            result.CodigoNivel6 = entity.CODIGO_NIVEL6;
            result.Denominacion = entity.DENOMINACION;
            if (entity.DESCRIPCION == null) entity.DESCRIPCION = "";
            result.Descripcion = entity.DESCRIPCION;
            result.CodigoPucPadre = entity.CODIGO_PUC_FK;
            result.CodigoPresupuesto = entity.CODIGO_PRESUPUESTO;

            return result;

        }

        public async Task<ResultDto<PrePlanUnicoCuentasGetDto>> Update(PrePlanUnicoCuentaUpdateDto dto)
        {
            ResultDto<PrePlanUnicoCuentasGetDto> result = new ResultDto<PrePlanUnicoCuentasGetDto>(null);
            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var empresString = @settings.EmpresaConfig;
            var empresa = Int32.Parse(empresString);

            var puc = await _repository.GetByCodigo(dto.CodigoPuc);
            if (puc == null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "No existe Codigo PUC";
                return result;
            }

            result = await ValidateDto(dto);
            if (result.IsValid == false) return result;

            var presupuesto = await _presupuesttoRepository.GetByCodigo(empresa, (int)dto.CodigoPresupuesto);

            puc.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
            puc.CODIGO_GRUPO = dto.CodigoGrupo;
            puc.CODIGO_NIVEL1 = dto.CodigoNivel1;
            puc.CODIGO_NIVEL2 = dto.CodigoNivel2;
            puc.CODIGO_NIVEL3 = dto.CodigoNivel3;
            puc.CODIGO_NIVEL4 = dto.CodigoNivel4;
            puc.CODIGO_NIVEL5 = dto.CodigoNivel5;
            puc.CODIGO_NIVEL6 = dto.CodigoNivel6;
            puc.DENOMINACION = dto.Denominacion;
            puc.DESCRIPCION = dto.Descripcion;
            puc.CODIGO_PUC_FK = 0;
            var  updated = await _repository.Update(puc);
            if (updated.Data != null)
            {
                result.Data = MapPucToDto(updated.Data);
                result.IsValid = updated.IsValid;
                result.Message = updated.Message;
            }
            else
            {
                result.Data = null;
                result.IsValid = updated.IsValid;
                result.Message = updated.Message;
            }
            

            return result;
         }

        public async Task<ResultDto<PrePlanUnicoCuentasGetDto>> Create(PrePlanUnicoCuentaUpdateDto dto)
        {
            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var empresString = @settings.EmpresaConfig;
            var empresa = Int32.Parse(empresString);

            ResultDto<PrePlanUnicoCuentasGetDto> result = new ResultDto<PrePlanUnicoCuentasGetDto>(null);

            var icp = await _repository.GetByCodigo(dto.CodigoPuc);
            if (icp != null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "Existe Codigo ICP";
                return result;
            }

            result = await ValidateDto(dto);
            if (result.IsValid == false) return result;

            PRE_PLAN_UNICO_CUENTAS pucNew = new PRE_PLAN_UNICO_CUENTAS();
            var presupuesto = await _presupuesttoRepository.GetByCodigo(empresa,(int)dto.CodigoPresupuesto);


            pucNew.CODIGO_PRESUPUESTO = dto.CodigoPresupuesto;
            pucNew.CODIGO_GRUPO = dto.CodigoGrupo;
            pucNew.CODIGO_NIVEL1 = dto.CodigoNivel1;
            pucNew.CODIGO_NIVEL2 = dto.CodigoNivel2;
            pucNew.CODIGO_NIVEL3 = dto.CodigoNivel3;
            pucNew.CODIGO_NIVEL4 = dto.CodigoNivel4;
            pucNew.CODIGO_NIVEL5 = dto.CodigoNivel5;
            pucNew.CODIGO_NIVEL6 = dto.CodigoNivel6;
            pucNew.DENOMINACION = dto.Denominacion;
            pucNew.DESCRIPCION = dto.Descripcion;
            var created = await _repository.Create(pucNew);
            if (created.Data != null)
            {
                result.Data = MapPucToDto(created.Data);
                result.IsValid = created.IsValid;
                result.Message = created.Message;
            }
            else
            {
                result.Data = null;
                result.IsValid = created.IsValid;
                result.Message = created.Message;
            }


            return result;
        }


        public async Task<ResultDto<PrePlanUnicoCuentasGetDto>> GetById(int codigoPuc)
        {
            ResultDto<PrePlanUnicoCuentasGetDto> result = new ResultDto<PrePlanUnicoCuentasGetDto>(null);
          

            var puc = await _repository.GetByCodigo(codigoPuc);

            if (puc != null)
            {
                result.Data = MapPucToDto(puc);
                result.IsValid = true;
                result.Message = "";
            }
            else
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "Puc No Existe";
            }
            

            return result;
        }

        
        public async Task DeleteByCodigoPresupuesto(int codigoPresupuesto)
        {
            var icps= await  GetAllByCodigoPresupuesto(codigoPresupuesto);
            if (icps!=null && icps.Data != null)
            {
                foreach (var item in icps.Data)
                {
                    DeletePrePucDto dto = new DeletePrePucDto();
                    dto.CodigoPuc = item.CodigoPuc;
                    await Delete(dto);
                }
            }

        }

        public async Task<ResultDto<DeletePrePucDto>> Delete(DeletePrePucDto dto)
        {

            ResultDto<DeletePrePucDto> result = new ResultDto<DeletePrePucDto>(null);
            try
            {

                var puc = await _repository.GetByCodigo(dto.CodigoPuc);
                if (puc == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "PUC no existe";
                    return result;
                }

               
                var icpExiste = await _PRE_ASIGNACIONESRepository.PUCExiste(dto.CodigoPuc);
                if (icpExiste)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "PUC no puede ser eliminado,tiene Movimiento Creado";
                    return result;
                }
               
                var listDto = await _repository.GetAllByCodigoPresupuesto((int)puc.CODIGO_PRESUPUESTO);
                var hijos = await ListarHijos(listDto, dto.CodigoPuc);


                if (hijos.Count > 0)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "PUC no puede ser eliminado,tiene Hijos!";
                    return result;

                }


                /*if (hijos.Count > 0)
                {
                    foreach (var item in hijos)
                    {
                        var pucExiste = await _PRE_ASIGNACIONESRepository.PUCExiste(item);
                        if (pucExiste)
                        {
                            result.Data = dto;
                            result.IsValid = false;
                            result.Message = "PUC no puede ser eliminado,tiene Movimiento Creado";
                            return result;
                        }
                    }

                }*/


                var deleted = await _repository.Delete(dto.CodigoPuc);

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


      
      

        public async Task<List<int>> ListarHijos(List<PRE_PLAN_UNICO_CUENTAS> listDto, int codPuc)
        {

            List<int> result = new List<int>();
            try
            {

              

              

                var listHijos = listDto.Where(x => x.CODIGO_PUC_FK == codPuc).ToList();
                if (listHijos.Count > 0)
                {

                    foreach (var item in listHijos)
                    {
                        if (item.CODIGO_PUC== codPuc)
                        {
                            result.Add(item.CODIGO_PUC);
                        }
                        else
                        {
                            result.Add(item.CODIGO_PUC);
                            var newList = await ListarHijos(listDto, item.CODIGO_PUC);
                            if (newList.Count > 0)
                            {
                                result.AddRange(newList);
                            }
                           

                        }
                        
                    }
                }


                return result;
             


            }
            catch (Exception ex)
            {
                return null;
            }



            return result;
        }

        public async Task<List<int>> ListarHijosITEM(List<Item> listDto, int codPuc)
        {

            List<int> result = new List<int>();
            try
            {





                var listHijos = listDto.Where(x => x.ParentId == codPuc).ToList();
                if (listHijos.Count > 0)
                {

                    foreach (var item in listHijos)
                    {
                        if (item.Id == codPuc)
                        {
                            result.Add(item.Id);
                            return result;
                        }
                        else
                        {
                            result.Add(item.Id);
                            var newList = await ListarHijosITEM(listDto, item.Id);
                            if (newList.Count > 0)
                            {
                                result.AddRange(newList);
                            }


                        }

                    }
                }


                return result;



            }
            catch (Exception ex)
            {
                return null;
            }



            return result;
        }

        private IList<Item> GetChild(int id, IList<Item> items)
        {
            var childs = items
                .Where(x => x.ParentId == id || x.Id == id)
                .Union(items.Where(x => x.ParentId == id)
                .SelectMany(y => GetChild(y.Id, items)));

            return childs.ToList();
        }


        public async  Task<List<Item>> GetItems(int codigoPresupuesto)
        {
            List<Item> result = new List<Item>();
            var categoriasList = await _repository.GetAllByCodigoPresupuesto(codigoPresupuesto);

            foreach (var item in categoriasList)
            {
                Item itenNew = new Item();
                itenNew.Id = item.CODIGO_PUC;

                itenNew.ParentId = (int)item.CODIGO_PUC_FK;
                var patchEvaluado = item.CODIGO_GRUPO + "-" + item.CODIGO_NIVEL1 + "-" + item.CODIGO_NIVEL2 + "-" + item.CODIGO_NIVEL3 + "-" + item.CODIGO_NIVEL4 + "-" + item.CODIGO_NIVEL5 + "-" + item.CODIGO_NIVEL6;

                itenNew.Name = patchEvaluado;
                itenNew.Denominacion = item.DENOMINACION;
                if (item.DESCRIPCION == null) item.DESCRIPCION = "";
                itenNew.Descripcion = item.DESCRIPCION;
                result.Add(itenNew);

            }

            return result;
        }
        public async Task<List<TreePUC>> ListTreeByPresupuesto(int codigoPresupuesto)
        {
            List<TreePUC> result = new List<TreePUC>();

          
            List<Item> items = await GetItems(codigoPresupuesto);
            foreach (var item in items)
            {
                if (item.Id == 35391)
                {
                    var a = "";
                }
                var lista = await  ListarHijosITEM(items, item.Id);
                if (lista.Count > 0)
                {
                    List<string> pucString = new List<string>();
                    foreach (var itemHijos in lista)
                    {
                        var itemObj = items.Where(x => x.Id == itemHijos).FirstOrDefault();
                        if (itemObj != null)
                        {
                            pucString.Add(itemObj.Name);
                        }

                    }

                    TreePUC treePUC = new TreePUC();
                    treePUC.Path = pucString;
                    treePUC.Id = item.Id;
                    treePUC.Denominacion = item.Denominacion;
                    treePUC.Descripcion = item.Descripcion;

                    result.Add(treePUC);
                }
                else
                {
                    List<string> pucString = new List<string>();
                    pucString.Add(item.Name);
                    TreePUC treePUC = new TreePUC();
                    treePUC.Path = pucString;
                    treePUC.Id = item.Id;
                    treePUC.Denominacion = item.Denominacion;
                    treePUC.Descripcion = item.Descripcion;

                    result.Add(treePUC);
                }
               
            }

       
            return result;
        }


        public async Task<List<string>> WriteStrings(int codigoPresupuesto)
        {

            List<string> result = new List<string>();
            List<Item> items = await  GetItems(codigoPresupuesto);
          
           
            IEnumerable<string> resultingStrings = from parent in items.Where(x => x.ParentId == x.Id)
                                                   join child in items on parent.Id equals child.ParentId
                                                   join grandChild in items on child.Id equals grandChild.ParentId
                                                   join grandGrandChild in items on grandChild.Id equals grandGrandChild.ParentId
                                                   join grandGrandGrandChild in items on grandGrandChild.Id equals grandGrandGrandChild.ParentId
                                                   join grandGrandGrandGrandChild in items on grandGrandGrandChild.Id equals grandGrandGrandGrandChild.ParentId
                                                   join grandGrandGrandGrandGrandChild in items on grandGrandGrandGrandChild.Id equals grandGrandGrandGrandGrandChild.ParentId
                                                   select string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}:{7}:{8}:{9}", parent.Name, child.Name, grandChild.Name, grandGrandChild.Name, grandGrandGrandChild.Name, grandGrandGrandGrandGrandChild.Name, grandGrandGrandGrandGrandChild.Id, grandGrandGrandGrandGrandChild.ParentId, grandGrandGrandGrandGrandChild.Denominacion, grandGrandGrandGrandGrandChild.Descripcion);
          
         

            foreach (var item in resultingStrings)
            {
                var search = result.Where(x => x == item).FirstOrDefault();
                result.Add(item);
            
               
            }
             
            return result;
        }


        public async Task<PRE_PLAN_UNICO_CUENTAS> GetByCodigos(FilterPrePUCPresupuestoCodigos filter)
        {
            var puc = await _repository.GetByCodigos(filter);
            return puc;
        }

        public async Task<ResultDto<PrePlanUnicoCuentasGetDto>> ValidateDto(PrePlanUnicoCuentaUpdateDto dto)
        {
            ResultDto<PrePlanUnicoCuentasGetDto> result = new ResultDto<PrePlanUnicoCuentasGetDto>(null);

            FilterPrePUCPresupuestoCodigos filter = new FilterPrePUCPresupuestoCodigos();

            filter.CodigoPresupuesto = dto.CodigoPresupuesto;
            filter.CodigoPuc = dto.CodigoPuc;
            filter.CodigoGrupo = dto.CodigoGrupo;
            filter.CodicoNivel1 = dto.CodigoNivel1;
            filter.CodicoNivel2 = dto.CodigoNivel2;
            filter.CodicoNivel3 = dto.CodigoNivel3;
            filter.CodicoNivel4 = dto.CodigoNivel4;
            filter.CodicoNivel5 = dto.CodigoNivel5;
            filter.CodicoNivel6 = dto.CodigoNivel6;

            var pucCodigos = await _repository.GetByCodigos(filter);
            if (pucCodigos != null && pucCodigos.CODIGO_PUC != dto.CodigoPuc)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "Categorizacion existe en otro registro";
                return result;
            }
            if (Int32.Parse(dto.CodigoGrupo) <=0)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "CodigoGrupo debe ser mayor a cero";
                return result;
            }
            if (dto.CodigoNivel1.Length != 2)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "CodigoNivel1 debe ser de 2 digitos";
                return result;
            }
            if (dto.CodigoNivel2.Length != 2)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "CodigoNivel2 debe ser de 2 digitos";
                return result;
            }
            if (dto.CodigoNivel3.Length != 2)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "CodigoNivel3 debe ser de 2 digitos";
                return result;
            }
            if (dto.CodigoNivel4.Length != 2)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "CodigoNivel4 debe ser de 2 digitos";
                return result;
            }
            if (dto.CodigoNivel5.Length != 2)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "CodigoOficina debe ser de 2 digitos";
                return result;
            }
            if (dto.CodigoNivel6.Length != 2)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "CodigoNivel6 debe ser de 2 digitos";
                return result;
            }


            var padre = await GetPucPadre(filter) ;
            if(padre == null  && dto.CodigoNivel5 != "00")
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = $"No existe un padre para Nivel5: {dto.CodigoNivel5}";
                return result;
            }
            if (padre == null && dto.CodigoNivel4 != "00")
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = $"No existe un padre para el Nivel4: {dto.CodigoNivel4}";
                return result;
            }
            if (padre == null && dto.CodigoNivel3 != "00")
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = $"No existe un padre para el Nivel3: {dto.CodigoNivel3}";
                return result;
            }
            if (padre == null && dto.CodigoNivel2 != "00")
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = $"No existe un padre para el Nivel2: {dto.CodigoNivel2}";
                return result;
            }

            result.Data = null;
            result.IsValid = true;
            result.Message = "";


            return result;
        }


        public async Task<ResultDto<List<PreCodigosPuc>>> ListCodigosHistoricoPuc()
        {
            ResultDto<List<PreCodigosPuc>> result = new ResultDto<List<PreCodigosPuc>>(null);

            List<PreCodigosPuc> resultList = new List<PreCodigosPuc>();


            var puc = await _repository.GetAll();

                var qpucDto = from s in puc.ToList()
                              group s by new
                              {
                               
                                  CodigoGrupo = s.CODIGO_GRUPO,
                                  CodigoNivel1 = s.CODIGO_NIVEL1,
                                  CodigoNivel2 = s.CODIGO_NIVEL2,
                                  CodigoNivel3 = s.CODIGO_NIVEL3,
                                  CodigoNivel4 = s.CODIGO_NIVEL4,
                                  CodigoNivel5 = s.CODIGO_NIVEL5,
                                  CodigoNivel6 = s.CODIGO_NIVEL6,

                              } into g
                              select new PreCodigosPuc
                              {


                                  CodigoGrupo = g.Key.CodigoGrupo,
                                  CodigoNivel1 = g.Key.CodigoNivel1,
                                  CodigoNivel2 = g.Key.CodigoNivel2,
                                  CodigoNivel3 = g.Key.CodigoNivel3,
                                  CodigoNivel4 = g.Key.CodigoNivel4,
                                  CodigoNivel5 = g.Key.CodigoNivel5,
                                  CodigoNivel6 = g.Key.CodigoNivel6,

                              };

            resultList = qpucDto.ToList();
            result.Data = resultList;
            result.IsValid = true;
            result.Message = "";


            return result;
        }

    }
}
