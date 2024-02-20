using System;
using Convertidor.Dtos.Bm;


namespace Convertidor.Services.Bm
{
	public interface IBmTituloService
	{


        Task<ResultDto<List<BmTitulosResponseDto>>> GetAll();
        Task<ResultDto<List<BmTreePUC>>> GetTreeTitulos();
        Task<ResultDto<BmTitulosResponseDto>> Update(BmTitulosUpdateDto dto);
        Task<ResultDto<BmTitulosResponseDto>> Create(BmTitulosUpdateDto dto);
        Task<ResultDto<BmTitulosDeleteDto>> Delete(BmTitulosDeleteDto dto);


    }
}

