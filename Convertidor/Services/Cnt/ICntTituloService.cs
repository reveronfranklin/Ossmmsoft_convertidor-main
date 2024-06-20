using Convertidor.Dtos.Cnt;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Cnt
{
    public interface ICntTituloService
	{


        Task<ResultDto<List<CntTitulosResponseDto>>> GetAll();
        Task<ResultDto<CntTitulosResponseDto>> Update(CntTitulosUpdateDto dto);
        Task<ResultDto<CntTitulosResponseDto>> Create(CntTitulosUpdateDto dto);
        Task<ResultDto<CntTitulosDeleteDto>> Delete(CntTitulosDeleteDto dto);


    }
}

