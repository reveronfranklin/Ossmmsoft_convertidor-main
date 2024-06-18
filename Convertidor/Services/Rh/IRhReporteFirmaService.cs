namespace Convertidor.Services.Rh;

public interface IRhReporteFirmaService
{
    Task<ResultDto<List<RhReporteFirmaResponseDto>>> GetAll();
}