using Convertidor.Dtos.Bm;
namespace Convertidor.Services.Bm
{
	public interface IBmClasificacionBienesService
    {
        Task<ResultDto<List<BmClasificacionBienesResponseDto>>> GetAll();
        
        Task<ResultDto<BmClasificacionBienesResponseDto>> Update(BmClasificacionBienesUpdateDto dto);
        Task<ResultDto<BmClasificacionBienesResponseDto>> Create(BmClasificacionBienesUpdateDto dto);
        Task<ResultDto<BmClasificacionBienesDeleteDto>> Delete(BmClasificacionBienesDeleteDto dto);

    }
}

