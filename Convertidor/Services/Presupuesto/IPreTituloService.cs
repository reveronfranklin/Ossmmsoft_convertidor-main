using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPreTituloService
	{


        Task<ResultDto<List<PreTitulosGetDto>>> GetAll();
        Task<ResultDto<List<TreePUC>>> GetTreeTitulos();
        Task<ResultDto<PreTitulosGetDto>> Update(PreTitulosUpdateDto dto);
        Task<ResultDto<PreTitulosGetDto>> Create(PreTitulosUpdateDto dto);
        Task<ResultDto<PreTitulosDeleteDto>> Delete(PreTitulosDeleteDto dto);


    }
}

