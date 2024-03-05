using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Dtos.Bm;
using Convertidor.Services.Bm;
using Ganss.Excel;
using iText.Barcodes;
using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Path = System.IO.Path;

namespace Convertidor.Services.Bm
{
	public class BM_V_BM1Service: IBM_V_BM1Service
    {
		

        private readonly IBM_V_BM1Repository _repository;
        private readonly IConfiguration _configuration;
     
  

        public BM_V_BM1Service(IBM_V_BM1Repository repository,
                                IConfiguration configuration
                               
                                )

        {
            _repository = repository;
            _configuration = configuration;
          

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
                                      CodigoIcp=s.CODIGO_ICP,
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
                                      CodigoMovBien=s.CODIGO_MOV_BIEN,
                                      FechaMovimiento =s.FECHA_MOVIMIENTO
                                      
                                  } into g
                                  select new Bm1GetDto()
                                  {

                                        CodigoIcp = g.Key.CodigoIcp,
                                      UnidadTrabajo = g.Key.UnidadTrabajo,
                                      CodigoGrupo = g.Key.CodigoGrupo,
                                      CodigoNivel1 = g.Key.CodigoNivel1,
                                      CodigoNivel2 = g.Key.CodigoNivel2,
                                      ConsecutivoPlaca=g.Key.NumeroPlaca,
                                      NumeroLote = g.Key.NumeroLote,
                                      Cantidad = g.Key.Cantidad,
                                      NumeroPlaca = g.Key.CodigoGrupo + "-" + g.Key.CodigoNivel1 +"-" +  g.Key.CodigoNivel2 + "-"+g.Key.NumeroPlaca,
                                      Articulo = g.Key.Articulo,
                                      Especificacion = g.Key.Especificacion,   
                                      Servicio = g.Key.Servicio,
                                      ResponsableBien = g.Key.ResponsableBien,
                                      CodigoBien = g.Key.CodigoBien,
                                      CodigoMovBien = g.Key.CodigoMovBien,
                                      FechaMovimiento=g.Key.FechaMovimiento

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
                  
                    
                    
                response.Data = lista.OrderBy(x=> x.CodigoGrupo)
                            .ThenBy(x=>x.CodigoNivel1)
                            .ThenBy(x=>x.CodigoNivel2)
                            .ThenBy(x=>x.ConsecutivoPlaca).ToList();
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
      public async Task<ResultDto<List<Bm1GetDto>>> GetAllByIcp(int codigoIcp)
            {
               
               
                ResultDto<List<Bm1GetDto>> response = new ResultDto<List<Bm1GetDto>>(null);
                try
                {

                    //var bienesFoto =  _bmBienesFotoRepository.BienesConFoto();
                    
                    var result = await _repository.GetAllByCodigoIcp(codigoIcp);
              
                        var lista = from s in result
                                      group s by new
                                      {
                                          CodigoIcp=s.CODIGO_ICP,
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
                                          CodigoMovBien=s.CODIGO_MOV_BIEN,
                                          FechaMovimiento=s.FECHA_MOVIMIENTO
                     
                                          
                                      } into g
                                      select new Bm1GetDto()
                                      {

                                            CodigoIcp = g.Key.CodigoIcp,
                                          UnidadTrabajo = g.Key.UnidadTrabajo,
                                          CodigoGrupo = g.Key.CodigoGrupo,
                                          CodigoNivel1 = g.Key.CodigoNivel1,
                                          CodigoNivel2 = g.Key.CodigoNivel2,
                                          ConsecutivoPlaca=g.Key.NumeroPlaca,
                                          NumeroLote = g.Key.NumeroLote,
                                          Cantidad = g.Key.Cantidad,
                                          NumeroPlaca = g.Key.CodigoGrupo + "-" + g.Key.CodigoNivel1 +"-" +  g.Key.CodigoNivel2 + "-"+g.Key.NumeroPlaca,
                                          Articulo = g.Key.Articulo,
                                          Especificacion = g.Key.Especificacion,   
                                          Servicio = g.Key.Servicio,
                                          ResponsableBien = g.Key.ResponsableBien,
                                          CodigoBien = g.Key.CodigoBien,
                                          CodigoMovBien = g.Key.CodigoMovBien,
                                          FechaMovimiento=g.Key.FechaMovimiento

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
                  
                    
                        
                        
                    response.Data = lista
                        .OrderBy(x=>x.CodigoBien)
                        .ThenBy(x=> x.CodigoGrupo)
                        .ThenBy(x=>x.CodigoNivel1)
                        .ThenBy(x=>x.CodigoNivel2)
                        .ThenBy(x=>x.ConsecutivoPlaca).ToList();
                    
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

        
      public async Task<ResultDto<List<Bm1GetDto>>> GetByListIcp(List<ICPGetDto> listIcpSeleccionado)
    {
       
       
        ResultDto<List<Bm1GetDto>> response = new ResultDto<List<Bm1GetDto>>(null);
        try
        {

          
            
            listIcpSeleccionado = listIcpSeleccionado.Where(x => x.CodigoIcp > 0).ToList();
            List<Bm1GetDto> searchList = new List<Bm1GetDto>();
            if (listIcpSeleccionado.Count > 0 )
            {
                foreach (var item in listIcpSeleccionado)
                {

                    var itemFilter = await GetAllByIcp(item.CodigoIcp);
                
                    if (itemFilter.Data.Count > 0)
                    {
                      
                        searchList.AddRange(itemFilter.Data);
                        
                    }
                }
            }
            else
            {
                var allData=await GetAll();
                searchList = allData.Data;

            }

            await CreateBardCodeMultiple(searchList);
            var fileName = $"BM1.xlsx";
            response.Data = searchList;
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


      public async Task<ResultDto<List<ICPGetDto>>> GetICP()
        {
           
           
            ResultDto<List<ICPGetDto>> response = new ResultDto<List<ICPGetDto>>(null);
            try
            {

              
                
                var result =  _repository.GetICP();
          
                   /* var lista = from s in result
                                  group s by new
                                  {
                                      CodigoIcp=s.CODIGO_ICP,
                                      UnidadTrabajo = s.UNIDAD_TRABAJO,
                                    
                 
                                      
                                  } into g
                                  select new ICPGetDto()
                                  {

                                        CodigoIcp = g.Key.CodigoIcp,
                                        UnidadTrabajo = g.Key.UnidadTrabajo,
                                   

                                  };*/
                    
            

               
                  
                    
                    
                response.Data = result.OrderBy(x=>x.CodigoIcp).ToList();
                response.IsValid = true;
                response.Message = "";
                response.LinkData= $"";
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

      public async Task<List<Bm1GetDto>> GetByPlaca(int codigoBien)
        {
            try
            {



                var result = await _repository.GetByPlaca(codigoBien);
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
                                CodigoMovBien = s.CODIGO_MOV_BIEN,
                                FechaMovimiento = s.FECHA_MOVIMIENTO

                            } into g
                            select new Bm1GetDto()
                            {


                                UnidadTrabajo = g.Key.UnidadTrabajo,
                                CodigoGrupo = g.Key.CodigoGrupo,
                                CodigoNivel1 = g.Key.CodigoNivel1,
                                CodigoNivel2 = g.Key.CodigoNivel2,
                                NumeroLote = g.Key.NumeroLote,
                                Cantidad = g.Key.Cantidad,
                                NumeroPlaca = g.Key.CodigoGrupo + "-" + g.Key.CodigoNivel1 + "-" + g.Key.CodigoNivel2 + "-" + g.Key.NumeroPlaca,
                                Articulo = g.Key.Articulo,
                                Especificacion = g.Key.Especificacion,
                                Servicio = g.Key.Servicio,
                                ResponsableBien = g.Key.ResponsableBien,
                                CodigoBien = g.Key.CodigoBien,
                                CodigoMovBien = g.Key.CodigoMovBien,
                                FechaMovimiento = g.Key.FechaMovimiento
                            };
                return lista.ToList();

            }
            catch (Exception ex)
            {
                var msg = ex.InnerException.Message;
                return null;
            }



        }

       protected async void GenerateMultiple(List<Bm1GetDto> placas,string dest)
        {
            // 2.5 * 72 = 180 5 * 72= 432
            Rectangle pageSize = new Rectangle(170, 85);
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(dest));

            Document doc = new Document(
                                            pdfDoc,
                                            new PageSize(pageSize)

                                        );
            doc.SetMargins(0, 0, 0, 0);

            //String code = "675-FH-A12";

            var intNumeroCopias = 2;
            for (int i = 1; i <= intNumeroCopias; i++)
            { 
                pdfDoc.AddNewPage();
            }

            foreach (var item in placas)
            {
                
                Table table = new Table(UnitValue.CreatePercentArray(1)).UseAllAvailableWidth();
                var _env = "development";
                var settings = _configuration.GetSection("Settings").Get<Settings>();

                var pathLogo = @settings.BmFiles;
                Image logo1 = new Image(ImageDataFactory.Create(pathLogo + ("EscudoChacao.png")));
                Image logo2 = new Image(ImageDataFactory.Create(pathLogo + ("LogoIzquierda.jpeg")));
                Paragraph logos = new Paragraph();
                logo1.ScaleAbsolute(20f, 13f).SetTextAlignment(TextAlignment.LEFT).SetMarginRight(50);
                logo2.ScaleAbsolute(20f, 12f).SetTextAlignment(TextAlignment.RIGHT).SetMarginLeft(76);
                logos.Add(logo1).SetWidth(30).SetVerticalAlignment(VerticalAlignment.TOP);
                logos.Add(logo2).SetWidth(30).SetVerticalAlignment(VerticalAlignment.TOP);
                Cell cell = new Cell(1,2);
                cell.SetBorder(null);
                cell.Add(logos);
                //cell.SetFixedPosition(5f, 30, 100f);
                //cell.SetVerticalAlignment(VerticalAlignment.TOP);
                //cell.Add(logo1.SetWidth(20).SetHorizontalAlignment(HorizontalAlignment.LEFT)
                //              .SetTextAlignment(TextAlignment.LEFT)).SetPaddingLeft(5);

                //cell.Add(logo2.SetWidth(20).SetHorizontalAlignment(HorizontalAlignment.RIGHT)
                //              .SetTextAlignment(TextAlignment.RIGHT)).SetPaddingRight(5).SetPaddingTop(0);

                table.AddCell(cell);

                Cell cell0 = new Cell();
                cell0.SetBorder(null);
                var fecha = $"{item.FechaMovimiento.Day.ToString()}/{item.FechaMovimiento.Month.ToString()}/{item.FechaMovimiento.Year.ToString()}";
                Paragraph fechaString = new Paragraph(fecha);
                //fechaString.SetTextAlignment(TextAlignment.CENTER);
                //fechaString.SetFixedPosition(30f, 50f,30f);
                cell0.SetTextAlignment(TextAlignment.CENTER);
                cell0.Add(fechaString).SetFontSize(5).SetPaddingTop(5).SetVerticalAlignment(VerticalAlignment.BOTTOM);
                //cell0.Add(fechaString).SetFontSize(5);
                table.AddCell(cell0);


                Barcode128 code128 = new Barcode128(pdfDoc);

                // If value is positive, the text distance under the bars. If zero or negative,
                // the text distance above the bars.
                code128.SetBaseline(10);
                code128.SetSize(12);
                code128.SetCode(item.NumeroPlaca);
                code128.SetCodeType(Barcode128.CODE128);
                Image code128Image = new Image(code128.CreateFormXObject(pdfDoc));
                code128Image.SetWidth(100);
                code128Image.SetHeight(20);
                // Notice that in iText5 in default PdfPCell constructor (new PdfPCell(Image img))
                // this image does not fit the cell, but it does in addCell().
                // In iText7 there is no constructor (new Cell(Image img)),
                // so the image adding to the cell can be done only using method add().



                Cell cell1 = new Cell(2,1);
                cell1.SetBorder(null);
                cell1.SetHorizontalAlignment(HorizontalAlignment.CENTER)
                                                       .SetTextAlignment(TextAlignment.CENTER);
                Paragraph texto = new Paragraph();
                texto.Add("Bienes Municipales");
                cell1.Add(texto).SetFontSize(7).SetBold();
                cell1.Add(code128Image.SetHorizontalAlignment(HorizontalAlignment.CENTER))
                                                      .SetVerticalAlignment(VerticalAlignment.MIDDLE);

                table.AddCell(cell1);

                Paragraph texto2 = new Paragraph("Consejo Municipal de Chacao");
                Paragraph texto3 = new Paragraph(item.UnidadTrabajo);

                Cell cell2 = new Cell(2,1);
                cell2.SetBorder(null);
                cell2.Add(texto2).SetFontSize(5).SetHorizontalAlignment(HorizontalAlignment.CENTER)
                                                .SetTextAlignment(TextAlignment.CENTER);
                cell2.Add(texto3).SetFontSize(5).SetHorizontalAlignment(HorizontalAlignment.CENTER)
                                                .SetTextAlignment(TextAlignment.CENTER);
                table.AddCell(cell2);

                doc.Add(table);

                
            }

           
            

            doc.Close();
        }

        public async Task CreateBardCodeMultiple(List<Bm1GetDto> bienes)
        {


            try
            {
                var _env = "development";
                var settings = _configuration.GetSection("Settings").Get<Settings>();

                var destino = @settings.ExcelFiles;
                FileInfo file = new FileInfo(destino);
                file.Directory.Create();
                destino = $"{destino}/placas.pdf";

                //var bienes = await _bM_V_BM1Service.GetAll();
                
                var listaBienes = bienes.OrderBy(b => b.UnidadTrabajo).ToList();
                GenerateMultiple(listaBienes, destino);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }


        }



    }
}

