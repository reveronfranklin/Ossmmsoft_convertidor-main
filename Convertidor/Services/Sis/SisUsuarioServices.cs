using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using AutoMapper;
using Convertidor.Data.Entities.Sis;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Dtos;
using Convertidor.Dtos.Sis;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Core.Types;

namespace Convertidor.Services.Sis
{
	public class SisUsuarioServices: ISisUsuarioServices
    {
		
        private readonly ISisUsuarioRepository _repository;
      

     

        private readonly IHttpContextAccessor _httpContextAccessor;

        public SisUsuarioServices(ISisUsuarioRepository repository,
                                    IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
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
                login = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
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




    }
}

