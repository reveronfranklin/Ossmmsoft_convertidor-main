using System.Security.Claims;
using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos.Sis;
using Convertidor.Utility;

namespace Convertidor.Services.Sis
{
	public class SisUsuarioServices: ISisUsuarioServices
    {
		
        private readonly ISisUsuarioRepository _repository;
        private readonly IOssUsuarioRolRepository _ossUsuarioRolRepository;
        private readonly IConfiguration _configuration;
        private readonly IRhDescriptivasService _rhDescriptivasService;
        private readonly IOssAuthGroupService _ossAuthGroupService;
        private readonly IOssAuthUserGroupService _ossAuthUserGroupService;
        private readonly IOssAuthGroupPermissionService _ossAuthGroupPermissionService;
        private readonly IOssAuthUserPermissionService _ossAuthUserPermissionService;
        private readonly IOssAuthPermissionsService _ossAuthPermissionsService;
        private readonly IOssAuthContentTypeService _ossAuthContentTypeService;


        private readonly IHttpContextAccessor _httpContextAccessor;

        public SisUsuarioServices(ISisUsuarioRepository repository,
                                    IOssUsuarioRolRepository ossUsuarioRolRepository,
                                    IHttpContextAccessor httpContextAccessor,
                                    IConfiguration configuration,
                                    IRhDescriptivasService rhDescriptivasService,
                                    IOssAuthGroupService ossAuthGroupService,
                                    IOssAuthUserGroupService ossAuthUserGroupService,
                                    IOssAuthGroupPermissionService ossAuthGroupPermissionService,
                                    IOssAuthUserPermissionService ossAuthUserPermissionService,
                                    IOssAuthPermissionsService ossAuthPermissionsService,
                                    IOssAuthContentTypeService ossAuthContentTypeService
                                    )
        {
            _repository = repository;
            _ossUsuarioRolRepository = ossUsuarioRolRepository;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _rhDescriptivasService = rhDescriptivasService;
            _ossAuthGroupService = ossAuthGroupService;
            _ossAuthUserGroupService = ossAuthUserGroupService;
            _ossAuthGroupPermissionService = ossAuthGroupPermissionService;
            _ossAuthUserPermissionService = ossAuthUserPermissionService;
            _ossAuthPermissionsService = ossAuthPermissionsService;
            _ossAuthContentTypeService = ossAuthContentTypeService;
        }

        public async Task<ResultLoginDto> Login(LoginDto dto)
        {
            var result = await _repository.Login(dto);
            return result;
        }

        public async Task<List<Permission>> GetPermissionsByUserId(int userId)
        {
            List<Permission> result = new List<Permission>();
            List<AuthPermissionResponseDto> allPermissions = new  List<AuthPermissionResponseDto>(); 
            AuthUserGroupFilterDto  userGroupFilter = new AuthUserGroupFilterDto();
            userGroupFilter.UserId = userId;
            var groupsByUser = await _ossAuthUserGroupService.GetByUser(userGroupFilter);
            if (groupsByUser.Data != null && groupsByUser.Data.Count > 0)
            {
                foreach (var itemGroupByUser in groupsByUser.Data)
                {
                    AuthGroupPermissionFilterDto filter = new AuthGroupPermissionFilterDto();
                    filter.GroupId = itemGroupByUser.GroupId;
                    var permissionByGroup = await _ossAuthGroupPermissionService.GetByGroup(filter);
                    if (permissionByGroup.Data != null && permissionByGroup.Data.Count > 0)
                    {
                        foreach (var itemPermissionByGroup in permissionByGroup.Data)
                        {
                            AuthPermissionResponseDto allPermissionsItem = new  AuthPermissionResponseDto();
                            AuthPermissionFilterDto permissionFilter = new AuthPermissionFilterDto();
                            permissionFilter.Id = itemPermissionByGroup.PermisionId;
                            var permission = await _ossAuthPermissionsService.GetById(permissionFilter);
                            if (permission.Data != null)
                            {
                                allPermissions.Add(permission.Data);
                            }
                        }
                    }
                }
                
                
             
            }
            
            var groupedByModel = allPermissions.GroupBy(p => p.Model);
            foreach (var group in groupedByModel)
            {
                Permission newPermisionByModel = new Permission();
                newPermisionByModel.Model = group.Key;
                Console.WriteLine($"Age Group: {group.Key}");
                List<string> actions = new List<string>();
                foreach (var itemGroup in group)
                {
                    Console.WriteLine($"  Name: {itemGroup.Codename}");
                    actions.Add(itemGroup.Codename);
                }

                newPermisionByModel.Actions = actions;
                result.Add(newPermisionByModel);
            }
            
            return result;

        }
        public async Task<ResultDto<UserPermissionDto>> GetUserPermissions(string login)
        {

            ResultDto<UserPermissionDto> result = new ResultDto<UserPermissionDto>(null);
            UserPermissionDto userPermissionDto = new UserPermissionDto();
            var user = await _repository.GetByLogin(login);
            if (user != null)
            {
                userPermissionDto.Login = login;
                userPermissionDto.UserId = user.CODIGO_USUARIO;
                userPermissionDto.Name = user.USUARIO;
                userPermissionDto.IsActive = false;
                if (user.STATUS == "1")
                {
                    userPermissionDto.IsActive = true;
                }
              
                userPermissionDto.IsSuperUser = false;
                if (user.IS_SUPERUSER == 1)
                {
                    userPermissionDto.IsSuperUser = true;
                }

                var permission = await GetPermissionsByUserId(userPermissionDto.UserId);
                userPermissionDto.Permissions = permission;
                result.Data = userPermissionDto;
                result.Message = "";
                result.IsValid = true;
            }
            else
            {
                result.Data = null;
                result.Message = "No Data";
                result.IsValid = false;

            }
          
           
            
            return result;
        }
        
        
        public string GetMyName()
        {
            var login = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                var usuario=_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                login = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
                if (login == null) login = "";
            }

