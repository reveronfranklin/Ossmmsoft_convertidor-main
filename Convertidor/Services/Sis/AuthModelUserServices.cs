using System.Text;
using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos.Sis;
using Microsoft.Extensions.Caching.Distributed;

namespace Convertidor.Services.Sis
{
	public class AuthModelUserServices: IAuthModelUserServices
    {
		
      
        private readonly IConfiguration _configuration;
        private readonly IOssAuthUserGroupService _ossAuthUserGroupService;
        private readonly IOssAuthGroupPermissionsRepository _ossAuthGroupPermissionsRepository;
        private readonly IOssAuthPermissionsService _ossAuthPermissionsService;
        private readonly IOssAuthUserPermissionsRepository _ossAuthUserPermissionRepository;
        private readonly IDistributedCache _distributedCache;


        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthModelUserServices(
                                     ISisUsuarioRepository sisUsuarioRepository,
                                    IHttpContextAccessor httpContextAccessor,
                                    IConfiguration configuration,
                                     IOssAuthUserGroupService ossAuthUserGroupService,
                                     IOssAuthGroupPermissionsRepository ossAuthGroupPermissionsRepository,
                                     IOssAuthPermissionsService ossAuthPermissionsService,
                                     IOssAuthUserPermissionsRepository ossAuthUserPermissionRepository,
                                    IDistributedCache distributedCache)
        {
            _sisUsuarioRepository = sisUsuarioRepository;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _ossAuthUserGroupService = ossAuthUserGroupService;
            _ossAuthGroupPermissionsRepository = ossAuthGroupPermissionsRepository;
            _ossAuthPermissionsService = ossAuthPermissionsService;
            _ossAuthUserPermissionRepository = ossAuthUserPermissionRepository;

            _distributedCache = distributedCache;
        }

