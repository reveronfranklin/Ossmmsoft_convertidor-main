using Convertidor.Data.Entities.Adm;

namespace Convertidor.Data.Interfaces.Adm;

public interface IAdmAdmProveedoresContactoRepository
{
    Task<List<ADM_V_PAGAR_A_LA_OP_TERCEROS>> GetAll();
}