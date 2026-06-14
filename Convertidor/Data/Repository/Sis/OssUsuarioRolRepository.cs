using Convertidor.Data.Entities.Sis;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

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
                if (string.IsNullOrWhiteSpace(usuario))
                {
                    return new List<OSS_USUARIO_ROL>();
                }

                var usuarioNormalizado = usuario.Trim();
                var usuarioMayusculas = usuarioNormalizado.ToUpper();
                var usuarioMinusculas = usuarioNormalizado.ToLower();
                var result = new List<OSS_USUARIO_ROL>();
                var connectionString = _configuration.GetConnectionString("DefaultConnectionSIS")
                    ?? _context.Database.GetDbConnection().ConnectionString;

                using var connection = new OracleConnection(connectionString);
                await connection.OpenAsync();

                using var command = new OracleCommand(@"
                    SELECT CODIGO_USUARIO_ROL,
                           CODIGO_USUARIO,
                           USUARIO,
                           DESCRIPCION,
                           JSON_MENU
                      FROM SIS.OSS_USUARIO_ROL
                     WHERE USUARIO IN (:p_USUARIO, :p_USUARIO_MAYUS, :p_USUARIO_MINUS)
                     ORDER BY CODIGO_USUARIO_ROL", connection)
                {
                    BindByName = true
                };

                command.Parameters.Add("p_USUARIO", OracleDbType.Varchar2).Value = usuarioNormalizado;
                command.Parameters.Add("p_USUARIO_MAYUS", OracleDbType.Varchar2).Value = usuarioMayusculas;
                command.Parameters.Add("p_USUARIO_MINUS", OracleDbType.Varchar2).Value = usuarioMinusculas;

                using var reader = (OracleDataReader)await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    result.Add(new OSS_USUARIO_ROL
                    {
                        CODIGO_USUARIO_ROL = GetInt32(reader, "CODIGO_USUARIO_ROL"),
                        CODIGO_USUARIO = GetInt32(reader, "CODIGO_USUARIO"),
                        USUARIO = GetString(reader, "USUARIO"),
                        DESCRIPCION = GetString(reader, "DESCRIPCION"),
                        JSON_MENU = GetClobString(reader, "JSON_MENU")
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException?.Message ?? ex.Message;
                return new List<OSS_USUARIO_ROL>();
            }


        }

        private static int GetInt32(OracleDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? 0 : Convert.ToInt32(reader.GetValue(ordinal));
        }

        private static string GetString(OracleDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? string.Empty : reader.GetValue(ordinal).ToString() ?? string.Empty;
        }

        private static string GetClobString(OracleDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            if (reader.IsDBNull(ordinal))
            {
                return string.Empty;
            }

            using OracleClob clob = reader.GetOracleClob(ordinal);
            return clob.IsNull ? string.Empty : clob.Value;
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
