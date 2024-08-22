using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos.Sis;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;

namespace Convertidor.Data.Repository.Sis
{
	public class OssAuthGroupPermissionsRepository: IOssAuthGroupPermissionsRepository
    {
		

        private readonly DataContextSis _context;
        public OssAuthGroupPermissionsRepository(DataContextSis context)
        {
            _context = context;
      
        }

        public async Task<List<AUTH_GROUP_PERMISSIONS>> GetALL()
        {
            try
            {
                var result = await _context.AUTH_GROUP_PERMISSIONS.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

           public  async Task<AuthGroupPermisionResponseDto> MapDto(AUTH_GROUP_PERMISSIONS entity)
        {
            AuthGroupPermisionResponseDto itemResult = new AuthGroupPermisionResponseDto();
            
            try
            {
                if (entity == null)
                {
                    return itemResult;
                }
                itemResult.Id = entity.ID;
             
                itemResult.GroupId = entity.GROUP_ID;
                itemResult.GroupName = "";
                var group = await _context.AUTH_GROUP.Where(x=>x.ID== itemResult.GroupId).FirstOrDefaultAsync();
                if (group != null)
                {
                    itemResult.GroupName = group.NAME;
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

        public async Task< List<AuthGroupPermisionResponseDto>> MapList(List<AUTH_GROUP_PERMISSIONS> dtos)
        {
            List<AuthGroupPermisionResponseDto> result = new List<AuthGroupPermisionResponseDto>();
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
      
        public async Task<ResultDto<List<AuthGroupPermisionResponseDto>>> GetByGroup(AuthGroupPermissionFilterDto dto)
        { 
            ResultDto<List<AuthGroupPermisionResponseDto>> result = new ResultDto<List<AuthGroupPermisionResponseDto>>(null);
            try
            {

                var groupPermission = await GetByGroup(dto.GroupId);
                if (groupPermission == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "No existen Permisos para este Grupo";
                    return result;
                }
                
                var resultDto =  await MapList(groupPermission);
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


        public async Task<AUTH_GROUP_PERMISSIONS> GetByID(int id)
        {
            try
            {
                var result = await _context.AUTH_GROUP_PERMISSIONS.DefaultIfEmpty().Where(x => x.ID == id).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }
        public async Task<List<AUTH_GROUP_PERMISSIONS>> GetByGroup(int groupId)
        {
            try
            {
                var result = await _context.AUTH_GROUP_PERMISSIONS.DefaultIfEmpty().Where(x => x.GROUP_ID == groupId).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }
        public async Task<AUTH_GROUP_PERMISSIONS> GetByGroupPermission(int groupId , int permissionId)
        {
            try
            {
                var result = await _context.AUTH_GROUP_PERMISSIONS.DefaultIfEmpty().Where(x => x.GROUP_ID == groupId && x.PERMISSION_ID==permissionId).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<ResultDto<AUTH_GROUP_PERMISSIONS>> Add(AUTH_GROUP_PERMISSIONS entity)
        {
            ResultDto<AUTH_GROUP_PERMISSIONS> result = new ResultDto<AUTH_GROUP_PERMISSIONS>(null);
            try
            {

                entity.ID = await GetNextKey();

                await _context.AUTH_GROUP_PERMISSIONS.AddAsync(entity);
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

        public async Task<ResultDto<AUTH_GROUP_PERMISSIONS>> Update(AUTH_GROUP_PERMISSIONS entity)
        {
            ResultDto<AUTH_GROUP_PERMISSIONS> result = new ResultDto<AUTH_GROUP_PERMISSIONS>(null);

            try
            {
                AUTH_GROUP_PERMISSIONS entityUpdate = await GetByID(entity.ID);
                if (entityUpdate != null)
                {


                    _context.AUTH_GROUP_PERMISSIONS.Update(entity);
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

        public async Task<string> Delete(int id)
        {

            try
            {
                var entity = await GetByID(id);
                if (entity != null)
                {
                    _context.AUTH_GROUP_PERMISSIONS.Remove(entity);
                    await _context.SaveChangesAsync();
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public async Task<int> GetNextKey()
        {
            try
            {
                int result = 0;
                var last = await _context.AUTH_GROUP_PERMISSIONS.DefaultIfEmpty()
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



    }

}

