﻿namespace Convertidor.Services.Rh
{
	public interface IRhDescriptivasService
	{

        Task<string> GetDescripcionByCodigoDescriptiva(int descripcionId);
        Task<List<RH_DESCRIPTIVAS>> GetAll();
        Task<List<SelectListDescriptiva>> GetByTitulo(int tituloId);
        Task<RH_DESCRIPTIVAS> GetByCodigoDescriptiva(int descripcionId);
	}
}