            return login;
        }
        public async Task<SIS_USUARIOS> GetByLogin(string login)
        {

            var result = await _repository.GetByLogin(login);
            return result;

        }

       

        public List<SisEstatus> GetListEstatus()
        {
            List<SisEstatus> result = new List<SisEstatus>();

            // Agregar objetos a la lista
            result.Add(new SisEstatus { Id = "0", Descripcion = "Activo" });
            result.Add(new SisEstatus { Id = "1", Descripcion = "Inactivo" });
            result.Add(new SisEstatus { Id = "2", Descripcion = "Suspendido" });

            return result;

        }
        
        public List<SisPrioridad> GetListPrioridad()
        {
            List<SisPrioridad> result = new List<SisPrioridad>();

            // Agregar objetos a la lista
            result.Add(new SisPrioridad { Id = 0, Descripcion = "No Auditor" });
            result.Add(new SisPrioridad { Id = 1, Descripcion = "Auditor" });

            return result;

        }

        public  async Task<SisUsuariosResponseDto> MapDto(SIS_USUARIOS entity)
        {
            SisUsuariosResponseDto itemResult = new SisUsuariosResponseDto();
            
            try
            {
                if (entity == null)
                {
                    return itemResult;
                }
                itemResult.CodigoUsuario = entity.CODIGO_USUARIO;
                itemResult.Usuario = entity.USUARIO;
                itemResult.Login = entity.LOGIN;
                itemResult.Cedula = entity.CEDULA;
                itemResult.DepartamentoId = entity.DEPARTAMENTO_ID;
                itemResult.DescripcionDepartamento = "";
                var departamento = await _rhDescriptivasService.GetByCodigoDescriptiva((int)itemResult.DepartamentoId);
                if (departamento != null)
                {
                    itemResult.DescripcionDepartamento = departamento.DESCRIPCION;
                }
                itemResult.CargoId = entity.CARGO_ID;
                itemResult.DescripcionCargo = "";
                var cargo = await _rhDescriptivasService.GetByCodigoDescriptiva((int)itemResult.CargoId);
                if (cargo != null)
                {
                    itemResult.DescripcionCargo = cargo.DESCRIPCION;
                }
                itemResult.DescripcionSistema = "";
                itemResult.SistemaId = entity.SISTEMA_ID;
                var sistema = await _rhDescriptivasService.GetByCodigoDescriptiva((int)itemResult.SistemaId);
                if (sistema != null)
                {
                    itemResult.DescripcionSistema = sistema.DESCRIPCION;
                }
                itemResult.FechaIngreso = entity.FECHA_INGRESO;
                itemResult.FechaIngresoString = Fecha.GetFechaString((DateTime)entity.FECHA_INGRESO);
                itemResult.FechaIngresoObj = Fecha.GetFechaDto((DateTime)entity.FECHA_INGRESO);
                itemResult.FechaEgreso = entity.FECHA_EGRESO;
                itemResult.FechaEgresoString = Fecha.GetFechaString((DateTime)entity.FECHA_INGRESO);
                itemResult.FechaEgresoObj = Fecha.GetFechaDto((DateTime)entity.FECHA_INGRESO);
                itemResult.Prioridad = entity.PRIORIDAD;
                var listPrioridad = GetListPrioridad();
                var prioridad = listPrioridad.Where(x=> x.Id==itemResult.Prioridad).FirstOrDefault();
                itemResult.DescripcionPrioridad = prioridad.Descripcion;
                itemResult.Status = entity.STATUS;
                itemResult.DescripcionStatus = "";
                var listEstatus = GetListEstatus();
                var estatus = listEstatus.Where(x=> x.Id==itemResult.Status).FirstOrDefault();
                if (estatus != null)
                {
                    itemResult.DescripcionStatus = estatus.Descripcion;
                }

                if (entity.IS_SUPERUSER == 0)
                {
                    itemResult.IsSuperUser = false;
                }
                else
                {
                    itemResult.IsSuperUser = true;
                }
         
                return itemResult;

            }
            catch (Exception e)
            {
                Console.WriteLine(entity);
                Console.WriteLine(e);
                return itemResult;
            }
          
        }

