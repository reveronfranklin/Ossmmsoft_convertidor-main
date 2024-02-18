using System;
using System.Collections.Generic;
using System.Globalization;
using Convertidor.Data.Entities.Bm;
using Convertidor.Data.Interfaces.Bm;
using Convertidor.Data.Repository.Rh;
using Convertidor.Dtos.Bm;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using NuGet.Packaging;

namespace Convertidor.Services.Bm
{
    public class BmBienesFotoService: IBmBienesFotoService
    {

      
        private readonly IBmBienesFotoRepository _repository;
        private readonly IBmBienesRepository _bienesRepository;
        private readonly ISisUsuarioRepository _sisUsuarioRepository;
        private readonly IConfiguration _configuration;
        public BmBienesFotoService(IBmBienesFotoRepository repository,
                                    IBmBienesRepository bienesRepository,
                                      ISisUsuarioRepository sisUsuarioRepository,
                                      IConfiguration configuration)
		{
            _repository = repository;
            _bienesRepository = bienesRepository;
            _sisUsuarioRepository = sisUsuarioRepository;
            _configuration = configuration;
           

        }

        public FechaDto GetFechaDto(DateTime fecha)
        {
            var FechaDesdeObj = new FechaDto();
            FechaDesdeObj.Year = fecha.Year.ToString();
            string month = "00" + fecha.Month.ToString();
            string day = "00" + fecha.Day.ToString();
            FechaDesdeObj.Month = month.Substring(month.Length - 2);
            FechaDesdeObj.Day = day.Substring(day.Length - 2);

            return FechaDesdeObj;
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
                if (created.IsValid && created.Data!=null)
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
        public string[] GetFicheros( string ruta )
        {
            
            string[] ficheros = Directory.GetFiles(ruta);
            string[] sorted = ficheros.OrderByDescending(o => o).ToArray();
            return sorted;
        }
        public static bool IsBase64(string base64String) {
            // Credit: oybek https://stackoverflow.com/users/794764/oybek
            if (string.IsNullOrEmpty(base64String) || base64String.Length % 4 != 0
                                                   || base64String.Contains(" ") || base64String.Contains("\t") || base64String.Contains("\r") || base64String.Contains("\n"))
                return false;

            try{
                Convert.FromBase64String(base64String);
                return true;
            }
            catch(Exception exception){
                // Handle the exception
            }
            return false;
        }
        public async Task<ResultDto<List<BmBienesFotoResponseDto>>> AddImage(int codigoBien,List<IFormFile> files)
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
                        fileName =file.FileName.Replace(" ", "_");
                        var findPlacaFoto =
                            await _repository.GetByNumeroPlacaFoto(numeroPlacaObj.NUMERO_PLACA, fileName);
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
                      
                        if (!Directory.Exists($"{destino}{numeroPlaca}"))
                        {
                            Directory.CreateDirectory($"{destino}{numeroPlaca}");
                        }
                        var filePatch = $"{destino}{numeroPlaca}/{fileName}";
                        if (!File.Exists($"{filePatch}"))
                        {
                            using (var stream =System.IO.File.Create(filePatch) )
                            {
                                await  file.CopyToAsync(stream);
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
                            var created=await Create(dtoCreate);
                            
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
                        using (var stream =System.IO.File.Create(filePatch) )
                        {
                           await  file.CopyToAsync(stream);
                        }
                        if (File.Exists($"{filePatch}"))
                        {
                            BmBienesFotoUpdateDto dtoCreate = new BmBienesFotoUpdateDto();
                            dtoCreate.CodigoBienFoto = 0;
                            dtoCreate.CodigoBien = numeroPlacaObj.CODIGO_BIEN;
                            dtoCreate.NumeroPlaca = numeroPlaca;
                            dtoCreate.Foto = file.FileName;
                            dtoCreate.Titulo = file.FileName;
                            var created=await Create(dtoCreate);
                            
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
                    
                    fileName=fileName.Replace(" ", "_");
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


    }
}

