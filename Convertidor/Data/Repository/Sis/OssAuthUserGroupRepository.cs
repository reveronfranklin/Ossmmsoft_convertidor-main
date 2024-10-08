﻿using Convertidor.Data.Entities.Sis;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;

namespace Convertidor.Data.Repository.Sis
{
	public class OssAuthUserGroupRepository: Interfaces.Sis.IOssAuthUserGroupRepository
    {
		

        private readonly DataContextSis _context;
        public OssAuthUserGroupRepository(DataContextSis context)
        {
            _context = context;
      
        }

        public async Task<List<AUTH_USER_GROUPS>> GetALL()
        {
            try
            {
                var result = await _context.AUTH_USER_GROUPS.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        
        public async Task<AUTH_USER_GROUPS> GetByUserGroup(int userId,int groupId)
        {
            try
            {
                var result = await _context.AUTH_USER_GROUPS.DefaultIfEmpty().Where(x => x.USER_ID == userId && x.GROUP_ID==groupId).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }
        public async Task<List<AUTH_USER_GROUPS>> GetByUser(int userId)
        {
            try
            {
                var result = await _context.AUTH_USER_GROUPS.DefaultIfEmpty().Where(x => x.USER_ID == userId ).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<AUTH_USER_GROUPS> GetByID(int id)
        {
            try
            {
                var result = await _context.AUTH_USER_GROUPS.DefaultIfEmpty().Where(x => x.ID == id).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<ResultDto<AUTH_USER_GROUPS>> Add(AUTH_USER_GROUPS entity)
        {
            ResultDto<AUTH_USER_GROUPS> result = new ResultDto<AUTH_USER_GROUPS>(null);
            try
            {

                entity.ID = await GetNextKey();

                await _context.AUTH_USER_GROUPS.AddAsync(entity);
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

        public async Task<ResultDto<AUTH_USER_GROUPS>> Update(AUTH_USER_GROUPS entity)
        {
            ResultDto<AUTH_USER_GROUPS> result = new ResultDto<AUTH_USER_GROUPS>(null);

            try
            {
                AUTH_USER_GROUPS entityUpdate = await GetByID(entity.ID);
                if (entityUpdate != null)
                {


                    _context.AUTH_USER_GROUPS.Update(entity);
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
                var last = await _context.AUTH_USER_GROUPS.DefaultIfEmpty()
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
                    _context.AUTH_USER_GROUPS.Remove(entity);
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

