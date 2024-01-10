using Convertidor.Dtos;
using Convertidor.Dtos.Catastro;

namespace Convertidor.Services.Catastro
{
	public interface ICAT_FICHAService
	{

        Task<ResultDto<GetCatFichaDto>> GetByKey(int codigoCatFicha);
    }
}

