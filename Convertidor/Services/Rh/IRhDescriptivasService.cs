using System;
namespace Convertidor.Services.Rh
{
	public interface IRhDescriptivasService
	{

        Task<string> GetDescripcionByCodigoDescriptiva(int descripcionId);

    }
}

