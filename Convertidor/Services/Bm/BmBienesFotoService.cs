using System.Net;
using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Dtos.Bm;
using ImageMagick;
using iText.Barcodes;
using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;
using TextAlignment = iText.Layout.Properties.TextAlignment;


namespace Convertidor.Services.Bm
{
    public class BmBienesFotoService : IBmBienesFotoService
    {


        private readonly IBmBienesFotoRepository _repository;
        private readonly IBmBienesRepository _bienesRepository;
        private readonly IBM_V_BM1Service _bM_V_BM1Service;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IConfiguration _configuration;
        public BmBienesFotoService(IBmBienesFotoRepository repository,
                                    IBmBienesRepository bienesRepository,
                                    IBM_V_BM1Service bM_V_BM1Service,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      IConfiguration configuration)
        {
            _repository = repository;
            _bienesRepository = bienesRepository;
            _bM_V_BM1Service = bM_V_BM1Service;
            _sisUsuarioRepository = sisUsuarioRepository;
            _configuration = configuration;


        }

      
        public async Task<BmBienesFotoResponseDto> MapBmBienesFoto(BM_BIENES_FOTO dtos)
        {


            BmBienesFotoResponseDto itemResult = new BmBienesFotoResponseDto();
            itemResult.CodigoBienFoto = dtos.CODIGO_BIEN_FOTO;
            itemResult.CodigoBien = dtos.CODIGO_BIEN;
            itemResult.NumeroPlaca = dtos.NUMERO_PLACA;
            itemResult.Foto = dtos.FOTO;
            if (dtos.TITULO == null) dtos.TITULO = "";
            itemResult.Titulo = dtos.TITULO;
            itemResult.Patch = $"/BmFiles/{itemResult.NumeroPlaca}/{dtos.FOTO}";



            return itemResult;

        }
        public async Task<List<BmBienesFotoResponseDto>> MapListBienesFotoDto(List<BM_BIENES_FOTO> dtos)
        {
            List<BmBienesFotoResponseDto> result = new List<BmBienesFotoResponseDto>();


            foreach (var item in dtos)
            {

                BmBienesFotoResponseDto itemResult = new BmBienesFotoResponseDto();

                itemResult = await MapBmBienesFoto(item);

                result.Add(itemResult);
            }
            return result;



        }

