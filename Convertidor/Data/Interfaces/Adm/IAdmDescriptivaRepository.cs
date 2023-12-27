﻿using System;
using Convertidor.Data.Entities.Adm;
using Convertidor.Data.Entities.Presupuesto;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.Adm
{
	public interface IAdmDescriptivaRepository
	{

        Task<ADM_DESCRIPTIVAS> GetByCodigoDescriptiva(int descripcionId);
        Task<List<ADM_DESCRIPTIVAS>> GetByTitulo(int tituloId);
        Task<List<ADM_DESCRIPTIVAS>> GetAll();
        Task<ADM_DESCRIPTIVAS> GetByCodigo(int descripcionId);
        Task<ResultDto<ADM_DESCRIPTIVAS>> Add(ADM_DESCRIPTIVAS entity);
        Task<ResultDto<ADM_DESCRIPTIVAS>> Update(ADM_DESCRIPTIVAS entity);
        Task<string> Delete(int descripcionId);
        Task<int> GetNextKey();
        Task<ADM_DESCRIPTIVAS> GetByCodigoDescriptivaTexto(string codigo);
        Task<List<ADM_DESCRIPTIVAS>> GetByFKID(int descripcionIdFk);
    }
}

