namespace Convertidor.Services.Destino.ADM;

public interface IAdmOrdenPagoDestinoService
{
    Task<ResultDto<bool>> CopiarOrdenPago(int codigoOrdenPago);
}