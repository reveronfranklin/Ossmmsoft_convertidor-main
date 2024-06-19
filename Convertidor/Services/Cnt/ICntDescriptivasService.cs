using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Cnt;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Cnt
{
	public interface ICntDescriptivasService
    {

        Task<ResultDto<List<TreePUC>>> GetTreeDescriptiva();
        Task<ResultDto<List<CntDescriptivasResponseDto>>> GetAll();
        Task<ResultDto<CntDescriptivasResponseDto>> Update(CntDescriptivasUpdateDto dto);
        Task<ResultDto<CntDescriptivasResponseDto>> Create(CntDescriptivasUpdateDto dto);
        Task<ResultDto<CntDescriptivasDeleteDto>> Delete(CntDescriptivasDeleteDto dto);
     

    }
}

