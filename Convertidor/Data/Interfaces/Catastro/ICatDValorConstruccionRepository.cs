using Convertidor.Data.Entities.Catastro;

namespace Convertidor.Data.Interfaces.Catastro
{
    public interface ICatDValorConstruccionRepository
    {
        Task<List<CAT_D_VALOR_CONSTRUCCION>> GetAll();
        Task<CAT_D_VALOR_CONSTRUCCION> GetByCodigo(int codigoParcela);
        Task<ResultDto<CAT_D_VALOR_CONSTRUCCION>> Add(CAT_D_VALOR_CONSTRUCCION entity);
        Task<ResultDto<CAT_D_VALOR_CONSTRUCCION>> Update(CAT_D_VALOR_CONSTRUCCION entity);
        Task<string> Delete(int codigoParcela);
        Task<int> GetNextKey();
    }
}
