﻿using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Dtos.Bm;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
	public class BM_V_BM1Repository :IBM_V_BM1Repository
    {
		

        private readonly DataContextBm _context;
        private readonly IConfiguration _configuration;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;

        public BM_V_BM1Repository(DataContextBm context,
            IConfiguration configuration,  
            ISisUsuarioRepository sisUsuarioRepository
            )
        {
            _context = context;
            _configuration = configuration;
            _sisUsuarioRepository = sisUsuarioRepository;
        }


        public List<ICPGetDto> GetICP()
        {
           
          
            var lista = from s in  _context.BM_V_BM1
                group s by new
                {
                    CodigoIcp=s.CODIGO_ICP,
                    UnidadTrabajo = s.UNIDAD_TRABAJO,
                                    
                 
                                      
                } into g
                select new ICPGetDto()
                {

                    CodigoIcp = g.Key.CodigoIcp,
                    UnidadTrabajo = g.Key.UnidadTrabajo,
                                   

                };
            return lista.ToList();

        }
        
        

        public async Task<List<BM_V_BM1>> GetAll()
        {
            try{
            
            
                var conectado = await _sisUsuarioRepository.GetConectado();
                var result = await _context.BM_V_BM1.DefaultIfEmpty().Where(b=>b.CODIGO_EMPRESA==conectado.Empresa).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }

        
        
        public async Task<List<BM_V_BM1>> GetAllByCodigoIcp(int codigoIcp)
        {
            try{
            
            
                var conectado = await _sisUsuarioRepository.GetConectado();
                var result = await _context.BM_V_BM1.DefaultIfEmpty().Where(b=>b.CODIGO_EMPRESA==conectado.Empresa && b.CODIGO_ICP==codigoIcp).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }



        public async Task<List<BM_V_BM1>> GetByPlaca(int codigoBien)
        {
            try
            {


                var conectado = await _sisUsuarioRepository.GetConectado();
                var result = await _context.BM_V_BM1.DefaultIfEmpty().Where(b => b.CODIGO_EMPRESA == conectado.Empresa && b.CODIGO_BIEN==codigoBien).ToListAsync();
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

