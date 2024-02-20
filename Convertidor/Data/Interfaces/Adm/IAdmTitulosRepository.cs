using Convertidor.Data.Entities.ADM;

namespace Convertidor.Data.Interfaces.Adm
{
	public interface IAdmTitulosRepository
	{

        Task<ADM_TITULOS> GetByCodigo(int tituloId);
        Task<List<ADM_TITULOS>> GetAll();
        Task<ResultDto<ADM_TITULOS>> Add(ADM_TITULOS entity);
        Task<ResultDto<ADM_TITULOS>> Update(ADM_TITULOS entity);
        Task<string> Delete(int tituloId);
        Task<int> GetNextKey();
        Task<ADM_TITULOS> GetByCodigoString(string codigo);
    }
}

