using Ganss.Excel;

namespace Convertidor.Services.Adm.Pagos.AdmPagoElectronicoService;

public partial class AdmPagoElectronicoService 
{
            public async Task<ResultDto<string>> GenerateFilePagoElectronico(int codigoLote,int usuario)
        {
            ResultDto<string> result = new ResultDto<string>(null);

            try
            {
                var lote = await _admLotePagoRepository.GetByCodigo(codigoLote);
                if (lote == null)
                {
                    result.Data = "Lote No existe";
                    result.IsValid = true;
                    result.Message = "Lote No existe";
                    result.LinkData = "";
                    return result;
                }
            
                var pagoElectronico = await _repository.GetByLote((int)lote.CODIGO_EMPRESA,codigoLote,(int)lote.CODIGO_PRESUPUESTO,usuario);
                if (pagoElectronico.Data != null)
                {  
                    lote = await _admLotePagoRepository.GetByCodigo(codigoLote);
                    var dataLote = await _admLotePagoRepository.GetByCodigo(codigoLote);
                    var data= Map(pagoElectronico.Data);
                    ExcelMapper mapper = new ExcelMapper();
                    var settings = _configuration.GetSection("Settings").Get<Settings>();
                    var ruta = @settings.ExcelFiles;
                    var fileNameTxt = lote.FILE_NAME;
                    string newFileTxt = Path.Combine(Directory.GetCurrentDirectory(), ruta, fileNameTxt);
                    if (File.Exists(newFileTxt))
                    { 
                        File.Delete(newFileTxt);
                    }
                    using(TextWriter tw = new StreamWriter(newFileTxt))
                    {
                        foreach (var s in data)
                            tw.WriteLine(s);
                        tw.Close();
                    }
                    var linkData = $"{fileNameTxt}";
                    result.IsValid = true;
                    result.Message = "";
                    result.LinkData = linkData;
                    result.LinkDataArlternative= linkData;
                    return result;
                }

                return result;
            }
            catch (Exception e)
            {
                result.Data = null;
                result.IsValid = true;
                result.Message = "";
                result.LinkData = "";
                return result;
            }
           
        }
        

}