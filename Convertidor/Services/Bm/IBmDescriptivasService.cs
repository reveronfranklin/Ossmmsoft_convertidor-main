using System;
using Convertidor.Dtos;
using Convertidor.Dtos.Bm;
namespace Convertidor.Services.Bm
{
	public interface IBmDescriptivasService
	{
        Task<ResultDto<List<BmTreePUC>>> GetTreeDecriptiva();
        Task<ResultDto<List<BmDescriptivasGetDto>>> GetAll();
        Task<ResultDto<BmDescriptivasGetDto>> Update(BmDescriptivasUpdateDto dto);
        Task<ResultDto<BmDescriptivasGetDto>> Create(BmDescriptivasUpdateDto dto);
        Task<ResultDto<BmDescriptivaDeleteDto>> Delete(BmDescriptivaDeleteDto dto);
        Task<bool> GetByIdAndTitulo(int tituloId, int id);
        Task<ResultDto<List<BmDescriptivasGetDto>>> GetByTitulo(int tituloId);
        Task<ResultDto<List<BmDescriptivasGetDto>>> GetByCodigoTitulo(string codigo);
    }
}

