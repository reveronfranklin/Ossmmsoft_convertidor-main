using System;
using Convertidor.Dtos.Bm;
namespace Convertidor.Services.Bm
{
	public interface IBmDescriptivasService
	{
        Task<ResultDto<List<BmTreePUC>>> GetTreeDecriptiva();
        Task<ResultDto<List<BmDescriptivasResponseDto>>> GetAll();
        Task<ResultDto<BmDescriptivasResponseDto>> Update(BmDescriptivasUpdateDto dto);
        Task<ResultDto<BmDescriptivasResponseDto>> Create(BmDescriptivasUpdateDto dto);
        Task<ResultDto<BmDescriptivaDeleteDto>> Delete(BmDescriptivaDeleteDto dto);
        Task<bool> GetByIdAndTitulo(int tituloId, int id);
  
        Task<List<SelectListDescriptiva>> GetByTitulo(int tituloId);
        Task<ResultDto<List<BmDescriptivasResponseDto>>> GetByCodigoTitulo(string codigo);
    }
}

