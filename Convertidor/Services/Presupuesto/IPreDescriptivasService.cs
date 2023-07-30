using System;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPreDescriptivasService
	{
        Task<ResultDto<List<TreePUC>>> GetTreeDecriptiva();
        Task<ResultDto<List<PreDescriptivasGetDto>>> GetAll();

    }
}

