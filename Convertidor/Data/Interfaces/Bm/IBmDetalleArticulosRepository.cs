using System;
using Convertidor.Data.Entities.Bm;
using Convertidor.Dtos;

namespace Convertidor.Data.Interfaces.Bm
{
    public interface IBmDetalleArticulosRepository
    {
        Task<BM_DETALLE_ARTICULOS> GetByCodigoArticulo(int codigoArticulo);
        Task<BM_DETALLE_ARTICULOS> GetByCodigoDetalleArticulo(int codigoDetalleArticulo);
  
        Task<List<BM_DETALLE_ARTICULOS>> GetAll();
        Task<ResultDto<BM_DETALLE_ARTICULOS>> Add(BM_DETALLE_ARTICULOS entity);
        Task<ResultDto<BM_DETALLE_ARTICULOS>> Update(BM_DETALLE_ARTICULOS entity);
        Task<string> Delete(int codigoArticulo);
        Task<int> GetNextKey();
        
    }
}

