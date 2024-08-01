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



        private readonly IHttpContextAccessor _httpContextAccessor;

        public SisUsuarioServices(ISisUsuarioRepository repository,
                                    IOssUsuarioRolRepository ossUsuarioRolRepository,
                                    IHttpContextAccessor httpContextAccessor,
                                    IConfiguration configuration)
        {
            _repository = repository;
            _ossUsuarioRolRepository = ossUsuarioRolRepository;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<ResultLoginDto> Login(LoginDto dto)
        {
            var result = await _repository.Login(dto);
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

        public async Task<ResultDto<SIS_USUARIOS>> Update(SIS_USUARIOS entity)
        {

           var result = await _repository.Update(entity);

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

