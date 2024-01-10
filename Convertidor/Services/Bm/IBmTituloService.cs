using System;
using Convertidor.Dtos;
using Convertidor.Dtos.Bm;


namespace Convertidor.Services.Bm
{
	public interface IBmTituloService
	{


        Task<ResultDto<List<BmTitulosGetDto>>> GetAll();
        Task<ResultDto<List<BmTreePUC>>> GetTreeTitulos();
        Task<ResultDto<BmTitulosGetDto>> Update(BmTitulosUpdateDto dto);
        Task<ResultDto<BmTitulosGetDto>> Create(BmTitulosUpdateDto dto);
        Task<ResultDto<BmTitulosDeleteDto>> Delete(BmTitulosDeleteDto dto);


    }
}

