using Convertidor.Dtos;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Adm
{
	public interface IAdmDescriptivasService
	{
        Task<ResultDto<List<TreePUC>>> GetTreeDecriptiva();
        Task<ResultDto<List<AdmDescriptivasGetDto>>> GetAll();
        Task<ResultDto<AdmDescriptivasGetDto>> Update(AdmDescriptivasUpdateDto dto);
        Task<ResultDto<AdmDescriptivasGetDto>> Create(AdmDescriptivasUpdateDto dto);
        Task<ResultDto<AdmDescriptivaDeleteDto>> Delete(AdmDescriptivaDeleteDto dto);
        Task<ResultDto<List<AdmDescriptivasGetDto>>> GetByTitulo(int tituloId);
        Task<ResultDto<List<AdmDescriptivasGetDto>>> GetByCodigoTitulo(string codigo);
    }
}