        public async Task<List<AuthModelUserAction>> UpdateCachetModelUserAction(int userId)
        {
                  List<AuthModelUserAction> result = new List<AuthModelUserAction>();
                  var cacheKey = $"List<AuthModelUserAction>-{userId}";
              
                  var user = await _sisUsuarioRepository.GetByCodigo(userId);

                  List<AuthPermissionResponseDto> allPermissions = new List<AuthPermissionResponseDto>();
                  AuthUserGroupFilterDto userGroupFilter = new AuthUserGroupFilterDto();
                  userGroupFilter.UserId = userId;
                  var groupsByUser = await _ossAuthUserGroupService.GetByUser(userGroupFilter);
                  if (groupsByUser.Data != null && groupsByUser.Data.Count > 0)
                  {
                      foreach (var itemGroupByUser in groupsByUser.Data)
                      {
                          AuthGroupPermissionFilterDto filter = new AuthGroupPermissionFilterDto();
                          filter.GroupId = itemGroupByUser.GroupId;
                          var permissionByGroup = await _ossAuthGroupPermissionsRepository.GetByGroup(filter);
                          if (permissionByGroup.Data != null && permissionByGroup.Data.Count > 0)
                          {
                              foreach (var itemPermissionByGroup in permissionByGroup.Data)
                              {
                                  AuthPermissionResponseDto allPermissionsItem = new AuthPermissionResponseDto();
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

                  AuthUserPermisionFilterDto filterUserPermission = new AuthUserPermisionFilterDto();
                  filterUserPermission.UserId = userId;
                  var permissionByUser = await _ossAuthUserPermissionRepository.GetByUser(filterUserPermission);
                  if (permissionByUser.Data != null && permissionByUser.Data.Count > 0)
                  {
                      foreach (var itempermissionByUser in permissionByUser.Data)
                      {
                          AuthPermissionResponseDto allPermissionsItem = new AuthPermissionResponseDto();
                          AuthPermissionFilterDto permissionFilter = new AuthPermissionFilterDto();
                          permissionFilter.Id = itempermissionByUser.PermisionId;
                          var permission = await _ossAuthPermissionsService.GetById(permissionFilter);
                          if (permission.Data != null)
                          {
                              allPermissions.Add(permission.Data);
                          }
                      }
                  }

                  foreach (var item in allPermissions)
                  {

                      AuthModelUserAction newPermisionByModel = new AuthModelUserAction();
                      newPermisionByModel.Model = item.Model;
                      newPermisionByModel.Action = item.Codename;
                      newPermisionByModel.Login = user.LOGIN;

                      result.Add(newPermisionByModel);

                  }


                  var options = new DistributedCacheEntryOptions()
                      .SetAbsoluteExpiration(DateTime.Now.AddDays(1))
                      .SetSlidingExpiration(TimeSpan.FromDays(1));
                  var serializedList = System.Text.Json.JsonSerializer.Serialize(result);
                  var redisListBytes = Encoding.UTF8.GetBytes(serializedList);
                  await _distributedCache.SetAsync(cacheKey,redisListBytes,options);
              return result;
          }
     
        
        //Busca los Permisos por usuario
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
                            var permissionByGroup = await _ossAuthGroupPermissionsRepository.GetByGroup(filter);
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
     
        
        public async Task<bool> ExistedPermissionInGroup(int userId,int permissionId)
        {
            bool result = false;
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
                    var permissionByGroup = await _ossAuthGroupPermissionsRepository.GetByGroup(filter);
                    if (permissionByGroup.Data != null && permissionByGroup.Data.Count > 0)
                    {
                        foreach (var itemPermissionByGroup in permissionByGroup.Data)
                        {
                            if (itemPermissionByGroup.PermisionId == permissionId)
                            {
                                result = true;
                                return result;
                            }
                        }
                    }
                }
                
                
             
            }
            
          
            
            return result;

        }

        
        public async Task<ResultDto<UserPermissionDto>> GetUserPermissions(string login)
        {

            ResultDto<UserPermissionDto> result = new ResultDto<UserPermissionDto>(null);
            UserPermissionDto userPermissionDto = new UserPermissionDto();
            
            var user = await _sisUsuarioRepository.GetByLogin(login);
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

         public async Task<List<AuthModelUserAction>> GetModelUserAction(int userId)
          {
              List<AuthModelUserAction> result = new List<AuthModelUserAction>();
              var cacheKey = $"List<AuthModelUserAction>-{userId}";
              var listPermission= await _distributedCache.GetAsync(cacheKey);
              if ( listPermission!= null)
              {
                  result = System.Text.Json.JsonSerializer.Deserialize<List<AuthModelUserAction>> (listPermission);
              }
              else
              {

                  var user = await _sisUsuarioRepository.GetByCodigo(userId);

                  List<AuthPermissionResponseDto> allPermissions = new List<AuthPermissionResponseDto>();
                  AuthUserGroupFilterDto userGroupFilter = new AuthUserGroupFilterDto();
                  userGroupFilter.UserId = userId;
                  var groupsByUser = await _ossAuthUserGroupService.GetByUser(userGroupFilter);
                  if (groupsByUser.Data != null && groupsByUser.Data.Count > 0)
                  {
                      foreach (var itemGroupByUser in groupsByUser.Data)
                      {
                          AuthGroupPermissionFilterDto filter = new AuthGroupPermissionFilterDto();
                          filter.GroupId = itemGroupByUser.GroupId;
                          var permissionByGroup = await _ossAuthGroupPermissionsRepository.GetByGroup(filter);
                          if (permissionByGroup.Data != null && permissionByGroup.Data.Count > 0)
                          {
                              foreach (var itemPermissionByGroup in permissionByGroup.Data)
                              {
                                  AuthPermissionResponseDto allPermissionsItem = new AuthPermissionResponseDto();
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

                  AuthUserPermisionFilterDto filterUserPermission = new AuthUserPermisionFilterDto();
                  filterUserPermission.UserId = userId;
                  var permissionByUser = await _ossAuthUserPermissionRepository.GetByUser(filterUserPermission);
                  if (permissionByUser.Data != null && permissionByUser.Data.Count > 0)
                  {
                      foreach (var itempermissionByUser in permissionByUser.Data)
                      {
                          AuthPermissionResponseDto allPermissionsItem = new AuthPermissionResponseDto();
                          AuthPermissionFilterDto permissionFilter = new AuthPermissionFilterDto();
                          permissionFilter.Id = itempermissionByUser.PermisionId;
                          var permission = await _ossAuthPermissionsService.GetById(permissionFilter);
                          if (permission.Data != null)
                          {
                              allPermissions.Add(permission.Data);
                          }
                      }
                  }

                  foreach (var item in allPermissions)
                  {

                      AuthModelUserAction newPermisionByModel = new AuthModelUserAction();
                      newPermisionByModel.Model = item.Model;
                      newPermisionByModel.Action = item.Codename;
                      newPermisionByModel.Login = user.LOGIN;

                      result.Add(newPermisionByModel);

                  }


                  var options = new DistributedCacheEntryOptions()
                      .SetAbsoluteExpiration(DateTime.Now.AddDays(1))
                      .SetSlidingExpiration(TimeSpan.FromDays(1));
                  var serializedList = System.Text.Json.JsonSerializer.Serialize(result);
                  var redisListBytes = Encoding.UTF8.GetBytes(serializedList);
                  await _distributedCache.SetAsync(cacheKey,redisListBytes,options);
                  
              }
              return result;
          }
         
         public async Task<ResultDto<bool>> ValidUserModel(int userId,string model,string action )
          {
              ResultDto<bool> result = new ResultDto<bool>(false);

              var user = await _sisUsuarioRepository.GetByCodigo(userId);
              if (user == null)
              {
                  result.Data = false;
                  result.IsValid = false;
                  result.Message = "Usuario Invalido";
                  return result;
              }
              if (user.IS_SUPERUSER==1)
              {
                  result.Data = true;
                  result.IsValid = true;
                  result.Message = "";
              }

              var modelUser = await   GetModelUserAction(user.CODIGO_USUARIO);
              if (modelUser == null || modelUser.Count==0)
              {
                  result.Data = false;
                  result.IsValid = false;
                  result.Message = "Usuario No Autorizado";
                  return result;
              }

              var searchModelUser = modelUser.Where(x => x.Model == model && x.Action==action).FirstOrDefault();
              if (searchModelUser == null )
              {
                  result.Data = false;
                  result.IsValid = false;
                  result.Message = "Usuario No Autorizado";
                  return result;
              }
              else
              {
                  result.Data = true;
                  result.IsValid = true;
                  result.Message = "";
                  return result;
              }

            
              return result;
          }




    }
}

