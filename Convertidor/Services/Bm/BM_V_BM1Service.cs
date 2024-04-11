using Convertidor.Data.Interfaces.Bm;
using Convertidor.Dtos.Bm;
using Ganss.Excel;
using iText.Barcodes;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

using Path = System.IO.Path;

namespace Convertidor.Services.Bm
{
    public class BM_V_BM1Service : IBM_V_BM1Service
    {


        private readonly IBM_V_BM1Repository _repository;
        private readonly IConfiguration _configuration;
        private readonly IBmMovBienesRepository _bmMovBienesRepository;


        public BM_V_BM1Service(IBM_V_BM1Repository repository,
                                IConfiguration configuration,
                                IBmMovBienesRepository bmMovBienesRepository
                                )

        {
            _repository = repository;
            _configuration = configuration;
            _bmMovBienesRepository = bmMovBienesRepository;
        }



        public async Task<ResultDto<List<Bm1GetDto>>> GetAll(DateTime? desde,DateTime? hasta)
        {


            ResultDto<List<Bm1GetDto>> response = new ResultDto<List<Bm1GetDto>>(null);
            try
            {

                //var bienesFoto =  _bmBienesFotoRepository.BienesConFoto();

                var result = await _repository.GetAll();
                List<Bm1GetDto> listaResult = new List<Bm1GetDto>();
                List<Bm1ExcelGetDto> listaResultExcel = new List<Bm1ExcelGetDto>();
                var lista = from s in result
                            group s by new
                            {
                                CodigoIcp = s.CODIGO_ICP,
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
                                FechaMovimiento = s.FECHA_MOVIMIENTO,
                                FechaMovimientoFiltro=s.FECHA_MOVIMIENTO

                            } into g
                            select new Bm1GetDto()
                            {

                                CodigoIcp = g.Key.CodigoIcp,
                                UnidadTrabajo = g.Key.UnidadTrabajo,
                                CodigoGrupo = g.Key.CodigoGrupo,
                                CodigoNivel1 = g.Key.CodigoNivel1,
                                CodigoNivel2 = g.Key.CodigoNivel2,
                                ConsecutivoPlaca = g.Key.NumeroPlaca,
                                NumeroLote = g.Key.NumeroLote,
                                Cantidad = g.Key.Cantidad,
                                NumeroPlaca = g.Key.CodigoGrupo + "-" + g.Key.CodigoNivel1 + "-" + g.Key.CodigoNivel2 + "-" + g.Key.NumeroPlaca,
                                Articulo = g.Key.Articulo,
                                Especificacion = g.Key.Especificacion,
                                Servicio = g.Key.Servicio,
                                ResponsableBien = g.Key.ResponsableBien,
                                CodigoBien = g.Key.CodigoBien,
                                CodigoMovBien = g.Key.CodigoMovBien,
                                FechaMovimiento = g.Key.FechaMovimiento,
                                FechaMovimientoFiltro= g.Key.FechaMovimientoFiltro
                               

                            };

                if (lista != null && lista.Count() > 0)
                {
                    foreach (var item in lista)
                    {
                        item.FechaMovimientoFiltro =
                            await _bmMovBienesRepository.GetByCodigoBienFecha(item.CodigoBien, (DateTime)hasta);
                        var fecha = (DateTime)item.FechaMovimientoFiltro;
                        item.Year = fecha.Year;
                        item.Month = fecha.Month;
                        
                        listaResult.Add(item);
                    }
                }
                
                listaResult = listaResult.Where(x => x.FechaMovimientoFiltro>=desde && x.FechaMovimientoFiltro <= hasta).OrderBy(x => x.CodigoGrupo)
                    .ThenBy(x => x.CodigoNivel1)
                    .ThenBy(x => x.CodigoNivel2)
                    .ThenBy(x => x.ConsecutivoPlaca).ToList();
                
                var listaExcel = from s in listaResult
                                 group s by new
                                 {
                                     UnidadTrabajo = s.UnidadTrabajo,
                                     CodigoGrupo = s.CodigoGrupo,
                                     CodigoNivel1 = s.CodigoNivel1,
                                     CodigoNivel2 = s.CodigoNivel2,
                                     NumeroLote = s.NumeroLote,
                                     Cantidad = s.Cantidad,
                                     NumeroPlaca = s.NumeroPlaca,
                                     Articulo = s.Articulo,
                                     Especificacion = s.Especificacion,
                                     Servicio = s.Servicio,
                                     ResponsableBien = s.ResponsableBien,
                                     CodigoBien = s.CodigoBien,
                                     CodigoMovBien = s.CodigoMovBien,
                                     FechaMovimiento = s.FechaMovimiento,
                                     FechaMovimientoFiltro=s.FechaMovimientoFiltro,
                                     Year=s.Year,
                                     Month=s.Month

                                 } into g
                                 select new Bm1ExcelGetDto()
                                 {


                                     UnidadTrabajo = g.Key.UnidadTrabajo,

                                     Cantidad = g.Key.Cantidad,
                                     NumeroPlaca = g.Key.CodigoGrupo + "-" + g.Key.CodigoNivel1 + "-" + g.Key.CodigoNivel2 + "-" + g.Key.NumeroPlaca,
                                     Articulo = g.Key.Articulo,
                                     CodigoBien = g.Key.CodigoBien,
                                     Especificacion = g.Key.Especificacion,
                                     Servicio = g.Key.Servicio,
                                     ResponsableBien = g.Key.ResponsableBien,
                                     FechaMovimiento = g.Key.FechaMovimiento,
                                     FechaMovimientoFiltro = (DateTime)g.Key.FechaMovimientoFiltro,
                                     Year = g.Key.Year,
                                     Month = g.Key.Month

                                 };
                /*if (listaExcel != null && listaExcel.Count() > 0)
                {
                    foreach (var item in listaExcel)
                    {
                        item.FechaMovimientoFiltro =
                            await _bmMovBienesRepository.GetByCodigoBienFecha(item.CodigoBien, (DateTime)hasta);
                        var fecha = (DateTime)item.FechaMovimientoFiltro;
                        item.Year = fecha.Year;
                        item.Month = fecha.Month;
                        listaResultExcel.Add(item);
                    }

                }*/
              
               
                ExcelMapper mapper = new ExcelMapper();


                var settings = _configuration.GetSection("Settings").Get<Settings>();


                var ruta = @settings.ExcelFiles;  //@"/Users/freveron/Documents/MM/App/full-version/public/ExcelFiles";
                var fileName = $"BM1.xlsx";
                string newFile = Path.Combine(Directory.GetCurrentDirectory(), ruta, fileName);

                var excelData =  listaExcel.ToList();
                mapper.Save(newFile, excelData, $"BM1", true);



                response.Data = listaResult;
                response.IsValid = true;
                response.Message = "";
                response.LinkData = $"/ExcelFiles/{fileName}";
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
        public async Task<ResultDto<List<Bm1GetDto>>> GetAllByIcp(int codigoIcp,DateTime? desde,DateTime? hasta)
        {


            ResultDto<List<Bm1GetDto>> response = new ResultDto<List<Bm1GetDto>>(null);
            try
            {

                //var bienesFoto =  _bmBienesFotoRepository.BienesConFoto();

                var result = await _repository.GetAllByCodigoIcp(codigoIcp);
                List<Bm1GetDto> listaResult = new List<Bm1GetDto>();
                List<Bm1ExcelGetDto> listaResultExcel = new List<Bm1ExcelGetDto>();
                var lista = from s in result
                            group s by new
                            {
                                CodigoIcp = s.CODIGO_ICP,
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

                                CodigoIcp = g.Key.CodigoIcp,
                                UnidadTrabajo = g.Key.UnidadTrabajo,
                                CodigoGrupo = g.Key.CodigoGrupo,
                                CodigoNivel1 = g.Key.CodigoNivel1,
                                CodigoNivel2 = g.Key.CodigoNivel2,
                                ConsecutivoPlaca = g.Key.NumeroPlaca,
                                NumeroLote = g.Key.NumeroLote,
                                Cantidad = g.Key.Cantidad,
                                NumeroPlaca = g.Key.CodigoGrupo + "-" + g.Key.CodigoNivel1 + "-" + g.Key.CodigoNivel2 + "-" + g.Key.NumeroPlaca,
                                Articulo = g.Key.Articulo,
                                Especificacion = g.Key.Especificacion,
                                Servicio = g.Key.Servicio,
                                ResponsableBien = g.Key.ResponsableBien,
                                CodigoBien = g.Key.CodigoBien,
                                CodigoMovBien = g.Key.CodigoMovBien,
                                FechaMovimiento = g.Key.FechaMovimiento,
                                Year = g.Key.FechaMovimiento.Year,
                                Month = g.Key.FechaMovimiento.Month,
                             

                            };

                if (lista != null && lista.Count() > 0)
                {
                    foreach (var item in lista)
                    {
                        item.FechaMovimientoFiltro =
                            await _bmMovBienesRepository.GetByCodigoBienFecha(item.CodigoBien, (DateTime)hasta);
                        var fecha = (DateTime)item.FechaMovimientoFiltro;
                        item.Year = fecha.Year;
                        item.Month = fecha.Month;
                        listaResult.Add(item);
                    }
                }
                listaResult = listaResult.Where(x => x.FechaMovimientoFiltro >= desde && x.FechaMovimientoFiltro <= hasta).OrderBy(x => x.CodigoGrupo)
                    .ThenBy(x => x.CodigoNivel1)
                    .ThenBy(x => x.CodigoNivel2)
                    .ThenBy(x => x.ConsecutivoPlaca).ToList();
                
                var listaExcel = from s in listaResult
                                 group s by new
                                 {
                                     UnidadTrabajo = s.UnidadTrabajo,
                                     CodigoGrupo = s.CodigoGrupo,
                                     CodigoNivel1 = s.CodigoNivel1,
                                     CodigoNivel2 = s.CodigoNivel2,
                                     NumeroLote = s.NumeroLote,
                                     Cantidad = s.Cantidad,
                                     NumeroPlaca = s.NumeroPlaca,
                                     Articulo = s.Articulo,
                                     Especificacion = s.Especificacion,
                                     Servicio = s.Servicio,
                                     ResponsableBien = s.ResponsableBien,
                                     CodigoBien = s.CodigoBien,
                                     CodigoMovBien = s.CodigoMovBien,
                                     FechaMovimiento = s.FechaMovimiento,
                                     FechaMovimientoFiltro=s.FechaMovimientoFiltro,
                                     Year=s.Year,
                                     Month=s.Month


                                 } into g
                                 select new Bm1ExcelGetDto()
                                 {


                                     UnidadTrabajo = g.Key.UnidadTrabajo,

                                     Cantidad = g.Key.Cantidad,
                                     NumeroPlaca = g.Key.CodigoGrupo + "-" + g.Key.CodigoNivel1 + "-" + g.Key.CodigoNivel2 + "-" + g.Key.NumeroPlaca,
                                     Articulo = g.Key.Articulo,
                                     Especificacion = g.Key.Especificacion,
                                     Servicio = g.Key.Servicio,
                                     ResponsableBien = g.Key.ResponsableBien,
                                     FechaMovimiento = g.Key.FechaMovimiento,
                                   
                                     CodigoBien=g.Key.CodigoBien,
                                     FechaMovimientoFiltro = (DateTime)g.Key.FechaMovimientoFiltro,
                                     Year = g.Key.Year,
                                     Month = g.Key.Month,


                                 };
                
               /* if (listaExcel != null && listaExcel.Count() > 0)
                {
                    foreach (var item in listaExcel)
                    {
                        item.FechaMovimientoFiltro =
                            await _bmMovBienesRepository.GetByCodigoBienFecha(item.CodigoBien, (DateTime)hasta);
                        var fecha = (DateTime)item.FechaMovimientoFiltro;
                        item.Year = fecha.Year;
                        item.Month = fecha.Month;
                        listaResultExcel.Add(item);
                    }
                }*/
              
                ExcelMapper mapper = new ExcelMapper();


                var settings = _configuration.GetSection("Settings").Get<Settings>();


                var ruta = @settings.ExcelFiles;  //@"/Users/freveron/Documents/MM/App/full-version/public/ExcelFiles";
                var fileName = $"BM1.xlsx";
                string newFile = Path.Combine(Directory.GetCurrentDirectory(), ruta, fileName);

                var excelData = listaExcel.ToList();
                mapper.Save(newFile, excelData, $"BM1", true);




                response.Data = listaResult.ToList();

                response.IsValid = true;
                response.Message = "";
                response.LinkData = $"/ExcelFiles/{fileName}";
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


        public async Task<ResultDto<List<Bm1GetDto>>> GetByListIcp(Bm1Filter filter)
        {

            var listIcpSeleccionado = filter.ListIcpSeleccionado;
            ResultDto<List<Bm1GetDto>> response = new ResultDto<List<Bm1GetDto>>(null);
            try
            {



                listIcpSeleccionado = listIcpSeleccionado.Where(x => x.CodigoIcp > 0).ToList();
                List<Bm1GetDto> searchList = new List<Bm1GetDto>();
                if (listIcpSeleccionado.Count > 0)
                {
                    foreach (var item in listIcpSeleccionado)
                    {

                        var itemFilter = await GetAllByIcp(item.CodigoIcp,filter.FechaDesde,filter.FechaHasta);

                        if (itemFilter.Data.Count > 0)
                        {

                            searchList.AddRange(itemFilter.Data);

                        }
                    }
                }
                else
                {
                    var allData = await GetAll(filter.FechaDesde,filter.FechaHasta);
                    searchList = allData.Data;

                }

                await CreateBardCodeMultiple(searchList);
                var fileName = $"BM1.xlsx";
                response.Data = searchList;
                response.IsValid = true;
                response.Message = "";
                response.LinkData = $"/ExcelFiles/{fileName}";
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



                var result = _repository.GetICP();

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







                response.Data = result.OrderBy(x => x.CodigoIcp).ToList();
                response.IsValid = true;
                response.Message = "";
                response.LinkData = $"";
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

        protected async void GenerateMultipleBackup(List<Bm1GetDto> placas, string dest)
        {

            // 2.5 * 72 = 180 5 * 72= 432
            Rectangle pageSize = new Rectangle(170, 85);
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(dest));

            Document doc = new Document(
                                            pdfDoc,
                                            new PageSize(pageSize)

                                        );
            doc.SetMargins(1,5,3,5);
            
            //String code = "675-FH-A12";

            var intNumeroCopias = 2;
            for (int i = 1; i <= intNumeroCopias; i++)
            {
                pdfDoc.AddNewPage();
            }

            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            foreach (var item in placas)
            {

                Table _table = new Table(UnitValue.CreatePercentArray(1)).UseAllAvailableWidth();
                
                var _env = "development";
                var settings = _configuration.GetSection("Settings").Get<Settings>();

                var pathLogo = @settings.BmFiles;
                Image logo1 = new Image(ImageDataFactory.Create(pathLogo + ("Escudo de chacao.png")));
                Image logo2 = new Image(ImageDataFactory.Create(pathLogo + ("logo concejo.png")));
                var fecha = $"{item.FechaMovimiento.Day.ToString()}/{item.FechaMovimiento.Month.ToString()}/{item.FechaMovimiento.Year.ToString()}";
                
                Paragraph logos = new Paragraph();
                logo1.ScaleAbsolute(30f, 30f);
                logo2.ScaleAbsolute(45f, 30f);
                
                
                logos.Add(logo1).SetHorizontalAlignment(HorizontalAlignment.LEFT).SetMarginRight(20);

                logos.Add(fecha).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetTextAlignment(TextAlignment.CENTER)
                                .SetFontSize(8);

                logos.Add(logo2).SetHorizontalAlignment(HorizontalAlignment.RIGHT).SetMarginLeft(20);

                Cell cell0 = new Cell(1, 3);
                cell0.SetBorder(null);


                cell0.Add(logos);

                _table.AddHeaderCell(cell0);
 
                Paragraph texto = new Paragraph("Bienes Municipales").SetVerticalAlignment(VerticalAlignment.TOP).SetFontSize(8)
                                      .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                                      .SetTextAlignment(TextAlignment.CENTER);



                Barcode128 code128 = new Barcode128(pdfDoc);

                // If value is positive, the text distance under the bars. If zero or negative,
                // the text distance above the bars.
                code128.SetBaseline(10);
                code128.SetSize(12);
                code128.SetCode(item.NumeroPlaca);
                code128.SetCodeType(Barcode128.CODE128);
                Image code128Image = new Image(code128.CreateFormXObject(pdfDoc));
              
                code128Image.ScaleToFit(120, 20);
                // Notice that in iText5 in default PdfPCell constructor (new PdfPCell(Image img))
                // this image does not fit the cell, but it does in addCell().
                // In iText7 there is no constructor (new Cell(Image img)),
                // so the image adding to the cell can be done only using method add().


                texto.Add(code128Image).SetVerticalAlignment(VerticalAlignment.BOTTOM)
                    .SetTextAlignment(TextAlignment.CENTER);

                Cell cell1 = new Cell(2,1);
                //cell1.SetBorder(null);
                cell1.Add(texto);
                _table.AddCell(cell1);


                Paragraph texto2 = new Paragraph("Concejo Municipal de Chacao").SetFontSize(7).SetFont(font);
                Paragraph texto3 = new Paragraph(item.UnidadTrabajo).SetFontSize(7).SetFont(font);

                 Cell cell2 = new Cell(2, 1);

                cell2.SetBorder(null);
                cell2.Add(texto2).SetVerticalAlignment(VerticalAlignment.BOTTOM)
                                 .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                                 .SetTextAlignment(TextAlignment.CENTER);

                cell2.Add(texto3).SetVerticalAlignment(VerticalAlignment.BOTTOM)
                                 .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                                 .SetTextAlignment(TextAlignment.CENTER);

                _table.AddFooterCell(cell2);

                try
                {
                    doc.Add(_table);
                }
                catch (Exception ex)
                {
                    var msg = ex.InnerException.Message;
                    return;
                }
            }

            doc.Close();
        }

        protected async void GenerateMultiple(List<Bm1GetDto> placas, string dest)
        {
            // 2.5 * 72 = 180 5 * 72= 432
            Rectangle pageSize = new Rectangle(170, 85);
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(dest));

            Document doc = new Document(
                                            pdfDoc,
                                            new PageSize(pageSize)

                                        );
            doc.SetMargins(3, 0, 0, 0);

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
                var fecha = $"{item.FechaMovimiento.Day.ToString()}/{item.FechaMovimiento.Month.ToString()}/{item.FechaMovimiento.Year.ToString()}";
                Image logo2 = new Image(ImageDataFactory.Create(pathLogo + ("LogoIzquierda.jpeg")));
                
                
                Paragraph logos = new Paragraph();
                logo1.ScaleAbsolute(30f, 25f).SetTextAlignment(TextAlignment.LEFT).SetMarginRight(20);
                logo2.ScaleAbsolute(40f, 25f).SetTextAlignment(TextAlignment.RIGHT).SetMarginLeft(20);

                logos.SetPaddingBottom(0);

                logos.Add(logo1).SetHorizontalAlignment(HorizontalAlignment.LEFT);

                logos.Add(fecha).SetTextAlignment(TextAlignment.CENTER)
                                .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                                .SetFontSize(8);

                logos.Add(logo2).SetHorizontalAlignment(HorizontalAlignment.RIGHT).SetMarginLeft(10).SetMarginRight(30);

                Cell cell = new Cell(1, 3);
                cell.SetPaddingBottom(0);
                cell.SetBorder(null);
                cell.Add(logos);


                table.AddHeaderCell(cell);

                //Cell cell0 = new Cell();
                ////cell0.SetBorder(null);
                //var fecha = $"{item.FechaMovimiento.Day.ToString()}/{item.FechaMovimiento.Month.ToString()}/{item.FechaMovimiento.Year.ToString()}";
                //Paragraph fechaString = new Paragraph(fecha);

                //cell0.SetMarginBottom(0).SetMarginTop(0).SetVerticalAlignment(VerticalAlignment.TOP);
                //cell0.SetTextAlignment(TextAlignment.CENTER);
                //cell0.Add(fechaString).SetFontSize(6);

                //table.AddCell(cell0);


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



                Cell cell1 = new Cell(2, 1);
                cell1.SetBorder(null);
                cell1.SetHorizontalAlignment(HorizontalAlignment.CENTER).SetPaddingBottom(0)
                                                       .SetTextAlignment(TextAlignment.CENTER);
                Paragraph texto = new Paragraph();
                texto.Add("Bienes Municipales");
                cell1.Add(texto).SetFontSize(7).SetBold().SetPaddingTop(0);
                cell1.Add(code128Image.SetHorizontalAlignment(HorizontalAlignment.CENTER));

                table.AddCell(cell1);

                Paragraph texto2 = new Paragraph("Concejo Municipal de Chacao").SetFontSize(6);
                Paragraph texto3 = new Paragraph(item.UnidadTrabajo);

                Cell cell2 = new Cell(2, 1);
                cell2.SetBorder(null);
                cell2.Add(texto2).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetPaddingBottom(3)
                                                .SetTextAlignment(TextAlignment.CENTER);
                cell2.Add(texto3).SetFontSize(5).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetPaddingBottom(2).SetMarginBottom(1)
                                                .SetTextAlignment(TextAlignment.CENTER);
                table.AddFooterCell(cell2);

                doc.Add(table);


            }




            doc.Close();
        }

        protected async void GenerateMultipleFont(List<Bm1GetDto> placas, string dest)
        {
            // 2.5 * 72 = 180 5 * 72= 432
            Rectangle pageSize = new Rectangle(170, 85);
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(dest));

            Document doc = new Document(
                                            pdfDoc,
                                            new PageSize(pageSize)

                                        );

            var _env = "development";
            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var pathFont = $"{settings.BmFiles + ("arial.ttf")}";
            FontProgram fontProgram =
                    FontProgramFactory.CreateFont(pathFont);
            PdfFont font = PdfFontFactory.CreateFont(fontProgram, PdfEncodings.WINANSI,PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);

            doc.SetFont(font);

            doc.SetMargins(3, 0, 0, 0);
            
            //String code = "675-FH-A12";
            try
            {
                var intNumeroCopias = 2;
                for (int i = 1; i <= intNumeroCopias; i++)
                {
                    pdfDoc.AddNewPage();
                }


                foreach (var item in placas)
                {

                    Table table = new Table(UnitValue.CreatePercentArray(1)).UseAllAvailableWidth();
                    
                    //PdfFont  font = PdfFontFactory.CreateFont(pathFont, PdfEncodings.IDENTITY_H);

                    var pathLogo = @settings.BmFiles;
                    Image logo1 = new Image(ImageDataFactory.Create(pathLogo + ("EscudoChacao.png")));
                    var fecha = $"{item.FechaMovimiento.Day.ToString()}/{item.FechaMovimiento.Month.ToString()}/{item.FechaMovimiento.Year.ToString()}";
                    Image logo2 = new Image(ImageDataFactory.Create(pathLogo + ("LogoIzquierda.jpeg")));


                    Paragraph logos = new Paragraph();
                    logo1.ScaleAbsolute(30f, 25f).SetTextAlignment(TextAlignment.LEFT).SetMarginRight(20);
                    logo2.ScaleAbsolute(40f, 25f).SetTextAlignment(TextAlignment.RIGHT).SetMarginLeft(20);

                    logos.SetPaddingBottom(0);

                    logos.Add(logo1).SetHorizontalAlignment(HorizontalAlignment.LEFT);

                    logos.Add(fecha).SetTextAlignment(TextAlignment.CENTER)
                                    .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                                    .SetFontSize(8);

                    logos.Add(logo2).SetHorizontalAlignment(HorizontalAlignment.RIGHT).SetMarginLeft(10).SetMarginRight(30);

                    Cell cell = new Cell(1, 3);
                    cell.SetPaddingBottom(0);
                    cell.SetBorder(null);
                    cell.Add(logos);


                    table.AddHeaderCell(cell);

              

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



                    Cell cell1 = new Cell(2, 1);
                    cell1.SetBorder(null);
                    cell1.SetHorizontalAlignment(HorizontalAlignment.CENTER).SetPaddingBottom(0)
                                                           .SetTextAlignment(TextAlignment.CENTER);
                    Paragraph texto = new Paragraph();
                    texto.Add("Bienes Municipales");
                    cell1.Add(texto).SetFontSize(7).SetBold().SetPaddingTop(0);
                    cell1.Add(code128Image.SetHorizontalAlignment(HorizontalAlignment.CENTER));

                    table.AddCell(cell1);

                    Paragraph texto2 = new Paragraph("Concejo Municipal de Chacao").SetFontSize(6);
                    Paragraph texto3 = new Paragraph(item.UnidadTrabajo);

                    Cell cell2 = new Cell(2, 1);
                    cell2.SetBorder(null);
                    cell2.Add(texto2).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetPaddingBottom(3)
                                                    .SetTextAlignment(TextAlignment.CENTER);
                    cell2.Add(texto3).SetFontSize(5).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetPaddingBottom(2).SetMarginBottom(1)
                                                    .SetTextAlignment(TextAlignment.CENTER);
                    table.AddFooterCell(cell2);

                    doc.Add(table);


                }

                doc.Close();

            }
            

            catch(System.IO.IOException cause) 
            {
                throw new iText.IO.Exceptions.IOException("Character code exception.", cause);
            }

        }

        //protected async void GenerateMultipleQuest(List<Bm1GetDto> placas, string dest)
        //{

        //    var settings = _configuration.GetSection("Settings").Get<Settings>();
        //    var destino = @settings.ExcelFiles;
        //    var fileName = $"{destino}placas).pdf";
        //    var pathlogo = settings.BmFiles;
        //    try
        //    {


        //        var intNumeroCopias = 2;
        //        QuestPDF.Fluent.Document.Create(documento =>

        //            documento.Page(page =>
        //          {
        //              for (int i = 1; i <= intNumeroCopias; i++)
        //              {

        //                  {

        //                      foreach (var item in placas)
        //                      {
        //                          page.Size(170, 85);

        //                          page.Header().Row(fila =>
        //                             {
        //                                 fila.ConstantItem(60).Height(10).Width(10).Image(filePath: pathlogo + "Escudo de chacao.png")
        //                             .FitWidth().FitHeight();
        //                                 fila.RelativeItem().Border(0).Column(col =>
        //                             {
        //                                 col.Item().AlignCenter().Text($"{item.FechaMovimiento.ToString()}").Bold().FontSize(7);

        //                                 col.Item().AlignCenter().Text("Bienes Municipales").FontSize(9);

        //                             });

        //                                 fila.RelativeItem().Height(10).Width(10).Border(0).Image(filePath: pathlogo + "logo concejo.png");
        //                             });

        //                      }
        //                  }
        //              }
        //          })).GeneratePdf(fileName);
        //    }

        //    catch (Exception ex)
        //    {
        //        var message = ex.Message;
        //    }
        //}

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
                GenerateMultipleFont(listaBienes, destino);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }


        }



    }
}

