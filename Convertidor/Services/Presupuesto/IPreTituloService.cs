using System;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPreTituloService
	{


        Task<ResultDto<List<PreTitulosGetDto>>> GetAll();
        Task<ResultDto<List<TreePUC>>> GetTreeTitulos();

    }
}

