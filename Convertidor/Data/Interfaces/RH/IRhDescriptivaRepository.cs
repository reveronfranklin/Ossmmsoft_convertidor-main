﻿namespace Convertidor.Data.Interfaces.RH
{
	public interface IRhDescriptivaRepository
	{

        Task<RH_DESCRIPTIVAS> GetByCodigoDescriptiva(int descripcionId);
        Task<List<RH_DESCRIPTIVAS>> GetAll();
        Task<List<RH_DESCRIPTIVAS>> GetByTituloId(int tituloId);
        Task<bool> GetByIdAndTitulo(int tituloId, int id);
        
	}
}

