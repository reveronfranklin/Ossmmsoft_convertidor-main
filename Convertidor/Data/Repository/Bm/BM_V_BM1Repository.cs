using System;
using System.Threading.Tasks;
using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Entities.Catastro;
using Convertidor.Data.EntitiesDestino;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Data.Interfaces.Catastro;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
	public class BM_V_BM1Repository :IBM_V_BM1Repository
    {
		

        private readonly DataContextBm _context;
        private readonly IConfiguration _configuration;
        public BM_V_BM1Repository(DataContextBm context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

     


        public async Task<List<BM_V_BM1>> GetAll()
        {
            try
            {
                var result = await _context.BM_V_BM1.DefaultIfEmpty().ToListAsync();
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