        public async Task< List<SisUsuariosResponseDto>> MapList(List<SIS_USUARIOS> dtos)
        {
            List<SisUsuariosResponseDto> result = new List<SisUsuariosResponseDto>();
            if (dtos.Count > 0)
            {
                foreach (var item in dtos)
                {
                    if (item == null)
                    {
                        var detener = "";
                    }
                    else
                    {
                        var itemResult =  await MapDto(item);
               
                        result.Add(itemResult);
                    }

                   
                }
            }
            
          
            return result;



        }
        
        
        
        public async Task<ResultDto<List<SisUsuariosResponseDto>>> GetAll()
        {
            ResultDto<List<SisUsuariosResponseDto>> result = new ResultDto<List<SisUsuariosResponseDto>>(null);
            try
            {

                var usuarios = await _repository.GetALL();
                if (usuarios == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No Data";
                    return result;
                }
                 
                var resultDto = await  MapList(usuarios);
                result.Data = resultDto;
                result.IsValid = true;
                result.Message = "";
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

        
        
     
        public async Task<ResultDto<SisUsuariosResponseDto>> Update(SisUsuariosUpdateDto dto)
        {

            ResultDto<SisUsuariosResponseDto> result = new ResultDto<SisUsuariosResponseDto>(null);
            try
            {
                var usuario = await _repository.GetByCodigo(dto.CodigoUsuario);
                if (usuario == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Usuario no existe";
                    return result;
                }
              
                
                if (String.IsNullOrEmpty(dto.Login) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Login Invalido";
                    return result;
                }
                if (String.IsNullOrEmpty(dto.Usuario) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nombre Usuario Invalido";
                    return result;
                }
                if (dto.Cedula<=0 )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cedula Invalido";
                    return result;
                }

                
                
                var departamento = await _rhDescriptivasService.GetByCodigoDescriptiva((int)dto.DepartamentoId);
                if (departamento == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Departamento Invalido";
                    return result;
                }
               
                var cargo = await _rhDescriptivasService.GetByCodigoDescriptiva((int)dto.CargoId);
                if (cargo != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cargo Invalido";
                    return result;
                }
              
                var sistema = await _rhDescriptivasService.GetByCodigoDescriptiva((int)dto.SistemaId);
                if (sistema == null)
                { result.Data = null;
                    result.IsValid = false;
                    result.Message = "Sistema Invalido";
                    return result;
                  
                }
                
                usuario.USUARIO = dto.Usuario;
                usuario.LOGIN = dto.Login;
                usuario.CEDULA = dto.Cedula;
                usuario.DEPARTAMENTO_ID = dto.DepartamentoId;
                usuario.CARGO_ID = dto.CargoId;
                usuario.SISTEMA_ID = dto.SistemaId;
                string dateString = Fecha.GetFechaString((DateTime)dto.FechaEgreso);
                DateTime dateValue;
                bool isValidDate = DateTime.TryParse(dateString, out dateValue);
                if (isValidDate)
                {
                    usuario.FECHA_EGRESO = dto.FechaEgreso;
                }
            
                usuario.FECHA_UPD = DateTime.Now;
                var conectado = await _repository.GetConectado();
                usuario.CODIGO_EMPRESA = conectado.Empresa;
                usuario.USUARIO_UPD = conectado.Usuario;
                await _repository.Update(usuario);
                var resultDto = await  MapDto(usuario);
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
        
        
        public async Task<ResultDto<SisUsuariosResponseDto>> Create(SisUsuariosUpdateDto dto)
        {

            ResultDto<SisUsuariosResponseDto> result = new ResultDto<SisUsuariosResponseDto>(null);
            try
            {

                var usuario = await _repository.GetByLogin(dto.Login);
                if (usuario != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = $"Ya existe este usuario:{dto.Usuario}";
                    return result;
                }
                
                if (String.IsNullOrEmpty(dto.Login) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Login Invalido";
                    return result;
                }
                if (String.IsNullOrEmpty(dto.Usuario) )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nombre Usuario Invalido";
                    return result;
                }
                if (dto.Cedula<=0 )
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cedula Invalido";
                    return result;
                }

                
                
                var departamento = await _rhDescriptivasService.GetByCodigoDescriptiva((int)dto.DepartamentoId);
                if (departamento == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Departamento Invalido";
                    return result;
                }
               
                var cargo = await _rhDescriptivasService.GetByCodigoDescriptiva((int)dto.CargoId);
                if (cargo != null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Cargo Invalido";
                    return result;
                }
              
                var sistema = await _rhDescriptivasService.GetByCodigoDescriptiva((int)dto.SistemaId);
                if (sistema == null)
                { result.Data = null;
                    result.IsValid = false;
                    result.Message = "Sistema Invalido";
                    return result;
                  
                }
               
                
                SIS_USUARIOS entity = new SIS_USUARIOS();
            
                entity.USUARIO = dto.Usuario;
                entity.LOGIN = dto.Login;
                entity.CEDULA = dto.Cedula;
                entity.DEPARTAMENTO_ID = dto.DepartamentoId;
                entity.CARGO_ID = dto.CargoId;
                entity.SISTEMA_ID = dto.SistemaId;
                entity.FECHA_INGRESO = dto.FechaIngreso;
                entity.STATUS = dto.Status;
                entity.PRIORIDAD = dto.Prioridad;
                if (dto.IsSuperUser)
                {
                    entity.IS_SUPERUSER =1 ;
                }
                else
                {
                    entity.IS_SUPERUSER =0 ;
                }

                var conectado = await _repository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.FECHA_INS = DateTime.Now;
                entity.USUARIO_INS = conectado.Usuario;
                var created=await _repository.Create(entity);
                
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapDto(created.Data);
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
        public string GetToken(SIS_USUARIOS usuario)
        {



            var jwt = _repository.GetToken(usuario);

            return jwt;
        }

        
        
        public async  Task<List<RoleMenuDto>> GetMenu(string usuario)
        {

            List<RoleMenuDto> result = new List<RoleMenuDto> ();


            var roles = await _ossUsuarioRolRepository.GetByUsuario(usuario);
            
      

            if (roles!=null)
            {
                foreach (var item in roles)
                {
                    RoleMenuDto resultItem = new RoleMenuDto();
                    resultItem.Role = item.DESCRIPCION;

                    var jsonValid = JsonValidator.IsValidJson(item.JSON_MENU);
                    if (jsonValid)
                    {
                        resultItem.Menu = item.JSON_MENU;
                    }
                    else
                    {
                        resultItem.Menu = "[{ \"title\": \"Nomina\",\n    \"icon\": \"mdi:file-document-outline\",}]";
                    }
                   
                    result.Add(resultItem);

                }
                
            }

            return result;

        }

        
        public async  Task<List<RoleMenuDto>> GetMenuOld(string usuario)
        {

            List<RoleMenuDto> result = new List<RoleMenuDto> ();

            var roles = await _repository.GetRolByUserName(usuario);
            if (roles!=null)
            {
                foreach (var item in roles)
                {
                    RoleMenuDto resultItem = new RoleMenuDto();
                    resultItem.Role = item.Role;
                    if (item.Role == "DEV" || item.Role == "sis")
                    {
                        resultItem.Menu = GetMenuDeveloper();
                    }
                    if (item.Role == "pre")
                    {
                        resultItem.Menu = GetMenuPre();
                    }
                    if (item.Role == "rh")
                    {
                        resultItem.Menu = GetMenuRh();
                    }
                    if (item.Role == "bm")
                    {
                        resultItem.Menu = GetMenuBm();
                    }

                    result.Add(resultItem);

                }
                
            }

            return result;

        }

        public string GetMenuPre()
        {

            try
            {
                var settings = _configuration.GetSection("Settings").Get<Settings>();


         

                string jsonFilePath = @settings.MenuFiles;

                string json = File.ReadAllText(jsonFilePath + "/MenuPre.json");

                return json;
            }
            catch (Exception e)
            {
                return "";
            }
            
           

        }

        public string GetMenuRh()
        {

            var settings = _configuration.GetSection("Settings").Get<Settings>();




            string jsonFilePath = @settings.MenuFiles;

            string json = File.ReadAllText(jsonFilePath + "/MenuRh.json");

            return json;

        }
        public string GetMenuBm()
        {

            var settings = _configuration.GetSection("Settings").Get<Settings>();




            string jsonFilePath = @settings.MenuFiles;

            string json = File.ReadAllText(jsonFilePath + "/MenuBM.json");

            return json;

        }
        public string GetMenuDeveloper()
        {

            var settings = _configuration.GetSection("Settings").Get<Settings>();




            string jsonFilePath = @settings.MenuFiles;

            string json = File.ReadAllText(jsonFilePath + "/MenuDeveloper.json");

            return json;

        }


    }
}

