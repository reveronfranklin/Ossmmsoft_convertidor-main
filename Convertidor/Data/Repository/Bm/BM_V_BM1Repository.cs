﻿using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Dtos.Bm;
using Convertidor.Dtos.Bm.Mobil;
using ImageMagick;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data.Repository.Catastro
{
	public class BM_V_BM1Repository :IBM_V_BM1Repository
    {
		

        private readonly DataContextBm _context;
        private readonly DataContextBmConteo _contextBmConteo;
        private readonly IConfiguration _configuration;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IBmBienesFotoRepository _fotoRepository;

        public BM_V_BM1Repository(DataContextBm context,
            DataContextBmConteo contextBmConteo,
            IConfiguration configuration,  
            ISisUsuarioRepository sisUsuarioRepository,
            IBmBienesFotoRepository fotoRepository
            )
        {
            _context = context;
            _contextBmConteo = contextBmConteo;
            _configuration = configuration;
            _sisUsuarioRepository = sisUsuarioRepository;
            _fotoRepository = fotoRepository;
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
        
        public async Task<List<BmPlacas>> GetPlacas()
        {
           
            var conectado = await _sisUsuarioRepository.GetConectado();
            var lista = from s in  _context.BM_V_BM1.Where(x=>x.CODIGO_EMPRESA==conectado.Empresa)
                group s by new
                {
                    Id = s.CODIGO_BIEN,
                    NumeroPlaca=s.NRO_PLACA,
                  
                                    
                 
                                      
                } into g
                select new BmPlacas()
                {
                    Id = g.Key.Id,
                    NumeroPlaca = g.Key.NumeroPlaca
                   
                                   

                };
            return lista.ToList();

        }
        
        public void MinFile(string nroPlaca,string foto)
        {
            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var destino = @settings.BmFiles;
            var filePatch = $"{destino}{nroPlaca}/{foto}";
            var newFoto = $"min_{foto}";
            var newfilePatch = $"{destino}{nroPlaca}/{newFoto}";

            if (System.IO.File.Exists(filePatch))
            {
                using (MagickImage oMagickImage = new MagickImage(filePatch))
                {
                    oMagickImage.Resize(900,0);
                    oMagickImage.Write(newfilePatch);
                }
                
            }
        }
        public async Task<List<ProductResponse>> GetProductMobil(ProductFilterDto filter) 
        {
            List<ProductResponse> result = new List<ProductResponse>();
            var conectado = await _sisUsuarioRepository.GetConectado();
            if (filter.PageNumber == 0) filter.PageNumber = 1;
            if (filter.PageSize == 0) filter.PageSize = 20;
            //if (filter.PageSize >100) filter.PageSize = 20;
            
            try
            {
                
                List<BM_V_BM1> pageData = new List<BM_V_BM1>();
                if (filter.SearhText.Length > 0)
                {
                    pageData = await _contextBmConteo.BM_V_BM1.DefaultIfEmpty()
                        .Where(x=>x.CODIGO_EMPRESA==conectado.Empresa && x.NRO_PLACA.Trim().ToLower().Contains(filter.SearhText.Trim().ToLower()))
                        .OrderBy(x => x.CODIGO_BIEN)
                        .ToListAsync();
                }
                else
                {
                    if (filter.CodigoDepartamentoResponsable > 0)
                    {
                        pageData = await _contextBmConteo.BM_V_BM1.DefaultIfEmpty()
                            .Where(x =>x.CODIGO_EMPRESA==conectado.Empresa && x.CODIGO_ICP==filter.CodigoDepartamentoResponsable )
                            .OrderBy(x => x.CODIGO_BIEN)
                            .Skip((filter.PageNumber - 1) * filter.PageSize)
                            .Take(filter.PageSize)
                            .ToListAsync();
                    }
                    else
                    {
                        pageData = await _contextBmConteo.BM_V_BM1.DefaultIfEmpty()
                            .Where(x=>x.CODIGO_EMPRESA==conectado.Empresa)
                            .OrderBy(x => x.CODIGO_BIEN)
                            .Skip((filter.PageNumber - 1) * filter.PageSize)
                            .Take(filter.PageSize)
                            .ToListAsync();
                    }
                }
              

              
  
                foreach (var item in pageData)
                {
                    ProductResponse itemData = new ProductResponse();
                    itemData.Key =item.CODIGO_BIEN.ToString();
                  
                    itemData.Id= item.CODIGO_BIEN;
                    itemData.Articulo= item.ARTICULO;
                    itemData.Descripcion= item.ESPECIFICACION;
                    if (item.RESPONSABLE_BIEN == null) item.RESPONSABLE_BIEN = "";
                    itemData.Responsable= item.RESPONSABLE_BIEN;
                    itemData.NroPlaca= item.NRO_PLACA;
                    itemData.CodigoDepartamentoResponsable= item.CODIGO_ICP;
                    itemData.DescripcionDepartamentoResponsable= item.UNIDAD_TRABAJO;
                    itemData.CodigoDirBien = item.CODIGO_DIR_BIEN;
                    List<string> listFotos = new List<string>(); 
                    /*var fotos = await _fotoRepository.GetByNumeroPlaca(item.NRO_PLACA);
                   
                    if (fotos != null && fotos.Count > 0)
                    {
                        foreach (var itemFotos in fotos)
                        {
                            listFotos.Add(itemFotos.FOTO);
                            MinFile(item.NRO_PLACA, itemFotos.FOTO);
                            
                        }
                     
                    }*/
                  
                    itemData.Images = listFotos.ToArray();
                    result.Add(itemData);
                }
                
             
                return result;
                
            }
            catch (Exception ex) 
            {
               
                return result;
            }
        }

        
           public async Task<ProductResponse> GetProductMobilById(ProductFilterDto filter) 
        {

            
            try
            {
                
                BM_V_BM1 item = new BM_V_BM1();

                item = await _contextBmConteo.BM_V_BM1.DefaultIfEmpty()
                    .Where(x =>x.CODIGO_BIEN==filter.CodigoBien )
                    .FirstOrDefaultAsync();
              
                    ProductResponse itemData = new ProductResponse();
                    itemData.Key = item.CODIGO_BIEN.ToString();
                    itemData.Id= item.CODIGO_BIEN;
                    itemData.Articulo= item.ARTICULO;
                    itemData.Descripcion= item.ESPECIFICACION;
                    if (item.RESPONSABLE_BIEN == null) item.RESPONSABLE_BIEN = "";
                    itemData.Responsable= item.RESPONSABLE_BIEN;
                    itemData.NroPlaca= item.NRO_PLACA;
                    itemData.CodigoDirBien = item.CODIGO_DIR_BIEN;
                    itemData.CodigoDepartamentoResponsable= item.CODIGO_ICP;
                    itemData.DescripcionDepartamentoResponsable= item.UNIDAD_TRABAJO;
                    var fotos = await _fotoRepository.GetByNumeroPlaca(item.NRO_PLACA);
                    List<string> listFotos = new List<string>(); 
                    if (fotos != null && fotos.Count > 0)
                    {
                        foreach (var itemFotos in fotos)
                        {
                            var foto = listFotos.Where(x => x == itemFotos.FOTO).FirstOrDefault();
                            if (foto == null)
                            {
                                listFotos.Add(itemFotos.FOTO);
                                MinFile(item.NRO_PLACA, itemFotos.FOTO);
                            }
                          
                            
                        }
                     
                    }
                    itemData.Images = listFotos.ToArray();
                  
                    
                
             
                return itemData;
                
            }
            catch (Exception ex) 
            {
               
                return null;
            }
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
        public async Task<BM_V_BM1> GetByNroPlaca(string nroPlaca)
        {
            try
            {


                var conectado = await _sisUsuarioRepository.GetConectado();
                var result = await _context.BM_V_BM1.DefaultIfEmpty().Where(b => b.CODIGO_EMPRESA == conectado.Empresa && b.NRO_PLACA==nroPlaca).FirstOrDefaultAsync();
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