        public async Task<ResultDto<List<BmBienesFotoResponseDto>>> GetByNumeroPlaca(string numeroPlaca)
        {

            ResultDto<List<BmBienesFotoResponseDto>> result = new ResultDto<List<BmBienesFotoResponseDto>>(null);
            try
            {

                var bienesFoto = await _repository.GetByNumeroPlaca(numeroPlaca);



                if (bienesFoto.Count() > 0)
                {


                    var listDto = await MapListBienesFotoDto(bienesFoto);


                    result.Data = listDto;

                    result.IsValid = true;
                    result.Message = "";
                }
                else
                {
                    result.Data = null;
                    result.IsValid = true;
                    result.Message = " No existen Datos";

                }
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }

        public async Task<ResultDto<BmBienesFotoResponseDto>> Update(BmBienesFotoUpdateDto dto)
        {

            ResultDto<BmBienesFotoResponseDto> result = new ResultDto<BmBienesFotoResponseDto>(null);
            try
            {
                var codigoBienFoto = await _repository.GetByCodigo(dto.CodigoBienFoto);
                if (codigoBienFoto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo bien invalido";
                    return result;
                }
                var codigoBien = await _bienesRepository.GetByCodigoArticulo(dto.CodigoBien);
                if (codigoBien == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo bien invalido";
                    return result;
                }



                codigoBienFoto.CODIGO_BIEN_FOTO = dto.CodigoBienFoto;
                codigoBienFoto.CODIGO_BIEN = dto.CodigoBien;
                codigoBienFoto.NUMERO_PLACA = dto.NumeroPlaca;
                codigoBienFoto.FOTO = dto.Foto;
                codigoBienFoto.TITULO = dto.Titulo;


                var conectado = await _sisUsuarioRepository.GetConectado();
                codigoBienFoto.CODIGO_EMPRESA = conectado.Empresa;
                codigoBienFoto.USUARIO_UPD = conectado.Usuario;
                codigoBienFoto.FECHA_UPD = DateTime.Now;

                await _repository.Update(codigoBienFoto);

                var resultDto = await MapBmBienesFoto(codigoBienFoto);
                result.Data = resultDto;
                result.IsValid = true;
                result.Message = "";

            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<ResultDto<BmBienesFotoResponseDto>> Create(BmBienesFotoUpdateDto dto)
        {

            ResultDto<BmBienesFotoResponseDto> result = new ResultDto<BmBienesFotoResponseDto>(null);
            try
            {

                var codigoBien = await _bienesRepository.GetByCodigoBien(dto.CodigoBien);
                if (codigoBien == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo no existe";
                    return result;
                }


                var numeroPlaca = await _bienesRepository.GetByNumeroPlaca(dto.NumeroPlaca);
                if (numeroPlaca == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero placa Invalido";
                    return result;
                }



                BM_BIENES_FOTO entity = new BM_BIENES_FOTO();
                entity.CODIGO_BIEN_FOTO = await _repository.GetNextKey();
                entity.CODIGO_BIEN = dto.CodigoBien;
                entity.NUMERO_PLACA = dto.NumeroPlaca;
                entity.FOTO = dto.Foto;
                entity.TITULO = dto.Titulo;


                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapBmBienesFoto(created.Data);
                    result.Data = resultDto;
                    result.IsValid = true;
                    result.Message = "";


                }
                else
                {

                    result.Data = null;
                    result.IsValid = created.IsValid;
                    result.Message = created.Message;
                }

                return result;



            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }

        public async Task<ResultDto<BmBienesFotoDeleteDto>> Delete(BmBienesFotoDeleteDto dto)
        {

            
            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var destino = @settings.BmFiles;
            
            ResultDto<BmBienesFotoDeleteDto> result = new ResultDto<BmBienesFotoDeleteDto>(null);
            try
            {

                var codigoBien = await _repository.GetByCodigo(dto.CodigoBienFoto);
                if (codigoBien == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Bien Foto no existe";
                    return result;
                }

                if (File.Exists($"{destino}{codigoBien.NUMERO_PLACA}/{codigoBien.FOTO}"))
                {
                    File.Delete($"{destino}{codigoBien.NUMERO_PLACA}/{codigoBien.FOTO}");
                }
                var deleted = await _repository.Delete(dto.CodigoBienFoto);

                if (deleted.Length > 0)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = deleted;
                }
                else
                {
                    result.Data = dto;
                    result.IsValid = true;
                    result.Message = deleted;

                }


            }
            catch (Exception ex)
            {
                result.Data = dto;
                result.IsValid = false;
                result.Message = ex.Message;
            }



            return result;
        }
        public string[] GetFicheros(string ruta)
        {

            string[] ficheros = Directory.GetFiles(ruta);
            string[] sorted = ficheros.OrderByDescending(o => o).ToArray();
            return sorted;
        }
        public static bool IsBase64(string base64String)
        {
            // Credit: oybek https://stackoverflow.com/users/794764/oybek
            if (string.IsNullOrEmpty(base64String) || base64String.Length % 4 != 0
                                                   || base64String.Contains(" ") || base64String.Contains("\t") || base64String.Contains("\r") || base64String.Contains("\n"))
                return false;

            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch (Exception exception)
            {
                // Handle the exception
            }
            return false;
        }

        public async Task<ResultDto<string>> AddOneImage(int codigoBien, IFormFile files)
        {
            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var destino = @settings.BmFiles;
            var numeroPlaca = "";
            var titulo = "";




            ResultDto<string> result = new ResultDto<string>(null);
            var numeroPlacaObj = await _bienesRepository.GetByCodigoBien(codigoBien);
            if (numeroPlacaObj == null)
            {
                result.Data = "";
                result.IsValid = false;
                result.Message = "Numero placa Invalido";
                return result;
            }
            else
            {
                numeroPlaca = numeroPlacaObj.NUMERO_PLACA;
            }
            try
            {


          
                        string fileName = files.FileName;
                        fileName = files.FileName.Replace(" ", "_");


                        if (fileName.Length > 13)
                        {

                            var longitud = files.FileName.Length - 14;
                            titulo = fileName.Substring(14, longitud);

                        }

                        if (!Directory.Exists($"{destino}{numeroPlaca}"))
                        {
                            Directory.CreateDirectory($"{destino}{numeroPlaca}");
                        }
                        var filePatch = $"{destino}{numeroPlaca}/{fileName}";
                        
                        // Eliminar el archivo si ya existe
                        if (File.Exists(filePatch))
                        {
                            File.Delete(filePatch);
                        }
                        
                        if (!File.Exists($"{filePatch}"))
                        {
                            using (var stream = System.IO.File.Create(filePatch))
                            {
                                await files.CopyToAsync(stream);
                            }
                        }
                        if (File.Exists($"{filePatch}"))
                        {
                            BmBienesFotoUpdateDto dtoCreate = new BmBienesFotoUpdateDto();
                            dtoCreate.CodigoBienFoto = 0;
                            dtoCreate.CodigoBien = numeroPlacaObj.CODIGO_BIEN;
                            dtoCreate.NumeroPlaca = numeroPlacaObj.NUMERO_PLACA;
                            dtoCreate.Foto = fileName;
                            dtoCreate.Titulo = titulo;
                            var created = await Create(dtoCreate);

                            MinFile(numeroPlacaObj.NUMERO_PLACA, fileName);

                        }
                    
              
                        result.Data = files.FileName;
                        result.IsValid = true;
                        result.Message = "";
                        return result;


            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }
            
            
           

            return result;
        }
        public async Task<ResultDto<List<BmBienesFotoResponseDto>>> AddImage(int codigoBien, List<IFormFile> files)
        {

            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var destino = @settings.BmFiles;
            var numeroPlaca = "";
            var titulo = "";




            ResultDto<List<BmBienesFotoResponseDto>> result = new ResultDto<List<BmBienesFotoResponseDto>>(null);

            var numeroPlacaObj = await _bienesRepository.GetByCodigoBien(codigoBien);
            if (numeroPlacaObj == null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "Numero placa Invalido";
                return result;
            }
            else
            {
                numeroPlaca = numeroPlacaObj.NUMERO_PLACA;
            }


            try
            {


                if (files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        string fileName = file.FileName;
                        fileName = file.FileName.Replace(" ", "_");
                      

                        if (fileName.Length > 13)
                        {

                            var longitud = file.FileName.Length - 14;
                            titulo = fileName.Substring(14, longitud);

                        }

                        if (!Directory.Exists($"{destino}{numeroPlaca}"))
                        {
                            Directory.CreateDirectory($"{destino}{numeroPlaca}");
                        }
                        var filePatch = $"{destino}{numeroPlaca}/{fileName}";
                        if (File.Exists($"{filePatch}"))
                        {
                            File.Delete($"{filePatch}");
                        }
                        if (!File.Exists($"{filePatch}"))
                        {
                            using (var stream = System.IO.File.Create(filePatch))
                            {
                                await file.CopyToAsync(stream);
                            }
                        }
                        if (File.Exists($"{filePatch}"))
                        {
                            BmBienesFotoUpdateDto dtoCreate = new BmBienesFotoUpdateDto();
                            dtoCreate.CodigoBienFoto = 0;
                            dtoCreate.CodigoBien = numeroPlacaObj.CODIGO_BIEN;
                            dtoCreate.NumeroPlaca = numeroPlacaObj.NUMERO_PLACA;
                            dtoCreate.Foto = fileName;
                            dtoCreate.Titulo = titulo;
                            var created = await Create(dtoCreate);

                            MinFile(numeroPlacaObj.NUMERO_PLACA, fileName);

                        }
                    }
                }

                result = await GetByNumeroPlaca(numeroPlaca);


                return result;



            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }

            return result;
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

        public async Task<ResultDto<List<BmBienesFotoResponseDto>>> AddImageModel(BmBienesimageUpdateDto dto)
        {
            var numeroPlaca = "2-01-00-00315";

            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var destino = @settings.BmFiles;
            if (!Directory.Exists($"{destino}{numeroPlaca}"))
            {
                Directory.CreateDirectory($"{destino}{numeroPlaca}");
            }
            ResultDto<List<BmBienesFotoResponseDto>> result = new ResultDto<List<BmBienesFotoResponseDto>>(null);
            try
            {
                var numeroPlacaObj = await _bienesRepository.GetByNumeroPlaca(numeroPlaca);
                if (numeroPlacaObj == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero placa Invalido";
                    return result;
                }
                if (dto.Files.Count > 0)
                {
                    foreach (var file in dto.Files)
                    {
                        var filePatch = $"{destino}{numeroPlaca}/{file.FileName}";
                        using (var stream = System.IO.File.Create(filePatch))
                        {
                            await file.CopyToAsync(stream);
                        }
                        if (File.Exists($"{filePatch}"))
                        {
                            BmBienesFotoUpdateDto dtoCreate = new BmBienesFotoUpdateDto();
                            dtoCreate.CodigoBienFoto = 0;
                            dtoCreate.CodigoBien = numeroPlacaObj.CODIGO_BIEN;
                            dtoCreate.NumeroPlaca = numeroPlaca;
                            dtoCreate.Foto = file.FileName;
                            dtoCreate.Titulo = file.FileName;
                            var created = await Create(dtoCreate);

                        }
                    }
                }

                result = await GetByNumeroPlaca(numeroPlaca);


                return result;



            }
            catch (Exception ex)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<string> CopiarArchivos()
        {

            string text1 = "";
            try
            {

                string outFileName = @"";

                var _env = "development";
                var settings = _configuration.GetSection("Settings").Get<Settings>();
                var destino = @settings.BmFiles;
                var origen = @settings.BmFilesProceso;

                var ficheros = GetFicheros(origen);
                foreach (string file in ficheros)
                {
                    var srcFileArr = file.Split("/");
                    if (_env == "producction")
                    {
                        srcFileArr = file.Split("\\");
                    }
                    var fileName = srcFileArr[srcFileArr.Length - 1];
                    var controlArray = fileName.Split("_");
                    var controlFinalArray = controlArray[controlArray.Length - 1].Split(".");
                    var control = controlFinalArray[0];
                    var numeroPlaca = "";
                    var titulo = "";

                    fileName = fileName.Replace(" ", "_");
                    if (control.Length >= 13)
                    {
                        numeroPlaca = control.Substring(0, 13);

                    }
                    if (control.Length > 13)
                    {
                        titulo = "";
                        var longitud = control.Length - 14;
                        titulo = control.Substring(14, longitud);

                    }


                    var bmBien = await _bienesRepository.GetByNumeroPlaca(numeroPlaca);
                    if (bmBien != null)
                    {
                        if (!Directory.Exists($"{destino}{numeroPlaca}"))
                        {
                            Directory.CreateDirectory($"{destino}{numeroPlaca}");
                        }

                        outFileName = $"{destino}{numeroPlaca}{fileName}";
                        if (!File.Exists($"{destino}{numeroPlaca}/{fileName}"))
                        {

                            File.Copy(file, $"{destino}{numeroPlaca}/{fileName}");
                            if (File.Exists($"{destino}{numeroPlaca}/{fileName}"))
                            {
                                BmBienesFotoUpdateDto dto = new BmBienesFotoUpdateDto();
                                dto.CodigoBienFoto = 0;
                                dto.CodigoBien = bmBien.CODIGO_BIEN;
                                dto.NumeroPlaca = numeroPlaca;
                                dto.Foto = fileName;
                                dto.Titulo = titulo;
                                await Create(dto);

                            }
                        }

                    }


                }



                return text1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return text1;
            }


        }



        protected void ManipulateEjemplo(String dest, String code, string unidadEjecutora, DateTime fecha)
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


            Table table = new Table(UnitValue.CreatePercentArray(1)).UseAllAvailableWidth();
            var _env = "development";
            var settings = _configuration.GetSection("Settings").Get<Settings>();

            var pathLogo = @settings.BmFiles;
            Image logo1 = new Image(ImageDataFactory.Create(pathLogo + ("LogoIzquierda.jpeg")));
            Image logo2 = new Image(ImageDataFactory.Create(pathLogo + ("LogoDerecha.jpeg")));
            Cell cell = new Cell();
            cell.SetBorder(null);
            cell.Add(logo1.SetWidth(20).SetFixedPosition(5f, 73f));

            cell.Add(logo2.SetWidth(20).SetHorizontalAlignment(HorizontalAlignment.RIGHT)
                          .SetTextAlignment(TextAlignment.RIGHT));

            table.AddCell(cell);

            Cell cell0 = new Cell();
            cell0.SetBorder(null);
            Paragraph fechaString = new Paragraph();
            fechaString.SetTextAlignment(TextAlignment.CENTER);
            fechaString.Add(fecha.ToShortDateString());
            cell0.Add(fechaString).SetFontSize(5);
            table.AddCell(cell0);

            Barcode128 code128 = new Barcode128(pdfDoc);

            // If value is positive, the text distance under the bars. If zero or negative,
            // the text distance above the bars.
            code128.SetBaseline(10);
            code128.SetSize(12);
            code128.SetCode(code);
            code128.SetCodeType(Barcode128.CODE128);
            Image code128Image = new Image(code128.CreateFormXObject(pdfDoc));
            code128Image.SetWidth(100);
            code128Image.SetHeight(20);
            // Notice that in iText5 in default PdfPCell constructor (new PdfPCell(Image img))
            // this image does not fit the cell, but it does in addCell().
            // In iText7 there is no constructor (new Cell(Image img)),
            // so the image adding to the cell can be done only using method add().



            Cell cell1 = new Cell();
            cell1.SetBorder(null);
            cell1.SetHorizontalAlignment(HorizontalAlignment.CENTER)
                                                   .SetTextAlignment(TextAlignment.CENTER);
            Paragraph texto = new Paragraph();
            texto.Add("Bienes Municipales");
            cell1.Add(texto).SetFontSize(6).SetBold();
            cell1.Add(code128Image.SetHorizontalAlignment(HorizontalAlignment.CENTER));

            table.AddCell(cell1);

            Paragraph texto2 = new Paragraph();
            Paragraph texto3 = new Paragraph();

            Cell cell2 = new Cell();
            cell2.SetBorder(null);
            cell2.SetHorizontalAlignment(HorizontalAlignment.CENTER);
            cell2.SetTextAlignment(TextAlignment.CENTER);
            texto2.Add("Consejo Municipal de Chacao");
            texto2.SetFontSize(5);
            texto3.Add(unidadEjecutora);
            texto3.SetFontSize(5);
            cell2.Add(texto2);
            cell2.Add(texto3);
            table.AddCell(cell2);




            /*table.AddCell("Add text and bar code separately:");

            code128 = new Barcode128(pdfDoc);
            
            // Suppress the barcode text
            code128.SetFont(null);
            code128.SetCode(code);
            code128.SetCodeType(Barcode128.CODE128);

            // Let the image resize automatically by setting it to be autoscalable.
            code128Image = new Image(code128.CreateFormXObject(pdfDoc)).SetAutoScale(true);
            cell = new Cell();
            cell.Add(new Paragraph("PO #: " + code));
            cell.Add(code128Image);
            table.AddCell(cell);*/

            //image1.ScalePercent(50f);

            doc.Add(table);

            doc.Close();
        }


        public async Task CreateBardCode()
        {


            try
            {
                var _env = "development";
                var settings = _configuration.GetSection("Settings").Get<Settings>();


                var bienes = await _bM_V_BM1Service.GetByPlaca(3145);
                foreach (var item in bienes)
                {
                    var destino = @settings.BmFiles;
                    FileInfo file = new FileInfo(destino);
                    file.Directory.Create();
                    destino = $"{destino}{item.NumeroPlaca}.pdf";
                    ManipulateEjemplo(destino, item.NumeroPlaca, item.UnidadTrabajo, item.FechaMovimiento);

                    //ManipulatePdf(destino, item.NUMERO_PLACA);


                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }


        }


    }
}