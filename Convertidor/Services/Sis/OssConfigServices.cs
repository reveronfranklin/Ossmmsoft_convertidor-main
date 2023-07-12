using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using AutoMapper;
using Convertidor.Data.Entities.Sis;
using Convertidor.Data.Interfaces.Presupuesto;
using Convertidor.Data.Interfaces.Sis;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;
using Convertidor.Dtos.Sis;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Core.Types;

namespace Convertidor.Services.Sis
{
	public class OssServices: IOssConfigServices
    {
		
        private readonly IOssConfigRepository _repository;
        private readonly IConfiguration _configuration;



        private readonly IHttpContextAccessor _httpContextAccessor;

        public OssServices(IOssConfigRepository repository,
                                    IHttpContextAccessor httpContextAccessor,
                                    IConfiguration configuration)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<ResultDto<List<OssConfigGetDto>>> GetListByClave(string clave)
        {

            ResultDto<List<OssConfigGetDto>> result = new ResultDto<List<OssConfigGetDto>>(null);
            try
            {
                var ossConfig = await _repository.GetListByClave(clave);
                var qossConfig = from s in ossConfig.ToList()
                              group s by new
                              {
                                  Clave = s.CLAVE,
                                  Valor = s.VALOR,
                                
                              } into g
                              select new OssConfigGetDto
                              {

                                  Clave = g.Key.Clave,
                                  Valor = g.Key.Valor,
                                

                              };
                result.Data= qossConfig.OrderBy(X=>X.Valor).ToList();
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







    }
}

