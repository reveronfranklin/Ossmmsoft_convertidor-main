using Convertidor.Data.Interfaces.Bm;
using Convertidor.Dtos.Bm;
using Ganss.Excel;
namespace Convertidor.Services.Catastro
{
	public class BM_V_BM1Service: IBM_V_BM1Service
    {
		

        private readonly IBM_V_BM1Repository _repository;
        private readonly IConfiguration _configuration;
        private readonly IBmBienesFotoRepository _bmBienesFotoRepository;

        public BM_V_BM1Service(IBM_V_BM1Repository repository,IConfiguration configuration,IBmBienesFotoRepository bmBienesFotoRepository)

        {
            _repository = repository;
            _configuration = configuration;
            _bmBienesFotoRepository = bmBienesFotoRepository;
        }

      

        public async Task<ResultDto<List<Bm1GetDto>>> GetAll()
        {
           
           
            ResultDto<List<Bm1GetDto>> response = new ResultDto<List<Bm1GetDto>>(null);
            try
            {

                //var bienesFoto =  _bmBienesFotoRepository.BienesConFoto();
                
                var result = await _repository.GetAll();
          
                    var lista = from s in result
                                  group s by new
                                  {
                                      UnidadTrabajo = s.UNIDAD_TRABAJO,
                                      CodigoGrupo = s.CODIGO_GRUPO,
                                      CodigoNivel1 = s.CODIGO_NIVEL1,
                                      CodigoNivel2 = s.CODIGO_NIVEL2,
                                      NumeroLote = s.NUMERO_LOTE,
                                      Cantidad = s.CANTIDAD,
                                      NumeroPlaca = s.NUMERO_PLACA,
                                      Articulo = s.ARTICULO,
                                      Especificacion = s.ESPECIFICACION,
                                      Servicio = s.SERVICIO,
                                      ResponsableBien = s.RESPONSABLE_BIEN,
                                      CodigoBien = s.CODIGO_BIEN,
                                      CodigoMovBien=s.CODIGO_MOV_BIEN
                 
                                      
                                  } into g
                                  select new Bm1GetDto()
                                  {

                                  
                                      UnidadTrabajo = g.Key.UnidadTrabajo,
                                      CodigoGrupo = g.Key.CodigoGrupo,
                                      CodigoNivel1 = g.Key.CodigoNivel1,
                                      CodigoNivel2 = g.Key.CodigoNivel2,
                                      NumeroLote = g.Key.NumeroLote,
                                      Cantidad = g.Key.Cantidad,
                                      NumeroPlaca = g.Key.CodigoGrupo + "-" + g.Key.CodigoNivel1 +"-" +  g.Key.CodigoNivel2 + "-"+g.Key.NumeroPlaca,
                                      Articulo = g.Key.Articulo,
                                      Especificacion = g.Key.Especificacion,   
                                      Servicio = g.Key.Servicio,
                                      ResponsableBien = g.Key.ResponsableBien,
                                      CodigoBien = g.Key.CodigoBien,
                                      CodigoMovBien = g.Key.CodigoMovBien

                                  };
                    
                
                       var listaExcel = from s in result
                                  group s by new
                                  {
                                      UnidadTrabajo = s.UNIDAD_TRABAJO,
                                      CodigoGrupo = s.CODIGO_GRUPO,
                                      CodigoNivel1 = s.CODIGO_NIVEL1,
                                      CodigoNivel2 = s.CODIGO_NIVEL2,
                                      NumeroLote = s.NUMERO_LOTE,
                                      Cantidad = s.CANTIDAD,
                                      NumeroPlaca = s.NUMERO_PLACA,
                                      Articulo = s.ARTICULO,
                                      Especificacion = s.ESPECIFICACION,
                                      Servicio = s.SERVICIO,
                                      ResponsableBien = s.RESPONSABLE_BIEN,
                                      CodigoBien = s.CODIGO_BIEN,
                                      CodigoMovBien=s.CODIGO_MOV_BIEN
                 
                                      
                                  } into g
                                  select new Bm1ExcelGetDto()
                                  {

                                  
                                      UnidadTrabajo = g.Key.UnidadTrabajo,
                                    
                                      Cantidad = g.Key.Cantidad,
                                      NumeroPlaca = g.Key.CodigoGrupo + "-" + g.Key.CodigoNivel1 +"-" +  g.Key.CodigoNivel2 + "-"+g.Key.NumeroPlaca,
                                      Articulo = g.Key.Articulo,
                                      Especificacion = g.Key.Especificacion,   
                                      Servicio = g.Key.Servicio,
                                      ResponsableBien = g.Key.ResponsableBien,
                                    

                                  };
                    ExcelMapper mapper = new ExcelMapper();


                    var settings = _configuration.GetSection("Settings").Get<Settings>();

               
                    var ruta = @settings.ExcelFiles;  //@"/Users/freveron/Documents/MM/App/full-version/public/ExcelFiles";
                    var fileName = $"BM1.xlsx";
                    string newFile = Path.Combine(Directory.GetCurrentDirectory(), ruta, fileName);

                    var excelData = listaExcel.ToList();
                    mapper.Save(newFile, excelData, $"BM1", true);
                  
                    
                    
                response.Data = lista.ToList();
                response.IsValid = true;
                response.Message = "";
                response.LinkData= $"/ExcelFiles/{fileName}";
                return response;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.IsValid = true;
                response.Message = ex.InnerException.Message;
                return response;
            }
           
        }

     


      

    }
}

