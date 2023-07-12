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
using StackExchange.Redis;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Convertidor.Data.Repository.Sis
{
	public class OssConfigRepository: Interfaces.Sis.IOssConfigRepository
    {
		

        private readonly DataContextSis _context;
        private readonly IConfiguration _configuration;
        public OssConfigRepository(DataContextSis context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<OSS_CONFIG>> GetALL()
        {
            try
            {
                var result = await _context.OSS_CONFIG.DefaultIfEmpty().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }

        public async Task<List<OSS_CONFIG>> GetListByClave(string clave)
        {
            try
            {
                var result = await _context.OSS_CONFIG.DefaultIfEmpty().Where(x=>x.CLAVE==clave).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }


        }



    }

}

