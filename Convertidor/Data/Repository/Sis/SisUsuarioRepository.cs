using System;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Data.Entities.Sis;
using Convertidor.Dtos;
using Convertidor.Dtos.Sis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using Oracle.ManagedDataAccess.Client;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Convertidor.Data.Repository.Sis
{
	public class SisUsuarioRepository: Interfaces.Sis.ISisUsuarioRepository
    {
		

        private readonly DataContextSis _context;
        private readonly IConfiguration _configuration;
        public SisUsuarioRepository(DataContextSis context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<SIS_USUARIOS>> GetALL()
        {
            try
            {
                var result = await _context.SIS_USUARIOS.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }


        public async Task<SIS_USUARIOS> GetByLogin(string login)
        {
            string newlogin = login;
            if (login.Contains("@"))
            {
                var listStrLineElements = login.Split('@').ToArray();
                newlogin = listStrLineElements[0];
            }
            try
            {
                var result = await _context.SIS_USUARIOS.DefaultIfEmpty().Where(x=>x.LOGIN== newlogin).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<ResultDto<SIS_USUARIOS>> Update(SIS_USUARIOS entity)
        {
            ResultDto<SIS_USUARIOS> result = new ResultDto<SIS_USUARIOS>(null);

            try
            {
                SIS_USUARIOS entityUpdate = await _context.SIS_USUARIOS.DefaultIfEmpty().Where(x => x.CODIGO_USUARIO == entity.CODIGO_USUARIO).FirstOrDefaultAsync();
                if (entityUpdate != null)
                {
                    _context.SIS_USUARIOS.Update(entity);
                    _context.SaveChanges();
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
                result.Message = ex.InnerException.Message;
                return result;
            }






        }
        public async Task<ResultLoginDto> Login(LoginDto dto)
        {
           
            ResultLoginDto resultLogin = new ResultLoginDto();
            try
            {
               
                FormattableString xquery = $"UPDATE SIS.SIS_USUARIOS SET PASSWORDSTRING=SIS.SIS_DESENCRYPTED(PASSWORD) WHERE LOGIN={dto.Login};";
                var result = _context.Database.ExecuteSqlInterpolated(xquery);


              
                var resultDiario = await _context.SIS_USUARIOS.Where(x => x.LOGIN == dto.Login).DefaultIfEmpty().FirstOrDefaultAsync();
                
                FormattableString xqueryClean = $"UPDATE SIS.SIS_USUARIOS SET PASSWORDSTRING='' WHERE LOGIN={dto.Login} ;";
                var resultClean = _context.Database.ExecuteSqlInterpolated(xqueryClean);
                if (resultDiario==null)
                {
                    resultLogin.Message = "Usuario o Clave invalidos";
                    resultLogin.accessToken = "";
                    resultLogin.Name = "";
                    UserData userData = new UserData();
                    userData.Id = 0;
                    userData.username = "";
                    userData.FullName = "";
                    userData.Role = "";
                    userData.Email = "";
                    resultLogin.UserData = userData;
                    return resultLogin;
                }
                else
                {
                    if (resultDiario.PASSWORDSTRING == dto.Password)
                    {
                        resultLogin.Message = "";

                        resultLogin.accessToken = GetToken(resultDiario);
                        resultLogin.Name = resultDiario.LOGIN;
                        UserData userData = new UserData();
                        userData.Id = resultDiario.CODIGO_USUARIO;
                        userData.username = resultDiario.LOGIN;
                        userData.FullName = resultDiario.USUARIO;
                        userData.Role = "admin";
                        userData.Email = $"{resultDiario.LOGIN}@ossmasoft.com";
                        resultLogin.UserData = userData;
                        return resultLogin;
                    }
                    else
                    {
                        resultLogin.Message = "Usuario o Clave invalidos";
                        resultLogin.accessToken = "";
                        resultLogin.Name = "";

                        UserData userData = new UserData();
                        userData.Id = 0;
                        userData.username = "";
                        userData.FullName = "";
                        userData.Role = "";
                        userData.Email = "";
                        resultLogin.UserData = userData;
                        return resultLogin;
                    }
                }

               
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                resultLogin.Message = "Usuario o Clave invalidos";
                resultLogin.accessToken = "";
                resultLogin.Name = "";
                UserData userData = new UserData();
                userData.Id = 0;
                userData.username = "";
                userData.FullName = "";
                userData.Role = "";
                userData.Email = "";
                resultLogin.UserData = userData;
                return resultLogin;
            }



         


        }

        public string GetToken(SIS_USUARIOS usuario) {
            
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,usuario.LOGIN),

                new Claim(ClaimTypes.Role,"Admin")
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires:DateTime.Now.AddDays(30),
                signingCredentials:cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


    }

}

