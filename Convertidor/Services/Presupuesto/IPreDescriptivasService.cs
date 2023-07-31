﻿using System;
using Convertidor.Dtos;
using Convertidor.Dtos.Presupuesto;

namespace Convertidor.Services.Presupuesto
{
	public interface IPreDescriptivasService
	{
        Task<ResultDto<List<TreePUC>>> GetTreeDecriptiva();
        Task<ResultDto<List<PreDescriptivasGetDto>>> GetAll();
        Task<ResultDto<PreDescriptivasGetDto>> Update(PreDescriptivasUpdateDto dto);
        Task<ResultDto<PreDescriptivasGetDto>> Create(PreDescriptivasUpdateDto dto);
        Task<ResultDto<PreDescriptivaDeleteDto>> Delete(PreDescriptivaDeleteDto dto);
    }
}

