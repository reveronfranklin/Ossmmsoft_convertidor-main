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


namespace Convertidor.Services.Rh
{
    public class RhDocumentosAdjuntosService : IRhDocumentosAdjuntosService
    {


        private readonly IRhDocumentosAdjuntosRepository _repository;
        private readonly IRhDocumentosRepository _documentosRepository;
  
   
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IConfiguration _configuration;
        public RhDocumentosAdjuntosService(IRhDocumentosAdjuntosRepository repository,
                                    IRhDocumentosRepository documentosRepository,
                
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      IConfiguration configuration)
        {
            _repository = repository;
            _documentosRepository = documentosRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _configuration = configuration;


        }

      
        public async Task<RhDocumentosAdjuntosResponseDto> MapRhDocumentosAdjuntos(RH_DOCUMENTOS_ADJUNTOS dtos)
        {


            RhDocumentosAdjuntosResponseDto itemResult = new RhDocumentosAdjuntosResponseDto();
            itemResult.CodigoDocumentoAdjunto = dtos.CODIGO_DOCUMENTO_ADJUNTO;
            itemResult.CodigoDocumento = dtos.CODIGO_DOCUMENTO;
            itemResult.Adjunto = dtos.ADJUNTO;
            if (dtos.TITULO == null) dtos.TITULO = "";
            itemResult.Titulo = dtos.TITULO;
            itemResult.Patch = $"/RhFiles/{itemResult.CodigoDocumento}/{dtos.ADJUNTO}";



            return itemResult;

        }
        public async Task<List<RhDocumentosAdjuntosResponseDto>> MapListRhDocumentosAdjuntosDto(List<RH_DOCUMENTOS_ADJUNTOS> dtos)
        {
            List<RhDocumentosAdjuntosResponseDto> result = new List<RhDocumentosAdjuntosResponseDto>();


            foreach (var item in dtos)
            {

                RhDocumentosAdjuntosResponseDto itemResult = new RhDocumentosAdjuntosResponseDto();

                itemResult = await MapRhDocumentosAdjuntos(item);

                result.Add(itemResult);
            }
            return result;



        }

