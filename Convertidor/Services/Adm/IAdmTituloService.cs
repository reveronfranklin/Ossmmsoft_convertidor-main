using Convertidor.Dtos;
using Convertidor.Dtos.Adm;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Adm
{
	public interface IAdmTituloService
	{


        Task<ResultDto<List<AdmTitulosGetDto>>> GetAll();
        Task<ResultDto<List<TreePUC>>> GetTreeTitulos();
        Task<ResultDto<AdmTitulosGetDto>> Update(AdmTitulosUpdateDto dto);
        Task<ResultDto<AdmTitulosGetDto>> Create(AdmTitulosUpdateDto dto);
        Task<ResultDto<AdmTitulosDeleteDto>> Delete(AdmTitulosDeleteDto dto);


    }
}

