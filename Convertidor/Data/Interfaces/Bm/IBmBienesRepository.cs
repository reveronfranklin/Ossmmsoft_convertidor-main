using System;
using Convertidor.Data.Entities.Bm;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.Bm
{
    public interface IBmBienesRepository
	{

        Task<BM_BIENES> GetByCodigoBien(int codigoBien);
        Task<BM_BIENES> GetByCodigoArticulo(int codigoArticulo);
        Task<List<BM_BIENES>> GetByProveedor(int codigoProveedor);
        Task<List<BM_BIENES>> GetByOrdenCompra(int codigoOrdenCompra);
        Task<BM_BIENES> GetByNumeroPlaca(string numeroPlaca);
        Task<List<BM_BIENES>> GetAll();
        Task<ResultDto<BM_BIENES>> Add(BM_BIENES entity);
        Task<ResultDto<BM_BIENES>> Update(BM_BIENES entity);
        Task<string> Delete(int codigoBien);
        Task<int> GetNextKey();
        //Task<string> GetNextKeyNumeroPlaca();


    }
}

