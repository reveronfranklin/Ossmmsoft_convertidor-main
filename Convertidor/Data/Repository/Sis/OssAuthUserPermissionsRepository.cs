using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos.Sis;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;

namespace Convertidor.Data.Repository.Sis
{
	public class OssAuthUserPermissionsRepository: IOssAuthUserPermissionsRepository
    {
		

        private readonly DataContextSis _context;
        public OssAuthUserPermissionsRepository(DataContextSis context)
        {
            _context = context;
      
        }

        public async Task<List<AUTH_USER_USER_PERMISSIONS>> GetALL()
        {
            try
            {
                var result = await _context.AUTH_USER_USER_PERMISSIONS.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }
        public async Task<List<AUTH_USER_USER_PERMISSIONS>> GetByUser(int userId)
        {
            try
            {
                var result = await _context.AUTH_USER_USER_PERMISSIONS.DefaultIfEmpty().Where(x=>x.USER_ID==userId).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public  async Task<AuthUserPermisionResponseDto> MapDto(AUTH_USER_USER_PERMISSIONS entity)
        {
            AuthUserPermisionResponseDto itemResult = new AuthUserPermisionResponseDto();
            
            try
            {
                if (entity == null)
                {
                    return itemResult;
                }
                itemResult.Id = entity.ID;
             
                itemResult.UserId = entity.USER_ID;
                itemResult.UserName = "";
                var user = await _context.SIS_USUARIOS.DefaultIfEmpty().Where(x=> x.CODIGO_USUARIO== entity.USER_ID).FirstOrDefaultAsync();
                if (user != null)
                {
                    itemResult.UserName = user.USUARIO;
                    itemResult.Login = user.LOGIN;
                }
                itemResult.PermisionId = entity.PERMISSION_ID;
                itemResult.DescriptionPermision = "";
                var permission = await  _context.AUTH_PERMISSION.Where(x=>x.ID==itemResult.PermisionId).FirstOrDefaultAsync();
                if (permission != null)
                {
                    var contentType = await _context.AUTH_CONTENT_TYPE.Where(x => x.ID == permission.CONTENT_TYPE_ID)
                        .FirstOrDefaultAsync();
                    itemResult.DescriptionPermision = $"{contentType.APP_LABEL}-{contentType.MODEL}-{permission.NAME}";
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

        public async Task< List<AuthUserPermisionResponseDto>> MapList(List<AUTH_USER_USER_PERMISSIONS> dtos)
        {
            List<AuthUserPermisionResponseDto> result = new List<AuthUserPermisionResponseDto>();
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
      
        public async Task<ResultDto<List<AuthUserPermisionResponseDto>>> GetByUser(AuthUserPermisionFilterDto dto)
        { 
            ResultDto<List<AuthUserPermisionResponseDto>> result = new ResultDto<List<AuthUserPermisionResponseDto>>(null);
            try
            {

                var userPermission = await GetByUser(dto.UserId);
                if (userPermission == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No existen Permisos para este Usuario";
                    return result;
                }
                
                var resultDto =  await MapList(userPermission);
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

        
        public async Task<AUTH_USER_USER_PERMISSIONS> GetByUserPermision(int userId,int permissionId)
        {
            try
            {
                var result = await _context.AUTH_USER_USER_PERMISSIONS.DefaultIfEmpty().Where(x => x.USER_ID == userId && x.PERMISSION_ID==permissionId).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<AUTH_USER_USER_PERMISSIONS> GetByID(int id)
        {
            try
            {
                var result = await _context.AUTH_USER_USER_PERMISSIONS.DefaultIfEmpty().Where(x => x.ID == id).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<ResultDto<AUTH_USER_USER_PERMISSIONS>> Add(AUTH_USER_USER_PERMISSIONS entity)
        {
            ResultDto<AUTH_USER_USER_PERMISSIONS> result = new ResultDto<AUTH_USER_USER_PERMISSIONS>(null);
            try
            {

                entity.ID = await GetNextKey();

                await _context.AUTH_USER_USER_PERMISSIONS.AddAsync(entity);
                await _context.SaveChangesAsync();


                result.Data = entity;
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

        public async Task<ResultDto<AUTH_USER_USER_PERMISSIONS>> Update(AUTH_USER_USER_PERMISSIONS entity)
        {
            ResultDto<AUTH_USER_USER_PERMISSIONS> result = new ResultDto<AUTH_USER_USER_PERMISSIONS>(null);

            try
            {
                AUTH_USER_USER_PERMISSIONS entityUpdate = await GetByID(entity.ID);
                if (entityUpdate != null)
                {


                    _context.AUTH_USER_USER_PERMISSIONS.Update(entity);
                    await _context.SaveChangesAsync();
                    result.Data = entity;
                    result.IsValid = true;
                    result.Message = "";

                }
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

        public async Task<int> GetNextKey()
        {
            try
            {
                int result = 0;
                var last = await _context.AUTH_USER_USER_PERMISSIONS.DefaultIfEmpty()
                    .OrderByDescending(x => x.ID)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.ID + 1;
                }

                return (int)result!;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return 0;
            }



        }

        public async Task<string> Delete(int id)
        {

            try
            {
                var entity = await GetByID(id);
                if (entity != null)
                {
                    _context.AUTH_USER_USER_PERMISSIONS.Remove(entity);
                    await _context.SaveChangesAsync();
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }



        }

    }

}