        public async Task<ResultDto<List<RhDocumentosAdjuntosResponseDto>>> GetByNumeroDocumento(int numeroDocumento)
        {

            ResultDto<List<RhDocumentosAdjuntosResponseDto>> result = new ResultDto<List<RhDocumentosAdjuntosResponseDto>>(null);
            try
            {

                var adjuntos = await _repository.GetByCodigoDocumento(numeroDocumento);



                if (adjuntos.Count() > 0)
                {


                    var listDto = await MapListRhDocumentosAdjuntosDto(adjuntos);


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

        public async Task<ResultDto<RhDocumentosAdjuntosResponseDto>> Update(RhDocumentosAdjuntosUpdateDto dto)
        {

            ResultDto<RhDocumentosAdjuntosResponseDto> result = new ResultDto<RhDocumentosAdjuntosResponseDto>(null);
            try
            {
                var adjunto = await _repository.GetByCodigo(dto.CodigoDocumentoAdjunto);
                if (adjunto == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo  adjunto invalido";
                    return result;
                }
                var cocumento = await _documentosRepository.GetByCodigo(dto.CodigoDocumento);
                if (cocumento == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo documento invalido";
                    return result;
                }



          
                adjunto.CODIGO_DOCUMENTO = dto.CodigoDocumento;
                adjunto.ADJUNTO = dto.Adjunto;
                adjunto.TITULO = dto.Titulo;


                var conectado = await _sisUsuarioRepository.GetConectado();
                adjunto.CODIGO_EMPRESA = conectado.Empresa;
                adjunto.USUARIO_UPD = conectado.Usuario;
                adjunto.FECHA_UPD = DateTime.Now;

                await _repository.Update(adjunto);

                var resultDto = await MapRhDocumentosAdjuntos(adjunto);
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

        public async Task<ResultDto<RhDocumentosAdjuntosResponseDto>> Create(RhDocumentosAdjuntosUpdateDto dto)
        {

            ResultDto<RhDocumentosAdjuntosResponseDto> result = new ResultDto<RhDocumentosAdjuntosResponseDto>(null);
            try
            {


                if (string.IsNullOrEmpty(dto.Adjunto))
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Nombre Adjunto invalido";
                    return result;
                }
                
                var cocumento = await _documentosRepository.GetByCodigo(dto.CodigoDocumento);
                if (cocumento == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Codigo documento invalido";
                    return result;
                }




                RH_DOCUMENTOS_ADJUNTOS entity = new RH_DOCUMENTOS_ADJUNTOS();
                entity.CODIGO_DOCUMENTO_ADJUNTO = await _repository.GetNextKey();
                entity.CODIGO_DOCUMENTO = dto.CodigoDocumento;
                entity.ADJUNTO = dto.Adjunto;
                entity.TITULO = dto.Titulo;


                var conectado = await _sisUsuarioRepository.GetConectado();
                entity.CODIGO_EMPRESA = conectado.Empresa;
                entity.USUARIO_INS = conectado.Usuario;
                entity.FECHA_INS = DateTime.Now;

                var created = await _repository.Add(entity);
                if (created.IsValid && created.Data != null)
                {
                    var resultDto = await MapRhDocumentosAdjuntos(created.Data);
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

        public async Task<ResultDto<RhDocumentosAdjuntosDeleteDto>> Delete(RhDocumentosAdjuntosDeleteDto dto)
        {

            
            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var destino = @settings.RhFiles;
            
            ResultDto<RhDocumentosAdjuntosDeleteDto> result = new ResultDto<RhDocumentosAdjuntosDeleteDto>(null);
            try
            {

                var documentoAdjunto = await _repository.GetByCodigo(dto.CodigoDocumentoAdjunto);
                if (documentoAdjunto == null)
                {
                    result.Data = dto;
                    result.IsValid = false;
                    result.Message = "Bien Foto no existe";
                    return result;
                }

                if (File.Exists($"{destino}{documentoAdjunto.CODIGO_DOCUMENTO}/{documentoAdjunto.ADJUNTO}"))
                {
                    File.Delete($"{destino}{documentoAdjunto.CODIGO_DOCUMENTO}/{documentoAdjunto.ADJUNTO}");
                }
                var deleted = await _repository.Delete(dto.CodigoDocumentoAdjunto);

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

        public async Task<ResultDto<string>> AddOneImage(int codigoDocumento, IFormFile files)
        {
            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var destino = @settings.RhFiles;
            var numeroDocumento = "";
            var titulo = "";




            ResultDto<string> result = new ResultDto<string>(null);
            var numeroDocumentoObj = await _documentosRepository.GetByCodigo(codigoDocumento);
            if (numeroDocumentoObj == null)
            {
                result.Data = "";
                result.IsValid = false;
                result.Message = "Numero placa Invalido";
                return result;
            }
            else
            {
                numeroDocumento = numeroDocumentoObj.CODIGO_DOCUMENTO.ToString();
            }
            try
            {


          
                        string fileName = files.FileName;
                        fileName = files.FileName.Replace(" ", "_");
                        var findPlacaFoto =
                            await _repository.GetByNumeroDocumentoAdjunto(codigoDocumento, fileName);
                        if (findPlacaFoto != null)
                        {
                            result.Data = "";
                            result.IsValid = false;
                            result.Message = $"Ya existe el adjunto: {files.FileName}";
                            return result;
                        }

                        if (fileName.Length > 13)
                        {

                            var longitud = files.FileName.Length - 14;
                            titulo = fileName.Substring(14, longitud);

                        }

                        if (!Directory.Exists($"{destino}{numeroDocumento}"))
                        {
                            Directory.CreateDirectory($"{destino}{numeroDocumento}");
                        }
                        var filePatch = $"{destino}{numeroDocumento}/{fileName}";
                        if (!File.Exists($"{filePatch}"))
                        {
                            using (var stream = System.IO.File.Create(filePatch))
                            {
                                await files.CopyToAsync(stream);
                            }
                        }
                        if (File.Exists($"{filePatch}"))
                        {
                            RhDocumentosAdjuntosUpdateDto dtoCreate = new RhDocumentosAdjuntosUpdateDto();
                            dtoCreate.CodigoDocumentoAdjunto = 0;
                            dtoCreate.CodigoDocumento = codigoDocumento;
                            dtoCreate.Adjunto = fileName;
                            dtoCreate.Titulo = titulo;
                            var created = await Create(dtoCreate);

                            MinFile(numeroDocumento, fileName);

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
        public async Task<ResultDto<List<RhDocumentosAdjuntosResponseDto>>> AddImage(int codigoDocumento, List<IFormFile> files)
        {

            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var destino = @settings.BmFiles;
            var numeroDocumento = "";
            var titulo = "";




            ResultDto<List<RhDocumentosAdjuntosResponseDto>> result = new ResultDto<List<RhDocumentosAdjuntosResponseDto>>(null);

            var documento = await _documentosRepository.GetByCodigo(codigoDocumento);
            if (documento == null)
            {
                result.Data = null;
                result.IsValid = false;
                result.Message = "Numero placa Invalido";
                return result;
            }
            else
            {
                numeroDocumento = documento.NUMERO_DOCUMENTO;
            }


            try
            {


                if (files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        string fileName = file.FileName;
                        fileName = file.FileName.Replace(" ", "_");
                        var findPlacaFoto =
                            await _repository.GetByNumeroDocumentoAdjunto(codigoDocumento, fileName);
                        if (findPlacaFoto != null)
                        {
                            result.Data = null;
                            result.IsValid = false;
                            result.Message = $"Ya existe la foto: {file.FileName}";
                            return result;
                        }

                        if (fileName.Length > 13)
                        {

                            var longitud = file.FileName.Length - 14;
                            titulo = fileName.Substring(14, longitud);

                        }

                        if (!Directory.Exists($"{destino}{codigoDocumento}"))
                        {
                            Directory.CreateDirectory($"{destino}{codigoDocumento}");
                        }
                        var filePatch = $"{destino}{codigoDocumento}/{fileName}";
                        if (!File.Exists($"{filePatch}"))
                        {
                            using (var stream = System.IO.File.Create(filePatch))
                            {
                                await file.CopyToAsync(stream);
                            }
                        }
                        if (File.Exists($"{filePatch}"))
                        {
                            RhDocumentosAdjuntosUpdateDto dtoCreate = new RhDocumentosAdjuntosUpdateDto();
                            dtoCreate.CodigoDocumentoAdjunto = 0;
                            dtoCreate.CodigoDocumento = codigoDocumento;
                            dtoCreate.Adjunto = fileName;
                            dtoCreate.Titulo = titulo;
                            var created = await Create(dtoCreate);

                            MinFile(codigoDocumento.ToString(), fileName);

                        }
                    }
                }

                result = await GetByNumeroDocumento(codigoDocumento);


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



        public void MinFile(string nroDocumento,string foto)
        {
            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var destino = @settings.RhFiles;
            var filePatch = $"{destino}{nroDocumento}/{foto}";
            var newFoto = $"min_{foto}";
            var newfilePatch = $"{destino}{nroDocumento}/{newFoto}";

            if (System.IO.File.Exists(filePatch))
            {
                using (MagickImage oMagickImage = new MagickImage(filePatch))
                {
                    oMagickImage.Resize(900,0);
                    oMagickImage.Write(newfilePatch);
                }
                
            }
        }

        public async Task<ResultDto<List<RhDocumentosAdjuntosResponseDto>>> AddImageModel(RhDocumentosFilesUpdateDto dto)
        {
         

            var settings = _configuration.GetSection("Settings").Get<Settings>();
            var destino = @settings.RhFiles;
            if (!Directory.Exists($"{destino}{dto.CodigoDocumento}"))
            {
                Directory.CreateDirectory($"{destino}{dto.CodigoDocumento}");
            }
            ResultDto<List<RhDocumentosAdjuntosResponseDto>> result = new ResultDto<List<RhDocumentosAdjuntosResponseDto>>(null);
            try
            {
                var numeroPlacaObj = await _documentosRepository.GetByCodigo(dto.CodigoDocumento);
                if (numeroPlacaObj == null)
                {
                    result.Data = null;
                    result.IsValid = false;
                    result.Message = "Numero Documento Invalido";
                    return result;
                }
                if (dto.Files.Count > 0)
                {
                    foreach (var file in dto.Files)
                    {
                        var filePatch = $"{destino}{dto.CodigoDocumento}/{file.FileName}";
                        using (var stream = System.IO.File.Create(filePatch))
                        {
                            await file.CopyToAsync(stream);
                        }
                        if (File.Exists($"{filePatch}"))
                        {
                            RhDocumentosAdjuntosUpdateDto dtoCreate = new RhDocumentosAdjuntosUpdateDto();
                            dtoCreate.CodigoDocumentoAdjunto = 0;
                            dtoCreate.CodigoDocumento = dto.CodigoDocumento;
                          
                            dtoCreate.Adjunto = file.FileName;
                            dtoCreate.Titulo = file.FileName;
                            var created = await Create(dtoCreate);

                        }
                    }
                }

                result = await GetByNumeroDocumento(dto.CodigoDocumento);


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

      

     
       

    }
}