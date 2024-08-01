using Convertidor.Data.Entities.Sis;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Sis
{
	public class OssUsuarioRolRepository: Interfaces.Sis.IOssUsuarioRolRepository
    {
		

        private readonly DataContextSis _context;
        private readonly IConfiguration _configuration;
        public OssUsuarioRolRepository(DataContextSis context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<OSS_USUARIO_ROL>> GetALL()
        {
            try
            {
                var result = await _context.OSS_USUARIO_ROL.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

      

        public async Task<OSS_USUARIO_ROL> GetByCodigo(int codigoUsuarioRol)
        {
            try
            {
                var result = await _context.OSS_USUARIO_ROL.DefaultIfEmpty().Where(x => x.CODIGO_USUARIO_ROL == codigoUsuarioRol).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }
        
        public async Task<List<OSS_USUARIO_ROL>> GetByCodigoUsuario(int codigoUsuario)
        {
            try
            {
                var result = await _context.OSS_USUARIO_ROL.DefaultIfEmpty().Where(x => x.CODIGO_USUARIO == codigoUsuario).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }
        
        public async Task<List<OSS_USUARIO_ROL>> GetByUsuario(string usuario)
        {
            try
            {
                var result = await _context.OSS_USUARIO_ROL.DefaultIfEmpty().Where(x => x.USUARIO.Trim().ToLower() == usuario.Trim().ToLower()).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<ResultDto<OSS_USUARIO_ROL>> Add(OSS_USUARIO_ROL entity)
        {
            ResultDto< OSS_USUARIO_ROL> result = new ResultDto<OSS_USUARIO_ROL>(null);
            try
            {

                entity.CODIGO_USUARIO_ROL = await GetNextKey();

                await _context.OSS_USUARIO_ROL.AddAsync(entity);
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

        public async Task<ResultDto<OSS_USUARIO_ROL>> Update(OSS_USUARIO_ROL entity)
        {
            ResultDto<OSS_USUARIO_ROL> result = new ResultDto<OSS_USUARIO_ROL>(null);

            try
            {
                OSS_USUARIO_ROL entityUpdate = await GetByCodigo(entity.CODIGO_USUARIO_ROL);
                if (entityUpdate != null)
                {


                    _context.OSS_USUARIO_ROL.Update(entity);
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
                var last = await _context.OSS_USUARIO_ROL.DefaultIfEmpty()
                    .OrderByDescending(x => x.CODIGO_USUARIO_ROL)
                    .FirstOrDefaultAsync();
                if (last == null)
                {
                    result = 1;
                }
                else
                {
                    result = last.CODIGO_USUARIO_ROL + 1;
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

