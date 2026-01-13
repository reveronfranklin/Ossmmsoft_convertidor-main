namespace Convertidor.Services.Adm.Pagos;

public interface IAdmPagoElectronicoService
{
    Task<ResultDto<string>> GenerateFilePagoElectronico(int codigoLote, int usuario);
}